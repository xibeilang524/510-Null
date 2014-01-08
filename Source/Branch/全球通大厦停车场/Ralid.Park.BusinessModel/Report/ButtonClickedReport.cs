using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 按下取卡按钮事件
    /// </summary>
    [DataContract]
    public class ButtonClickedReport : ReportBase
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置取卡按钮，1为按钮1，2为按钮2，依此类推
        /// </summary>
        [DataMember]
        public int Button { get; set; }
        #endregion
        #region 重写基类方法
        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2}{3}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"), SourceName,
                   Resouce.Resource1.Report_CardButton, Button);
            }
        }
        #endregion
    }
}
