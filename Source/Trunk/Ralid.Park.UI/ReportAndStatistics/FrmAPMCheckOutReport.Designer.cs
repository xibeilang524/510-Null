namespace Ralid.Park.UI.ReportAndStatistics
{
    partial class FrmAPMCheckOutReport
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAPMCheckOutReport));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ucDateTimeInterval1 = new Ralid.Park.UserControls.UCDateTimeInterval();
            this.comAPM = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAPMOperator = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.customDataGridView1 = new Ralid.Park.UserControls.CustomDataGridView(this.components);
            this.colMID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAPMOperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheckOutDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActualAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDifference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTheBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIncomeMoneny = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPayMoney = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCoin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHundred = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFifty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTwenty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCashAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            // 
            // btnSaveAs
            // 
            resources.ApplyResources(this.btnSaveAs, "btnSaveAs");
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.ucDateTimeInterval1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // ucDateTimeInterval1
            // 
            resources.ApplyResources(this.ucDateTimeInterval1, "ucDateTimeInterval1");
            this.ucDateTimeInterval1.EndDateTime = new System.DateTime(2012, 9, 14, 15, 12, 56, 827);
            this.ucDateTimeInterval1.Name = "ucDateTimeInterval1";
            this.ucDateTimeInterval1.ShowTime = true;
            this.ucDateTimeInterval1.StartDateTime = new System.DateTime(2012, 9, 14, 15, 12, 56, 830);
            // 
            // comAPM
            // 
            resources.ApplyResources(this.comAPM, "comAPM");
            this.comAPM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comAPM.FormattingEnabled = true;
            this.comAPM.Name = "comAPM";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtAPMOperator
            // 
            resources.ApplyResources(this.txtAPMOperator, "txtAPMOperator");
            this.txtAPMOperator.Name = "txtAPMOperator";
            // 
            // customDataGridView1
            // 
            resources.ApplyResources(this.customDataGridView1, "customDataGridView1");
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMID,
            this.colAPMOperator,
            this.colCheckOutDateTime,
            this.colTotalAmount,
            this.colAmount,
            this.colActualAmount,
            this.colDifference,
            this.colTheBalance,
            this.colLastBalance,
            this.colIncomeMoneny,
            this.colPayMoney,
            this.colBalance,
            this.colCoin,
            this.colHundred,
            this.colFifty,
            this.colTwenty,
            this.colTen,
            this.colCash,
            this.colCashAmount,
            this.colLastDateTime,
            this.colMemo});
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 23;
            this.customDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // colMID
            // 
            resources.ApplyResources(this.colMID, "colMID");
            this.colMID.Name = "colMID";
            this.colMID.ReadOnly = true;
            // 
            // colAPMOperator
            // 
            resources.ApplyResources(this.colAPMOperator, "colAPMOperator");
            this.colAPMOperator.Name = "colAPMOperator";
            this.colAPMOperator.ReadOnly = true;
            // 
            // colCheckOutDateTime
            // 
            resources.ApplyResources(this.colCheckOutDateTime, "colCheckOutDateTime");
            this.colCheckOutDateTime.Name = "colCheckOutDateTime";
            this.colCheckOutDateTime.ReadOnly = true;
            // 
            // colTotalAmount
            // 
            resources.ApplyResources(this.colTotalAmount, "colTotalAmount");
            this.colTotalAmount.Name = "colTotalAmount";
            this.colTotalAmount.ReadOnly = true;
            // 
            // colAmount
            // 
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            // 
            // colActualAmount
            // 
            resources.ApplyResources(this.colActualAmount, "colActualAmount");
            this.colActualAmount.Name = "colActualAmount";
            this.colActualAmount.ReadOnly = true;
            // 
            // colDifference
            // 
            resources.ApplyResources(this.colDifference, "colDifference");
            this.colDifference.Name = "colDifference";
            this.colDifference.ReadOnly = true;
            // 
            // colTheBalance
            // 
            resources.ApplyResources(this.colTheBalance, "colTheBalance");
            this.colTheBalance.Name = "colTheBalance";
            this.colTheBalance.ReadOnly = true;
            // 
            // colLastBalance
            // 
            resources.ApplyResources(this.colLastBalance, "colLastBalance");
            this.colLastBalance.Name = "colLastBalance";
            this.colLastBalance.ReadOnly = true;
            // 
            // colIncomeMoneny
            // 
            resources.ApplyResources(this.colIncomeMoneny, "colIncomeMoneny");
            this.colIncomeMoneny.Name = "colIncomeMoneny";
            this.colIncomeMoneny.ReadOnly = true;
            // 
            // colPayMoney
            // 
            resources.ApplyResources(this.colPayMoney, "colPayMoney");
            this.colPayMoney.Name = "colPayMoney";
            this.colPayMoney.ReadOnly = true;
            // 
            // colBalance
            // 
            resources.ApplyResources(this.colBalance, "colBalance");
            this.colBalance.Name = "colBalance";
            this.colBalance.ReadOnly = true;
            // 
            // colCoin
            // 
            resources.ApplyResources(this.colCoin, "colCoin");
            this.colCoin.Name = "colCoin";
            this.colCoin.ReadOnly = true;
            // 
            // colHundred
            // 
            resources.ApplyResources(this.colHundred, "colHundred");
            this.colHundred.Name = "colHundred";
            this.colHundred.ReadOnly = true;
            // 
            // colFifty
            // 
            resources.ApplyResources(this.colFifty, "colFifty");
            this.colFifty.Name = "colFifty";
            this.colFifty.ReadOnly = true;
            // 
            // colTwenty
            // 
            resources.ApplyResources(this.colTwenty, "colTwenty");
            this.colTwenty.Name = "colTwenty";
            this.colTwenty.ReadOnly = true;
            // 
            // colTen
            // 
            resources.ApplyResources(this.colTen, "colTen");
            this.colTen.Name = "colTen";
            this.colTen.ReadOnly = true;
            // 
            // colCash
            // 
            resources.ApplyResources(this.colCash, "colCash");
            this.colCash.Name = "colCash";
            this.colCash.ReadOnly = true;
            // 
            // colCashAmount
            // 
            resources.ApplyResources(this.colCashAmount, "colCashAmount");
            this.colCashAmount.Name = "colCashAmount";
            this.colCashAmount.ReadOnly = true;
            // 
            // colLastDateTime
            // 
            resources.ApplyResources(this.colLastDateTime, "colLastDateTime");
            this.colLastDateTime.Name = "colLastDateTime";
            this.colLastDateTime.ReadOnly = true;
            // 
            // colMemo
            // 
            resources.ApplyResources(this.colMemo, "colMemo");
            this.colMemo.Name = "colMemo";
            this.colMemo.ReadOnly = true;
            // 
            // FrmAPMCheckOutReport
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAPMOperator);
            this.Controls.Add(this.comAPM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmAPMCheckOutReport";
            this.Load += new System.EventHandler(this.FrmAPMCheckOutReport_Load);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnSaveAs, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.comAPM, 0);
            this.Controls.SetChildIndex(this.txtAPMOperator, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.customDataGridView1, 0);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.UCDateTimeInterval ucDateTimeInterval1;
        private System.Windows.Forms.ComboBox comAPM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private GeneralLibrary.WinformControl.DBCTextBox txtAPMOperator;
        private UserControls.CustomDataGridView customDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAPMOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCheckOutDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colActualAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDifference;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTheBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIncomeMoneny;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPayMoney;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCoin;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHundred;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFifty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTwenty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCash;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCashAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemo;
    }
}
