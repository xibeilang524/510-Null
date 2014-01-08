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

namespace Ralid.Park.UI
{
    public partial class FrmCarPlateOfXinLuTong : Form, IPlateRecognition, IReportHandler
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

        #region 私有方法
        private List<CarPlateDevice> _Devices = new List<CarPlateDevice>();
        private List<AxHVActiveX2Lib.AxHVActiveX2> _HVS = new List<AxHVActiveX2Lib.AxHVActiveX2>();
        private CarPlateDevice _ActiveDevice;
        #endregion

        #region 私有方法
        private void ConnectAllDevices()
        {
            while (true)
            {
                foreach (AxHVActiveX2Lib.AxHVActiveX2 axHV in _HVS)
                {
                    DeviceState ds = axHV.Tag as DeviceState;
                    if (ds.State != 0)
                    {
                        axHV.ConnectTo(ds.IP);
                        Thread.Sleep(5000);
                        ds.State = axHV.GetStatus();
                    }
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
                Thread.Sleep(60 * 1000);
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
            if (ax != null) row.Cells["colState"].Value = (ax.Tag as DeviceState).State == 0 ? "已连接" : "未连接";
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
            string dir = TempFolderManager.GetCurrentFolder();
            PlateRecognitionResult ret = new PlateRecognitionResult();
            try
            {
                CarPlateDevice device = _Devices.SingleOrDefault(item => item.EntranceID == entranceID);
                if (device != null && device.EventDateTime != null)
                {
                    ret.CarPlate = device.CarPlate;
                    ret.Color = device.PlateColor;
                    device.ResetResult();
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
            return null;
        }
        #endregion

        #region 公共方法
        public void Init()
        {
            foreach (ParkInfo park in ParkBuffer.Current.Parks)
            {
                if (park.IsRootPark && park.HostWorkstation == WorkStationInfo.CurrentStation.StationID)
                {
                    List<EntranceInfo> entrances = park.GetEntrances(true);
                    foreach (EntranceInfo entrance in entrances)
                    {
                        if (!string.IsNullOrEmpty(entrance.CarPlateIP))
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
                                axHV.OnReceivePlate += new EventHandler(axHV_OnReceivePlate);
                                axHV.OnReceiveVideo += new EventHandler(axHV_OnReceiveVideo);
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
            ParkInfo park = ParkBuffer.Current.GetPark(r.ParkID);
            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                CarPlateDevice device = row.Tag as CarPlateDevice;
                if (device.EntranceID == r.EntranceID)
                {
                    if (r is CardEventReport)
                    {
                        CardEventReport cardEvent = r as CardEventReport;
                        if (cardEvent.EventStatus == CardEventStatus.Valid && !string.IsNullOrEmpty(device.SnapPath))
                        {
                            SnapShot shot = new SnapShot(cardEvent.EventDateTime, r.EntranceID, device.SnapPath, cardEvent.CardID);
                            (new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).Insert(shot);
                        }
                    }
                }
            }
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
            string strName = "";
            int iSize = 1024 * 1024;
            IntPtr irMapFile = IntPtr.Zero;
            IntPtr itData = IntPtr.Zero;
            try
            {
                AxHVActiveX2Lib.AxHVActiveX2 axHV = sender as AxHVActiveX2Lib.AxHVActiveX2;
                DeviceState info = axHV.Tag as DeviceState;
                info.State = 0;
                if (_ActiveDevice != null && _ActiveDevice.IP == (axHV.Tag as DeviceState).IP)
                {
                    strName = axHV.GetVideoFrameSM(_ActiveDevice.VideoID, ref iSize);
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
                    MemoryStream stream = new MemoryStream(bData);
                    video.Image = Image.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            finally
            {
                ///完成一次接收后调用下面两个函数关闭共享内存(也就是不用内存映射的时候要关闭)
                UnmapViewOfFile(itData);
                CloseHandle(irMapFile);
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
