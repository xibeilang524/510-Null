using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Ralid.Park.BusinessModel.Security
{
    /// <summary>
    /// 构造一个对称算法,使用3Des加密
    /// 如果当前的 Key 属性为 NULL，可调用 GenerateKey 方法以创建新的随机 Key。 
    /// 如果当前的 IV 属性为 NULL，可调用 GenerateIV 方法以创建新的随机 IV
    /// </summary>
    public class CryptoTripleDes
    {
        //加密矢量
        private static byte[] IV = { 0xB0, 0xA1, 0xB2, 0xA3, 0xB4, 0xA5, 0xB6, 0xA7 };
        /// <summary>
        /// 使用指定的128字节的密钥对字节数组进行3Des加密
        /// </summary>
        /// <param name="keys">密钥，16字节，128位</param>
        /// <param name="values">要加密的数组</param>
        /// <returns>已加密的数组</returns>
        public static byte[] CreateEncryptByte(byte[] keys, byte[] values)
        {
            TripleDESCryptoServiceProvider tdsc = new TripleDESCryptoServiceProvider();
            //指定密匙长度，默认为192位
            tdsc.KeySize = 128;
            //使用指定的key和IV（加密向量）
            tdsc.Key = keys;
            tdsc.IV = IV;
            //加密模式，偏移
            tdsc.Mode = CipherMode.ECB;
            tdsc.Padding = PaddingMode.None;
            //进行加密转换运算
            ICryptoTransform ct = tdsc.CreateEncryptor();
            //加密结果,8字节
            byte[] results = ct.TransformFinalBlock(values, 0, 8);

            return results;
        }
        /// <summary>
        /// 使用指定的128字节的密钥对字符串进行3Des加密
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static byte[] CreateEncryptString(string strKey, string strValue)
        {
            TripleDESCryptoServiceProvider tdsc = new TripleDESCryptoServiceProvider();
            byte[] results = new byte[strValue.Length];
            tdsc.KeySize = 128;
            if (!string.IsNullOrEmpty(strKey))
            {
                tdsc.Key = Encoding.UTF8.GetBytes(strKey);
            }
            tdsc.IV = IV;
            using (ICryptoTransform ct = tdsc.CreateDecryptor())
            {
                byte[] byt = Encoding.UTF8.GetBytes(strValue);
                results = ct.TransformFinalBlock(byt, 0, 8);
            }
            return results;
        }
        /// <summary>
        /// 对加密字字节组进行解密
        /// </summary>
        /// <param name="keys">密匙</param>
        /// <param name="values">已加密字符串</param>
        /// <returns>解密结果</returns>
        public static byte[] CreateDescryptByte(byte[] keys, byte[] values)
        {
            TripleDESCryptoServiceProvider tdsc = new TripleDESCryptoServiceProvider();

            //指定密匙长度，默认为192位
            tdsc.KeySize = 128;
            //使用指定的key和IV（加密向量）
            tdsc.Key = keys;
            tdsc.IV = IV;
            //加密模式，偏移
            tdsc.Mode = CipherMode.ECB;
            tdsc.Padding = PaddingMode.None;
            //进行解密转换运算
            ICryptoTransform ct = tdsc.CreateDecryptor();
            //解密结果,8字节
            byte[] results = ct.TransformFinalBlock(values, 0, 8);

            return results;
        }
    }
}
