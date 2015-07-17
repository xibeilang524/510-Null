using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.PlateRecognition;
using Ralid.GeneralLibrary;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Result;
//using Ralid.Park.SnapShotCapture;
using Ralid.Park.VideoCapture;

namespace Ralid.Park.UI
{
    public partial class FrmCarPlateOfXinLuTong : Form, IPlateRecognition, IVideoCapture//, IReportHandler, ISnapShotCapture
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
        private static FrmCarPlateOfXinLuTong _Instance;
        /// <summary>
        /// 获取单个实例
        /// </summary>
        /// <returns></returns>
        public static FrmCarPlateOfXinLuTong GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new FrmCarPlateOfXinLuTong();
            }
            return _Instance;
        }
        #endregion

        #region 构造函数
        public FrmCarPlateOfXinLuTong()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private List<CarPlateDevice> _Devices = new List<CarPlateDevice>();
        private List<AxHVActiveX2Lib.AxHVActiveX2> _HVS = new List<AxHVActiveX2Lib.AxHVActiveX2>();
        private CarPlateDevice _ActiveDevice;
        private bool _Inited = false;//窗体是否已进行初始化了
        #endregion

        #region 私有方法
        private void ConnectAllDevices()
        {
            while (true)
            {
                foreach (AxHVActiveX2Lib.AxHVActiveX2 axHV in _HVS)
                {
                    DeviceState ds = axHV.Tag as DeviceState;
                    if (axHV.GetStatus() != 0)
                    {
                        axHV.ConnectTo(ds.IP);
                    }
                    ds.State = axHV.GetStatus();
                }
                Action action = delegate()
                {
                    ShowItemsOnGrid(_Devices);
                };
                if (this.InvokeRequired)
                {
                    this.Invoke(action);
                }
                else
                {
                    action();
                }
                Thread.Sleep(30 * 1000);
            }
        }

        private void ShowItemsOnGrid(List<CarPlateDevice> devices)
        {
            dataGridView1.Rows.Clear();
            foreach (CarPlateDevice device in devices)
            {
                int row = dataGridView1.Rows.Add();
                ShowItemOnRow(device, dataGridView1.Rows[row]);
            }
        }

        private void ShowItemOnRow(CarPlateDevice device, DataGridViewRow row)
        {
            row.Tag = device;
            row.Cells["colEntranceName"].Value = device.EntranceName;
            row.Cells["colIP"].Value = device.IP;
            row.Cells["colVideoID"].Value = device.VideoID;
            row.Cells["colCarPlate"].Value = device.CarPlate;
            row.Cells["colEventDate"].Value = device.EventDateTime;
            AxHVActiveX2Lib.AxHVActiveX2 ax = _HVS.SingleOrDefault(item => (item.Tag as DeviceState).IP == device.IP);
            if (ax != null) row.Cells["colState"].Value = (ax.Tag as DeviceState).State == 0 ? Resources.Resource1.Connected : Resources.Resource1.Unconnected;
        }

        /// <summary>
        /// 抓拍图片
        /// </summary>
        /// <param name="device">要抓拍的摄像机</param>
        /// <param name="force">是否强制重新抓拍，不管之前有没有抓拍到图片</param>
        /// <returns></returns>
        private bool SnapShotTo(CarPlateDevice device, bool force)
        {
            if (device != null)
            {
                if (!force && !string.IsNullOrEmpty(device.SnapPath))
                {
                    //如果不强制重新抓拍，之前已经有抓拍图片了，就不再需要抓拍图片了
                    return true;
                }

                AxHVActiveX2Lib.AxHVActiveX2 axHV = _HVS.SingleOrDefault(item => (item.Tag as DeviceState).IP == device.IP);
                if (axHV != null)
                {
                    device.ResetResult();
                    axHV.ForceSendEx(device.VideoID);

                    int wait = 0;
                    //最多等待1秒来等待抓拍图片
                    while (wait < 1000)
                    {
                        Thread.Sleep(100);
                        if (!string.IsNullOrEmpty(device.SnapPath))
                        {
                            return true;
                        }
                        wait += 100;
                    }
                }

            }

            return false;
        }
        #endregion

        #region 实现 IPlateRecognition接口
        /// <summary>
        /// 识别某个通道的车牌号
        /// </summary>
        /// <param name="parkID"></param>
        /// <param name="entranceID"></param>
        /// <returns></returns>
        public PlateRecognitionResult Recognize(int parkID, int entranceID)
        {
            PlateRecognitionResult ret = new PlateRecognitionResult();
            try
            {
                CarPlateDevice device = _Devices.SingleOrDefault(item => item.EntranceID == entranceID);
                if (device != null && device.EventDateTime != null)
                {
                    ret.CarPlate = device.CarPlate;
                    ret.Color = device.PlateColor;
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return ret;
        }

        /// <summary>
        /// 通过图片文件识别车牌号
        /// </summary>
        public PlateRecognitionResult Recognize(string path)
        {
            return new PlateRecognitionResult();
        }
        #endregion

        #region ISnapShot接口实现
        /// <summary>
        /// 对通道进行图片抓拍,要求此通道有一个用于车牌识别的摄像机
        /// </summary>
        /// <param name="parkID"></param>
        /// <param name="entranceID"></param>
        /// <returns>抓拍图片的地址，返回空时表示抓拍失败</returns>
        public string SnapShot(int parkID, int entranceID)
        {
            try
            {
                CarPlateDevice device = _Devices.SingleOrDefault(item => item.EntranceID == entranceID);
                if (device != null)
                {
                    AxHVActiveX2Lib.AxHVActiveX2 axHV = _HVS.SingleOrDefault(item => (item.Tag as DeviceState).IP == device.IP);
                    if (axHV != null)
                    {
                        device.ResetResult();
                        axHV.ForceSendEx(device.VideoID);

                        int wait = 0;
                        //最多等待1秒来等待抓拍图片
                        while (wait < 1000)
                        {
                            Thread.Sleep(100);
                            if (!string.IsNullOrEmpty(device.SnapPath))
                            {
                                return device.SnapPath;
                            }
                            wait += 100;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return string.Empty;
        }
        #endregion

        #region IVideoCapture接口实现
        /// <summary>
        /// 抓拍图片
        /// </summary>
        /// <param name="info">摄像机</param>
        /// <param name="force">是否强制重新抓拍，不管之前有没有抓拍到图片</param>
        /// <returns>抓拍图片的地址，返回空时表示抓拍失败</returns>
        public string CapturePicture(VideoSourceInfo info, bool force)
        {
            try
            {
                //获取该通道车牌识别一体机
                CarPlateDevice device = _Devices.FirstOrDefault(item => item.EntranceID == info.EntranceID);
                if (SnapShotTo(device, force))
                {
                    return device.SnapPath;
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return string.Empty;
        }

        /// <summary>
        /// 清除抓拍图片信息
        /// </summary>
        /// <param name="info">摄像机</param>
        public void ClearCapture(VideoSourceInfo info)
        {
            try
            {
                CarPlateDevice device = _Devices.FirstOrDefault(item => item.EntranceID == info.EntranceID);
                if (device != null)
                {
                    lock (device)
                    {
                        device.ResetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        #region 公共方法
        public void Init()
        {
            if (_Inited) return;//已经初始化的，返回
            _Inited = true;

            foreach (ParkInfo park in ParkBuffer.Current.Parks)
            {
                if (park.IsRootPark && park.HostWorkstation == WorkStationInfo.CurrentStation.StationID)
                {
                    List<EntranceInfo> entrances = park.GetEntrances(true);
                    foreach (EntranceInfo entrance in entrances)
                    {
                        if (!string.IsNullOrEmpty(entrance.CarPlateIP)
                            && entrance.CarPlateIP != "0.0.0.0")
                        {
                            if (!_HVS.Exists(item => (item.Tag as DeviceState).IP == entrance.CarPlateIP))
                            {
                                AxHVActiveX2Lib.AxHVActiveX2 axHV = new AxHVActiveX2Lib.AxHVActiveX2();
                                ((System.ComponentModel.ISupportInitialize)(axHV)).BeginInit();
                                this.Controls.Add(axHV);
                                axHV.Visible = false;
                                ((System.ComponentModel.ISupportInitialize)(axHV)).EndInit();
                                axHV.RecvSnapImageFlag = 1;
                                axHV.RecvPlateImageFlag = 1;
                                axHV.RecvPlateBinImageFlag = 0;
                                axHV.RecvVideoFlag = 0;
                                axHV.AutoSaveFlag = false;
                                axHV.OnReceivePlate -= new EventHandler(axHV_OnReceivePlate);
                                axHV.OnReceivePlate += new EventHandler(axHV_OnReceivePlate);
                                axHV.OnReceiveVideo -= new EventHandler(axHV_OnReceiveVideo);
                                axHV.OnReceiveVideo += new EventHandler(axHV_OnReceiveVideo);
                                axHV.OnReceiveVideo -= new EventHandler(axHV_OnReceiveVideo1);
                                axHV.OnReceiveVideo += new EventHandler(axHV_OnReceiveVideo1);
                                axHV.Tag = new DeviceState() { IP = entrance.CarPlateIP, State = -1 };
                                _HVS.Add(axHV);
                            }
                            CarPlateDevice device = new CarPlateDevice()
                            {
                                IP = entrance.CarPlateIP,
                                VideoID = entrance.VideoID != null ? entrance.VideoID.Value : 0,
                                EntranceID = entrance.EntranceID,
                                EntranceName = entrance.EntranceName
                            };
                            _Devices.Add(device);
                        }
                    }
                    ShowItemsOnGrid(_Devices);
                    if (_Devices.Count > 0)
                    {
                        _ActiveDevice = _Devices[0];
                    }
                }
                if (this.dataGridView1.Rows.Count > 0)
                {
                    dataGridView1_CellMouseDown(this.dataGridView1, new DataGridViewCellMouseEventArgs(0, 0, 0, 0, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)));
                }
            }
            Thread t = new Thread(ConnectAllDevices);
            t.IsBackground = true;
            t.Start();
        }
        #endregion

        #region IReportHandler 成员
        public void ProcessReport(ReportBase r)
        {
            if (r is CarSenseReport)
            {
                CarSenseReport cp = r as CarSenseReport;
                if (cp.InOrOutFlag == 0)//车走时清空识别结果
                {
                    foreach (CarPlateDevice device in _Devices)
                    {
                        if (device.EntranceID == r.EntranceID)
                        {
                            lock (device)
                            {
                                device.ResetResult();
                            }
                        }
                    }
                }
            }

            #region 以下操作由FrmSnapShoter完成
            //ParkInfo park = ParkBuffer.Current.GetPark(r.ParkID);
            //if (park != null && park.RootParkID > 0) park = ParkBuffer.Current.GetPark(park.RootParkID);
            //if (park == null) return;

            //foreach (DataGridViewRow row in this.dataGridView1.Rows)
            //{
            //    CarPlateDevice device = row.Tag as CarPlateDevice;
            //    if (device.EntranceID == r.EntranceID)
            //    {
            //        if (r is CardEventReport)
            //        {
            //            CardEventReport cardEvent = r as CardEventReport;
            //            if (cardEvent.EventStatus == CardEventStatus.Valid && !string.IsNullOrEmpty(device.SnapPath))
            //            {
            //                EntranceInfo entrace = ParkBuffer.Current.GetEntrance(r.EntranceID);
            //                int videoSourceID = -1;
            //                if (entrace != null)
            //                {
            //                    VideoSourceInfo videoSource = entrace.VideoSources.FirstOrDefault(item => item.MediaSource == device.IP && item.Channel == device.VideoID);
            //                    if (videoSource != null) videoSourceID = videoSource.VideoID;
            //                }
            //                if (videoSourceID == -1) videoSourceID = r.EntranceID * 1000 + device.VideoID;//没有找到视频ID的，手动生成一个，通道id*1000+视频路数
            //                SnapShot shot = new SnapShot(cardEvent.EventDateTime, videoSourceID, cardEvent.CardID, device.SnapPath);
            //                if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.ImageDBConnStr))
            //                {
            //                    CommandResult result = (new SnapShotBll(AppSettings.CurrentSetting.ImageDBConnStr)).Insert(shot);
            //                    string standby = AppSettings.CurrentSetting.CurrentStandbyConnect;
            //                    if (result.Result != ResultCode.Successful && !string.IsNullOrEmpty(standby)) (new SnapShotBll(standby)).Insert(shot);
            //                }
            //                else
            //                {
            //                    string master = AppSettings.CurrentSetting.CurrentMasterConnect;
            //                    string standby = AppSettings.CurrentSetting.CurrentStandbyConnect;
            //                    CommandResult result = (new SnapShotBll(master)).Insert(shot);
            //                    if (result.Result != ResultCode.Successful && !string.IsNullOrEmpty(standby)) (new SnapShotBll(standby)).Insert(shot);
            //                }
            //                //(new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).Insert(shot);
            //            }
            //        }
            //    }
            //}
            #endregion
        }
        #endregion

        #region 事件处理程序
        private void axHV_OnReceivePlate(object sender, EventArgs e)
        {
            try
            {
                AxHVActiveX2Lib.AxHVActiveX2 axHV = sender as AxHVActiveX2Lib.AxHVActiveX2;
                int videoID = axHV.GetVideoID();
                DeviceState info = axHV.Tag as DeviceState;
                info.State = 0;
                foreach (CarPlateDevice device in _Devices)
                {
                    if (device.IP == info.IP && device.VideoID == videoID)
                    {
                        device.ResetResult();
                        device.CarPlate = axHV.GetPlate();
                        device.EventDateTime = DateTime.Now;
                        device.PlateColor = axHV.GetPlateColor();
                        string dir = TempFolderManager.GetCurrentFolder();
                        string path;
                        path = Path.Combine(dir, Guid.NewGuid().ToString() + ".jpg");
                        if (axHV.SaveSnapImage(path) == 0)
                        {
                            device.SnapPath = path;
                        }
                        else
                        {
                            device.SnapPath = string.Empty;
                        }

                        path = Path.Combine(dir, Guid.NewGuid().ToString() + ".jpg");
                        if (axHV.SavePlateImage(path) == 0)
                        {
                            device.PlatePath = path;
                        }
                        else
                        {
                            device.PlatePath = string.Empty;
                        }

                        Action action = delegate()
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                CarPlateDevice d = row.Tag as CarPlateDevice;
                                if (d.IP == device.IP && d.VideoID == device.VideoID)
                                {
                                    ShowItemOnRow(device, row);
                                }
                            }
                            if (_ActiveDevice != null && _ActiveDevice.IP == (axHV.Tag as DeviceState).IP && videoID == _ActiveDevice.VideoID)
                            {
                                this.picPlate.Image = Properties.Resources.NoImage;
                                if (!string.IsNullOrEmpty(device.PlatePath)) this.picPlate.Image = Image.FromFile(device.PlatePath);
                            }
                        };
                        if (this.InvokeRequired)
                        {
                            this.Invoke(action);
                        }
                        else
                        {
                            action();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void axHV_OnReceiveVideo(object sender, EventArgs e)
        {
            try
            {
                if (_ActiveDevice.VideoID == 0)
                {
                    AxHVActiveX2Lib.AxHVActiveX2 axHV = sender as AxHVActiveX2Lib.AxHVActiveX2;
                    if (_ActiveDevice != null && _ActiveDevice.IP == (axHV.Tag as DeviceState).IP)
                    {
                        string strName = "";
                        int iSize = 0;
                        strName = axHV.GetVideoFrameSM(0, ref iSize);
                        if (iSize == 0) return;

                        IntPtr irMapFile = IntPtr.Zero;
                        irMapFile = OpenFileMapping(FILE_MAP_READ, false, strName);
                        if (irMapFile == IntPtr.Zero) return;

                        IntPtr itData = IntPtr.Zero;
                        itData = MapViewOfFile(irMapFile, FILE_MAP_READ, 0, 0, 0);
                        if (itData == IntPtr.Zero)
                        {
                            CloseHandle(irMapFile);
                            return;
                        }

                        byte[] bData = new byte[iSize];
                        Marshal.Copy(itData, bData, 0, iSize);

                        ///完成一次接收后调用下面两个函数关闭共享内存(也就是不用内存映射的时候要关闭)
                        UnmapViewOfFile(itData);
                        CloseHandle(irMapFile);

                        using (MemoryStream stream = new MemoryStream(bData))
                        {
                            video.Image = Image.FromStream(stream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            finally
            {
            }
        }

        private void axHV_OnReceiveVideo1(object sender, EventArgs e)
        {
            try
            {
                if (_ActiveDevice.VideoID == 1)
                {
                    AxHVActiveX2Lib.AxHVActiveX2 axHV = sender as AxHVActiveX2Lib.AxHVActiveX2;
                    if (_ActiveDevice != null && _ActiveDevice.IP == (axHV.Tag as DeviceState).IP)
                    {
                        string strName = "";
                        int iSize = 0;
                        strName = axHV.GetVideoFrameSM(1, ref iSize);
                        if (iSize == 0) return;

                        IntPtr irMapFile = IntPtr.Zero;
                        irMapFile = OpenFileMapping(FILE_MAP_READ, false, strName);
                        if (irMapFile == IntPtr.Zero) return;

                        IntPtr itData = IntPtr.Zero;
                        itData = MapViewOfFile(irMapFile, FILE_MAP_READ, 0, 0, 0);
                        if (itData == IntPtr.Zero)
                        {
                            CloseHandle(irMapFile);
                            return;
                        }

                        byte[] bData = new byte[iSize];
                        Marshal.Copy(itData, bData, 0, iSize);

                        ///完成一次接收后调用下面两个函数关闭共享内存(也就是不用内存映射的时候要关闭)
                        UnmapViewOfFile(itData);
                        CloseHandle(irMapFile);

                        using (MemoryStream stream = new MemoryStream(bData))
                        {
                            video.Image = Image.FromStream(stream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            finally
            {
            }
        }

        private void FrmCarPlateOfXinLuTong_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void btnForceSnap_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows != null && this.dataGridView1.SelectedRows.Count == 1)
            {
                CarPlateDevice device = this.dataGridView1.SelectedRows[0].Tag as CarPlateDevice;
                AxHVActiveX2Lib.AxHVActiveX2 axHV = _HVS.SingleOrDefault(item => (item.Tag as DeviceState).IP == device.IP);
                if (axHV != null)
                {
                    device.ResetResult();
                    axHV.ForceSendEx(device.VideoID);
                }
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CarPlateDevice device = dataGridView1.Rows[e.RowIndex].Tag as CarPlateDevice;
                if (device != null)
                {
                    foreach (AxHVActiveX2Lib.AxHVActiveX2 axHV in _HVS)
                    {
                        if ((axHV.Tag as DeviceState).IP == device.IP)
                        {
                            axHV.RecvVideoFlag = chkVideo.Checked ? 1 : 0;
                        }
                        else
                        {
                            axHV.RecvVideoFlag = 0;
                        }
                    }
                    _ActiveDevice = device;
                }
            }
        }
        #endregion

        private void chkVideo_CheckedChanged(object sender, EventArgs e)
        {
            foreach (AxHVActiveX2Lib.AxHVActiveX2 axHV in _HVS)
            {
                if (_ActiveDevice != null && (axHV.Tag as DeviceState).IP == _ActiveDevice.IP)
                {
                    axHV.RecvVideoFlag = chkVideo.Checked ? 1 : 0;
                }
                else
                {
                    axHV.RecvVideoFlag = 0;
                }
            }
        }
    }

    /// <summary>
    /// 表示一个通道的车牌识别设备
    /// </summary>
    internal class CarPlateDevice
    {
        #region 构造函数
        public CarPlateDevice() { }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置设备的IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 获取或设置通道的视频,（有些设备支持双路识别，一个通道可以用其中的一路视频进行识别,如果是双路识别器，第一路为0，第二路为1)
        /// </summary>
        public int VideoID { get; set; }
        /// <summary>
        /// 获取或设置通道ID
        /// </summary>
        public int EntranceID { get; set; }
        /// <summary>
        /// 获取或设置通道名称
        /// </summary>
        public string EntranceName { get; set; }
        /// <summary>
        /// 获取或设置最近识别到的车牌号
        /// </summary>
        public string CarPlate { get; set; }
        /// <summary>
        /// 获取或设置车牌颜色
        /// </summary>
        public string PlateColor { get; set; }
        /// <summary>
        /// 获取或设置最近识别到的车牌图片路径
        /// </summary>
        public string SnapPath { get; set; }
        /// <summary>
        /// 获取或设置车牌图片路径
        /// </summary>
        public string PlatePath { get; set; }
        /// <summary>
        /// 获取或设置上传时间
        /// </summary>
        public DateTime? EventDateTime { get; set; }

        #region 以下是大华摄像机会用到的
        /// <summary>
        /// 获取或设置视频源信息
        /// </summary>
        public VideoSourceInfo VideoSource { get; set; }
        /// <summary>
        /// 获取或设置登陆返回的句柄
        /// </summary>
        public int m_nLoginID { get; set; }
        /// <summary>
        /// 获取或设置设备消息事件订阅句柄
        /// </summary>
        public int m_nRealLoadPic { get; set; }
        /// <summary>
        /// 获取或设置启动实时监控返回的句柄
        /// </summary>
        public int m_realPlayH { get; set; }
        /// <summary>
        /// 获取或设置设备的连接状态 0表示未连接，1表示已连接，2表示断线，其它的表示未连接
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 获取或设置车身颜色
        /// </summary>
        public string CarColor { get; set; }
        /// <summary>
        /// 获取或设置车道号
        /// </summary>
        public int Lane { get; set; }
        /// <summary>
        /// 设置抓拍的时间
        /// </summary>
        public DateTime? DeviceSnapTime { get; set; }
        #endregion
        #endregion

        #region 公共方法
        /// <summary>
        /// 清空上传结果
        /// </summary>
        public void ResetResult()
        {
            CarPlate = string.Empty;
            PlateColor = string.Empty;
            SnapPath = string.Empty;
            PlatePath = string.Empty;
            EventDateTime = null;

            CarColor = string.Empty;
            Lane = 0;
            DeviceSnapTime = null;
        }
        #endregion
    }

    internal class DeviceState
    {
        /// <summary>
        /// 获取或设置设备的IP
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 获取或设置设备的连接状态 0表示已连接，其它的表示未连接
        /// </summary>
        public int State { get; set; }
    }
}
