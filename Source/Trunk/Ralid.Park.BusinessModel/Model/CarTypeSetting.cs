using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Resouce;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示目前系统支持的车型，由于系统车型与硬件上目前的车车型不匹配，所以有可能有多个系统车型对应一个硬件车型的情况
    /// </summary>
    [DataContract]
    public class CarTypeSetting
    {
        #region 静态属性
        public static CarTypeSetting Current { get; set; }

        /// <summary>
        /// 获取默认车型
        /// </summary>
        public static byte DefaultCarType
        {
            get { return 0; }
        }

        #endregion

        /// <summary>
        /// 获取或设置所有的车型信息
        /// </summary>
        [DataMember]
        private List<CarType> _CarTypes { get; set; }

        public CarTypeSetting()
        {
            _CarTypes = new List<CarType>();
            _CarTypes.Add(new CarType() { ID = 0, Description = CarTypeDescription.CarType_Car, HardwareCarType = 0 });
            _CarTypes.Add(new CarType() { ID = 1, Description = CarTypeDescription.CarType_Truck, HardwareCarType = 1 });
            _CarTypes.Add(new CarType() { ID = 2, Description = CarTypeDescription.CarType_SuperTruck, HardwareCarType = 2 });
            _CarTypes.Add(new CarType() { ID = 3, Description = CarTypeDescription.CarType_Motor, HardwareCarType = 3 });
        }

        /// <summary>
        /// 获取车型的文字描述
        /// </summary>
        /// <param name="carType"></param>
        /// <returns></returns>
        public string GetDescription(int carType)
        {
            string ret = string.Empty;
            foreach (CarType item in _CarTypes)
            {
                if (item.ID == carType) return item.Description;
            }
            return ret;
        }

        /// <summary>
        /// 通过车型的ID号获取车型信息，如果不存在，返回null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CarType this[int id]
        {
            get
            {
                return _CarTypes.SingleOrDefault(item => item.ID == id);
            }
        }
        /// <summary>
        /// 获取某一车型的控制板车型
        /// </summary>
        /// <param name="carType"></param>
        /// <returns></returns>
        public int GetHardwareCarType(int carType)
        {
            CarType ct = this[carType];
            return ct == null ? 0 : ct.HardwareCarType;
        }
        /// <summary>
        /// 增加一个车型
        /// </summary>
        /// <param name="carType"></param>
        public void Add(CarType carType)
        {
            foreach (CarType item in _CarTypes)
            {
                if (item.ID == carType.ID || item.Description == carType.Description)
                {
                    return;
                }
            }
            _CarTypes.Add(carType);
        }

        /// <summary>
        /// 移除某个车型
        /// </summary>
        /// <param name="carType"></param>
        public void Remove(CarType carType)
        {
            foreach (CarType item in _CarTypes)
            {
                if (item.ID == carType.ID)
                {
                    _CarTypes.Remove(item);
                    break;
                }
            }
        }
        /// <summary>
        /// 清空车型信息
        /// </summary>
        public void Clear()
        {
            _CarTypes.Clear();
        }

        /// <summary>
        /// 获取所有的车型信息
        /// </summary>
        public CarType[] CarTypes
        {
            get
            {
                if (_CarTypes == null) //如果为空则说明序列化时有错误，以默认车型返回
                {
                    _CarTypes = new List<CarType>();
                    _CarTypes.Add(new CarType() { ID = 0, Description = CarTypeDescription.CarType_Car, HardwareCarType = 0 });
                    _CarTypes.Add(new CarType() { ID = 1, Description = CarTypeDescription.CarType_Truck, HardwareCarType = 1 });
                    _CarTypes.Add(new CarType() { ID = 2, Description = CarTypeDescription.CarType_SuperTruck, HardwareCarType = 2 });
                    _CarTypes.Add(new CarType() { ID = 3, Description = CarTypeDescription.CarType_Motor, HardwareCarType = 3 });
                }
                return _CarTypes.ToArray();
            }
        }

        /// <summary>
        /// 车型是否能删除，默认4种车型不能删除
        /// </summary>
        public bool CanDel(byte carType)
        {
            return carType != 0 && carType != 1 && carType != 2 && carType != 3;
        }

        /// <summary>
        /// 车型是否能删除，默认4种车型不能删除
        /// </summary>
        public bool CanDel(int carType)
        {
            return carType != 0 && carType != 1 && carType != 2 && carType != 3;
        }
    }

    /// <summary>
    /// 表示一个车型信息
    /// </summary>
    [DataContract]
    public class CarType
    {
        /// <summary>
        /// 获取或设置车型ID
        /// </summary>
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置车型描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置车型在控制板上对应的车型
        /// </summary>
        [DataMember]
        public int HardwareCarType { get; set; }

        /// <summary>
        /// 获取或设置车型是否能删除，默认4种车型不能删除
        /// </summary>
        public bool CanDel
        {
            get
            {
                return ID != 0 && ID != 1 && ID != 2 && ID != 3;
            }
        }
    }
}
