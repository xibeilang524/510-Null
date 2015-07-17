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
            cardPayment.CarPlate = card.IsCarPlateList ? card.CarPlate : card.LastCarPlate;//如果是名单车牌，以车牌号为准
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
            if (cardPayment.LastTotalFee != card.TotalPaidFee)
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

            if (card.EnableHotelApp && card.FreeDateTime.HasValue)
            {
                cardPayment.Memo = string.Format("{0} {1}",Resouce.Resource1.CardPaymentInfoFactory_Free, card.FreeDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            return cardPayment;
        }

        public static CardPaymentInfo CreateCardPaymentRecord(int? parkID,CardInfo card, TariffSetting ts, Byte carType, DateTime chargeDateTime)
        {
            CardPaymentInfo cardPayment = new CardPaymentInfo();
            cardPayment.CardID = card.CardID;
            cardPayment.OwnerName = card.OwnerName;
            cardPayment.CardCertificate = card.CardCertificate;
            cardPayment.CarPlate = card.IsCarPlateList ? card.CarPlate : card.LastCarPlate;//如果是车牌名单，车牌号码为名单车牌
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
            if (cardPayment.LastTotalFee != card.TotalPaidFee)
            {
                cardPayment.LastTotalPaid = card.TotalPaidFee;
                cardPayment.LastTotalDiscount = 0;
                cardPayment.LastStationID = string.Empty;
            }

            ParkAccountsInfo parkFee = ts.CalculateCardParkFee(parkID,card, carType, chargeDateTime);
            cardPayment.Accounts = parkFee.Accounts;
            cardPayment.TariffType = parkFee.TariffType;

            cardPayment.ParkFee = parkFee.ParkFee;
            cardPayment.PaymentCode = PaymentCode.Computer;

            //从入场时间截止本次收费时间的优惠金额
            int currentWorkHour = 0;//本次优惠时数
            int usedHour = card.LastPayment != null ? card.LastPayment.DiscountHour : 0;//已使用的优惠时数
            decimal discountMoney = ts.CalculateCardDiscountMoney(parkID, card, carType, chargeDateTime, usedHour, out currentWorkHour);
            //如果计算出的优惠金额大于应收费用时，优惠金额为应收费用
            cardPayment.Discount = cardPayment.Accounts < discountMoney ? cardPayment.Accounts : discountMoney;
            cardPayment.DiscountHour = usedHour + currentWorkHour; //累计已优惠时数=已使用优惠时数+本次优惠时数
            cardPayment.CurrDiscountHour = currentWorkHour;//本次优惠时数
            
            //cardPayment.Discount = (discountMoney - cardPayment.LastTotalDiscount) > parkFee.Accounts ? parkFee.Accounts : (discountMoney - cardPayment.LastTotalDiscount) > 0 ? (discountMoney - cardPayment.LastTotalDiscount) : 0; //本次折扣额 = 【入场~收费时间】优惠金额-累计折扣
            //cardPayment.DiscountHour = currentWorkHour; //累计已优惠时数=计算优惠时数
            //if (card.LastPayment != null)
            //    cardPayment.CurrDiscountHour = cardPayment.DiscountHour - card.LastPayment.CurrHasPaidDiscountHour;
            //else
            //    cardPayment.CurrDiscountHour = currentWorkHour;


            if (card.EnableHotelApp && card.FreeDateTime.HasValue)
            {
                cardPayment.Memo = string.Format("{0} {1}；", Resouce.Resource1.CardPaymentInfoFactory_Free, card.FreeDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            if (card.DiscountHour > 0 && card.PreferentialTime.HasValue)
            {
                //yyyy-MM-dd HH:mm:ss 优惠 n 小时
                cardPayment.Memo += string.Format("{0} {1} {2} {3}；", card.PreferentialTime.Value.ToString("yyyy-MM-dd HH:mm:ss"), Resouce.Resource1.Preferential, card.DiscountHour, Resouce.Resource1.Hour);
            }

            return cardPayment;
        }
    }
}
