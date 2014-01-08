using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCardChangeKey : Form
    {
        #region 构造函数
        public FrmCardChangeKey()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private Timer _ChangeKeyTimer = new Timer();//修改卡片密钥定时器
        private byte[] _OldKey;
        private string _CurrentCardID = string.Empty;
        #endregion

        #region 私有方法
        private bool Check()
        {
            if (rdbInputKey.Checked && this.txtOldKey.HexValue.Length != 6)
            {
                MessageBox.Show(Resources.Resource1.FrmCardChangeKey_InputOldKey);
                this.txtOldKey.Focus();
                return false;
            }
            return true;
        }

        private void ChangeKeyTimerHandler(object sender, EventArgs e)
        {
            if (_OldKey != null)
            {
                ReadCardResult result = CardReaderManager.GetInstance(UserSetting.Current.WegenType).ReadSection(null, (int)ICCardSection.Parking, 0, 3, _OldKey, false, false, false);
                if (!string.IsNullOrEmpty(result.CardID) && _CurrentCardID != result.CardID)
                {
                    if (result.ResultCode == CardOperationResultCode.Success)
                    {
                        CardOperationResultCode changeResult = CardReaderManager.GetInstance(UserSetting.Current.WegenType).SetSectionKey(result.CardID, (int)ICCardSection.Parking, _OldKey, GlobalVariables.ParkingKey, true, true);
                        AddCardRow(result.CardID, result.ParkingDate, changeResult == CardOperationResultCode.Success ? Color.Green : Color.Red);
                    }
                    else
                    {
                        CardReaderManager.GetInstance(UserSetting.Current.WegenType).FailBuz();
                        AddCardRow(result.CardID, result.ParkingDate, Color.Red);
                    }
                }
                _CurrentCardID = result.CardID;
            }
        }

        private void AddCardRow(string cardID,byte[] parkingBytes, Color color)
        {
            DataGridViewRow selectrow = null;
            CardInfo card = CardDateResolver.Instance.GetCardInfoFromData(cardID, parkingBytes);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string cardid = string.Empty;
                if (row.Tag is string)
                {
                    cardid = row.Tag as string;
                }
                else
                {
                    CardInfo into = row.Tag as CardInfo;
                    if (into != null) cardid = into.CardID;
                }
                if (cardid == cardID)
                {
                    SelectSingleRow(row);
                    selectrow = row;
                    break;
                }
            }

            if (selectrow == null)
            {
                int addrow = dataGridView1.Rows.Add();
                selectrow = dataGridView1.Rows[addrow];
            }
            if (card != null)
            {
                selectrow.Tag = card;
                selectrow.Cells["colCardID"].Value = card.CardID;
                selectrow.Cells["colCardType"].Value = card.CardType.Name;
                selectrow.Cells["colCarPlate"].Value = card.CarPlate;
                selectrow.Cells["colParkingStatus"].Value = ParkingStatusDescription.GetDescription(card.ParkingStatus);
            }
            else
            {
                selectrow.Tag = cardID;
                selectrow.Cells["colCardID"].Value = cardID;
            }

            this.dataGridView1.FirstDisplayedScrollingRowIndex = selectrow.Index;
            selectrow.DefaultCellStyle.ForeColor = color;
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


        #region 窗体事件

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                _OldKey = rdbInputKey.Checked ? this.txtOldKey.HexValue : GlobalVariables.DefaultParkingKey;
                if (this._ChangeKeyTimer.Enabled)
                {
                    this.btnOK.Text = Resources.Resource1.FrmCardChangeKey_Start;
                    this.groupBox2.Enabled = true;
                    this._ChangeKeyTimer.Enabled = false;
                }
                else
                {
                    this.btnOK.Text = Resources.Resource1.FrmCardChangeKey_Stop;
                    this.groupBox2.Enabled = false;
                    this._ChangeKeyTimer.Enabled = true;
                }

            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCardChangeKey_Load(object sender, EventArgs e)
        {
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).StopReadCard();
            this._ChangeKeyTimer.Interval = 1000;
            this._ChangeKeyTimer.Enabled = false;
            this._ChangeKeyTimer.Tick += ChangeKeyTimerHandler;
        }
        private void FrmCardChangeKey_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._ChangeKeyTimer.Enabled = false;
            this._ChangeKeyTimer.Dispose();
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).BeginReadCard();
        }

        private void rdbInputKey_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Enabled = this.rdbInputKey.Checked;
        }

        private void chkShowOld_CheckedChanged(object sender, EventArgs e)
        {
            this.txtOldKey.PasswordChar = this.chkShowOld.Checked ? char.MinValue : '*';
        }
        #endregion







    }
}
