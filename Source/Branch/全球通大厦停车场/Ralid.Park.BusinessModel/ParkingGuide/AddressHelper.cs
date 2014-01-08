using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.ParkingGuide
{
    /// <summary>
    /// 用于从虚拟地址中解析出相关硬件的信息，包括硬件的总线编号，硬件类型，硬件的实际拨码地址等。
    /// 也可以通过硬件的这些信息构建一个虚拟地址
    /// </summary>
    public static class AddressHelper
    {
        #region 公共方法
        /// <summary>
        /// 根据设备类型，总线地址，和拨码地址生成相应的虚拟地址，设备类型适用于区域控制器和引导屏
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="busNum"></param>
        /// <param name="dialingAddress"></param>
        /// <returns></returns>
        public static int CreateAddress(DeviceType deviceType, int busNum, int dialingAddress)
        {
            return ((byte)deviceType << 13) + (busNum << 11) + (dialingAddress << 5);
        }

        /// <summary>
        /// 根据设备类型，总线地址，拨码地址及上一级设备的拨码地址生成虚拟地址，设备类型适用于引导屏和车辆探测器
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="busNum"></param>
        /// <param name="parentDialingAddress"></param>
        /// <param name="dialingAddress"></param>
        /// <returns></returns>
        public static int CreateAddress(DeviceType deviceType, int busNum, int parentDialingAddress, int dialingAddress)
        {
            return ((byte)deviceType << 13) + (busNum << 11) + (parentDialingAddress << 5) + dialingAddress;
        }

        /// <summary>
        /// 解析出虚拟地址中的硬件设备类型
        /// </summary>
        public static DeviceType GetDeviceType(int address)
        {
            return (DeviceType)((address >> 13) & 0x03);  //虚拟地址14-15F位表示设备类型
        }

        /// <summary>
        /// 解析出虚拟地址中的硬件总线编号
        /// </summary>
        public static int GetRootBusNum(int address)
        {
            return (address & 0x1800) >> 11;
        }

        /// <summary>
        /// 解析出虚拟地址中的硬件拨码地址
        /// </summary>
        public static int GetDialCode(int address)
        {
            int RegionDial = (address & 0x07E0) >> 5;
            int detectorDial = address & 0x1F;  //0-4bit

            if (GetDeviceType(address) == DeviceType.RegionController)
            {
                return RegionDial;
            }
            else if (GetDeviceType(address) == DeviceType.ParkingDetector)
            {
                return address & 0x1F;  //0-4bit
            }
            else if (GetDeviceType(address) == DeviceType.Sreen)
            {
                if (detectorDial > 0) return detectorDial;
                return RegionDial;
            }
            return 0;
        }
        #endregion
    }
}