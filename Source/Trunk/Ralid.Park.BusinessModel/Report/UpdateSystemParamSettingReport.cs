using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Report
{
    /// <summary>
    /// 更新系统参数通知报告
    /// </summary>
    [DataContract]
    public class UpdateSystemParamSettingReport:ReportBase
    {
         #region 构造函数
        public UpdateSystemParamSettingReport()
        {
        }

        public UpdateSystemParamSettingReport(DateTime eventDateTime,string SourceName, string operatorid, string stationID, string paramTypeName)
            : base(0, 0, eventDateTime, SourceName)
        {
            OperatorID = operatorid;
            StationID = stationID;
            ParamTypeName = paramTypeName;
        }
        #endregion
        #region 公共属性
        /// <summary>
        /// 获取或设置修改参数设置的操作员
        /// </summary>
        [DataMember]
        public string OperatorID { get; set; }

        /// <summary>
        /// 获取或设置修改参数设置的工作站ID
        /// </summary>
        [DataMember]
        public string StationID { get; set; }
        
        /// <summary>
        /// 获取或设置修改参数类的类名称
        /// </summary>
        [DataMember]
        public string ParamTypeName { get; set; }
        #endregion

        #region 重写基类方法
        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2} {3} {4}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"), SourceName,
                     OperatorID, Resouce.Resource1.UpdateSystemParamSettingReport_Update, ParamTypeName);
            }
        }
        #endregion
    }
}
