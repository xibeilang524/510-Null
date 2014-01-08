using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary.Printer;
using Ralid.BusinessModel.Model;
using Ralid.BusinessModel.Enum;

namespace Ralid.Monitor
{
    public class ParkBillFactory
    {
        /// <summary>
        /// 根据卡片收费记录生成停车收费小票
        /// </summary>
        /// <param name="cardPayment"></param>
        /// <returns></returns>
        public static ParkBillInfo CreateParkBill(CardPaymentRecord cardPayment)
        {
            ParkBillInfo bill = new ParkBillInfo();
            bill.CardID = cardPayment.CardID;
            bill.Accounts = cardPayment.Accounts;
            bill.CarType = EnumDescription.GetDescription(cardPayment.CarType);
            bill.ChargeDateTime = cardPayment.ChargeDateTime;
            bill.CompanyName = GeneralSetting.Current.CompanyName;
            if (cardPayment.EnterDateTime != null)
            {
                bill.EnterDateTime = cardPayment.EnterDateTime.Value;
            }
            bill.HavePaid = cardPayment.HavePaid;
            bill.LastAccounts = cardPayment.LastAccounts;
            bill.Operator = OperatorInfo.CurrentOperator.OperatorName;
            bill.OwnerName = cardPayment.OwnerName;
            bill.Paid = cardPayment.Paid;
            bill.TariffType = EnumDescription.GetDescription(cardPayment.TariffType);
            return bill;
        }
    }
}
