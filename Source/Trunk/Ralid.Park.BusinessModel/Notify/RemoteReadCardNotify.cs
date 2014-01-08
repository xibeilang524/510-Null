using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Notify
{
    /// <summary>
    /// 表示一个远程读卡通知
    /// </summary>
    [DataContract]
    public class RemoteReadCardNotify
    {
        public RemoteReadCardNotify()
        {
        }

        public RemoteReadCardNotify(int parkID, int entranceID, string cardID)
        {
            ParkID = parkID;
            EntranceID = entranceID;
            CardID = cardID;
        }

        public RemoteReadCardNotify(int parkID, int entranceID, string cardID, byte[] parkingData)
        {
            ParkID = parkID;
            EntranceID = entranceID;
            CardID = cardID;
            ParkingData = parkingData;
        }

        public RemoteReadCardNotify(int parkID, int entranceID, string cardID, byte[] parkingData, string lastCarPlate)
        {
            ParkID = parkID;
            EntranceID = entranceID;
            CardID = cardID;
            ParkingData = parkingData;
            LastCarPlate = lastCarPlate;
        }

        /// <summary>
        /// 获取或设置停车场ID
        /// </summary>
        [DataMember]
        public int ParkID { get; set; }

        /// <summary>
        /// 获取或设置通道地址
        /// </summary>
        [DataMember]
        public int EntranceID { get; set; }

        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        [DataMember]
        public string CardID { get; set; }

        /// <summary>
        /// 获取或设置卡片的停车场数据
        /// </summary>
        [DataMember]
        public byte[] ParkingData { get; set; }

        /// <summary>
        /// 获取或设置上次识别到的车牌号码
        /// </summary>
        [DataMember]
        public string LastCarPlate { get; set; }
    }
}
