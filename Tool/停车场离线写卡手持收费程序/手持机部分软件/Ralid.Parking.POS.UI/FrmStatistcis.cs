using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Ralid.Parking.POS.Model;
using Ralid.Parking.POS.DAL;
using Ralid.Parking.POS.Tool;

namespace Ralid.Parking.POS.UI
{
    public partial class FrmStatistcis : Form
    {
        public FrmStatistcis()
        {
            InitializeComponent();
        }

        private List<CardPaymentInfo> _Records = new List<CardPaymentInfo>();

        private void FrmCondition_Load(object sender, EventArgs e)
        {
            this.comOperator.Items.Clear();
            if (MySetting.Current.Operators != null && MySetting.Current.Operators.Count > 0)
            {
                this.comOperator.Items.Add(string.Empty);
                foreach (OperatorInfo opt in MySetting.Current.Operators)
                {
                    this.comOperator.Items.Add(opt.OperatorName);
                }
            }

            this.comOperator.Text = OperatorInfo.CurrentOperator.OperatorName;
            this.dtBegin.Value = DateTime.Today;
            this.dtEnd.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            this.btnExport.Enabled = (_Records.Count > 0);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            decimal sum = 0;
            decimal count = 0;
            this._Records.Clear();
            List<CardPaymentInfo> items = (new CardPaymentInfoProvider()).GetRecords(dtBegin.Value, dtEnd.Value);
            if (items != null && items.Count > 0)
            {
                foreach (CardPaymentInfo payment in items)
                {
                    if (!string.IsNullOrEmpty(comOperator.Text))
                    {
                        if (payment.Operator == comOperator.Text)
                        {
                            sum += payment.Paid;
                            count++;
                            _Records.Add(payment);
                        }
                    }
                    else
                    {
                        sum += payment.Paid;
                        count++;
                        _Records.Add(payment);
                    }
                }
            }
            lblCount.Text = string.Format("记录条数：{0}", count);
            lblAmount.Text = string.Format("收费金额：{0}", sum);
            this.btnExport.Enabled = _Records.Count > 0;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string file = Path.Combine(appPath, "Record.txt");
            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        foreach (CardPaymentInfo payment in _Records)
                        {
                            writer.WriteLine(CardPaymentInfoSerializer.Serialize(payment));
                        }
                    }
                }
                MessageBox.Show(string.Format("成功导出 {0} 条收费记录", _Records.Count));
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            btnExport.Enabled = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}