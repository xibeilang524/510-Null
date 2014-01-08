using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// SQL语句字符串帮助类
    /// </summary>
    public class SQLStringHelper
    {
        /// <summary>
        /// 字符串类转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FromString(string str)
        {
            string ret = string.Empty;
            if (str == null) ret = "NULL";
            else ret = string.Format(@"'{0}'", str);
            return ret;
        }

        /// <summary>
        /// 可空整数转换
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string FromInt(int? num)
        {
            string ret = string.Empty;
            if (num == null) ret = "NULL";
            else ret = num.Value.ToString();
            return ret;
        }

        /// <summary>
        /// 整数转换
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string FromInt(int num)
        {
            return num.ToString();
        }

        /// <summary>
        /// 可空短整数转换
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string FromShort(short? num)
        {
            string ret = string.Empty;
            if (num == null) ret = "NULL";
            else ret = num.Value.ToString();
            return ret;
        }

        /// <summary>
        /// 短整数转换
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string FromShort(short num)
        {
            return num.ToString();
        }

        /// <summary>
        /// 可空字节型转换
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string FromByte(byte? b)
        {
            string ret = string.Empty;
            if (b == null) ret = "NULL";
            else ret = b.Value.ToString();
            return ret;
        }

        /// <summary>
        /// 字节型转换
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string FromByte(byte b)
        {
            return b.ToString();
        }

        /// <summary>
        /// 可空布尔型转换
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string FromBool(bool? b)
        {
            string ret = string.Empty;
            if (b == null) ret = "NULL";
            else ret =string.Format(@"'{0}'", b.Value.ToString());
            return ret;
        }

        /// <summary>
        /// 布尔型转换
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string FromBool(bool b)
        {
            string ret = string.Format(@"'{0}'", b.ToString());
            return ret;
        }

        /// <summary>
        /// 可空时间类型转换
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string FromDateTime(DateTime? d)
        {
            string ret = string.Empty;
            if (d == null) ret = "NULL";
            else ret = string.Format(@"'{0}'", d.Value.ToString());
            return ret;
        }

        /// <summary>
        /// 时间类型转换
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string FromDateTime(DateTime d)
        {
            string ret = string.Format(@"'{0}'", d.ToString());
            return ret;
        }
        
        /// <summary>
        /// 可空十进制数转换
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string FromDecimal(decimal? d)
        {
            string ret = string.Empty;
            if (d == null) ret = "NULL";
            else ret = d.Value.ToString("F2");
            return ret;
        }

        /// <summary>
        /// 十进制数转换
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string romDecimal(decimal d)
        {
            return d.ToString("F2");
        }
    }
}
