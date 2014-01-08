using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.ParkService
{
    /// <summary>
    /// 停车场上传消息事件处理程序
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sender"></param>
    /// <param name="report"></param>
    public delegate void ReportingHandler<T>(object sender, T report);
}
