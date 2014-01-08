using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel .Enum ;
using Ralid.Park.BusinessModel .Model ;

namespace Ralid.Park.BusinessModel.Factory
{
    /// <summary>
    /// 卡片停车收费记录工厂
    /// </summary>
    public class CardPaymentInfoFactory
    {
        /// <summary>
        /// 生成卡片外车场停车收费记录
        /// </summary>
        /// <param name="card">卡片</param>
        /// <param name="ts">停车场费率设置</param>
        /// <param name="tos" >收费设置选项</param>
        /// <param name="chargeDateTime">收费时间</param>
        /// <returns></returns>
        public static CardPaymentInfo CreateCardPaymentRecord(CardInfo card, TariffSetting ts, Byte carType, DateTime chargeDateTime)
        {
            CardPaymentInfo cardPayment = new CardPaymentInfo();
            cardPayment.CardID = card.CardID;
            cardPayment.OwnerName = card.OwnerName;
            cardPayment.CardCertificate = card.CardCertificate;
            cardPayment.CarPlate = card.LastCarPlate;
            cardPayment.CardType = card.CardType;
            cardPayment.CarType = carType;
            cardPayment.EnterDateTime = card.LastDateTime;
            cardPayment.ChargeDateTime = chargeDateTime;

            //计算应收费用
            if (card.LastPayment != null)
            {
                cardPayment.LastTotalPaid = card.LastPayment.TotalPaid;
                cardPayment.LastTotalDiscount = card.LastPayment.TotalDiscount;
                cardPayment.LastStationID = card.LastPayment.StationID;
            }
            //如果卡片的已缴费用与收费记录不一致，以卡片的数据为准
            if ((cardPayment.LastTotalFee + cardPayment.LastTotalDiscount) != card.TotalPaidFee)
            {
                cardPayment.LastTotalPaid = card.TotalPaidFee;
                cardPayment.LastTotalDiscount = 0;
                cardPayment.LastStationID = string.Empty;
            }

            ParkAccountsInfo parkFee = ts.CalculateCardParkFee(card, carType, chargeDateTime);
            cardPayment.Accounts = parkFee.Accounts;
            cardPayment.TariffType = parkFee.TariffType;

            cardPayment.ParkFee = parkFee.ParkFee;
            cardPayment.PaymentCode = PaymentCode.Computer;
            return cardPayment;
        }
    }
}
