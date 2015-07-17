using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 自助缴费机结账记录信息类
    /// </summary>
    public class APMCheckOutRecord
    {
        #region 私有属性
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置ID号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 获取或设置缴费机号
        /// </summary>
        public string MID { get; set; }

        /// <summary>
        /// 获取或设置结账时间
        /// </summary>
        public DateTime CheckOutDateTime { get; set; }

        /// <summary>
        /// 获取或设置上次结账时间
        /// </summary>
        public DateTime LastDateTime { get; set; }

        /// <summary>
        /// 获取或设置上次结账余额
        /// </summary>
        public decimal LastBalance { get; set; }

        /// <summary>
        /// 获取或设置结账金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 获取或设置实际结账金额
        /// </summary>
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 获取或设置本次结余
        /// </summary>
        public decimal TheBalance { get; set; }

        /// <summary>
        /// 获取或设置支出金额
        /// </summary>
        public decimal PayMoney { get; set; }

        /// <summary>
        /// 获取或设置收入金额
        /// </summary>
        public decimal IncomeMoneny { get; set; }

        /// <summary>
        /// 获取或设置100元数量
        /// </summary>
        public int Hundred { get; set; }

        /// <summary>
        /// 获取或设置五十元数量
        /// </summary>
        public int Fifty { get; set; }

        /// <summary>
        /// 获取或设置二十元数量
        /// </summary>
        public int Twenty { get; set; }

        /// <summary>
        /// 获取或设置十元数量
        /// </summary>
        public int Ten { get; set; }


        /// <summary>
        /// 获取或设置纸币数量
        /// </summary>
        public int Cash { get; set; }

        /// <summary>
        /// 获取或设置纸币金额
        /// </summary>
        public decimal CashAmount { get; set; }

        /// <summary>
        /// 获取或设置硬币数量
        /// </summary>
        public int Coin { get; set; }

        /// <summary>
        /// 获取或设置结账管理员
        /// </summary>
        public string APMOperator { get; set; }


        /// <summary>
        /// 获取或设置备注说明
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 获取钱箱总金额
        /// </summary>
        public decimal TotalAmount
        {
            get
            {
                return CashAmount + Coin;
            }
        }

        /// <summary>
        /// 获取收支平衡
        /// </summary>
        public decimal Balance
        {
            get
            {
                return IncomeMoneny - PayMoney + LastBalance;
            }
        }

        /// <summary>
        /// 获取结账差额
        /// </summary>
        public decimal Difference
        {
            get
            {
                return Amount - ActualAmount;
            }
        }
        #endregion
    }
}
