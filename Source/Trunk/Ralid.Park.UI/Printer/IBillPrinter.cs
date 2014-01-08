using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing ;
using System.Text;
using Ralid.BusinessModel.Report ;
using Ralid.BusinessModel.Model;

namespace Ralid.Monitor
{
    /// <summary>
    /// 电脑小票打印机接口
    /// </summary>
    public interface IBillPrinter
    {
        /// <summary>
        /// 打印收费小票
        /// </summary>
        void PrintBill(Graphics g,CardInfo card,CardEventReport payment);
    }
}
