namespace Ralid.Park.UI
{
    partial class FrmRemoteReadCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRemoteReadCard));
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVisitor = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtCarplate = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.comVisitor = new System.Windows.Forms.ComboBox();
            this.btnVisitor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            this.btnOk.Enter += new System.EventHandler(this.txtCardID_Enter);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label1.Name = "label1";
            // 
            // txtVisitor
            // 
            resources.ApplyResources(this.txtVisitor, "txtVisitor");
            this.txtVisitor.Name = "txtVisitor";
            this.txtVisitor.Enter += new System.EventHandler(this.txtCardID_Enter);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label2.Name = "label2";
            // 
            // listBox1
            // 
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Name = "listBox1";
            this.listBox1.Click += new System.EventHandler(this.listBox1_Click);
            // 
            // txtCarplate
            // 
            resources.ApplyResources(this.txtCarplate, "txtCarplate");
            this.txtCarplate.Name = "txtCarplate";
            this.txtCarplate.TextChanged += new System.EventHandler(this.txtCarplate_TextChanged);
            // 
            // comVisitor
            // 
            resources.ApplyResources(this.comVisitor, "comVisitor");
            this.comVisitor.FormattingEnabled = true;
            this.comVisitor.Items.AddRange(new object[] {
            resources.GetString("comVisitor.Items"),
            resources.GetString("comVisitor.Items1"),
            resources.GetString("comVisitor.Items2"),
            resources.GetString("comVisitor.Items3"),
            resources.GetString("comVisitor.Items4"),
            resources.GetString("comVisitor.Items5"),
            resources.GetString("comVisitor.Items6"),
            resources.GetString("comVisitor.Items7"),
            resources.GetString("comVisitor.Items8"),
            resources.GetString("comVisitor.Items9"),
            resources.GetString("comVisitor.Items10"),
            resources.GetString("comVisitor.Items11"),
            resources.GetString("comVisitor.Items12"),
            resources.GetString("comVisitor.Items13"),
            resources.GetString("comVisitor.Items14"),
            resources.GetString("comVisitor.Items15"),
            resources.GetString("comVisitor.Items16"),
            resources.GetString("comVisitor.Items17"),
            resources.GetString("comVisitor.Items18"),
            resources.GetString("comVisitor.Items19"),
            resources.GetString("comVisitor.Items20"),
            resources.GetString("comVisitor.Items21"),
            resources.GetString("comVisitor.Items22"),
            resources.GetString("comVisitor.Items23"),
            resources.GetString("comVisitor.Items24"),
            resources.GetString("comVisitor.Items25"),
            resources.GetString("comVisitor.Items26"),
            resources.GetString("comVisitor.Items27"),
            resources.GetString("comVisitor.Items28")});
            this.comVisitor.Name = "comVisitor";
            // 
            // btnVisitor
            // 
            this.btnVisitor.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            resources.ApplyResources(this.btnVisitor, "btnVisitor");
            this.btnVisitor.Name = "btnVisitor";
            this.btnVisitor.UseVisualStyleBackColor = true;
            this.btnVisitor.Click += new System.EventHandler(this.btnVisitor_Click);
            // 
            // FrmRemoteReadCard
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnVisitor);
            this.Controls.Add(this.comVisitor);
            this.Controls.Add(this.txtCarplate);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtVisitor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmRemoteReadCard";
            this.Load += new System.EventHandler(this.FrmManualEnter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtVisitor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBox1;
        private GeneralLibrary.WinformControl.DBCTextBox txtCarplate;
        private System.Windows.Forms.ComboBox comVisitor;
        private System.Windows.Forms.Button btnVisitor;

    }
}