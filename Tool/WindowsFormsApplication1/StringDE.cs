using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace WindowsFormsApplication1
{
    public class StringDE
    {
        private string m_QueryStringKey = "^#%6_!@&";
        public string EncryptString(string qString)
        {
            return this.Encrypt(qString, this.m_QueryStringKey);
        }
        public string DecryptString(string qString)
        {
            return this.Decrypt(qString, this.m_QueryStringKey);
        }
        private string Encrypt(string pToEncrypt, string sKey)
        {
            string result;
            if (string.IsNullOrEmpty(pToEncrypt))
            {
                result = null;
            }
            else
            {
                DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
                byte[] bytes = Encoding.Default.GetBytes(pToEncrypt);
                dESCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(sKey);
                dESCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(sKey);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
                StringBuilder stringBuilder = new StringBuilder();
                byte[] array = memoryStream.ToArray();
                for (int i = 0; i < array.Length; i++)
                {
                    byte b = array[i];
                    stringBuilder.AppendFormat("{0:X2}", b);
                }
                stringBuilder.ToString();
                result = stringBuilder.ToString();
            }
            return result;
        }
        private string Decrypt(string pToDecrypt, string sKey)
        {
            string result;
            if (string.IsNullOrEmpty(pToDecrypt))
            {
                result = null;
            }
            else
            {
                DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
                byte[] array = new byte[pToDecrypt.Length / 2];
                for (int i = 0; i < pToDecrypt.Length / 2; i++)
                {
                    int num = Convert.ToInt32(pToDecrypt.Substring(i * 2, 2), 16);
                    array[i] = (byte)num;
                }
                dESCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(sKey);
                dESCryptoServiceProvider.IV = Encoding.ASCII.GetBytes(sKey);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(array, 0, array.Length);
                cryptoStream.FlushFinalBlock();
                StringBuilder stringBuilder = new StringBuilder();
                result = Encoding.Default.GetString(memoryStream.ToArray());
            }
            return result;
        }
    }
}
