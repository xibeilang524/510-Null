using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.LR280
{
    public class LR280CardInfo
    {
        /// <summary>
        /// 返回码，00表示成功，其它值表示失败
        /// </summary>
        public string ErrorCode { get; set; }

        public string ErrorStr { get; set; }

        public string CardNo { get; set; }

        public int CardType { get; set; }

        public string BankNo { get; set; }

        public int Balance { get; set; }
    }
}
