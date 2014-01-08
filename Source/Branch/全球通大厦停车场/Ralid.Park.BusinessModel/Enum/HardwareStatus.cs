using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime .Serialization ;

namespace Ralid.BusinessModel.Enum
{
    /// <summary>
    /// 
    /// </summary>
    [System.Runtime.Serialization.DataContract]
    public enum HardwareStatus
    {
        [EnumMember]
        OffLine = 1,
        [EnumMember]
        OnLine = 0,
    }
}
