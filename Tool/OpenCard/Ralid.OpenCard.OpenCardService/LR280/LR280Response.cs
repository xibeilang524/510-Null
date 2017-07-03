using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.LR280
{
    public class LR280Response
    {
        public static readonly string SUCCESS = "00";
        #region 公共属性
        /// <summary>
        /// 返回码	N2	00 表示成功，其它表示失败
        /// </summary>
        public string 返回码 { get; set; }
        /// <summary>
        /// ANS2
        /// </summary>
        public string 交易类型 { get; set; }
        /// <summary>
        /// 银行行号	N	4	发卡行代码
        /// </summary>
        public string 银行行号 { get; set; }
        /// <summary>
        /// 卡号	N20	卡号（屏蔽部分，保留前6后4）
        /// </summary>
        public string 卡号 { get; set; }
        /// <summary>
        /// 凭证号	N	6	
        /// </summary>
        public string 凭证号 { get; set; }
        /// <summary>
        /// 金额(分)	
        /// </summary>
        public int 金额 { get; set; }
        /// <summary>
        /// 错误说明	ANS	40	中文解释
        /// </summary>
        public string 错误说明 { get; set; }
        /// <summary>
        /// 商户号	N	15
        /// </summary>
        public string 商户号 { get; set; }
        /// <summary>
        /// 终端号	N	8
        /// </summary>
        public string 终端号 { get; set; }
        /// <summary>
        /// 批次号	N	6
        /// </summary>
        public string 批次号 { get; set; }
        /// <summary>
        /// 交易日期	N	4
        /// </summary>
        public string 交易日期 { get; set; }
        /// <summary>
        /// 交易时间	N	6	
        /// </summary>
        public string 交易时间 { get; set; }
        /// <summary>
        /// 交易参考号	N	12
        /// </summary>
        public string 交易参考号 { get; set; }
        /// <summary>
        /// 授权号	N	6
        /// </summary>
        public string 授权号 { get; set; }
        /// <summary>
        /// 清算日期	N	4	
        /// </summary>
        public string 清算日期 { get; set; }
        /// <summary>
        /// ANS	3	三位数字，应该和请求一致（可忽略）
        /// </summary>
        public string LRC校验 { get; set; }
        #endregion
    }
}
