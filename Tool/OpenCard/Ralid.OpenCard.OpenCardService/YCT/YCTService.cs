using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
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
        #endregion

        #region 公共属性
        public YCTSetting Setting { get; set; }
        #endregion

        #region 私有方法
        private void PollRoute(object obj)
        {
            YCTItem item = obj as YCTItem;
            if (item == null) return;
            if (item.Reader == null || !item.Reader.IsOpened) return;
            EntranceInfo entrance = null;
            if (item.EntranceID != null) entrance = ParkBuffer.Current.GetEntrance(item.EntranceID.Value);
            try
            {
                while (item.Reader.IsOpened)
                {
                    YCTWallet w = item.Reader.Poll();
                    if (w != null)
                    {
                        HandleWallet(w, entrance);
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        private void HandleWallet(YCTWallet w, EntranceInfo entrance)
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

                    }
                }
            }
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
            Dictionary<YCTReader, YCTItem> temp = new Dictionary<YCTReader, YCTItem>();
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
                        var reader = new YCTReader((byte)item.Comport, 57600);
                        reader.Open();
                        if (reader.IsOpened) //需要正常初始化后才能加到列表中
                        {
                            bool sucess = reader.SetServiceCode(Setting.ServiceCode);
                            if (sucess) reader.InitPaidMode();
                            if (sucess)
                            {
                                item.Reader = reader;
                                _Readers.Add(item);
                                Thread t = new Thread(new ParameterizedThreadStart(PollRoute));
                                t.IsBackground = true;
                                _PollRoutes[item] = t;
                                t.Start(item);
                            }
                            else
                            {
                                reader.Close(); //没有正常的读卡器最后要关闭
                            }
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (var item in _Readers)
            {
                if (item.Reader != null && item.Reader.IsOpened) item.Reader.Close();
            }
            _Readers.Clear();
        }
        #endregion
    }
}
