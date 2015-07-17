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
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.UI
{
    public partial class FrmCarPlateManualEnter : Form
    {
        public FrmCarPlateManualEnter()
        {
            InitializeComponent();
        }

        #region 私有变量
        private List<string> _Plates;
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
        /// 获取输入的临时访客车牌号
        /// </summary>
        public string VisitorCarPlate
        {
            get
            {
                if (!this.chkNotPlate.Checked)
                {
                    return comVisitor.Text.Trim() + txtVisitor.Text.Trim().ToUpper();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// 获取进场的车牌号码
        /// </summary>
        public string EnterCarPlate
        {
            get
            {
                //使用输入的车牌号
                string carplate = CarPlate;
                if (string.IsNullOrEmpty(carplate))
                {
                    //如果输入的车牌号为空，使用临时访客车牌号
                    carplate = VisitorCarPlate;
                }
                return carplate;
            }
        }
        #endregion

        #region 事件处理程序
        private void FrmCarPlateManualEnter_Load(object sender, EventArgs e)
        {
            _Plates = GetAllCarplates();
            this.comVisitor.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            txtVisitor.Text = string.Empty;
            comVisitor.Text = string.Empty;
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
            if (!string.IsNullOrEmpty(txt) || !string.IsNullOrEmpty(txtVisitor.Text.Trim()))
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnVisitor_Click(object sender, EventArgs e)
        {
            this.txtCarplate.Text = string.Empty;
            string txt = VisitorCarPlate;
            if (!this.chkNotPlate.Checked && txt.Length != 7 && txt.Length != 8)
            {
                MessageBox.Show("输入的车牌号格式有误，请输入７位或８位车牌号");
                return;
            }
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void txtCarplate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCarplate.Text))
            {
                if (!this.listBox1.Visible)
                {
                    this.listBox1.Visible = true;
                    this.comVisitor.Visible = false;
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
                this.comVisitor.Visible = true;
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            this.txtCarplate.Text = this.listBox1.SelectedItem == null ? string.Empty : this.listBox1.SelectedItem.ToString();
            this.listBox1.Visible = false;
            this.comVisitor.Visible = true;
        }

        private void txtVisitor_Enter(object sender, EventArgs e)
        {
            this.listBox1.Visible = false;
            this.comVisitor.Visible = true;
        }

        private void chkNotPlate_CheckedChanged(object sender, EventArgs e)
        {
            this.comVisitor.Enabled = !this.chkNotPlate.Checked;
            this.txtVisitor.Enabled = !this.chkNotPlate.Checked;
        }
        #endregion

    }
}
