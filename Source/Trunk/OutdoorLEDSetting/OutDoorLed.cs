using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.GeneralLibrary .LED ;

namespace Ralid.Park.OutdoorLEDSetting
{
    /// <summary>
    /// 表示一块户外屏,户外屏是指澳大的一块独立放置的屏，每块户外屏由上下两块160*16 的小屏组成,上面一块用于显示汽车车位，下面一块用于显示电单车车位
    /// 每块小屏再分成三个独立显示区域，所以每块户外屏共有六个显示区域，可以显示三种卡片类型组合两种车型（小车,电单车)的车余位信息
    /// </summary>
    [DataContract]
    public class OutDoorLed
    {
        #region  构造函数
        public OutDoorLed()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置屏的串口号
        /// </summary>
        [DataMember]
        public byte Commport { get; set; }
        /// <summary>
        /// 获取或设置波特率
        /// </summary>
        [DataMember]
        public int Baud { get; set; }
        /// <summary>
        /// 获取或设置汽车屏的地址
        /// </summary>
        [DataMember]
        public int CarLedAddress { get; set; }
        /// <summary>
        /// 获取或设置电单车屏的地址
        /// </summary>
        [DataMember]
        public int MotorLedAddress { get; set; }
        /// <summary>
        /// 获取或设置亮度
        /// </summary>
        [DataMember]
        public byte Brightness { get; set; }
        #endregion
    }
}
