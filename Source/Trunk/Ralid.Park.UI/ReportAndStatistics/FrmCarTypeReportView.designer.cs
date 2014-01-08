namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmCarTypeReportView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCarTypeReportView));
            this.collectInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cardPaymentInfoBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.cardPaymentInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.collectInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardPaymentInfoBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardPaymentInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // collectInfoBindingSource
            // 
            this.collectInfoBindingSource.DataSource = typeof(Ralid.Park.BusinessModel.Model.CollectInfo);
            // 
            // cardPaymentInfoBindingSource2
            // 
            this.cardPaymentInfoBindingSource2.DataSource = typeof(Ralid.Park.BusinessModel.Model.CardPaymentInfo);
            // 
            // cardPaymentInfoBindingSource
            // 
            this.cardPaymentInfoBindingSource.DataSource = typeof(Ralid.Park.BusinessModel.Model.CardPaymentInfo);
            // 
            // reportViewer1
            // 
            this.reportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsCollect";
            reportDataSource1.Value = this.collectInfoBindingSource;
            reportDataSource2.Name = "dsCardPayment2";
            reportDataSource2.Value = this.cardPaymentInfoBindingSource2;
            reportDataSource3.Name = "dsCardPayment";
            reportDataSource3.Value = this.cardPaymentInfoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Ralid.Monitor.ReportAndStatistics.FrmCarTypeReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.PageCountMode = Microsoft.Reporting.WinForms.PageCountMode.Actual;
            this.reportViewer1.Size = new System.Drawing.Size(779, 478);
            this.reportViewer1.TabIndex = 0;
            // 
            // FrmCarTypeReportView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 478);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCarTypeReportView";
            this.Text = "预览报表";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCarTypeReportView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.collectInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardPaymentInfoBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardPaymentInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource collectInfoBindingSource;
        private System.Windows.Forms.BindingSource cardPaymentInfoBindingSource;
        private System.Windows.Forms.BindingSource cardPaymentInfoBindingSource2;
    }
}