using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Model;

namespace ECardInterface
{
    [DataContract]
    public class Mysetting
    {
        public static Mysetting Current { get; set; }

        #region 构造函数
        public Mysetting()
        {

        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置E车通接口的网址
        /// </summary>
        [DataMember]
        public string ECarUri { get; set; }

        /// <summary>
        /// 获取或设置需要检测车场满位的时间段
        /// </summary>
        [DataMember]
        public List<Ralid.Park.BusinessModel.Model.TimeZone> ParkfullCheckTimezones { get; set; }
        #endregion
    }
}
