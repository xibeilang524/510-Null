using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ParkingDebugTool
{
    public partial class UCSection : UserControl
    {
        public UCSection()
        {
            InitializeComponent();
        }

        #region 私有变量
        private int _Section = 0;
        #endregion

        #region 私有方法
        private void ClearInput()
        {
            foreach(Control ctrl in Controls)
            {
                if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).Checked = true;
                }
                else if (ctrl is Ralid.GeneralLibrary.WinformControl.HexTextBox)
                {
                    ((Ralid.GeneralLibrary.WinformControl.HexTextBox)ctrl).HexValue = new byte[16];
                }
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置扇区
        /// </summary>
        public int Section
        {
            get
            {
                return _Section;
            }
            set
            {
                _Section = value;
                this.lblText.Text = string.Format("扇区 {0}", value.ToString());
            }
        }

        /// <summary>
        /// 获取或设置扇区数据
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<byte[]> SectionData
        {
            get
            {
                List<byte[]> data=new List<byte[]>();
                for(int i=0;i<3;i++)
                {
                    data.Add(null);
                }
                if (ckbblock0.Checked)
                {
                    data[0] = txtBlock0.HexValue;
                }
                if (ckbblock1.Checked)
                {
                    data[1] = txtBlock1.HexValue;
                }
                if (ckbblock2.Checked)
                {
                    data[2] = txtBlock2.HexValue;
                }
                return data;
            }
            set
            {
                if (value.Count == 3)
                {
                    ClearInput();

                    ckbblock0.Checked = value[0] != null && value[0].Length == 16;
                    if (ckbblock0.Checked)
                    {
                        txtBlock0.HexValue = value[0];
                    }

                    ckbblock1.Checked = value[1] != null && value[1].Length == 16;
                    if (ckbblock1.Checked)
                    {
                        txtBlock1.HexValue = value[1];
                    }

                    ckbblock2.Checked = value[2] != null && value[2].Length == 16;
                    if (ckbblock2.Checked)
                    {
                        txtBlock2.HexValue = value[2];
                    }
                }
            }
        }
        #endregion

        #region 公共方法
        public bool CheckInput()
        {
            if (ckbblock0.Checked&&txtBlock0.HexValue.Length!=16)
            {
                MessageBox.Show("请输入正确的块0数据");
                txtBlock0.Focus();
                return false;
            }

            if (ckbblock1.Checked && txtBlock1.HexValue.Length != 16)
            {
                MessageBox.Show("请输入正确的块1数据");
                txtBlock1.Focus();
                return false;
            }

            if (ckbblock2.Checked && txtBlock2.HexValue.Length != 16)
            {
                MessageBox.Show("请输入正确的块2数据");
                txtBlock0.Focus();
                return false;
            }

            return true;
        }
        #endregion
    }
}
