using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ralid.Park.LocalDataBase.Model;
using Ralid.Park.LocalDataBase.DAL.IDAL;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.LocalDataBase.BLL
{
    /// <summary>
    /// 上传本地数据服务类
    /// </summary>
    public class LDB_UpdateLoaclDataService
    {
        #region 构造函数
        /// <summary>
        /// 上传本地数据服务类
        /// </summary>
        /// <param name="repoUri">本地数据库</param>
        /// <param name="databaseUri">上传到的数据库</param>
        public LDB_UpdateLoaclDataService(string repoUri, string databaseUri)
        {
            _Provider = LDB_ProviderFactory.Create<LDB_ICardPaymentRecordProvider>(repoUri);
            _CardPaymentProvider = ProviderFactory.Create<ICardPaymentRecordProvider>(databaseUri);
            _DataBaseUri = databaseUri;
            _UpdateInterval = 5;
        }
        #endregion

        #region 私有变量
        private LDB_ICardPaymentRecordProvider _Provider;
        private ICardPaymentRecordProvider _CardPaymentProvider;
        private Thread _UpdateThread;
        private bool _UpdateStarted;
        private int _UpdateInterval;
        private string _DataBaseUri;
        private AutoResetEvent _UpdateLoaclDataEvent = new AutoResetEvent(false);
        #endregion

        #region 私有方法
        private void UpdateLocalData_Thread()
        {
            try
            {
                while (true)
                {
                    _UpdateLoaclDataEvent.WaitOne(UpdateInterval * 60 * 1000);

                    if (AppSettings.CurrentSetting.EnableWriteCard)
                    {
                        LDB_CardPaymentRecordBll bll = new LDB_CardPaymentRecordBll(LDB_AppSettings.Current.LDBConnect);
                        try
                        {
                            //如果是主数据库时，需要主数据库连接上
                            if (!DataBaseConnectionsManager.Current.IsMasterConnectionString(_DataBaseUri)
                                || DataBaseConnectionsManager.Current.MasterConnected)
                            {
                                LDB_CardPaymentRecordSearchCondition con = new LDB_CardPaymentRecordSearchCondition();
                                con.UpdateFlag = false;
                                List<LDB_CardPaymentInfo> records = bll.GetItems(con).QueryObjects;
                                if (records != null && records.Count > 0)
                                {
                                    CommandResult result = null;
                                    foreach (LDB_CardPaymentInfo record in records)
                                    {
                                        CardPaymentInfo info = LDB_InfoFactory.CreateCardPaymentInfo(record);
                                        info.UpdateFlag = true;

                                        CardPaymentRecordSearchCondition searchCondition = new CardPaymentRecordSearchCondition();
                                        searchCondition.CardID = info.CardID;
                                        searchCondition.ChargeDateTime = info.ChargeDateTime;

                                        List<CardPaymentInfo> check = _CardPaymentProvider.GetItems(searchCondition).QueryObjects;
                                        if (check == null || check.Count == 0)
                                        {
                                            result = _CardPaymentProvider.Insert(info);
                                        }
                                        else
                                        {
                                            //已存在该记录，可认为插入成功
                                            result = new CommandResult(ResultCode.Successful, string.Empty);
                                        }

                                        if (result.Result == ResultCode.Successful)
                                        {
                                            record.UpdateFlag = true;
                                            bll.Update(record);
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
            }
            catch (ThreadAbortException ex)
            {
                Ralid.GeneralLibrary.LOG.FileLog.Log("系统", "上传本地数据库数据服务停止");
            }
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置上传本地数据的时间间隔（分钟)，默认为5分钟，要在调用Start方法之前设置此参数
        /// </summary>
        public int UpdateInterval
        {
            get { return _UpdateInterval; }
            set
            {
                if (value > 0)
                {
                    _UpdateInterval = value;
                }
                else
                {
                    throw new InvalidOperationException("同步时间间隔有误，必须大于0！");
                }
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 开始时间同步
        /// </summary>
        public void Start()
        {
            if (!_UpdateStarted)
            {
                _UpdateThread = new Thread(UpdateLocalData_Thread);
                _UpdateThread.IsBackground = true;
                _UpdateThread.Start();
            }
        }

        /// <summary>
        /// 结束时间同步
        /// </summary>
        public void Stop()
        {
            _UpdateStarted = false;
            if (_UpdateThread != null && (_UpdateThread.ThreadState == ThreadState.Running || _UpdateThread.ThreadState == ThreadState.WaitSleepJoin))
            {
                _UpdateThread.Abort();
            }
        }

        /// <summary>
        /// 立刻更新一次本地数据到主数据库
        /// </summary>
        public void UpdateLoaclData()
        {
            _UpdateLoaclDataEvent.Set();
        }
        #endregion
    }
}
