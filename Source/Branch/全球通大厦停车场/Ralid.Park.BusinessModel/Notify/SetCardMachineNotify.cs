using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Interface;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 发卡机控制指令　参数;0=关闭;1=启用
    /// </summary>
    [DataContract]
    public class SetCardMachineNotify
    {
        private byte _action;
        [DataMember]
        public byte Action
        {
            get { return _action; }
            set { _action = value; }
        }

        public SetCardMachineNotify(byte action)
        {
            this._action = action;
        }
    }
}
