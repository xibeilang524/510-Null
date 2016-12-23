using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.OpenCard .OpenCardService.ETC.Response ;

namespace Ralid.OpenCard.OpenCardService.ETC
{
    internal class ReadOBUInfoEventArgs : EventArgs
    {
        public GetOBUInfoResponse OBUInfo { get; set; }
    }

    internal class ReadCardInfoEventArgs : EventArgs
    {
        public GetCardInfoResponse CardInfo { get; set; }
    }
}
