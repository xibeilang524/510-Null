using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.OpenCard.OpenCardService.ETC
{
    [DataContract]
    public class ETCSetting
    {
        public static ETCSetting Current { get; set; }

        public static readonly string CardTyte = "粤通卡";

        #region 公共属性
        [DataMember]
        public Dictionary<string, int> ETCEntrancePairs { get; set; }
        #endregion
    }
}
