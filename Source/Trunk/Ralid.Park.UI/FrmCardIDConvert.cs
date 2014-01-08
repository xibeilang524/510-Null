using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCardIDConvert : Form
    {
        public FrmCardIDConvert()
        {
            InitializeComponent();
        }

        #region 私有变量
        private List<CardInfo> _Cards = null;
        #endregion
        
        #region 私有方法
        private bool ConvertCardID(string newID, string originalID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = AppSettings.CurrentSetting.ParkConnect;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string sql = "update card set cardid='" + newID + "' where cardid='" + originalID + "' " +
                                   "update CardPaymentRecord set cardid='" + newID + "' where cardid='" + originalID + "' " +
                                   "update CardEvent set cardid='" + newID + "' where cardid='" + originalID + "' " +
                                   "update CardCharge set cardid='" + newID + "' where cardid='" + originalID + "' " +
                                   "update CardDefer set cardid='" + newID + "' where cardid='" + originalID + "' " +
                                   "update CardRelease set cardid='" + newID + "' where cardid='" + originalID + "' " +
                                   "update CardLostRestore set cardid='" + newID + "' where cardid='" + originalID + "' " +
                                   "update CardDisableEnable set cardid='" + newID + "' where cardid='" + originalID + "' " +
                                   "update CardDeleteRecord set cardid='" + newID + "' where cardid='" + originalID + "' ";
                        cmd.Connection = con;
                        con.Open();
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return false;
            }
        }

        private void ReadCardHandler(object sender, CardReadEventArgs args)
        {
            Action action = delegate()
            {
                long cardid = 0;
                if (long.TryParse(args.CardID, out cardid))
                {
                    string wegen34 = args.CardID;
                    string wegen26 = (cardid & 0xFFFFFF).ToString();

                    foreach (DataGridViewRow row in this.dataGridView1.Rows)
                    {
                        CardInfo card = row.Tag as CardInfo;
                        if (rdTo34.Checked && card.CardID == wegen26)
                        {
                            if (ConvertCardID(wegen34, wegen26))
                            {
                                card.CardID = wegen34;
                                row.Cells["colNewCardID"].Value = wegen34;
                                this.dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                                row.DefaultCellStyle.ForeColor = Color.Green;
                                SelectSingleRow(row);
                            }
                        }
                        else if (rdTo26.Checked && card.CardID == wegen34)
                        {
                            if (ConvertCardID(wegen26, wegen34))
                            {
                                card.CardID = wegen26;
                                row.Cells["colNewCardID"].Value = wegen26;
                                this.dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                                row.DefaultCellStyle.ForeColor = Color.Green;
                                SelectSingleRow(row);
                            }
                        }
                    }
                }
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void SelectSingleRow(DataGridViewRow row)
        {
            foreach (DataGridViewRow r in this.dataGridView1.Rows)
            {
                if (r == row)
                {
                    r.Selected = true;
                }
                else
                {
                    r.Selected = false;
                }
            }
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            this.comCardType.Init();
        }

        private void btnFilterCard_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            int count = 0;
            CardSearchCondition con = new CardSearchCondition();
            con.CardType = comCardType.SelectedCardType;
            con.OwnerName = txtOwnerName.Text;
            con.CarPlate = txtCarPlate.Text;
            con.CardCertificate = txtCardCertificate.Text;
            _Cards = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCards(con).QueryObjects;
            foreach (CardInfo card in _Cards)
            {
                long cardID = 0;
                if (long.TryParse(card.CardID, out cardID))
                {
                    if ((rdTo34.Checked && cardID <= Math.Pow(2, 24)) ||  //转成韦根34时，则只显示韦根26的卡号
                        (rdTo26.Checked && cardID > Math.Pow(2, 24))      ////转成韦根26时，则只显示韦根34的卡号
                        )
                    {
                        int row = this.dataGridView1.Rows.Add();
                        this.dataGridView1.Rows[row].Tag = card;
                        this.dataGridView1.Rows[row].Cells["colCardID"].Value = card.CardID;
                        this.dataGridView1.Rows[row].Cells["colOwnerName"].Value = card.OwnerName;
                        this.dataGridView1.Rows[row].Cells["colCardType"].Value = card.CardType.Name;
                        this.dataGridView1.Rows[row].Cells["colCertificate"].Value = card.CardCertificate;
                        this.dataGridView1.Rows[row].Cells["colCarPlate"].Value = card.CarPlate;
                        this.dataGridView1.Rows[row].Cells["colParkingStatus"].Value = ParkingStatusDescription.GetDescription(card.ParkingStatus);
                        count++;
                    }
                }
            }
            this.lblCount.Text = string.Format(Resources.Resource1.FrmMasterBase_StatusBar, count);
        }

        private void btnFresh_Click(object sender, EventArgs e)
        {
            _Cards = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetAllCards().QueryObjects;
            btnFilterCard_Click(btnFilter, EventArgs.Empty);
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            CardReaderManager.GetInstance(WegenType.Wengen34).PushCardReadRequest(ReadCardHandler);
        }

        private void FrmCardIDConvert_FormClosed(object sender, FormClosedEventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).PopCardReadRequest(ReadCardHandler);
        }
    }
}
