using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.BusinessModel.Enum ;
using Ralid.BusinessModel.Model;
using Ralid.BusinessModel.Report ;

namespace Ralid.BusinessModel.Report
{
    /// <summary>
    /// 刷卡时,上传的停车费用信息
    /// </summary>
    [Serializable()]
    [DataContract]
    public class CardEventDetailReport : ReportBase
    {
        #region 硬件传上来的参数
        /// <summary>
        /// 获取或设置报告发生的通道
        /// </summary>
        [DataMember]
        public EntranceInfo Entrance { get; set; }

        /// <summary>
        /// 获取或设置上卡片上一次活动的通道,出口事件用它表示入口地址
        /// </summary>
        [DataMember]
        public byte LastAddress { get; set; }

        /// <summary>
        /// 获取或设置事件卡号
        /// </summary>
        [DataMember]
        public string CardID { get; set; }

        /// <summary>
        /// 获取或设置报警发生时间
        /// </summary>
        [DataMember]
        public DateTime EventDateTime { get; set; }

        /// <summary>
        /// 获取或设置卡片上次活动时间,出口事件则用它来表示入口时间
        /// </summary>
        [DataMember]
        public DateTime LastDateTime { get; set; }

        /// <summary>
        /// 获取或设置事件状态
        /// </summary>
        [DataMember]
        public byte EventStatus { get; set; }

        /// <summary>
        /// 获取卡片类型
        /// </summary>
        [DataMember]
        public CardType CardType { get; set; }

        /// <summary>
        /// 获取计费车型
        /// </summary>
        [DataMember]
        public CarType CarType { get; set; }

        /// <summary>
        /// 获取计费类型,分为普通收费，节假日收费，室内收费和室内节假日收费四种
        /// </summary>
        [DataMember]
        public TariffType TariffType { get; set; }

        /// <summary>
        /// 获取或设置停车状态(用于更新卡片的停车状态和判断卡片此次是否当成临时卡来收费)
        /// </summary>
        [DataMember]
        public ParkingStatus ParkingStatus { get; set; }

        /// <summary>
        /// 获取或设置处理此事件的操作员编号
        /// </summary>
        [DataMember]
        public string  OperatorID { get; set; }

        /// <summary>
        /// 获取或设置应收金额(元),入口事件为0
        /// </summary>
        [DataMember]
        public decimal Accounts { get; set; }

        /// <summary>
        /// 卡片余额(元),储值类卡用
        /// </summary>
        [DataMember]
        public decimal Balance { get; set; }

        /// <summary>
        /// 获取或设置上次事件时识别到的车牌(出场事件中为入场识别到的车牌,入场事件为空)
        /// </summary>
        [DataMember]
        public string LastCarPlate { get; set; }

        /// <summary>
        /// 获取或设置识别到的车牌
        /// </summary>
        [DataMember]
        public string CarPlate { get; set; }
        #endregion

        #region 只读属性
        /// <summary>
        /// 是否是出口刷卡事件
        /// </summary>
        public bool IsExitEvent
        {
            get
            {
                return Address % 2 == 1;
            }
        }

        /// <summary>
        /// 是否当成临时卡来收费(固定卡,月卡过期,储值卡余额不足也当成临时卡收费)
        /// </summary>
        public bool ChargeAsTempCard
        {
            get
            {
                return (CardType.IsTempCard) || ((ParkingStatus & ParkingStatus.BIT_AsTempCard) == ParkingStatus.BIT_AsTempCard);
            }
        }

        /// <summary>
        /// 进出车牌对比,如果车牌相同则返回TRUE
        /// </summary>
        /// <param name="maxCarPlateErrorChar">车牌允许误差的位数</param>
        /// <returns></returns>
        public bool CarPlateComparison(int maxCarPlateErrorChar)
        {
            bool success = false;
            int errChar = 0;
            if (!string.IsNullOrEmpty(CarPlate) && !string.IsNullOrEmpty(LastCarPlate))
            {
                if (CarPlate == LastCarPlate)
                {
                    success = true;
                }
                else
                {
                    int len = CarPlate.Length > LastCarPlate.Length ? CarPlate.Length : LastCarPlate.Length;
                    for (int i = 0; i < len; i++)
                    {
                        if (LastCarPlate.Length > i && CarPlate.Length > i)
                        {
                            if (CarPlate[i] != LastCarPlate[i])
                            {
                                errChar++;
                            }
                        }
                        else
                        {
                            errChar++;
                        }
                    }
                    success = (errChar <= maxCarPlateErrorChar);
                }
            }
            return success;
        }

        /// <summary>
        /// 获取停车时长
        /// </summary>
        public string ParkInterval
        {
            get
            {
                TimeSpan span = new TimeSpan(EventDateTime.Ticks - LastDateTime.Ticks);
                return string.Format("{0}小时{1}分钟", span.Days * 24 + span.Hours, span.Minutes);
            }
        }
        #endregion
    }
}
