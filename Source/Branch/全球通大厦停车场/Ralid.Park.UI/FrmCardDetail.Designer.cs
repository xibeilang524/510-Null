namespace Ralid.Park.UI
{
    partial class FrmCardDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCardDetail));
            this.textBox1 = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.textBox2 = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.textBox4 = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.textBox5 = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.ucCardInfo = new Ralid.Park.UserControls.UCCard();
            this.chkWriteCard = new System.Windows.Forms.CheckBox();
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
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // textBox4
            // 
            resources.ApplyResources(this.textBox4, "textBox4");
            this.textBox4.Name = "textBox4";
            // 
            // textBox5
            // 
            resources.ApplyResources(this.textBox5, "textBox5");
            this.textBox5.Name = "textBox5";
            // 
            // ucCardInfo
            // 
            resources.ApplyResources(this.ucCardInfo, "ucCardInfo");
            this.ucCardInfo.Name = "ucCardInfo";
            // 
            // chkWriteCard
            // 
            resources.ApplyResources(this.chkWriteCard, "chkWriteCard");
            this.chkWriteCard.Name = "chkWriteCard";
            this.chkWriteCard.UseVisualStyleBackColor = true;
            // 
            // FrmCardDetail
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkWriteCard);
            this.Controls.Add(this.ucCardInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCardDetail";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCardDetail_FormClosing);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.ucCardInfo, 0);
            this.Controls.SetChildIndex(this.chkWriteCard, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Ralid.GeneralLibrary.WinformControl.DBCTextBox textBox1;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox textBox2;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox textBox4;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox textBox5;
        private Ralid.Park.UserControls.UCCard ucCardInfo;
        private System.Windows.Forms.CheckBox chkWriteCard;
    }
}