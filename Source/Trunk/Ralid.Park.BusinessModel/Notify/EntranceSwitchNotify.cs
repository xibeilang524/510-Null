using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 转换通道通知
    /// </summary>
    [DataContract]
    public class EntranceSwitchNotify
    {
        /// <summary>
        /// 获取或设置当前控制器ID
        /// </summary>
        [DataMember]
        public int CurrentEntranceID { get; set; }

        /// <summary>
        /// 获取或设置新的控制器ID
        /// </summary>
        [DataMember]
        public int NewEntranceID { get; set; }

        public EntranceSwitchNotify(int currentID, int newID)
        {
            this.CurrentEntranceID = currentID;
            this.NewEntranceID = newID;
        }

        public EntranceSwitchNotify()
        {
        }
    }
}
