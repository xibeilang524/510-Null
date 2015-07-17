using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Resouce
{
    public class ParkingStatusDescription
    {
        /// <summary>
        /// 获取停车状态的字符串描述
        /// </summary>
        /// <param name="parkingFlag"></param>
        /// <returns></returns>
        public static string GetDescription(ParkingStatus parkingFlag)
        {
            //所有这几个设置了一下顺序,这个方法不好,但没有想到更好的方法
            if ((parkingFlag & ParkingStatus.IndoorIn) == ParkingStatus.IndoorIn)
            {
                return Resource1.ParkingStatus_IndoorIn;
            }
            if ((parkingFlag & ParkingStatus.In) == ParkingStatus.In)
            {
                return Resource1.ParkingStatus_In;
            }
            if ((parkingFlag & ParkingStatus.Out) == ParkingStatus.Out)
            {
                return Resource1.ParkingStatus_Out;
            }
            if ((parkingFlag & ParkingStatus.AsTempCard) == ParkingStatus.AsTempCard)
            {
                return  Resource1.ParkingStatus_AsTempCard;
            }
            if ((parkingFlag & ParkingStatus.Consume) == ParkingStatus.Consume)
            {
                return Resource1.ParkingStatus_Consume;
            }
            if ((parkingFlag & ParkingStatus.NestedParkMarked) == ParkingStatus.NestedParkMarked)
            {
                return Resource1.ParkingStatus_NestedParkMarked;
            }
            if ((parkingFlag & ParkingStatus.PaidBill) == ParkingStatus.PaidBill)
            {
                return Resource1.ParkingStatus_PaidBill;
            }
            if ((parkingFlag & ParkingStatus.NotCheckOut) == ParkingStatus.NotCheckOut)
            {
                return Resource1.ParkingStatus_NotCheckOut;
            }
            if ((parkingFlag & ParkingStatus.HotelApp) == ParkingStatus.HotelApp)
            {
                return Resource1.ParkingStatus_HotelApp;
            }
            return string.Empty;
        }
    }
}
