using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 设置LedScreen输出
    /// </summary>
    [DataContract]
    public class SetLedDisplayNotify
    {
        #region 构造函数
        public SetLedDisplayNotify(int entranceID, byte ledAddress, string msg, bool isPermanent, byte index)
        {
            EntranceID = entranceID;
            LedAddress = ledAddress;
            DisplayMsg = msg;
            IsPermanent = isPermanent;
            MsgIndex = index;
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 控制器ID
        /// </summary>
        [DataMember]
        public int EntranceID { get; set; }

        /// <summary>
        /// LED屏的地址
        /// </summary>
        [DataMember]
        public byte LedAddress { get; set; }

        /// <summary>
        /// 要显示的字符串
        /// </summary>
        [DataMember]
        public string DisplayMsg { get; set; }
        /// <summary>
        /// 是否永久保存在LED中
        /// </summary>
        [DataMember]
        public bool IsPermanent { get; set; }

        /// <summary>
        /// 字符串的索引号,只在要永久保存的信息时有用(0-255)
        /// </summary>
        [DataMember]
        public byte MsgIndex { get; set; }
        #endregion
    }
}
