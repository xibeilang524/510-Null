﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.ParkAdapter;

namespace Ralid.Park.UI
{
    public partial class FrmDownLoadAllCards : Form
    {
        public FrmDownLoadAllCards()
        {
            InitializeComponent();
        }

        #region 私有变量
        private DateTime _Begin;
        private List<ParkInfo> _Parks;
        private Thread SyncCards_Thread;
        private List<EntranceInfo> _Entrances;
        private bool _DownLoadAll;
        private bool _DownLoadCard = true;
        private bool CancelNeedWaiting = false;//停止下载时需要等待已执行的命令完成
        private AutoResetEvent CancelWaitingEvent = new AutoResetEvent(false);//等待已执行的命令完成的通知事件
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置下载的卡片
        /// </summary>
        public List<CardInfo> Cards { get; set; }
        /// <summary>
        /// 获取或设置是否将卡片下发到控制器，为False时表示将卡片从控制器中删除
        /// </summary>
        public bool DownLoadCard 
        {
            get { return _DownLoadCard; }
            set { _DownLoadCard = value; }
        }
        #endregion

        #region 私有方法
        private void SyncAllCards()
        {
            try
            {
                bool success = true;
                List<CardInfo> cards = Cards;
                if (cards == null || cards.Count == 0) return;
                cards = (from card in cards orderby card.Index ascending select card).ToList();
                if (cards != null && cards.Count > 0)
                {
                    success = SyncCards(cards);
                }
                if (success)
                {
                    if (_DownLoadCard)
                    {
                        NotifyMessage(Resources.Resource1.FrmDownloadAllCards_Complete);
                    }
                    else
                    {
                        NotifyMessage(string.Format(Resources.Resource1.FrmDownLoadAllCards_DeleteFinish));
                    }
                }
                else
                {
                    if (_DownLoadCard)
                    {
                        NotifyMessage(string.Format(Resources.Resource1.FrmDownLoadAllCards_NotDownloadAll));
                    }
                    else
                    {
                        NotifyMessage(string.Format(Resources.Resource1.FrmDownLoadAllCards_NotDeleteAll));
                    }

                }
                NotifyProgress(this.progressBar1.Maximum);
                this.btnCancel.Text = Resources.Resource1.Form_Cancel;
            }
            catch (Exception)
            {

            }
            this.timer1.Enabled = false;
        }

        /// <summary>
        /// 网络型下载卡片
        /// </summary>
        private bool SyncCards(List<CardInfo> cards)
        {
            bool result = true;
            foreach (EntranceInfo entrance in _Entrances)
            {
                IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                if (pad != null)
                {
                    bool success = true;
                    if (_DownLoadAll)
                    {
                        //全部下载时，下载前需要清空卡片名单
                        CancelNeedWaiting = true;
                        NotifyMessage(string.Format("{0} {1} {2}",Resources.Resource1.FrmDownLoadAllCards_Entrance, entrance.EntranceName, Resources.Resource1.FrmDownloadAllCards_Clear));
                        success = pad.ClearCardsToEntrance(entrance.EntranceID);//删除所有卡片
                        CancelWaitingEvent.Set();
                        CancelNeedWaiting = false;
                    }
                    if (success)
                    {
                        foreach (CardInfo card in cards)
                        {
                            string msg = _DownLoadCard ? Resources.Resource1.FrmDownloadAllCards_CardProcessing : Resources.Resource1.FrmDownLoadAllCards_Delete;
                            NotifyMessage(string.Format("{0} {1} {2}", Resources.Resource1.FrmDownLoadAllCards_Entrance, entrance.EntranceName, string.Format(msg, card.CardID)));
                            if (_DownLoadCard)
                            {
                                success = pad.SaveCardToEntrance(entrance.EntranceID, card, ActionType.Add) ? success : false;
                            }
                            else
                            {
                                success = pad.DeleteCardToEntrance(entrance.EntranceID, card) ? success : false;//删除选定卡片 
                            }
                            NotifyProgress(null);
                        }
                    }
                    result = success ? result : false;
                    NotifyHardwareTreeEntrance(entrance.EntranceID, success);
                }
            }
            return result;
        }

        private void NotifyMessage(string msg)
        {
            Action<string> action = delegate(string s)
            {
                this.label2.Text = msg;
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, msg);
            }
            else
            {
                action(msg);
            }
        }

        private void NotifyProgress(int? value)
        {
            Action<int?> action = delegate(int? v)
            {
                if (v.HasValue)
                {
                    this.progressBar1.Value = v.Value;
                }
                else
                {
                    this.progressBar1.Value++;
                }
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, value);
            }
            else
            {
                action(value);
            }
        }

        private void NotifyHardwareTreeEntrance(int entranceID, bool success)
        {
            Action<int, bool> action = delegate(int eID, bool s)
            {
                TreeNode node = hardwareTree1.GetEntranceNode(eID);
                if (node != null) node.ForeColor = s ? Color.Green : Color.Red;
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, entranceID, success);
            }
            else
            {
                action(entranceID, success);
            }
        }

        private void NotifyHardwareTreePark(int parkID, bool success)
        {
            Action<int, bool> action = delegate(int pID, bool s)
            {
                TreeNode node = hardwareTree1.GetParkNode(pID);
                if (node != null) node.ForeColor = s ? Color.Green : Color.Red;
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, parkID, success);
            }
            else
            {
                action(parkID, success);
            }
        }

        private void Init()
        {
            if (_DownLoadCard)
            {
                if (_DownLoadAll) this.Text = Resources.Resource1.FrmDownLoadAllCards_DownloadCards;
                else this.Text = Resources.Resource1.FrmDownLoadAllCards_DownloadCard;
            }
            else
            {
                if (_DownLoadAll) this.Text = Resources.Resource1.FrmDownLoadAllCards_DeleteCards;
                else this.Text = Resources.Resource1.FrmDownLoadAllCards_DeleteCard;
            }
            this.label2.Text = string.Empty;
            this.timer1.Enabled = false;
            this.btnOk.Enabled = true;
            this.btnCancel.Enabled = false;
            this.progressBar1.Visible = false;
            this.btnCancel.Text = Resources.Resource1.Form_Cancel;
        }
        #endregion

        private void FrmDownLoadAllCards_Load(object sender, EventArgs e)
        {
            this.hardwareTree1.ShowEntrance = true;
            _DownLoadAll = Cards == null;
            this.hardwareTree1.Init();
            Init();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_DownLoadAll)
            {
                CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                Cards = bll.GetAllCards().QueryObjects;
            }
            if (Cards != null && Cards.Count > 0)
            {
                btnOk.Enabled = false;
                btnCancel.Enabled = true;
                this.btnCancel.Text = Resources.Resource1.Form_Stop;
                _Parks = hardwareTree1.GetSelectedParks();
                _Entrances = hardwareTree1.GetSelectedEntrances();
                this.timer1.Enabled = true;
                this.timer1.Interval = 1000;
                _Begin = DateTime.Now;
                this.progressBar1.Maximum = Cards.Count * _Entrances.Count;
                this.progressBar1.Value = 0;
                this.progressBar1.Visible = true;
                SyncCards_Thread = new Thread(SyncAllCards);
                SyncCards_Thread.Start();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CancelNeedWaiting)
            {
                this.Cursor = Cursors.WaitCursor;
                NotifyMessage(Resources.Resource1.FrmDownLoadBase_StopDownload);
                this.btnCancel.Enabled = false;
                CancelWaitingEvent.WaitOne(15000);

                this.Cursor = Cursors.Arrow;
                this.btnCancel.Enabled = true;
            }
            if (SyncCards_Thread != null && (SyncCards_Thread.ThreadState == ThreadState.Running || SyncCards_Thread.ThreadState == ThreadState.WaitSleepJoin))
            {
                SyncCards_Thread.Abort();
            }
            Init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - _Begin.Ticks);
            this.Text = string.Format(Resources.Resource1.FrmDownloadAllCards_Seconds, (int)ts.TotalSeconds);
        }

        private void FrmDownLoadAllCards_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SyncCards_Thread != null && (SyncCards_Thread.ThreadState == ThreadState.Running || SyncCards_Thread.ThreadState == ThreadState.WaitSleepJoin))
            {
                SyncCards_Thread.Abort();
            }
        }
    }
}
