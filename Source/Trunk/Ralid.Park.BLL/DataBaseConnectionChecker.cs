using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.BLL
{
    /// <summary>
    /// 用于检查数据库连接的类
    /// </summary>
    public class DataBaseConnectionChecker
    {

        #region 构造函数
        public DataBaseConnectionChecker(string masterRepoUri, string standbyRepoUri)
        {
            _masterRepoUri = masterRepoUri;
            _standbyRepoUri = standbyRepoUri;
            _SyncInterval = 5;
            _CheckConnectionThread = new Thread(CheckConnection_Thread);
            _CheckConnectionThread.IsBackground = true;
        }
        #endregion


        #region 事件
        /// <summary>
        /// 数据库连接状态改变产生的事件
        /// </summary>
        public EventHandler DataBaseStatusChangedEvent;
        #endregion

        #region 私有变量
        private string _masterRepoUri;
        private string _standbyRepoUri;
        private Thread _CheckConnectionThread;
        private bool _CheckConnectionStarted;
        private int _SyncInterval;
        #endregion

        #region 私有方法
        private void CheckConnection_Thread()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        bool connected = false;
                        bool changed = false;

                        if (!string.IsNullOrEmpty(_masterRepoUri))
                        {
                            connected = CheckConnectionAvailableBySql(_masterRepoUri);

                            DataBaseConnectionStatus master = DataBaseConnectionsManager.Current.MasterStatus;
                            DataBaseConnectionsManager.Current.MasterStatus = connected ? DataBaseConnectionStatus.Connected : DataBaseConnectionStatus.Disconnect;

                            if (master != DataBaseConnectionsManager.Current.MasterStatus) changed = true;
                        }
                        if (!string.IsNullOrEmpty(_standbyRepoUri))
                        {
                            connected = CheckConnectionAvailableBySql(_standbyRepoUri);

                            DataBaseConnectionStatus standby = DataBaseConnectionsManager.Current.StandbyStatus;
                            DataBaseConnectionsManager.Current.StandbyStatus = connected ? DataBaseConnectionStatus.Connected : DataBaseConnectionStatus.Disconnect;
                            if (standby != DataBaseConnectionsManager.Current.StandbyStatus) changed = true;
                        }

                        if (changed && DataBaseStatusChangedEvent != null) this.DataBaseStatusChangedEvent(this, EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                    }
                    Thread.Sleep(SyncInterval * 1000);
                }
            }
            catch (ThreadAbortException ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置检查连接的时间间隔（秒)，默认为5秒，要在调用Start方法之前设置此参数
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

        /// <summary>
        /// 获取或设置服务的当前区域资源
        /// </summary>
        public System.Globalization.CultureInfo CurrentUICulture
        {
            get 
            { 
                return  _CheckConnectionThread.CurrentUICulture; 
            }
            set
            {
                if (value != null && _CheckConnectionThread.CurrentUICulture != value)
                {
                    _CheckConnectionThread.CurrentUICulture = value;
                    _CheckConnectionThread.CurrentCulture = value;
                }
            }
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 使用连接数据库方法检查数据库连接状态
        /// </summary>
        /// <param name="repoUri"></param>
        /// <returns></returns>
        public bool CheckConnectionAvailableBySql(string repoUri)
        {
            bool result = true;
            using (SqlConnection conn = new SqlConnection(repoUri))
            {
                try
                {
                    if (AppSettings.CurrentSetting.CheckConnectionWithPing)
                    {
                        IPAddress ip = null;
                        if (IPAddress.TryParse(conn.DataSource, out ip))
                        {
                            Ping ping = new Ping();
                            PingReply reply = ping.Send(conn.DataSource, 3000);
                            result = reply.Status == IPStatus.Success;
                        }
                    }
                    if (result)
                    {
                        conn.Open();
                        result = conn.State == System.Data.ConnectionState.Open;
                    }
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }

        /// <summary>
        /// 开始检查连接
        /// </summary>
        public void Start()
        {
            if (!_CheckConnectionStarted)
            {                
                _CheckConnectionThread.Start();
            }
        }

        /// <summary>
        /// 结束检查连接
        /// </summary>
        public void Stop()
        {
            _CheckConnectionStarted = false;
            if (_CheckConnectionThread != null && (_CheckConnectionThread.ThreadState == ThreadState.Running || _CheckConnectionThread.ThreadState == ThreadState.WaitSleepJoin))
            {
                _CheckConnectionThread.Abort();
            }
        }
        #endregion

    }
}
