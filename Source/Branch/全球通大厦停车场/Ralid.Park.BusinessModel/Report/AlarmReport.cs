using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;
using Ralid.Park .BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Report
{
    [DataContract]
    public class AlarmReport : ReportBase
    {
        #region 构造函数
        public AlarmReport()
        {
        }

        public AlarmReport(int parkID, int entranceID, DateTime eventDateTime, string sourceName, AlarmType alarmType, string alarmDescription, string operatorid)
            : base(parkID, entranceID, eventDateTime, sourceName)
        {
            AlarmType = alarmType;
            AlarmDescription = alarmDescription;
            OpeartorID = operatorid;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置报警类型
        /// </summary>
        [DataMember]
        public AlarmType AlarmType { get; set; }
        /// <summary>
        /// 获取或设置报警的描述
        /// </summary>
        [DataMember]
        public string AlarmDescription { get; set; }
        /// <summary>
        /// 获取或设置报警的相关操作员
        /// </summary>
        [DataMember]
        public string OpeartorID { get; set; }
        #endregion

        #region 重写基类方法
        public override string Description
        {
            get
            {
                return string.Format("【{0} ＠ {1}】:{2} {3} {4}", EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"), SourceName, 
                    Ralid.Park .BusinessModel .Resouce .AlarmTypeDescription .GetDescription (AlarmType), AlarmDescription, OpeartorID);
            }
        }
        #endregion
    }
}
