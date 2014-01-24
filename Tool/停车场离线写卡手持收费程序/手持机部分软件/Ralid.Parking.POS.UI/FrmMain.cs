using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Windows.Forms;
using Ralid.Parking.POS.Model;

namespace Ralid.Parking.POS.UI
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        #region dllimport
        [DllImport("coredll.dll")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("coredll.dll")]
        internal extern static int EnableWindow(int hwnd, int fEnable);

        [DllImport("coredll.dll")]
        public static extern int ShowWindow(int hwnd, int nCmdShow);
        #endregion

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //屏蔽系统任务栏   
            int hTaskBarWnd = FindWindow("HHTaskBar", null);
            ShowWindow(hTaskBarWnd, 0);
            this.WindowState = FormWindowState.Maximized;

            //初始化所有配置参数
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string file = Path.Combine(appPath, "MySetting.xml");

            if (File.Exists(file))
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(MySetting));
                    using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                    {
                        MySetting ms = ser.Deserialize(fs) as MySetting;
                        if (ms != null)
                        {
                            MySetting.Current = ms;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (MySetting.Current == null) MySetting.Current = MySetting.DefaultSetting;
            DoLogin();
        }

        private void DoLogin()
        {
            FrmLogin frm = new FrmLogin();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.lblStatusBar.Text = string.Format("当前操作员：{0}", OperatorInfo.CurrentOperator.OperatorName);
                btnPayment_Click(btnPayment, EventArgs.Empty);
            }
            else
            {
                frm.Close();
                this.Close();
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            FrmPaying frm = new FrmPaying();
            frm.ShowDialog();
        }

        private void btnPaymentStatistics_Click(object sender, EventArgs e)
        {
            FrmStatistcis frm = new FrmStatistcis();
            frm.ShowDialog();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否退出系统？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void FrmMain_Closed(object sender, EventArgs e)
        {
            //显示系统任务栏   
            int hTaskBarWnd = FindWindow("HHTaskBar", null);
            ShowWindow(hTaskBarWnd, 1); 
        }
    }
}