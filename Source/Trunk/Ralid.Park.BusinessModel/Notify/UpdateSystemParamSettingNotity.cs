using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Interface;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 更新系统参数设置通知
    /// </summary>
    [DataContract]
    public class UpdateSystemParamSettingNotity
    {
        /// <summary>
        /// 获取或设置修改参数设置的操作员
        /// </summary>
        [DataMember]
        public OperatorInfo Operator { get; set; }

        /// <summary>
        /// 获取或设置修改参数设置的工作站ID
        /// </summary>
        [DataMember]
        public string  StationID { get; set; }


        /// <summary>
        /// 获取或设置修改参数设置的工作站名称
        /// </summary>
        [DataMember]
        public string StationName { get; set; }

        /// <summary>
        /// 获取或设置修改参数类的类名称
        /// </summary>
        [DataMember]
        public string ParamTypeName { get; set; }

        public UpdateSystemParamSettingNotity(OperatorInfo opt, string stationID, string stationName, string paramTypeName)
        {
            this.Operator = opt;
            this.StationID = stationID;
            this.StationName = stationName;
            this.ParamTypeName = paramTypeName;
        }

        public UpdateSystemParamSettingNotity()
        {
        }
    }
}
