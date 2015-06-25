using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.OpenCard.UI
{
    public partial class FrmWorkstationSelection : Form
    {
        public FrmWorkstationSelection()
        {
            InitializeComponent();
        }

        public string SelectedWorkstation { get; set; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty (workStationCombobox1.StationID))
            {
                SelectedWorkstation = workStationCombobox1.StationID;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void FrmWorkstationSelection_Load(object sender, EventArgs e)
        {
            this.workStationCombobox1.Init();
            if (WorkStationInfo.CurrentStation != null)
            {
                this.workStationCombobox1.StationID = WorkStationInfo.CurrentStation.StationID;
            }
        }
    }
}
