using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Ralid.Parking.POS.Tool;

namespace Ralid.Parking.POS.Device
{
    /// <summary>
    /// 表示致远HPC3000型号的手持机
    /// </summary>
    public class HPC3000 : POSDeviceBase
    {
        #region 构造函数
        public HPC3000()
        {
        }
        #endregion

        #region 私有变量
        private bool _RFIDOpened = false;
        #endregion

        #region 蜂鸣器控制
        /// <summary>
        /// 蜂鸣器控制-成功提示
        /// </summary>
        public override void Buzz_OK()
        {
            Buzzer.BeepOK();
        }

        /// <summary>
        /// 蜂鸣器控制-失败提示
        /// </summary>

        public override void Buzz_Fail()
        {
            Buzzer.BeepError();
        }
        #endregion

        #region RFID功能
        /// <summary>
        /// 设备打开函数，成功返回true，失败返回false
        /// </summary>
        /// <returns></returns>
        public override bool OpenRFID()
        {
            byte ret = HPC_RFID_DLL.RfidModuleOpenPort(0);  //打开串口一
            _RFIDOpened = ret == 0;
            return ret == 0;
        }

        /// <summary>
        /// 设备关闭函数，成功返回true，失败返回false
        /// </summary>
        /// <returns></returns>
        public override bool CloseRFID()
        {
            byte ret = HPC_RFID_DLL.RfidModuleClosePort();
            _RFIDOpened = false;
            return ret == 0;
        }

        /// <summary>
        /// RFID设备是否已经打开
        /// </summary>
        public override bool IsRFIDOpened
        {
            get { return _RFIDOpened; }
        }

        /// <summary>
        /// 读取卡片的序列号
        /// </summary>
        /// <returns></returns>
        public override string ReadCardID()
        {
            string cardID = string.Empty;
            byte[] data = new byte[256];
            byte ret = HPC_RFID_DLL.ISO14443A_ReadCardSn(data);
            if (ret == 0)
            {
                cardID = SEBinaryConverter.BytesToLong(new byte[] { data[0], data[1], data[2], data[3] }).ToString();
            }
            return cardID;
        }

        /// <summary>
        /// 检查卡片密码函数
        /// </summary>
        /// <param name="sector">扇区</param>
        /// <param name="keybyte">密码</param>
        /// <returns></returns>
        public override bool CheckKey(int sector, byte[] keybyte)
        {
            byte ret = HPC_RFID_DLL.ISO14443A_MF1AuthKey((byte)(4 * sector), keybyte, 0x60);
            return ret == 0;
        }

        /// <summary>
        /// 读取第n扇区m块的数据
        /// </summary>
        /// <param name="Sector">扇区</param>
        /// <param name="Blk">块</param>
        /// <returns></returns>
        public override byte[] ReadBlock(int Sector, int Blk)
        {
            byte[] data = new byte[16];
            byte ret = HPC_RFID_DLL.ISO14443A_ReadMF1Block((byte)(Sector * 4 + Blk), data);
            return data;
        }

        /// <summary>
        /// 第n扇区m块写入数据
        /// </summary>
        /// <param name="wstr">写入数据</param>
        /// <param name="Sector">扇区</param>
        /// <param name="Blk">块</param>
        /// <returns></returns>
        public override bool WriteBlock(byte[] wstr, int Sector, int Blk)
        {
            byte ret = HPC_RFID_DLL.ISO14443A_WriteMF1Block((byte)(Sector * 4 + Blk), wstr);
            return ret == 0;
        }
        #endregion
    }
}
