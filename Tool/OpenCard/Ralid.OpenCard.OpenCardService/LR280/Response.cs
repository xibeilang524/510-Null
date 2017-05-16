using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.LR280
{
    internal class Response
    {
        #region 工厂方法
        public static Response GetResponse(byte[] val)
        {
            if (val == null || val.Length == 0) return null;
            var str = System.Text.ASCIIEncoding.Default.GetString(val);
            Response ret = new Response();
            ret.RETURNCODE = Encoding.GetEncoding("GB2312").GetString(val, 0, 2).Trim();
            ret.BANKNO = Encoding.GetEncoding("GB2312").GetString(val, 2, 4).Trim();
            ret.CARDNO = Encoding.GetEncoding("GB2312").GetString(val, 6, 20).Trim();
            ret.ORDERID = Encoding.GetEncoding("GB2312").GetString(val, 26, 6).Trim();
            ret.MONEY = Encoding.GetEncoding("GB2312").GetString(val, 32, 12);
            ret.ERRORMSG = Encoding.GetEncoding("GB2312").GetString(val, 44, 40);
            ret.BUSINESSNO = Encoding.GetEncoding("GB2312").GetString(val, 84, 15);
            ret.TERMINAL = Encoding.GetEncoding("GB2312").GetString(val, 99, 8);
            ret.BATCHNO = Encoding.GetEncoding("GB2312").GetString(val, 107, 6);
            ret.PAYDATE = Encoding.GetEncoding("GB2312").GetString(val, 113, 4);
            ret.PAYTIME = Encoding.GetEncoding("GB2312").GetString(val, 117, 6);
            ret.REFERENCE = Encoding.GetEncoding("GB2312").GetString(val, 123, 12);
            ret.AUTHORIZE = Encoding.GetEncoding("GB2312").GetString(val, 135, 6);
            ret.CLEARDATE = Encoding.GetEncoding("GB2312").GetString(val, 141, 4);
            ret.LRC = Encoding.GetEncoding("GB2312").GetString(val, 145, 3);
            return ret;
        }

        private static string GetString(byte[] val, int index, int count)
        {
            return Encoding.GetEncoding("GB2312").GetString(val, index, count).Replace("\0", "");
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 返回码	N	2	00 表示成功，其它表示失败
        /// </summary>
        public string RETURNCODE { get; set; }
        /// <summary>
        /// 银行行号	N	4	发卡行代码
        /// </summary>
        public string BANKNO { get; set; }
        /// <summary>
        /// 卡号	N	20	卡号（屏蔽部分，保留前6后4）
        /// </summary>
        public string CARDNO { get; set; }
        /// <summary>
        /// 凭证号	N	6	
        /// </summary>
        public string ORDERID { get; set; }
        /// <summary>
        /// 金额	N	12	
        /// </summary>
        public string MONEY { get; set; }
        /// <summary>
        /// 错误说明	ANS	40	中文解释
        /// </summary>
        public string ERRORMSG { get; set; }
        /// <summary>
        /// 商户号	N	15
        /// </summary>
        public string BUSINESSNO { get; set; }
        /// <summary>
        /// 终端号	N	8
        /// </summary>
        public string TERMINAL { get; set; }
        /// <summary>
        /// 批次号	N	6
        /// </summary>
        public string BATCHNO { get; set; }
        /// <summary>
        /// 交易日期	N	4
        /// </summary>
        public string PAYDATE { get; set; }
        /// <summary>
        /// 交易时间	N	6	
        /// </summary>
        public string PAYTIME { get; set; }
        /// <summary>
        /// 交易参考号	N	12
        /// </summary>
        public string REFERENCE { get; set; }
        /// <summary>
        /// 授权号	N	6
        /// </summary>
        public string AUTHORIZE { get; set; }

        /// <summary>
        /// 清算日期	N	4	
        /// </summary>
        public string CLEARDATE { get; set; }
        /// <summary>
        /// 会员卡数据
        /// </summary>
        public string CARDDATA { get; set; }
        /// <summary>
        /// 二维码路径
        /// </summary>
        public string TwoCode { get; set; }
        /// <summary>
        /// 账单号
        /// </summary>
        public string QRnum { get; set; }
        /// <summary>
        /// 账单时间
        /// </summary>
        public string QRtime { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        public string TranCode { get; set; }
        /// <summary>
        /// ANS	3	三位数字，应该和请求一致
        /// </summary>
        public string LRC { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public string TranType { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public string DiscountAmount { get; set; }
        /// <summary>
        /// tmsData
        /// </summary>
        public string TMSData { get; set; }
        #endregion
    }
}
