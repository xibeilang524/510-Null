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

        public RemoteReadCardNotify(int parkID, int entranceID, string cardID, string carPlate, string operatorID, string station)
        {
            ParkID = parkID;
            EntranceID = entranceID;
            CardID = cardID;
            CarPlate = carPlate;
            OperatorID = operatorID;
            Station = station;
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
        /// 获取或设置卡号（车牌事件时为空）
        /// </summary>
        [DataMember]
        public string CardID { get; set; }

        /// <summary>
        /// 获取或设置车牌号码（车牌无车牌时填入为空）
        /// </summary>
        [DataMember]
        public string CarPlate { get; set; }

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


        /// <summary>
        /// 获取或设置远程读卡的操作员ID
        /// </summary>
        [DataMember]
        public string OperatorID { get; set; }

        /// <summary>
        /// 获取或设置远程读卡的工作站
        /// </summary>
        [DataMember]
        public string Station { get; set; }

        /// <summary>
        /// 获取或设置远程读卡的工作站
        /// </summary>
        [DataMember]
        public Ralid.Park.BusinessModel.Enum.EntranceReader? Reader { get; set; }
    }
}
