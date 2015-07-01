using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary;

namespace Ralid.OpenCard.OpenCardService
{
    public class YiTingPacket
    {
        #region 静态方法
        /// <summary>
        /// 获取日期的编码
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static byte[] GetDateBytes(DateTime dt)
        {
            byte[] ret= ASCIIEncoding.ASCII.GetBytes(dt.ToString("yyyyMMddHHmmss"));
            return ConvertAscToNumber(ret);
        }
        /// <summary>
        /// 获取金额的编码
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static byte[] GetMoneyBytes(decimal money)
        {
            byte[] ret= ASCIIEncoding.ASCII.GetBytes(money.ToString("F2").Replace(".", string.Empty).PadLeft(6, '0').Substring(0, 6));
            return ConvertAscToNumber(ret);
        }
        /// <summary>
        /// 获取时间间隔的编码
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static byte[] GetIntervalBytes(DateTime begin, DateTime end)
        {
            TimeSpan span = new TimeSpan(end.Ticks - begin.Ticks);
            byte[] ret= ASCIIEncoding.ASCII.GetBytes(string.Format("{0:D2}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}", 0, 0, (span.Days % 100), span.Hours, span.Minutes, span.Seconds));
            return ConvertAscToNumber(ret);
        }
        /// <summary>
        /// 从编码中获取金额
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static decimal GetMoney(byte[] data)
        {
            decimal ret = 0;
            string temp = ConvertToAsc(data);
            temp = temp.TrimStart('0');
            if (!string.IsNullOrEmpty(temp) && decimal.TryParse(temp, out ret))
            {
                ret /= 100;
            }
            return ret;
        }
        /// <summary>
        /// 将每一位由原生的数字转化成对应的ASC码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string  ConvertToAsc(byte[] data) 
        {
            byte[] temp = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                temp[i] = (byte)(data[i]);
            }
            return ASCIIEncoding.ASCII.GetString(temp).Trim('\0').Trim();
        }
        /// <summary>
        /// 将ASC码转化成原生的数字
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ConvertAscToNumber(byte[] data)
        {
            byte[] temp = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                temp[i] = (byte)(data[i]);
            }
            return temp;
        }

        public static string GetCardID(byte[] data)
        {
            string ret = null;
            try
            {
                ret = ConvertToAsc(data);
                if (ret.Length <= 8)
                {
                    byte[] temp = HexStringConverter.StringToHex(ret);
                    if (temp != null && temp.Length > 0)
                    {
                        ret = BEBinaryConverter.BytesToUint(temp).ToString();
                    }
                }
            }
            catch
            {
            }
            return ret;
        }
        #endregion
        //包的结构 头(2byte) + 命令(2byte) + 通讯方向(1byte) + 位置(1byte) + 校验和(1byte) + 数据长度(2byte) + 数据(nbyte) + 尾(2byte)
        #region 构造函数
        public YiTingPacket(byte[] packet)
        {
            _Data = packet;
        }
        #endregion

        #region 私有变量
        private byte[] _Data;
        #endregion

        #region 私有方法
        private byte CalCRC(IEnumerable<byte> buffer)
        {
            byte checksum = 0;
            foreach (byte b in buffer)//从长度到数据
            {
                checksum ^= b;
            }
            return checksum;
        }
        #endregion

        #region 公共属性
        public bool IsValid
        {
            get
            {
                if (_Data == null || _Data.Length < 11) return false;
                if (_Data[0] != 0x78 || _Data[1] != 0xB6) return false; //头
                if (_Data[_Data.Length - 2] != 0x21 || _Data[_Data.Length - 1] != 0xD3) return false; //尾
                //校验和
                byte[] temp = new byte[_Data.Length - 9];
                Array.Copy(_Data, 7, temp, 0, temp.Length);
                byte crc = CalCRC(temp);
                if (crc != _Data[6]) return false;
                //检验包长度
                int dlen = BEBinaryConverter.BytesToInt(new byte[] { _Data[7], _Data[8] });
                if (dlen + 11 != _Data.Length) return false;
                return true;
            }
        }

        public bool IsHearbeat
        {
            get
            {
                if (_Data != null && _Data.Length >= 4) return _Data[2] == 0x4C && _Data[3] == 0x5A;
                return false;
            }
        }

        public bool IsEnterRead
        {
            get
            {
                if (_Data != null && _Data.Length >= 4) return _Data[2] == 0x4C && _Data[3] == 0x6B;
                return false;
            }
        }

        public bool IsPayingRequest
        {
            get
            {
                if (_Data != null && _Data.Length >= 4) return _Data[2] == 0x4C && _Data[3] == 0x7C;
                return false;
            }
        }

        public bool IsPayingState
        {
            get
            {
                if (_Data != null && _Data.Length >= 4) return _Data[2] == 0x4C && _Data[3] == 0x8D;
                return false;
            }
        }

        public byte[] Data
        {
            get
            {
                if (!IsValid) return null;
                if (_Data.Length <= 11) return null;
                byte[] ret = new byte[_Data.Length - 11];
                Array.Copy(_Data, 9, ret, 0, ret.Length);
                return ret;
            }
        }
        #endregion

        #region 公共方法
        public YiTingPacket CreateResponse(byte[] data)
        {
            List<byte> temp = new List<byte>();
            if (data != null) temp.AddRange(data);
            temp.InsertRange(0, BEBinaryConverter.ShortToBytes((short)(data == null ? 0 : data.Length)));
            temp.Insert(0, CalCRC(temp));
            temp.Insert(0, _Data[5]);
            temp.Insert(0, 0x02); //方向
            temp.InsertRange(0, new byte[] { _Data[2], _Data[3] });
            temp.InsertRange(0, new byte[] { _Data[0], _Data[1] });
            temp.AddRange(new byte[] { _Data[_Data.Length - 2], _Data[_Data.Length - 1] });
            return new YiTingPacket(temp.ToArray());
        }

        public byte[] ToBytes()
        {
            if (_Data == null) return null;
            return _Data.ToArray();
        }
        #endregion
    }
}
