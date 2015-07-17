using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 数据库连接管理类
    /// </summary>
    public class DataBaseConnectionsManager
    {
        #region 静态属性
        private static DataBaseConnectionsManager _current;

        /// <summary>
        /// 获取或设置当前数据库连接管理类实例
        /// </summary>
        public static DataBaseConnectionsManager Current
        {
            get
            {
                if (_current == null) _current = new DataBaseConnectionsManager();
                return _current;
            }
            set
            {
                _current = value;
            }
        }
        #endregion

        #region 构造函数
        public DataBaseConnectionsManager()
        { 
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置主数据库连接状态
        /// </summary>
        public DataBaseConnectionStatus MasterStatus { get; set; }
        /// <summary>
        /// 获取或设置备用数据库连接状态
        /// </summary>
        public DataBaseConnectionStatus StandbyStatus { get; set; }
        /// <summary>
        /// 获取或设置图片数据库连接状态
        /// </summary>
        public DataBaseConnectionStatus ImageDBStatus { get; set; }
        #endregion

        #region 公共只读属性
        /// <summary>
        /// 获取是否已连接上主数据库
        /// </summary>
        public bool MasterConnected
        {
            get
            {
                return MasterStatus == DataBaseConnectionStatus.Connected;
            }
        }
        /// <summary>
        /// 获取是否已连接上备用数据库
        /// </summary>
        public bool StandbyConnected
        {
            get
            {
                return StandbyStatus == DataBaseConnectionStatus.Connected;
            }
        }
        /// <summary>
        /// 获取是否两个数据库都连接上或没有备用数据库
        /// </summary>
        public bool BothCconnectedOrNoStandby
        {
            get
            {
                return MasterStatus == DataBaseConnectionStatus.Connected && StandbyStatus != DataBaseConnectionStatus.Disconnect;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 连接字符串是否主数据库连接字符串
        /// </summary>
        /// <param name="connectstr"></param>
        /// <returns></returns>
        public bool IsMasterConnectionString(string connectstr)
        {
            return !string.IsNullOrEmpty(connectstr) && connectstr == AppSettings.CurrentSetting.MasterParkConnect;
        }
        /// <summary>
        /// 连接字符串是否备用数据库连接字符串
        /// </summary>
        /// <param name="connectstr"></param>
        /// <returns></returns>
        public bool IsStandbyConnectionString(string connectstr)
        {
            return !string.IsNullOrEmpty(connectstr) && connectstr == AppSettings.CurrentSetting.StandbyParkConnect;
        }
        #endregion
    }
}
