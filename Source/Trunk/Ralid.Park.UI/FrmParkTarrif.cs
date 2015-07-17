using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.ParkAdapter;

namespace Ralid.Park.UI
{
    public partial class FrmParkTarrif : Form
    {
        public int ParkID { get; set; }
        public string ParkName { get; set; }

        public FrmParkTarrif()
        {
            InitializeComponent();
        }

        private void FrmParkTarrif_Load(object sender, EventArgs e)
        {
            this.Text = ParkName + "停车场单独费率设置";

            this.btnOK.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.EditSysSetting);
            this.btnDownLoad.Enabled = OperatorInfo.CurrentOperator.Permit(Permission.EditSysSetting);

            TariffSetting ts = TariffSetting.Current;
            if (ts == null
                || ts.ParkTariffDictionary == null
                || !ts.ParkTariffDictionary.ContainsKey(ParkID)
                || ts.ParkTariffDictionary[ParkID] == null
                || ts.ParkTariffDictionary[ParkID].Count == 0)
            {
                this.lblHadSet.Visible = true;
                this.lblHadSet.ForeColor = Color.Blue;
                this.lblHadSet.Text = "未设置单独费率";
            }
            //if (ts.ParkTariffDictionary == null)
            //    ts.ParkTariffDictionary = new Dictionary<int, List<TariffBase>>();
            //if (!ts.ParkTariffDictionary.ContainsKey(ParkID))
            //{
            //    //ts.ParkTariffDictionary.Add(ParkID, new List<TariffBase>());
            //    this.lblHadSet.ForeColor = Color.Red;
            //    this.lblHadSet.Text = "未设置单独费率";
            //}
            //else
            //{
            //    this.lblHadSet.ForeColor = Color.Blue;
            //    this.lblHadSet.Text = "已设置单独费率";
            //}
            ShowTariffSetting(ts);
        }

        private void ShowTariffSetting(TariffSetting ts)
        {
            tariffGrid.Columns["colGeneral"].Tag = TariffType.Normal;
            tariffGrid.Columns["colHoliday"].Tag = TariffType.Holiday;
            tariffGrid.Columns["colInnerRoom"].Tag = TariffType.InnerRoom;
            tariffGrid.Columns["colHolidayAndInnerRoom"].Tag = TariffType.HolidayAndInnerRoom;

            List<CardType> cardTtypes = CardType.GetBaseCardTypes();
            if (CustomCardTypeSetting.Current != null && CustomCardTypeSetting.Current.CardTypes != null)
            {
                cardTtypes.AddRange(CustomCardTypeSetting.Current.CardTypes);
            }
            cardTtypes.Remove(CardType.Ticket);//纸票与临时卡使用同一种费率，所以这里就不在设置纸票的费率了
            foreach (CardType cardType in cardTtypes)
            {
                foreach (CarType carType in CarTypeSetting.Current.CarTypes)
                {
                    int row = tariffGrid.Rows.Add();
                    InitTariffGridRow(tariffGrid.Rows[row], cardType, carType, ts);
                }
            }
        }

        private void InitTariffGridRow(DataGridViewRow row, CardType cardType, CarType carType, TariffSetting ts)
        {
            TariffBase tariff = null;

            row.Cells["colCardType"].Value = cardType.Name;
            row.Cells["colCardType"].Tag = cardType.ID;

            row.Cells["colCarType"].Value = carType.Description;
            row.Cells["colCarType"].Tag = carType.ID;

            tariff = ts.GetAloneTariff(ParkID, cardType.ID, carType.ID, TariffType.Normal);
            row.Cells["colGeneral"].Value = (tariff != null) ? tariff.ToString() : "N/A";
            row.Cells["colGeneral"].Tag = (tariff != null) ? tariff : null;

            tariff = ts.GetAloneTariff(ParkID, cardType.ID, carType.ID, TariffType.Holiday);
            row.Cells["colHoliday"].Value = (tariff != null) ? tariff.ToString() : "N/A";
            row.Cells["colHoliday"].Tag = (tariff != null) ? tariff : null;

            tariff = ts.GetAloneTariff(ParkID, cardType.ID, carType.ID, TariffType.InnerRoom);
            row.Cells["colInnerRoom"].Value = (tariff != null) ? tariff.ToString() : "N/A";
            row.Cells["colInnerRoom"].Tag = (tariff != null) ? tariff : null;

            tariff = ts.GetAloneTariff(ParkID, cardType.ID, carType.ID, TariffType.HolidayAndInnerRoom);
            row.Cells["colHolidayAndInnerRoom"].Value = (tariff != null) ? tariff.ToString() : "N/A";
            row.Cells["colHolidayAndInnerRoom"].Tag = (tariff != null) ? tariff : null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (CheckTariffInput())
            {
                SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect);
                TariffSetting.Current = GetTariffSettingFromInput();
                //TariffSetting.Current.TariffOption = GetTollOptionFromInput();
                ssb.SaveSetting<TariffSetting>(TariffSetting.Current);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool CheckTariffInput()
        {
            bool result = true;
            TariffBase defaultTariff = null;
            foreach (DataGridViewRow row in this.tariffGrid.Rows)
            {
                defaultTariff = null;

                TariffBase tempTariff = row.Cells["colGeneral"].Tag as TariffBase;
                if (tempTariff != null) defaultTariff = tempTariff;

                tempTariff = row.Cells["colHoliday"].Tag as TariffBase;
                if (tempTariff != null)
                {
                    if (defaultTariff == null)
                    {
                        defaultTariff = tempTariff;
                    }
                    else if (tempTariff.GetType() != defaultTariff.GetType())
                    {
                        result = false;
                        row.Selected = true;
                        break;
                    }
                }

                tempTariff = row.Cells["colInnerRoom"].Tag as TariffBase;
                if (tempTariff != null)
                {
                    if (defaultTariff == null)
                    {
                        defaultTariff = tempTariff;
                    }
                    else if (tempTariff.GetType() != defaultTariff.GetType())
                    {
                        result = false;
                        row.Selected = true;
                        break;
                    }
                }

                tempTariff = row.Cells["colHolidayAndInnerRoom"].Tag as TariffBase;
                if (tempTariff != null)
                {
                    if (defaultTariff == null)
                    {
                        defaultTariff = tempTariff;
                    }
                    else if (tempTariff.GetType() != defaultTariff.GetType())
                    {
                        result = false;
                        row.Selected = true;
                        break;
                    }
                }
            }

            if (!result)
            {
                this.tariffGrid.Focus();
                MessageBox.Show(Resources.Resource1.FrmSystemOption_SameTariff);
            }
            return result;
        }

        private TariffSetting GetTariffSettingFromInput()
        {
            TariffSetting ts = TariffSetting.Current;

            if (ts.ParkTariffDictionary == null)
                ts.ParkTariffDictionary = new Dictionary<int, List<TariffBase>>();

            if (!ts.ParkTariffDictionary.ContainsKey(ParkID))
            {
                ts.ParkTariffDictionary.Add(ParkID, new List<TariffBase>());
            }
            else
            {
                ts.ParkTariffDictionary[ParkID] = new List<TariffBase>();
            }

            foreach (DataGridViewRow row in tariffGrid.Rows)
            {
                for (int i = 1; i < tariffGrid.Columns.Count; i++)
                {
                    TariffBase tariff = row.Cells[i].Tag as TariffBase;
                    if (tariff != null)
                    {
                        ts.ParkTariffDictionary[ParkID].Add(tariff);
                    }
                }
            }

            return ts;
        }

        private TollOptionSetting GetTollOptionFromInput()
        {
            TollOptionSetting tos = new TollOptionSetting();
            //tos.FreeTimeAfterPay = this.txtFreeTimeAfterPay.IntergerValue;
            //tos.PointCount = (byte)(rdYuan.Checked ? 0 : 1);
            return tos;
        }

        private void tariffGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 1)
            {
                FrmTariffSelection frm = new FrmTariffSelection();
                if (tariffGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag != null)
                {
                    frm.SelectedTariff = tariffGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as TariffBase;
                }
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    TariffBase info = frm.SelectedTariff;
                    info.CardType = Convert.ToByte(tariffGrid.Rows[e.RowIndex].Cells["colCardType"].Tag);
                    info.CarType = Convert.ToByte(tariffGrid.Rows[e.RowIndex].Cells["colCarType"].Tag);
                    if (tariffGrid.Columns[e.ColumnIndex].Tag is TariffType)
                    {
                        info.TariffType = (TariffType)tariffGrid.Columns[e.ColumnIndex].Tag;
                    }
                    tariffGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = info.ToString();
                    tariffGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = info;
                }
            }
        }

        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            FrmDownLoadBase frm = null;
            SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect);

            if (CheckTariffInput())
            {
                TariffSetting.Current = GetTariffSettingFromInput();
                ssb.SaveSetting<TariffSetting>(TariffSetting.Current);
                foreach (IParkingAdapter ad in ParkingAdapterManager.Instance.ParkAdapters)
                {
                    ad.DownloadTariffSetting(TariffSetting.Current);
                }

                frm = new FrmDownLoadTariffSetting();
            }
            

            if (frm != null)
            {
                frm.ParkID = ParkID;
                frm.ShowDialog();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnu_Clear_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in tariffGrid.SelectedCells)
            {
                if (cell.Tag != null && cell.Tag is TariffBase)
                {
                    cell.Tag = null;
                    cell.Value = "N/A";
                }
            }
        }

        private void mnu_ClearAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in tariffGrid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Tag != null && cell.Tag is TariffBase)
                    {
                        cell.Tag = null;
                        cell.Value = "N/A";
                    }
                }
            }
        }

    }
}
