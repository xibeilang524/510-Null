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
    /// 充值收费记录序列化处理者
    /// </summary>
    public class CardChargeRecordSerializer
    {
        #region 公共方法
        /// <summary>
        /// 充值收费记录序列化
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public static string Serialize(CardChargeRecord payment)
        {
            try
            {
                string text = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}",
                                           payment.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                                           payment.CardID,
                                           payment.OwnerName,
                                           payment.CardCertificate,
                                           payment.CarPlate,
                                           payment.ChargeAmount,
                                           payment.Payment,
                                           payment.Balance,
                                           payment.ValidDate.ToString(),
                                           (byte)payment.PaymentMode,
                                           payment.OperatorID,
                                           payment.StationID,
                                           payment.Memo);
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
        /// 充值收费记录反序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CardChargeRecord Deserialize(string value)
        {
            CardChargeRecord item = null;
            try
            {
                string hex = (new DTEncrypt()).DSEncrypt(value);
                byte[] data = HexStringConverter.StringToHex(hex);
                string text = System.Text.UTF8Encoding.UTF8.GetString(data, 0, data.Length);
                string[] temp = text.Split('|');
                if (temp != null && temp.Length > 0)
                {
                    item = new CardChargeRecord();
                    item.ChargeDateTime = DateTime.Parse(temp[0]);
                    item.CardID = temp[1];
                    item.OwnerName = temp[2];
                    item.CardCertificate = temp[3];
                    item.CarPlate = temp[4];
                    item.ChargeAmount = decimal.Parse(temp[5]);
                    item.Payment = decimal.Parse(temp[6]);
                    item.Balance = decimal.Parse(temp[7]);
                    item.ValidDate = DateTime.Parse(temp[8]);
                    item.PaymentMode = (PaymentMode)(byte.Parse(temp[9]));
                    item.OperatorID = temp[10];
                    item.StationID = temp[11];
                    item.Memo = temp[12];
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
