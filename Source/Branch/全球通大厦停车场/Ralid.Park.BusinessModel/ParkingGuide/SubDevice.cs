using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.ParkingGuide
{
    [DataContract]
    public abstract class SubDevice:DeviceBase 
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置设备的拨码地址
        /// </summary>
        [DataMember]
        public int DialCode { get; internal set; }

        /// <summary>
        /// 获取或设置设备所属总线编号
        /// </summary>
        [DataMember]
        public int BusNum { get; internal set; }

        /// <summary>
        /// 获取设备所属的区域控制器
        /// </summary>
        public CentralController CentralController
        {
            get
            {
                DeviceBase parent = this.Parent;
                while (parent != null && !(parent is CentralController))
                {
                    parent = parent.Parent;
                }
                return parent as CentralController;
            }
        }

        /// <summary>
        /// 获取设备的虚拟地址
        /// </summary>
        public abstract int Address { get; }
        #endregion
    }
}