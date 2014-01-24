using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Parking.POS.Model;
using Ralid.Parking.POS.Tool;

namespace Ralid.Parking.POS.Model
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
        public static CardPaymentInfo CreateCardPaymentRecord(CardInfo card, MySetting  ts, Byte carType, DateTime chargeDateTime)
        {
            CardPaymentInfo cardPayment = new CardPaymentInfo();
            cardPayment.ChargeDateTime = chargeDateTime;
            cardPayment.CardID = card.CardID;
            cardPayment.EnterDateTime = card.LastDateTime;
            cardPayment.CarPlate = card.CarPlate;
            cardPayment.CardType = card.CardType.ID;
            cardPayment.CarType = carType;
            cardPayment.PaymentCode = PaymentCode.POS;
            cardPayment.PaymentMode = PaymentMode.Cash;
            cardPayment.IsCenterCharge = false;
            cardPayment.LastTotalPaid = card.TotalPaidFee;

            ParkAccountsInfo parkFee = ts.CalculateCardParkFee(card, carType, chargeDateTime);
            cardPayment.ParkFee = parkFee.ParkFee;
            cardPayment.Accounts = parkFee.Accounts;
            cardPayment.TariffType = parkFee.TariffType;
           
            return cardPayment;
        }
    }
}
