using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.Park.BusinessModel.ParkingGuide
{
    /// <summary>
    /// 表示一个车位探测器
    /// </summary>
    [DataContract]
    public class ParkingDetector:SubDevice 
    {
        #region 构造函数
        public ParkingDetector(int dialCode)
        {
            this.DialCode = dialCode;
        }
        #endregion

        #region 私有变量
        /// <summary>
        /// 车位探测器参数
        /// </summary>
        [DataMember]
        internal byte Params;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取车位探测器的虚拟地址
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
                return 0xffff;
            }
        }

        /// <summary>
        /// 获取或设置车位探测器的探测高度
        /// </summary>
        public DetectHeight DetectHeight
        {
            //探测器参数的5,6,7位
            get
            {
                return (DetectHeight)((Params >> 4) & 0x07);
            }
            set
            {
                Params = (byte)(((byte)value << 4) + (Params & 0x0F));
            }
        }

        /// <summary>
        /// 获取或设置车位测试器的延时等级
        /// </summary>
        public DetectInterval DetectInterval
        {
            get { return (DetectInterval)(Params & 0x07); }
            set { Params = (byte)((Params & 0xF8) + (byte)value); }
        }
        #endregion
    }
}
