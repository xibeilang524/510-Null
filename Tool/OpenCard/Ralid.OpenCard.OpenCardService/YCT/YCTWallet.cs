using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    /// <summary>
    /// 表示羊城通电子钱包
    /// </summary>
    public class YCTWallet
    {
        #region 构造函数
        public YCTWallet() { }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置钱包类别0表示其它IC卡 1为M1钱包 2为CPU钱包
        /// </summary>
        public byte WalletType { get; set; }
        /// <summary>
        /// 获取或设置物理卡号
        /// </summary>
        public string PhysicalCardID { get; set; }
        /// <summary>
        /// 获取或设置逻辑卡号
        /// </summary>
        public string LogicCardID { get; set; }
        /// <summary>
        /// 获取或设置卡片类型
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 获取或设置余额(以分为单位)
        /// </summary>
        public int Balance { get; set; }
        /// <summary>
        /// 获取或设置卡计数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 获取或设置余额上限(以分为单位)
        /// </summary>
        public int MaxBalance { get; set; }
        /// <summary>
        /// 获取或设置余额下限(以分为单位)
        /// </summary>
        public int MinBalance { get; set; }
        /// <summary>
        /// 获取或设置押金(以分为单位)
        /// </summary>
        public int Deposit { get; set; }
        #endregion
    }
}
