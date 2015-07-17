using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.Park.ParkAdapter;
using Ralid.GeneralLibrary.LED;

namespace Ralid.Park.UI
{
    public partial class FrmVehicleLedSetting : Form,IReportHandler
    {
        #region 静态方法
        private static FrmVehicleLedSetting _Instance;

        public static FrmVehicleLedSetting GetInstance()
        {
            if (_Instance == null) _Instance = new FrmVehicleLedSetting();
            return _Instance;
        }
        #endregion

        #region 构造函数
        public FrmVehicleLedSetting()
        {
            InitializeComponent();
        }
        #endregion

        #region 常量
        /// <summary>
        /// 命令之间的发送时间间隔
        /// </summary>
        private const int _SendInterval = 500;
        #endregion

        #region 私有变量
        private VehicleLedSetting _VehicleLedSetting;
        private Dictionary<byte, VehicleLed> _VehicleLeds = new Dictionary<byte, VehicleLed>();
        #endregion

        #region 公共只读属性
        /// <summary>
        /// 获取设置窗体的车辆信息显示LED屏设置
        /// </summary>
        public VehicleLedSetting VehicleLedSetting
        {
            get
            {
                return _VehicleLedSetting;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            _VehicleLedSetting = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<VehicleLedSetting>();
            InitLedMessage();
        }
        #endregion


        #region 私有方法
        /// <summary>
        /// 通过串口获取LED屏
        /// </summary>
        /// <param name="comPort"></param>
        /// <returns></returns>
        private VehicleLed GetVehicleLed(byte comPort)
        {
            if (!_VehicleLeds.ContainsKey(comPort))
            {
                VehicleLed led = new VehicleLed(comPort);
                led.Open();
                _VehicleLeds.Add(comPort, led);
            }
            return _VehicleLeds[comPort];
        }

        /// <summary>
        /// 子屏显示信息
        /// </summary>
        /// <param name="comPort">串口号</param>
        /// <param name="address">地址</param>
        /// <param name="interval">显示时长</param>
        /// <param name="msg">显示信息</param>
        private void DisplayMsg(byte comPort, byte address, int interval, string msg)
        {
            if (AppSettings.CurrentSetting.Debug)
            {
                string str = string.Format("【Port {0} @ Address {1}】发送数据【{2}】", comPort, address, msg);
                Ralid.GeneralLibrary.LOG.FileLog.Log("VehicleLed", str);
            }
            VehicleLed led = GetVehicleLed(comPort);
            if (!led.DisplayMsg(address, msg, 500))
            {
                string str = string.Format("【Port {0} @ Address {1}】发送【{2}】失败", comPort, address, msg);
                Ralid.GeneralLibrary.LOG.FileLog.Log("VehicleLed", str);
            }
        }


        /// <summary>
        /// 车辆信息显示屏显示指定信息
        /// </summary>
        /// <param name="comPort">串口号</param>
        /// <param name="showTitle">显示标题</param>
        /// <param name="address">子屏地址</param>
        /// <param name="msgType">显示类型</param>
        /// <param name="displayMsg">显示信息</param>
        /// <returns>true:已发送，false:不需发送</returns>
        private bool VehicleLedItemDisplayMsg(byte comPort, bool showTitle, byte address, string title, int interval, VehicleLEDMessageType msgType, string displayMsg)
        {
            if (address > 0)
            {
                string msg = displayMsg;
                if (showTitle && msgType != VehicleLEDMessageType.None)
                {
                    msg = title + "：" + msg;
                }
                DisplayMsg(comPort, address, interval, msg);
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// 获取初始化LED屏信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgType"></param>
        /// <returns></returns>
        private string GetInitLedMessage(int parkID, int entranceID, VehicleLEDMessageType msgType)
        {
            ParkInfo park = null;
            EntranceInfo entrance = null;
            string empty = " ";

            switch (msgType)
            {
                case VehicleLEDMessageType.TotalPosition:
                    if (parkID > 0) park = ParkBuffer.Current.GetPark(parkID);
                    return park != null ? park.TotalPosition.ToString() : empty;
                case VehicleLEDMessageType.Vacant:
                    if (parkID > 0) park = ParkBuffer.Current.GetPark(parkID);
                    int vacant = park != null && park.Vacant > 0 ? park.Vacant : 0;
                    return park != null ? vacant.ToString() : empty;
                case VehicleLEDMessageType.Park:
                    if (parkID > 0) park = ParkBuffer.Current.GetPark(parkID);
                    return park != null ? park.ParkName : empty;
                case VehicleLEDMessageType.Entrance:
                    if (entranceID > 0) entrance = ParkBuffer.Current.GetEntrance(entranceID);
                    return entrance != null ? entrance.EntranceName : empty;
                default:
                    return empty;
            }
        }

        /// <summary>
        /// 初始化Led屏信息
        /// </summary>
        /// <param name="item"></param>
        private void InitLedItemMessage(VehicleLedItem item)
        {
            string msg = GetInitLedMessage(item.ParkID, item.EntranceID, item.SubMessage1);
            if (VehicleLedItemDisplayMsg(item.ComPort, item.ShowTitle, item.SubAddress1,item.SubTitle1, item.SubInterval1, item.SubMessage1, msg))
            {
                //发送后等待一定时间
                //Thread.Sleep(_SendInterval);
            }
            msg = GetInitLedMessage(item.ParkID, item.EntranceID, item.SubMessage2);
            if (VehicleLedItemDisplayMsg(item.ComPort, item.ShowTitle, item.SubAddress2, item.SubTitle2, item.SubInterval2, item.SubMessage2, msg))
            {
                //发送后等待一定时间
                //Thread.Sleep(_SendInterval);
            }
            msg = GetInitLedMessage(item.ParkID, item.EntranceID, item.SubMessage3);
            if (VehicleLedItemDisplayMsg(item.ComPort, item.ShowTitle, item.SubAddress3, item.SubTitle3, item.SubInterval3, item.SubMessage3, msg))
            {
            }
        }

        /// <summary>
        /// 初始化当前工作站控制的Led屏信息
        /// </summary>
        private void InitLedMessage()
        {
            if (_VehicleLedSetting != null)
            {
                List<VehicleLedItem> items = _VehicleLedSetting.GetLEDs(WorkStationInfo.CurrentStation.StationID);
                if (items != null)
                {
                    foreach (VehicleLedItem item in items)
                    {
                        InitLedItemMessage(item);
                    }
                }
            }
        }

        /// <summary>
        /// 显示信息是否刷卡事件信息
        /// </summary>
        /// <param name="msgType"></param>
        /// <returns></returns>
        private bool IsCardEventMsg(VehicleLEDMessageType msgType)
        {
            switch (msgType)
            { 
                case VehicleLEDMessageType.Department:
                case VehicleLEDMessageType.OwnerName:
                case VehicleLEDMessageType.CardCarPlate:
                case VehicleLEDMessageType.RegCarPlate:
                case VehicleLEDMessageType.CardCertificate:
                case VehicleLEDMessageType.LastCarPlate:
                case VehicleLEDMessageType.LastDateTime:
                case VehicleLEDMessageType.LastEntrance:
                case VehicleLEDMessageType.ValidDate:
                case VehicleLEDMessageType.Balance:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 清除某通道的刷卡事件信息
        /// </summary>
        /// <param name="entranceID"></param>
        private void ClearCardEventMessage(int entranceID)
        {
            if (_VehicleLedSetting != null)
            {
                List<VehicleLedItem> items = _VehicleLedSetting.GetLEDsFromEntranceID(WorkStationInfo.CurrentStation.StationID, entranceID);
                if (items != null)
                {
                    foreach (VehicleLedItem item in items)
                    {
                            string msg = " ";
                        if (IsCardEventMsg(item.SubMessage1))
                        {
                            if (VehicleLedItemDisplayMsg(item.ComPort, item.ShowTitle, item.SubAddress1, item.SubTitle1, item.SubInterval1, item.SubMessage1, msg))
                            {
                                //发送后等待一定时间
                                //Thread.Sleep(_SendInterval);
                            }
                        }
                        if (IsCardEventMsg(item.SubMessage2))
                        {
                            if (VehicleLedItemDisplayMsg(item.ComPort, item.ShowTitle, item.SubAddress2, item.SubTitle2, item.SubInterval2, item.SubMessage2, msg))
                            {
                                //发送后等待一定时间
                                //Thread.Sleep(_SendInterval);
                            }
                        }

                        if (IsCardEventMsg(item.SubMessage3))
                        {
                            if (VehicleLedItemDisplayMsg(item.ComPort, item.ShowTitle, item.SubAddress3, item.SubTitle3, item.SubInterval3, item.SubMessage3, msg))
                            {
                            }
                        }
                    }
                }
            }
        }

        private void ShowSetting()
        {
            dataGridView1.Rows.Clear();
            if (_VehicleLedSetting != null && _VehicleLedSetting.Items != null && _VehicleLedSetting.Items.Count > 0)
            {
                List<WorkStationInfo> stations = (new WorkstationBll(AppSettings.CurrentSetting.ParkConnect)).GetAllWorkstations().QueryObjects;
                foreach (VehicleLedItem item in _VehicleLedSetting.Items)
                {
                    ParkInfo park = ParkBuffer.Current.GetPark(item.ParkID);
                    EntranceInfo entrance = ParkBuffer.Current.GetEntrance(item.EntranceID);
                    WorkStationInfo station = stations == null ? null : stations.FirstOrDefault(s => s.StationID == item.StationID);
                    int row = dataGridView1.Rows.Add();
                    ShowItemOnRow(dataGridView1.Rows[row], item, park != null ? park.ParkName : string.Empty, entrance != null ? entrance.EntranceName : string.Empty, station != null ? station.StationName : string.Empty);
                    if (chkOnlyStationLed.Checked)
                    {
                        dataGridView1.Rows[row].Visible = item != null && item.StationID == WorkStationInfo.CurrentStation.StationID;
                    }
                }
            }
        }

        private void ShowItemOnRow(DataGridViewRow row, VehicleLedItem item,string park, string entrance, string station)
        {
            row.Tag = item;

            row.Cells["colName"].Value = item.Name;
            row.Cells["colPark"].Value = park;
            row.Cells["colPark"].Tag = item.ParkID;
            row.Cells["colEntrance"].Value = entrance;
            row.Cells["colEntrance"].Tag = item.EntranceID;
            row.Cells["colStation"].Value = station;
            row.Cells["colStation"].Tag = item.StationID;
            row.Cells["colComPort"].Value = item.ComPort;
            row.Cells["colShowTitle"].Value = item.ShowTitle;
            row.Cells["colSubAddress1"].Value = item.SubAddress1;
            row.Cells["colSubTitle1"].Value = item.SubTitle1;
            row.Cells["colSubMessage1"].Value = VehicleLEDMessageTypeDescription.GetDescription(item.SubMessage1);
            row.Cells["colSubMessage1"].Tag = item.SubMessage1;
            row.Cells["colSubInterval1"].Value = item.SubInterval1;
            row.Cells["colSubAddress2"].Value = item.SubAddress2;
            row.Cells["colSubTitle2"].Value = item.SubTitle2;
            row.Cells["colSubMessage2"].Value = VehicleLEDMessageTypeDescription.GetDescription(item.SubMessage2);
            row.Cells["colSubMessage2"].Tag = item.SubMessage2;
            row.Cells["colSubInterval2"].Value = item.SubInterval2;
            row.Cells["colSubAddress3"].Value = item.SubAddress3;
            row.Cells["colSubTitle3"].Value = item.SubTitle3;
            row.Cells["colSubMessage3"].Value = VehicleLEDMessageTypeDescription.GetDescription(item.SubMessage3);
            row.Cells["colSubMessage3"].Tag = item.SubMessage3;
            row.Cells["colSubInterval3"].Value = item.SubInterval3;
            row.Cells["colMemo"].Value = item.Memo;
        }

        /// <summary>
        /// 获取某LED屏名称在网格中所在行的行号,如果没有找到,返回-1
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int FindRow(string name)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["colName"].Value.ToString() == name)
                {
                    return row.Index;
                }
            }
            return -1;
        }
        #endregion


        #region 事件处理程序
        private void FrmVehicleLedSetting_Load(object sender, EventArgs e)
        {
            ResourceUtil.ApplyResource(this);
            ShowSetting();
        }

        private void FrmVehicleLedSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }


        private void chkOnlyStationLed_CheckedChanged(object sender, EventArgs e)
        {
            ShowSetting();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                _VehicleLedSetting = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<VehicleLedSetting>();
                ShowSetting();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                VehicleLedSetting setting = new VehicleLedSetting();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    //VehicleLedItem item = new VehicleLedItem();
                    //item.Name = Convert.ToString(row.Cells["colName"].Value);
                    //item.EntranceID = Convert.ToInt32(row.Cells["colEntrance"].Tag);
                    //item.StationID = Convert.ToString(row.Cells["colStation"].Tag);
                    //item.ComPort = Convert.ToByte(row.Cells["colComPort"].Value);
                    //item.ShowTitle = Convert.ToBoolean(row.Cells["colShowTitle"].Value);
                    //item.SubAddress1 = Convert.ToByte(row.Cells["colSubAddress1"].Value);
                    //item.SubMessage1 = (VehicleLEDMessageType)Convert.ToInt32(row.Cells["colSubMessage1"].Tag);
                    //item.SubAddress2 = Convert.ToByte(row.Cells["colSubAddress2"].Value);
                    //item.SubMessage2 = (VehicleLEDMessageType)Convert.ToInt32(row.Cells["colSubMessage2"].Tag);
                    //item.SubAddress3 = Convert.ToByte(row.Cells["colSubAddress3"].Value);
                    //item.SubMessage3 = (VehicleLEDMessageType)Convert.ToInt32(row.Cells["colSubMessage3"].Tag);
                    //item.Memo = Convert.ToString(row.Cells["colMemo"].Value);

                    VehicleLedItem item = row.Tag as VehicleLedItem;
                    if (item != null)
                    {
                        setting.Items.Add(item);
                    }
                }
                CommandResult ret = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).SaveSetting<VehicleLedSetting>(setting);
                if (ret.Result == ResultCode.Successful)
                {
                    _VehicleLedSetting = setting;
                    foreach (IParkingAdapter pad in ParkingAdapterManager.Instance.ParkAdapters)
                    {
                        UpdateSystemParamSettingNotity notity = new UpdateSystemParamSettingNotity();
                        notity.Operator = OperatorInfo.CurrentOperator;
                        notity.StationID = WorkStationInfo.CurrentStation.StationID;
                        notity.StationName = WorkStationInfo.CurrentStation.StationName;
                        notity.ParamTypeName = typeof(VehicleLedSetting).Name;
                        pad.UpdateSystemParamSetting(notity);
                    }
                    MessageBox.Show(Resources.Resource1.FrmReportBase_SaveSuccess);
                }
                else
                {
                    MessageBox.Show(ret.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void mnu_Add_Click(object sender, EventArgs e)
        {
            FrmVehicleLedDetail frm = new FrmVehicleLedDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (FindRow(frm.Item.Name) >= 0)
                {
                    MessageBox.Show(string.Format(Resources.Resource1.FrmVehicleLedSetting_HadLed, frm.Item.Name));
                }
                else
                {
                    int row = dataGridView1.Rows.Add();
                    ShowItemOnRow(dataGridView1.Rows[row], frm.Item,frm.ParkName, frm.EntranceName, frm.StationName);
                }
            }
        }
        private void mnu_Update_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                FrmVehicleLedDetail frm = new FrmVehicleLedDetail();
                VehicleLedItem item = dataGridView1.SelectedRows[0].Tag as VehicleLedItem;
                frm.Item = item.Clone();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (FindRow(frm.Item.Name) >= 0 && FindRow(frm.Item.Name) != dataGridView1.SelectedRows[0].Index)
                    {
                        MessageBox.Show(string.Format(Resources.Resource1.FrmVehicleLedSetting_HadLed, frm.Item.Name));
                    }
                    else
                    {
                        dataGridView1.SelectedRows[0].Tag = frm.Item;
                        ShowItemOnRow(dataGridView1.SelectedRows[0], frm.Item, frm.ParkName, frm.EntranceName, frm.StationName);
                    }
                }
            }
        }
        private void mnu_Delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Resources.Resource1.FrmMasterBase_DeleteQuery, Resources.Resource1.Form_Query, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.Remove(row);
                }
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            this.mnu_Update_Click(sender, e);
        }

        private void mnu_InitLedMsg_Click(object sender, EventArgs e)
        {
            VehicleLedItem item = null;
            if (dataGridView1.SelectedRows.Count == 1)
            {
                item = dataGridView1.SelectedRows[0].Tag as VehicleLedItem;
            }
            if (item != null)
            {
                if (item.StationID == WorkStationInfo.CurrentStation.StationID)
                {
                    InitLedItemMessage(item);
                    MessageBox.Show(Resources.Resource1.FrmVehicleLedSetting_SendInitLedItemMessage);
                }
                else
                {
                    MessageBox.Show(Resources.Resource1.FrmVehicleLedSetting_CannotContralLed); 
                }
            }
            else
            {
                MessageBox.Show(Resources.Resource1.FrmVehicleLedSetting_SelectLed);
            }
        }
        #endregion


        #region IReportHandler私有方法
        /// <summary>
        /// 获取事件要显示的信息
        /// </summary>
        /// <param name="msg">返回显示信息</param>
        /// <param name="parkID">停車場ID</param>
        /// <param name="entranceID">通道ID</param>
        /// <param name="msgType">显示类型</param>
        /// <param name="report">事件</param>
        /// <returns>成功或失败，当不需要显示事件信息时，返回失败</returns>
        private bool GetSendMessage(ref string msg,int parkID, int entranceID, VehicleLEDMessageType msgType, ReportBase report)
        {
            if (report is CardEventReport)
            {
                return GetSendMessage(ref msg,parkID,entranceID, msgType, report as CardEventReport);
            }
            else if (report is ParkVacantReport)
            {
                return GetSendMessage(ref msg, parkID, entranceID, msgType, report as ParkVacantReport);
            }
            msg=string.Empty;
            return false;
        }

        /// <summary>
        /// 获取刷卡事件要显示的信息
        /// </summary>
        /// <param name="msg">返回显示信息</param>
        /// <param name="msgType">显示类型</param>
        /// <param name="report">事件</param>
        /// <returns>成功或失败，当不需要显示刷卡信息时，返回失败</returns>
        private bool GetSendMessage(ref string msg, int parkID, int entranceID, VehicleLEDMessageType msgType, CardEventReport report)
        {
            ParkInfo park = null;
            EntranceInfo entrance = null;
            switch (msgType)
            {
                case VehicleLEDMessageType.Department:
                    msg = report.Department;
                    return true;
                case VehicleLEDMessageType.OwnerName:
                    msg = report.OwnerName;
                    return true;
                case VehicleLEDMessageType.CardCarPlate:
                    msg = report.CardCarPlate;
                    return true;
                case VehicleLEDMessageType.RegCarPlate:
                    msg = report.CarPlate;
                    return true;
                case VehicleLEDMessageType.CardCertificate:
                    msg = report.CardCertificate;
                    return true;
                case VehicleLEDMessageType.LastCarPlate:
                    if (report.IsExitEvent)
                    {
                        msg = report.LastCarPlate;
                    }
                    else
                    {
                        //如果是入场事件，入场车牌为识别车牌
                        msg = report.CarPlate;
                    }
                    return true;
                case VehicleLEDMessageType.LastDateTime:
                    if (report.IsExitEvent)
                    {
                        //只有只有出场事件并且有入场时间时，才会返回入场时间
                        msg = report.IsExitEvent && report.LastDateTime != null ? report.LastDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                    }
                    else
                    {
                        //如果是入场事件，返回事件时间
                        msg = report.EventDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    return true;
                case VehicleLEDMessageType.LastEntrance:
                    if (report.IsExitEvent)
                    {
                        EntranceInfo entrace = ParkBuffer.Current.GetEntrance(report.LastEntrance);
                        msg = entrace != null ? entrace.EntranceName : string.Empty;
                    }
                    else
                    {
                        //如果是入场事件，返回事件的通道
                        EntranceInfo entrace = ParkBuffer.Current.GetEntrance(report.EntranceID);
                        msg = entrace != null ? entrace.EntranceName : string.Empty;
                    }
                    return true;
                case VehicleLEDMessageType.ValidDate:
                    msg = report.ValidDate.ToString("yyyy-MM-dd");
                    return true;
                case VehicleLEDMessageType.Balance:
                    msg = report.Balance.ToString();
                    return true;
                case VehicleLEDMessageType.TotalPosition:
                    if (parkID > 0) park = ParkBuffer.Current.GetPark(parkID);
                    msg = park != null ? park.TotalPosition.ToString() : string.Empty;
                    return true;
                case VehicleLEDMessageType.Park:
                    if (parkID > 0) park = ParkBuffer.Current.GetPark(parkID);
                    msg = park != null ? park.ParkName : string.Empty;
                    return true;
                case VehicleLEDMessageType.Entrance:
                    if (entranceID > 0) entrance = ParkBuffer.Current.GetEntrance(entranceID);
                    msg = entrance != null ? entrance.EntranceName : string.Empty;
                    return true;
                default:
                    msg = string.Empty;
                    return false;
            }
        }

        /// <summary>
        /// 获取车位数事件的显示信息
        /// </summary>
        /// <param name="msg">返回显示信息</param>
        /// <param name="msgType">显示类型</param>
        /// <param name="report">事件</param>
        /// <returns>>成功或失败，当不需要显示车位信息时，返回失败</returns>
        private bool GetSendMessage(ref string msg, int parkID, int entranceID, VehicleLEDMessageType msgType, ParkVacantReport report)
        {
            ParkInfo park = null;
            EntranceInfo entrance = null;
            switch (msgType)
            {
                case VehicleLEDMessageType.Vacant:
                    msg = report.ParkVacant > 0 ? report.ParkVacant.ToString() : "0";
                    return true;
                case VehicleLEDMessageType.TotalPosition:
                    if (parkID > 0) park = ParkBuffer.Current.GetPark(parkID);
                    msg = park != null ? park.TotalPosition.ToString() : string.Empty;
                    return true;
                case VehicleLEDMessageType.Park:
                    if (parkID > 0) park = ParkBuffer.Current.GetPark(parkID);
                    msg = park != null ? park.ParkName : string.Empty;
                    return true;
                case VehicleLEDMessageType.Entrance:
                    if (entranceID > 0) entrance = ParkBuffer.Current.GetEntrance(entranceID);
                    msg = entrance != null ? entrance.EntranceName : string.Empty;
                    return true;
                default:
                    msg = string.Empty;
                    return false;
            }
        }

        /// <summary>
        /// 车辆信息显示屏显示信息
        /// </summary>
        /// <param name="comPort">串口号</param>
        /// <param name="showTitle">显示标题</param>
        /// <param name="address">子屏地址</param>
        /// <param name="msgType">显示类型</param>
        /// <param name="report">事件</param>
        /// <returns>true:已发送，false:不需发送</returns>
        private bool VehicleLedItemDisplay(int parkID, int entranceID, byte comPort, bool showTitle, byte address, string title, int interval, VehicleLEDMessageType msgType, ReportBase report)
        {
            if (address > 0)
            {
                string msg = string.Empty;
                if (GetSendMessage(ref msg, parkID, entranceID, msgType, report))
                {
                    if (showTitle)
                    {
                        msg = title + "：" + msg;
                    }
                    DisplayMsg(comPort, address, interval, msg);
                    return true;
                }
            }
            return false;
        }

        private void ReportHandle(CardEventReport report)
        {
            try
            {
                //更新到相关控制器的屏上
                List<VehicleLedItem> items = _VehicleLedSetting.GetLEDsFromEntranceID(WorkStationInfo.CurrentStation.StationID, report.EntranceID);

                if (items != null && items.Count > 0)
                {
                    foreach (VehicleLedItem item in items)
                    {
                        if (VehicleLedItemDisplay(item.ParkID,item.EntranceID,item.ComPort, item.ShowTitle, item.SubAddress1,item.SubTitle1, item.SubInterval1, item.SubMessage1, report))
                        {
                            //发送后等待一定时间
                            //Thread.Sleep(_SendInterval);
                        }
                        if (VehicleLedItemDisplay(item.ParkID, item.EntranceID, item.ComPort, item.ShowTitle, item.SubAddress2, item.SubTitle2, item.SubInterval2, item.SubMessage2, report))
                        {
                            //发送后等待一定时间
                            //Thread.Sleep(_SendInterval);
                        }
                        if (VehicleLedItemDisplay(item.ParkID, item.EntranceID, item.ComPort, item.ShowTitle, item.SubAddress3, item.SubTitle3, item.SubInterval3, item.SubMessage3, report))
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void ReportHandle(ParkVacantReport report)
        {
            try
            {
                //更新到相关停车场的屏上
                List<VehicleLedItem> items = _VehicleLedSetting.GetLEDsFromParkID(WorkStationInfo.CurrentStation.StationID, report.ParkID);

                if (items != null && items.Count > 0)
                {
                    foreach (VehicleLedItem item in items)
                    {
                        if (VehicleLedItemDisplay(item.ParkID, item.EntranceID, item.ComPort, item.ShowTitle, item.SubAddress1, item.SubTitle1, item.SubInterval1, item.SubMessage1, report))
                        {
                            //发送后等待一定时间
                            //Thread.Sleep(_SendInterval);
                        }
                        if (VehicleLedItemDisplay(item.ParkID, item.EntranceID, item.ComPort, item.ShowTitle, item.SubAddress2, item.SubTitle2, item.SubInterval2, item.SubMessage2, report))
                        {
                            //发送后等待一定时间
                            //Thread.Sleep(_SendInterval);
                        }
                        if (VehicleLedItemDisplay(item.ParkID, item.EntranceID, item.ComPort, item.ShowTitle, item.SubAddress3, item.SubTitle3, item.SubInterval3, item.SubMessage3, report))
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void ReportHandle(CarSenseReport report)
        {
            ClearCardEventMessage(report.EntranceID);
        }

        private void ReportHandle(UpdateSystemParamSettingReport report)
        {
            if (report.StationID != WorkStationInfo.CurrentStation.StationID)
            {
                Type type = typeof(VehicleLedSetting);
                if (report.ParamTypeName == type.Name)
                {
                    _VehicleLedSetting = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<VehicleLedSetting>();

                    if (this.Visible)
                    {
                        Action action = delegate()
                        {
                            ShowSetting();
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
                }
            }
        }
        #endregion

        #region 实现 IReportHandler 接口
        public void ProcessReport(ReportBase report)
        {
            try
            {
                if (report is CardEventReport)
                {
                    CardEventReport r = report as CardEventReport;
                    ReportHandle(r);
                }
                else if(report is ParkVacantReport)
                {
                    ParkVacantReport r = report as ParkVacantReport;
                    ReportHandle(r);
                }
                else if (report is CarSenseReport)
                {
                    CarSenseReport r = report as CarSenseReport;
                    //车到、车走时清空刷卡事件信息
                    ReportHandle(r);
                }
                else if (report is UpdateSystemParamSettingReport)
                {
                    UpdateSystemParamSettingReport r = report as UpdateSystemParamSettingReport;
                    ReportHandle(r);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion







    }
}
