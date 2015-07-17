using System;
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
using Ralid.Park.BusinessModel.SearchCondition;
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
        }

        /// <summary>
        /// 网络型下载卡片
        /// </summary>
        private bool SyncCards(List<CardInfo> cards)
        {
            bool result = true;
            int entranceCount = 0;
            foreach (EntranceInfo entrance in _Entrances)
            {
                IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                if (pad != null)
                {
                    pad.WaitingCommandServiceEnable(false);
                    try
                    {
                        bool success = true;
                        if (_DownLoadAll)
                        {
                            //全部下载时，下载前需要清空卡片名单
                            CancelNeedWaiting = true;
                            NotifyMessage(string.Format("{0} {1} {2}", Resources.Resource1.FrmDownLoadAllCards_Entrance, entrance.EntranceName, Resources.Resource1.FrmDownloadAllCards_Clear));
                            success = pad.ClearCardsToEntrance(entrance.EntranceID);//删除所有卡片
                            CancelWaitingEvent.Set();
                            CancelNeedWaiting = false;
                        }
                        if (success)
                        {
                            int currentcount = 0;
                            int savecount = 16;
                            int failcount = 0;
                            List<CardInfo> FailCards = new List<CardInfo>();

                            do
                            {
                                List<CardInfo> infos = new List<CardInfo>();
                                for (int i = 0; i < savecount && currentcount < cards.Count; i++)
                                {
                                    infos.Add(cards[currentcount]);
                                    currentcount++;
                                }

                                if (!pad.SaveCardsToEntrance(entrance.EntranceID, infos, _DownLoadCard ? ActionType.Add : ActionType.Delete))
                                {
                                    success = false;
                                    failcount += infos.Count;
                                    FailCards.AddRange(infos);
                                }

                                string msg = _DownLoadCard ? Resources.Resource1.FrmDownloadAllCards_CardProcessing : Resources.Resource1.FrmDownLoadAllCards_Delete;
                                msg = string.Format(msg, string.Format(" {0}/{1} {2}:{3}", currentcount, cards.Count, Resources.Resource1.FrmDownLoadAllCards_Fail, failcount));
                                NotifyMessage(string.Format("{0} {1} {2}", Resources.Resource1.FrmDownLoadAllCards_Entrance, entrance.EntranceName, msg));

                                NotifyProgress(entranceCount * cards.Count + currentcount);
                                TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - _Begin.Ticks);
                                NotifyTime(ts);

                                //Thread.Sleep(50);
                            }
                            while (currentcount < cards.Count);

                            //if (FailCards.Count > 0)
                            //{
                            //    WaitingCommandBLL wcBll = new WaitingCommandBLL(AppSettings.CurrentSetting.CurrentMasterConnect);
                            //    foreach (CardInfo fcard in FailCards)
                            //    {
                            //        WaitingCommandInfo wcInfo = new WaitingCommandInfo();
                            //        wcInfo.EntranceID = entrance.EntranceID;
                            //        wcInfo.Command = _DownLoadCard ? BusinessModel.Enum.CommandType.AddCard : BusinessModel.Enum.CommandType.DeleteCard;
                            //        wcInfo.CardID = fcard.CardID;
                            //        wcBll.DeleteAndInsert(wcInfo);
                            //    }
                            //}
                        }
                        result = success ? result : false;
                        NotifyHardwareTreeEntrance(entrance.EntranceID, success);
                    }
                    catch (Exception ex)
                    {
                        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                    }

                    pad.WaitingCommandServiceEnable(true);
                }
                entranceCount++;
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

        private void NotifyTime(TimeSpan ts)
        {
            Action<TimeSpan> action = delegate(TimeSpan s)
            {
                this.label3.Text = string.Format(Resources.Resource1.FrmDownloadAllCards_Seconds, (int)s.TotalSeconds);
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, ts);
            }
            else
            {
                action(ts);
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
            this.label3.Text = string.Empty;
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
            this.hardwareTree1.ExpandRootOnly();
            Init();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_DownLoadAll)
            {
                CardBll bll = new CardBll(AppSettings.CurrentSetting.ParkConnect);
                //Cards = bll.GetAllCards().QueryObjects;

                //获取所有脱机时脱机处理的卡片，在线处理的卡片不下发，过滤掉在线处理的卡片
                CardSearchCondition search = new CardSearchCondition();
                //search.Status = CardStatus.Enabled | CardStatus.Disabled | CardStatus.Loss | CardStatus.Recycled;
                search.OnlineHandleWhenOfflineMode = false;
                Cards = bll.GetCards(search).QueryObjects;
            }
            //if (Cards != null && Cards.Count > 0 && DownLoadCard)
            //{
            //    //下发卡片时，在线处理的卡片不下发，过滤掉在线处理的卡片
            //    Cards = Cards.Where(c => !c.OnlineHandleWhenOfflineMode).ToList();

            //    //IEnumerable<CardInfo> cardsIEnum = Cards.Where(c => !c.OnlineHandleWhenOfflineMode);
            //    ////临时性的车牌名单不下发
            //    //cardsIEnum = cardsIEnum.Where(c => c.ListType == CardListType.Card || !c.CardType.IsTempCard);

            //    //Cards = cardsIEnum.ToList();
            //}
            if (Cards != null && Cards.Count > 0)
            {
                btnOk.Enabled = false;
                btnCancel.Enabled = true;
                this.btnCancel.Text = Resources.Resource1.Form_Stop;
                _Parks = hardwareTree1.GetSelectedParks();
                _Entrances = hardwareTree1.GetSelectedEntrances();
                _Begin = DateTime.Now;
                this.progressBar1.Maximum = Cards.Count * _Entrances.Count;
                this.progressBar1.Value = 0;
                this.progressBar1.Visible = true;
                SyncCards_Thread = new Thread(SyncAllCards);
                SyncCards_Thread.Start();
            }
            else
            {
                if (DownLoadCard)
                {
                    this.label2.Text = Resources.Resource1.FrmDownLoadAllCards_DownloadCardsNote;
                    this.label3.Text = Resources.Resource1.FrmDownLoadAllCards_NotDownloadCards;
                }
                else
                {
                    this.label3.Text = Resources.Resource1.FrmDownLoadAllCards_NotDeleteCards;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CancelNeedWaiting)
            {
                this.Cursor = Cursors.WaitCursor;
                NotifyMessage(Resources.Resource1.FrmDownLoadBase_StopDownload);
                this.btnCancel.Enabled = false;
                //由于清空卡片命令的超时时间是80秒，所以这里就需要等待80秒了
                CancelWaitingEvent.WaitOne(80000);

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
