using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary;
using Ralid.GeneralLibrary.SoftDog;

namespace Ralid.Park.POS.Model
{
    /// <summary>
    /// 免费授权记录序列化者
    /// </summary>
    public class FreeAuthorizationLogSerializer
    {
        #region 公共方法
        public static string Serialize(FreeAuthorizationLog log)
        {
            string text = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                                       log.LogDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                       log.CardID,
                                       log.BeginDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                       log.EndDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                                       log.InPark,
                                       log.CheckOut,
                                       log.OperatorID,
                                       log.StationID,
                                       log.Memo);
            byte[] data = System.Text.UTF8Encoding.UTF8.GetBytes(text);
            string hex = HexStringConverter.HexToString(data, string.Empty);
            string encript = (new DTEncrypt()).Encrypt(hex);
            return encript;
        }

        public static FreeAuthorizationLog Deserialize(string value)
        {
            FreeAuthorizationLog item = null;
            string hex = (new DTEncrypt()).DSEncrypt(value);
            byte[] data = HexStringConverter.StringToHex(hex);
            string text = System.Text.UTF8Encoding.UTF8.GetString(data, 0, data.Length);
            string[] temp = text.Split('|');
            if (temp != null && temp.Length > 0)
            {
                try
                {
                    item = new FreeAuthorizationLog();
                    item.LogDateTime = DateTime.Parse(temp[0]);
                    item.CardID = temp[1];
                    item.BeginDateTime = DateTime.Parse(temp[2]);
                    item.EndDateTime = DateTime.Parse(temp[3]);
                    item.InPark = bool.Parse(temp[4]);
                    item.CheckOut = bool.Parse(temp[5]);
                    item.OperatorID = temp[6];
                    item.StationID = temp[7];
                    item.Memo = temp[8];
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
