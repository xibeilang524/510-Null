using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel .Report ;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.ParkAdapter
{ 
    /// <summary>
    /// 报告源
    /// </summary>
    public interface IReportSource
    {
        /// <summary>
        /// 收到服务器上传事件时产生此事件
        /// </summary>
        event ReportHandler<ReportBase> Reporting;
    }
}
