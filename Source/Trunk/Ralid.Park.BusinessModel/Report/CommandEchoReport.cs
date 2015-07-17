using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 表示卡片有效指令的返回消息
    /// </summary>
    [DataContract]
    public class CommandEchoReport:ReportBase
    {
        /// <summary>
        /// 卡号
        /// </summary>
        [DataMember]
        public string CardID { get; set; }

        #region 重写基类方法
        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2} {3}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"), SourceName, Resouce.Resource1.Report_CommandEcho, CardID);
            }
        }
        #endregion
    }
}
