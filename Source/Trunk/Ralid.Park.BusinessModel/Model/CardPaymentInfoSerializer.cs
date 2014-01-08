using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;
using Ralid.GeneralLibrary;
using Ralid.GeneralLibrary.ExceptionHandling;
using Ralid.GeneralLibrary.SoftDog;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 停车收费记录序列化处理者
    /// </summary>
    public class CardPaymentInfoSerializer
    {
        #region 公共方法
        /// <summary>
        /// 停车收费记录序列化
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public static string Serialize(CardPaymentInfo payment)
        {
            try
            {
                string text = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}",
                                           payment.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                           payment.CardID,
                                           payment.EnterDateTime != null ? payment.EnterDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"): string.Empty,
                                           payment.CarPlate,
                                           (byte)payment.CardType,
                                           payment.CarType,
                                           (byte)payment.TariffType,
                                           payment.LastTotalPaid,
                                           payment.Accounts,
                                           payment.Paid,
                                           payment.Discount,
                                           (byte)payment.PaymentMode,
                                           (byte)payment.PaymentCode,
                                           payment.IsCenterCharge,
                                           payment.OperatorID,
                                           payment.StationID,
                                           payment.Memo,
                                           payment.ParkFee);
                byte[] data = System.Text.UTF8Encoding.UTF8.GetBytes(text);
                string hex = HexStringConverter.HexToString(data, string.Empty);
                string encript = (new DTEncrypt()).Encrypt(hex);
                return encript;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            return string.Empty;
        }
        /// <summary>
        /// 停车收费记录反序列号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CardPaymentInfo Deserialize(string value)
        {
            CardPaymentInfo item = null;
            try
            {
                string hex = (new DTEncrypt()).DSEncrypt(value);
                byte[] data = HexStringConverter.StringToHex(hex);
                string text = System.Text.UTF8Encoding.UTF8.GetString(data, 0, data.Length);
                string[] temp = text.Split('|');
                if (temp != null && temp.Length > 0)
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
                    item.CardType = (CardType)byte.Parse(temp[4]);
                    item.CarType = byte.Parse(temp[5]);
                    item.TariffType = (TariffType)(byte.Parse(temp[6]));
                    item.LastTotalPaid = decimal.Parse(temp[7]);
                    item.Accounts = decimal.Parse(temp[8]);
                    item.Paid = decimal.Parse(temp[9]);
                    item.Discount = decimal.Parse(temp[10]);
                    item.PaymentMode = (PaymentMode)(byte.Parse(temp[11]));
                    item.PaymentCode = (PaymentCode)(byte.Parse(temp[12]));
                    bool iscenter = false;
                    if (bool.TryParse(temp[13], out iscenter))
                    {
                        item.IsCenterCharge = iscenter;
                    }
                    item.OperatorID = temp[14];
                    item.StationID = temp[15];
                    item.Memo = temp[16];
                    item.ParkFee = decimal.Parse(temp[17]);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
                item = null;
            }
            return item;
        }
        #endregion
    }
}
