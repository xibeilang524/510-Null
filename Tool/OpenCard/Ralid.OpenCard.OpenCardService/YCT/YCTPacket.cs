using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    public class YCTPacket
    {

        //包结构 头(1byte) + 包长(2byte) + Command(1byte) + status(1byte) + data(nbyte) + checksum32(4byte)
        #region 构造函数
        public YCTPacket(byte[] packet)
        {
            _Packet = packet;
        }
        #endregion

        #region 私有变量
        private byte[] _Packet;
        #endregion

        #region 公共只读属性
        /// <summary>
        /// 获取命令
        /// </summary>
        public byte Command
        {
            get
            {
                return _Packet[3];
            }
        }
        /// <summary>
        /// 获取命令是否执行成功, 为否的话可以查看Status
        /// </summary>
        public bool IsCommandExcuteOk
        {
            get
            {
                return Status == 0x00;
            }
        }
        /// <summary>
        /// 获取命令执行的状态
        /// </summary>
        public byte Status
        {
            get
            {
                return _Packet[4];
            }
        }
        /// <summary>
        /// 获取命令返回的数据
        /// </summary>
        public byte[] Data
        {
            get
            {
                if (_Packet.Length > 9)
                {
                    byte[] ret = new byte[_Packet.Length - 9];
                    Array.Copy(_Packet, 5, ret, 0, ret.Length);
                    return ret;
                }
                return null;
            }
        }

        public byte[] AllBytes
        {
            get
            {
                return _Packet;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 校验包
        /// </summary>
        /// <returns></returns>
        public bool CheckCRC()
        {
            byte[] temp = new byte[_Packet.Length - 4];
            Array.Copy(_Packet, 0, temp, 0, temp.Length);
            int crc = CRC32Helper.CRC32(temp);
            byte[] crcBytes = new byte[4];
            Array.Copy(_Packet, temp.Length, crcBytes, 0, crcBytes.Length);
            return Ralid.GeneralLibrary.BEBinaryConverter.BytesToInt(crcBytes) == crc;
        }
        #endregion
    }
}
