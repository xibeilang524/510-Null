using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 设置停车场设备的工作模式
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    [Serializable]
    public class DeviceModeNotify
    {
        /// <summary>
        ///获取或设置要设置工作模式的设备地址
        /// </summary>
        [System.Runtime.Serialization.DataMember]
        public byte Address { get; set; }

        /// <summary>
        /// 获取或设置设备的工作模式 
        /// </summary>
        [System.Runtime.Serialization.DataMember]
        public DeviceWorkMode DeviceMode { get; set; }

        public DeviceModeNotify()
        {
        }

        public DeviceModeNotify(byte address, DeviceWorkMode mode)
        {
            Address = address;
            DeviceMode = mode;
        }
    }

    /// <summary>
    /// 设备工作模式
    /// </summary>
    public enum DeviceWorkMode
    {
        /// <summary>
        /// 自动模式,适用于无人值守图像要求不严的车场:数据脱机运行，等待图像捕捉或对比确认首次超过设定时间秒数,发送问询指令,
        /// 得不到应答,自动放弃图像捕捉与对比确认,其后每次照常发送卡号同时发送问询指令,得不到应答,保持脱机,得到应答
        /// </summary>
        Auto = 0,

        /// <summary>
        /// 实时模式,一切交由PC机控制，适用于港口海关等需要与其他条件联合放行的场合，由专用软件控制
        /// </summary>
        RealTime = 3,

        /// <summary>
        /// 发行器模式,管理机进入注册登记模式，不做出口机使用，作发行器，；此模式可用总监卡或管理卡进入，可脱机发行
        /// </summary>
        Register = 4
    }
}
