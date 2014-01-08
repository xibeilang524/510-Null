using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 提取卡片档案申请
    /// </summary>
    [DataContract]
    public class FetchCardRequesNotify
    {
        /// <summary>
        /// 获取或设置要获取卡片的ID
        /// </summary>
        [DataMember]
        public string CardID { get; set; }

        public FetchCardRequesNotify()
        {
        }

        public FetchCardRequesNotify(int cardID)
        {
            CardID = cardID.ToString ();
        }

        public FetchCardRequesNotify(string cardID)
        {
            CardID = cardID;
        }
    }
}
