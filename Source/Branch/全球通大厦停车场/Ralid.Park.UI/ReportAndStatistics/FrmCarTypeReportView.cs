using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmCarTypeReportView : Form
    {
        /// <summary>
        /// 卡片停车收费明细列表
        /// </summary>
        public List<CardPaymentInfo> CardPaymentList { get; set; }

        /// <summary>
        /// 车类型收费统计列表
        /// </summary>
        public List<CollectInfo> CollectList { get; set; }

        /// <summary>
        /// 报表标题
        /// </summary>
        public string  Title { get; set; }


        public FrmCarTypeReportView()
        {
            InitializeComponent();
        }

        private void FrmCarTypeReportView_Load(object sender, EventArgs e)
        {
            DoDisplay();
        }

        public void DoPrint()
        {
            DoDisplay();

            // 执行直接打印
            new DirectPrint().DoPrint(this.reportViewer1.LocalReport);
        }

        private void DoDisplay()
        {
            if (CardPaymentList != null)
            {
                List<CardPaymentInfo> LeftCardPaymentList = new List<CardPaymentInfo>();
                List<CardPaymentInfo> RightCardPaymentList = new List<CardPaymentInfo>();
                int index = 0;
                foreach (CardPaymentInfo info in this.CardPaymentList)
                {
                    if (index % 2 == 0)
                    {
                        // 偶数位显示于左边
                        LeftCardPaymentList.Add(info);
                    }
                    else
                    {
                        RightCardPaymentList.Add(info);
                    }
                    index++;
                }

                this.cardPaymentInfoBindingSource.Clear();
                this.cardPaymentInfoBindingSource.DataSource = LeftCardPaymentList;

                this.cardPaymentInfoBindingSource2.Clear();
                this.cardPaymentInfoBindingSource2.DataSource = RightCardPaymentList;

                this.collectInfoBindingSource.Clear();
                this.collectInfoBindingSource.DataSource = this.CollectList;

                this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Title", Title));
                this.reportViewer1.RefreshReport();
            }
        }
    }
}
