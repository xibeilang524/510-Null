using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 轮换信息类
    /// </summary>
    [DataContract]
    public class RotationInfo
    {
        /// <summary>
        /// 获取或设置控制器ID
        /// </summary>
        [DataMember]
        public int EntranceID { get; set; }
        /// <summary>
        /// 获取或设置轮换序号
        /// </summary>
        [DataMember]
        public int Number { get; set; }
    }
}
