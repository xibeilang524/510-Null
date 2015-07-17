using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MyNet.CCTV.Control.Implement.DaHua;
using Ralid.Park.VideoCapture;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.UserControls.VideoPanels
{
    /// <summary>
    /// 表示大华网络摄像机视频控件
    /// </summary>
    public partial class DaHuaVideoPanel : Ralid.Park.UserControls.VideoPanels.VideoPanel
    {
        #region 构造函数
        public DaHuaVideoPanel()
        {
            InitializeComponent();

            disConnect = new fDisConnect(DisConnectEvent);
            onlineMsg = new fHaveReConnect(OnlineEvent);
            //anaCallback = new fAnalyzerDataCallBack(AnalyzerDataCallBackEvent);

        }
        //~DaHuaVideoPanel()
        //{
        //    if (initialized)
        //    {
        //        DaHuaSDKManager.GetInstance().DisConnectEventHandle -= disConnect;
        //        DaHuaSDKManager.GetInstance().OnlineMsgEventHandle -= onlineMsg;
        //    }
        //}
        #endregion

        #region 私有变量
        private fDisConnect disConnect;       //设备离线消息
        private fHaveReConnect onlineMsg;     //设备重新在线消息
        //private fAnalyzerDataCallBack anaCallback; //设备过车事件及抓拍等消息
        
        private int m_nLoginID;     // 登陆返回的句柄
        //private int m_nRealLoadPic; // 设备消息事件订阅句柄
        private int m_realPlayH;    // 启动实时监控返回的句柄

        private bool initialized; //设备是否已初始化
        //private string snapPath;  //手动抓拍图片保存的文件路径

        //private AutoResetEvent _SnapEvent = new AutoResetEvent(false); //图片抓拍事件通知

        /// <summary>
        /// 是否不打开视频时抓拍
        /// </summary>
        private bool _IsReadyForSnapshot;
        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化设备
        /// </summary>
        private bool Init()
        {
            //初始化SDK
            if (initialized == false)
            {
                //DHClient.DHInit(disConnect, IntPtr.Zero);
                //DHClient.DHSetAutoReconnect(onlineMsg, IntPtr.Zero);
                //initialized = true;
                //这里不直接使用DHClient初始化，是因为DHClient的disConnect和onlineMsg只支持一个事件回调，
                //所以这里使用管理器的事件处理，在管理器中使用DHClient初始化
                DaHuaSDKManager.GetInstance().DisConnectEventHandle -= disConnect;
                DaHuaSDKManager.GetInstance().DisConnectEventHandle += disConnect;
                DaHuaSDKManager.GetInstance().OnlineMsgEventHandle -= onlineMsg;
                DaHuaSDKManager.GetInstance().OnlineMsgEventHandle += onlineMsg;
                DaHuaSDKManager.GetInstance().InitSDK();
                initialized = true;
            }
            if (initialized == false) return false;

            //登入设备
            if (m_nLoginID == 0)
            {
                NET_DEVICEINFO deviceInfo = new NET_DEVICEINFO();
                int error = 0;
                m_nLoginID = DHClient.DHLogin(VideoSource.MediaSource, (ushort)VideoSource.StreamPort
                                                , VideoSource.UserName, VideoSource.Password, out deviceInfo, out error);
            }
            if (m_nLoginID == 0) return false;

            return true;
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

                    //初始化设备
                    if (Init() == false) return;

                    //启动监视
                    m_realPlayH = DHClient.DHRealPlay(m_nLoginID, VideoSource.Channel, this.video.Handle);
                    if (m_realPlayH == 0) return;
                    
                    this._Status = VideoStatus.Playing;
                }
            }
        }

        public override void Pause()
        {
            lock (_StatusLock)
            {
                if (VideoSource == null) return;
                if (m_realPlayH != 0)
                {
                    DHClient.DHStopRealPlay(m_realPlayH);
                    m_realPlayH = 0;
                }
                this._Status = VideoStatus.Paused;
            }
        }

        public override void Close()
        {
            lock (_StatusLock)
            {
                if (VideoSource != null)
                {
                    ////停止消息订阅句柄
                    //if (m_nRealLoadPic != 0)
                    //{
                    //    DHClient.DHStopLoadPic(m_nRealLoadPic);
                    //    m_nRealLoadPic = 0;
                    //}
                    //停止监视
                    if (m_realPlayH != 0)
                    {
                        DHClient.DHStopRealPlay(m_realPlayH);
                        m_realPlayH = 0;
                    }
                    if (m_nLoginID != 0)
                    {
                        DHClient.DHLogout(m_nLoginID);
                        m_nLoginID = 0;
                    }
                    if (initialized)
                    {
                        DaHuaSDKManager.GetInstance().DisConnectEventHandle -= disConnect;
                        DaHuaSDKManager.GetInstance().OnlineMsgEventHandle -= onlineMsg;
                        initialized = false;
                    }
                }
                this._Status = VideoStatus.Disconnected;
            }
            if (!this.TitlePanel.InvokeRequired)
            {
                this.TitlePanel.Visible = false;
                this.video.BackColor = Color.Navy;
                this.video.Image = null;
            }
        }

        public override void OpenForSnapshot(bool _async)
        {
            _IsReadyForSnapshot = true;
            //Init();
            //////发起订阅设备事件消息
            ////if (m_nRealLoadPic == 0)
            ////{
            ////    m_nRealLoadPic = DHClient.DHRealLoadPicture(m_nLoginID, VideoSource.Channel, EventIvs.EVENT_IVS_ALL, anaCallback, 0);
            ////}
        }

        public override bool IsReadyForSnapshot
        {
            get
            {
                return _IsReadyForSnapshot;
                //return m_nLoginID != 0;
                ////return m_nLoginID != 0 && m_nRealLoadPic != 0;
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
            try
            {
                if (_IsReadyForSnapshot)
                {
                    //不打开视频时抓拍
                    IVideoCapture capture = VideoCaptureManager.Instance[(int)VideoServerType.DaHua];
                    if (capture != null)
                    {
                        path = capture.CapturePicture(this.VideoSource, force);
                        success = !string.IsNullOrEmpty(path);
                    }
                }
                else
                {
                    lock (_StatusLock)
                    {
                        //////发起订阅设备事件消息
                        ////if (m_nRealLoadPic == 0)
                        ////{
                        ////    m_nRealLoadPic = DHClient.DHRealLoadPicture(m_nLoginID, VideoSource.Channel, EventIvs.EVENT_IVS_ALL, anaCallback, 0);
                        ////}
                        //////设置上传图片的存储路径
                        ////snapPath = path;

                        //////触发手动抓拍测试
                        ////MANUAL_SNAP_PARAMETER snap = new MANUAL_SNAP_PARAMETER();
                        ////snap.nChannel = VideoSource.Channel;
                        ////IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(snap));
                        ////Marshal.StructureToPtr(snap, ptr, false);
                        ////bool bRet = DHClient.DHControlDevice(m_nLoginID, CtrlType.DH_MANUAL_SNAP, ptr, 1000);
                        ////System.Runtime.InteropServices.Marshal.FreeHGlobal(ptr);

                        //////等待图片上传
                        ////_SnapEvent.WaitOne(timeout);

                        //////清除上传图片的存储路径
                        ////snapPath = string.Empty;

                        ////////取消订阅事件消息
                        //////if (m_nRealLoadPic != 0)
                        //////{
                        //////    DHClient.DHStopLoadPic(m_nRealLoadPic);
                        //////    m_nRealLoadPic = 0;
                        //////}   

                        //if (m_realPlayH != 0)
                        //{
                        //    success = DHClient.DHCapturePicture(m_realPlayH, path);
                        //}

                        //打开视频时抓拍
                        if (m_realPlayH != 0)
                        {
                            success = DHClient.DHCapturePicture(m_realPlayH, path);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return success;
        }

        public override void ClearSnapShot()
        {
            try
            {
                IVideoCapture capture = VideoCaptureManager.Instance[(int)VideoServerType.DaHua];
                if (capture != null)
                {
                    capture.ClearCapture(this.VideoSource);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        #region 回调事件
        /// <summary>
        /// 设备离线事件
        /// </summary>
        /// <param name="lLoginID"></param>
        /// <param name="pchDVRIP"></param>
        /// <param name="nDVRPort"></param>
        /// <param name="dwUser"></param>
        private void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            if (lLoginID == m_nLoginID)
            {
                ////设备离线消息；设备非正常关机，SDK可以检测到；需要取消订阅,当重新在线消息时，再发起订阅事件
                //if (m_nRealLoadPic != 0)
                //{
                //    DHClient.DHStopLoadPic(m_nRealLoadPic);
                //    m_nRealLoadPic = 0;
                //}
                this._Status = VideoStatus.Disconnected;

                Action action = delegate()
                {
                    //this.Caption = string.Format("{0} - 断线", VideoSource.VideoName);
                    this.CaptionColor = Color.Red;
                };
                if (!this.InvokeRequired)
                {
                    action();
                }
                else
                {
                    this.BeginInvoke(action);
                }
            }
        }

        /// <summary>
        /// 自动重连成功事件
        /// </summary>
        /// <param name="lLoginID"></param>
        /// <param name="pchDVRIP"></param>
        /// <param name="nDVRPort"></param>
        /// <param name="dwUser"></param>
        private void OnlineEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            if (lLoginID == m_nLoginID)
            {
                ////自动重连成功事件后，再发起订阅设备事件消息
                //if (m_nRealLoadPic != 0)
                //{
                //    m_nRealLoadPic = DHClient.DHRealLoadPicture(m_nLoginID, VideoSource.Channel, EventIvs.EVENT_IVS_ALL, anaCallback, 0);
                //}
                this._Status = VideoStatus.Connected;
                Action action = delegate()
                {
                    //this.Caption = string.Format("{0} - 重连", VideoSource.VideoName);
                    this.CaptionColor = Color.White;
                };
                if (!this.InvokeRequired)
                {
                    action();
                }
                else
                {
                    this.BeginInvoke(action);
                }
            }
        }

        /// <summary>
        /// 开始智能交通设备实时上传--回调
        /// </summary>
        /// <param name="lAnalyzerHandle"></param>
        /// <param name="dwAlarmType"></param>
        /// <param name="pAlarmInfo"></param>
        /// <param name="pBuffer"></param>
        /// <param name="dwBufSize"></param>
        /// <param name="dwUser"></param>
        /// <param name="nSequence"></param>
        /// <param name="reserved"></param>
        /// <returns></returns>
        //private int AnalyzerDataCallBackEvent(Int32 lAnalyzerHandle, UInt32 dwAlarmType, IntPtr pAlarmInfo, IntPtr pBuffer, UInt32 dwBufSize, UInt32 dwUser, Int32 nSequence, IntPtr reserved)
        //{
        //    if (dwBufSize == 0)
        //    {
        //        return 1;
        //    }

        //    //当抓拍时需要保存上传图片时，才保存上传的图片
        //    if (!string.IsNullOrEmpty(snapPath))
        //    {
        //        try
        //        {
        //            // 记录文件
        //            byte[] buf = new byte[dwBufSize];
        //            Marshal.Copy(pBuffer, buf, 0, (int)dwBufSize);
        //            File.WriteAllBytes(snapPath, buf);
        //        }
        //        catch
        //        { 
        //        }
        //        //通知抓拍等待，图片已上传
        //        _SnapEvent.Set();
        //    }
        //    return 1;
        //}
        #endregion
    }
}
