using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SNDLL_CSharp;

namespace SNDLLTester_CSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ulong serialNum = 0;
            SNDLL.GetSerialNumber(ref serialNum);
            txtInfo.Text = serialNum.ToString("X");
        }
    }
}