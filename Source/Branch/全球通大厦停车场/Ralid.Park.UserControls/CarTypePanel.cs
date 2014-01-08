using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.UserControls
{
    public partial class CarTypePanel : UserControl
    {
        public CarTypePanel()
        {
            InitializeComponent();
            this.Size = new Size(212, 3);
        }

        #region 私有变量
        private byte _SelectedCarType;
        #endregion

        #region 私有方法
        private void LayoutCarTypeButtons(List<Button> buttons)
        {
            int padding = 3;
            int buttonHeight = 30;
            int buttongWidth = (this.Width - padding * 3) / 2;  //根据整个控件的大小来确定按钮的大小
            int totalHeight = padding;

            Point p = new Point(padding, padding);
            for (int i = 0; i < buttons.Count; i++)
            {
                int rows = (i / 2) + 1;  //表示目前的行数
                if (i % 2 == 0) //换行
                {
                    p = new Point(padding, padding + (buttonHeight + padding) * (rows - 1));  //新行的起始位置
                    totalHeight += buttonHeight + padding;
                }
                else   //不换行在第二列
                {
                    p = new Point(padding + buttongWidth + padding, padding + (buttonHeight + padding) * (rows - 1));  //两个控件之间间隔三个leftPad
                }
                buttons[i].Location = p;
                buttons[i].Size = new Size(buttongWidth, buttonHeight);
            }
            this.Size = new Size(this.Width, totalHeight);
        }

        private void b_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                this.SelectedCarType = Convert.ToByte(b.Tag);
                if (this.CarTypeSelectedChanged != null)
                {
                    this.CarTypeSelectedChanged(this, EventArgs.Empty);
                }
            }
        }
        #endregion

        #region 重写基类方法
        protected override void OnSizeChanged(EventArgs e)
        {
            List<Button> buttons=new List<Button> ();
            foreach (Control control in this.Controls)
            {
                if (control is Button) buttons.Add(control as Button);
            }
            LayoutCarTypeButtons(buttons);
            base.OnSizeChanged(e);
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置选中的车型
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Byte SelectedCarType
        {
            get
            {
                return _SelectedCarType;
            }
            set
            {
                foreach (Control c in this.Controls)
                {
                    if (c is Button)
                    {
                        if (Convert.ToByte(c.Tag) == value)
                        {
                            c.BackColor = System.Drawing.Color.Fuchsia;
                        }
                        else
                        {
                            c.BackColor = System.Drawing.SystemColors.ButtonFace;
                        }
                    }
                }
                _SelectedCarType = value;
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 当选中的车型改变时产生此事件
        /// </summary>
        public event EventHandler CarTypeSelectedChanged;
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            List<Button> buttons = new List<Button>();
            foreach (CarType carType in CarTypeSetting.Current.CarTypes)
            {
                Button b = new Button();
                b.Click += new EventHandler(b_Click);
                b.BackColor = System.Drawing.SystemColors.ButtonFace;
                b.Font = new System.Drawing.Font("宋体", 12, FontStyle.Bold);
                b.Tag = carType.ID;
                b.Text = string.Format("{0}[{1}]", carType.Description, carType.ID);
                this.Controls.Add(b);
                buttons.Add(b);
            }
            LayoutCarTypeButtons(buttons);
        }
        /// <summary>
        /// 选择一种车型
        /// </summary>
        /// <param name="carType"></param>
        public void Select(byte carType)
        {
            this.SelectedCarType = carType;
            if (this.CarTypeSelectedChanged != null)
            {
                this.CarTypeSelectedChanged(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
