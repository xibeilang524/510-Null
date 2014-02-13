using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.POS.Model
{
    /// <summary>
    /// 表示系统常规设置
    /// </summary>
    public class MySetting
    {
        #region 静态属性
        /// <summary>
        /// 获取或设置系统当前的常规设置
        /// </summary>
        public static MySetting Current { get; set; }
        #endregion

        #region 构造函数
        public MySetting()
        {
            WegenType = WegenType.Wengen34;
            ParkingKey = new byte[] { 0xf1, 0xff, 0xff, 0xff, 0xff, 0xf1 };
            ParkingSection = 2;
            FreeTimeAfterPay = 15;
        }
        #endregion

        #region 常规设置
        /// <summary>
        /// 获取或设置系统使用的Wegen协议
        /// </summary>
        public WegenType WegenType { get; set; }

        /// <summary>
        /// 密钥值
        /// </summary>
        public byte[] ParkingKey { get; set; }

        /// <summary>
        /// 获取停车场读写的扇区
        /// </summary>
        public byte ParkingSection { get; set; }

        /// <summary>
        /// 获取或设置收费后最多可以在停车场内呆多少分钟而不用收费
        /// </summary>
        public int FreeTimeAfterPay { get; set; }
        #endregion

        #region 费率设置
        //由于费率设置采用继承自Tariffbase的方式，采用XML序列化时不能直接映射到子类，所以每种子类在这里定义一个列表，反序列化后再将所有列表组成一个基类集合
        //系统每增加一种收费子类都需要在这里建一个列表才能序列化和序列化

        public List<TariffOfDixiakongjian> TariffOfDixiakongjians { get; set; }

        public List<TariffOfGuanZhou> TariffOfGuanZhous { get; set; }

        public List<TariffOfLimitation> TariffOfLimitations { get; set; }

        public List<TariffOfTurningLimited> TariffOfTurningLimiteds { get; set; }

        public List<TariffOfTurning> TariffOfTurnings { get; set; }

        public List<TariffPerDay> TariffPerDays { get; set; }

        public List<TariffPerTime> TariffPerTimes { get; set; }
        #endregion

        #region 节假日设置
        /// <summary>
        ///获取或设置星期六是否为休息日
        /// </summary>
        public bool SaturdayIsRest { get; set; }

        /// <summary>
        /// 获取或设置星期日是否为休息日
        /// </summary>
        public bool SundayIsRest { get; set; }

        /// <summary>
        /// 获取节假日列表
        /// </summary>
        public List<HolidayInfo> Holidays { get; set; }
        #endregion

        #region 停车场系统操作员
        public List<OperatorInfo> Operators { get; set; }
        #endregion
    }
}
