namespace Ralid.Park.UserControls
{
    partial class UCEntrance
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCEntrance));
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comPark = new Ralid.Park.UserControls.ParkCombobox(this.components);
            this.comEntrance = new Ralid.Park.UserControls.EntranceComboBox(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ImageKey = global::Ralid.Park.UserControls.Resources.Resource1.CardGridHeader_CanEnterWhenFull;
            this.label3.Name = "label3";
            // 
            // comPark
            // 
            resources.ApplyResources(this.comPark, "comPark");
            this.comPark.FormattingEnabled = true;
            this.comPark.Name = "comPark";
            this.comPark.SelectedIndexChanged += new System.EventHandler(this.comPark_SelectedIndexChanged);
            // 
            // comEntrance
            // 
            resources.ApplyResources(this.comEntrance, "comEntrance");
            this.comEntrance.FormattingEnabled = true;
            this.comEntrance.Name = "comEntrance";
            // 
            // UCEntrance
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comEntrance);
            this.Controls.Add(this.comPark);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "UCEntrance";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private ParkCombobox comPark;
        private EntranceComboBox comEntrance;
    }
}
