using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.SearchCondition;

namespace InterfaceTestProject
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        ServiceReference1.ParkWebServiceClient client = new ServiceReference1.ParkWebServiceClient();

        private void ClearCardFeePayInterface()
        {
            this.lblCardID.Text = string.Empty;
            this.lblChargeTime.Text = string.Empty;
            this.lblEnterTime.Text = string.Empty;
            this.lblEntranceName.Text = string.Empty;
            this.lblTimeInterval.Text = string.Empty;
            this.lblCarPlate.Text = string.Empty;
            this.lblAccounts.Text = string.Empty;
            this.lblLastTotalFee.Text = string.Empty;
            this.lblDiscount.Text = string.Empty;
            this.lblCurrDiscountHour.Text = string.Empty;
            this.lblPaid.Text = string.Empty;
            this.lblMemo.Text = string.Empty;

            this.txtPaid.DecimalValue = 0;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //初始化系统设置

            SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.AvailableParkConnect);
            //UserSetting.Current = ssb.GetOrCreateSetting<UserSetting>();
            //HolidaySetting.Current = ssb.GetOrCreateSetting<HolidaySetting>();
            //AccessSetting.Current = ssb.GetOrCreateSetting<AccessSetting>();
            //TariffSetting.Current = ssb.GetOrCreateSetting<TariffSetting>();
            CarTypeSetting.Current = ssb.GetOrCreateSetting<CarTypeSetting>();
            //CustomCardTypeSetting.Current = ssb.GetOrCreateSetting<CustomCardTypeSetting>();
            //BaseCardTypeSetting.Current = ssb.GetOrCreateSetting<BaseCardTypeSetting>();
            //KeySetting.Current = ssb.GetOrCreateSetting<KeySetting>();

            //GlobalVariables.SetCardReaderKeysetting();

            this.ucCard1.Init();
            this.ucCard1.UseToShow();

            ClearCardFeePayInterface();
            this.cbbPayMode.SelectedIndex = 0;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string cardID = this.txtCardID.Text.Trim();
            if (this.tabControl1.SelectedTab == tabPage1)
            {//卡片管理接口
                if (CheckInput())
                {
                    CardInfo info = client.GetCardByID(cardID).QueryObject;
                    if (info != null)
                        this.ucCard1.Card = info;
                    else
                    {
                        MessageBox.Show("找不到卡片！");
                        this.ucCard1.Card = null;
                    }
                }
            }
            else if (this.tabControl1.SelectedTab == tabPage2)
            {//空车位 
                if (string.IsNullOrEmpty(this.txtParkID.Text))
                {
                    MessageBox.Show("必须输入parkID");
                    return;
                }
                string strParkID = this.txtParkID.Text.Trim();
                int parkID = 0;
                try
                {
                    parkID = Convert.ToInt32(strParkID);
                }
                catch{}
                this.txtVacant.Text = client.GetVacant(parkID).ToString();
            }
            else if (this.tabControl1.SelectedTab == tabPage3)
            {//卡片状态及停车费用 
                if (CheckInput())
                {
                    CardInfo info = client.GetCardByID(cardID).QueryObject;
                    if (info != null)
                    {
                        string descr = string.Empty;
                        CardStatus cs = client.GetCardStatusByCardID(cardID);
                        if (cs == CardStatus.Deleted)
                            descr = "已删除";
                        else if (cs == CardStatus.Disabled)
                            descr = "禁用";
                        else if (cs == CardStatus.Enabled)
                            descr = "已发行";
                        else if (cs == CardStatus.Loss)
                            descr = "挂失";
                        else if (cs == CardStatus.Recycled)
                            descr = "待发行";
                        else if (cs == CardStatus.UnRegister)
                            descr = "此卡未登记";
                        this.txtCardStatus.Text = descr;
                        this.txtParkFee.Text = client.GetCardLastPayment(info, null).ToString();
                    }
                }
            }
            else if (this.tabControl1.SelectedTab == tabPage4)
            {//记录查询 
                RecordSearchCondition con = new RecordSearchCondition();
                con.CardID = cardID;
                if (this.tabControlRecord.SelectedTab == tabRecord1)
                {//卡片充值记录

                    List<CardChargeRecord> list = client.GetCardChargeRecords(con).QueryObjects;
                    this.cDGV_Record1.DataSource = list;
                }
                else if (this.tabControlRecord.SelectedTab == tabRecord2)
                {//卡片延期记录 
                    List<CardDeferRecord> list = client.GetCardDeferRecords(con).QueryObjects;
                    this.cDGV_Record2.DataSource = list;
                }
                else if (this.tabControlRecord.SelectedTab == tabRecord3)
                {//卡片挂失恢复记录 
                    List<CardLostRestoreRecord> list = client.GetCardLostRestoreRecords(con).QueryObjects;
                    this.cDGV_Record3.DataSource = list;
                }
                else if (this.tabControlRecord.SelectedTab == tabRecord4)
                {//卡片禁用启用记录
                    List<CardDisableEnableRecord> list = client.GetCardDisableEnableRecords(con).QueryObjects;
                    this.cDGV_Record4.DataSource = list;
                }
                else if (this.tabControlRecord.SelectedTab == tabRecord5)
                {//卡片回收记录
                    List<CardRecycleRecord> list = client.GetCardRecycleRecords(con).QueryObjects;
                    this.cDGV_Record5.DataSource = list;
                }
                else if (this.tabControlRecord.SelectedTab == tabRecord6)
                {//卡片发行记录
                    if (CheckInput())
                    {
                        List<CardReleaseRecord> list = client.GetCardReleaseRecords(con).QueryObjects;
                        this.cDGV_Record6.DataSource = list;
                    }
                }
                else if (this.tabControlRecord.SelectedTab == tabRecord7)
                {//卡片删除记录
                    if (CheckInput())
                    {
                        List<CardDeleteRecord> list = client.GetCardDeleteRecords(con).QueryObjects;
                        this.cDGV_Record7.DataSource = list;
                    }
                }
            }
            else if (this.tabControl1.SelectedTab == tabPage5)
            {//设备清单
                List<EntranceInfo> list = client.GetAllEntraces().QueryObjects;
                this.cDGV_Entrance.DataSource = list;
            }
            else if (this.tabControl1.SelectedTab == tabPage6)
            {//停车收费接口
                ClearCardFeePayInterface();
                QueryResult<ServiceReference1.WSCardPaymentInfo> result = client.GetCardPayment(this.txtCardID.Text, this.txtDiscountHourInput.Text, this.txtDiscountInput.Text, string.Empty, string.Empty);
                if (result.Result != ResultCode.Successful)
                {
                    MessageBox.Show(result.Message);
                }
                else
                {
                    this.lblCardID.Text = result.QueryObject.CardID;
                    this.lblChargeTime.Text = result.QueryObject.ChargeDateTime;
                    this.lblEnterTime.Text = result.QueryObject.EnterDateTime;
                    this.lblEntranceName.Text = result.QueryObject.EntranceName;
                    this.lblTimeInterval.Text = result.QueryObject.TimeInterval;
                    this.lblCarPlate.Text = result.QueryObject.CarPlate;
                    this.lblAccounts.Text = result.QueryObject.Accounts;
                    this.lblLastTotalFee.Text = result.QueryObject.LastTotalFee;
                    this.lblDiscount.Text = result.QueryObject.Discount;
                    this.lblCurrDiscountHour.Text = result.QueryObject.CurrDiscountHour;
                    this.lblPaid.Text = result.QueryObject.Paid;
                    this.lblMemo.Text = result.QueryObject.Memo;

                    this.txtPaid.Text = result.QueryObject.Paid;
                }
            }
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(this.txtCardID.Text))
            {
                MessageBox.Show("卡号不能为空！");
                return false;
            }
            return true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = sender as TabControl;
            if (tc.SelectedTab != tabPage1)
            {
                this.btnAdd.Enabled = false;
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
                if (tc.SelectedTab == tabPage2)
                    this.btnEdit.Enabled = true;
            }
            else
            {
                this.btnAdd.Enabled = true;
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab == tabPage1)
            {
                CardInfo info = this.ucCard1.Card;
                if (info != null)
                {
                    CommandResult cr = client.SaveCard2(info);
                    if (cr.Result == ResultCode.Successful)
                        MessageBox.Show("保存成功！");
                    else
                        MessageBox.Show("保存失败！");
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab == tabPage1)
            {
                CardInfo info = this.ucCard1.Card;
                if (info != null)
                {
                    CommandResult cr = client.SaveCard2(info);
                    if (cr.Result == ResultCode.Successful)
                        MessageBox.Show("保存成功！");
                    else
                        MessageBox.Show("保存失败！");
                }
                else
                {
                    MessageBox.Show("卡片信息不存在");
                }
            }
            else if (this.tabControl1.SelectedTab == tabPage2)
            {//空车位

                if (string.IsNullOrEmpty(this.txtParkID.Text))
                {
                    MessageBox.Show("没有parkID ！");
                    return;
                }
                string strParkID = this.txtParkID.Text.Trim();
                string strVacant = this.txtVacant.Text.Trim();
                int parkID = 0;
                int vacant = 0;
                try
                {
                    parkID = Convert.ToInt32(strParkID);
                    vacant = Convert.ToInt32(strVacant);
                }
                catch{}
                if (client.SetVacant(parkID, vacant) == ServiceReference1.InterfaceReturnCode.Success)
                    MessageBox.Show("修改成功！");
                else
                    MessageBox.Show("修改失败！请检查parkID是否正确！");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                string cardID = this.txtCardID.Text.Trim();
                CommandResult result = null;
                try
                {
                    result = client.DeleteCard(cardID);
                }
                catch (Exception)
                {
                    MessageBox.Show("已发行的，非临时卡的卡片不能删除！");
                    return;
                }
                if (result == null)
                {
                    MessageBox.Show("没有找到卡片！");
                }
                else
                {
                    if (result.Result == ResultCode.Successful)
                        MessageBox.Show("删除卡片成功！");
                    else
                        MessageBox.Show(result.Message);
                }
            }
        }

        private void btnEnableAdd_Click(object sender, EventArgs e)
        {
            this.ucCard1.UseToRelease();
        }

        private void btnEnableEdit_Click(object sender, EventArgs e)
        {
            this.ucCard1.UseToEdit();
        }

        private void btnCardFeePay_Click(object sender, EventArgs e)
        {
            CommandResult result = client.CardFeePay(this.lblCardID.Text,
                this.lblChargeTime.Text,
                this.txtPaid.Text,
                (this.cbbPayMode.SelectedIndex + 1).ToString(),
                this.lblMemo.Text,
                string.Empty,
                string.Empty);

            if (result.Result == ResultCode.Successful)
            {
                MessageBox.Show("收费成功");
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }
    }
}
