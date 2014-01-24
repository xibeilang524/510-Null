using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Ralid.Parking.POS.Device
{
    public abstract class POSDeviceBase
    {
        #region 蜂鸣器控制
        /// <summary>
        /// 蜂鸣器控制-成功提示
        /// </summary>
        public abstract void Buzz_OK();

        /// <summary>
        /// 蜂鸣器控制-失败提示
        /// </summary>

        public abstract void Buzz_Fail();
        #endregion

        #region RFID 相关
        /// <summary>
        /// 设备打开函数，成功返回true，失败返回false
        /// </summary>
        /// <returns></returns>
        public abstract bool OpenRFID();

        /// <summary>
        /// 设备关闭函数，成功返回true，失败返回false
        /// </summary>
        /// <returns></returns>
        public abstract bool CloseRFID();

        /// <summary>
        /// 判断设备是否打开
        /// </summary>
        /// <returns></returns>
        public abstract bool IsRFIDOpened { get; }

        /// <summary>
        /// 读取卡片的序列号
        /// </summary>
        /// <returns></returns>
        public abstract string ReadCardID();

        /// <summary>
        /// 检查卡片密码函数
        /// </summary>
        /// <param name="sector">扇区</param>
        /// <param name="keybyte">密码</param>
        /// <returns></returns>
        public abstract bool CheckKey(int sector, byte[] keybyte);

        /// <summary>
        /// 读取第n扇区m块的数据
        /// </summary>
        /// <param name="Sector">扇区</param>
        /// <param name="Blk">块</param>
        /// <returns></returns>
        public abstract byte[] ReadBlock(int Sector, int Blk);

        /// <summary>
        /// 第n扇区m块写入数据
        /// </summary>
        /// <param name="wstr">写入数据</param>
        /// <param name="Sector">扇区</param>
        /// <param name="Blk">块</param>
        /// <returns></returns>
        public abstract bool WriteBlock(byte[] wstr, int Sector, int Blk);
        #endregion
    }
}
