using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using Ralid.OpenCard.OpenCardService;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.GeneralLibrary.CardReader;

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
            string lastCard = null;
            DateTime lastDT = DateTime.Now;
            YCTItem item = obj as YCTItem;
            if (item == null || item.Reader == null) return;
            try
            {
                while (true)
                {
                    Thread.Sleep(500);
                    if (item.Reader.IsOpened)
                    {
                        if (item.Reader.LastError == -1) //如果是没有响应,则说明有可能是断电了,则需要将服务代码重新下发
                        {
                            item.Reader.SetServiceCode(Setting.ServiceCode);
                        }
                        else
                        {
                            YCTWallet w = item.Reader.Poll();
                            if (w != null)
                            {
                                if (w.LogicCardID == lastCard && CalInterval(lastDT, DateTime.Now) < 3) continue; //同一张卡间隔至少要3秒才处理
                                lastCard = w.LogicCardID;
                                lastDT = DateTime.Now;
                                var black = GetBlackList(w.LogicCardID, w.PhysicalCardID);
                                if (black != null)
                                {
                                    HandleBlacklist(item, w, black); //先处理黑名单
                                }
                                else
                                {
                                    HandleWallet(item, w);
                                }
                            }
                            else
                            {
                                if (item.Reader.LastError == 0x80) //没有卡片
                                {
                                }
                                else if (item.Reader.LastError == 0x83) //验证出错,说明卡片是其它IC卡,继续读其序列号
                                {
                                    string sn = item.Reader.ReadSN(UserSetting.Current != null && UserSetting.Current.WegenType == WegenType.Wengen26 ? 1 : 0);
                                    if (sn != null)
                                    {
                                        w = new YCTWallet() { LogicCardID = sn, PhysicalCardID = sn, CardType = string.Empty };
                                        HandleWallet(item, w);
                                    }
                                }
                                else if (item.Reader.LastError != 0x64 || item.Reader.LastError != 0x65) //屏蔽读卡和选择卡片错误
                                {
                                    HandleError(item, item.Reader.LastErrorDescr);
                                }
                            }
                        }
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        private double CalInterval(DateTime dt1, DateTime dt2)
        {
            TimeSpan ts = new TimeSpan(dt2.Ticks - dt1.Ticks);
            return ts.TotalSeconds;
        }

        private YCTBlacklist GetBlackList(string logicCardID, string physicalCardID)
        {
            var item = new YCTBlacklistBll(AppSettings.CurrentSetting.MasterParkConnect).GetByID(logicCardID).QueryObject;
            return item;
        }

        private void HandleWallet(YCTItem item, YCTWallet w)
        {
            EntranceInfo entrance = item.EntranceID.HasValue ? ParkBuffer.Current.GetEntrance(item.EntranceID.Value) : null;
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = w.LogicCardID,
                CardType = w.WalletType == 0 ? string.Empty : YCTSetting.CardTyte,
                Entrance = entrance,
                Balance = (decimal)w.Balance / 100,
            };
            if (args.CardType == YCTSetting.CardTyte)
            {
                ParkInfo p = ParkBuffer.Current.GetPark(entrance.ParkID);
                CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardByID(w.LogicCardID).QueryObject;
                if (card != null && (entrance == null || (!p.IsNested && entrance.IsExitDevice)))
                {
                    HandlePayment(item, w, args);//中央收费处和非嵌套车场的出口,并且是羊城通卡,则进行收费处理
                }
                else
                {
                    if (this.OnReadCard != null) this.OnReadCard(this, args);
                }
            }
            else
            {
                if (this.OnReadCard != null) this.OnReadCard(this, args);
            }
        }

        private void HandlePayment(YCTItem item, YCTWallet w, OpenCardEventArgs args)
        {
            if (this.OnPaying != null) this.OnPaying(this, args); //产生收费事件
            if (args.Payment == null) return;
            if (args.Payment.GetPaying() <= 0) //不用收费直接返回收款成功事件
            {
                args.Payment.PaymentCode = Ralid.Park.BusinessModel.Enum.PaymentCode.Computer;
                args.Payment.PaymentMode = Ralid.Park.BusinessModel.Enum.PaymentMode.YangChengTong;
                if (this.OnPaidOk != null) this.OnPaidOk(this, args);
            }
            else //扣费
            {
                //判断余额是否够扣费，否则返回"余额不足",注意钱包单位是分的，这里要转成分比较
                //因为CPU钱包里有一个余额下限，余额下限是不允许扣费的，如果不比较费用和余额，有可以会扣到余额下限
                if (((int)(args.Payment.GetPaying()* 100)) <= w.Balance)
                {
                    int balance;
                    if (Paid(item, w, args.Payment, out balance))
                    {
                        args.Paid = args.Payment.GetPaying();
                        args.Payment.PaymentCode = Ralid.Park.BusinessModel.Enum.PaymentCode.Computer;
                        args.Payment.PaymentMode = Ralid.Park.BusinessModel.Enum.PaymentMode.YangChengTong;
                        args.Balance = (decimal)balance / 100;
                        if (this.OnPaidOk != null) this.OnPaidOk(this, args);
                    }
                    else
                    {
                        args.LastError = item.Reader.LastErrorDescr;
                        if (this.OnPaidFail != null) this.OnPaidFail(this, args);
                    }
                }
                else
                {
                    args.LastError = "余额不足";
                    if (this.OnPaidFail != null) this.OnPaidFail(this, args);
                }
            }
        }

        private void HandleBlacklist(YCTItem item, YCTWallet w, YCTBlacklist black)
        {
            bool catched = item.Reader.CatchBlackList(); //捕捉黑名单
            if (catched && black.CatchAt == null) //之前没有捕捉日期的才写数据库
            {
                black.WalletType = w.WalletType;
                black.CatchAt = DateTime.Now;
                black.UploadFile = null;
                new YCTBlacklistBll(AppSettings.CurrentSetting.MasterParkConnect).Update(black);
            }
            EntranceInfo entrance = item.EntranceID.HasValue ? ParkBuffer.Current.GetEntrance(item.EntranceID.Value) : null;
            OpenCardEventArgs args = new OpenCardEventArgs()
            {
                CardID = w.LogicCardID,
                CardType = w.WalletType == 0 ? string.Empty : YCTSetting.CardTyte,
                Entrance = entrance,
                Balance = (decimal)w.Balance / 100,
                LastError = "黑名单卡",
            };
            if (this.OnError != null) this.OnError(this, args);
        }

        private void HandleError(YCTItem item, string error)
        {
            EntranceInfo entrance = item.EntranceID.HasValue ? ParkBuffer.Current.GetEntrance(item.EntranceID.Value) : null;
            if (this.OnError != null) //
            {
                OpenCardEventArgs args = new OpenCardEventArgs()
                {
                    CardID = "0",
                    CardType = YCTSetting.CardTyte,
                    Entrance = entrance,
                    LastError = error,
                };
                this.OnError(this, args);
            }
        }

        private bool Paid(YCTItem item, YCTWallet w, CardPaymentInfo paid, out int balance)
        {
            balance = 0;
            YCTPaymentInfo payment = item.Reader.Prepaid((int)(paid.GetPaying() * 100), w.WalletType, Setting.MaxOfflineMonth);
            if (payment == null) return false;
            //这里应该保存记录,保存记录成功然后再进行下一步
            YCTPaymentRecord record = CreateRecord(payment);
            record.WalletType = w.WalletType;
            record.EnterDateTime = paid.EnterDateTime.Value;
            record.State = YCTPaymentRecordState.PaidFail;
            YCTPaymentRecordBll bll = new YCTPaymentRecordBll(AppSettings.CurrentSetting.MasterParkConnect);
            CommandResult result = bll.Insert(record);
            if (result.Result != ResultCode.Successful) return false;

            string tac = item.Reader.CompletePaid();
            if (string.IsNullOrEmpty(tac))
            {
                int err = item.Reader.LastError;
                //if (err == 0x01) bll.Delete(record); //失败 删除记录
                return false;
            }
            YCTPaymentRecord newVal = record.Clone();
            if (w.WalletType == 0x02) newVal.TAC = tac; //cpu钱包将TAC写到记录中
            newVal.State = YCTPaymentRecordState.PaidOk; //标记为完成
            result = bll.Update(newVal, record);
            balance = record.BAL; //返回余额
            if (w.WalletType == 2) balance -= w.MinBalance; //CPU钱包可用余额为余额减去最小余额
            return result.Result == ResultCode.Successful;
        }

        private YCTPaymentRecord CreateRecord(YCTPaymentInfo payment)
        {
            YCTPaymentRecord record = new YCTPaymentRecord();
            record.PID = payment.本次交易设备编号;
            record.PSN = payment.终端交易流水号;
            record.TIM = payment.本次交易日期时间;
            record.FCN = payment.物理卡号;
            record.LCN = payment.逻辑卡号;
            record.TF = payment.交易金额;
            record.FEE = payment.票价;
            record.BAL = payment.本次余额;
            record.TT = payment.交易类型;
            record.ATT = payment.附加交易类型;
            record.CRN = payment.票卡充值交易计数;
            record.XRN = payment.票卡消费交易计数;
            record.DMON = payment.累计门槛月份;
            record.BDCT = payment.公交门槛计数;
            record.MDCT = payment.地铁门槛计数;
            record.UDCT = payment.联乘门槛计数;
            record.EPID = payment.本次交易入口设备编号;
            record.ETIM = payment.本次交易入口日期时间;
            record.LPID = payment.上次交易设备编号;
            record.LTIM = payment.上次交易日期时间;
            record.AREA = payment.区域代码;
            record.ACT = payment.区域卡类型;
            record.SAREA = payment.区域子码;
            record.TAC = payment.交易认证码;
            return record;
        }
        #endregion

        #region 实现接口IOpenCardService
        public event EventHandler<OpenCardEventArgs> OnReadCard;

        public event EventHandler<OpenCardEventArgs> OnPaying;

        public event EventHandler<OpenCardEventArgs> OnPaidOk;

        public event EventHandler<OpenCardEventArgs> OnPaidFail;

        public event EventHandler<OpenCardEventArgs> OnError;

        public void Init()
        {
            if (Setting == null) throw new InvalidOperationException("没有提供羊城通参数");
            if (_ChkComport != null) _ChkComport.Dispose();
            List<YCTItem> keys = _Readers.ToList();
            if (keys != null && keys.Count > 0)//将所有不在新设置中的读卡器删除
            {
                foreach (var key in keys)
                {
                    var item = Setting.Items != null ? Setting.Items.SingleOrDefault(it => it.ID == key.ID) : null;
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
                    if (_Readers == null || !_Readers.Exists(it => it.ID == item.ID))
                    {
                        var reader = new YCTPOS((byte)item.Comport, 57600);
                        reader.Open();
                        item.Reader = reader;
                        _Readers.Add(item);
                        if (reader.IsOpened) //需要正常初始化后才能加到列表中
                        {
                            reader.SetServiceCode(Setting.ServiceCode);
                            reader.InitPaidMode();
                        }
                        Thread t = new Thread(new ParameterizedThreadStart(PollRoute));
                        t.IsBackground = true;
                        _PollRoutes[item] = t;
                        t.Start(item);
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
                if (_PollRoutes.ContainsKey(item)) _PollRoutes[item].Abort();
            }
            _Readers.Clear();
        }
        #endregion
    }
}
