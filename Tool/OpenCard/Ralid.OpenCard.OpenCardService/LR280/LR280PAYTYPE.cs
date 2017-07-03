using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.LR280
{
    internal class LR280PAYTYPE
    {
        #region 静态变量
        public static readonly string 消费 = "00";
        public static readonly string 撤消 = "01";
        public static readonly string 退货 = "02";
        public static readonly string 查余额 = "03";
        public static readonly string 取打印 = "04";
        public static readonly string 签到 = "05";
        public static readonly string 结算 = "06";
        public static readonly string 读卡 = "70";
        public static readonly string 握手 = "99";
        public static readonly string 查询状态 = "SQ";
        public static readonly string PING网络测试 = "PI";
        public static readonly string 关闭资源 = "CL";
        public static readonly string 设置超时时间 = "TI";
        #endregion
    }
}
