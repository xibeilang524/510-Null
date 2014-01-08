using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Ralid.Park.BusinessModel.Model;
using Ralid.GeneralLibrary;

namespace Ralid.Park.ParkService.CANParking
{
    /// <summary>
    /// 表示一个完整的下发到硬件或从硬件接收到的消息包
    /// </summary>
    public class Packet
    {
        //Sys1+Sys2+Sys3+发送地址+指令来源+指令+参数长度+参数+CRC
        //Sys1+Sys2+Sys3=0xFE+0xFD+0xFE

        private List<byte> _Parameters = new List<byte>();

        #region 构造函数
        public Packet()
        {
        }
        #endregion

        #region 公共方法和属性
        /// <summary>
        /// 发送或接收地址
        /// </summary>
        public byte Address { get; set; }
        /// <summary>
        /// 指令来源或者指令对象,通常为0
        /// </summary>
        public byte Source { get; set; }
        /// <summary>
        /// 指令码
        /// </summary>
        public byte Order { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public List<Byte> Parameters
        {
            get
            {
                return _Parameters;
            }
        }

        public void AddByte(byte b)
        {
            _Parameters.Add(b);
        }

        public void AddBytes(byte[] bytes)
        {
            _Parameters.AddRange(bytes);
        }

        public void AddShort(short s)
        {
            _Parameters.AddRange(SEBinaryConverter.ShortToBytes(s));
        }

        public void AddInt(int i)
        {
            _Parameters.AddRange(SEBinaryConverter.IntToBytes(i));
        }

        public void AddLong(long l)
        {
            _Parameters.AddRange(SEBinaryConverter.LongToBytes(l));
        }

        public void AddDateTime(DateTime dt)
        {
            byte[] bytes = new byte[6];
            bytes[0] = (byte)dt.Second;
            bytes[1] = (byte)dt.Minute;
            bytes[2] = (byte)dt.Hour;
            bytes[3] = (byte)dt.Day;
            bytes[4] = (byte)dt.Month;
            bytes[5] = (byte)(dt.Year - 2000);
            _Parameters.AddRange(bytes);
        }

        public void AddString(string s)
        {
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(s);
            _Parameters.AddRange(bytes);
        }

        public void AddTimeEntity(TimeEntity t)
        {
            short s = (short)(t.Hour * 60 + t.Minute);
            AddShort(s);
        }

        public void AddDate(DateTime date)
        {
            byte[] bytes = new byte[3];
            bytes[0] = (byte)date.Day;
            bytes[1] = (byte)date.Month;
            bytes[2] = (byte)(date.Year - 2000);
            _Parameters.AddRange(bytes);
        }

        /// <summary>
        /// 转换成命令字节,如果格式不正确,则返回null
        /// </summary>
        /// <returns></returns>
        public byte[] ToCommandBytes()
        {
            if (this.Parameters.Count >= 0 && this.Parameters.Count < 256)
            {
                byte ValidationCode = 0;
                List<byte> list = new List<byte>();
                list.Add((byte)0xFE);
                list.Add((byte)0xFD);
                list.Add((byte)0xFE);
                list.Add(this.Address);
                list.Add(this.Source);
                list.Add(this.Order);
                list.Add((byte)this.Parameters.Count);
                list.AddRange(this.Parameters);

                for (int i = 3; i < list.Count; i++)
                {
                    ValidationCode = (byte)(ValidationCode ^ list[i]);
                }
                list.Add(ValidationCode);

                return list.ToArray();
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            StringBuilder bs = new StringBuilder();
            byte[] data = this.ToCommandBytes();
            foreach (byte b in data)
            {
                bs.Append(b.ToString("X2") + " ");
            }
            return bs.ToString();
        }

        /// <summary>
        /// 从参数列表中读出字节
        /// </summary>
        /// <param name="index">读取的起始位置</param>
        /// <param name="count">读取的字节数量</param>
        /// <returns></returns>
        public byte[] ReadDataFromParameter(int index, int count)
        {
            byte[] data = new byte[count];
            for (int i = 0; i < count; i++)
            {
                data[i] = Parameters[index + i];
            }
            return data;
        }

        public byte ReadByteFromParameter(int index)
        {
            return _Parameters[index];
        }
        #endregion
    }   
}
