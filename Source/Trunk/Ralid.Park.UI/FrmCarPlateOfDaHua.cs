using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.PlateRecognition;
using Ralid.GeneralLibrary;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Result;
//using Ralid.Park.SnapShotCapture;
using Ralid.Park.UserControls.VideoPanels;
using MyNet.CCTV.Control.Implement.DaHua;
using Ralid.Park.VideoCapture;

namespace Ralid.Park.UI
{
    public partial class FrmCarPlateOfDaHua : Form, IPlateRecognition, IVideoCapture//, IReportHandler, ISnapShotCapture
    {

        #region 静态属性
        private static FrmCarPlateOfDaHua _Instance;
        /// <summary>
        /// 获取单个实例
        /// </summary>
        /// <returns></returns>
        public static FrmCarPlateOfDaHua GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new FrmCarPlateOfDaHua();
            }
            return _Instance;
        }
        #endregion

        #region 构造函数
        public FrmCarPlateOfDaHua()
        {
            InitializeComponent();

            disConnect = new fDisConnect(DisConnectEvent);
            onlineMsg = new fHaveReConnect(OnlineEvent);
            anaCallback = new fAnalyzerDataCallBack(AnalyzerDataCallBackEvent);
        }
        #endregion

        #region 私有变量
        private List<CarPlateDevice> _Devices = new List<CarPlateDevice>();
        private CarPlateDevice _ActiveDevice;
        private fDisConnect disConnect;       //设备离线消息
        private fHaveReConnect onlineMsg;     //设备重新在线消息
        private fAnalyzerDataCallBack anaCallback; //设备过车事件及抓拍等消息
        private bool initialized; //设备SDK是否已初始化
        private Thread _ConnectThread;//设备连接线程
        private bool _Inited = false;//窗体是否已进行初始化了
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
            CarPlateDevice device = _Devices.SingleOrDefault(d => d.m_nLoginID == lLoginID);
            if (device != null)
            {
                //设备离线消息；设备非正常关机，SDK可以检测到；需要取消订阅,当重新在线消息时，再发起订阅事件
                if (device.m_nRealLoadPic != 0)
                {
                    DHClient.DHStopLoadPic(device.m_nRealLoadPic);
                    device.m_nRealLoadPic = 0;
                }
                device.State = 2;
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
            CarPlateDevice device = _Devices.SingleOrDefault(d => d.m_nLoginID == lLoginID);
            if (device != null)
            {
                //自动重连成功事件后，发起订阅设备事件消息

                device.m_nRealLoadPic = DHClient.DHRealLoadPicture(device.m_nLoginID, device.VideoID, EventIvs.EVENT_IVS_ALL, anaCallback, 0);

                device.State = 1;
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
        private int AnalyzerDataCallBackEvent(Int32 lAnalyzerHandle, UInt32 dwAlarmType, IntPtr pAlarmInfo, IntPtr pBuffer, UInt32 dwBufSize, UInt32 dwUser, Int32 nSequence, IntPtr reserved)
        {
            if (dwBufSize == 0)
            {
                return 1;
            }

            try
            {
                CarPlateDevice device = _Devices.SingleOrDefault(d => d.m_nRealLoadPic == lAnalyzerHandle);
                if (device != null)
                {
                    // 记录文件
                    byte[] buf = new byte[dwBufSize];
                    Marshal.Copy(pBuffer, buf, 0, (int)dwBufSize);

                    DH_MSG_OBJECT plateObj = new DH_MSG_OBJECT();
                    DH_MSG_OBJECT VehicleObj = new DH_MSG_OBJECT();
                    NET_TIME_EX utc = new NET_TIME_EX();
                    int lane = 0;
                    string strMsg;

                    bool bret = DaHuaSDKManager.GetInstance().GetStuObject(dwAlarmType, pAlarmInfo, out plateObj, out VehicleObj, out utc, out lane, out strMsg);

                    device.ResetResult();
                    if (plateObj.szText != null)
                    {
                        device.CarPlate = Encoding.GetEncoding("gb2312").GetString(plateObj.szText);
                    }
                    if (!string.IsNullOrEmpty(device.CarPlate)) device.CarPlate = device.CarPlate.TrimEnd('\0');//去除结束符\0
                    device.EventDateTime = DateTime.Now;
                    if (plateObj.bColor == 1)
                    {
                        device.PlateColor = DaHuaSDKManager.GetInstance().GetColorString(plateObj.rgbaMainColor);
                    }
                    if (VehicleObj.bColor == 1)
                    {
                        device.CarColor = DaHuaSDKManager.GetInstance().GetColorString(VehicleObj.rgbaMainColor);
                    }
                    device.Lane = lane;
                    try
                    {
                        device.DeviceSnapTime = new DateTime((int)utc.dwYear, (int)utc.dwMonth, (int)utc.dwDay, (int)utc.dwHour, (int)utc.dwMinute, (int)utc.dwSecond, (int)utc.dwMillisecond);
                    }
                    catch { }
                    device.SnapPath = DaHuaSDKManager.GetInstance().SaveSnapImage(buf);
                    device.PlatePath = DaHuaSDKManager.GetInstance().SavePlateJpg(plateObj, pBuffer, buf);

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        CarPlateDevice d = row.Tag as CarPlateDevice;
                        if (d != null)
                        {
                            if (d.m_nRealLoadPic == device.m_nRealLoadPic)
                            {
                                ShowItemOnRow(device, row);
                            }
                        }
                    }
                    if (_ActiveDevice != null && _ActiveDevice.m_nRealLoadPic == device.m_nRealLoadPic)
                    {
                        ShowCarPlateInfo(device);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            
            return 1;
        }
        #endregion


        #region 私有方法
        private void ConnectAllDevices()
        {
            while (true)
            {
                foreach (CarPlateDevice device in _Devices)
                {
                    //登入设备
                    if (device.m_nLoginID == 0 && device.VideoSource != null)
                    {
                        NET_DEVICEINFO deviceInfo = new NET_DEVICEINFO();
                        int error = 0;
                        device.m_nLoginID = DHClient.DHLogin(device.IP, (ushort)device.VideoSource.StreamPort
                                                        , device.VideoSource.UserName, device.VideoSource.Password, out deviceInfo, out error);
                        if (device.m_nLoginID != 0)
                        {
                            //订阅事件
                            device.m_nRealLoadPic = DHClient.DHRealLoadPicture(device.m_nLoginID, device.VideoID, EventIvs.EVENT_IVS_ALL, anaCallback, 0);
                            device.State = 1;
                        }
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
                Thread.Sleep(30 * 1000);
            }
        }

        private void ShowItemsOnGrid(List<CarPlateDevice> devices)
        {
            dataGridView1.Rows.Clear();
            foreach (CarPlateDevice device in devices)
            {
                //只显示用于车牌识别的摄像机
                if (device.VideoSource != null && device.VideoSource.IsForCarPlate)
                {
                    int row = dataGridView1.Rows.Add();
                    ShowItemOnRow(device, dataGridView1.Rows[row]);
                }
            }
        }

        private void ShowItemOnRow(CarPlateDevice device, DataGridViewRow row)
        {
            Action action = delegate()
            {
                row.Tag = device;
                row.Cells["colEntranceName"].Value = device.EntranceName;
                row.Cells["colIP"].Value = device.IP;
                row.Cells["colVideoID"].Value = device.VideoID;
                row.Cells["colCarPlate"].Value = device.CarPlate;
                row.Cells["colEventDate"].Value = device.EventDateTime;
                if (device.State == 1)
                {
                    row.Cells["colState"].Value = Resources.Resource1.Connected;
                }
                else if (device.State == 1)
                {
                    row.Cells["colState"].Value = Resources.Resource1.Disconnect;
                }
                else
                {
                    row.Cells["colState"].Value = Resources.Resource1.Unconnected;
                }
            };
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        private void ShowCarPlateInfo(CarPlateDevice device)
        {
            Action action = delegate()
            {
                this.pictureBoxCar.Image = null;
                this.pictureBoxSnapshot.Image = null;
                if (!string.IsNullOrEmpty(device.PlatePath)) this.pictureBoxCar.Image = Image.FromFile(device.PlatePath);
                if (!string.IsNullOrEmpty(device.SnapPath)) this.pictureBoxSnapshot.Image = Image.FromFile(device.SnapPath);
                this.textBoxPlate.Text = device.CarPlate;
                this.textBoxPlateColor.Text = device.PlateColor;
                this.textBoxAutoColor.Text = device.CarColor;
                this.textBoxLane.Text = device.Lane.ToString();
                this.textBoxTime.Text = device.DeviceSnapTime.HasValue ? device.DeviceSnapTime.Value.ToString() : string.Empty;
            };
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        private void ClearCarPlateInfo()
        {
            Action action = delegate()
            {
                this.pictureBoxCar.Image = null;
                this.pictureBoxSnapshot.Image = null;
                this.textBoxPlate.Text = string.Empty;
                this.textBoxPlateColor.Text = string.Empty;
                this.textBoxAutoColor.Text = string.Empty;
                this.textBoxLane.Text = string.Empty;
                this.textBoxTime.Text = string.Empty;
            };
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        private void RealPlay(CarPlateDevice device)
        {
            if (device != null && device.m_nLoginID != 0)
            {
                //启动监视
                device.m_realPlayH = DHClient.DHRealPlay(device.m_nLoginID, device.VideoID, this.pictureBoxImg.Handle);
            }
        }
        private void StopRealPlay(CarPlateDevice device)
        {
            if (device != null && device.m_nLoginID != 0 && device.m_realPlayH != 0)
            {
                //停止监视
                DHClient.DHStopRealPlay(device.m_realPlayH);
                device.m_realPlayH = 0;
                this.pictureBoxImg.Image = null;
            }
        }
        /// <summary>
        /// 触发手动抓拍
        /// </summary>
        /// <param name="device"></param>
        private void ManualSnap(CarPlateDevice device)
        {
            device.ResetResult();
            //触发手动抓拍测试
            MANUAL_SNAP_PARAMETER snap = new MANUAL_SNAP_PARAMETER();
            snap.nChannel = device.VideoID;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(snap));
            Marshal.StructureToPtr(snap, ptr, false);
            bool bRet = DHClient.DHControlDevice(device.m_nLoginID, CtrlType.DH_MANUAL_SNAP, ptr, 1000);
            System.Runtime.InteropServices.Marshal.FreeHGlobal(ptr);
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
                
                ManualSnap(device);

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
                CarPlateDevice device = _Devices.FirstOrDefault(item => item.EntranceID == entranceID && item.VideoSource != null && item.VideoSource.IsForCarPlate);
                if (device != null)
                {
                    if (device.EventDateTime == null)
                    {
                        //如果没有上传时间，重新进行抓拍
                        SnapShotTo(device, true);
                    }

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
                CarPlateDevice device = _Devices.FirstOrDefault(item => item.EntranceID == entranceID && item.VideoSource != null && item.VideoSource.IsForCarPlate);
                if (SnapShotTo(device, false))
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
                CarPlateDevice device = _Devices.FirstOrDefault(item => item.IP == info.MediaSource);
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
                CarPlateDevice device = _Devices.FirstOrDefault(item => item.IP == info.MediaSource);
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

            _Devices.Clear();
            foreach (ParkInfo park in ParkBuffer.Current.Parks)
            {
                if (park.IsRootPark && park.HostWorkstation == WorkStationInfo.CurrentStation.StationID)
                {
                    List<EntranceInfo> entrances = park.GetEntrances(true);
                    foreach (EntranceInfo entrance in entrances)
                    {
                        foreach (VideoSourceInfo video in entrance.VideoSources)
                        {
                            if (video.VideoSourceType == (int)VideoServerType.DaHua)
                            {
                                CarPlateDevice device = new CarPlateDevice()
                                {
                                    IP = video.MediaSource,
                                    VideoID = video.Channel,
                                    EntranceID = video.EntranceID,
                                    EntranceName = entrance.EntranceName,
                                    VideoSource = video
                                };
                                _Devices.Add(device);
                            }
                        }
                    }
                    ShowItemsOnGrid(_Devices);
                }
            }
            //初始化SDK
            if (initialized == false)
            {
                //这里不直接使用DHClient初始化，是因为DHClient的disConnect和onlineMsg只支持一个事件回调，
                //所以这里使用管理器的事件处理，在管理器中使用DHClient初始化
                DaHuaSDKManager.GetInstance().DisConnectEventHandle -= disConnect;
                DaHuaSDKManager.GetInstance().DisConnectEventHandle += disConnect;
                DaHuaSDKManager.GetInstance().OnlineMsgEventHandle -= onlineMsg;
                DaHuaSDKManager.GetInstance().OnlineMsgEventHandle += onlineMsg;
                DaHuaSDKManager.GetInstance().InitSDK();
                initialized = true;
            }
            if (_ConnectThread == null)
            {
                _ConnectThread = new Thread(ConnectAllDevices);
                _ConnectThread.IsBackground = true;
                _ConnectThread.Start();
            }
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
                        if (device.EntranceID == r.EntranceID && device.VideoSource != null)
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
            //if (park.HostWorkstation == WorkStationInfo.CurrentStation.StationID) //如果本机是停车场的通讯主机,则它要负责抓拍图片
            //{
            //foreach (CarPlateDevice device in _Devices)
            //{
            //    if (device.EntranceID == r.EntranceID && device.VideoSource != null)
            //    {
            //        if (r is CardEventReport)
            //        {
            //            CardEventReport cardEvent = r as CardEventReport;
            //            if (cardEvent.EventStatus == CardEventStatus.Valid && SnapShotTo(device, false))
            //            {
            //                string snappath = device.SnapPath;
            //                //事件有效抓拍后，清空识别结果
            //                lock (device)
            //                {
            //                    device.ResetResult();
            //                }
            //                SnapShot shot = new SnapShot(cardEvent.EventDateTime, device.VideoSource.VideoID, cardEvent.CardID, snappath);
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
            //        else if (r is CarSenseReport)
            //        {
            //            CarSenseReport cp = r as CarSenseReport;
            //            if (cp.InOrOutFlag == 1)//车到时
            //            {
            //                if (UserSetting.Current.SnapshotWhenCarArrive)  //车压地感时抓拍图片
            //                {
            //                    if (SnapShotTo(device, false))
            //                    {
            //                        SnapShot shot = new SnapShot(cp.EventDateTime, device.VideoSource.VideoID, string.Empty, device.SnapPath);
            //                        if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.ImageDBConnStr))
            //                        {
            //                            CommandResult result = (new SnapShotBll(AppSettings.CurrentSetting.ImageDBConnStr)).Insert(shot);
            //                            string standby = AppSettings.CurrentSetting.CurrentStandbyConnect;
            //                            if (result.Result != ResultCode.Successful && !string.IsNullOrEmpty(standby)) (new SnapShotBll(standby)).Insert(shot);
            //                        }
            //                        else
            //                        {
            //                            string master = AppSettings.CurrentSetting.CurrentMasterConnect;
            //                            string standby = AppSettings.CurrentSetting.CurrentStandbyConnect;
            //                            CommandResult result = (new SnapShotBll(master)).Insert(shot);
            //                            if (result.Result != ResultCode.Successful && !string.IsNullOrEmpty(standby)) (new SnapShotBll(standby)).Insert(shot);
            //                        }
            //                        //(new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).Insert(shot);
            //                    }
            //                }
            //            }
            //            else//车走时清空识别结果
            //            {
            //                lock (device)
            //                {
            //                    device.ResetResult();
            //                }
            //            }
            //        }
            //        else if (r is AlarmReport)
            //        {
            //            AlarmReport ar = r as AlarmReport;
            //            if (ar.AlarmType == Ralid.Park.BusinessModel.Enum.AlarmType.Opendoor
            //                || ar.AlarmType == Ralid.Park.BusinessModel.Enum.AlarmType.GateAlarm)
            //            {
            //                if (SnapShotTo(device, true))
            //                {
            //                    string snappath = device.SnapPath;
            //                    //抓拍后，清空识别结果
            //                    lock (device)
            //                    {
            //                        device.ResetResult();
            //                    }
            //                    SnapShot shot = new SnapShot(ar.EventDateTime, device.VideoSource.VideoID, string.Empty, snappath);
            //                    if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.ImageDBConnStr))
            //                    {
            //                        CommandResult result = (new SnapShotBll(AppSettings.CurrentSetting.ImageDBConnStr)).Insert(shot);
            //                        string standby = AppSettings.CurrentSetting.CurrentStandbyConnect;
            //                        if (result.Result != ResultCode.Successful && !string.IsNullOrEmpty(standby)) (new SnapShotBll(standby)).Insert(shot);
            //                    }
            //                    else
            //                    {
            //                        string master = AppSettings.CurrentSetting.CurrentMasterConnect;
            //                        string standby = AppSettings.CurrentSetting.CurrentStandbyConnect;
            //                        CommandResult result = (new SnapShotBll(master)).Insert(shot);
            //                        if (result.Result != ResultCode.Successful && !string.IsNullOrEmpty(standby)) (new SnapShotBll(standby)).Insert(shot);
            //                    }
            //                    //(new SnapShotBll(AppSettings.CurrentSetting.ParkConnect)).Insert(shot);                                    
            //                }
            //            }
            //        }
            //    }
            //}
            //}
            #endregion
        }
        #endregion

        #region 事件处理程序
        private void FrmCarPlateOfDaHua_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //停止监控和清空上传事件显示
                if (this.btnStopRealPlay.Enabled)
                {
                    btnStopRealPlay_Click(this.btnStopRealPlay, EventArgs.Empty);
                }
                _ActiveDevice = null;
                ClearCarPlateInfo();
                
                this.Hide();
                e.Cancel = true;
            }
        }

        private void btnSnap_Click(object sender, EventArgs e)
        {
            if (_ActiveDevice != null)
            {
                ManualSnap(_ActiveDevice);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CarPlateDevice device = dataGridView1.Rows[e.RowIndex].Tag as CarPlateDevice;
                if (device != null)
                {
                    if (_ActiveDevice != device)
                    {
                        //如果停止监控按钮可以使用，表示正在监控，先停止当前监控，再切换到选择的摄像机进行监控
                        if (this.btnStopRealPlay.Enabled)
                        {
                            StopRealPlay(_ActiveDevice);
                            RealPlay(device);
                        }
                        ShowCarPlateInfo(device);

                        _ActiveDevice = device;
                    }
                }
            }
        }
        private void btnRealPlay_Click(object sender, EventArgs e)
        {
            RealPlay(_ActiveDevice);
            this.btnRealPlay.Enabled = false;
            this.btnStopRealPlay.Enabled = true;
        }
        private void btnStopRealPlay_Click(object sender, EventArgs e)
        {
            StopRealPlay(_ActiveDevice);
            this.btnRealPlay.Enabled = true;
            this.btnStopRealPlay.Enabled = false;
        }
        #endregion


    }
}
