using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BLL;

namespace Ralid.Park.UI
{
    public partial class FrmNoCardLost : Form
    {
        public FrmNoCardLost()
        {
            InitializeComponent();
        }

        #region 私有方法
        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(this.txtCarPlate.Text.Trim()))
            {
                MessageBox.Show(Resources.Resource1.FrmNoCardLost_InvalidCarPlate);
                this.txtCarPlate.Focus();
                return false;
            }
            return true;
        }
        #endregion

        #region 窗体事件
        private void FrmNoCardLost_Load(object sender, EventArgs e)
        {
            this.comPaymentMode.Init();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInput())
                {
                    CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                    CommandResult result = bll.NoCardLoss(this.txtCarPlate.Text.Trim(), this.txtOwnerName.Text.Trim(), this.txtMemo.Text.Trim(), this.txtCardCost.DecimalValue, this.comPaymentMode.SelectedPaymentMode);
                    if (result.Result == ResultCode.Successful)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion


    }
}
