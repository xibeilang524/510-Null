using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.ETC.Response
{
    internal class WriteCardResponse : ETCResponse
    {
        /// <summary>
        /// 密钥服务流水
        /// </summary>
        public string KeyServiceNo { get; set; }
        /// <summary>
        /// 消费类型  6:普通消   9:复合消费
        /// </summary>
        public int TradeType { get; set; }
        /// <summary>
        /// 终端交易序列号
        /// </summary>
        public string TermTradeNo { get; set; }
        /// <summary>
        /// 卡片交易序列号
        /// </summary>
        public string CardTradeNo { get; set; }
        /// <summary>
        /// PSAM终端号
        /// </summary>
        public string TermCode { get; set; }
        /// <summary>
        /// 校验数据
        /// </summary>
        public string Tac { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public int CashMoney { get; set; }
        /// <summary>
        /// 卡片余额（单位：分，消费后的余额）
        /// </summary>
        public long Balance { get; set; }
    }
}