using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 基本卡片类型设置类
    /// </summary>
    [DataContract]
    public class BaseCardTypeSetting
    {
        #region 静态属性
        /// <summary>
        /// 获取或设置当前系统的基本卡片类型设置实例
        /// </summary>
        public static BaseCardTypeSetting Current { get; set; }
        #endregion

        #region 私有变量
        /// <summary>
        /// 存储基本卡片类型自定义名称
        /// </summary>
        /// <returns></returns>
        [DataMember]
        private Dictionary<int, string> _UserDefinedName = new Dictionary<int, string>();
        #endregion

        #region 公共方法
        /// <summary>
        /// 通过基本卡片类型增加自定义名称
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="name"></param>
        public void AddUserDefinedName(CardType cardType, string name)
        {
            AddUserDefinedName(cardType.ID, name);
        }
        /// <summary>
        /// 通过基本卡片类型增加自定义名称
        /// </summary>
        /// <param name="cardType"></param>
        /// <param name="name"></param>
        public void AddUserDefinedName(byte cardType, string name)
        {
            if (_UserDefinedName.ContainsKey(cardType))
            {
                _UserDefinedName[cardType] = name;
            }
            else
            {
                _UserDefinedName.Add(cardType, name);
            }
        }
        /// <summary>
        /// 通过基本卡片类型获取自定义名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetUserDefinedName(CardType cardType)
        {
            return GetUserDefinedName(cardType.ID);
        }
        /// <summary>
        /// 通过基本卡片类型值获取自定义名称
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetUserDefinedName(byte cardType)
        {
            if (_UserDefinedName.ContainsKey(cardType))
            {
                return _UserDefinedName[cardType];
            }
            return string.Empty;
        }
        #endregion
    }
}
