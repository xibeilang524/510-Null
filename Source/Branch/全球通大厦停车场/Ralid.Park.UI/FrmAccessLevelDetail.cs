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
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.UI.Resources;

namespace Ralid.Park.UI
{
    public partial class FrmAccessLevelDetail : Form
    {
        public FrmAccessLevelDetail()
        {
            InitializeComponent();
        }

        public AccessInfo UpdatingItem{get;set;}
        public event EventHandler<ItemUpdatedEventArgs> ItemUpdated;
        public event EventHandler<ItemAddedEventArgs> ItemAdded;
        public event GetAccessesHandler GetAccesses;

        #region 私有方法
        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                MessageBox.Show(Resource1.FrmAccessLevelDetail_InvalidAccessName);
                return false;
            }
            return true;
        }

        private void InitControls()
        {
            this.btnOk.Enabled = OperatorInfo.CurrentOperator.Role.Permit(Permission.EditSysSetting);
            this.btnAdd.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.EditSysSetting);
            this.btnUpdate.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.EditSysSetting);
            this.btnDelete.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.EditSysSetting);
        }

        private void ShowItem(AccessInfo info)
        {
            this.Text = info.Name;
            this.txtName.Text = info.Name;
            this.gridView.RowCount = 0;
            foreach (AccessTimeZone timezone in info.AccessTimeZones)
            {
                DataGridViewRow row = this.gridView.Rows[this.gridView.Rows.Add()];
                ShowItemOnGrid(row,timezone);
            }
        }

        private AccessInfo GetAccessInfoFromInput()
        {
            AccessInfo access = null;
            if (UpdatingItem == null)
            {
                access = new AccessInfo();
                access.AccessTimeZones = new List<AccessTimeZone>();
            }
            else
            {
                access = UpdatingItem;
            }
            access.Name = txtName.Text;
            access.AccessTimeZones.Clear();
            foreach (DataGridViewRow row in this.gridView.Rows)
            {
                access.AccessTimeZones.Add(row.Tag as AccessTimeZone);
            }
            return access;
        }
        #endregion

        #region 私有事件
        private List<AccessInfo> GetAccessesHandler(object sender, EntranceEventArgs e)
        {
            if (GetAccesses != null)
            {
                List<AccessInfo> accesses = GetAccesses(sender, e);
                if (UpdatingItem != null && accesses.Any(item => item == UpdatingItem))
                {
                    accesses.Remove(UpdatingItem);
                }
                return accesses;
            }
            return new List<AccessInfo>();
        }

        private List<AccessTimeZone> GetAccessTimeZonesHandler(object sender, EntranceEventArgs e)
        {
            List<AccessTimeZone> accessTimeZones = new List<AccessTimeZone>();
            foreach (DataGridViewRow row in this.gridView.Rows)
            {
                AccessTimeZone accessTimeZone = row.Tag as AccessTimeZone;
                if (accessTimeZone != null)
                {
                    if (accessTimeZone.AccessEntrances.Any(item => item == e.EntranceID))
                    {
                        accessTimeZones.Add(accessTimeZone);
                    }
                }
            }
            return accessTimeZones;
        }
        #endregion


        private void ShowItemOnGrid(DataGridViewRow row, AccessTimeZone timezone)
        {
            row.Tag = timezone;
            row.Cells["colBeginTime"].Value = timezone.BeginTime.ToString();
            row.Cells["colEndTime"].Value = timezone.EndTime.ToString();
            string[] address = timezone.AccessEntrances.Select(id => id.ToString()).ToArray();
            row.Cells["colAccessGroup"].Value = string.Join(",", address.ToArray());
        }

        private void FrmAccessLevelDetail_Load(object sender, EventArgs e)
        {
            InitControls();
            if (UpdatingItem != null)
            {
                ShowItem(UpdatingItem);
            }
            else
            {
                this.Text = Resource1.Form_Add;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                AccessInfo access = GetAccessInfoFromInput();
                if (UpdatingItem != null)
                {
                    if (ItemUpdated != null)
                    {
                        ItemUpdated(this, new ItemUpdatedEventArgs(access));
                    }
                }
                else
                {
                    if (ItemAdded != null)
                    {
                        ItemAdded(this, new ItemAddedEventArgs(access));
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAccessGroup frm = new FrmAccessGroup();
            frm.GetAccesses += GetAccessesHandler;
            frm.GetAccessTimeZones += GetAccessTimeZonesHandler;
            frm.Left = this.Left + this.Width;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataGridViewRow row = this.gridView.Rows[this.gridView.Rows.Add()];
                ShowItemOnGrid(row, frm.TimeZone);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gridView.SelectedRows.Count == 1)
            {
                AccessTimeZone timezone = gridView.SelectedRows[0].Tag as AccessTimeZone;
                FrmAccessGroup frm = new FrmAccessGroup();
                frm.GetAccesses += GetAccessesHandler;
                frm.GetAccessTimeZones += GetAccessTimeZonesHandler;
                frm.Left = this.Left + this.Width;
                frm.TimeZone = timezone;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ShowItemOnGrid(gridView.SelectedRows[0], frm.TimeZone);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in gridView.SelectedRows)
            {
                gridView.Rows.Remove(row);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnUpdate.Enabled)
            {
                btnUpdate_Click(btnUpdate, EventArgs.Empty);
            }
        }
    }
}
