using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.ParkService.CANParking
{
    /// <summary>
    /// 二进制度流处理类，处理串口上传的数据,
    /// 接收到数据先放入数据缓冲区，等接收到一个完整的数据包后再处理
    /// </summary>
    public class PacketStream
    {
        private readonly int  MINPACKETLENGTH=8;         //最小包长度,即一个没有包含参数的包
        private readonly int PACKET_LENGTH_POSITION=6;  //包中指示包长度的字节位置

        #region 属性
        private byte[] buffer;  // 数据缓冲区
        private int dataLength;   // 缓冲区中实际数据的长度

        private static object lockObj = new object(); // 同步对象
        #endregion 属性

        #region 构造函数
        public PacketStream()
        {
            buffer = new byte[1000];
        }
        #endregion 构造函数

        #region 私有方法
        /// <summary>
        /// 从缓冲区开始处丢弃count数量的字节
        /// </summary>
        /// <param name="count"></param>
        private void Remove(int count)
        {
            if (buffer.Length >= count)
            {
                for (int i = count; i < buffer.Length - count; i++)
                {
                    buffer[i - count] = buffer[i];
                }
                dataLength -= count;
            }
            else
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = 0;
                }
                dataLength = 0;
            }
        }

        /// <summary>
        /// 定位到一个包的头部
        /// </summary>
        /// <returns></returns>
        private int PositionOfPacketHead()
        {
            for (int i = 2; i < buffer.Length; i++)
            {
                if (buffer[i - 2] == 0xFE && buffer[i - 1] == 0xFD && buffer[i] == 0xFE)
                {
                    if (i > 2) //说明前面有些字节是无用的，此时记录到日志中
                    {
                        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(new Exception("包前面有数据丢失！"));
                    }
                    return i - 2;
                    
                }
            }
            return -1;
        }

        /// <param name="bytes">组成一个包的字节数组</param>
        /// <returns></returns>
        private Packet CreatePacket(byte[] bytes)
        {
            if (bytes !=null && bytes.Length >=MINPACKETLENGTH )
            {
                if ((bytes[PACKET_LENGTH_POSITION] + MINPACKETLENGTH == bytes.Length) && bytes[0] == 0xFE && bytes[1] == 0xFD && bytes[2] == 0xFE)
                {
                    Packet packet = new Packet
                    {
                        Address = bytes[3],
                        Source = bytes[4],
                        Order = bytes[5],
                    };
                    for (int i = PACKET_LENGTH_POSITION + 1; i < bytes.Length - 1; i++)
                    {
                        packet.Parameters.Add(bytes[i]);
                    }
                    return packet;
                }
                else
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(new InvalidDataException("数据包解析错误!"));
                }
            }
            return null;
        }
        #endregion 私有方法

        //一个完整的包结构  Sys1+Sys2+Sys3+发送地址+指令来源+指令+参数长度+参数+CRC

        #region 公开方法
        /// <summary>
        /// 从缓冲区中获取一个完整的数据包
        /// </summary>
        /// <returns></returns>
        public Packet GetAPacket()
        {
            lock (lockObj)
            {
                int head = PositionOfPacketHead();
                byte packetLength=0;
                if (head >= 0)
                {
                    if (dataLength > head + PACKET_LENGTH_POSITION)
                    {
                        packetLength = buffer[head + PACKET_LENGTH_POSITION]; //读取包长度

                        if (dataLength >= head + MINPACKETLENGTH + packetLength)
                        {
                            byte[] data = new byte[MINPACKETLENGTH + packetLength];
                            for (int i = 0; i < data.Length; i++)
                            {
                                data[i] = buffer[head + i];
                            }
                            Remove(head + MINPACKETLENGTH + packetLength );
                            return CreatePacket(data);
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="_data"></param>
        public void Append(byte[] data)
        {
            lock (lockObj)
            {
                if (dataLength +data.Length  > buffer.Length)  //如果要加入的数据超出缓冲区的最大容量则扩容
                {
                    byte[] temp = new byte[dataLength +data.Length ];
                    for (int i =0; i < dataLength; i++)
                    {
                        temp[i] = buffer[i];
                    }
                    buffer=temp;
                }
                data.CopyTo (buffer,dataLength );
                dataLength +=data.Length ;
            }
        }
        #endregion 公开方法
    }
}
