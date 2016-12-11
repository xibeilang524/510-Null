using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.ETC.Response
{
    internal class OBUSearchResponse : ETCResponse
    {
        public string OBUID { get; set; }
        public string OBUNO { get; set; }
    }
}
