using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading ;
using System.Runtime .InteropServices ;
using Ralid.Park .BusinessModel .Result ;
using Ralid.Park .DAL .IDAL ;

namespace Ralid.Park.BLL
{
    public class DatetimeSyncService
    {
        #region  DLLImport 声明
        [DllImport("Kernel32.dll")]
        private static extern bool SetSystemTime(ref SystemTime sysTime);
        [DllImport("Kernel32.dll")]
        private static extern bool SetLocalTime(ref SystemTime sysTime);
        #endregion

        #region 构造函数
        public DatetimeSyncService(string repoUri)
        {
            _Provider = ProviderFactory.Create<IServerDatetimeProvider>(repoUri);
            _SyncInterval = 5;
        }
        #endregion

        #region 私有变量
        private IServerDatetimeProvider _Provider;
        private Thread _SyncTimeThread;
        private bool _SyncTimeStarted;
        private int _SyncInterval;
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
                        DateTime? dt;
                        CommandResult ret = GetServerDateTime(out dt);
                        if (ret.Result == ResultCode.Successful)
                        {
                            SystemTime st = new SystemTime();
                            st.wYear = (ushort)dt.Value.Year;
                            st.wMonth = (ushort)dt.Value.Month;
                            st.wDay = (ushort)dt.Value.Day;
                            st.wHour = (ushort)dt.Value.Hour;
                            st.wMinute = (ushort)dt.Value.Minute;
                            st.wSecond = (ushort)dt.Value.Second;
                            st.wMiliseconds = (ushort)dt.Value.Millisecond;
                            st.wDayOfWeek = (ushort)dt.Value.DayOfWeek;
                            SetLocalTime(ref st);
                        }
                    }
                    catch (Exception ex)
                    {
                        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                    }
                    Thread.Sleep(SyncInterval * 60 * 1000);
                }
            }
            catch (ThreadAbortException)
            {

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
        /// 获取服务器当前时间,如果方法执行时间超过3S，则失败，返回的时间不可用
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public CommandResult GetServerDateTime(out DateTime? dt)
        {
            DateTime dt1 = DateTime.Now;
            CommandResult ret = _Provider.GetServerDateTime(out dt);
            DateTime dt2 = DateTime.Now;
            TimeSpan ts = new TimeSpan(dt2.Ticks - dt1.Ticks);
            if (ts.TotalSeconds >= 3)
            {
                dt = null;
                return new CommandResult(ResultCode.Fail, "超时");
            }
            else
            {
                return ret;
            }
        }

        /// <summary>
        /// 开始时间同步
        /// </summary>
        public void Start()
        {
            if (!_SyncTimeStarted)
            {
                _SyncTimeThread = new Thread(SyncTime_Thread);
                _SyncTimeThread.IsBackground = true;
                _SyncTimeThread.Start();
            }
        }

        /// <summary>
        /// 结束时间同步
        /// </summary>
        public void Stop()
        {
            _SyncTimeStarted = false;
            if (_SyncTimeThread != null && (_SyncTimeThread.ThreadState == ThreadState.Running || _SyncTimeThread.ThreadState == ThreadState.WaitSleepJoin))
            {
                _SyncTimeThread.Abort();
            }
        }
        #endregion
    }

    /// <summary>
    /// 系统时间结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct SystemTime
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMiliseconds;
    }
}
