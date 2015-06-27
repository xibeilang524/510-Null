using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BLL;
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
        private Dictionary<string, EntranceInfo> _WaitingExitCards = new Dictionary<string, EntranceInfo>(); //等待出场的卡片
        private Dictionary<string, CardPaymentInfo> _WaitingPayingCards = new Dictionary<string, CardPaymentInfo>(); //等待交费的卡片
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
                ActivationDate = DateTime.Now,
                ValidDate = new DateTime(2099, 12, 31),
                Balance = 0,
            };
            return (new CardBll(AppSettings.CurrentSetting.MasterParkConnect)).AddCard(card).Result == ResultCode.Successful;
        }

        private void s_OnReadCard(object sender, OpenCardEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CardID)) return;
            _WaitingExitCards.Remove(e.CardID); //
            _WaitingPayingCards.Remove(e.CardID);
            if (e.EntranceID == null) return;
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(e.EntranceID.Value);
            if (entrance == null) return;

            if (!entrance.IsExitDevice) //入口
            {
                CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardByID(e.CardID).QueryObject;
                if (card == null) //保存卡片信息
                {
                    CardType ct = CustomCardTypeSetting.Current.GetCardType(e.CardType);
                    if (ct == null) return; //系统不支持的卡片类型
                    if (!SaveOpenCard(e.CardID, ct)) return;
                }
                //通过远程读卡方式
                IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                if (pad != null)
                {
                    pad.RemoteReadCard(new RemoteReadCardNotify(entrance.RootParkID, entrance.EntranceID, e.CardID));
                }
            }
        }

        private void s_OnPaying(object sender, OpenCardEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CardID)) return;
            _WaitingExitCards.Remove(e.CardID); //
            _WaitingPayingCards.Remove(e.CardID);
            CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardByID(e.CardID).QueryObject;
            if (card == null) return;

            CardPaymentInfo payment = CardPaymentInfoFactory.CreateCardPaymentRecord(card, TariffSetting.Current, card.CarType, DateTime.Now);
            e.Payment = payment;
            _WaitingPayingCards[e.CardID] = payment;
            if (e.EntranceID != null)
            {
                EntranceInfo entrance = ParkBuffer.Current.GetEntrance(e.EntranceID.Value);
                if (entrance != null) _WaitingExitCards[e.CardID] = entrance;
            }
        }

        private void s_OnPaidOk(object sender, OpenCardEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CardID)) return;
            if (!_WaitingPayingCards.ContainsKey(e.CardID)) return;

            if (_WaitingPayingCards.ContainsKey(e.CardID))
            {
                CardPaymentInfo pay = _WaitingPayingCards[e.CardID];
                if (pay != null && (pay.Accounts > 0 || pay.Discount > 0)) //只有要收费的记录才保存
                {
                    pay.PaymentMode = e.PaymentMode;
                    pay.PaymentCode = e.PaymentCode;
                    pay.IsCenterCharge = true;
                    pay.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
                    pay.StationID = WorkStationInfo.CurrentStation.StationName;
                    CommandResult ret = (new CardBll(AppSettings.CurrentSetting.MasterParkConnect)).PayParkFee(pay);
                }
                _WaitingPayingCards.Remove(e.CardID);
            }

            if (_WaitingExitCards.ContainsKey(e.CardID)) //如果是出口，远程读卡
            {
                EntranceInfo entrance = _WaitingExitCards[e.CardID];
                if (entrance != null)
                {
                    IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                    if (pad != null)
                    {
                        pad.RemoteReadCard(new RemoteReadCardNotify(entrance.RootParkID, entrance.EntranceID, e.CardID));
                    }
                }
                _WaitingExitCards.Remove(e.CardID);
            }
        }

        private void s_OnPaidFail(object sender, OpenCardEventArgs e)
        {
            if (string.IsNullOrEmpty(e.CardID)) return;
            _WaitingPayingCards.Remove(e.CardID);
            _WaitingExitCards.Remove(e.CardID);
        }
        #endregion

        #region 公共方法
        public void Init(object  setting)
        {
            Type t = setting.GetType();
            if (setting is ZSTSetting)
            {
                if (!_Services.ContainsKey(t))
                {
                    ZSTService zst = new ZSTService(setting as ZSTSetting);
                    zst.OnReadCard += new EventHandler<OpenCardEventArgs>(s_OnReadCard);
                    zst.OnPaying += new EventHandler<OpenCardEventArgs>(s_OnPaying);
                    zst.OnPaidOk += new EventHandler<OpenCardEventArgs>(s_OnPaidOk);
                    zst.OnPaidFail += new EventHandler<OpenCardEventArgs>(s_OnPaidFail);
                    zst.Init();
                    _Services[t] = zst;
                }
                else
                {
                    ZSTService s = _Services[t] as ZSTService;
                    s.Setting = setting as ZSTSetting;
                }
            }
            else if (setting is YiTingShanFuSetting)
            {
                YiTingShanFuSetting yt = setting as YiTingShanFuSetting;
                YiTingShanFuService yts = null;
                if (_Services.ContainsKey(t))
                {
                    yts = _Services[t] as YiTingShanFuService;
                    if (yts.Setting != null && yts.Setting.IP == yt.IP && yts.Setting.Port == yt.Port) //如果通讯参数不变,则不用重新初始化服务
                    {
                        yts.Setting = yt;
                    }
                    else
                    {
                        yts.Dispose();
                        yts = null;
                    }
                }
                if (yts == null)
                {
                    yts = new YiTingShanFuService(yt);
                    yts.OnReadCard += new EventHandler<OpenCardEventArgs>(s_OnReadCard);
                    yts.OnPaying += new EventHandler<OpenCardEventArgs>(s_OnPaying);
                    yts.OnPaidOk += new EventHandler<OpenCardEventArgs>(s_OnPaidOk);
                    yts.OnPaidFail += new EventHandler<OpenCardEventArgs>(s_OnPaidFail);
                    _Services[t] = yts;
                    yts.Init();
                }
            }
        }
        #endregion
    }
}
