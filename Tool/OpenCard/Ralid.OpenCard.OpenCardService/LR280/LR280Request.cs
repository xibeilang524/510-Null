using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.LR280
{
    internal class LR280Request
    {
        #region 公共属性
        /// <summary>
        /// 应用类型 01表示银行卡  02表示pos通
        /// </summary>
        public string 应用类型 { get; set; }
        /// <summary>
        /// POS机号
        /// </summary>
        public string POS机号 { get; set; }
        /// <summary>
        /// POS员工号
        /// </summary>
        public string POS员工号 { get; set; }
        /// <summary>
        /// 交易类型标志
        /// </summary>
        public string 交易类型标志 { get; set; }
        /// <summary>
        /// 金额(分)
        /// </summary>
        public int 金额 { get; set; }
        /// <summary>
        /// 原交易日期,退货时用，其他交易为空
        /// </summary>
        public string 原交易日期 { get; set; }
        /// <summary>
        /// 原交易参考号 退货时用，其他为空
        /// </summary>
        public string 原交易参考号 { get; set; }
        /// <summary>
        /// 原凭证号	撤消时用，其他交易空格
        /// </summary>
        public string 原凭证号 { get; set; }
        /// <summary>
        /// LRC校验	ANS	3	3位随机数字
        /// </summary>
        public int LRC { get; set; }
        /// <summary>
        /// 授权号
        /// </summary>
        public string 授权码 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string 卡号 { get; set; }
        #endregion
    }
}
