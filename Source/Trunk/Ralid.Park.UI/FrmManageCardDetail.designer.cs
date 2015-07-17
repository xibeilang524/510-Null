namespace Ralid.Park.UI
{
    partial class FrmManageCardDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageCardDetail));
            this.dtValidDate = new System.Windows.Forms.DateTimePicker();
            this.txtCardID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.operatorComboBox1 = new Ralid.Park.UserControls.OperatorComboBox(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comCardType = new Ralid.Park.UserControls.ManageCardTypeComboBox(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chkWriteCard = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblReceivedFees = new System.Windows.Forms.Label();
            this.txtDepartment = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.chkIsForbid = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            // 
            // dtValidDate
            // 
            resources.ApplyResources(this.dtValidDate, "dtValidDate");
            this.dtValidDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtValidDate.Name = "dtValidDate";
            // 
            // txtCardID
            // 
            resources.ApplyResources(this.txtCardID, "txtCardID");
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Enter += new System.EventHandler(this.txtCardID_Enter);
            this.txtCardID.Leave += new System.EventHandler(this.txtCardID_Leave);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // operatorComboBox1
            // 
            resources.ApplyResources(this.operatorComboBox1, "operatorComboBox1");
            this.operatorComboBox1.FormattingEnabled = true;
            this.operatorComboBox1.Name = "operatorComboBox1";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // comCardType
            // 
            resources.ApplyResources(this.comCardType, "comCardType");
            this.comCardType.FormattingEnabled = true;
            this.comCardType.Name = "comCardType";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::Ralid.Park.UI.Properties.Resources.CardReader;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // chkWriteCard
            // 
            resources.ApplyResources(this.chkWriteCard, "chkWriteCard");
            this.chkWriteCard.Name = "chkWriteCard";
            this.chkWriteCard.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lblReceivedFees
            // 
            resources.ApplyResources(this.lblReceivedFees, "lblReceivedFees");
            this.lblReceivedFees.Name = "lblReceivedFees";
            // 
            // txtDepartment
            // 
            resources.ApplyResources(this.txtDepartment, "txtDepartment");
            this.txtDepartment.Name = "txtDepartment";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // chkIsForbid
            // 
            resources.ApplyResources(this.chkIsForbid, "chkIsForbid");
            this.chkIsForbid.Name = "chkIsForbid";
            this.chkIsForbid.UseVisualStyleBackColor = true;
            // 
            // FrmManageCardDetail
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkIsForbid);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblReceivedFees);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkWriteCard);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comCardType);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.operatorComboBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtValidDate);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmManageCardDetail";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmManageCardDetail_FormClosing);
            this.Load += new System.EventHandler(this.FrmManageCardDetail_Load);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtCardID, 0);
            this.Controls.SetChildIndex(this.dtValidDate, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.operatorComboBox1, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.comCardType, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.chkWriteCard, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lblReceivedFees, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtDepartment, 0);
            this.Controls.SetChildIndex(this.chkIsForbid, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtValidDate;
        private System.Windows.Forms.TextBox txtCardID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private Ralid.Park .UserControls .OperatorComboBox operatorComboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Ralid.Park.UserControls.ManageCardTypeComboBox comCardType;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox chkWriteCard;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblReceivedFees;
        private GeneralLibrary.WinformControl.DBCTextBox txtDepartment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkIsForbid;

    }
}