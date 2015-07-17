using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BLL;

namespace Ralid.Park.UI
{
    public partial class FrmSpeedingDetail : Form
    {
        #region 构造函数
        public FrmSpeedingDetail()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private CardInvalidEventReport _ProcessingEvent;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置要处理的违章卡片ID
        /// </summary>
        public CardInvalidEventReport ProcessingEvent
        {
            get
            {
                return _ProcessingEvent;
            }
            set
            {
                _ProcessingEvent = value;
                if (value != null)
                {
                    ShowSpeedingDetail(value);
                }
                else
                {
                    ClearDetail();
                }
            }
        }
        #endregion

        #region 私有方法
        private void ClearDetail()
        {
            this.lblCardID.Text = string.Empty;
            this.lblCarPlate.Text = string.Empty;
            this.lblOwner.Text = string.Empty;
            this.lblDepartment.Text = string.Empty;
            this.pbPhoto.ImageLocation = string.Empty;
            this.dgvSpeeding.Rows.Clear();
            
            this.ucVideoes.Clear();
        }

        private void ShowSpeedingDetail(CardInvalidEventReport info)
        {
            ClearDetail();

            CardBll cbll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
            CardInfo card = cbll.GetCardByID(info.CardID).QueryObject;
            if (card == null) return;//没找到卡片，返回

            this.lblCardID.Text = card.CardID;
            this.lblCarPlate.Text = card.CarPlate;
            this.lblOwner.Text = card.OwnerName;
            this.lblDepartment.Text = card.Department;

            if (!string.IsNullOrEmpty(card.CarPlate))
            {
                SpeedingRecordBll sbll = new SpeedingRecordBll(AppSettings.CurrentSetting.ParkConnect);
                List<SpeedingRecord> records = sbll.GetRecordsByCarPlate(card.CarPlate).QueryObjects;
                ShowSpeedingRecords(records);
            }
            
            ucVideoes.ShowVideoes(ParkBuffer.Current.GetEntrance(info.EntranceID).VideoSources);
        }

        private void ShowSpeedingRecords(List<SpeedingRecord> records)
        {
            if (records != null && records.Count > 0)
            {
                foreach (SpeedingRecord record in records)
                {
                    int row = this.dgvSpeeding.Rows.Add();
                    ShowSpeedingRecordOnGrid(this.dgvSpeeding.Rows[row], record);
                }
                //this.pbPhoto.ImageLocation = records[0].Photo;
                this.pbPhoto.LoadAsync(records[0].Photo);
            }
        }

        private void ShowSpeedingRecordOnGrid(DataGridViewRow row, SpeedingRecord record)
        {
            row.Tag = record;
            row.Cells["colSpeedingDateTime"].Value = record.SpeedingDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            row.Cells["colSpeed"].Value = record.Speed;
            row.Cells["colPlace"].Value = record.Place;
            row.Cells["colMemo"].Value = record.Memo;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 清除超速违章详细信息并隐藏窗口
        /// </summary>
        public void CloseSpeedingDetail()
        {
            btnClose_Click(this.btnClose, EventArgs.Empty);
        }
        #endregion


        #region 事件处理

        private void btnClose_Click(object sender, EventArgs e)
        {
            ClearDetail();
            _ProcessingEvent = null;
            this.ucVideoes.Clear();
            this.Hide();
        }
        private void FrmSpeedingDetail_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F12:
                    if (this.btnClose.Enabled)
                    {
                        btnClose_Click(this.btnClose, EventArgs.Empty);
                    }
                    break;
            }
        }
        private void FrmSpeedingDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
        private void dgvSpeeding_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dgvSpeeding.Rows[e.RowIndex];
            if (row != null)
            {
                SpeedingRecord record = row.Tag as SpeedingRecord;
                if (record != null)
                {
                    //this.pbPhoto.ImageLocation = record.Photo;
                    this.pbPhoto.LoadAsync(record.Photo);
                }
            }
        }
        #endregion



    }
}
