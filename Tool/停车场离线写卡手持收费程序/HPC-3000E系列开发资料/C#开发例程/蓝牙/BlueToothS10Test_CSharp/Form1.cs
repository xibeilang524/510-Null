using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZLG;

namespace BlueToothS10Test_CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cbbCom.Items.Add("COM1");
            cbbCom.Items.Add("COM2");
            cbbCom.Items.Add("COM3");
            cbbCom.Items.Add("COM4");
            cbbCom.Items.Add("COM5");
            cbbCom.SelectedIndex = 2;

            cbbMode.Items.Add("AT Command");
            cbbMode.Items.Add("Communication");
            cbbMode.SelectedIndex = 1;

            cbbRole.Items.Add("Master");
            cbbRole.Items.Add("Slave");
            cbbRole.Items.Add("Loopback");
            cbbRole.SelectedIndex = 0;
        }

        bool AssertResult(S10_RetCode result)
        {
            if(result != S10_RetCode.SUCCESS_SETTING)
            {
                string str = string.Format("{0:D}", result);
                MessageBox.Show(str);
                return false;
            }
            return true;
        }

        private void bnOpen_Click(object sender, EventArgs e)
        {
            if(AssertResult(BlueTooth.S10_Init((S10_Coms)(S10_Coms.COM1+cbbCom.SelectedIndex))))
            {
                bnClose.Enabled = true;
                bnOpen.Enabled = false;
            }
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            if(AssertResult(BlueTooth.S10_Close()))
            {
                bnClose.Enabled = false;
                bnOpen.Enabled = true;
            }
        }

        private void bnSetMode_Click(object sender, EventArgs e)
        {
            AssertResult(BlueTooth.S10_SetOperationMode((S10_OperationMode)cbbMode.SelectedIndex));
        }

        private void bnReboot_Click(object sender, EventArgs e)
        {
            AssertResult(BlueTooth.S10_Reboot());
        }

        private void bnSoftVersion_Click(object sender, EventArgs e)
        {
            byte[] bstr = new byte[1024];
            uint ulen = 1024;
            if(AssertResult(BlueTooth.S10_GetModuleVersion(bstr,ref ulen)))
            {
                bstr[ulen] = 0;
                MessageBox.Show(Encoding.ASCII.GetString(bstr,0,(int)ulen));
            }
        }
    }
}