using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 服务器切换记录类
    /// </summary>
    public class ServerSwitchRecord
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置记录ID
        /// </summary>
        public int RecordID { get; set; }
        /// <summary>
        /// 获取或设置停车场ID
        /// </summary>
        public int ParkID { get; set; }
        /// <summary>
        /// 获取或设置切换时间
        /// </summary>
        public DateTime SwitchDateTime { get; set; }
        /// <summary>
        /// 获取或设置上一次切换时间
        /// </summary>
        public DateTime? LastDateTime { get; set; }
        /// <summary>
        /// 获取或设置切换的服务器IP
        /// </summary>
        public string SwitchServerIP { get; set; }
        /// <summary>
        /// 获取或设置上一次切换的服务器IP
        /// </summary>
        public string LastIP { get; set; }
        /// <summary>
        /// 获取或设置切换的服务器使用状态
        /// </summary>
        public HostStandbyStatus SwitchStatus { get; set; }
        /// <summary>
        /// 获取或设置上一次服务器的使用状态
        /// </summary>
        public HostStandbyStatus LastStatus { get; set; }
        /// <summary>
        /// 获取或设置上短信发送状态
        /// </summary>
        public SMSSendStatus SMSStatus { get; set; }
        /// <summary>
        /// 获取或设置切换操作员
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 获取或设置切换工作站ID
        /// </summary>
        public string StationID { get; set; }
        /// <summary>
        /// 获取或设置备份说明
        /// </summary>
        public string Memo { get; set; }
        #endregion
    }
}
