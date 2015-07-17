using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ralid.Park.UserControls
{
    public partial class VideoTypeComboBox : ComboBox
    {
        public VideoTypeComboBox()
        {
            InitializeComponent();
        }

        public VideoTypeComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private bool _showDefault;

        public void Init(bool showDefault)
        {
            _showDefault = showDefault;
            this.Items.Clear();
            if (showDefault)
            {
                this.Items.Add(Resources.Resource1.Default);
            }
            this.Items.Add(Resources.Resource1.Type + " " + "A");
            this.Items.Add(Resources.Resource1.Type + " " + "X");
            this.Items.Add(Resources.Resource1.Type + " " + "J");
            this.Items.Add(Resources.Resource1.Type + " " + "D");

            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// 获取或设置选择的视频服务类型
        /// </summary>
        [Browsable(false)]
        [Localizable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? VideoType
        {
            get
            {
                //最小的类型索引，当显示默认时为1
                int minIndex = _showDefault ? 1 : 0;
                if (this.SelectedIndex < minIndex)
                {
                    return null;
                }
                else
                {
                    return this.SelectedIndex - minIndex;
                }
            }
            set
            {
                //最小的类型索引，当显示默认时为1
                int minIndex = _showDefault ? 1 : 0;

                if (value == null)
                {
                    this.SelectedIndex = -1 + minIndex;
                }
                else if (value.Value < this.Items.Count - minIndex)
                {
                    this.SelectedIndex = value.Value + minIndex;
                }
                else
                {
                    this.SelectedIndex = -1 + minIndex;
                }
            }
        }
    }
}
