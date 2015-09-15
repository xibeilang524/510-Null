using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Ralid.OpenCard.YCTFtpTool
{
    public class EventListTraceLog : TraceListener
    {
        #region 构造函数
        public EventListTraceLog(FrmMain frm)
        {
            _ListBox = frm;
        }

        private FrmMain  _ListBox = null;
        #endregion

        #region 重写基类方法
        public override void Write(string message)
        {
            _ListBox.InsertMsg(message);
        }

        public override void WriteLine(string message)
        {
            _ListBox.InsertMsg(message);
        }
        #endregion
    }
}
