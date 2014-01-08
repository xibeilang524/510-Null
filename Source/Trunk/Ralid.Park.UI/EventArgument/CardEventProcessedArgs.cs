using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Report;

namespace Ralid.Park.UI.EventArgument
{
    [Serializable]
    public class CardEventProcessedArgs:EventArgs 
    {
        public CardEventProcessedArgs()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardEvent">已经处理的刷卡事件</param>
        public CardEventProcessedArgs(CardEventReport cardEvent,int action)
        {
            ProcessedEvent = cardEvent;
            Action = action;
        }

        /// <summary>
        /// 获取或设置已经处理完成的事件
        /// </summary>
        public CardEventReport ProcessedEvent { get; set; }

        /// <summary>
        /// 获取或设置事件处理方式(0 事件有效，1 事件无效)
        /// </summary>
        public int Action { get; set; }
    }
}
