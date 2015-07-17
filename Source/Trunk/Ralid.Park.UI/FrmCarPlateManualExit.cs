using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCarPlateManualExit : Form
    {
        public FrmCarPlateManualExit()
        {
            InitializeComponent();

            //this.Height = 200;
            //this.pnlSearch.Visible = false;
        }

        #region 私有变量
        private string _CardID;
        private List<string> _Plates;
        private bool _Advance;//高级查找
        #endregion

        #region 私有方法
        private List<string> GetAllCarplates()
        {
            List<string> plates = new List<string>();
            List<CardInfo> cards = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetAllCarPlateLists().QueryObjects;
            if (cards != null && cards.Count > 0)
            {
                foreach (CardInfo card in cards)
                {
                    if (!string.IsNullOrEmpty(card.CarPlate))
                    {
                        plates.Add(card.CarPlate);
                    }
                }
                if (plates.Count > 0)
                {
                    plates = plates.Distinct().ToList();
                    plates.Sort();
                }
            }
            return plates;
        }

        /// <summary>
        /// 在表格行上显示车牌名单
        /// </summary>
        /// <param name="row"></param>
        /// <param name="card"></param>
        private void ShowCarPlateListOnGrid(DataGridViewRow row, CardInfo card)
        {
            row.Tag = card;
            row.Cells["colCardID"].Value = card.CardID;
            row.Cells["colCarPlate"].Value = card.CarPlate;
            row.Cells["colEnterDateTime"].Value = card.LastDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 显示车牌名单
        /// </summary>
        /// <param name="cards"></param>
        private void ShowCarPlateLists(List<CardInfo> cards)
        {
            foreach (CardInfo card in cards)
            {
                //名单生效且在场时才添加
                if (card.Status == CardStatus.Enabled && card.IsInPark)
                {
                    //只显示无车牌车辆时，需判断车牌号是否为空
                    if (this.chkNoCarPlate.Checked)
                    {
                        if (!string.IsNullOrEmpty(card.CarPlate))
                        {
                            continue;
                        }
                    }
                    int row = this.dgvCarPlateLists.Rows.Add();
                    ShowCarPlateListOnGrid(this.dgvCarPlateLists.Rows[row], card);
                }
            }
            this.searchInfo.Text = string.Format(Resources.Resource1.FrmMasterBase_StatusBar, this.dgvCarPlateLists.Rows.Count);
            this.btnCardIDOK.Enabled = this.dgvCarPlateLists.Rows.Count > 0;
            ShowCarPlatListSnapShot(this.dgvCarPlateLists.CurrentRow);
        }

        /// <summary>
        /// 显示车牌名单的抓拍图片
        /// </summary>
        /// <param name="row"></param>
        private void ShowCarPlatListSnapShot(DataGridViewRow row)
        {
            if (row != null)
            {
                CardInfo card = row.Tag as CardInfo;
                if (card != null)
                {
                     SnapShotBll bll = new SnapShotBll(AppSettings.CurrentSetting.ImageDBConnStr);
                    List<SnapShot> shots = bll.GetSnapShots(card.LastDateTime, card.CardID);
                    if (shots != null && shots.Count > 0)
                    {
                        plEvent.ShowSnapShots(shots);
                    }
                }
            }
        }
        #endregion

        #region 公共属性和方法
        /// <summary>
        /// 获取输入的车牌号
        /// </summary>
        public string CarPlate
        {
            get
            {
                return txtCarplate.Text.Trim().ToUpper();
            }
        }
        /// <summary>
        /// 获取选择的卡号
        /// </summary>
        public string CardID
        {
            get
            {
                return _CardID;
            }
        }
        #endregion

        #region 窗体事件
        private void FrmCarPlateManualExit_Load(object sender, EventArgs e)
        {
            this.Height = 200;
            this.pnlSearch.Visible = false;
            this.ucEnterDateTime.Init();
            _Plates = GetAllCarplates();
        }
        private void txtCarplate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCarplate.Text))
            {
                if (!this.listBox1.Visible)
                {
                    this.listBox1.Visible = true;
                    this.listBox1.Location = new Point(txtCarplate.Left, txtCarplate.Top + txtCarplate.Height);
                    this.listBox1.Width = this.txtCarplate.Width;
                }

                List<string> plates = _Plates;
                if (!string.IsNullOrEmpty(txtCarplate.Text))
                {
                    string txt = txtCarplate.Text.Trim().ToUpper();
                    plates = _Plates.Where(item => item.ToUpper().Contains(txt)).ToList();
                }
                listBox1.Items.Clear();
                foreach (string str in plates)
                {
                    listBox1.Items.Add(str);
                }
            }
            else
            {
                listBox1.Visible = false;
            }
        }
        private void btnAdvance_Click(object sender, EventArgs e)
        {
            if (_Advance)
            {
                this.Height = 200;
                this.pnlSearch.Visible = false;
            }
            else
            {
                this.pnlSearch.Visible = true;
                this.Height = 675;
            }
            _Advance = !_Advance;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            this._CardID = string.Empty;
            string txt = CarPlate;
            if (txt.Length != 7 && txt.Length != 8)
            {
                MessageBox.Show("输入的车牌号格式有误，请输入７位或８位车牌号");
                return;
            }
            if ((!string.IsNullOrEmpty(txt) && !_Plates.Exists(item => item.ToUpper() == txt)))
            {
                MessageBox.Show("输入的车牌号在系统中没有登记，请输入一个系统中登记的车牌号");
                return;
            }
            if (!string.IsNullOrEmpty(txt))
            {
                this.DialogResult = DialogResult.OK;
            }
        }
        private void chkNoCarPlate_CheckedChanged(object sender, EventArgs e)
        {
            this.txtFindCarPlate.Enabled = !this.chkNoCarPlate.Checked;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.dgvCarPlateLists.Rows.Clear();
            this.plEvent.Clear();
            CardSearchCondition search = new CardSearchCondition();
            search.ListType = CardListType.CarPlate;
            search.LastDateTime = new DateTimeRange(this.ucEnterDateTime.StartDateTime, this.ucEnterDateTime.EndDateTime);
            if (!this.chkNoCarPlate.Checked)
            {
                search.CarPlate = this.txtFindCarPlate.Text.Trim();
            }
            List<CardInfo> cards = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCards(search).QueryObjects;
            if (cards != null)
            {
                ShowCarPlateLists(cards);
            }
        }
        private void dgvCarPlateLists_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            plEvent.Clear();
            DataGridViewRow row = this.dgvCarPlateLists.Rows[e.RowIndex];
            ShowCarPlatListSnapShot(row);
        }
        private void btnCardIDOK_Click(object sender, EventArgs e)
        {
            this.txtCarplate.Text = string.Empty;

            CardInfo card = null;
            DataGridViewRow row = this.dgvCarPlateLists.CurrentRow;
            if (row != null)
            {
                card = row.Tag as CardInfo;
            }
            if (card == null)
            {
                MessageBox.Show("请选择出场的车辆！");
                return;
            }
            this._CardID = card.CardID;
            this.DialogResult = DialogResult.OK;
        }
        private void listBox1_Click(object sender, EventArgs e)
        {
            this.txtCarplate.Text = this.listBox1.SelectedItem == null ? string.Empty : this.listBox1.SelectedItem.ToString();
            this.listBox1.Visible = false;
        }
        #endregion









    }
}
