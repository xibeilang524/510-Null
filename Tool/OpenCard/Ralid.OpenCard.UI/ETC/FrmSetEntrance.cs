using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ralid.OpenCard.UI.ETC
{
    public partial class FrmSetEntrance : Form
    {
        public FrmSetEntrance()
        {
            InitializeComponent();
        }

        public int EntranceID { get; set; }

        private void FrmSetEntrance_Load(object sender, EventArgs e)
        {
            comEntrance.Init();
            comEntrance.SelectedEntranceID = EntranceID;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            EntranceID = comEntrance.SelectedEntranceID;
            this.DialogResult = DialogResult.OK;
        }
    }
}
