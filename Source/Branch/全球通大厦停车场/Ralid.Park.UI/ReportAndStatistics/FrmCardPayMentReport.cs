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
            this.ucDateTimeInterval1.Init();
            if (UserSetting.Current.EnableForceShifting && UserSetting.Current.ForceShiftingTime != null)
            {
                TimeEntity te = UserSetting.Current.ForceShiftingTime;
                if ((DateTime.Now.Hour > te.Hour) ||
                    (DateTime.Now.Hour == te.Hour && DateTime.Now.Minute >= te.Minute))
                {
                    ucDateTimeInterval1.StartDateTime = DateTime.Today.AddHours(te.Hour).AddMinutes(te.Minute);
                    ucDateTimeInterval1.EndDateTime = DateTime.Today.AddDays(1).AddHours(te.Hour).AddMinutes(te.Minute).AddSeconds(-1);
                }
                else
                {
                    ucDateTimeInterval1.StartDateTime = DateTime.Today.AddDays(-1).AddHours(te.Hour).AddMinutes(te.Minute);
                    ucDateTimeInterval1.EndDateTime = DateTime.Today.AddHours(te.Hour).AddMinutes(te.Minute).AddSeconds(-1);
                }
            }
            this.comCardType.Init();
            this.comOperator.Init();
            this.comPaymentMode.Init();
            this.workStationCombobox1.Init();
            this.carTypeComboBox1.Init();
            this.comPaymentCode.Init();
        }

        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            CardPaymentRecordSearchCondition con = new CardPaymentRecordSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;
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
            con.CarPlate = this.txtCarPlate.Text.Trim();

            if (this.comOperator.SelectecOperator != null)
            {
                con.Operator = this.comOperator.SelectecOperator;
            }
            con.StationID = this.workStationCombobox1.Text.Trim();
            con.PaymentCode = this.comPaymentCode.SelectedPaymentCode;
            con.OperatorCardID = this.txtOperatorCardID.Text.Trim(); ;

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
            this.GridView.Rows.Clear();
            items = (from CardPaymentInfo cr in items
                     orderby cr.ChargeDateTime descending
                     select cr).ToList();
            foreach (CardPaymentInfo info in items)
            {
                if (info.Paid == 0 && this.rdPaid.Checked) continue; //只查询收费记录时排除掉免费记录
                if (info.Paid > 0 && this.rdFree.Checked) continue;  //只查询免费记录时排除掉收费记录
                int row = GridView.Rows.Add();
                ShowCardPaymentOnRow(info, row);
                paid += info.Paid;
                discount += info.Discount;
            }
            this.txtTotal.Text = (paid + discount).ToString();
            this.txtPaid.Text = paid.ToString();
            this.txtAccounts.Text = discount.ToString();
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
            GridView.Rows[row].Cells["colHandled"].Value = info.SettleDateTime != null;
            GridView.Rows[row].Cells["colTotalAccounts"].Value = info.Accounts;
            GridView.Rows[row].Cells["colTotalPaid"].Value = info.TotalPaid;
            GridView.Rows[row].Cells["colTotalDiscount"].Value = info.TotalDiscount;
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
        #endregion
    }
}
