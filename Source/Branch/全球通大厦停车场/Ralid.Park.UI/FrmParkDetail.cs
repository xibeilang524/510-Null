using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    public partial class FrmParkDetail : FrmDetailBase
    {
        private ParkBll bll = new ParkBll(AppSettings.CurrentSetting.ParkConnect);

        public FrmParkDetail()
        {
            InitializeComponent();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置父停车场
        /// </summary>
        public ParkInfo ParentPark { get; set; }
        #endregion

        #region  基类事件处理
        protected override void InitControls()
        {
            if (IsAdding)
            {
                this.Text = Resources.Resource1.Form_Add;
            }
            this.comWorkStation.Init();
            this.comPort.Init();

            this.comDeviceType.Items.Clear();
            this.comDeviceType.Items.Add(EntranceDeviceTypeDescription.GetDescription(EntranceDeviceType.CANEntrance));
            this.comDeviceType.Items.Add(EntranceDeviceTypeDescription.GetDescription(EntranceDeviceType.NETEntrance));
            this.comDeviceType.Enabled = (IsAdding && ParentPark == null);

            this.comWorkMode.Items.Clear();
            this.comWorkMode.Items.Add(ParkWorkModeDescription.GetDescription(ParkWorkMode.OffLine));
            this.comWorkMode.Items.Add(ParkWorkModeDescription.GetDescription(ParkWorkMode.Fool));
            this.comWorkMode.Enabled = (IsAdding && ParentPark == null);//不允许修改停车场工作模式

            if (ParentPark != null || (UpdatingItem != null && (UpdatingItem as ParkInfo).IsRootPark == false)) //非项级停车场不能设置串口号和通讯工作站
            {
                if (ParentPark != null)
                {
                    this.comDeviceType.SelectedIndex = (int)ParentPark.DeviceType;
                    this.comWorkMode.SelectedIndex = (int)ParentPark.WorkMode;
                }
                this.comWorkStation.Enabled = false;
                this.comPort.Enabled = false;
                this.chkIsNested.Enabled = true;
                this.comDeviceType.Enabled = false;
                this.comWorkMode.Enabled = false;
            }
            else
            {
                this.comWorkStation.Enabled = true;
                this.comPort.Enabled = true;
                this.chkIsNested.Checked = false;
                this.chkIsNested.Enabled = false;
            }

            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            this.btnOk.Enabled = role.Permit(Permission.EditPark);
        }

        protected override void ItemShowing()
        {
            ParkInfo info = (ParkInfo)UpdatingItem;
            this.Text = info.ParkName;
            this.txtParkName.Text = info.ParkName;
            this.txtMaxPortCount.IntergerValue = info.TotalPosition;
            this.txtParkFullText.Text = info.ParkFullText;
            this.txtVacant.IntergerValue = info.Vacant;
            this.txtVacantText.Text = info.VacantText;
            this.comWorkStation.StationID = info.HostWorkstation;
            this.comPort.ComPort = info.CommPort;
            this.comDeviceType.SelectedIndex = (int)info.DeviceType;
            if (info.DeviceType == EntranceDeviceType.CANEntrance)
            {
                this.comWorkMode.SelectedIndex = 1;
                this.comWorkMode.Enabled = false;
            }
            else
            {
                this.comWorkMode.SelectedIndex = (int)info.WorkMode;
                ////不允许修改停车场工作模式
                //this.comWorkMode.Enabled = false;
                this.comWorkMode.Enabled = info.HostWorkstation == WorkStationInfo.CurrentStation.StationID;
                this.comPort.Enabled = false;
            }
            if (!info.IsRootPark)
            {
                this.comWorkStation.Enabled = false;
                this.comPort.Enabled = false;
                this.comDeviceType.Enabled = false;
                this.comWorkMode.Enabled = false;
            }
            this.chkIsNested.Checked = info.IsNested;
        }

        protected override object GetItemFromInput()
        {
            ParkInfo info;
            bool workmodeChanged = false;
            if (IsAdding)
            {
                info = new ParkInfo();
            }
            else
            {
                info = UpdatingItem as ParkInfo;
                workmodeChanged = (info.WorkMode != (ParkWorkMode)comWorkMode.SelectedIndex);
            }
            info.ParkName = this.txtParkName.Text.Trim();
            info.TotalPosition = (short)txtMaxPortCount.IntergerValue;
            info.ParkFullText = txtParkFullText.Text;
            info.Vacant = (short)txtVacant.IntergerValue;
            info.VacantText = txtVacantText.Text.Trim();
            info.HostWorkstation = comWorkStation.StationID;
            info.CommPort = comPort.ComPort;
            info.DeviceType = (EntranceDeviceType)comDeviceType.SelectedIndex;
            info.WorkMode = (ParkWorkMode)comWorkMode.SelectedIndex;
            info.IsNested = chkIsNested.Checked;
            if (ParentPark != null)
            {
                info.ParentID = ParentPark.ParkID;
                info.DeviceType = ParentPark.DeviceType;
                info.WorkMode = ParentPark.WorkMode;
            }
            if (workmodeChanged) //如果工作模式改变了,提醒用户要重启软件
            {
                MessageBox.Show(Resources.Resource1.FrmParkDetail_WorkModeChangedAlert);
            }
            return info;
        }

        protected override CommandResult AddItem(object addingItem)
        {
            return bll.Insert(addingItem as ParkInfo);
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            return bll.Update(updatingItem as ParkInfo);
        }

        protected override bool CheckInput()
        {
            string txt = null;

            txt = this.txtParkName.Text.Trim();
            if (txt.Length == 0)
            {
                MessageBox.Show(Resources.Resource1.FrmParkDetail_InvalidName);
                return false;
            }
            if (txtMaxPortCount.IntergerValue < 0 || txtMaxPortCount.IntergerValue > 65535)
            {
                MessageBox.Show(Resources.Resource1.FrmParkDetail_InvalidTotalPort);
                return false;
            }
            if (txtVacant.IntergerValue > txtMaxPortCount.IntergerValue)
            {
                MessageBox.Show(Resources.Resource1.FrmParkDetail_InvalidVacant);
                return false;
            }
            txt = this.txtParkFullText.Text.Trim();
            byte[] bytes = System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(txt);
            if (bytes != null && bytes.Length > 63)
            {
                MessageBox.Show(Resources.Resource1.FrmParkDetail_InvalidFullText);
                return false;
            }
            txt = this.txtVacantText.Text.Trim();
            bytes=System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(txt);
            if (bytes != null && bytes.Length > 63)
            {
                MessageBox.Show(Resources.Resource1.FrmParkDetail_InvalidVacantText);
                return false;
            }
            if (comDeviceType.SelectedIndex == -1)
            {
                MessageBox.Show(Resources.Resource1.FrmParkDetail_NoDeviceType);
                comDeviceType.Focus();
                return false;
            }
            if (comWorkMode.SelectedIndex == -1)
            {
                MessageBox.Show(Resources.Resource1.FrmParkDetail_NoWorkMode);
                comWorkMode.Focus();
                return false;
            }
            return true;
        }
        #endregion

        private void comDeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comDeviceType.SelectedIndex == 0)
            {
                this.comWorkMode.SelectedIndex = 1;
                this.comWorkMode.Enabled = false;
            }
            else
            {
                this.comWorkMode.Enabled = true;
            }
        }
    }
}
