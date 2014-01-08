using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.ParkingGuide
{
    /// <summary>
    /// 表示车位引导系统中用于显示引导信息的屏
    /// </summary>
    [DataContract]
    public class ParkingScreen : SubDevice
    {
        #region 构造函数
        public ParkingScreen(int dialCode)
        {
            this.DialCode = dialCode;
            Index = -1; //-1 表示还没有保存到硬件中
        }
        #endregion

        #region 私有变量
        [DataMember]
        internal byte Params;

        [DataMember]
        internal int Index;

        [DataMember]
        private List<Guid> _MonitorDetectors = new List<Guid>();
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取设备的虚拟地址
        /// </summary>
        public override int Address
        {
            get
            {
                if (this.Parent is RegionController)
                {
                    RegionController parent = this.Parent as RegionController;
                    return ((byte)DeviceType.ParkingDetector << 13) + (parent.BusNum << 11) + (parent.DialCode << 5) + DialCode;
                }
                else if (this.Parent is CentralController)
                {
                    return ((byte)DeviceType.RegionController << 13) + (this.BusNum << 11) + (this.DialCode << 5);
                }
                return 0xffff;
            }
        }

        /// <summary>
        /// 获取或设置是否显示车位数
        /// </summary>
        public bool ShowVacant
        {
            get
            {
                return (Params & 0x40) == 0x40;
            }
            set
            {
                Params |= 0x40;
                if (!value)
                {
                    Params -= 0x40;
                }
            }
        }

        /// <summary>
        /// 获取或设置是否显示箭头
        /// </summary>
        public bool ShowArrow
        {
            get
            {
                return (Params & 0x80) == 0x80;
            }
            set
            {
                Params |= 0x80;
                if (!value)
                {
                    Params -= 0x80;
                }
            }
        }

        /// <summary>
        /// 获取或设置箭头是否滚动
        /// </summary>
        public bool MovingArrow
        {
            get
            {
                return (Params & 0x20) == 0x20;
            }
            set
            {
                Params |= 0x20;
                if (!value)
                {
                    Params -= 0x20;
                }
            }
        }

        /// <summary>
        /// 获取或设置箭头指示方向
        /// </summary>
        public ArrowDirection ArrowDirection
        {
            get
            {
                return (ArrowDirection)(Params & 0x03);
            }
            set
            {
                Params = (byte)((Params & 0xFC) + (byte)value);
            }
        }

        /// <summary>
        /// 获取引导屏要显示其状态的所有车位探测器
        /// </summary>
        public List<ParkingDetector> Detectors
        {
            get
            {
                return new List<ParkingDetector>();
            }
        }
        #endregion
    }
}
