namespace Ralid.Park.UserControls
{
    partial class UCPaging
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCPaging));
            this.tbpageindex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labtotalpages = new System.Windows.Forms.Label();
            this.labfristpage = new System.Windows.Forms.Label();
            this.labuppage = new System.Windows.Forms.Label();
            this.labdownpage = new System.Windows.Forms.Label();
            this.lablastpage = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labgotopage = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labtotalpagecount = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labtotalcount = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbgotoindex = new System.Windows.Forms.TextBox();
            this.cmbpagesize = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbpageindex
            // 
            resources.ApplyResources(this.tbpageindex, "tbpageindex");
            this.tbpageindex.Name = "tbpageindex";
            this.tbpageindex.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // labtotalpages
            // 
            resources.ApplyResources(this.labtotalpages, "labtotalpages");
            this.labtotalpages.Name = "labtotalpages";
            // 
            // labfristpage
            // 
            resources.ApplyResources(this.labfristpage, "labfristpage");
            this.labfristpage.BackColor = System.Drawing.Color.Silver;
            this.labfristpage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labfristpage.Name = "labfristpage";
            this.labfristpage.Click += new System.EventHandler(this.labfristpage_Click);
            // 
            // labuppage
            // 
            resources.ApplyResources(this.labuppage, "labuppage");
            this.labuppage.BackColor = System.Drawing.Color.Silver;
            this.labuppage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labuppage.Name = "labuppage";
            this.labuppage.Click += new System.EventHandler(this.labuppage_Click);
            // 
            // labdownpage
            // 
            resources.ApplyResources(this.labdownpage, "labdownpage");
            this.labdownpage.BackColor = System.Drawing.Color.Silver;
            this.labdownpage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labdownpage.Name = "labdownpage";
            this.labdownpage.Click += new System.EventHandler(this.labdownpage_Click);
            // 
            // lablastpage
            // 
            resources.ApplyResources(this.lablastpage, "lablastpage");
            this.lablastpage.BackColor = System.Drawing.Color.Silver;
            this.lablastpage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lablastpage.Name = "lablastpage";
            this.lablastpage.Click += new System.EventHandler(this.lablastpage_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // labgotopage
            // 
            resources.ApplyResources(this.labgotopage, "labgotopage");
            this.labgotopage.BackColor = System.Drawing.Color.Silver;
            this.labgotopage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labgotopage.Name = "labgotopage";
            this.labgotopage.Click += new System.EventHandler(this.labgotopage_Click);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // labtotalpagecount
            // 
            resources.ApplyResources(this.labtotalpagecount, "labtotalpagecount");
            this.labtotalpagecount.Name = "labtotalpagecount";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // labtotalcount
            // 
            resources.ApplyResources(this.labtotalcount, "labtotalcount");
            this.labtotalcount.Name = "labtotalcount";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbgotoindex);
            this.panel1.Controls.Add(this.cmbpagesize);
            this.panel1.Controls.Add(this.lablastpage);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.tbpageindex);
            this.panel1.Controls.Add(this.labtotalcount);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.labtotalpages);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.labfristpage);
            this.panel1.Controls.Add(this.labtotalpagecount);
            this.panel1.Controls.Add(this.labuppage);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.labdownpage);
            this.panel1.Controls.Add(this.labgotopage);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // tbgotoindex
            // 
            resources.ApplyResources(this.tbgotoindex, "tbgotoindex");
            this.tbgotoindex.Name = "tbgotoindex";
            // 
            // cmbpagesize
            // 
            this.cmbpagesize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbpagesize, "cmbpagesize");
            this.cmbpagesize.FormattingEnabled = true;
            this.cmbpagesize.Items.AddRange(new object[] {
            resources.GetString("cmbpagesize.Items"),
            resources.GetString("cmbpagesize.Items1"),
            resources.GetString("cmbpagesize.Items2"),
            resources.GetString("cmbpagesize.Items3"),
            resources.GetString("cmbpagesize.Items4"),
            resources.GetString("cmbpagesize.Items5")});
            this.cmbpagesize.Name = "cmbpagesize";
            this.cmbpagesize.SelectedIndexChanged += new System.EventHandler(this.cmbpagesize_SelectedIndexChanged);
            // 
            // UCPaging
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UCPaging";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbpageindex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labtotalpages;
        private System.Windows.Forms.Label labfristpage;
        private System.Windows.Forms.Label labuppage;
        private System.Windows.Forms.Label labdownpage;
        private System.Windows.Forms.Label lablastpage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labgotopage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labtotalpagecount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labtotalcount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbpagesize;
        private System.Windows.Forms.TextBox tbgotoindex;
    }
}
