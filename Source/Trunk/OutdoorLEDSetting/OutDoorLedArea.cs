using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.GeneralLibrary.LED;

namespace Ralid.Park.OutdoorLEDSetting
{
    /// <summary>
    /// 表示户外屏的一个车位显示区域
    /// </summary>
    [DataContract]
    public class OutDoorLedArea
    {
        /// <summary>
        /// 获取或设置区域原点的X坐标(以像素点为单位)
        /// </summary>
        [DataMember]
        public short X { get; set; }
        /// <summary>
        /// 获取或设置区域原点的Y坐标(以像素点为单位)
        /// </summary>
        [DataMember]
        public short Y { get; set; }
        /// <summary>
        /// 获取或设置区域的宽度(以像素点为单位)
        /// </summary>
        [DataMember]
        public short Width { get; set; }
        /// <summary>
        /// 获取或设置区域的高度(以像素点为单位)
        /// </summary>
        [DataMember]
        public short Height { get; set; }
        /// <summary>
        /// 获取或设置区域要显示哪种卡片类型的车位数(具体由停车场卡片类型而定)
        /// </summary>
        [DataMember]
        public byte? CardType { get; set; }
        /// <summary>
        /// 获取或设置区域要显示哪种车类型的车位数(0表示汽车,1表示电单车)
        /// </summary>
        [DataMember]
        public byte CarType { get; set; }
        /// <summary>
        /// 获取或设置区域的车位余数
        /// </summary>
        [DataMember]
        public int Vacant { get; set; }
        /// <summary>
        /// 获取或设置区域的车位总数
        /// </summary>
        [DataMember]
        public int CarPort { get; set; }
        /// <summary>
        /// 获取或设置余位显示颜色(1红 2绿 3黄)
        /// </summary>
        [DataMember]
        public int VacantColor { get; set; }
        /// <summary>
        /// 获取或设置满位显示颜色(1红 2绿 3黄)
        /// </summary>
        [DataMember]
        public int FullColor { get; set; }
    }
}
