using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.LR280
{
    internal class Request
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

        public string 卡号 { get; set; }

        public string 增值信息_B扫C { get; set; }

        public string 商户账单号_C扫B { get; set; }

        public string 账单时间_C扫B { get; set; }
        #endregion

        #region 公共方法
        public byte[] ToBytes()
        {
            string ret = "";
            string space = "";
            ret += string.IsNullOrEmpty(this.应用类型) ? "01" : this.应用类型;
            ret += string.IsNullOrEmpty(this.POS机号) ? space.PadRight(8) : this.POS机号.PadRight(8);
            ret += string.IsNullOrEmpty(this.POS员工号) ? space.PadRight(8) : this.POS员工号.PadRight(8);
            ret += this.交易类型标志;
            ret += 金额.ToString().PadLeft(12, '0');
            ret += string.IsNullOrEmpty(原交易日期) ? space.PadRight(8) : this.原交易日期;
            ret += string.IsNullOrEmpty(原交易参考号) ? space.PadRight(12) : this.原交易参考号;
            ret += string.IsNullOrEmpty(原凭证号) ? space.PadRight(6) : this.原凭证号;
            ret += LRC.ToString().PadLeft(3, '0') ;
            ret += string.IsNullOrEmpty(授权码) ? space.PadRight(6) : this.授权码;
            ret += string.IsNullOrEmpty(卡号) ? space.PadRight(20) : 卡号.PadRight(20);
            ret += string.IsNullOrEmpty(增值信息_B扫C) ? space.PadRight(50) : 增值信息_B扫C.PadRight(50);
            ret += string.IsNullOrEmpty(商户账单号_C扫B) ? space.PadRight(64) : 商户账单号_C扫B.PadRight(64);
            ret += string.IsNullOrEmpty(账单时间_C扫B) ? space.PadRight(10) : 账单时间_C扫B.PadRight(10);
            return System.Text.ASCIIEncoding.Default.GetBytes(ret);
        }
        #endregion
    }
}
