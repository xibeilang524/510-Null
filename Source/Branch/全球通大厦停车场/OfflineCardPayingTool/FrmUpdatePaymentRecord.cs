using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.LocalDataBase.BLL;
using Ralid.Park.LocalDataBase.Model;

namespace OfflineCardPayingTool
{
    public partial class FrmUpdatePaymentRecord : Ralid.Park.UI.ReportAndStatistics.FrmReportBase
    {
        #region 构造函数
        public FrmUpdatePaymentRecord()
        {
            InitializeComponent();
            this.ItemSearching += ItemSearching_Handler;
        }
        #endregion

        #region 私有方法
        private void InitOperatorCombox()
        {
            List<OperatorInfo> items = LDB_ParkingDataBuffer.Current.Operators;
            this.comOperator.Items.Clear();
            this.comOperator.Items.Add("");
            foreach (var item in items)
            {
                this.comOperator.Items.Add(item);
            }
            this.comOperator.DisplayMember = "OperatorName";
            this.comOperator.SelectedIndex = 0;
            this.comOperator.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void InitCardTypeCombox()
        {
            this.comCardType.Items.Clear();
            this.comCardType.Items.Add(string.Empty);
            this.comCardType.Items.AddRange(
                new CardType[]{
                CardType.VipCard,
                CardType.OwnerCard ,
                CardType.MonthRentCard ,
                CardType.PrePayCard ,
                CardType.TempCard ,
                CardType.UserDefinedCard1,
                CardType.UserDefinedCard2
            });
            this.comCardType.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void ShowReportsOnGrid(List<LDB_CardPaymentInfo> items)
        {
            decimal paid = 0;
            decimal discount = 0;
            this.GridView.Rows.Clear();
            items = (from LDB_CardPaymentInfo cr in items
                     orderby cr.ChargeDateTime descending
                     select cr).ToList();
            foreach (LDB_CardPaymentInfo info in items)
            {
                if (info.Paid == 0 && this.rdPaid.Checked) continue; //只查询收费记录时排除掉免费记录
                if (info.Paid > 0 && this.rdFree.Checked) continue;  //只查询免费记录时排除掉收费记录
                int row = GridView.Rows.Add();
                ShowCardPaymentOnRow(info, row);
                paid += info.Paid;
                discount += info.Discount;
            }
            this.txtPaid.Text = paid.ToString();
            this.txtAccounts.Text = discount.ToString();
        }

        private void ShowCardPaymentOnRow(LDB_CardPaymentInfo info, int row)
        {
            GridView.Rows[row].Tag = info;
            GridView.Rows[row].Cells["colCardID"].Value = info.CardID;
            GridView.Rows[row].Cells["colCarType"].Value = CarTypeSetting.Current.GetDescription(info.CarType);
            GridView.Rows[row].Cells["colCardType"].Value = info.CardType.ToString();
            GridView.Rows[row].Cells["colCarPlate"].Value = info.CarPlate;
            GridView.Rows[row].Cells["colExitDateTime"].Value = info.ChargeDateTime;
            GridView.Rows[row].Cells["colOperator"].Value = info.OperatorID;
            GridView.Rows[row].Cells["colPaymentMode"].Value = Ralid.Park.BusinessModel.Resouce.PaymentModeDescription.GetDescription(info.PaymentMode);
            GridView.Rows[row].Cells["colTariffType"].Value = Ralid.Park.BusinessModel.Resouce.TariffTypeDescription.GetDescription(info.TariffType);
            GridView.Rows[row].Cells["colEnterDateTime"].Value = info.EnterDateTime;
            GridView.Rows[row].Cells["colTimeSpan"].Value = info.TimeInterval;
            GridView.Rows[row].Cells["colPaid"].Value = info.Paid;
            GridView.Rows[row].Cells["colDiscount"].Value = info.Discount;
            GridView.Rows[row].Cells["colHandled"].Value = info.UpdateFlag;
            GridView.Rows[row].Cells["colTotalAccounts"].Value = info.Accounts;
            GridView.Rows[row].Cells["colTotalPaid"].Value = info.TotalPaid;
            GridView.Rows[row].Cells["colTotalDiscount"].Value = info.TotalDiscount;
            GridView.Rows[row].Cells["colStation"].Value = info.StationID;
            GridView.Rows[row].Cells["colMemo"].Value = info.Memo;
            GridView.Rows[row].Cells["colPaymentCode"].Value = Ralid.Park.BusinessModel.Resouce.PaymentCodeDescription.GetDescription(info.PaymentCode);
            GridView.Rows[row].Cells["colOperatorCardID"].Value = info.OperatorCardID;
            GridView.Rows[row].DefaultCellStyle.ForeColor = info.Discount > 0 ? Color.Red : Color.Black;
        }

        private void ClearInput()
        {
            this.progressBar1.Visible = false;
        }

        private void InitProgress()
        { 
            this.progressBar1.Maximum = GridView.Rows.Count;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = true;
        }

        protected void NotifyProgress(int? value)
        {
            Action<int?> action = delegate(int? v)
            {
                if (v.HasValue)
                {
                    this.progressBar1.Value = v.Value;
                }
                else
                {
                    this.progressBar1.Value++;
                }
                if (this.progressBar1.Value < this.progressBar1.Maximum)
                {
                    this.searchInfo.Text = string.Format("共 {0} 项，正在上传第 {1} 项 ", this.progressBar1.Maximum, this.progressBar1.Value + 1);
                }
                else
                {
                    this.searchInfo.Text = string.Format("共 {0} 项", GridView.Rows.Count);
                }
                this.progressBar1.Refresh();
                this.statusStrip1.Refresh();
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, value);
            }
            else
            {
                action(value);
            }
        }
        #endregion

        #region 事件处理方法
        private void FrmUpdatePaymentRecord_Load(object sender, EventArgs e)
        {
            this.ucDateTimeInterval1.Init();
            this.carTypeComboBox1.Init();
            this.comPaymentMode.Init();
            InitOperatorCombox();
            InitCardTypeCombox();
            ClearInput();
        }

        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            LDB_CardPaymentRecordSearchCondition con = new LDB_CardPaymentRecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;
            con.CardType = this.comCardType.SelectedCardType;
            con.CardID = this.txtCardID.Text.Trim();
            if (carTypeComboBox1.SelectedIndex > 0) con.CarType = carTypeComboBox1.SelectedCarType;
            if (rdUpdated.Checked) con.UpdateFlag = true;
            if (rdUnupdated.Checked) con.UpdateFlag = false;
            if (chkPaymentMode.Checked)
            {
                con.PaymentMode = comPaymentMode.SelectedPaymentMode;
            }
            con.CarPlate = this.txtCarPlate.Text.Trim();

            if (this.comOperator.SelectecOperator != null)
            {
                con.Operator = this.comOperator.SelectecOperator;
            }

            LDB_CardPaymentRecordBll bll = new LDB_CardPaymentRecordBll(LDB_AppSettings.Current.LDBConnect);
            QueryResultList<LDB_CardPaymentInfo> result = bll.GetItems(con);
            if (result.Result == ResultCode.Successful)
            {
                ShowReportsOnGrid(result.QueryObjects);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            CardPaymentRecordBll cbll = new CardPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect);
            LDB_CardPaymentRecordBll lcbll = new LDB_CardPaymentRecordBll(LDB_AppSettings.Current.LDBConnect);
            InitProgress();
            NotifyProgress(0);
                foreach (DataGridViewRow row in GridView.Rows)
                {
                    LDB_CardPaymentInfo info = row.Tag as LDB_CardPaymentInfo;
                    CardPaymentInfo payment = LDB_InfoFactory.CreateCardPaymentInfo(info);
                    CommandResult result = cbll.InsertRecordWithCheck(payment);
                    if (result.Result == ResultCode.Successful)
                    {
                        info.UpdateFlag = true;
                        lcbll.Update(info);
                        ShowCardPaymentOnRow(info, row.Index);
                        row.DefaultCellStyle.ForeColor = Color.Green;
                    }
                    else
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                    GridView.Refresh();
                    NotifyProgress(null);
                }
            NotifyProgress(this.progressBar1.Maximum);
            this.progressBar1.Visible = false;
        }
        #endregion

    }
}
