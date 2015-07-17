using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.BusinessModel.Configuration
{

    public class AppSettings
    {
        /// <summary>
        /// 获取或设置系统的当前设置
        /// </summary>
        public static AppSettings CurrentSetting
        {
            get
            {
                if (_instance == null)
                    _instance = new AppSettings(Application.ExecutablePath + ".config");
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        #region 私有变量
        private static AppSettings _instance = null;
        private XmlDocument _doc = null;
        private XmlNode _parent = null;
        private string _path;

        private string _WorkstationID;
        private string _ParkConnect;
        private string _StandbyParkConnect;
        private DataBaseType _SelectedPark;
        private SystemType _SystemType;
        private string _SnapShotSavePath;
        private byte _TicketReaderCOMPort;
        private byte _ParkFeeLedCOMPort;
        private byte _ParkFeeLedType;
        private byte _BillPrinterCOMPort;
        private byte _YCTReaderCOMPort;
        private byte _ParkFullLedCOMPort;
        private byte _VehicleLedCOMPort;
        private bool _Debug;
        private bool _DatabaseNeedUpgrade;
        private bool _OpenLastOpenedVideo;
        private bool _ShowOnlyListenedPark;
        private bool _Optimized;
        private bool _NeedPasswordWhenExit;
        private string _Language;
        private bool _RememberLogID;
        private bool _EnableTTS;
        private bool _EnlargeMemo;
        private bool _ChargeAfterMemo;
        private bool _ShowAPMMonitor;
        private bool _EnableZST;
        private string _ZSTReaderIP;
        private bool _EnableWriteCard;
        private bool _AuotAddToFirewallException;
        private string _ParkingCommunicationIP;
        private bool _AllowChangeParkWorkMode;
        private bool _CheckConnectionWithPing = true;
        private int _ParkVacantLed;
        private bool _SwitchEntrance;
        private bool _EnableHotel;
        private bool _NewCardValidCommand;
        private byte _ParkingSamNO;
        private bool _EnablePOSButton = true;
        private bool _SpeakPromptWhenCarArrival;
        private CarPlateRecognizationType _CarPlateRecognization;
        #endregion

        #region 构造函数
        public AppSettings(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    _path = path;
                    this._doc = new XmlDocument();
                    this._doc.Load(_path);
                    _parent = this._doc.SelectSingleNode("configuration/appSettings");

                    _WorkstationID = GetConfigContent("WorkStationID");
                    _ParkConnect = GetConfigContent("Parking");

                    _StandbyParkConnect = GetConfigContent("StandbyParking");

                    _SnapShotSavePath = GetConfigContent("SnapShotSavePath");

                    string temp = GetConfigContent("SystemType");
                    _SystemType = (!string.IsNullOrEmpty(temp) && temp == "1") ? SystemType.Server : SystemType.WorkStation;

                    temp = GetConfigContent("SelectedParking");
                    _SelectedPark = (string.IsNullOrEmpty(temp) || temp == "0") ? DataBaseType.Master : DataBaseType.Standby;

                    temp = GetConfigContent("ParkFeeLedCOMPort");
                    byte.TryParse(temp, out _ParkFeeLedCOMPort);

                    temp = GetConfigContent("ParkFeeLedType");
                    byte.TryParse(temp, out _ParkFeeLedType);

                    temp = GetConfigContent("TicketReaderCOMPort");
                    byte.TryParse(temp, out _TicketReaderCOMPort);

                    temp = GetConfigContent("BillPrinterCOMPort");
                    byte.TryParse(temp, out _BillPrinterCOMPort);

                    temp = GetConfigContent("YCTReaderCOMPort");
                    byte.TryParse(temp, out _YCTReaderCOMPort);

                    temp = GetConfigContent("ParkFullLedCOMPort");
                    byte.TryParse(temp, out _ParkFullLedCOMPort);

                    temp = GetConfigContent("ThreeLedCOMPort");
                    byte.TryParse(temp, out _VehicleLedCOMPort);

                    temp = GetConfigContent("Debug");
                    bool.TryParse(temp, out _Debug);

                    temp = GetConfigContent("DatabaseNeedUpgrade");
                    bool.TryParse(temp, out _DatabaseNeedUpgrade);

                    temp = GetConfigContent("OpenLastOpenedVideo");
                    bool.TryParse(temp, out _OpenLastOpenedVideo);

                    temp = GetConfigContent("ShowOnlyListenedPark");
                    bool.TryParse(temp, out _ShowOnlyListenedPark);

                    temp = GetConfigContent("Optimized");
                    bool.TryParse(temp, out _Optimized);

                    temp = GetConfigContent("NeedPasswordWhenExit");
                    bool.TryParse(temp, out _NeedPasswordWhenExit);

                    _Language = GetConfigContent("Language");

                    temp = GetConfigContent("RememberLogID");
                    bool.TryParse(temp, out _RememberLogID);

                    temp = GetConfigContent("EnableTTS");
                    bool.TryParse(temp, out _EnableTTS);

                    temp = GetConfigContent("EnlargeMemo");
                    bool.TryParse(temp, out _EnlargeMemo);

                    temp = GetConfigContent("ChargeAfterMemo");
                    bool.TryParse(temp, out _ChargeAfterMemo);

                    temp = GetConfigContent("ShowAPMMonitor");
                    bool.TryParse(temp, out _ShowAPMMonitor);

                    temp = GetConfigContent("EnableZST");
                    bool.TryParse(temp, out _EnableZST);

                    _ZSTReaderIP = GetConfigContent("ZSTReaderIP");

                    temp = GetConfigContent("EnableWriteCard");
                    bool.TryParse(temp, out _EnableWriteCard);

                    temp = GetConfigContent("AuotAddToFirewallException");
                    bool.TryParse(temp, out _AuotAddToFirewallException);

                    _ParkingCommunicationIP = GetConfigContent("ParkingCommunicationIP");

                    temp = GetConfigContent("AllowChangeParkWorkMode");
                    bool.TryParse(temp, out _AllowChangeParkWorkMode);

                    temp = GetConfigContent("CheckConnectionWithPing");
                    bool.TryParse(temp, out _CheckConnectionWithPing);

                    temp = GetConfigContent("ParkVacantLed");
                    if (!string.IsNullOrEmpty(temp)) int.TryParse(temp, out _ParkVacantLed);

                    temp = GetConfigContent("SwitchEntrance");
                    bool.TryParse(temp, out _SwitchEntrance);

                    temp = GetConfigContent("EnableHotel");
                    bool.TryParse(temp, out _EnableHotel);

                    temp = GetConfigContent("NewCardValidCommand");
                    bool.TryParse(temp, out _NewCardValidCommand);

                    temp = GetConfigContent("ParkingSamNO");
                    byte.TryParse(temp, out _ParkingSamNO);

                    temp = GetConfigContent("EnablePosButton");
                    _EnablePOSButton = bool.TryParse(temp, out _EnablePOSButton) ? _EnablePOSButton : true;//默认显示

                    temp = GetConfigContent("SpeakPromptWhenCarArrival");
                    bool.TryParse(temp, out _SpeakPromptWhenCarArrival);

                    temp = GetConfigContent("CarPlateRecognization");
                    if (temp == "VECON")
                    {
                        _CarPlateRecognization = CarPlateRecognizationType.VECON;
                    }
                    else if (temp == "XinLuTong")
                    {
                        _CarPlateRecognization = CarPlateRecognizationType.XinLuTong;
                    }
                    else if (temp == "DaHua")
                    {
                        _CarPlateRecognization = CarPlateRecognizationType.DaHua;
                    }
                    else
                    {
                        //默认用文通
                        _CarPlateRecognization = CarPlateRecognizationType.WINTONE;
                    }
                }
                catch
                {
                }
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 工作站标识
        /// </summary>
        public string WorkstationID
        {
            get
            {
                return _WorkstationID;
            }
            set
            {
                if (_WorkstationID != value)
                {
                    _WorkstationID = value;
                    SaveConfig("WorkStationID", value);
                }
            }
        }

        /// <summary>
        /// 获取当前主数据库的数据库连接字符串
        /// </summary>
        public string ParkConnect
        {
            get
            {
                return CurrentMasterConnect;
                //if (_SelectedPark == DataBaseType.Master)
                //{
                //    return CurrentMasterConnect;
                //}
                //else
                //{
                //    return CurrentStandbyConnect;
                //}
            }
        }
        /// <summary>
        /// 获取停车场当前主数据库连接字串，会根据主数据库的连接状态自动选择,返回空字符串时，表示与数据库连接已断开
        /// </summary>
        public string CurrentMasterConnect
        {
            get
            {
                string connectstr = MasterParkConnect;//返回默认数据库连接字符串

                //主数据库连接断开的，返回空字符串
                if (DataBaseConnectionsManager.Current.MasterStatus == DataBaseConnectionStatus.Disconnect)
                {
                    connectstr = string.Empty;
                }
                return connectstr;
            }
        }
        /// <summary>
        /// 获取停车场当前备份数据库连接字串，会根据备份数据库的连接状态自动选择,返回空字符串时，表示与数据库连接已断开
        /// </summary>
        public string CurrentStandbyConnect
        {
            get
            {
                string connectstr = StandbyParkConnect;//返回默认数据库连接字符串

                //备份数据库连接断开的，返回空字符串
                if (DataBaseConnectionsManager.Current.StandbyStatus == DataBaseConnectionStatus.Disconnect)
                {
                    connectstr = string.Empty;
                }
                return connectstr;
            }
        }

        /// <summary>
        /// 停车场主数据库连接字串
        /// </summary>
        public string MasterParkConnect
        {
            //连接字串分两段加密，首先前8个字符为加密的日期，做为实际连接字符串信息的加密密码。
            //解密连接字串：先用默认加密密码的加密类型解密出前8个字符的明文，再用一个密码为此明文的加密类解密出后续字符，得到连接字符的明文。
            get
            {
                string con = string.Empty;
                if (!string.IsNullOrEmpty(_ParkConnect) && _ParkConnect.Length > 8)
                {
                    con = (new Ralid.GeneralLibrary.SoftDog.DTEncrypt()).DSEncrypt(_ParkConnect);
                }
                return con;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _ParkConnect = (new Ralid.GeneralLibrary.SoftDog.DTEncrypt()).Encrypt(value);
                    SaveConfig("Parking", _ParkConnect);
                }
                else
                {
                    _ParkConnect = string.Empty;
                    SaveConfig("Parking", string.Empty);
                }
            }
        }

        /// <summary>
        /// 停车场备用数据库连接字串
        /// </summary>
        public string StandbyParkConnect
        {
            //连接字串分两段加密，首先前8个字符为加密的日期，做为实际连接字符串信息的加密密码。
            //解密连接字串：先用默认加密密码的加密类型解密出前8个字符的明文，再用一个密码为此明文的加密类解密出后续字符，得到连接字符的明文。
            get
            {
                string con = string.Empty;
                if (!string.IsNullOrEmpty(_StandbyParkConnect) && _StandbyParkConnect.Length > 8)
                {
                    con = (new Ralid.GeneralLibrary.SoftDog.DTEncrypt()).DSEncrypt(_StandbyParkConnect);
                }
                return con;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _StandbyParkConnect = (new Ralid.GeneralLibrary.SoftDog.DTEncrypt()).Encrypt(value);
                    SaveConfig("StandbyParking", _StandbyParkConnect);
                }
                else
                {
                    _StandbyParkConnect = string.Empty;
                    SaveConfig("StandbyParking", string.Empty);
                }
            }
        }

        /// <summary>
        /// 获取可用的停车场数据库连接
        /// </summary>
        public string AvailableParkConnect
        {
            get
            {
                if (DataBaseConnectionsManager.Current.MasterConnected)
                {
                    return MasterParkConnect;//主数据库连接上时，返回主数据库连接
                }
                else if (DataBaseConnectionsManager.Current.StandbyConnected)
                {
                    return StandbyParkConnect;//备用数据库连接上时，返回备用数据库连接
                }
                else if (DataBaseConnectionsManager.Current.MasterStatus == DataBaseConnectionStatus.Unconnected
                    && DataBaseConnectionsManager.Current.StandbyStatus == DataBaseConnectionStatus.Unconnected)
                {
                    //主数据和备用数据库都未连接的，返回选择登录验证的数据库
                    return SelectedParkConnect;
                }
                return string.Empty;//没有可用的数据库
            }
        }

        /// <summary>
        /// 获取图片数据连接串
        /// </summary>
        public string ImageDBConnStr
        {
            get
            {
                string connectstr = string.Empty;
                if (string.IsNullOrEmpty(UserSetting.Current.ParkingImageConnStr))
                {
                    //没有设置数据库连接字符串的，返回主数据库连接字符串
                    connectstr= ParkConnect;
                }
                else
                {
                    //图片据库连接断开的，返回空字符串
                    if (DataBaseConnectionsManager.Current.ImageDBStatus == DataBaseConnectionStatus.Disconnect)
                    {
                        connectstr = string.Empty;
                    }
                    else
                    {
                        connectstr = UserSetting.Current.ParkingImageConnStr;
                    }
                }
                return connectstr;
            }
        }

        /// <summary>
        /// 获取选择登录验证的数据库连接
        /// </summary>
        public string SelectedParkConnect
        {
            get
            {
                return _SelectedPark == DataBaseType.Master ? MasterParkConnect : StandbyParkConnect;
            }
        }

        /// <summary>
        /// 选择的数据库
        /// </summary>
        public DataBaseType SelectedPark
        {
            get
            {
                return _SelectedPark;
            }
            set
            {
                if (_SelectedPark != value)
                {
                    _SelectedPark = value;
                    SaveConfig("SelectedPark", (_SelectedPark == DataBaseType.Master) ? "0" : "1");
                }
            }
        }

        /// <summary>
        /// 系统类型
        /// </summary>
        public SystemType SystemType
        {
            get
            {
                return _SystemType;
            }
            set
            {
                if (_SystemType != value)
                {
                    _SystemType = value;
                    SaveConfig("SystemType", (_SystemType == SystemType.Server) ? "1" : "0");
                }
            }
        }

        /// <summary>
        /// 抓拍照片保存路径
        /// </summary>
        public string SnapShotSavePath
        {
            get { return _SnapShotSavePath; }
            set
            {
                if (_SnapShotSavePath != value)
                {
                    _SnapShotSavePath = value;
                    SaveConfig("SnapShotSavePath", value);
                }
            }
        }

        /// <summary>
        /// 获取或设置工作站条码枪串口号
        /// </summary>
        public byte TicketReaderCOMPort
        {
            get
            {
                return _TicketReaderCOMPort;
            }
            set
            {
                if (_TicketReaderCOMPort != value)
                {
                    _TicketReaderCOMPort = value;
                    SaveConfig("TicketReaderCOMPort", value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置工作站收费屏串口号
        /// </summary>
        public byte ParkFeeLedCOMPort
        {
            get
            {
                return _ParkFeeLedCOMPort;
            }
            set
            {
                if (_ParkFeeLedCOMPort != value)
                {
                    _ParkFeeLedCOMPort = value;
                    SaveConfig("ParkFeeLedCOMPort", value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置桌面收费屏的类型 0，表示中矿桌面收费屏 1,表示颜色USB桌面收费屏
        /// </summary>
        public byte ParkFeeLedType
        {
            get
            {
                return _ParkFeeLedType;
            }
            set
            {
                if (_ParkFeeLedType != value)
                {
                    _ParkFeeLedType = value;
                    SaveConfig("ParkFeeLedType", _ParkFeeLedType.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置满位屏串口号
        /// </summary>
        public byte ParkFullLedCOMPort
        {
            get { return _ParkFullLedCOMPort; }
            set
            {
                if (_ParkFullLedCOMPort != value)
                {
                    _ParkFullLedCOMPort = value;
                    SaveConfig("ParkFullLedCOMPort", value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置小票打印机串口号
        /// </summary>
        public byte BillPrinterCOMPort
        {
            get
            {
                return _BillPrinterCOMPort;
            }
            set
            {
                if (_BillPrinterCOMPort != value)
                {
                    _BillPrinterCOMPort = value;
                    SaveConfig("BillPrinterCOMPort", value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置羊城通读写器串口号
        /// </summary>
        public byte YCTReaderCOMPort
        {
            get
            {
                return _YCTReaderCOMPort;
            }
            set
            {
                if (_YCTReaderCOMPort != value)
                {
                    _YCTReaderCOMPort = value;
                    SaveConfig("YCTReaderCOMPort", value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置车辆信息LED显示屏串口号(目前用于神华项目，分三栏显示）
        /// </summary>
        public byte VehicleLedCOMPort
        {
            get { return _VehicleLedCOMPort; }
            set
            {
                if (_VehicleLedCOMPort != value)
                {
                    _VehicleLedCOMPort = value;
                    SaveConfig("ThreeLedCOMPort", value.ToString());
                }
            }
        }

        /// <summary>
        /// 是否处于调试状态
        /// </summary>
        public bool Debug
        {
            get
            {
                return _Debug;
            }
            set
            {
                if (_Debug != value)
                {
                    _Debug = value;
                    SaveConfig("Debug", _Debug ? "True" : "False");
                }
            }
        }

        /// <summary>
        /// 获取或设置数据库是否需要升级
        /// </summary>
        public bool DatabaseNeedUpgrade
        {
            get
            {
                return _DatabaseNeedUpgrade;
            }
            set
            {
                if (_DatabaseNeedUpgrade != value)
                {
                    _DatabaseNeedUpgrade = value;
                    SaveConfig("DatabaseNeedUpgrade", value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置 实时监控是否重新打开上次打开的视频
        /// </summary>
        public bool OpenLastOpenedVideo
        {
            get
            {
                return _OpenLastOpenedVideo;
            }
            set
            {
                if (_OpenLastOpenedVideo != value)
                {
                    _OpenLastOpenedVideo = value;
                    SaveConfig("OpenLastOpenedVideo", _OpenLastOpenedVideo.ToString());
                }
            }

        }

        /// <summary>
        /// 获取或设置是否只显示侦听事件的停车场硬件
        /// </summary>
        public bool ShowOnlyListenedPark
        {
            get
            {
                return _ShowOnlyListenedPark;
            }
            set
            {
                if (_ShowOnlyListenedPark != value)
                {
                    _ShowOnlyListenedPark = value;
                    SaveConfig("ShowOnlyListenedPark", value.ToString());
                }
            }
        }

        /// <summary>
        /// 软件是否进行优化
        /// </summary>
        public bool Optimized
        {
            get
            {
                return _Optimized;
            }
            set
            {
                if (_Optimized != value)
                {
                    _Optimized = value;
                    SaveConfig("Optimized", _Optimized.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置软件退出时是否需要输入密码
        /// </summary>
        public bool NeedPasswordWhenExit
        {
            get
            {
                return _NeedPasswordWhenExit;
            }
            set
            {
                if (_NeedPasswordWhenExit != null)
                {
                    _NeedPasswordWhenExit = value;
                    SaveConfig("NeedPasswordWhenExit", value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置系统的语言
        /// </summary>
        public string Language
        {
            get { return _Language; }
            set
            {
                if (_Language != value)
                {
                    _Language = value;
                    SaveConfig("Language", value);
                }
            }
        }
        /// <summary>
        /// 获取或设置登录时是否记录登录名
        /// </summary>
        public bool RememberLogID
        {
            get { return _RememberLogID; }
            set
            {
                if (_RememberLogID != value)
                {
                    _RememberLogID = value;
                    SaveConfig("RememberLogID", _RememberLogID.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置是否启用电脑语音报放
        /// </summary>
        public bool EnableTTS
        {
            get
            {
                return _EnableTTS;
            }
            set
            {
                if (_EnableTTS != value)
                {
                    _EnableTTS = value;
                    SaveConfig("EnableTTS", _EnableTTS.ToString());
                }
            }
        }
        /// <summary>
        /// 获取或设置是否加大收费说明框
        /// </summary>
        public bool EnlargeMemo
        {
            get
            {
                return _EnlargeMemo;
            }
            set
            {
                if (_EnlargeMemo != value)
                {
                    _EnlargeMemo = value;
                    SaveConfig("EnlargeMemo", _EnlargeMemo.ToString());
                }
            }
        }
        /// <summary>
        /// 获取或设置在输入完成收费说明后是否直接现金收费
        /// </summary>
        public bool ChargeAfterMemo
        {
            get { return _ChargeAfterMemo; }
            set
            {
                if (_ChargeAfterMemo != value)
                {
                    _ChargeAfterMemo = value;
                    SaveConfig("ChargeAfterMemo", _ChargeAfterMemo.ToString());
                }
            }
        }
        /// <summary>
        /// 获取或设置是否在收费界面显示自助缴费机状态栏
        /// </summary>
        public bool ShowAPMMonitor
        {
            get { return _ShowAPMMonitor; }
            set
            {
                if (_ShowAPMMonitor != value)
                {
                    _ShowAPMMonitor = value;
                    SaveConfig("ShowAPMMonitor", _ShowAPMMonitor.ToString());
                }
            }
        }
        /// <summary>
        /// 获取或设置是否启用中山通
        /// </summary>
        public bool EnableZST
        {
            get { return _EnableZST; }
            set
            {
                if (_EnableZST != value)
                {
                    _EnableZST = value;
                    SaveConfig("EnableZST", value.ToString());
                }
            }
        }
        /// <summary>
        /// 获取或设置中山通桌面读卡器的IP地址
        /// </summary>
        public string ZSTReaderIP
        {
            get
            {
                return _ZSTReaderIP;
            }
            set
            {
                if (_ZSTReaderIP != value)
                {
                    _ZSTReaderIP = value;
                    SaveConfig("ZSTReaderIP", value);
                }
            }
        }

        /// <summary>
        /// 获取或设置是否启用写卡模式
        /// </summary>
        public bool EnableWriteCard
        {
            get
            {
                return _EnableWriteCard;
            }
            set
            {
                if (_EnableWriteCard != value)
                {
                    _EnableWriteCard = value;
                    SaveConfig("EnableWriteCard", value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置是否自动添加系统防火墙例外
        /// </summary>
        public bool AuotAddToFirewallException
        {
            get
            {
                return _AuotAddToFirewallException;
            }
            set
            {
                if (_AuotAddToFirewallException != value)
                {
                    _AuotAddToFirewallException = value;
                    SaveConfig("AuotAddToFirewallException", value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置停车场通讯IP，当用户没有设置，会以查找到电脑的第一个IP为通讯IP，设定时如电脑找不到该通讯IP，则不会初始化停车场通讯
        /// </summary>
        public string ParkingCommunicationIP
        {
            get
            {
                return _ParkingCommunicationIP;
            }
            set
            {
                if (_ParkingCommunicationIP != value)
                {
                    _ParkingCommunicationIP = value;
                    SaveConfig("ParkingCommunicationIP", value);
                }
            }
        }

        /// <summary>
        /// 是否允许更改系统工作模式
        /// </summary>
        public bool AllowChangeParkWorkMode
        {
            get
            {
                return _AllowChangeParkWorkMode;
            }
            set
            {
                if (_AllowChangeParkWorkMode != value)
                {
                    _AllowChangeParkWorkMode = value;
                    SaveConfig("AllowChangeParkWorkMode", value.ToString());
                }
            }
        }

        /// <summary>
        /// 是否使用Ping网络来测试数据库连接
        /// </summary>
        public bool CheckConnectionWithPing
        {
            get
            {
                return _CheckConnectionWithPing;
            }
            set
            {
                if (_CheckConnectionWithPing != value)
                {
                    _CheckConnectionWithPing = value;
                    SaveConfig("CheckConnectionWithPing", value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置车场满位屏类型,主要是用于满位屏直接接在电脑上这种情况,0表示中矿满位屏，1表示科拓满位屏
        /// </summary>
        public int ParkVacantLed
        {
            get
            {
                return _ParkVacantLed;
            }
            set
            {
                if (_ParkVacantLed != value)
                {
                    _ParkVacantLed = value;
                    SaveConfig("ParkVacantLed", value.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置是否显示通道切换按钮
        /// </summary>
        public bool SwitchEntrance
        {
            get
            {
                return _SwitchEntrance;
            }
            set
            {
                if (_SwitchEntrance != value)
                {
                    _SwitchEntrance = value;
                    SaveConfig("SwitchEntrance", _SwitchEntrance.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置是否启用酒店应用
        /// </summary>
        public bool EnableHotel
        {
            get
            {
                return _EnableHotel;
            }
            set
            {
                if (_EnableHotel != value)
                {
                    _EnableHotel = value;
                    SaveConfig("EnableHotel", _EnableHotel.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置-网络控制板是否使用新的卡片有效命令，新命令数据包含卡片属性信息，该功能需控制器硬件支持，否则可能导致卡片出场不能抬闸
        /// </summary>
        public bool NewCardValidCommand
        {
            get
            {
                return _NewCardValidCommand;
            }
            set
            {
                if (_NewCardValidCommand != value)
                {
                    _NewCardValidCommand = value;
                    SaveConfig("NewCardValidCommand", _NewCardValidCommand.ToString());
                }
            }
        }

        /// <summary>
        /// 获取或设置验证CPU卡使用SAM卡所在的读卡器SAM卡座号，范围1~8
        /// </summary>
        public byte ParkingSamNO
        {
            get
            {
                if (_ParkingSamNO > 0 && _ParkingSamNO < 9)
                {
                    return _ParkingSamNO;
                }
                return 1;
            }
            set
            {
                if (_ParkingSamNO > 0 && _ParkingSamNO < 9)
                {
                    _ParkingSamNO = value;
                    SaveConfig("ParkingSamNO", _ParkingSamNO.ToString());
                }
            }
        }
        /// <summary>
        /// 获取或设置是否启用POS收费按钮
        /// </summary>
        public bool EnablePOSButton
        {
            get
            {
                return _EnablePOSButton;
            }
            set
            {
                if (_EnablePOSButton != value)
                {
                    _EnablePOSButton = value;
                    SaveConfig("EnablePosButton", _EnablePOSButton.ToString());
                }
            }
        }
        /// <summary>
        /// 获取或设置是车辆到达时是否播放欢迎语
        /// </summary>
        public bool SpeakPromptWhenCarArrival
        {
            get
            {
                return _SpeakPromptWhenCarArrival;
            }
            set
            {
                if (_SpeakPromptWhenCarArrival != value)
                {
                    _SpeakPromptWhenCarArrival = value;
                    SaveConfig("SpeakPromptWhenCarArrival", _SpeakPromptWhenCarArrival.ToString());
                }
            }
        }
        /// <summary>
        /// 获取或设置软件方式的车牌识别厂家，VECON表示亚视，WINTONE表示清华文通，其他默认使用清华文通
        /// </summary>
        public CarPlateRecognizationType CarPlateRecognization
        {
            get
            {
                return _CarPlateRecognization;
            }
            set
            {
                if (_CarPlateRecognization != value)
                {
                    _CarPlateRecognization = value;
                    string temp = string.Empty;
                    switch (_CarPlateRecognization)
                    {
                        case CarPlateRecognizationType.VECON:
                            temp = "VECON";
                            break;
                        case CarPlateRecognizationType.XinLuTong:
                            temp = "XinLuTong";
                            break;
                        case CarPlateRecognizationType.DaHua:
                            temp = "DaHua";
                            break;
                        default:
                            temp = "WINTONE";
                            break;
                    }
                    SaveConfig("CarPlateRecognization", temp);
                }
            }
        }
        
        #endregion

        #region 公共方法
        public bool SaveConfig(string configName, string configContent)
        {
            if (_parent != null)
            {
                try
                {
                    XmlElement add = null;
                    XmlAttribute key = null;
                    XmlAttribute value = null;
                    XmlNodeList nodeList = _parent.ChildNodes;
                    foreach (XmlNode xn in nodeList)
                    {
                        if (xn is XmlElement)
                        {
                            XmlElement xe = (XmlElement)xn;
                            if (xe.GetAttribute("key") == configName)
                            {
                                xe.SetAttribute("value", configContent);
                                add = xe;
                                break;
                            }
                        } // end if
                    }
                    if (add == null)
                    {
                        add = _doc.CreateElement("add");
                        key = _doc.CreateAttribute("key");
                        key.Value = configName;
                        value = _doc.CreateAttribute("value");
                        value.Value = configContent;

                        add.Attributes.Append(key);
                        add.Attributes.Append(value);
                        _parent.AppendChild(add);
                    }
                    this._doc.Save(_path.ToString());
                    return true;
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
            }
            return false;
        }

        public string GetConfigContent(string configName)
        {
            if (_parent != null)
            {
                try
                {
                    XmlNodeList nodeList = _parent.ChildNodes;
                    foreach (XmlNode xn in nodeList)
                    {
                        if (xn is XmlElement)
                        {
                            XmlElement xe = (XmlElement)xn;
                            if (xe.GetAttribute("key") == configName)
                            {
                                return xe.GetAttribute("value");
                            }
                        } // end if
                    }
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
            }
            return "";
        }
        #endregion
    }
}
