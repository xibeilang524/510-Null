namespace Ralid.Park.UI
{
    partial class FrmWorkstationDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWorkstationDetail));
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new Ralid.GeneralLibrary.WinformControl.DBCTextBox(this.components);
            this.EntranceTree = new Ralid.Park.UserControls.HardwareTree(this.components);
            this.chkCenterCharge = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
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
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label1.Name = "label1";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            // 
            // EntranceTree
            // 
            resources.ApplyResources(this.EntranceTree, "EntranceTree");
            this.EntranceTree.CheckBoxes = true;
            this.EntranceTree.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.EntranceTree.Name = "EntranceTree";
            this.EntranceTree.SelectedImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.EntranceTree.ShowEntrance = true;
            this.EntranceTree.ShowVideoSource = false;
            // 
            // chkCenterCharge
            // 
            resources.ApplyResources(this.chkCenterCharge, "chkCenterCharge");
            this.chkCenterCharge.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.chkCenterCharge.Name = "chkCenterCharge";
            this.chkCenterCharge.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ImageKey = global::Ralid.Park.UI.Resources.Resource1.FrmTariffSelection_InvalidTimezone;
            this.label2.Name = "label2";
            // 
            // FrmWorkstationDetail
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkCenterCharge);
            this.Controls.Add(this.EntranceTree);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmWorkstationDetail";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.EntranceTree, 0);
            this.Controls.SetChildIndex(this.chkCenterCharge, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Ralid.GeneralLibrary.WinformControl.DBCTextBox txtName;
        private Ralid.Park.UserControls.HardwareTree EntranceTree;
        private System.Windows.Forms.CheckBox chkCenterCharge;
        private System.Windows.Forms.Label label2;
    }
}