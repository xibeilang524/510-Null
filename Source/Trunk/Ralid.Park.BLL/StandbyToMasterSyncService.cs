using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.BLL
{
    /// <summary>
    /// 表示备用数据库同步到主数据库的服务类
    /// </summary>
    public class StandbyToMasterSyncService
    {
        #region 构造函数
        public StandbyToMasterSyncService(string masterUri,string standbyUri)
        {
            _MasterUri = masterUri;
            _StandbyUri = standbyUri;
            _MasterCardBll = new CardBll(masterUri);
            _MasterPaymentBll = new CardPaymentRecordBll(masterUri);
            _StandbyCardBll = new CardBll(standbyUri);
            _StandbyPaymentBll = new CardPaymentRecordBll(standbyUri);
            _SyncInterval = 5;
        }
        #endregion

        #region 私有变量
        private CardBll _MasterCardBll;
        private CardPaymentRecordBll _MasterPaymentBll;
        private CardBll _StandbyCardBll;
        private CardPaymentRecordBll _StandbyPaymentBll;
        private Thread _SyncDataThread;
        private bool _SyncDataStarted;
        private bool _SyncDataPause;//暂停
        private int _SyncInterval;
        private string _MasterUri;
        private string _StandbyUri;
        private AutoResetEvent _SyncDateBaseEvent = new AutoResetEvent(false);
        #endregion

        #region 私有方法
        private void SyncTime_Thread()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        _SyncDateBaseEvent.WaitOne(SyncInterval * 60 * 1000);

                        if (DataBaseConnectionsManager.Current.MasterConnected
                            && DataBaseConnectionsManager.Current.StandbyConnected)
                        {
                            CommandResult result = null;
                            if (!_SyncDataPause)
                            {
                                CardSearchCondition cardcon = new CardSearchCondition();
                                cardcon.UpdateFlag = false;
                                List<CardInfo> sCards = _StandbyCardBll.GetCards(cardcon).QueryObjects;
                                if (sCards != null && sCards.Count > 0)
                                {
                                    foreach (CardInfo sCard in sCards)
                                    {
                                        if (_SyncDataPause) break;
                                        CardInfo mCard = _MasterCardBll.GetCardByID(sCard.CardID).QueryObject;
                                        if (mCard != null)
                                        {
                                            sCard.UpdateFlag = true;
                                            result = _MasterCardBll.UpdateCardPaymentInfo(sCard);
                                            if (result.Result == ResultCode.Successful)
                                            {
                                                _StandbyCardBll.UpdateCard(sCard);
                                            }
                                        }
                                    }
                                }
                            }

                            if (!_SyncDataPause)
                            {
                                CardPaymentRecordSearchCondition paymentcon = new CardPaymentRecordSearchCondition();
                                paymentcon.UpdateFlag = false;
                                List<CardPaymentInfo> sRecords = _StandbyPaymentBll.GetItems(paymentcon).QueryObjects;
                                if (sRecords != null && sRecords.Count > 0)
                                {
                                    foreach (CardPaymentInfo sRecord in sRecords)
                                    {
                                        if (_SyncDataPause) break;
                                        CardPaymentInfo cpInfo = sRecord.Clone();
                                        cpInfo.UpdateFlag = true;
                                        result = _MasterPaymentBll.InsertRecordWithCheck(cpInfo);
                                        if (result.Result == ResultCode.Successful)
                                        {
                                            sRecord.UpdateFlag = true;
                                            _StandbyPaymentBll.Update(sRecord);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
                Ralid.GeneralLibrary.LOG.FileLog.Log("系统", "备用数据库同步到主数据库服务停止");
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置自动同步时同步的时间间隔（分钟)，默认为5分钟，要在调用Start方法之前设置此参数
        /// </summary>
        public int SyncInterval
        {
            get { return _SyncInterval; }
            set
            {
                if (value > 0)
                {
                    _SyncInterval = value;
                }
                else
                {
                    throw new InvalidOperationException(Resource1.DatetimeSyncService_Invalidtime);
                }
            }
        }
        #endregion

        #region 公共方法
        
        /// <summary>
        /// 开始数据同步
        /// </summary>
        public void Start()
        {
            if (!_SyncDataStarted)
            {
                _SyncDataThread = new Thread(SyncTime_Thread);
                _SyncDataThread.IsBackground = true;
                _SyncDataThread.Start();
            }
        }

        /// <summary>
        /// 结束数据同步
        /// </summary>
        public void Stop()
        {
            _SyncDataStarted = false;
            if (_SyncDataThread != null && (_SyncDataThread.ThreadState == ThreadState.Running || _SyncDataThread.ThreadState == ThreadState.WaitSleepJoin))
            {
                _SyncDataThread.Abort();
            }
        }

        /// <summary>
        /// 暂停数据同步
        /// </summary>
        public void Pause()
        {
            _SyncDataPause = true;
        }

        /// <summary>
        /// 恢复数据同步
        /// </summary>
        public void Recover()
        {
            _SyncDataPause = false;
            _SyncDateBaseEvent.Set();//立刻同步一次
        }

        /// <summary>
        /// 立刻同步一次数据库，暂停数据同步不会同步
        /// </summary>
        public void SyncDataBase()
        {
            if(!_SyncDataPause) _SyncDateBaseEvent.Set();
        }
        #endregion
    }
}
