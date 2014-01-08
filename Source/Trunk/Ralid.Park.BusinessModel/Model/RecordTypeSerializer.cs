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
    /// 记录类型序列化处理者
    /// </summary>
    public class RecordTypeSerializer
    {
        #region 公共方法
        /// <summary>
        /// 记录类型序列化
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public static string Serialize(RecordType type)
        {
            try
            {
                string text = type.ToString();
                byte[] data = System.Text.UTF8Encoding.UTF8.GetBytes(text);
                string hex = HexStringConverter.HexToString(data, string.Empty);
                string encript = (new DTEncrypt()).Encrypt(hex);
                return encript;
            
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return string.Empty;

        }
        /// <summary>
        /// 记录类型反序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static RecordType Deserialize(string value)
        {
            try
            {
                RecordType type = RecordType.Unknow;
                string hex = (new DTEncrypt()).DSEncrypt(value);
                //string hex = DSEncrypt(value);
                byte[] data = HexStringConverter.StringToHex(hex);
                string text = System.Text.UTF8Encoding.UTF8.GetString(data, 0, data.Length);
                if (System.Enum.TryParse<RecordType>(text, out type))
                {
                    return type;
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return RecordType.Unknow;
        }

        /// <summary>
        /// 从字密明字串中解密获取明文
        /// </summary>
        /// <param name="codeWord">加密因子</param>
        public static string DSEncrypt(string str)
        {
            string temp = (new DSEncrypt()).Encrypt(str);
            int p = temp.IndexOf("&&");
            if (p > 0)
            {
                string key = temp.Substring(0, p);
                temp = (new DSEncrypt(key)).Encrypt(temp.Substring(p + "&&".Length));
                return temp;
            }
            return str;
        }
        #endregion
    }
}
