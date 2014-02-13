using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Ralid.GeneralLibrary;
using Ralid.GeneralLibrary.SoftDog;

namespace Ralid.Park.POS.Model
{
    public class CardPaymentInfoSerializer
    {
        #region 公共方法
        public static string Serialize(CardPaymentInfo payment)
        {
            string text = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}",
                                       payment.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                       payment.CardID,
                                       payment.EnterDateTime != null ? payment.EnterDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty,
                                       payment.CarPlate,
                                       payment.CardType,
                                       payment.CarType,
                                       (byte)payment.TariffType,
                                       payment.LastTotalPaid,
                                       payment.Accounts,
                                       payment.Paid,
                                       payment.Discount,
                                       (byte)payment.PaymentMode,
                                       (byte)payment.PaymentCode,
                                       payment.IsCenterCharge,
                                       payment.Operator,
                                       payment.StationID,
                                       payment.Memo);
            byte[] data = System.Text.UTF8Encoding.UTF8.GetBytes(text);
            string hex = HexStringConverter.HexToString(data, string.Empty);
            string encript = (new DTEncrypt()).Encrypt(hex);
            return encript;
        }

        public static CardPaymentInfo Deserialize(string value)
        {
            CardPaymentInfo item = null;
            string hex = (new DTEncrypt()).DSEncrypt(value);
            byte[] data = HexStringConverter.StringToHex(hex);
            string text = System.Text.UTF8Encoding.UTF8.GetString(data, 0, data.Length);
            string[] temp = text.Split('|');
            if (temp != null && temp.Length > 0)
            {
                try
                {
                    item = new CardPaymentInfo();
                    item.ChargeDateTime = DateTime.Parse(temp[0]);
                    item.CardID = temp[1];
                    if (string.IsNullOrEmpty(temp[2]))
                    {
                        item.EnterDateTime = null;
                    }
                    else
                    {
                        item.EnterDateTime = DateTime.Parse(temp[2]);
                    }
                    item.CarPlate = temp[3];
                    item.CardType = byte.Parse(temp[4]);
                    item.CarType = byte.Parse(temp[5]);
                    item.TariffType = (TariffType)(byte.Parse(temp[6]));
                    item.LastTotalPaid = decimal.Parse(temp[7]);
                    item.Accounts = decimal.Parse(temp[8]);
                    item.Paid = decimal.Parse(temp[9]);
                    item.Discount = decimal.Parse(temp[10]);
                    item.PaymentMode = (PaymentMode)(byte.Parse(temp[11]));
                    item.PaymentCode = (PaymentCode)(byte.Parse(temp[12]));
                    item.IsCenterCharge = bool.Parse(temp[13]);
                    item.Operator = temp[14];
                    item.StationID = temp[15];
                    item.Memo = temp[16];
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                    item = null;
                }
            }
            return item;
        }
        #endregion
    }
}
