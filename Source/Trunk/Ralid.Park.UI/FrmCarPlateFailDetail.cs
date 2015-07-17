using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BLL;
using Ralid.Park.UserControls;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.ParkAdapter;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.UI.EventArgument;

namespace Ralid.Park.UI
{
    public partial class FrmCarPlateFailDetail : Form
    {
        #region 构造函数
        public FrmCarPlateFailDetail()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private CardEventReport _ProcessingEvent;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置要处理的事件
        /// </summary>
        public CardEventReport ProcessingEvent
        {
            get
            {
                return _ProcessingEvent;
            }
            set
            {
                _ProcessingEvent = value;
                if (value != null)
                {
                    ShowFrmCarPlateFailDetail(value);
                }
            }
        }
        #endregion

        #region 事件
        public event CardEventProcessedHandler CardEventProcessed;
        #endregion

        #region 私有方法
        private void ShowDetail(string cardID,string enterCarPlate, string exitCarPlate, DateTime? enterDateTime, DateTime? exitDateTime)
        {
            this.txtCardID.Text = cardID;
            this.lblEnterCarPlate.Text = enterCarPlate;
            this.lblExitCarPlate.Text = exitCarPlate;
            this.lblEnterDateTime.Text = string.Empty;
            this.lblExitDateTime.Text = string.Empty;

            if (enterDateTime != null)
            {
                this.lblEnterDateTime.Text = enterDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (exitDateTime != null)
            {
                this.lblExitDateTime.Text = exitDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            SnapShotBll ssbll = new SnapShotBll(AppSettings.CurrentSetting.ImageDBConnStr);
            this.picIn.Clear();
            if (enterDateTime != null)
            {
                List<SnapShot> imgs = ssbll.GetSnapShots(enterDateTime.Value, cardID);
                if (imgs != null && imgs.Count > 0)
                {
                    this.picIn.ShowSnapShots(imgs);
                }
            }

            this.picOut.Clear();
            if (exitDateTime != null)
            {
                List<SnapShot> outImgs = ssbll.GetSnapShots(exitDateTime.Value, cardID);
                if (outImgs != null && outImgs.Count > 0)
                {
                    this.picOut.ShowSnapShots(outImgs);
                }
            }
        }
        #endregion

        #region 事件处理

        private void ShowFrmCarPlateFailDetail(CardEventReport info)
        {

            //this.txtCardID.Text = info.CardID;
            //this.lblEnterCarPlate.Text = info.LastCarPlate;
            //this.lblExitCarPlate.Text = info.CarPlate;
            //if (info.LastDateTime != null)
            //{
            //    this.lblEnterDateTime.Text = info.LastDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            //}
            //this.lblExitDateTime.Text = info.EventDateTime.ToString("yyyy-MM-dd HH:mm:ss");            

            //SnapShotBll ssbll=new SnapShotBll(AppSettings.CurrentSetting.ParkConnect);

            //this.picIn.Clear();
            //if (info.LastDateTime != null)
            //{
            //    List<SnapShot> imgs = ssbll.GetSnapShots(info.LastDateTime.Value,info.CardID);
            //    if (imgs != null && imgs.Count > 0)
            //    {
            //        this.picIn.ShowSnapShots(imgs);
            //    }
            //}

            //this.picOut.Clear();
            //if (info.EventDateTime != null)
            //{
            //    List<SnapShot> outImgs = ssbll.GetSnapShots(info.EventDateTime, info.CardID);
            //    if (outImgs != null && outImgs.Count > 0)
            //    {
            //        this.picOut.ShowSnapShots(outImgs);
            //    }
            //}

            if (info.IsExitEvent)
            {
                ShowDetail(info.CardID, info.LastCarPlate, info.CarPlate, info.LastDateTime, info.EventDateTime);
            }
            else
            {
                ShowDetail(info.CardID, info.CarPlate, string.Empty, info.EventDateTime, null);
            }
            ucVideoes.ShowVideoes(ParkBuffer.Current.GetEntrance(info.EntranceID).VideoSources);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (ProcessingEvent != null)
            {
                if (this.CardEventProcessed != null)
                {
                    this.CardEventProcessed(this, new CardEventProcessedArgs(ProcessingEvent, 0));
                }
                this.ucVideoes.Clear();
                this.Hide();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (ProcessingEvent != null)
            {
                this.ucVideoes.Clear();
                this.Hide();
            }
        }

        private void FrmCarPlateFailDetail_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F11:
                    if (this.btnClear.Enabled)
                    {
                        btnClear_Click(this.btnClear, EventArgs.Empty);
                    }
                    break;
                case Keys.F12:
                    if (this.btnClose.Enabled)
                    {
                        btnClose_Click(this.btnClose, EventArgs.Empty);
                    }
                    break;
            }
        }

        private void FrmCarPlateFailDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
        #endregion
    }
}
