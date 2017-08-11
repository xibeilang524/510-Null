using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BLL;
using System.Threading;
using Ralid.Park.ParkAdapter;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Factory;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.OpenCard.OpenCardService
{
    public class OpenCardMessageHandler
    {
        #region 构造函数

        #endregion

        #region 私有变量
        private Dictionary<Type, IOpenCardService> _Services = new Dictionary<Type, IOpenCardService>();
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置是否启用日志记录功能
        /// </summary>
        public bool Debug { get; set; }
        #endregion

        #region 私有方法
        private bool SaveOpenCard(string cardID, CardType cardType, decimal balance, string cardSN = null)
        {
            CardInfo card = new CardInfo()
            {
                CardID = cardID,
                CardType = cardType,
                CarType = CarTypeSetting.DefaultCarType,
                CardNum = 1000,
                CardSN = cardSN,
                OwnerName = cardType.Name,
                Options = CardOptions.ForbidRepeatIn | CardOptions.ForbidRepeatOut | CardOptions.HolidayEnable | CardOptions.WithCount,
                Status = CardStatus.Enabled,
                ParkingStatus = ParkingStatus.Out,
                LastDateTime = DateTime.Now,
                LastEntrance = 0,
                ActivationDate = new DateTime(2000, 1, 1),
                ValidDate = new DateTime(2099, 12, 31),
                //Balance = balance,
            };
            return (new CardBll(AppSettings.CurrentSetting.MasterParkConnect)).AddCard(card).Result == ResultCode.Successful;
        }

        private CardPaymentInfo GetPaymentInfo(CardInfo card, OpenCardEventArgs e, DateTime dt)
        {
            CardPaymentInfo ret = null;
            IParkingAdapter pad = null;
            EntranceInfo entrance = e.Entrance;
            if (entrance != null)
            {
                pad = ParkingAdapterManager.Instance[entrance.RootParkID];
            }
            else //中央收费,默认使用卡片的入场停车场来扣费
            {
                entrance = ParkBuffer.Current.GetEntrance(card.LastEntrance);
                if (entrance != null)
                {
                    pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                }
                else
                {
                    if (ParkingAdapterManager.Instance != null && ParkingAdapterManager.Instance.ParkAdapters != null)
                        pad = ParkingAdapterManager.Instance.ParkAdapters[0];
                }
            }
            if (pad != null)
            {
                ret = pad.CreateCardPaymentRecord(card, card.CarType, dt);
            }
            return ret;
        }
        #endregion

        #region 事件处理程序
        private void s_OnReadCard(object sender, OpenCardEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CardID)) return;
            if (e.Entrance == null) return;  //没有指定通道的读卡事件丢掉不处理
            if (!e.Entrance.IsExitDevice) //入口刷卡时,如果卡片类型为开放卡片类型,则在系统中增加此卡片信息
            {
                CardType ct = string.IsNullOrEmpty(e.CardType) ? null : CustomCardTypeSetting.Current.GetCardType(e.CardType);
                if (ct != null)
                {
                    CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardByID(e.CardID).QueryObject;
                    if (card == null) SaveOpenCard(e.CardID, ct, e.Balance, e.CardSN);
                }
            }

            //通过远程读卡方式
            IParkingAdapter pad = ParkingAdapterManager.Instance[e.Entrance.RootParkID];
            if (pad != null)
            {
                var notify = new RemoteReadCardNotify(e.Entrance.RootParkID, e.Entrance.EntranceID, e.CardID, string.Empty,
                    OperatorInfo.CurrentOperator.OperatorName, WorkStationInfo.CurrentStation.StationName);
                string temp = AppSettings.CurrentSetting.GetConfigContent("RemoteReader");
                int reader = 0;
                if (!int.TryParse(temp, out reader)) reader = 0;
                notify.Reader = (EntranceReader)reader;
                if (!pad.RemoteReadCard(notify))
                {
                    if (Debug) Ralid.GeneralLibrary.LOG.FileLog.Log(e.Entrance.EntranceName, string.Format("【{0}】 读卡事件 远程读卡失败", e.CardID));
                }
                if (!string.IsNullOrEmpty(e.CardType)) //只有开放卡片才显示余额
                {
                    WaitCallback wc = (WaitCallback)((object state) =>
                    {
                        System.Threading.Thread.Sleep(AppSettings.CurrentSetting.GetShowBalanceInterval() * 1000);
                        pad.LedDisplay(new SetLedDisplayNotify(e.Entrance.EntranceID, CanAddress.TicketBoxLed, string.Format("余额{0}元", e.Balance), false, 0));
                    });
                    ThreadPool.QueueUserWorkItem(wc);
                }
            }
            if (this.OnReadCard != null) this.OnReadCard(sender, e);
        }

        private void s_OnPaying(object sender, OpenCardEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CardID)) return;

            //add by Jan 2016-04-27 增加对上次未完成的收费信息的处理
            CardPaymentInfo unPay = e.UnFinishedPayment;
            if (unPay != null && (unPay.Accounts > 0 || unPay.Discount > 0)) //只有要收费的记录才保存
            {
                //先保存上次未完成的收费信息的处理
                unPay.IsCenterCharge = true;
                unPay.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
                unPay.StationID = WorkStationInfo.CurrentStation.StationName;
                CommandResult ret = (new CardBll(AppSettings.CurrentSetting.MasterParkConnect)).PayParkFee(unPay);
                if (ret.Result != ResultCode.Successful)
                {
                    e.Payment = null;
                    return;//如果保存失败的，就不需要继续了
                }
            }
            //end add by  Jan 2016-04-27

            CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardByID(e.CardID).QueryObject;
            if (card == null) return;
            DateTime dt = e.ChargeDateTime.HasValue ? e.ChargeDateTime.Value : DateTime.Now; //add by Jan 2016-04-27 有输入计费时间时，用输入的计费时间
            CardPaymentInfo payment = GetPaymentInfo(card, e, dt);
            if (!card.IsInPark && payment != null) { payment.Accounts = 0; payment.Discount = 0; }//停车场获取收费信息时已经出场的卡片也会产生费用信息，所以这里对这种情况处理为应收0元
            e.Payment = payment;
            if (this.OnPaying != null) this.OnPaying(sender, e);

            //add by Jan 2016-04-27 清空上次未完成的收费信息
            e.UnFinishedPayment = null;
            e.ChargeDateTime = null;
            //end add by  Jan 2016-04-27
        }

        private void s_OnPaidOk(object sender, OpenCardEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.CardID)) return;
                CardPaymentInfo pay = e.Payment;
                if (pay != null && (pay.Accounts > 0 || pay.Discount > 0)) //只有要收费的记录才保存
                {
                    pay.Paid = e.Paid;
                    pay.IsCenterCharge = true;
                    pay.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
                    pay.StationID = WorkStationInfo.CurrentStation.StationName;
                    CommandResult ret = (new CardBll(AppSettings.CurrentSetting.MasterParkConnect)).PayParkFee(pay);
                }
                if (e.Entrance != null)
                {
                    IParkingAdapter pad = ParkingAdapterManager.Instance[e.Entrance.RootParkID];
                    if (pad != null)
                    {
                        var notify = new RemoteReadCardNotify(e.Entrance.RootParkID, e.Entrance.EntranceID, e.CardID, string.Empty,
                            OperatorInfo.CurrentOperator.OperatorName, WorkStationInfo.CurrentStation.StationName);
                        string temp = AppSettings.CurrentSetting.GetConfigContent("RemoteReader");
                        int reader = 0;
                        if (!int.TryParse(temp, out reader)) reader = 0;
                        notify.Reader = (EntranceReader)reader;
                        if (!pad.RemoteReadCard(notify))
                        {
                            if (Debug) Ralid.GeneralLibrary.LOG.FileLog.Log(e.Entrance.EntranceName, string.Format("【{0}】 缴费事件 远程读卡失败", e.CardID));
                        }
                        if (!string.IsNullOrEmpty(e.CardType)) //只有开放卡片才显示余额
                        {
                            WaitCallback wc = (WaitCallback)((object state) =>
                            {
                                System.Threading.Thread.Sleep(AppSettings.CurrentSetting.GetShowBalanceInterval() * 1000);
                                pad.LedDisplay(new SetLedDisplayNotify(e.Entrance.EntranceID, CanAddress.TicketBoxLed, string.Format("车费{0}元 余额{1}元", e.Paid, e.Balance), false, 0));
                            });
                            ThreadPool.QueueUserWorkItem(wc);
                        }
                    }
                }
                if (this.OnPaidOk != null) this.OnPaidOk(sender, e);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                throw ex;
            }
        }

        private void s_OnPaidFail(object sender, OpenCardEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CardID)) return;
            if (e.Entrance != null)
            {
                IParkingAdapter pad = ParkingAdapterManager.Instance[e.Entrance.RootParkID];
                if (pad != null)
                {
                    if (!e.LastError.Contains("余额不足"))
                    {
                        pad.LedDisplay(new SetLedDisplayNotify(e.Entrance.EntranceID, CanAddress.TicketBoxLed, string.IsNullOrEmpty(e.LastError) ? "扣款失败" : e.LastError, false, 0));
                    }
                    else
                    {
                        string temp = AppSettings.CurrentSetting.GetConfigContent("RemoteReader");
                        int reader = 0;
                        if (!int.TryParse(temp, out reader)) reader = 0;
                        var ce = new CardEventReport()
                        {
                            EntranceID = e.Entrance.EntranceID,
                            CardID = e.Payment.CardID,
                            CardType = e.Payment.CardType,
                            Reader = (EntranceReader)reader,
                        };
                        var notify = new EventInvalidNotify()
                        {
                            CardEvent = ce,
                            Balance = e.Balance,
                            OperatorNum = OperatorInfo.CurrentOperator.OperatorNum,
                            InvalidType = EventInvalidType.INV_Balance
                        };
                        pad.EventInvalid(notify);
                        if (!string.IsNullOrEmpty(e.CardType)) //只有开放卡片才显示余额
                        {
                            WaitCallback wc = (WaitCallback)((object state) =>
                            {
                                System.Threading.Thread.Sleep(AppSettings.CurrentSetting.GetShowBalanceInterval() * 1000);
                                pad.LedDisplay(new SetLedDisplayNotify(e.Entrance.EntranceID, CanAddress.TicketBoxLed, string.Format("车费{0}元 余额{1}元", e.Payment.GetPaying(), e.Balance), false, 0));
                            });
                            ThreadPool.QueueUserWorkItem(wc);
                        }
                    }
                }
            }
            if (this.OnPaidFail != null) this.OnPaidFail(sender, e);
        }

        private void s_OnError(object sender, OpenCardEventArgs e)
        {
            if (e.Entrance != null)
            {
                IParkingAdapter pad = ParkingAdapterManager.Instance[e.Entrance.RootParkID];
                if (pad != null)
                {
                    if (e.LastError.Contains("黑名单"))
                    {
                        string temp = AppSettings.CurrentSetting.GetConfigContent("RemoteReader");
                        int reader = 0;
                        if (!int.TryParse(temp, out reader)) reader = 0;
                        var fuck = new CardEventReport()
                        {
                            EntranceID = e.Entrance.EntranceID,
                            CardID = e.CardID,
                            CardType = CustomCardTypeSetting.Current.GetCardType(e.CardType),
                            Reader = (EntranceReader)reader,
                        };
                        var notify = new EventInvalidNotify()
                        {
                            CardEvent = fuck,
                            Balance = e.Balance,
                            OperatorNum = OperatorInfo.CurrentOperator.OperatorNum,
                            InvalidType = EventInvalidType.INV_Invalid,
                        };
                        pad.EventInvalid(notify);
                    }
                    else
                    {
                        pad.LedDisplay(new SetLedDisplayNotify(e.Entrance.EntranceID, CanAddress.TicketBoxLed, e.LastError, false, 0));
                    }
                }
            }
            if (this.OnError != null) this.OnError(this, e);
        }
        #endregion

        #region 事件
        public event EventHandler<OpenCardEventArgs> OnReadCard;

        public event EventHandler<OpenCardEventArgs> OnPaying;

        public event EventHandler<OpenCardEventArgs> OnPaidOk;

        public event EventHandler<OpenCardEventArgs> OnPaidFail;

        public event EventHandler<OpenCardEventArgs> OnError;
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="setting"></param>
        public void InitService(ZSTSettings setting)
        {
            if (!_Services.ContainsKey(typeof(ZSTSettings)))
            {
                ZSTService s = new ZSTService(setting as ZSTSettings);
                s.OnReadCard += new EventHandler<OpenCardEventArgs>(s_OnReadCard);
                s.OnPaying += new EventHandler<OpenCardEventArgs>(s_OnPaying);
                s.OnPaidOk += new EventHandler<OpenCardEventArgs>(s_OnPaidOk);
                s.OnPaidFail += new EventHandler<OpenCardEventArgs>(s_OnPaidFail);
                s.OnError += new EventHandler<OpenCardEventArgs>(s_OnError);
                s.Init();
                _Services[typeof(ZSTSettings)] = s;
            }
            else
            {
                ZSTService s = _Services[typeof(ZSTSettings)] as ZSTService;
                s.Setting = setting as ZSTSettings;
            }
        }
        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="yt"></param>
        public void InitService(YiTingShanFuSetting yt)
        {
            YiTingShanFuService s = null;
            if (_Services.ContainsKey(yt.GetType()))
            {
                s = _Services[yt.GetType()] as YiTingShanFuService;
                if (s.Setting != null && s.Setting.IP == yt.IP && s.Setting.Port == yt.Port) //如果通讯参数不变,则不用重新初始化服务
                {
                    s.Setting = yt;
                }
                else
                {
                    s.Dispose();
                    s = null;
                }
            }
            if (s == null)
            {
                s = new YiTingShanFuService(yt);
                s.OnReadCard += new EventHandler<OpenCardEventArgs>(s_OnReadCard);
                s.OnPaying += new EventHandler<OpenCardEventArgs>(s_OnPaying);
                s.OnPaidOk += new EventHandler<OpenCardEventArgs>(s_OnPaidOk);
                s.OnPaidFail += new EventHandler<OpenCardEventArgs>(s_OnPaidFail);
                s.OnError += new EventHandler<OpenCardEventArgs>(s_OnError);
                _Services[yt.GetType()] = s;
                s.Init();
            }
        }
        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="yct"></param>
        public void InitService(YCT.YCTSetting yct)
        {
            if (!_Services.ContainsKey(yct.GetType()))
            {
                IOpenCardService s = new YCT.YCTService(yct);
                s.OnReadCard += new EventHandler<OpenCardEventArgs>(s_OnReadCard);
                s.OnPaying += new EventHandler<OpenCardEventArgs>(s_OnPaying);
                s.OnPaidOk += new EventHandler<OpenCardEventArgs>(s_OnPaidOk);
                s.OnPaidFail += new EventHandler<OpenCardEventArgs>(s_OnPaidFail);
                s.OnError += new EventHandler<OpenCardEventArgs>(s_OnError);
                _Services[yct.GetType()] = s;
            }
            (_Services[yct.GetType()] as YCT.YCTService).Setting = yct;
            _Services[yct.GetType()].Init();
        }

        public void InitService(ETC.ETCSetting etc)
        {
            ETC.ETCService s = null;
            if (!_Services.ContainsKey(etc.GetType()))
            {
                s = new ETC.ETCService();
                s.OnReadCard += new EventHandler<OpenCardEventArgs>(s_OnReadCard);
                s.OnPaying += new EventHandler<OpenCardEventArgs>(s_OnPaying);
                s.OnPaidOk += new EventHandler<OpenCardEventArgs>(s_OnPaidOk);
                s.OnPaidFail += new EventHandler<OpenCardEventArgs>(s_OnPaidFail);
                s.OnError += new EventHandler<OpenCardEventArgs>(s_OnError);
                _Services[etc.GetType()] = s;
            }
            else
            {
                s = _Services[etc.GetType()] as ETC.ETCService;
            }
            s.Setting = etc;
            _Services[etc.GetType()].Init();
        }

        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="yct"></param>
        public void InitService(LR280.LR280Setting lr280)
        {
            if (!_Services.ContainsKey(lr280.GetType()))
            {
                IOpenCardService s = new LR280.LR280Service(lr280);
                s.OnReadCard += new EventHandler<OpenCardEventArgs>(s_OnReadCard);
                s.OnPaying += new EventHandler<OpenCardEventArgs>(s_OnPaying);
                s.OnPaidOk += new EventHandler<OpenCardEventArgs>(s_OnPaidOk);
                s.OnPaidFail += new EventHandler<OpenCardEventArgs>(s_OnPaidFail);
                s.OnError += new EventHandler<OpenCardEventArgs>(s_OnError);
                _Services[lr280.GetType()] = s;
            }
            (_Services[lr280.GetType()] as LR280.LR280Service).Setting = lr280;
            _Services[lr280.GetType()].Init();
        }

        /// <summary>
        /// 查看是否已经启动某个类型的服务
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public bool ContainService<T>()
        {
            return _Services.ContainsKey(typeof(T));
        }
        /// <summary>
        /// 关闭属于某个类型的服务
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public void CloseService<T>()
        {
            if (_Services.ContainsKey(typeof(T)))
            {
                _Services[typeof(T)].Dispose();
                _Services.Remove(typeof(T));
            }
        }

        public void HandleReport(ReportBase report)
        {
            if (report is CardEventReport) HandleCardEvent(report as CardEventReport);
            else if (report is CardInvalidEventReport)
            {
                CardInvalidEventReport ci = report as CardInvalidEventReport;
                GlobalSettings.Current.Get<Dictionary<int, CardEventReport>>()[report.EntranceID] = new CardEventReport()
                {
                    CardID = ci.CardID,
                    EventDateTime = ci.EventDateTime,
                    EntranceID = ci.EntranceID
                };
            }
        }

        public void HandleCardEvent(CardEventReport report)
        {
            GlobalSettings.Current.Get<Dictionary<int, CardEventReport>>()[report.EntranceID] = report; //
            if (report.EventStatus != CardEventStatus.Valid) return;
            if (report.CardType != null && (report.CardType.Name == YiTingShanFuSetting.CardType || report.CardType.Name == YCT.YCTSetting.CardTyte || report.CardType.Name == ETC.ETCSetting.CardTyte || report.CardType.Name == LR280.LR280Setting.CardTyte)) //
            {
                if (report.IsExitEvent) //出场后,将开放卡片从系统中删除
                {
                    CardBll bll = new CardBll(AppSettings.CurrentSetting.MasterParkConnect);
                    CardInfo card = bll.GetCardByID(report.CardID).QueryObject;
                    if (card != null && (card.ParkingStatus & ParkingStatus.Out) == ParkingStatus.Out) //只有在卡片已经出场的情况下才删除它
                    {
                        bll.DeleteCardAtAll(card);
                    }
                }
            }
        }
        #endregion
    }
}
