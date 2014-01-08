using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;
using Ralid.Park .BusinessModel .Enum ;

namespace Ralid.Park.BusinessModel.Model
{
    [DataContract]
    public class CustomCardTypeSetting
    {
        #region 静态属性
        /// <summary>
        /// 获取或设置当前系统的自定义卡片类型设置实例
        /// </summary>
        public static CustomCardTypeSetting  Current{get;set;}
        #endregion

        #region 私有变量
        [DataMember]
        private List<CardType> _CardTypes = new List<CardType>();
        #endregion

        #region 公共方法
        /// <summary>
        /// 通过自定义卡片类型值获取自定义卡片类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CardType GetCardType(byte value)
        {
            if (_CardTypes != null && _CardTypes.Count > 0) return _CardTypes.SingleOrDefault(c => (byte)c == value);
            return null;
        }
        /// <summary>
        /// 通过自定义卡片类型名获取自定义卡片类型
        /// </summary>
        /// <param name="cardTypeName"></param>
        /// <returns></returns>
        public CardType GetCardType(string cardTypeName)
        {
            if (_CardTypes != null && _CardTypes.Count > 0) return _CardTypes.SingleOrDefault(c => c.Name == cardTypeName);
            return null;
        }

        //public void AddCardType(string name, byte baseID, TariffBase tariff)
        //{
        //    if (_CardTypes == null) _CardTypes = new List<CardType>();
        //    List<CardType> cardtypes = GetCardTypeFromBase(baseID);
        //    CardType card = new CardType((CardType)baseID, (byte)(cardtypes.Count + 1), name);
        //    card.SetTariff(tariff);
        //    _CardTypes.Add(card);
        //}

        public void AddCardType(string name, byte baseID)
        {
            if (_CardTypes == null) _CardTypes = new List<CardType>();
            List<CardType> cardtypes = GetCardTypeFromBase(baseID);
            CardType card = new CardType((CardType)baseID, (byte)(cardtypes.Count + 1), name);
            _CardTypes.Add(card);
        }

        public CardType[] CardTypes
        {
            get
            {
                if (_CardTypes != null && _CardTypes.Count > 0) return _CardTypes.ToArray();
                return null;
            }
        }

        /// <summary>
        /// 通过原型卡类值获取所有自定义卡片类型值
        /// </summary>
        /// <param name="baseID"></param>
        /// <returns></returns>
        public List<CardType> GetCardTypeFromBase(byte baseID)
        {
            if (_CardTypes == null) _CardTypes = new List<CardType>();
            List<CardType> cardtypes = _CardTypes.FindAll(item => CardType.GetBaseCardType(item.ID).ID == baseID);
            return cardtypes;
        }

        /// <summary>
        /// 通过原型卡类值获取第一个自定义卡片类型值,如果没有找到，则返回原型卡类
        /// </summary>
        /// <param name="baseID"></param>
        /// <returns></returns>
        public CardType GetFirstCardTypeFromBase(byte baseID)
        {
            if (_CardTypes == null) _CardTypes = new List<CardType>();
            CardType cardtype = _CardTypes.FirstOrDefault(item => CardType.GetBaseCardType(item.ID).ID == baseID);
            if (cardtype == null) cardtype = (CardType)baseID;
            return cardtype;
        }
        #endregion
    }
}
