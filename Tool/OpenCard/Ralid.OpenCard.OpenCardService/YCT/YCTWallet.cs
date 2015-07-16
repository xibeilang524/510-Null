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
        /// 获取或设置钱包类别 1为M1钱包 2为CPU钱包
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
        /// 获取或设置余额
        /// </summary>
        public decimal Balence { get; set; }
        /// <summary>
        /// 获取或设置卡计数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 获取或设置余额上限
        /// </summary>
        public decimal MaxBalance { get; set; }
        /// <summary>
        /// 获取或设置余额下限
        /// </summary>
        public decimal MinBalance { get; set; }
        /// <summary>
        /// 获取或设置押金 
        /// </summary>
        public decimal Deposit { get; set; }
        #endregion
    }
}
