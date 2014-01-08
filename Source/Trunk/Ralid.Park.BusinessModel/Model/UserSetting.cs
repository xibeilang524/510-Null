using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Ralid.GeneralLibrary.CardReader;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示系统常规设置
    /// </summary>
    [DataContract()]
    public class UserSetting
    {
        #region 静态属性
        /// <summary>
        /// 获取或设置系统当前的常规设置
        /// </summary>
        public static UserSetting Current { get; set; }
        #endregion

        #region 构造函数
        public UserSetting()
        {
            CompanyName = "广州瑞立德停车场系统";
            WegenType = Ralid.GeneralLibrary.CardReader.WegenType.Wengen34;
            SoftWareCarPlateRecognize = true;
        }
        #endregion

        #region 私有变量
        [DataMember]
        private bool _CardValidNeedResponse;
        #endregion

        #region 常规设置
        /// <summary>
        /// 获取或设置公司名称，即在控制器LED上显示的公司名称
        /// </summary>
        [DataMember ]
        public string CompanyName{get;set;}
        /// <summary>
        /// 获取或设置数据库中是否只保留最近几个月的车辆进出抓拍图片
        /// </summary>
        [DataMember]
        public bool EnableDeleteOverTimeImages { get; set; }
        /// <summary>
        /// 获取或设置数据库中保留最近多少个月的车辆进出抓拍图片
        /// </summary>
        [DataMember]
        public int Month { get; set; }
        /// <summary>
        /// 获取或设置是否强制交班
        /// </summary>
        [DataMember]
        public bool EnableForceShifting { get; set; }
        /// <summary>
        /// 获取或设置强制交班时间
        /// </summary>
        [DataMember]
        public TimeEntity  ForceShiftingTime { get; set; }
        /// <summary>
        /// 获取或设置停车收费自定义说明
        /// </summary>
        [DataMember]
        public List<string> PaymentComments { get; set; }
        /// <summary>
        /// 获取或设置系统使用的Wegen协议
        /// </summary>
        [DataMember]
        public Ralid.GeneralLibrary.CardReader.WegenType  WegenType { get; set; }
        /// <summary>
        /// 获取或设置一键开闸,Add By Tom,2012-3-6
        /// </summary>
        [DataMember]
        public bool OneKeyOpenDoor { get; set; }
        /// <summary>
        /// 获取或设置车压地感时是否抓拍图片
        /// </summary>
        [DataMember]
        public bool SnapshotWhenCarArrive { get; set; }
        /// <summary>
        /// 获取或设置是否在操作员结算时要输入上交金额
        /// </summary>
        [DataMember]
        public bool InputHandInCashWhenSettle { get; set; }
        /// <summary>
        /// 获取或设置是否启用澳大户外屏
        /// </summary>
        [DataMember]
        public bool EnableOutdoorLed { get; set; }
        /// <summary>
        /// 获取或设置最低临时卡报警的临时卡临界数量(小于零时不报警)
        /// </summary>
        [DataMember]
        public int MinTempCard { get; set; }

        /// <summary>
        /// 获取或设置视频服务器类型 0表示ACTI视频服务器，1表示信路通视频服务器
        /// </summary>
        [DataMember]
        public int VideoType { get; set; }
        /// <summary>
        /// 获取或设置是否在操作员写卡模式结算时要读取操作员卡金额
        /// </summary>
        [DataMember]
        public bool OperatorCardCashWhenSettle { get; set; }
        #endregion

        #region 车牌识别选项
        /// <summary>
        /// 获取或设置是否启用车牌识别
        /// </summary>
        [DataMember]
        public bool EnableCarPlateRecognize { get; set; }

        /// <summary>
        /// 获取或设置车牌识别允许有多少位误差
        /// </summary>
        [DataMember]
        public int MaxCarPlateErrorChar { get; set; }

        /// <summary>
        /// 获取或设置固定卡车入场和出场车牌对比失败需确认(此时比对的是车辆登记时的车牌号)
        /// </summary>
        [DataMember]
        public bool FixCardEnterAndExitWaitWhenCarPlateFail { get; set; }

        /// <summary>
        /// 获取或设置固定卡车出场车牌对比失败需确认
        /// </summary>
        [DataMember]
        public bool FixCardExitWaitWhenCarPlateFail { get; set; }

        /// <summary>
        /// 获取或设置临时卡车出场车牌对比失败需确认
        /// </summary>
        [DataMember]
        public bool TempCardExitWaitWhenCarPlateFail { get; set; }

        /// <summary>
        /// 获取或设置是否使用软件车牌识别
        /// </summary>
        [DataMember]
        public bool SoftWareCarPlateRecognize { get; set; }

        /// <summary>
        /// 获取或设置是否使用硬件车牌识别
        /// </summary>
        [DataMember]
        public bool HardWareCarPlateRecognize { get; set; }
        #endregion
    }
}
