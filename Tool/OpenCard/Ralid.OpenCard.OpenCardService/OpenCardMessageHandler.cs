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

        #region 私有方法
        private bool SaveOpenCard(string cardID, CardType cardType)
        {
            CardInfo card = new CardInfo()
            {
                CardID = cardID,
                CardType = cardType,
                CarType = CarTypeSetting.DefaultCarType,
                CardNum = 1000,
                OwnerName = cardType.Name,
                Options = CardOptions.ForbidRepeatIn | CardOptions.ForbidRepeatOut | CardOptions.HolidayEnable | CardOptions.WithCount,
                Status = CardStatus.Enabled,
                ParkingStatus = ParkingStatus.Out,
                LastDateTime = DateTime.Now,
                LastEntrance = 0,
                ActivationDate = new DateTime(2000, 1, 1),
                ValidDate = new DateTime(2099, 12, 31),
                Balance = 0,
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
            else //中央收费,默认用第一个停车场的费率来收取费用
            {
                if (ParkingAdapterManager.Instance != null && ParkingAdapterManager.Instance.ParkAdapters != null)
                    pad = ParkingAdapterManager.Instance.ParkAdapters[0];
            }
            if (pad != null)
            {
                ret = pad.CreateCardPaymentRecord(card, card.CarType, dt);
            }
            return ret;
        }

        private string SerilPayment(CardPaymentInfo pay)
        {
            try
            {
                return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24})",
                        SQLStringHelper.FromInt(pay.ID),
                        SQLStringHelper.FromString(pay.CardID),
                        SQLStringHelper.FromString(pay.CardCertificate),
                        SQLStringHelper.FromString(pay.CarPlate),
                        SQLStringHelper.FromDateTime(pay.ChargeDateTime),
                        SQLStringHelper.FromDateTime(pay.EnterDateTime),
                        SQLStringHelper.FromByte((byte)pay.CardType),
                        SQLStringHelper.FromByte(pay.CarType),
                        SQLStringHelper.FromByte((byte)pay.TariffType),
                        SQLStringHelper.FromDecimal(pay.LastTotalPaid),
                        SQLStringHelper.FromDecimal(pay.LastTotalDiscount),
                        SQLStringHelper.FromDecimal(pay.Accounts),
                        SQLStringHelper.FromDecimal(pay.Paid),
                        SQLStringHelper.FromDecimal(pay.Discount),
                        SQLStringHelper.FromByte((byte)pay.PaymentMode),
                        SQLStringHelper.FromByte((byte)pay.DiscountHour),
                        SQLStringHelper.FromBool(pay.IsCenterCharge),
                        SQLStringHelper.FromString(pay.OperatorID),
                        SQLStringHelper.FromString(pay.StationID),
                        SQLStringHelper.FromDateTime(pay.SettleDateTime),
                        SQLStringHelper.FromString(pay.Memo),
                        SQLStringHelper.FromDecimal(pay.ParkFee),
                        SQLStringHelper.FromByte((byte)pay.PaymentCode),
                        SQLStringHelper.FromString(pay.OperatorCardID),
                        SQLStringHelper.FromBool(pay.UpdateFlag));
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
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
                    if (card == null) SaveOpenCard(e.CardID, ct);
                }
            }

            //通过远程读卡方式
            IParkingAdapter pad = ParkingAdapterManager.Instance[e.Entrance.RootParkID];
            if (pad != null)
            {
                pad.RemoteReadCard(new RemoteReadCardNotify(e.Entrance.RootParkID, e.Entrance.EntranceID, e.CardID, string.Empty,
                    OperatorInfo.CurrentOperator.OperatorID, WorkStationInfo.CurrentStation.StationID));
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
            CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardByID(e.CardID).QueryObject;
            if (card == null) return;
            CardPaymentInfo payment = GetPaymentInfo(card, e, DateTime.Now);
            if (!card.IsInPark && payment != null) payment.Accounts = 0; //停车场获取收费信息时已经出场的卡片也会产生费用信息，所以这里对这种情况处理为应收0元
            e.Payment = payment;
            if (this.OnPaying != null) this.OnPaying(sender, e);
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
                        pad.RemoteReadCard(new RemoteReadCardNotify(e.Entrance.RootParkID, e.Entrance.EntranceID, e.CardID, string.Empty,
                            OperatorInfo.CurrentOperator.OperatorID, WorkStationInfo.CurrentStation.StationID));
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
                    pad.LedDisplay(new SetLedDisplayNotify(e.Entrance.EntranceID, CanAddress.TicketBoxLed, string.IsNullOrEmpty(e.LastError) ? "扣款失败" : e.LastError, false, 0));
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
                    pad.LedDisplay(new SetLedDisplayNotify(e.Entrance.EntranceID, CanAddress.TicketBoxLed, e.LastError, false, 0));
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
        #endregion
    }
}
