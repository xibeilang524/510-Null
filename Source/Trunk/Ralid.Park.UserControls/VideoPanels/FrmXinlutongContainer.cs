using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;

namespace Ralid.Park.UserControls.VideoPanels
{
    public delegate void  VideoRecievedEventHandler(object sender,int channel,byte[] data);

    public partial class FrmXinlutongContainer : Form
    {
        #region 处理共享内存的函数
        //打开共享内存
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);
        ///读共享内存中的数据
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);
        //关闭内存映射文件
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(IntPtr handle);
        private const int FILE_MAP_READ = 0x0004;
        #endregion

        #region 静态属性
        private static FrmXinlutongContainer  _Instance;
        /// <summary>
        /// 获取单个实例
        /// </summary>
        /// <returns></returns>
        public static FrmXinlutongContainer GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new FrmXinlutongContainer();
            }
            return _Instance;
        }
        #endregion

        public FrmXinlutongContainer()
        {
            InitializeComponent();
        }

        #region 私有变量
        private Dictionary<string, AxHVActiveX2Lib.AxHVActiveX2> _AllDevices = new Dictionary<string, AxHVActiveX2Lib.AxHVActiveX2>();
        #endregion

        #region 私有方法
        private void CloseUnUsedAxHV_Thread()
        {
            while (true)
            {
                if (_AllDevices != null && _AllDevices.Count > 0)
                {
                    foreach (AxHVActiveX2Lib.AxHVActiveX2 axHV in _AllDevices.Values)
                    {
                        if (axHV.Tag is DateTime)
                        {
                            DateTime dt = (DateTime)(axHV.Tag);
                            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - dt.Ticks);
                            if (ts.TotalMinutes >= 2)
                            {
                                if (axHV.GetStatus() == 0)
                                {
                                    axHV.RecvVideoFlag = 0;
                                    axHV.Disconnect();
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(30);
            }
        }
        #endregion

        #region 公共方法
        public void Init()
        {
            List<EntranceInfo> entrances = ParkBuffer.Current.GetEntrances();
            if (entrances != null && entrances.Count > 0)
            {
                foreach (EntranceInfo entrance in entrances)
                {
                    if (entrance.VideoSources != null && entrance.VideoSources.Count > 0)
                    {
                        foreach (VideoSourceInfo video in entrance.VideoSources)
                        {
                            if (!_AllDevices.Keys.Contains(video.MediaSource))
                            {
                                AxHVActiveX2Lib.AxHVActiveX2 axHV = new AxHVActiveX2Lib.AxHVActiveX2();
                                ((System.ComponentModel.ISupportInitialize)(axHV)).BeginInit();
                                this.Controls.Add(axHV);
                                axHV.Visible = false;
                                ((System.ComponentModel.ISupportInitialize)(axHV)).EndInit();
                                axHV.RecvSnapImageFlag = 0;
                                axHV.RecvPlateImageFlag = 0;
                                axHV.RecvPlateBinImageFlag = 0;
                                axHV.AutoSaveFlag = false;
                                axHV.OnReceiveVideo += new EventHandler(axHV_OnReceiveVideo);
                                axHV.OnReceiveVideo += new EventHandler(axHV_OnReceiveVideo1);
                                _AllDevices.Add(video.MediaSource , axHV);
                            }
                        }
                    }
                }
            }

            Thread t = new Thread(CloseUnUsedAxHV_Thread);
            t.IsBackground = true;
            t.Start();
        }

        public void RequestVideo(string ip, VideoRecievedEventHandler callback)
        {
            if (_AllDevices.Keys.Contains(ip))
            {
                AxHVActiveX2Lib.AxHVActiveX2 axHV = _AllDevices[ip];
                if (axHV.GetStatus() != 0)
                {
                    axHV.ConnectTo(ip);
                    axHV.RecvVideoFlag = 1;
                }
                List<VideoRecievedEventHandler> handlers = axHV.Tag as List<VideoRecievedEventHandler>;
                if (handlers == null) handlers = new List<VideoRecievedEventHandler>();
                if (!handlers.Any(c => c == callback)) handlers.Add(callback);
                axHV.Tag = handlers;
            }
        }

        public void CancelVideo(string ip, VideoRecievedEventHandler callback)
        {
            if (_AllDevices.Keys.Contains(ip))
            {
                AxHVActiveX2Lib.AxHVActiveX2 axHV = _AllDevices[ip];
                List<VideoRecievedEventHandler> handlers = axHV.Tag as List<VideoRecievedEventHandler>;
                if (handlers != null)
                {
                    handlers.Remove(callback);
                    if (handlers.Count == 0)
                    {
                        axHV.Tag = DateTime.Now;
                    }
                }
            }
        }
        #endregion

        #region 事件处理方法
        private void axHV_OnReceiveVideo(object sender, EventArgs e)
        {
            AxHVActiveX2Lib.AxHVActiveX2 axHV = sender as AxHVActiveX2Lib.AxHVActiveX2;
            IntPtr irMapFile = IntPtr.Zero;
            IntPtr itData = IntPtr.Zero;
            string strName = "";
            int iSize = 1024 * 1024;
            try
            {
                strName = axHV.GetVideoFrameSM(0, ref iSize);
                irMapFile = OpenFileMapping(FILE_MAP_READ, false, strName);
                if (irMapFile == IntPtr.Zero) return;

                itData = MapViewOfFile(irMapFile, FILE_MAP_READ, 0, 0, 0);
                if (itData == IntPtr.Zero)
                {
                    CloseHandle(irMapFile);
                    return;
                }
                byte[] bData = new byte[iSize];
                Marshal.Copy(itData, bData, 0, iSize);

                List<VideoRecievedEventHandler> handlers = axHV.Tag as List<VideoRecievedEventHandler>;
                if (handlers != null && handlers.Count > 0)
                {
                    foreach (VideoRecievedEventHandler handler in handlers)
                    {
                        byte[] data = new byte[iSize];
                        Array.Copy(bData, data, bData.Length);
                        handler(sender, 0, data);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ///完成一次接收后调用下面两个函数关闭共享内存(也就是不用内存映射的时候要关闭)
                UnmapViewOfFile(itData);
                CloseHandle(irMapFile);
            }
        }

        private void axHV_OnReceiveVideo1(object sender, EventArgs e)
        {
            AxHVActiveX2Lib.AxHVActiveX2 axHV = sender as AxHVActiveX2Lib.AxHVActiveX2;
            IntPtr irMapFile = IntPtr.Zero;
            IntPtr itData = IntPtr.Zero;
            string strName = "";
            int iSize = 1024 * 1024;
            try
            {
                strName = axHV.GetVideoFrameSM(1, ref iSize);
                irMapFile = OpenFileMapping(FILE_MAP_READ, false, strName);
                if (irMapFile == IntPtr.Zero) return;

                itData = MapViewOfFile(irMapFile, FILE_MAP_READ, 0, 0, 0);
                if (itData == IntPtr.Zero)
                {
                    CloseHandle(irMapFile);
                    return;
                }
                byte[] bData = new byte[iSize];
                Marshal.Copy(itData, bData, 0, iSize);

                List<VideoRecievedEventHandler> handlers = axHV.Tag as List<VideoRecievedEventHandler>;
                if (handlers != null && handlers.Count > 0)
                {
                    foreach (VideoRecievedEventHandler handler in handlers)
                    {
                        byte[] data = new byte[iSize];
                        Array.Copy(bData, data, bData.Length);
                        handler(sender, 1, data);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ///完成一次接收后调用下面两个函数关闭共享内存(也就是不用内存映射的时候要关闭)
                UnmapViewOfFile(itData);
                CloseHandle(irMapFile);
            }
        }
        #endregion
    }
}
