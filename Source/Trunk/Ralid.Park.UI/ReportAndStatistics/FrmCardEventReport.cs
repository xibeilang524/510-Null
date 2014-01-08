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

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCardEventReport : FrmReportBase
    {
        public FrmCardEventReport()
        {
            InitializeComponent();
            this.ItemSearching += ItemSearching_Handler;
        }

        #region 私有变量
        private int _EventIndex;
        private List<CardEventRecord> _CardEvents;
        #endregion

        private void FrmCardEventReport_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AppSettings.CurrentSetting.StandbyParkConnect))
            {
                this.gbDB.Visible = false;
                this.btnExport.Location = new Point(this.gbDB.Location.X, this.btnExport.Location.Y);
                this.btnSaveAs.Location = new Point(this.gbDB.Location.X, this.btnSaveAs.Location.Y);
                this.btnSearch.Location = new Point(this.gbDB.Location.X, this.btnSearch.Location.Y);
            }

            this.ucDateTimeInterval1.Init();
            this.ucEntrance1.Init();
            this.comCardType.Init();
            this.comOperator.Init();
            this.carTypeComboBox1.Init();
        }

        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            CardEventSearchCondition con = new CardEventSearchCondition();
            con.RecordDateTimeRange = new DateTimeRange();
            con.RecordDateTimeRange.Begin = this.ucDateTimeInterval1.StartDateTime;
            con.RecordDateTimeRange.End = this.ucDateTimeInterval1.EndDateTime;
            con.CardType = this.comCardType.SelectedCardType;
            con.CardCertificate = this.txtCertificate.Text;
            if (carTypeComboBox1.SelectedIndex > 0) con.CarType = this.carTypeComboBox1.SelectedCarType;
            con.OwnerName = txtOwnerName.Text;
            con.CardID = this.txtCardID.Text.Trim();
            OperatorInfo opt = this.comOperator.SelectecOperator;
            if (opt != null)
            {
                con.Operator = opt;
            }
            con.Source = this.ucEntrance1.SelectedEntrances;
            con.CarPlate = this.txtCarPlate.Text;

            CardEventBll bll = null;
            if (this.rdbMaster.Checked)
            {
                bll = new CardEventBll(AppSettings.CurrentSetting.ParkConnect);
            }
            else
            {
                bll = new CardEventBll(AppSettings.CurrentSetting.CurrentStandbyConnect); 
            }
            QueryResultList<CardEventRecord> result = bll.GetCardEvents(con);
            if (result.Result == ResultCode.Successful)
            {
                ShowReportsOnGrid(result.QueryObjects);
            }
            else
            {
                MessageBox.Show(result.Message);
            }            
        }

        private void ShowReportsOnGrid(List<CardEventRecord> items)
        {
            GridView.Rows.Clear();

            _CardEvents = (from CardEventRecord cr in items
                           orderby cr.EventDateTime descending
                           select cr).ToList();
            foreach (CardEventRecord record in _CardEvents)
            {
                int index = GridView.Rows.Add();
                DataGridViewRow row = GridView.Rows[index];
                row.Tag = record;
                row.Cells["colCardID"].Value = record.CardID;
                row.Cells["colOwnerName"].Value = record.OwnerName;
                row.Cells["colCardCertificate"].Value = record.CardCertificate;
                row.Cells["colCardType"].Value = record.CardType.ToString();
                row.Cells["colCarType"].Value = CarTypeSetting.Current != null ? CarTypeSetting.Current.GetDescription(record.CarType) : string.Empty;
                row.Cells["colEventDateTime"].Value = record.EventDateTime;
                row.Cells["colEntranceName"].Value = record.EntranceName;
                if (record.IsExitEvent && record.LastDateTime != null)
                {
                    row.Cells["colLastDateTime"].Value = record.LastDateTime.Value;
                }
                row.Cells["colEventType"].Value = record.IsExitEvent ? Resources.Resource1.FrmCardEventReport_Out : Resources.Resource1.FrmCardEventReport_In;
                row.Cells["colCarPlate"].Value = record.CarPlate;
                row.Cells["colOperatorID"].Value = record.OperatorID;
            }
        }

        private void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                _EventIndex = e.RowIndex;
                CardEventRecord record = this.GridView.Rows[e.RowIndex].Tag as CardEventRecord;
                FrmSnapShotViewer frm = new FrmSnapShotViewer();
                frm.PreRecord += new RecordPositionEventHandler(frm_PreRecord);
                frm.NextRecord += new RecordPositionEventHandler(frm_NextRecord);
                frm.ConnectStandby = this.rdbStandby.Checked;
                if (record.IsExitEvent && record.LastDateTime != null)
                {
                    frm.ShowImage(record.EventDateTime, record.LastDateTime.Value, record.CardID);
                }
                else
                {
                    frm.ShowImage(record.EventDateTime, record.CardID);
                }
                frm.ShowDialog();
                frm.PreRecord -= new RecordPositionEventHandler(frm_PreRecord);
                frm.NextRecord -= new RecordPositionEventHandler(frm_NextRecord);
            }
        }

        void frm_NextRecord(object sender, RecordPositionEventArgs e)
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
                CardEventRecord record = this.GridView.Rows[_EventIndex].Tag as CardEventRecord;
                FrmSnapShotViewer frm = sender as FrmSnapShotViewer;
                if (record.IsExitEvent && record.LastDateTime != null)
                {
                    frm.ShowImage(record.EventDateTime, record.LastDateTime.Value, record.CardID);
                }
                else
                {
                    frm.ShowImage(record.EventDateTime, record.CardID);
                }
            }
            e.IsButtonRecord = (_EventIndex == GridView.Rows.Count - 1);
        }

        void frm_PreRecord(object sender, RecordPositionEventArgs e)
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
                CardEventRecord record = this.GridView.Rows[_EventIndex].Tag as CardEventRecord;
                FrmSnapShotViewer frm = sender as FrmSnapShotViewer;
                if (record.IsExitEvent && record.LastDateTime != null)
                {
                    frm.ShowImage(record.EventDateTime, record.LastDateTime.Value, record.CardID);
                }
                else
                {
                    frm.ShowImage(record.EventDateTime, record.CardID);
                }
            }
            e.IsTopRecord = (_EventIndex == 0);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (_CardEvents != null && _CardEvents.Count > 0)
                {
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        FrmSnapshotExporter frm = new FrmSnapshotExporter();
                        frm.CardEvents = _CardEvents;
                        frm.ExportFolder = folderBrowserDialog1.SelectedPath;
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
