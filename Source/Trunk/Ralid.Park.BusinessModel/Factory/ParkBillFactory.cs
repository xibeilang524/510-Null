using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary.Printer;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Factory
{
    public class ParkBillFactory
    {
        /// <summary>
        /// 根据卡片收费记录生成停车收费小票
        /// </summary>
        /// <param name="cardPayment"></param>
        /// <returns></returns>
        public static ParkBillInfo CreateParkBill(CardPaymentInfo cardPayment)
        {
            ParkBillInfo bill = new ParkBillInfo();
            bill.CardID = cardPayment.CardID;
            bill.CarPlate = (string.IsNullOrEmpty(cardPayment.CarPlate) ? string.Empty : cardPayment.CarPlate);
            bill.Accounts = cardPayment.Accounts;
            bill.CarType = CarTypeSetting.Current.GetDescription(cardPayment.CarType);
            bill.ChargeDateTime = cardPayment.ChargeDateTime;
            bill.CompanyName = UserSetting.Current.CompanyName;
            if (cardPayment.EnterDateTime != null)
            {
                bill.EnterDateTime = cardPayment.EnterDateTime.Value;
            }
            bill.HavePaid = cardPayment.LastTotalDiscount;
            bill.LastAccounts = cardPayment.LastTotalPaid;
            bill.Operator = OperatorInfo.CurrentOperator.OperatorID;
            bill.OwnerName = cardPayment.OwnerName;
            bill.Paid = cardPayment.Paid;
            bill.StationID = cardPayment.StationID;
            bill.TariffType = Ralid.Park.BusinessModel.Resouce.TariffTypeDescription.GetDescription(cardPayment.TariffType);
            return bill;
        }
    }
}
