using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Resouce
{
    public class CarTypeDescription
    {
        #region 公共属性
        /// <summary>
        /// 获取A型车的文字描述
        /// </summary>
        public static string CarType_A
        {
            get { return Resource1.CarType_A; }
        }

        /// <summary>
        /// 获取B型车的文字描述
        /// </summary>
        public static string CarType_B
        {
            get { return Resource1.CarType_B; }
        }

        /// <summary>
        /// 获取C型车的文字描述
        /// </summary>
        public static string CarType_C
        {
            get { return Resource1.CarType_C; }
        }

        /// <summary>
        /// 获取D型车的文字描述
        /// </summary>
        public static string CarType_D
        {
            get { return Resource1.CarType_D; }
        }

        /// <summary>
        /// 获取小型车的文字描述
        /// </summary>
        public static string CarType_Car
        {
            get { return Resource1.CarType_Car; }
        }

        /// <summary>
        /// 获取大型车的文字描述
        /// </summary>
        public static string CarType_Truck
        {
            get { return Resource1.CarType_Truck; }
        }

        /// <summary>
        /// 获取超大型车的文字描述
        /// </summary>
        public static string CarType_SuperTruck
        {
            get { return Resource1.CarType_SuperTruck; }
        }

        /// <summary>
        /// 获取摩托车的文字描述
        /// </summary>
        public static string CarType_Motor
        {
            get { return Resource1.CarType_Motor; }
        }

        /// <summary>
        /// 获取小型车的文字描述
        /// </summary>
        public static string CarType_CarOrA
        {
            get { return Resource1.CarType_Car + "/" + Resource1.CarType_A; }
        }

        /// <summary>
        /// 获取大型车的文字描述
        /// </summary>
        public static string CarType_TruckOrB
        {
            get { return Resource1.CarType_Truck + "/" + Resource1.CarType_B; }
        }

        /// <summary>
        /// 获取超大型车的文字描述
        /// </summary>
        public static string CarType_SuperTruckOrC
        {
            get { return Resource1.CarType_SuperTruck + "/" + Resource1.CarType_C; }
        }

        /// <summary>
        /// 获取摩托车的文字描述
        /// </summary>
        public static string CarType_MotorOrD
        {
            get { return Resource1.CarType_Motor + "/" + Resource1.CarType_D; }
        }
        #endregion
    }
}
