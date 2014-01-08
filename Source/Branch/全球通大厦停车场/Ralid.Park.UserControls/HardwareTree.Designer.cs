namespace Ralid.Park.UserControls
{
    partial class HardwareTree
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HardwareTree));
            this.images = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // images
            // 
            this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
            this.images.TransparentColor = System.Drawing.Color.Transparent;
            this.images.Images.SetKeyName(0, "Park.ico");
            this.images.Images.SetKeyName(1, "parking.png");
            this.images.Images.SetKeyName(2, "entrance.png");
            this.images.Images.SetKeyName(3, "Video.png");
            this.images.Images.SetKeyName(4, "offLine.ico");
            this.images.Images.SetKeyName(5, "alert.png");
            this.images.Images.SetKeyName(6, "master.png");
            // 
            // HardwareTree
            // 
            this.LineColor = System.Drawing.Color.Black;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList images;
    }
}
