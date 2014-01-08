using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization ;

namespace Ralid.Park.BusinessModel.ParkingGuide
{
    /// <summary>
    /// 表示车位引导系统中的区域控制器
    /// </summary>
    [DataContract]
    public class RegionController:SubDevice
    {
        #region 构造函数
        public RegionController(int dialCode)
        {
            this.DialCode = dialCode;
        }
        #endregion

        #region 私有变量
        private ParkingGuideBus[] _Buses; //不要直接在你的代码中使用此变量，应使用Buses属性
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取区域控制器下的所有车位探测器
        /// </summary>
        public List<ParkingDetector> Detectors
        {
            get
            {
                List<ParkingDetector> items = new List<ParkingDetector>();
                foreach (ParkingGuideBus Bus in Buses)
                {
                    SubDevice[] _Devices = Bus.SubDevices;
                    for (int i = 0; i < _Devices.Length; i++)
                    {
                        if (_Devices[i] is ParkingDetector)
                        {
                            items.Add(_Devices[i] as ParkingDetector);
                        }
                    }
                }
                return items;
            }
        }

        /// <summary>
        /// 获取区域控制器下的所有引导屏
        /// </summary>
        public List<ParkingScreen> Screens
        {
            get
            {
                List<ParkingScreen> items = new List<ParkingScreen>();
                foreach (ParkingGuideBus Bus in Buses)
                {
                    SubDevice[] _Devices = Bus.SubDevices;
                    for (int i = 0; i < _Devices.Length; i++)
                    {
                        if (_Devices[i] is ParkingScreen)
                        {
                            items.Add(_Devices[i] as ParkingScreen);
                        }
                    }
                }
                return items;
            }
        }

        /// <summary>
        /// 获取区域控制器的虚拟地址
        /// </summary>
        public override int Address
        {
            get
            {
                return ((byte)DeviceType.RegionController << 13) + (this.BusNum << 11) + (this.DialCode << 5);
            }
        }

        /// <summary>
        /// 获取区域控制器上的总线
        /// </summary>
        public override ParkingGuideBus[] Buses
        {
            get
            {
                if (_Buses == null)
                {
                    _Buses = new ParkingGuideBus[1];//区域控制器只有一个总线
                    _Buses[0] = new ParkingGuideBus(0, 32);
                    _Buses[0].Parent = this;
                }
                return _Buses;
            }
        }
        #endregion

        #region 重写基类方法
        public override string ToString()
        {
            return string.Format("[总线{0}]:拨码[{1}]", this.BusNum, this.DialCode);
        }
        #endregion
    }
}
