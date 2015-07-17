using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Ralid.Park.UserControls.VideoPanels
{
    /// <summary>
    /// 表示景阳网络摄像机视频控件
    /// </summary>
    public partial class JingYangVideoPanel : VideoPanel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public JingYangVideoPanel()
        {
            InitializeComponent();
        }

        #region 常量
        /// <summary>
        /// 远程抓拍超时时间
        /// </summary>
        private const int _RemoteSnapshotTimeout = 1000;
        #endregion

        #region 私有变量
        private int nHandle = 0;
        private bool _IsReadyForSnapshot = false;
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="nRet"></param>
        /// <returns></returns>
        private string GetErrMessage(int nRet)
        {
            string szErrorInfo = string.Empty;
            byte[] errorby = new byte[512];
            string retStr = "";
            InitRemoteLivePlayer2.Remote_Nvd_formatMessage(nRet, errorby, 512);
            retStr = Encoding.ASCII.GetString(errorby);

            return retStr;
        }
        #endregion

        #region 重写基类方法
        public override void Play(bool isAsync)
        {
            lock (_StatusLock)
            {
                if (this._Status != VideoStatus.Playing)
                {
                    if (VideoSource == null) return;
                    if (!this.TitlePanel.InvokeRequired) //显示视频头
                    {
                        this.Caption = VideoSource.VideoName;
                        this.TitlePanel.Visible = ShowTitle;
                    }

                    ST_DeviceInfo stDeviceInfo = new ST_DeviceInfo();
                    stDeviceInfo.st_InetAddr.szHostIP = VideoSource.MediaSource;
                    stDeviceInfo.st_InetAddr.nPORT = (ushort)VideoSource.StreamPort;
                    stDeviceInfo.st_InetAddr.nIPProtoVer = 1;
                    stDeviceInfo.szUserID = VideoSource.UserName;
                    stDeviceInfo.szPassword = VideoSource.Password;
                    stDeviceInfo.nDeviceType = 1;  //

                    int nRet = 0;
                    int nProtocol = 1;  //1表示udp协议,2表示tcp协议 4表示rtp协议
                    //初始化设备信息
                    nRet = InitRemoteLivePlayer2.Remote_Nvd_Init(ref nHandle, ref stDeviceInfo, nProtocol);
                    if (nRet != 0) return;

                    //设置是否拉伸模式
                    nRet = InitRemoteLivePlayer2.Remote_LivePlayer2_SetStretchMode(nHandle, StretchToFit);
                    nRet = InitRemoteLivePlayer2.Remote_LivePlayer2_SetAutoResizeFlag(nHandle, StretchToFit);
                    if (nRet != 0) return;

                    //设置默认流ID，需根据网络摄像机的流设置来设置ID，不同码流会有不同的分辨率
                    nRet = InitRemoteLivePlayer2.Remote_LivePlayer2_SetDefaultStreamId(nHandle, VideoSource.Channel);
                    if (nRet != 0) return;

                    nRet = InitRemoteLivePlayer2.Remote_LivePlayer2_SetAutoConnectFlag(nHandle, VideoSource.AutoReconnect);
                    if (nRet != 0) return;

                    //打开摄像机
                    nRet = InitRemoteLivePlayer2.Remote_LivePlayer2_Open(nHandle, 1);
                    if (nRet != 0) return;

                    //播放视频
                    nRet = InitRemoteLivePlayer2.Remote_LivePlayer2_Play(nHandle);
                    if (nRet != 0) return;

                    nRet = InitRemoteLivePlayer2.Remote_LivePlayer2_SetVideoWindow(nHandle, int.Parse(video.Handle.ToString()), 0, 0, video.Width, video.Height);
                    if (nRet != 0) return;

                    //设置远程抓拍超时时间
                    nRet = InitRemoteLivePlayer2.Remote_Snapshot_SetTimeout(nHandle, _RemoteSnapshotTimeout);

                    ////打开远程抓拍,不调用这个方法，打开视频时不能抓拍图片
                    //nRet = InitRemoteLivePlayer2.Remote_Snapshot_Open(nHandle);//在抓拍时才打开

                    if (nRet != 0) return;
                    this._Status = VideoStatus.Playing;
                    this._IsReadyForSnapshot = true;
                }
            }
        }

        public override void Pause()
        {
            lock (_StatusLock)
            {
                if (VideoSource == null) return;
                int nRet = InitRemoteLivePlayer2.Remote_LivePlayer2_Pause(nHandle);
                this._Status = VideoStatus.Paused;
                this._IsReadyForSnapshot = false;
            }
        }

        public override void Close()
        {
            lock (_StatusLock)
            {
                if (VideoSource != null)
                {
                    InitRemoteLivePlayer2.Remote_Snapshot_Close(nHandle);
                    InitRemoteLivePlayer2.Remote_Nvd_UnInit(nHandle);
                    InitRemoteLivePlayer2.Remote_LivePlayer2_Close(nHandle);
                }
                this._Status = VideoStatus.Disconnected;
                this._IsReadyForSnapshot = false;
            }
            if (!this.TitlePanel.InvokeRequired)
            {
                this.TitlePanel.Visible = false;
                this.video.BackColor = Color.Navy;
            }
        }

        public override void OpenForSnapshot(bool _async)
        {
            if (!_IsReadyForSnapshot)
            {
                ST_DeviceInfo stDeviceInfo = new ST_DeviceInfo();
                stDeviceInfo.st_InetAddr.szHostIP = VideoSource.MediaSource;
                stDeviceInfo.st_InetAddr.nPORT = (ushort)VideoSource.StreamPort;
                stDeviceInfo.st_InetAddr.nIPProtoVer = 1;
                stDeviceInfo.szUserID = VideoSource.UserName;
                stDeviceInfo.szPassword = VideoSource.Password;
                stDeviceInfo.nDeviceType = 1;  //

                int nRet = 0;
                int nProtocol = 1;  //1表示udp协议,2表示tcp协议 4表示rtp协议
                //初始化设备信息
                nRet = InitRemoteLivePlayer2.Remote_Nvd_Init(ref nHandle, ref stDeviceInfo, nProtocol);

                //设置远程抓拍超时时间
                nRet = InitRemoteLivePlayer2.Remote_Snapshot_SetTimeout(nHandle, _RemoteSnapshotTimeout);

                //if (nRet == 0)
                //{
                //    nRet = InitRemoteLivePlayer2.Remote_Snapshot_Open(nHandle);//在抓拍时才打开远程抓拍
                //}
                this._IsReadyForSnapshot = (nRet == 0);
            }
        }

        public override bool IsReadyForSnapshot
        {
            get
            {
                return _IsReadyForSnapshot;
            }
        }

        public override bool SnapShotTo(ref string path)
        {
            return SnapShotTo(ref path, 1000, false);
        }        

        /// <summary>
        /// 抓拍图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="timeout">超时时间(ms)</param>
        /// <returns></returns>
        public override bool SnapShotTo(ref string path, int timeout, bool force)
        {
            bool success = false;
            ST_AVFrameData stAVFrameData = new ST_AVFrameData();
            try
            {
                lock (_StatusLock)
                {
                    //在初始化时设置超时时间了，这里就不再设置超时时间了
                    //int nRet = InitRemoteLivePlayer2.Remote_Snapshot_SetTimeout(nHandle, timeout);

                    //在这里打开远程抓拍，是因为当打开远程抓拍后，如果一段时间内不进行抓拍动作，再进行抓拍时会报错
                    //所以在这里打开远程抓拍，并且抓拍完成后，需关闭远程抓拍
                    int nRet = InitRemoteLivePlayer2.Remote_Snapshot_Open(nHandle);
                    if (nRet == 0)
                    {
                        ST_RemoteSnapshotParam stRemoteSnapshotParam = new ST_RemoteSnapshotParam();
                        //stRemoteSnapshotParam.nCameraID = this.VideoSource.Channel;
                        //这里设置nCameraID为1，是因为IP Camera设备只有一个Camera，所以固定设为1
                        stRemoteSnapshotParam.nCameraID = 1;
                        stRemoteSnapshotParam.nQuality = 5;
                        stRemoteSnapshotParam.nPhotoFormat = 1;// PHOTOFORMAT_JPEG

                        nRet = InitRemoteLivePlayer2.Remote_Snapshot_GetSnapshotPicture(nHandle, ref stRemoteSnapshotParam, ref stAVFrameData);
                        if (nRet != 0)
                        {
                            if (Ralid.Park.BusinessModel.Configuration.AppSettings.CurrentSetting.Debug)
                            {
                                string logmsg = string.Format("【{0}】远程抓拍失败  ", this.VideoSource.VideoName);
                                logmsg += " 错误代码：" + nRet;
                                logmsg += " 错误信息：" + GetErrMessage(nRet);

                                Ralid.GeneralLibrary.LOG.FileLog.Log("FrmSnapShoter", logmsg);
                            }
                        }
                    }
                    else
                    {
                        if (Ralid.Park.BusinessModel.Configuration.AppSettings.CurrentSetting.Debug)
                        {
                            string logmsg = string.Format("【{0}】打开远程抓拍失败  ", this.VideoSource.VideoName);
                            logmsg += " 错误代码：" + nRet;
                            logmsg += " 错误信息：" + GetErrMessage(nRet);
                            Ralid.GeneralLibrary.LOG.FileLog.Log("FrmSnapShoter", logmsg);
                        }
                    }
                    if (nRet == 0)
                    {
                        byte[] data = new byte[stAVFrameData.nDataLength];
                        System.Runtime.InteropServices.Marshal.Copy(stAVFrameData.pszData, data, 0, stAVFrameData.nDataLength);
                        File.WriteAllBytes(path, data);
                    }

                    //关闭远程抓拍
                    nRet = InitRemoteLivePlayer2.Remote_Snapshot_Close(nHandle);
                    if (nRet != 0)
                    {
                        if (Ralid.Park.BusinessModel.Configuration.AppSettings.CurrentSetting.Debug)
                        {
                            string logmsg = string.Format("【{0}】关闭远程抓拍失败  ", this.VideoSource.VideoName);
                            logmsg += " 错误代码：" + nRet;
                            logmsg += " 错误信息：" + GetErrMessage(nRet);
                            Ralid.GeneralLibrary.LOG.FileLog.Log("FrmSnapShoter", logmsg);
                        }
                    }

                    return File.Exists(path);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return success;
        }
        #endregion

        private void video_Resize(object sender, EventArgs e)
        {
            lock (_StatusLock)
            {
                if (_Status == VideoStatus.Playing)
                {
                    InitRemoteLivePlayer2.Remote_LivePlayer2_SetVideoWindow(nHandle, int.Parse(video.Handle.ToString()), 0, 0, video.Width, video.Height);
                }
            }
        }

        private void SetVideoWindow()
        {
            Action action = delegate()
            {
                InitRemoteLivePlayer2.Remote_LivePlayer2_SetVideoWindow(nHandle, int.Parse(video.Handle.ToString()), 0, 0, video.Width, video.Height);
            };
            if (this.video.InvokeRequired)
            {
                this.video.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }

    #region 动态库DLLImport 和一些结构
    //IP地址
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ST_InetAddr
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 49)]
        public string szHostIP;		//IP地址（点分符形式）
        public ushort nPORT;		//端口号
        public Int32 nIPProtoVer;		//IP协议版本（1：IPv4协议，2：IPv6协议）
    }


    //设备信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ST_DeviceInfo
    {

        public ST_InetAddr st_InetAddr;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct ST_InetAddr
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 49)]
            public string szHostIP;		//IP地址（点分符形式）
            public ushort nPORT;		//端口号
            public Int32 nIPProtoVer;		//IP协议版本（1：IPv4协议，2：IPv6协议）
        }

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string szUserID;		//登陆设备的用户ID

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string szPassword;	//登陆设备的密码

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string szDeviceID;	//设备ID

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string szDeviceName;	//设备名称

        public int nDeviceType;	//设备类型
    }

    //视频信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ST_VideoInfo
    {
        public int nBitRate;								//比特率

        public int nBitErrorRate;

        public int nTimePerFrame;

        public int nSize;

        public int nWidth;									//视频宽度

        public int nHeight;								//视频高度

        public int nPlanes;

        public int nBitCount;

        public int nCompression;

        public int nSizeImage;

        public int nXPelsPerMeter;

        public int nYPelsPerMeter;

        public int nClrUsed;

        public int nClrImportant;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string pszSPS_PPSData;

        public int nSPS_PPSDataLen;
    }

    //音频信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ST_AudioInfo
    {
        public int nFormatTag;			//格式标记

        public int nChannels;			//声音通道数（声道）

        public int nSamplesPerSec;		//每秒采样数

        public int nAvgBytesPerSec;	//

        public int nBlockAlign;		//

        public int nBitsPerSample;		//每次采样的大小

        public int nCBSize;			//后面追加数据长度

        public int nEncodeType;
    }

    //设备概要信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ST_DeviceSummaryParam
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szDeviceName;				//设备名称
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szDeviceId;					//设备号
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szManufacturerId;			//设备型号
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szManufacturerName;			//生产厂家
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szProductModel;				//产品模组
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szProductDescription;		//产品描述
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szHardwareModel;			//硬件模组
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szHardwareDescription;		//硬件描述
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szMACAddress;				//MAC地址
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szBarCode;					//机器条形码
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szProductionTime;			//生产时间

        public int nDeviceType;					//设备类型
        public int nVideoSystem;					//编码帧模式

        public int nCameraNum;						//摄像头数
        public int nAlarmInNum;					//报警输入个数
        public int nAlarmOutNum;					//报警输出个数
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szHardwareVer;				//版本信息
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szSoftwareVer;

        public int nRS485Num;						//RS485串口个数
        public int nRS232Num;						//RS232串口个数
    }

    //设备基本信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ST_CameraInfoParam
    {
        public int nCameraId;				//摄像机的ID号
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szCameraName;			//摄像机的名字
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
        public string szCameraModel;			//摄像机的样式	
        public int nVideoSystem;			//视频制式
    }

    //音视频类
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ST_AVFrameData
    {
        public int nStreamFormat;			//1表示原始流，2表示TS混合流

        public int nESStreamType;						//原始流类型，1表示视频，2表示音频

        public int nEncoderType;						//编码格式

        public int nCameraNo;							//摄像机号，表示数据来自第几路

        public int nSequenceId;						//数据帧序号

        public int nFrameType;						//数据帧类型,1表示I帧, 2表示P帧, 0表示未知类型


        public System.Int64 nTimeStamp;				//数据采集时间戳，单位为毫秒


        public IntPtr pszData;							//数据

        public int nDataLength;						//数据有效长度

        public int nFrameRate;							//帧率

        public int nBitRate;							//当前码率　

        public int nImageFormatId;						//当前格式

        public int nImageWidth;						//视频宽度

        public int nImageHeight;						//视频高度

        public int nVideoSystem;						//当前视频制式

        public int nFrameBufLen;						//当前缓冲长度

        public int nStreamId;							// 流ID
        public int nTimezone;							// 时区
        public int nDaylightSavingTime;				//夏令时
    }

    //快照参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ST_RemoteSnapshotParam
    {
        public int nCameraID;													//通道号
        public int nQuality;													//质量
        public int nPhotoFormat;												//图像格式
    };

    internal class InitRemoteLivePlayer2
    {
        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_Nvd_Init(ref int p_handle, ref ST_DeviceInfo st_DeviceInfo, int p_nTransferProtocol);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_Nvd_UnInit(int p_hHandle);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SetStretchMode(int p_hHandle, bool p_bStretchMode);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SetAutoConnectFlag(int p_hHandle, bool p_bAutoFlag);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SetDefaultStreamId(int p_hHandle, int p_nStreamId);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SetVideoWindow(int p_hHandle, int p_hDisplayWnd, int x, int y, int width, int height);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SetUseTimeStamp(int p_hHandle, bool bUseFlag);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_Open(int p_hHandle, int p_nCameraID);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_Play(int p_hHandle);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_Pause(int p_hHandle);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_Close(int p_hHandle);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_PlaySound(int p_hHandle);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_StopSound(int p_hHandle);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_IsOnSound(int p_hHandle, bool p_bOnSound);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SetCurrentContrast(int p_hHandle, int p_nValue);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SetCurrentBrightness(int p_hHandle, int p_nValue);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SetCurrentHue(int p_hHandle, int p_nValue);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SetCurrentSaturation(int p_hHandle, int p_nValue);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SnapShot(int p_hHandle, string p_pszFileName);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_Snapshot_SetTimeout(int p_hHandle, int p_nTimeout);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_Snapshot_Open(int p_hHandle);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_Snapshot_Close(int p_hHandle);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_Snapshot_GetSnapshotPicture(int p_hHandle, ref ST_RemoteSnapshotParam p_pstRemoteSnapshotParam, ref ST_AVFrameData p_pstAVFrameData);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SetRecorderFile(int p_hHandle, string p_pszFileName);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_StartRecord(int p_hHandle);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_StopRecord(int p_hHandle);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_GetRecorderStatus(int p_hHandle, int p_nStatus);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_ResizeWindow(long p_hHandle, int x, int y, int width, int height);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SetAutoResizeFlag(long p_hHandle, bool p_bAutoResizeFlag);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_LivePlayer2_SetAutoConnectFlag(long p_hHandle, bool p_bAutoFlag);

        [DllImport("NvdcDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern int Remote_Nvd_formatMessage(int p_nErrorCode, byte[] p_pszErrorMessage, int p_nMessageBufLen);
    }
    #endregion
}
