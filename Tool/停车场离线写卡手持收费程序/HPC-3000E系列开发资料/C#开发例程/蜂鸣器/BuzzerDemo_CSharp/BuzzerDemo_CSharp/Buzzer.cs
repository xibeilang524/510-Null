using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BuzzerDLL_CSharp;

namespace BuzzerDemo_CSharp
{
    public partial class Buzzer : Form
    {
        public Buzzer()
        {
            InitializeComponent();
        }

        private void bnBuzzerOn_Click(object sender, EventArgs e)
        {
            uint uRet = BuzzerDll.epcBuzzerOn(0);                                   /*  使蜂鸣器一直蜂鸣            */
            if (uRet == 0)
            {
                MessageBox.Show("蜂鸣器蜂鸣失败");
            }
        }

        private void bnBeepFive_Click(object sender, EventArgs e)
        {
            uint bRet = BuzzerDll.epcBuzzerBeeps(5, 200, 200);                         /*  蜂鸣器鸣叫5次               */
            if (bRet == 0)
            {
                MessageBox.Show("蜂鸣器鸣叫失败");
            }
        }

        private void bnBuzzerOff_Click(object sender, EventArgs e)
        {
            uint uRet = BuzzerDll.epcBuzzerOff();                                   /*  使蜂鸣器一直蜂鸣            */
            if (uRet == 0)
            {
                MessageBox.Show("蜂鸣器禁止失败");
            }
        }

        private void bnBuzzerState_Click(object sender, EventArgs e)
        {	
            uint dwStatus = BuzzerDll.epcBuzzerGetStatus();                          /*  读蜂鸣器状态                */
	        if (dwStatus > 1){
		        MessageBox.Show("读蜂鸣器状态失败");
		        return;
	        }
	        if (dwStatus == 0){
		        MessageBox.Show("蜂鸣器处于蜂鸣状态");
	        } else {
		        MessageBox.Show("蜂鸣器处于禁止状态");
	        }
        }
    }
}