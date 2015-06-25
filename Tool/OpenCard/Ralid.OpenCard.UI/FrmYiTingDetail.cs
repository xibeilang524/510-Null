using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park .BusinessModel .Model ;

namespace Ralid.OpenCard.UI
{
    public partial class FrmYiTingDetail :Form 
    {
        public FrmYiTingDetail()
        {
            InitializeComponent();
            comEntrance.Init();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置中山通读卡器IP
        /// </summary>
        public string ReaderID
        {
            get
            {
                return txtID.Text;
            }
            set
            {
                txtID .Text = value;
            }
        }
        /// <summary>
        /// 获取或设置相关停车场通道ID
        /// </summary>
        public int EntranceID
        {
            get
            {
                return comEntrance.SelectedEntranceID;
            }
            set
            {
                comEntrance.SelectedEntranceID = value;
            }
        }
        /// <summary>
        /// 获取相关停车场通道的名称
        /// </summary>
        public string EntranceName
        {
            get
            {
                return comEntrance.SelectedEntranceName;
            }
        }
        /// <summary>
        /// 获取说明信息
        /// </summary>
        public string Memo
        {
            get
            {
                return txtMemo.Text;
            }
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
