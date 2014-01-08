using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.UI
{
    public class ReportEventArgs<T>:EventArgs 
    {
        /// <summary>
        /// 报告
        /// </summary>
        public T Report { get; set; }
    }
}
