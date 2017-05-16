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
        /// 应用类型 ANS 2
        /// </summary>
        public string TYPE { get; set; }
        /// <summary>
        /// POS机号	ANS	8	不足右补空格(可全部空格)
        /// </summary>
        public string POSID { get; set; }
        /// <summary>
        /// POS员工号	ANS	8	不足右补空格(可全部空格)
        /// </summary>
        public string STAFFID { get; set; }
        /// <summary>
        /// 交易类型标志	N	2	'00'－消费        '01'－撤消
        /// '02'－退货        '03'－查余额
        /// '05'－签到'06'－结算
        /// </summary>
        public string PAYTYPE { get; set; }
        /// <summary>
        /// 金额	N	12	信用卡消费金额，char(12)，没有小数点"."，精确到分，最后两位为小数位，不足左补0。
        /// </summary>
        public int MONEY { get; set; }
        /// <summary>
        /// 原交易日期	N	8	yyyymmdd格式，退货时用，其他交易空格
        /// </summary>
        public string PAYDATE { get; set; }
        /// <summary>
        /// 原交易参考号	N	12	退货时用，其他交易空格
        /// </summary>
        public string REFERENCE { get; set; }
        /// <summary>
        /// 原凭证号	N	6	撤消时用，其他交易空格
        /// </summary>
        public string ORDERID { get; set; }
        /// <summary>
        /// LRC校验	ANS	3	3位随机数字
        /// </summary>
        public int LRC { get; set; }
        /// <summary>
        /// 授权号
        /// </summary>
        public string AuthCode { get; set; }
        #endregion

        #region 公共方法
        public byte[] ToBytes()
        {
            string ret = "";
            string space = "";
            ret += string.IsNullOrEmpty(this.TYPE) ? "01" : this.TYPE;
            ret += string.IsNullOrEmpty(this.POSID) ? space.PadLeft(8, ' ') : this.POSID.PadRight(8, ' ');
            ret += string.IsNullOrEmpty(this.STAFFID) ? space.PadLeft(8, ' ') : this.STAFFID.PadRight(8, ' ');
            ret += this.PAYTYPE;
            ret += MONEY.ToString().PadLeft(12, '0');
            ret += string.IsNullOrEmpty(PAYDATE) ? space.PadLeft(8, ' ') : this.PAYDATE;
            ret += string.IsNullOrEmpty(REFERENCE) ? space.PadLeft(12, ' ') : this.REFERENCE;
            ret += string.IsNullOrEmpty(ORDERID) ? space.PadLeft(6, ' ') : this.ORDERID;
            ret += LRC.ToString("D3");
            ret += string.IsNullOrEmpty(AuthCode) ? space.PadLeft(6, ' ') : this.AuthCode;
            return System.Text.ASCIIEncoding.Default.GetBytes(ret);
        }
        #endregion
    }
}
