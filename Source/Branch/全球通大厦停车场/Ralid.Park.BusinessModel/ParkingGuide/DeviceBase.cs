using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.ParkingGuide
{
    [DataContract]
    public abstract class DeviceBase
    {
        #region  构造函数
        public DeviceBase()
        {
            this.ID = Guid.NewGuid();
        }
        #endregion

        #region  公共属性
        /// <summary>
        /// 获取或设置设备ID
        /// </summary>
        [DataMember]
        public Guid ID { get; set; }

        /// <summary>
        /// 获取或设置硬件的文字描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置上一级设备的ID
        /// </summary>
        [DataMember]
        public Guid ParentID { get; set; }

        /// <summary>
        /// 获取或设置上一级设备
        /// </summary>
        public DeviceBase Parent { get; set; }

        /// <summary>
        /// 获取设备上的总线,如果设备没有下级总线，返回null
        /// </summary>
        public virtual ParkingGuideBus[] Buses
        {
            get { return null; }
        }
        #endregion
    }
}
