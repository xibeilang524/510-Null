using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 软件的全局变量类
    /// </summary>
    public class GlobalVariables
    {

        #region 私有变量
        #endregion

        #region 静态只读属性
        /// <summary>
        /// 卡片默认密钥
        /// </summary>
        public static readonly byte[] DefaultKey = { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

        /// <summary>
        /// 默认停车场扇区密钥
        /// </summary>
        public static readonly byte[] DefaultParkingKey = { 0xf1, 0xff, 0xff, 0xff, 0xff, 0xf1 };

        ///// <summary>
        ///// 系统密钥加解密因子
        ///// </summary>
        //public static readonly byte[] KeyFactor = Encoding.GetEncoding("gb2312").GetBytes("Ralid_Key_Factor");

        /// <summary>
        /// 系统目前使用的停车场卡片数据格式版本
        /// </summary>
        public static readonly byte CurrentCardVersion = 0x01;

        /// <summary>
        /// 获取停车场扇区密钥
        /// </summary>
        public static byte[] ParkingKey
        {
            get
            {
                return KeySetting.Current != null && KeySetting.Current.ParkingKeyIsValid ? KeySetting.Current.ParkingKey : DefaultParkingKey;
            }
        }

        /// <summary>
        /// 获取停车场读写的扇区
        /// </summary>
        public static byte ParkingSection
        {
            get
            {
                return KeySetting.Current != null ? KeySetting.Current.ParkingSection : (byte)2;//默认扇区2
            }
        }

        /// <summary>
        /// 获取当前停车场通讯IP
        /// </summary>
        public static IPAddress CurrentParkingCommunicationIP
        {
            get
            {
                IPAddress ipa = GeneralLibrary.NetTool.GetFirstIP();
                if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.ParkingCommunicationIP))
                {
                    System.Net.IPAddress.TryParse(AppSettings.CurrentSetting.ParkingCommunicationIP, out ipa);
                }
                return ipa;
            }
        }

        /// <summary>
        /// 获取或设置控制器的事件侦听端口
        /// </summary>
        public static int EventListenerPort
        {
            get
            {
                return 11000;
            }
        }

        /// <summary>
        /// 获取能否初始化停车场通讯
        /// </summary>
        /// <returns></returns>
        public static bool EnableInitParkingCommunication
        {
            get
            {
                IPAddress ipa = GeneralLibrary.NetTool.GetFirstIP();
                if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.ParkingCommunicationIP))
                {
                    if (System.Net.IPAddress.TryParse(AppSettings.CurrentSetting.ParkingCommunicationIP, out ipa))
                    {
                        IPAddress[] ips = GeneralLibrary.NetTool.GetLocalIPS();
                        return ips.Any(item => item.ToString() == ipa.ToString());
                    }
                }
                return ipa != null;
            }
        }
        
        #endregion

        
    }
}
