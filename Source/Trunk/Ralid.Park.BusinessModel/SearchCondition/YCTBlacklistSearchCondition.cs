using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.SearchCondition
{
    public class YCTBlacklistSearchCondition : SearchCondition
    {
        public int? WalletType { get; set; }

        public bool OnlyCatched { get; set; }

        public bool OnlyUnUploaded { get; set; }
    }
}
