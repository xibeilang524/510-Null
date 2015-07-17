using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PreferentialSystem
{
    public partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            this.lbVersion.Text = Application.ProductVersion;
        }

        private void FrmAbout_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
