using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UI
{
    public partial class FrmVehicleLedDetail : Form
    {
        public FrmVehicleLedDetail()
        {
            InitializeComponent();
        }
        
        #region 公共属性
        /// <summary>
        /// 获取或设置车辆信息显示LED屏
        /// </summary>
        public VehicleLedItem Item { get; set; }

        /// <summary>
        /// 获取停车场名称
        /// </summary>
        public string ParkName
        {
            get
            {
                ParkInfo park = this.ucEntrance.SelectedPark;
                if (park == null)
                {
                    EntranceInfo entrance = this.ucEntrance.SelectedEntrance;
                    if (entrance != null)
                    {
                        this.ucEntrance.SelectedParkID = entrance.ParkID;
                        this.ucEntrance.SelectedEntranceID = entrance.EntranceID;
                        park = this.ucEntrance.SelectedPark;
                    }
                }
                return park != null ? park.ParkName : string.Empty;
            }
        }

        /// <summary>
        /// 获取通道名称
        /// </summary>
        public string EntranceName
        {
            get
            {
                EntranceInfo entrance = this.ucEntrance.SelectedEntrance;
                return entrance != null ? entrance.EntranceName : string.Empty;
            }
        }

        /// <summary>
        /// 获取通信工作站名称
        /// </summary>
        public string StationName
        {
            get
            {
                return this.comStation.Text;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            this.ucEntrance.Init();
            this.comStation.Init();
            this.comPort.Init();
            this.comSubAddress1.Items.Clear();
            this.comSubAddress2.Items.Clear();
            this.comSubAddress3.Items.Clear();
            for (int i = 0; i < 32; i++)
            {
                this.comSubAddress1.Items.Add(i);
                this.comSubAddress2.Items.Add(i);
                this.comSubAddress3.Items.Add(i);
            }
            this.comSubAddress1.SelectedIndex = 0;
            this.comSubAddress2.SelectedIndex = 0;
            this.comSubAddress3.SelectedIndex = 0;
            this.comSubMessage1.Init();
            this.comSubMessage2.Init();
            this.comSubMessage3.Init();
            
        }
        /// <summary>
        /// 输入检查
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if(string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                this.txtName.Focus();
                MessageBox.Show(Resources.Resource1.FrmVehicleLedDetail_InvalidName);
                return false;
            }

            return true;
        }
        #endregion

        #region 窗口处理事件

        private void FrmVehicleLedDetail_Load(object sender, EventArgs e)
        {
            InitControl();

            if (Item != null)
            {
                this.txtName.Text = Item.Name;
                this.ucEntrance.SelectedParkID = Item.ParkID;
                this.ucEntrance.SelectedEntranceID = Item.EntranceID;
                this.comStation.StationID = Item.StationID;
                this.comPort.ComPort = Item.ComPort;
                this.chkShowTitle.Checked = Item.ShowTitle;
                this.chkEnabledInterval.Checked = Item.EnabledInterval;
                this.comSubAddress1.SelectedIndex = Item.SubAddress1 < 32 ? Item.SubAddress1 : 0;
                this.txtSubTitle1.Text = Item.SubTitle1;
                this.comSubMessage1.SelectedVehicleLEDMessageType = Item.SubMessage1;
                this.txtSubInterval1.IntergerValue = Item.SubInterval1;
                this.comSubAddress2.SelectedIndex = Item.SubAddress2 < 32 ? Item.SubAddress2 : 0;
                this.txtSubTitle2.Text = Item.SubTitle2;
                this.comSubMessage2.SelectedVehicleLEDMessageType = Item.SubMessage2;
                this.txtSubInterval2.IntergerValue = Item.SubInterval2;
                this.comSubAddress3.SelectedIndex = Item.SubAddress3 < 32 ? Item.SubAddress3 : 0;
                this.txtSubTitle3.Text = Item.SubTitle3;
                this.comSubMessage3.SelectedVehicleLEDMessageType = Item.SubMessage3;
                this.txtSubInterval3.IntergerValue = Item.SubInterval3;
                this.txtMemo.Text = Item.Memo;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                if (Item == null)
                {
                    Item = new VehicleLedItem();
                }
                Item.Name = this.txtName.Text.Trim();
                Item.ParkID = this.ucEntrance.SelectedParkID;
                Item.EntranceID = this.ucEntrance.SelectedEntranceID;
                if (Item.ParkID == 0 && Item.EntranceID != 0)
                {
                    //如果没有选择停车场，而有选择通道的，自动选择通道的停车场
                    EntranceInfo entrance = this.ucEntrance.SelectedEntrance;
                    if (entrance != null)
                    {
                        Item.ParkID = entrance.ParkID;
                    }
                }
                Item.StationID = this.comStation.StationID;
                Item.ComPort = this.comPort.ComPort;
                Item.ShowTitle = this.chkShowTitle.Checked;
                Item.EnabledInterval = this.chkEnabledInterval.Visible ? this.chkEnabledInterval.Checked : false;
                Item.SubAddress1 = (byte)this.comSubAddress1.SelectedIndex;
                Item.SubTitle1 = this.txtSubTitle1.Text.Trim();
                Item.SubMessage1 = this.comSubMessage1.SelectedVehicleLEDMessageType;
                Item.SubInterval1 = this.txtSubInterval1.IntergerValue;
                Item.SubAddress2 = (byte)this.comSubAddress2.SelectedIndex;
                Item.SubTitle2 = this.txtSubTitle2.Text.Trim();
                Item.SubMessage2 = this.comSubMessage2.SelectedVehicleLEDMessageType;
                Item.SubInterval2 = this.txtSubInterval2.IntergerValue;
                Item.SubAddress3 = (byte)this.comSubAddress3.SelectedIndex;
                Item.SubTitle3 = this.txtSubTitle3.Text.Trim();
                Item.SubMessage3 = this.comSubMessage3.SelectedVehicleLEDMessageType;
                Item.SubInterval3 = this.txtSubInterval3.IntergerValue;
                Item.Memo = this.txtMemo.Text.Trim();

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
        #endregion


    }
}
