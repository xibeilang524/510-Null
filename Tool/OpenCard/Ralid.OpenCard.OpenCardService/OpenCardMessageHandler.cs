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
        private List<IOpenCardService> _ZST = new List<IOpenCardService>();
        #endregion

        #region 私有方法
        private bool SaveZSTCard(string cardID, CardType cardType)
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
            IOpenCardService service = sender as IOpenCardService;
            if (service == null) return;
            if (e.EntranceID == null) return;
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(e.EntranceID.Value);
            if (entrance == null) return;

            if (!entrance.IsExitDevice) //入口
            {
                CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardByID(e.CardID).QueryObject;
                if (card == null) //保存卡片信息
                {
                    CardType ct = CustomCardTypeSetting.Current.GetCardType(service.CardType);
                    if (ct == null) return; //系统不支持的卡片类型
                    if (!SaveZSTCard(e.CardID, ct)) return;
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
            IOpenCardService service = sender as IOpenCardService;
            if (service == null) return;
            CardInfo card = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCardByID(e.CardID).QueryObject;
            if (card == null) return;

            CardPaymentInfo payment = CardPaymentInfoFactory.CreateCardPaymentRecord(card, TariffSetting.Current, card.CarType, DateTime.Now);
            e.Payment = payment;
        }

        private void s_OnPaidOk(object sender, OpenCardEventArgs e)
        {
            IOpenCardService service = sender as IOpenCardService;
            if (service == null) return;
            CardPaymentInfo pay = e.Payment;
            if (pay != null && pay.Accounts > 0) //只有要收费的记录才保存
            {
                pay.PaymentMode = service.PaymentMode;
                pay.PaymentCode = service.PaymentCode;
                pay.IsCenterCharge = e.EntranceID == null;
                pay.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
                pay.StationID = WorkStationInfo.CurrentStation.StationName;
                CommandResult ret = (new CardBll(AppSettings.CurrentSetting.MasterParkConnect)).PayParkFee(pay);
            }

            if (e.EntranceID == null) return;
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(e.EntranceID.Value);
            if (entrance != null)
            {
                IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                if (pad != null)
                {
                    pad.RemoteReadCard(new RemoteReadCardNotify(entrance.RootParkID, entrance.EntranceID, e.CardID));
                }
            }
        }

        private void s_OnPaidFail(object sender, OpenCardEventArgs e)
        {
            IOpenCardService service = sender as IOpenCardService;
            if (service == null) return;
            if (e.EntranceID == null) return;
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(e.EntranceID.Value);
            if (entrance == null) return;
            //通过远程读卡方式
            IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
            if (pad != null)
            {
                pad.EventInvalid(new EventInvalidNotify()
                {
                    InvalidType = EventInvalidType.IVN_NotPaid,
                });
            }
        }
        #endregion

        #region 公共方法
        public void Init()
        {
            
            foreach (IOpenCardService s in _ZST)
            {
                s.OnReadCard += new EventHandler<OpenCardEventArgs>(s_OnReadCard);
                s.OnPaying += new EventHandler<OpenCardEventArgs>(s_OnPaying);
                s.OnPaidOk += new EventHandler<OpenCardEventArgs>(s_OnPaidOk);
                s.OnPaidFail += new EventHandler<OpenCardEventArgs>(s_OnPaidFail);
            }
        }
        #endregion
    }
}
