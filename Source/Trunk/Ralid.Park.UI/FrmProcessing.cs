using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ralid.Park.UI
{
    public partial class FrmProcessing : Form
    {
        #region 构造函数
        public FrmProcessing()
        {
            InitializeComponent();
        }
        #endregion

        #region 公共方法
        public void ShowProgress(string message, decimal completeRation)
        {
            try
            {
                Action action = delegate()
                {
                    this.label1.Text = message;
                    this.progressBar1.Value = (int)(completeRation * 100);
                    if (completeRation == 1)
                    {
                        this.DialogResult = DialogResult.OK;
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
            catch
            {
            }
        }
        #endregion

        #region 窗体处理事件
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

    }
}
