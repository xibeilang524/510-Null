using System;
using System.Collections.Generic;
using System.Text;
using NLSCAN.MacCtrl;
using System.Runtime.InteropServices;
using Ralid.Parking.POS.Tool;

namespace Ralid.Parking.POS.Device
{
    public class HandsetCardReader : POSDeviceBase
    {
        #region dll动态库调用
        /// <summary>
        /// 打开读卡器
        /// </summary>
        /// <param name="devname">读卡器名</param>
        /// <param name="flag">读卡器参数</param>
        /// <returns>读卡器句柄</returns>
        [DllImport("RFID.dll", EntryPoint = "NLRF_Open")]
        private static extern IntPtr NLRF_Open(string devname, string flag);

        /// <summary>
        /// 关闭读卡器
        /// </summary>
        /// <param name="hdev">读卡器句柄</param>
        /// <returns>成功为0，失败为非0</returns>
        [DllImport("RFID.dll", EntryPoint = "NLRF_Close")]
        private static extern int NLRF_Close(IntPtr hdev);

        /// <summary>
        /// 读取卡片块的数据
        /// </summary>
        /// <param name="hdev">读卡器句柄</param>
        /// <param name="Sector">扇区</param>
        /// <param name="Blk">块</param>
        /// <param name="pBuf">用于储存读取到数据的指针</param>
        /// <param name="dwLen">pBuf的长度</param>
        /// <returns>成功返回pBuf的长度，失败返回一个负数</returns>
        [DllImport("RFID.dll", EntryPoint = "NLRF_ReadBlk", CharSet = CharSet.Auto)]
        private static extern int NLRF_ReadBlk(IntPtr hdev, int Sector, int Blk, byte[] pBuf, int dwLen);

        /// <summary>
        /// 向卡片的块写数据
        /// </summary>
        /// <param name="hdev">读卡器句柄</param>
        /// <param name="Sector">扇区</param>
        /// <param name="Blk">块</param>
        /// <param name="pBuf">要写入的数据</param>
        /// <param name="dwLen">要写入的数据的长度</param>
        /// <returns>成功返回写入数据的长度，失败返回一个负数</returns>
        [DllImport("RFID.dll", EntryPoint = "NLRF_WriteBlk")]
        private static extern int NLRF_WriteBlk(IntPtr hdev, int Sector, int Blk, byte[] pBuf, int dwLen);

        /// <summary>
        /// 检验卡片密码
        /// </summary>
        /// <param name="hdev">读卡器句柄</param>
        /// <param name="nKeyType">要检验的扇区</param>
        /// <param name="pKey">用于检验的密码</param>
        /// <param name="dwKeyLen">密码的长度</param>
        /// <returns>成功返回0，失败返回非0</returns>
        [DllImport("RFID.dll", EntryPoint = "NLRF_ChkKey")]
        private static extern int NLRF_ChkKey(IntPtr hdev, int nKeyType, byte[] pKey, int dwKeyLen);

        /// <summary>
        /// 设置卡片密码
        /// </summary>
        /// <param name="hdev">读卡器句柄</param>
        /// <param name="nKeyType">要设置密码的扇区</param>
        /// <param name="pOldKey">旧密码</param>
        /// <param name="pKey">新密码</param>
        /// <param name="OdwKeyLen">就密码长度</param>
        /// <param name="dwKeyLen">新密码长度</param>
        /// <returns>成功返回0，失败返回非0</returns>
        [DllImport("RFID.dll", EntryPoint = "NLRF_SetKey")]
        private static extern int NLRF_SetKey(IntPtr hdev, int nKeyType, byte[] pOldKey, byte[] pKey, int OdwKeyLen, int dwKeyLen);

        /// <summary>
        /// 查询卡片信息
        /// </summary>
        /// <param name="hdev">读卡器句柄</param>
        /// <param name="pInfo">卡片信息对象</param>
        /// <returns>成功返回0，失败返回非0</returns>
        [DllImport("RFID.dll", EntryPoint = "NLRF_QueryCardInfo")]
        private static extern int NLRF_QueryCardInfo(IntPtr hdev, ref RFDEV_CARDINFO pInfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct RFDEV_CARDINFO
        {
            public int nSector;
            public int nBlkCnt;
            public int nBlkSize;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] CardNum;
        }
        #endregion

        #region 构造函数
        public HandsetCardReader()
        {
        }
        #endregion

        #region 变量
        public IntPtr _Hdev;
        #endregion

        #region 蜂鸣器控制
        /// <summary>
        /// 蜂鸣器控制-成功提示
        /// </summary>
        public override void  Buzz_OK()
        {
            int m_iFreq = 2730;
            int m_iVolume = 60;
            int m_iMdelay = 100;
            int m_iBuzCtrlRe = -1;
            m_iBuzCtrlRe = NLSSysCtrl.buz_ctrl(m_iFreq, m_iVolume, m_iMdelay);
        }

        /// <summary>
        /// 蜂鸣器控制-失败提示
        /// </summary>

        public override void  Buzz_Fail()
        {
            int m_iFreq = 2730;
            int m_iVolume = 60;
            int m_iMdelay = 650;
            int m_iBuzCtrlRe = -1;
            m_iBuzCtrlRe = NLSSysCtrl.buz_ctrl(m_iFreq, m_iVolume, m_iMdelay);

            m_iMdelay = 200;
            m_iBuzCtrlRe = NLSSysCtrl.buz_ctrl(m_iFreq, m_iVolume, m_iMdelay);

            m_iMdelay = 650;
            m_iBuzCtrlRe = NLSSysCtrl.buz_ctrl(m_iFreq, m_iVolume, m_iMdelay);
        }
        #endregion

        #region RFID 相关
        /// <summary>
        /// 设备打开函数，成功返回true，失败返回false
        /// </summary>
        /// <returns></returns>
        public override bool OpenRFID()
        {
            _Hdev = NLRF_Open("NL_CS2040", "COM7:9600,n,8,1");
            return IsRFIDOpened;
        }

        /// <summary>
        /// 设备关闭函数，成功返回true，失败返回false
        /// </summary>
        /// <returns></returns>
        public override bool CloseRFID()
        {
            if (NLRF_Close(_Hdev) == 0)
            {
                _Hdev = IntPtr.Zero;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断设备是否打开
        /// </summary>
        /// <returns></returns>
        public override bool IsRFIDOpened
        {
            get
            {
                return _Hdev != IntPtr.Zero;
            }
        }

        /// <summary>
        /// 检查卡片密码函数
        /// </summary>
        /// <param name="sector">扇区</param>
        /// <param name="keybyte">密码</param>
        /// <returns></returns>
        public override bool CheckKey(int sector, byte[] keybyte)
        {
            string temp = HexStringConverter.HexToString(keybyte, string.Empty);
            if (!string.IsNullOrEmpty(temp))
            {
                byte[] key = System.Text.ASCIIEncoding.ASCII.GetBytes(temp);
                if (NLRF_ChkKey(_Hdev, 0, key, 12) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 读取第n扇区m块的数据
        /// </summary>
        /// <param name="Sector">扇区</param>
        /// <param name="Blk">块</param>
        /// <returns></returns>
        public override byte[] ReadBlock(int Sector, int Blk)
        {
            int bulkSize = 16;
            byte[] readbyte = new byte[bulkSize];
            int iread = NLRF_ReadBlk(_Hdev, Sector, Blk, readbyte, bulkSize);
            if (iread != bulkSize)
            {
                readbyte = null;
            }
            return readbyte;
        }

        /// <summary>
        /// 读取卡片的序列号
        /// </summary>
        /// <returns></returns>
        public override string ReadCardID()
        {
            string readdata = "";
            RFDEV_CARDINFO card = new RFDEV_CARDINFO();
            card.CardNum = new byte[4];
            int ret = NLRF_QueryCardInfo(_Hdev, ref card);
            if (ret == 0)
            {
                readdata = SEBinaryConverter.BytesToLong(card.CardNum).ToString();
            }
            return readdata;
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
            int ret = NLRF_WriteBlk(_Hdev, Sector, Blk, wstr, 16);
            if (ret > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
