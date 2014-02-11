﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Parking.POS.Tool
{
    /// <summary>
    /// 字节数据与整数之间的转换器，可以在整数与大端法表示的字节数组(即高字节在前，低位字节在后)之间相互转换
    /// </summary>
    public class BEBinaryConverter
    {
        #region 转换成字节数组
        /// <summary>
        /// 把长整形转换成字节数组
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回值为一个长度为8的字节数组</returns>
        public static byte[] LongToBytes(long t)
        {
            byte[] bytes = SEBinaryConverter.LongToBytes(t);
            return bytes.Reverse().ToArray();
        }

        /// <summary>
        /// 把整形转换成字节的数组
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回值为一个长度为4的字节数组</returns>
        public static byte[] IntToBytes(int t)
        {
            byte[] bytes = SEBinaryConverter.IntToBytes(t);
            return bytes.Reverse().ToArray();
        }

        /// <summary>
        /// 把无符号整形转换成字节的数组
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回值为一个长度为4的字节数组</returns>
        public static byte[] UintToBytes(uint t)
        {
            byte[] bytes = SEBinaryConverter.UintToBytes(t);
            return bytes.Reverse().ToArray();
        }

        /// <summary>
        /// 把短整形转换成字节数组
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回值是一个长度为2的字节数组</returns>
        public static byte[] ShortToBytes(short t)
        {
            byte[] bytes = SEBinaryConverter.ShortToBytes(t);
            return bytes.Reverse().ToArray();
        }
        #endregion

        #region 从字节数组转换成数据
        /// <summary>
        /// 从字节数组生成长整形数
        /// </summary>
        /// <param name="bytes">字节数组的长度小于或等于8,否则产生InvalidCastException异常</param>
        /// <returns></returns>
        public static long BytesToLong(byte[] bytes)
        {
            byte[] bs = bytes.Reverse().ToArray();
            bytes = bs;

            long temp = 0;
            if (bytes.Length <= 8)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    temp += (long)bytes[i] << (i * 8);
                }
                return temp;
            }
            else
            {
                throw new InvalidCastException("转换成长整形数最多只能是8个字节");
            }
        }

        /// <summary>
        /// 从字节数组生成整形数
        /// </summary>
        /// <param name="bytes">字节数组的长度小于或等于4,否则产生InvalidCastException异常</param>
        /// <returns></returns>
        public static int BytesToInt(byte[] bytes)
        {
            byte[] bs = bytes.Reverse().ToArray();
            bytes = bs;

            int temp = 0;
            if (bytes.Length <= 4)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    temp += bytes[i] << (i * 8);
                }
                return temp;
            }
            else
            {
                throw new InvalidCastException("转换成整数最多只能是四个字节的数组");
            }
        }

        /// <summary>
        /// 从字节数组生成无符号整形数
        /// </summary>
        /// <param name="bytes">字节数组的长度小于或等于4,否则产生InvalidCastException异常</param>
        /// <returns></returns>
        public static uint BytesToUint(byte[] bytes)
        {
            byte[] bs = bytes.Reverse().ToArray();
            bytes = bs;

            uint temp = 0;
            if (bytes.Length <= 4)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    temp += (uint)bytes[i] << (i * 8);
                }
                return temp;
            }
            else
            {
                throw new InvalidCastException("转换成无符号整数最多只能是四个字节的数组");
            }
        }

        /// <summary>
        /// 从字节数组生成短整形数
        /// </summary>
        /// <param name="bytes">字节数组的长度小于或等于2,否则产生InvalidCastException异常</param>
        /// <returns></returns>
        public static short BytesToShort(byte[] bytes)
        {
            byte[] bs = bytes.Reverse().ToArray();
            bytes = bs;

            int temp = 0;
            if (bytes.Length <= 2)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    temp += bytes[i] << (i * 8);
                }
                return (short)temp;
            }
            else
            {
                throw new InvalidCastException("转换成短整数最多只能是两个字节");
            }
        }
        #endregion
    }
}
