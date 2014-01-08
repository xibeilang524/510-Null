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
    public partial class FrmWaiting : Form
    {
        public int Progess
        {
            set { this.progressBar1.Value = value; }
        }
        
        public delegate void UpdateControlDelegate(string tips);

        public void SetTips(string tips)
        {
            this.lbtips.Invoke(new UpdateControlDelegate(UpdateControlsTips),new object[]{tips});
        }

        public void UpdateControlsTips(string strText)
        {
            this.lbtips.Text = strText;
        }

        public FrmWaiting()
        {
            InitializeComponent();
            InitControl();
        }

        private void InitControl()
        {
            //this.panelProgress.Visible = true;
            //this.panelResult.Visible = false;
            //this.Width = this.panelProgress.Width;
            //this.Height = this.panelProgress.Height;
            this.progressBar1.Maximum = 6;
            this.progressBar1.Minimum = 0;
        }
    }
}
