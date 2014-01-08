using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park .BusinessModel .Report ;
using Ralid.Park .BusinessModel .Interface ;

namespace Ralid.Park.UI
{
    public partial class FrmRealReport : Form,IReportHandler 
    {
        public FrmRealReport()
        {
            InitializeComponent();
        }

        private void eventList_DoubleClick(object sender, EventArgs e)
        {
            eventList.Items.Clear();
        }


        #region 实现接口IReportHandler
        public void ProcessReport(ReportBase report)
        {
            Action<ReportBase> action = delegate(ReportBase report1)
            {
                eventList.InsertReport(report1);
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, report);
            }
            else
            {
                action(report);
            }
        }
        #endregion
    }
}
