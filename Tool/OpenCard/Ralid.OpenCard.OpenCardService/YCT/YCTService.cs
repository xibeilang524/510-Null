﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    public class YCTService : IOpenCardService
    {
        #region 构造函数
        public YCTService()
        {
        }

        public YCTService(YCTSetting setting)
        {
            Setting = setting;
        }
        #endregion

        #region 私有变量
        private List<YCTItem> _Readers = new List<YCTItem>();
        private Dictionary<YCTItem, Thread> _PollRoutes = new Dictionary<YCTItem, Thread>();
        private Timer _ChkComport = null;
        #endregion

        #region 公共属性
        public YCTSetting Setting { get; set; }
        #endregion

        #region 私有方法
        private void CheckComport(object state)
        {
            foreach (var item in _Readers)
            {
                if (item.Reader != null && !item.Reader.IsOpened)
                {
                    item.Reader.Open();
                    if (item.Reader.IsOpened) //需要正常初始化后才能加到列表中
                    {
                        item.Reader.SetServiceCode(Setting.ServiceCode);
                        item.Reader.InitPaidMode();
                    }
                }
            }
        }

        private void PollRoute(object obj)
        {
            YCTItem item = obj as YCTItem;
            if (item == null || item.Reader == null) return;
            EntranceInfo entrance = null;
            if (item.EntranceID != null) entrance = ParkBuffer.Current.GetEntrance(item.EntranceID.Value);
            try
            {
                while (true)
                {
                    if (item.Reader.IsOpened)
                    {
                        YCTWallet w = item.Reader.Poll();
                        if (w != null)
                        {
                            //此处应该先判断黑名单
                            if (InBlackList(w.PhysicalCardID, w.LogicCardID))
                            {
                                item.Reader.CatchBlackList();
                            }
                            else
                            {
                                HandleWallet(item, w, entrance);
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        private bool InBlackList(string physicalCardID, string logicCardID)
        {
            return false;
        }

        private void HandleWallet(YCTItem item, YCTWallet w, EntranceInfo entrance)
        {
            if (entrance != null && !entrance.IsExitDevice) //入口
            {
                OpenCardEventArgs args = new OpenCardEventArgs()
                {
                    CardID = w.PhysicalCardID,
                    CardType = "羊城通",
                    EntranceID = entrance.EntranceID,
                    EntranceName = entrance.EntranceName,
                };
                if (this.OnReadCard != null) this.OnReadCard(this, args);
            }
            else
            {
                OpenCardEventArgs args = new OpenCardEventArgs();
                args.CardID = w.PhysicalCardID;
                if (entrance != null)
                {
                    args.EntranceID = entrance.EntranceID;
                    args.EntranceName = entrance.EntranceName;
                }
                else
                {
                    args.EntranceName = "中央收费";
                }
                if (this.OnPaying != null)
                {
                    this.OnPaying(this, args); //产生收费事件
                    if (args.Payment == null) return;
                    if (args.Payment.Accounts == 0) //不用收费直接返回收款成功事件
                    {
                        if (this.OnPaidOk != null) this.OnPaidOk(this, args);
                    }
                    else //扣费
                    {
                        if (Paid(item, w, args.Payment.Accounts))
                        {
                            args.Paid = args.Payment.Accounts;
                            if (this.OnPaidOk != null) this.OnPaidOk(this, args);
                        }
                        else
                        {
                            args.LastError = item.Reader.LastError.ToString();
                            if (this.OnPaidFail != null) this.OnPaidFail(this, args);
                        }
                    }
                }
            }
        }

        private bool Paid(YCTItem item, YCTWallet w, decimal paid)
        {
            YCTPaymentRecord record = item.Reader.Prepaid((int)(paid * 100));
            if (record == null) return false;
            //这里应该保存记录,保存记录成功然后再进行下一步
            YCTPaymentRecordBll bll = new YCTPaymentRecordBll(AppSettings.CurrentSetting.MasterParkConnect);
            CommandResult result = bll.Insert(record);
            if (result.Result != ResultCode.Successful) return false;

            string tac = item.Reader.CompletePaid();
            if (string.IsNullOrEmpty(tac))
            {
                int err = item.Reader.LastError;
                if (err == 0x01) bll.Delete(record); //失败 删除记录
                return false;
            }
            YCTPaymentRecord newVal = record.Clone();
            if (w.WalletType == 0x02)
            {
                newVal.TAC = tac;
            }
            newVal.UploadString = string.Empty; //这里要将记录的字符串生成好
            newVal.State = YCTPaymentRecordState.Completed; //标记为完成
            result = bll.Update(newVal, record);
            return result.Result == ResultCode.Successful;
        }
        #endregion

        #region 实现接口IOpenCardService
        public event EventHandler<OpenCardEventArgs> OnReadCard;

        public event EventHandler<OpenCardEventArgs> OnPaying;

        public event EventHandler<OpenCardEventArgs> OnPaidOk;

        public event EventHandler<OpenCardEventArgs> OnPaidFail;

        public void Init()
        {
            if (Setting == null) throw new InvalidOperationException("没有提供羊城通参数");
            if (_ChkComport != null) _ChkComport.Dispose();
            Dictionary<YCTPOS, YCTItem> temp = new Dictionary<YCTPOS, YCTItem>();
            List<YCTItem> keys = _Readers.ToList();
            if (keys != null && keys.Count > 0)//将所有不在新设置中的读卡器删除
            {
                foreach (var key in keys)
                {
                    var item = Setting.Items != null ? Setting.Items.SingleOrDefault(it => it.Comport == key.Comport) : null;
                    if (item == null)
                    {
                        var reader = key.Reader;
                        reader.Close();
                        _Readers.Remove(key);
                        if (_PollRoutes.ContainsKey(key))
                        {
                            Thread t = _PollRoutes[key];
                            if (t.ThreadState != ThreadState.Stopped) t.Abort();
                            _PollRoutes.Remove(key);
                        }
                    }
                    else
                    {
                        key.EntranceID = item.EntranceID;
                    }
                }
            }
            if (Setting.Items != null)
            {
                foreach (var item in Setting.Items)
                {
                    if (_Readers == null || !_Readers.Exists(it => it.Comport == item.Comport))
                    {
                        var reader = new YCTPOS((byte)item.Comport, 57600);
                        reader.Open();
                        item.Reader = reader;
                        _Readers.Add(item);
                        Thread t = new Thread(new ParameterizedThreadStart(PollRoute));
                        t.IsBackground = true;
                        _PollRoutes[item] = t;
                        t.Start(item);
                        if (reader.IsOpened) //需要正常初始化后才能加到列表中
                        {
                            reader.SetServiceCode(Setting.ServiceCode);
                            reader.InitPaidMode();
                        }
                    }
                }
            }
            _ChkComport = new Timer(new TimerCallback(CheckComport), null, 5000, 10000); //启动检查串口状态的定时器
        }

        public void Dispose()
        {
            if (_ChkComport != null) _ChkComport.Dispose();
            foreach (var item in _Readers)
            {
                if (item.Reader != null && item.Reader.IsOpened) item.Reader.Close();
            }
            _Readers.Clear();
        }
        #endregion
    }
}
