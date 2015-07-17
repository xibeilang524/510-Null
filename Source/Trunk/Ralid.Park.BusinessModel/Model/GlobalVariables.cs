using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary.CardReader;

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
        /// 系统使用的CPU卡应用目录
        /// </summary>
        public static readonly ApplicationType DefaultAppType = ApplicationType.ParkingApp;

        /// <summary>
        /// 系统使用过的CPU卡应用文件短文件标识
        /// </summary>
        public static readonly byte ParkingFile = 0x16;

        /// <summary>
        /// 获取停车场使用的CPU卡应用目录，读卡模式为IC卡时为0
        /// </summary>
        public static ApplicationType AppType
        {
            get
            {
                if (KeySetting.Current != null && KeySetting.Current.ReaderReadMode != ReaderReadMode.MifareIC)
                {
                    return DefaultAppType;
                }
                return 0;
            }
        }

        /// <summary>
        /// 获取停车场使用的CPU卡加密算法
        /// </summary>
        public static AlgorithmType AlgorithmType
        {
            get
            {
                if (KeySetting.Current != null)
                {
                    return KeySetting.Current.AlgorithmType;
                }
                return AlgorithmType.DES3;
            }
        }

        /// <summary>
        /// 获取停车场是否使用MifareIC卡
        /// </summary>
        public static bool UseMifareIC
        {
            get
            {
                return AppType == 0;
            }
        }

        /// <summary>
        /// 获取停车场使用的SAM卡卡座号，为0时表示使用固定密钥
        /// </summary>
        public static byte SamNO
        {
            get
            {
                if (KeySetting.Current != null && KeySetting.Current.ReaderReadMode == ReaderReadMode.SAM)
                {
                    return AppSettings.CurrentSetting.ParkingSamNO;
                }
                return 0;
            }
        }

        /// <summary>
        /// 获取停车场CPU卡密钥值
        /// </summary>
        public static byte[] ParkingCPUKey
        {
            get
            {
                if (KeySetting.Current != null)
                {
                    return KeySetting.Current.EncryptParkingCPUKey;
                }
                return null ;
            }
        }

        /// <summary>
        /// 获取或设置发卡机验证CPU卡使用的SAM卡号或固定密钥，长度为1时使为SAM卡号，长度为16时为密钥值
        /// </summary>
        public static byte[] CPUKey
        {
            get
            {
                if (KeySetting.Current != null)
                {
                    if (KeySetting.Current.ReaderReadMode == ReaderReadMode.FixedKey)
                    {
                        //使用固定密钥
                        if (KeySetting.Current.ParkingCPUKeyIsValid)
                        {
                            return KeySetting.Current.EncryptParkingCPUKey;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        //使用SAM卡号验证
                        return new byte[] { AppSettings.CurrentSetting.ParkingSamNO };
                    }
                }
                return null;
            }
        }

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
        /// 获取停车场读写的扇区或CPU文件号
        /// </summary>
        public static byte ParkingSection
        {
            get
            {
                if (KeySetting.Current != null)
                {
                    if (KeySetting.Current.ReaderReadMode == ReaderReadMode.MifareIC)
                    {
                        return KeySetting.Current.ParkingSection;
                    }
                    else
                    {
                        return ParkingFile;
                    }
                }
                return (byte)2;//默认使用IC卡扇区2
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

        #region 公共方法
        /// <summary>
        /// 设置读卡器读卡设置
        /// </summary>
        public static void SetCardReaderKeysetting()
        {
            //设置读卡模式及读卡应用目录
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).AppType = GlobalVariables.AppType;
            //设置CPU卡验证算法
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).AlgorithmType = GlobalVariables.AlgorithmType;
            //使用IC卡
            if (GlobalVariables.UseMifareIC)
            {
                //添加读卡器读取停车场扇区和密钥
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).AddReadSectionAndKey(GlobalVariables.ParkingSection, GlobalVariables.ParkingKey);
            }
            else
            {
                //设置CPU卡使用的SAM卡号或密钥
                CardReaderManager.GetInstance(UserSetting.Current.WegenType).CPUKey = GlobalVariables.CPUKey;
            }
        }
        #endregion
    }
}
