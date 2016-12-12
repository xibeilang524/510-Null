using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.ETC.Response
{
    internal class ETCResponse
    {
        /// <summary>
        /// 错误代码，0表示成功，
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// 回复的原始内容字符串
        /// </summary>
        public string Content { get; set; }
    }
}
