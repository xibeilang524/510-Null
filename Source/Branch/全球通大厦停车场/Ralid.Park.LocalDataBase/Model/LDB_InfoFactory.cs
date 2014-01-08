using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.LocalDataBase.Model
{
    /// <summary>
    /// 本地数据库记录生成工厂
    /// </summary>
    public class LDB_InfoFactory
    {
        /// <summary>
        /// 生成本地数据库的卡片缴费记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static LDB_CardPaymentInfo CreateLDBCardPaymentInfo(CardPaymentInfo info)
        {
            LDB_CardPaymentInfo record = new LDB_CardPaymentInfo();

            record.CardID = info.CardID;
            record.CardCertificate = info.CardCertificate;
            record.CarPlate = info.CarPlate;
            record.ChargeDateTime = info.ChargeDateTime;
            record.EnterDateTime = info.EnterDateTime;
            record.CardType = info.CardType;
            record.CarType = info.CarType;
            record.TariffType = info.TariffType;
            record.LastTotalPaid = info.LastTotalPaid;
            record.Accounts = info.Accounts;
            record.Paid = info.Paid;
            record.Discount = info.Discount;
            record.PaymentMode = info.PaymentMode;
            record.DiscountHour = info.DiscountHour;
            record.OperatorID = info.OperatorID;
            record.StationID = info.StationID;
            record.Memo = info.Memo;
            record.SettleDateTime = info.SettleDateTime;
            record.IsCenterCharge = info.IsCenterCharge;
            record.ParkFee = info.ParkFee;
            record.PaymentCode = info.PaymentCode;
            record.OperatorCardID = info.OperatorCardID;
            record.UpdateFlag = false;

            return record;
        }

        /// <summary>
        /// 生成停车场数据库的卡片缴费记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CardPaymentInfo CreateCardPaymentInfo(LDB_CardPaymentInfo info)
        {
            CardPaymentInfo record = new CardPaymentInfo();

            record.CardID = info.CardID;
            record.CardCertificate = info.CardCertificate;
            record.CarPlate = info.CarPlate;
            record.ChargeDateTime = info.ChargeDateTime;
            record.EnterDateTime = info.EnterDateTime;
            record.CardType = info.CardType;
            record.CarType = info.CarType;
            record.TariffType = info.TariffType;
            record.LastTotalPaid = info.LastTotalPaid;
            record.Accounts = info.Accounts;
            record.Paid = info.Paid;
            record.Discount = info.Discount;
            record.PaymentMode = info.PaymentMode;
            record.DiscountHour = info.DiscountHour;
            record.OperatorID = info.OperatorID;
            record.StationID = info.StationID;
            record.Memo = info.Memo;
            record.SettleDateTime = info.SettleDateTime;
            record.IsCenterCharge = info.IsCenterCharge;
            record.ParkFee = info.ParkFee;
            record.PaymentCode = info.PaymentCode;
            record.OperatorCardID = info.OperatorCardID;

            return record;
        }
    }
}
