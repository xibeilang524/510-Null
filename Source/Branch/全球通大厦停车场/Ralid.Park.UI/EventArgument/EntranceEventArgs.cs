using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.UI
{
    public class EntranceEventArgs : EventArgs
    {
        /// <summary>
        /// 控制器ID
        /// </summary>
        public int EntranceID { get; set; }
    }
}
