using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.UI.Resources;

namespace Ralid.Park.UI
{
    public partial class FrmVideoSourceDetail : FrmDetailBase
    {
        public FrmVideoSourceDetail()
        {
            InitializeComponent();
        }

        public EntranceInfo Entrance { get; set; }

        #region 重写基类方法
        protected override void InitControls()
        {
            if (IsAdding)
            {
                this.Text = Resources.Resource1.Form_Add;
            }
            RoleInfo role = OperatorInfo.CurrentOperator.Role;
            this.btnOk.Enabled = role.Permit(Permission.EditVideo);
        }

        protected override void ItemShowing()
        {
            VideoSourceInfo info = (VideoSourceInfo)UpdatingItem;
            this.txtName.Text = info.VideoName;
            this.txtMediaSource.Text = info.MediaSource;
            this.txtChannel.IntergerValue = info.Channel;
            this.txtUserName.Text = info.UserName;
            this.txtPassword.Text = info.Password;
            this.txtControlPort.Text = info.ControlPort.ToString();
            this.txtStreamPort.Text = info.StreamPort.ToString();
            this.txtConnectTimeOut.Text = info.ConnectTimeOut.ToString();
            this.chkForCarPlate.Checked = info.IsForCarPlate;
            this.Text = info.VideoName;
        }

        protected override object GetItemFromInput()
        {
            VideoSourceInfo info = null;
            if (IsAdding)
            {
                info = new VideoSourceInfo();
            }
            else
            {
                info = UpdatingItem as VideoSourceInfo;
            }
            info.VideoName = this.txtName.Text.Trim();
            info.MediaSource = this.txtMediaSource.Text.Trim();
            info.Channel = this.txtChannel.IntergerValue;
            info.UserName = this.txtUserName.Text.Trim();
            info.Password = this.txtPassword.Text.Trim();
            info.IsForCarPlate = this.chkForCarPlate.Checked;
            info.ConnectTimeOut = this.txtConnectTimeOut.IntergerValue;
            info.ControlPort = this.txtControlPort.IntergerValue;
            info.StreamPort = this.txtStreamPort.IntergerValue;
            if (Entrance != null)
            {
                info.EntranceID = Entrance.EntranceID;
            }
            return info;
        }

        protected override CommandResult AddItem(object addingItem)
        {
            VideoSourceBll bll = new VideoSourceBll(AppSettings.CurrentSetting.ParkConnect);
            return bll.Insert((VideoSourceInfo)addingItem);
        }

        protected override CommandResult UpdateItem(object updatingItem)
        {
            VideoSourceBll bll = new VideoSourceBll(AppSettings.CurrentSetting.ParkConnect);
            return bll.Update((VideoSourceInfo)updatingItem);
        }

        protected override bool CheckInput()
        {
            string txt;
            txt = this.txtName.Text.Trim();
            if (string.IsNullOrEmpty(txt))
            {
                MessageBox.Show(Resource1.FrmVideosourceDetail_InvalidName);
                return false;
            }
            txt = this.txtMediaSource.Text.Trim();
            if (string.IsNullOrEmpty(txt))
            {
                MessageBox.Show(Resource1.FrmVideosourceDetail_InvalidIP);
                return false;
            }
            if (txtChannel.IntergerValue < 0)
            {
                MessageBox.Show(Resource1.FrmVideosourceDetail_InvalidChannel);
                return false;
            }
            txt = this.txtUserName.Text.Trim();
            if (string.IsNullOrEmpty(txt))
            {
                MessageBox.Show(Resource1.FrmVideosourceDetail_InvalidUserName);
                return false;
            }
            txt = this.txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(txt))
            {
                MessageBox.Show(Resource1.FrmVideosourceDetail_InvalidPwd);
                return false;
            }
            else if (txtControlPort.IntergerValue < 1024 && txtControlPort.IntergerValue > 65535)
            {
                MessageBox.Show(Resource1.FrmVideosourceDetail_InvalidControlPort);
                return false;
            }
            else if (txtStreamPort.IntergerValue < 1024 && txtStreamPort.IntergerValue > 65535)
            {
                MessageBox.Show(Resource1.FrmVideosourceDetail_InvalidStreamPort);
                return false;
            }

            if (txtConnectTimeOut.IntergerValue < 0)
            {
                MessageBox.Show(Resource1.FrmVideosourceDetail_InvalidConnectTimeout);
                return false;
            }
            return true;
        }
        #endregion
    }
}
