using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.UI
{
    public partial class FrmHolidayDetail : Form
    {
        public FrmHolidayDetail()
        {
            InitializeComponent();
        }

        public bool IsAdding { get; set; }
        public HolidayInfo UpdatingItem { get; set; }
        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;
        public event EventHandler<ItemAddedEventArgs> ItemAdded;

        #region 私有方法
        private bool CheckInput()
        {
            if (this.dtStartDate.Value.Date > this.dtEndDate.Value.Date)
            {
                MessageBox.Show(Resources.Resource1.FrmHolidayDetail_InvalidHoliday, Resources.Resource1.Form_Alert,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if ((checkBox1.Checked && (this.dtWorkday1StartDate.Value.Date > this.dtWorkday1EndDate.Value.Date))
                ||(checkBox2.Checked && (this.dtWorkday2StartDate.Value.Date > this.dtWorkday2EndDate.Value.Date)))
            {
                MessageBox.Show("周末调整为工作日的开始时间不能大于结束时间", Resources.Resource1.Form_Alert,
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private HolidayInfo GetItemFromInput()
        {
            HolidayInfo info;
            if (UpdatingItem == null)
            {
                info = new HolidayInfo();
            }
            else
            {
                info = UpdatingItem as HolidayInfo;
            }
            info.StartDate = this.dtStartDate.Value.Date;
            info.EndDate = this.dtEndDate.Value.Date;
            info.WeekenToWorkDayInterval.Clear();
            if (checkBox1.Checked)
            {
                DatetimeInterval di = new DatetimeInterval();
                di.Begin = dtWorkday1StartDate.Value;
                DateTime dt = dtWorkday1EndDate.Value;
                di.End = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                info.WeekenToWorkDayInterval.Add(di);
            }
            if (checkBox2.Checked)
            {
                DatetimeInterval di = new DatetimeInterval();
                di.Begin = dtWorkday2StartDate.Value;
                DateTime dt = dtWorkday2EndDate.Value;
                di.End = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                info.WeekenToWorkDayInterval.Add(di);
            }
            return info;
        }

        private void ShowHoliday(HolidayInfo info)
        {
            this.dtStartDate.Value = info.StartDate.Date;
            this.dtEndDate.Value = info.EndDate.Date;
            if (info.WeekenToWorkDayInterval != null)
            {
                if (info.WeekenToWorkDayInterval.Count > 0)
                { 
                    checkBox1.Checked = true;
                    dtWorkday1StartDate.Value = info.WeekenToWorkDayInterval[0].Begin;
                    dtWorkday1EndDate.Value = info.WeekenToWorkDayInterval[0].End;
                }
                if (info.WeekenToWorkDayInterval.Count > 1)
                {
                    checkBox2.Checked = true;
                    dtWorkday2StartDate.Value = info.WeekenToWorkDayInterval[1].Begin;
                    dtWorkday2EndDate.Value = info.WeekenToWorkDayInterval[1].End;
                }
            }
            
        }
        #endregion

        #region 事件处理程序
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                HolidayInfo info = GetItemFromInput();
                if (IsAdding && ItemAdded != null)
                {
                    ItemAdded(this, new ItemAddedEventArgs(info));
                }
                else if (ItemUpdated != null)
                {
                    ItemUpdated(this, new ItemUpdatedEventArgs(info));
                }
            }
        }

        private void FrmHolidayDetail_Load(object sender, EventArgs e)
        {
            if (IsAdding)
            {
                this.Text = Resources.Resource1.Form_Add;
            }
            else
            {
                ShowHoliday(UpdatingItem);
            }
            this.btnOk.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.EditSysSetting);
        }

       

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (object.ReferenceEquals(sender, checkBox1))
            {
                dtWorkday1StartDate.Enabled = checkBox1.Checked;
                dtWorkday1EndDate.Enabled = checkBox1.Checked;
            }
            else if (object.ReferenceEquals(sender, checkBox2))
            {
                dtWorkday2StartDate.Enabled = checkBox2.Checked;
                dtWorkday2EndDate.Enabled = checkBox2.Checked;
            }
        }
        #endregion
    }
}
