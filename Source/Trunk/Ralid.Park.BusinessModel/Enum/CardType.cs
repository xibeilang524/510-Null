using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.BusinessModel.Enum
{
    //目前系统支持16种原型卡片类型，每种原型类型都有一个静态属性来获取。
    //系统还支持自定义卡片类型，由原型卡片类型引申而来，每种原型卡可以引申出16种自定义类型，
    //引申出的卡片进出停车场的行为与其原型卡基本一致，系统对原型卡的一些设置在引申卡类型上也生效，
    //比如从月卡引申出一种自定义卡“员工卡",如果系统设置了月卡可以重复进出场，则“员工卡"也可以重复进也场。
    //引申卡片类型的ID，由两部分组成，低四位表示其原型卡类型，高四位表示引申卡的子编号，而原型卡类型高四位都为0。
    //引申卡片类型也有一些特殊功能，比如可以设置引申卡片类型的名称，可以对引申卡设置费率，如月卡或业主卡的引申卡设置费率后，
    //出场时就会通过费率来收费，不会自动放行，收费后，就可以自动出场了。
    //如果系统中删除了此引申卡类型，类型为此引申卡片类型的卡片的卡片类型自动恢复成对应原型卡片类型
    [DataContract]
    public class CardType
    {
        #region 静态属性
        /// <summary>
        /// 总监卡
        /// </summary>
        public static CardType MonitorCard
        {
            get
            {
                return new CardType(0);
            }
        }
        /// <summary>
        /// 管理卡
        /// </summary>
        public static CardType ManagerCard
        {
            get
            {
                return new CardType(1);
            }
        }
        /// <summary>
        /// 操作卡（写卡模式时用于收费功能卡）
        /// </summary>
        public static CardType OperatorCard
        {
            get
            {
                return new CardType(2);
            }
        }
        /// <summary>
        /// 测试卡
        /// </summary>
        public static CardType TestCard
        {
            get
            {
                return new CardType(3);
            }
        }
        /// <summary>
        /// 固定卡,业主卡
        /// </summary>
        public static CardType OwnerCard
        {
            get
            {
                return new CardType(4);
            }
        }
        /// <summary>
        /// 贵宾卡
        /// </summary>
        public static CardType VipCard
        {
            get
            {
                return new CardType(5);
            }
        }
        /// <summary>
        /// 月卡
        /// </summary>
        public static CardType MonthRentCard
        {
            get
            {
                return new CardType(6);
            }
        }
        /// <summary>
        /// 日租卡
        /// </summary>
        public static CardType DayRentCard
        {
            get
            {
                return new CardType(7);
            }
        }
        /// <summary>
        /// 储值卡
        /// </summary>
        public static CardType PrePayCard
        {
            get
            {
                return new CardType(8);
            }
        }
        /// <summary>
        /// 临时卡
        /// </summary>
        public static CardType TempCard
        {
            get
            {
                return new CardType(9);
            }
        }
        /// <summary>
        /// 羊城通
        /// </summary>
        public static CardType Yangcheng
        {
            get
            {
                return new CardType(10);
            }
        }
        /// <summary>
        /// 中山通卡
        /// </summary>
        public static CardType Zhongshan
        {
            get
            {
                return new CardType(11);
            }
        }
        /// <summary>
        /// 深圳通卡
        /// </summary>
        public static CardType ShenZhen
        {
            get
            {
                return new CardType(12);
            }
        }
        /// <summary>
        /// 自定义卡片1
        /// </summary>
        public static CardType UserDefinedCard1
        {
            get
            {
                return new CardType(13);
            }
        }
        /// <summary>
        /// 自定义卡片2
        /// </summary>
        public static CardType UserDefinedCard2
        {
            get
            {
                return new CardType(14);
            }
        }
        /// <summary>
        /// 纸票,只用于联机系统
        /// </summary>
        public static CardType Ticket
        {
            get
            {
                return new CardType(15);
            }
        }

        public static CardType GetSystemCardType(byte id)
        {
            if (CustomCardTypeSetting.Current != null)
            {
                CardType cardType = CustomCardTypeSetting.Current.GetCardType(id);
                if (cardType != null) return cardType;
            }
            return CardType.GetBaseCardType(id);
        }

        /// <summary>
        /// 获取所有原型卡片类型
        /// </summary>
        /// <returns></returns>
        public static List<CardType> GetBaseCardTypes()
        {
            List<CardType> cardTypes = new List<CardType>();
            cardTypes.Add(new CardType(CardType.MonthRentCard.ID));
            cardTypes.Add(new CardType(CardType.OwnerCard.ID));
            cardTypes.Add(new CardType(CardType.PrePayCard.ID));
            cardTypes.Add(new CardType(CardType.TempCard.ID));
            cardTypes.Add(new CardType(CardType.Ticket.ID));
            return cardTypes;
        }

        /// <summary>
        /// 通过ID号获取原型卡片类型
        /// </summary>
        /// <param name="cardTypeID"></param>
        /// <returns></returns>
        public static CardType GetBaseCardType(byte cardTypeID)
        {
            return new CardType((byte)(cardTypeID & 0x0F));
        }

        /// <summary>
        /// 获取卡片类型的原型类型
        /// </summary>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public static CardType GetBaseCardType(CardType cardType)
        {
            if (cardType._ID < 16) return cardType;
            return new CardType((byte)(cardType._ID & 0x0F));
        }
        #endregion

        #region 与byte转换的操作符重载
        public static explicit operator CardType(byte value)
        {
            return new CardType(value);
        }
        public static explicit operator byte(CardType cardType)
        {
            return cardType._ID;
        }
        public static bool operator ==(CardType c1, CardType c2)
        {
            return object.Equals(c1, c2);
        }
        public static bool operator !=(CardType c1, CardType c2)
        {
            return !object.Equals(c1, c2);
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 卡片类型构造函数
        /// </summary>
        public CardType()
        {
            this._ID = 0;
        }
        /// <summary>
        /// 卡片类型构造函数
        /// </summary>
        /// <param name="id">卡片类型ID</param>
        public CardType(byte id)
        {
            this._ID = id;
        }
        /// <summary>
        /// 卡片类型构造函数
        /// </summary>
        /// <param name="id">卡片类型ID</param>
        /// <param name="name">卡片类型名</param>
        public CardType(byte id, string name)
        {
            _ID = id;
            _Name = name;
        }
        /// <summary>
        /// 卡片类型构造函数
        /// </summary>
        /// <param name="baseID">卡片类型的原类型，此卡类型的行为与原类型一致,卡类型原类型必须是静态卡片类型中的一种</param>
        /// <param name="subIndex">卡片类型的序号(1-15)</param>
        /// <param name="name">卡片类型的名称</param>
        public CardType(CardType baseType, byte subIndex, string name)
        {
            if (baseType._ID <= 16 && subIndex >= 1 && subIndex <= 15)
            {
                _ID = (byte)((subIndex << 4) + baseType._ID);
                _Name = name;
            }
            else
            {
                throw new InvalidCastException("传入的参数baseType 或subIndex 有误");
            }
        }
        #endregion

        #region 私有变量
        [DataMember]
        private byte _ID { get; set; }

        [DataMember]
        private string _Name { get; set; }

        //[DataMember]
        //private List<TariffBase> _Tariffs;
        #endregion

        #region 实例公共属性和方法
        /// <summary>
        /// 是否是管理类卡片类型(管理类卡片包括总监卡,管理卡和操作员卡)
        /// </summary>
        public bool IsManagedCard
        {
            get
            {
                int value = (_ID & 0x0F);
                return (value == CardType.MonitorCard._ID || value == CardType.ManagerCard._ID || value == CardType.OperatorCard._ID);
            }
        }
        /// <summary>
        /// 是否操作员卡
        /// </summary>
        public bool IsOperatorCard
        {
            get
            {
                int value = (_ID & 0x0F);
                return value == CardType.OperatorCard._ID;
            }
        }
        /// <summary>
        /// 是否是临时性卡片类型
        /// </summary>
        public bool IsTempCard
        {
            get
            {
                return (!IsMonthCard && !IsPrepayCard && !IsManagedCard);
            }
        }
        /// <summary>
        /// 是否是月租卡类型
        /// </summary>
        public bool IsMonthCard
        {
            get
            {
                int value = (_ID & 0x0F);
                return (value == CardType.MonthRentCard._ID 
                    || value == CardType.OwnerCard._ID 
                    || value == CardType.VipCard._ID
                    || value == CardType.UserDefinedCard1._ID 
                    || value == CardType.UserDefinedCard2._ID);
            }
        }
        /// <summary>
        /// 是否是储值卡类型
        /// </summary>
        public bool IsPrepayCard
        {
            get
            {
                int value = (_ID & 0x0F);
                return (value == CardType.PrePayCard._ID);
            }
        }
        /// <summary>
        /// 是否是自定义卡类型（原生卡片类型中的自定义卡类型）
        /// </summary>
        public bool IsUserDefinedCard
        {
            get
            {
                int value = (_ID & 0x0F);
                return (value == CardType.UserDefinedCard1._ID || value == CardType.UserDefinedCard2._ID);
            }
        }

        /// <summary>
        /// 获取或设置卡片类型是否是原生卡片类型（非自定义卡类)
        /// </summary>
        public bool IsPrimaryCardType
        {
            get { return _ID <= 0x0F; }
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is CardType)
            {
                CardType o = obj as CardType;
                return o._ID == this._ID;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        //public TariffBase GetTariff()
        //{
        //    if (_Tariffs != null && _Tariffs.Count > 0) return _Tariffs[0];
        //    return null;
        //}

        //public void SetTariff(TariffBase tariff)
        //{
        //    if (_Tariffs == null) _Tariffs = new List<TariffBase>();
        //    _Tariffs.Clear();
        //    _Tariffs.Add(tariff);
        //}

        /// <summary>
        /// 卡类型ID
        /// </summary>
        public byte ID { get { return _ID; } }

        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string Name
        {
            get
            {
                if (!string.IsNullOrEmpty(_Name)) return _Name;
                if (BaseCardTypeSetting.Current != null)
                {
                    string name = BaseCardTypeSetting.Current.GetUserDefinedName(_ID);
                    if (!string.IsNullOrEmpty(name)) return name;
                }
                return DefaultName;
            }
        }

        /// <summary>
        /// 卡类型默认名称
        /// </summary>
        public string DefaultName
        {
            get
            {
                if (!string.IsNullOrEmpty(_Name)) return _Name;
                switch (_ID & 0x0F)
                {
                    case 0:
                        return Resouce.Resource1.CardType_MonitorCard;
                    case 1:
                        return Resouce.Resource1.CardType_ManagedCard;
                    case 2:
                        return Resouce.Resource1.CardType_OperatorCard;
                    case 3:
                        return Resouce.Resource1.CardType_TestCard;
                    case 4:
                        return Resouce.Resource1.CardType_OwnerCard;
                    case 5:
                        return Resouce.Resource1.CardType_VipCard;
                    case 6:
                        return Resouce.Resource1.CardType_MonthRentCard;
                    case 7:
                        return Resouce.Resource1.CardType_DayRentCard;
                    case 8:
                        return Resouce.Resource1.CardType_PrepayCard;
                    case 9:
                        return Resouce.Resource1.CardType_TempCard;
                    case 10:
                        return Resouce.Resource1.CardType_YangCheng;
                    case 11:
                        return Resouce.Resource1.CardType_Zhongshan;
                    case 12:
                        return Resouce.Resource1.CardType_ShenZhen;
                    case 13:
                        return Resouce.Resource1.CardType_UserDefinedCard1;
                    case 14:
                        return Resouce.Resource1.CardType_UserDefinedCard2;
                    case 15:
                        return Resouce.Resource1.CardType_Ticket;
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// 通过原型卡类值获取第一个自定义卡片类型值,如果没有找到，则返回原型卡类
        /// </summary>
        /// <param name="baseID"></param>
        /// <returns></returns>
        public CardType GetFirstCardTypeFromBase
        {
            get
            {
                byte baseCardType=(byte)(_ID & 0x0F);
                if(CustomCardTypeSetting.Current!=null)
                {
                    return CustomCardTypeSetting.Current.GetFirstCardTypeFromBase(baseCardType);
                }
                return (CardType)baseCardType;
            }
        }
        #endregion
    }
}
