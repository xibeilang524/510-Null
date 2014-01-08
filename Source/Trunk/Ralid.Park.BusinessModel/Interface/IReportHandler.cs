using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.BusinessModel .Report ;

namespace Ralid.Park.BusinessModel.Interface
{
    public interface  IReportHandler
    {
        /// <summary>
        /// 处理来自控制器的消息
        /// </summary>
        /// <param name="report"></param>
        void ProcessReport(ReportBase report);
    }
}
