using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示CAN总线的设备地址
    /// </summary>
    [DataContract]
    public class CanAddress
    {
        /// <summary>
        /// 一号通信主机的地址
        /// </summary>
        public static byte HostEntrance
        {
            get { return 1; }
        }
        /// <summary>
        /// 所有设备广播地址
        /// </summary>
        public static byte BroadCast
        {
            get
            {
                return 0xf0;
            }
        }

        /// <summary>
        /// 所有通道广播地址
        /// </summary>
        public static byte EntranceBroadCast
        {
            get
            {
                return 0x3f;
            }
        }

        /// <summary>
        /// 票箱LED地址
        /// </summary>
        public static byte TicketBoxLed
        {
            get
            {
                return 97;
            }
        }

        /// <summary>
        /// 收费LED地址
        /// </summary>
        public static byte ChargeLed
        {
            get
            {
                return 98;
            }
        }

        public static byte VacantLed
        {
            get
            {
                return 99;
            }
        }

        /// <summary>
        /// 所有LED广播地址
        /// </summary>
        public static byte LedBroadCast
        {
            get
            {
                return 96;
            }
        }
    }
}
