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
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;
using Ralid.Park.UserControls;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCardPaymentReport : FrmReportBase
    {
        public FrmCardPaymentReport()
        {
            InitializeComponent();
            this.ItemSearching += ItemSearching_Handler;
        }

        #region 私有变量
        private int _EventIndex;
        private CardEventBll _CardEventBll = new CardEventBll(AppSettings.CurrentSetting.ParkConnect);
        #endregion

        #region 事件处理方法
        private void FrmParkingPayDetailReport_Load(object sender, EventArgs e)
        {
            this.UCChargeDateTime.Init();
            this.UCEnterDateTime.Init();
            if (UserSetting.Current.EnableForceShifting && UserSetting.Current.ForceShiftingTime != null)
            {
                TimeEntity te = UserSetting.Current.ForceShiftingTime;
                if ((DateTime.Now.Hour > te.Hour) ||
                    (DateTime.Now.Hour == te.Hour && DateTime.Now.Minute >= te.Minute))
                {
                    UCChargeDateTime.StartDateTime = DateTime.Today.AddHours(te.Hour).AddMinutes(te.Minute);
                    UCChargeDateTime.EndDateTime = DateTime.Today.AddDays(1).AddHours(te.Hour).AddMinutes(te.Minute).AddSeconds(-1);
                }
                else
                {
                    UCChargeDateTime.StartDateTime = DateTime.Today.AddDays(-1).AddHours(te.Hour).AddMinutes(te.Minute);
                    UCChargeDateTime.EndDateTime = DateTime.Today.AddHours(te.Hour).AddMinutes(te.Minute).AddSeconds(-1);
                }
            }
            this.comCardType.Init();
            this.comOperator.Init();
            this.comPaymentMode.Init();
            this.workStationCombobox1.Init();
            this.carTypeComboBox1.Init();
            this.comPaymentCode.Init();
            this.comOperatorDept.Init();
            this.comStationDept.Init();
        }

        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            CardPaymentRecordSearchCondition con = new CardPaymentRecordSearchCondition();
            if (this.chkChargeDateTime.Checked)
            {
                con.RecordDateTimeRange = new DateTimeRange();
                con.RecordDateTimeRange.Begin = this.UCChargeDateTime.StartDateTime;
                con.RecordDateTimeRange.End = this.UCChargeDateTime.EndDateTime;
            }
            if (this.chkEnterDateTime.Checked)
            {
                con.EnterDateTimeRange = new DateTimeRange();
                con.EnterDateTimeRange.Begin = this.UCEnterDateTime.StartDateTime;
                con.EnterDateTimeRange.End = this.UCEnterDateTime.EndDateTime;
            }
            con.CardType = this.comCardType.SelectedCardType;
            con.CardID = this.txtCardID.Text.Trim();
            con.CardCertificate = txtCertificate.Text.Trim();
            if (carTypeComboBox1.SelectedIndex > 0) con.CarType = carTypeComboBox1.SelectedCarType;
            if (rdSettled.Checked) con.IsUnSettled = false;
            if (rdUnsettled.Checked) con.IsUnSettled = true;
            if (chkCenterCharge.Checked) con.IsCenterCharge = true;
            if (chkPaymentMode.Checked)
            {
                con.PaymentMode = comPaymentMode.SelectedPaymentMode;
            }
            con.CarPlate  = this.txtCarPlate.Text.Trim();

            if (this.comOperator.SelectecOperator != null)
            {
                con.Operator = this.comOperator.SelectecOperator;
            }
            con.StationID = this.workStationCombobox1.Text.Trim();
            con.PaymentCode = this.comPaymentCode.SelectedPaymentCode;
            con.OperatorCardID = this.txtOperatorCardID.Text.Trim();
            con.StationIDs = this.txtWorkStations.Tag as List<string>;
            con.OperatorDeptID = this.comOperatorDept.SelectedDeptID;
            con.StationDeptID = this.comStationDept.SelectedDeptID;

            //if (!string.IsNullOrEmpty(this.comOperatorDept.SelectedDeptIDString))
            //{//操作员部门 
            //    OperatorBll opeBll = new OperatorBll(AppSettings.CurrentSetting.ParkConnect);
            //    OperatorSearchCondition opeCon = new OperatorSearchCondition();
            //    opeCon.DeptID = Guid.Parse(this.comOperatorDept.SelectedDeptIDString);
            //    List<OperatorInfo> operators = opeBll.GetOperators(opeCon).QueryObjects;
            //    List<string> lts_operators = new List<string>();
            //    if (operators.Count > 0)
            //    {
            //        foreach (OperatorInfo item in operators)
            //        {
            //            //lts_operators.Add(item.OperatorID);
            //            lts_operators.Add(item.OperatorName);
            //        }
            //    }
            //    else
            //    {
            //        lts_operators.Add("此部门没有操作员");
            //    }
            //    con.OperatorIDs = lts_operators;
            //}
            //if (!string.IsNullOrEmpty(this.comStationDept.SelectedDeptIDString))
            //{//工作站部门 
            //    WorkstationBll workBll = new WorkstationBll(AppSettings.CurrentSetting.ParkConnect);
            //    WorkstationSearchCondition workCon = new WorkstationSearchCondition();
            //    workCon.DeptID = Guid.Parse(this.comStationDept.SelectedDeptIDString);
            //    List<WorkStationInfo> stations = workBll.GetWorkstations(workCon).QueryObjects;
            //    List<string> lts_stations = new List<string>();
            //    if (stations.Count > 0)
            //    {
            //        foreach (WorkStationInfo item in stations)
            //        {
            //            lts_stations.Add(item.StationName);
            //        }
            //    }
            //    else
            //    {
            //        lts_stations.Add("此部门没有工作站");
            //    }
            //    con.StationIDs = lts_stations;
            //}

            CardPaymentRecordBll bll = new CardPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect); 
            QueryResultList<CardPaymentInfo> result = bll.GetItems(con);
            if (result.Result == ResultCode.Successful)
            {
                ShowReportsOnGrid(result.QueryObjects);
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void ShowReportsOnGrid(List<CardPaymentInfo> items)
        {
            decimal paid = 0;
            decimal discount = 0;
            decimal cash = 0;
            decimal pos = 0;
            decimal other = 0;
            this.GridView.Rows.Clear();
            items = (from CardPaymentInfo cr in items
                     orderby cr.ChargeDateTime descending
                     select cr).ToList();
            foreach (CardPaymentInfo info in items)
            {
                if (info.Paid == 0 && this.rdPaid.Checked) continue; //只查询收费记录时排除掉免费记录
                if (info.Paid > 0 && this.rdFree.Checked) continue;  //只查询免费记录时排除掉收费记录
                int row = GridView.Rows.Add();
                ShowCardPaymentOnRow(info,row);
                paid += info.Paid;
                discount += info.Discount;
                if (info.PaymentMode == PaymentMode.Cash)
                {
                    cash += info.Paid;
                }
                else if (info.PaymentMode == PaymentMode.Pos)
                {
                    pos += info.Paid;
                }
                else
                {
                    other += info.Paid;
                }
            }
            this.txtTotal.Text = (paid + discount).ToString("N");
            this.txtPaid.Text = paid.ToString("N");
            this.txtAccounts.Text = discount.ToString("N");
            this.txtCash.Text = cash.ToString("N");
            this.txtPOS.Text = pos.ToString("N");
            this.txtOther.Text = other.ToString("N");
        }

        private void ShowCardPaymentOnRow(CardPaymentInfo info, int row)
        {
            GridView.Rows[row].Tag = info;
            GridView.Rows[row].Cells["colCardID"].Value = info.CardID;
            GridView.Rows[row].Cells["colCardCertificate"].Value = info.CardCertificate;
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
            GridView.Rows[row].Cells["colCurDiscountHour"].Value = info.CurrDiscountHour.HasValue ? info.CurrDiscountHour.Value.ToString() : string.Empty;
            GridView.Rows[row].Cells["colHandled"].Value = info.SettleDateTime != null;
            GridView.Rows[row].Cells["colSettleDateTime"].Value = info.SettleDateTime;
            GridView.Rows[row].Cells["colTotalAccounts"].Value = info.Accounts;
            GridView.Rows[row].Cells["colTotalPaid"].Value = info.TotalPaid;
            GridView.Rows[row].Cells["colTotalDiscount"].Value = info.TotalDiscount;
            GridView.Rows[row].Cells["colDiscountHour"].Value = info.DiscountHour;
            GridView.Rows[row].Cells["colStation"].Value = info.StationID;
            GridView.Rows[row].Cells["colMemo"].Value = info.Memo;
            GridView.Rows[row].Cells["colPaymentCode"].Value = Ralid.Park.BusinessModel.Resouce.PaymentCodeDescription.GetDescription(info.PaymentCode);
            GridView.Rows[row].Cells["colOperatorCardID"].Value = info.OperatorCardID;
            GridView.Rows[row].DefaultCellStyle.ForeColor = info.Discount > 0 ? Color.Red : Color.Black;
        }

        private void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex >= 0)
                {
                    _EventIndex = e.RowIndex;
                    CardPaymentInfo info = this.GridView.Rows[e.RowIndex].Tag as CardPaymentInfo;
                    if (info.EnterDateTime.HasValue)
                    {
                        FrmSnapShotViewer frm = new FrmSnapShotViewer();
                        frm.PreRecord += new RecordPositionEventHandler(frm_PreRecord);
                        frm.NextRecord += new RecordPositionEventHandler(frm_NextRecord);
                        frm.ShowImage(info.ChargeDateTime, info.EnterDateTime.Value, info.CardID);
                        frm.ShowDialog();
                        frm.PreRecord -= new RecordPositionEventHandler(frm_PreRecord);
                        frm.NextRecord -= new RecordPositionEventHandler(frm_NextRecord);
                    }
                }
                else if (OperatorInfo.CurrentOperator.Permit(Permission.ModifyCardPaymentRecord))
                {
                    CardPaymentInfo info = customDataGridView1.Rows[e.RowIndex].Tag as CardPaymentInfo;
                    if (info != null && info.SettleDateTime == null)
                    {
                        FrmCardPaymentRecordDetail frm = new FrmCardPaymentRecordDetail();
                        frm.CardPaymentRecord = info;
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            ShowCardPaymentOnRow(info, e.RowIndex);
                        }
                    }
                }
            }
        }

        private void frm_NextRecord(object sender, RecordPositionEventArgs e)
        {
            if (_EventIndex < customDataGridView1.Rows.Count - 1)
            {
                this.customDataGridView1.Rows[_EventIndex].Selected = false;
                _EventIndex++;
                this.customDataGridView1.Rows[_EventIndex].Selected = true;
                if (_EventIndex > this.customDataGridView1.FirstDisplayedScrollingRowIndex + this.customDataGridView1.DisplayedRowCount(false) - 1)
                {
                    this.customDataGridView1.FirstDisplayedScrollingRowIndex += this.customDataGridView1.DisplayedRowCount(false);
                }
                CardPaymentInfo info = this.GridView.Rows[_EventIndex].Tag as CardPaymentInfo;
                FrmSnapShotViewer frm = sender as FrmSnapShotViewer;
                frm.ShowImage(info.ChargeDateTime, info.EnterDateTime.Value, info.CardID);
            }
            e.IsButtonRecord = (_EventIndex == GridView.Rows.Count - 1);
        }

        private void frm_PreRecord(object sender, RecordPositionEventArgs e)
        {
            if (_EventIndex > 0)
            {
                this.customDataGridView1.Rows[_EventIndex].Selected = false;
                _EventIndex--;
                this.customDataGridView1.Rows[_EventIndex].Selected = true;
                if (_EventIndex < this.customDataGridView1.FirstDisplayedScrollingRowIndex)
                {
                    if (this.customDataGridView1.FirstDisplayedScrollingRowIndex >= this.customDataGridView1.DisplayedRowCount(false))
                    {
                        this.customDataGridView1.FirstDisplayedScrollingRowIndex -= this.customDataGridView1.DisplayedRowCount(false);
                    }
                    else
                    {
                        this.customDataGridView1.FirstDisplayedScrollingRowIndex = 0;
                    }
                }
                CardPaymentInfo info = this.GridView.Rows[_EventIndex].Tag as CardPaymentInfo;
                FrmSnapShotViewer frm = sender as FrmSnapShotViewer;
                frm.ShowImage(info.ChargeDateTime, info.EnterDateTime.Value, info.CardID);
            }
            e.IsTopRecord = (_EventIndex == 0);
        }
        private void customDataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //停车时长按停车的时间长度排序，不是按字符串排序
            if (e.Column.Name == "colTimeSpan")
            {
                CardPaymentInfo info1 = this.customDataGridView1.Rows[e.RowIndex1].Tag as CardPaymentInfo;
                CardPaymentInfo info2 = this.customDataGridView1.Rows[e.RowIndex2].Tag as CardPaymentInfo;
                if (info1 != null
                    && info2 != null
                    && info1.EnterDateTime.HasValue
                    && info2.EnterDateTime.HasValue)
                {
                    TimeSpan ts1 = new TimeSpan(info1.ChargeDateTime.Ticks - info1.EnterDateTime.Value.Ticks);
                    TimeSpan ts2 = new TimeSpan(info2.ChargeDateTime.Ticks - info2.EnterDateTime.Value.Ticks);
                    e.SortResult = ts1.Ticks > ts2.Ticks ? 1 : ts1.Ticks < ts2.Ticks ? -1 : 0;
                    e.Handled = true;
                }
            }
        }
        private void chkChargeDateTime_CheckedChanged(object sender, EventArgs e)
        {
            this.UCChargeDateTime.Enabled = this.chkChargeDateTime.Checked;
        }

        private void chkEnterDateTime_CheckedChanged(object sender, EventArgs e)
        {
            this.UCEnterDateTime.Enabled = this.chkEnterDateTime.Checked;
        }
        private void lklWorkStations_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmWorkstationsSelection frm = new FrmWorkstationsSelection();
            frm.SelectionItems = this.txtWorkStations.Tag as List<string>;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtWorkStations.Tag = frm.SelectionItems;
                this.txtWorkStations.Text = string.Join(";", frm.SelectionItems);
            }
        }
        #endregion



    }
}
