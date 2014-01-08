using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示车场分区车位信息
    /// </summary>
    [DataContract]
    public class CarPortSetting
    {
        #region 构造函数
        public CarPortSetting()
        {
        }

        public CarPortSetting(ParkInfo park)
        {
            ParkID = park.ParkID;
            CarPortUpLimit = park.TotalPosition;
            CarPortDownLimit = park.MinPosition;
            VacantPort = park.Vacant;
            VacantText = park.VacantText;
            ParkFullText = park.ParkFullText;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 停车场ID
        /// </summary>
        [DataMember]
        public int ParkID { get; set; }
        /// <summary>
        /// 车位上限
        /// </summary>
        [DataMember]
        public short CarPortUpLimit { get; set; }
        /// <summary>
        /// 车位下限
        /// </summary>
        [DataMember]
        public short CarPortDownLimit { get; set; }
        /// <summary>
        /// 车位余数
        /// </summary>
        [DataMember]
        public short VacantPort { get; set; }
        /// <summary>
        /// 获取或设置提示车位余数的字符串
        /// </summary>
        [DataMember]
        public string VacantText { get; set; }
        /// <summary>
        /// 获取或设置提示车场满位的字符串
        /// </summary>
        [DataMember]
        public string ParkFullText { get; set; }
        #endregion

    }
}
