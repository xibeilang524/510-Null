using System.ServiceModel;
using System.Collections.Generic;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Notify;

namespace Ralid.Park.ParkAdapter
{
    [ServiceContract(CallbackContract = typeof(IReportSinker))]
    public interface IParkingAdapter
    {
        /// <summary>
        /// 增加控制板
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddEntrance(EntranceInfo info);
        /// <summary>
        /// 更新控制板
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateEntrance(EntranceInfo info);
        /// <summary>
        /// 删除控制板
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [OperationContract]
        bool DeleteEntrance(EntranceInfo info);
        /// <summary>
        /// 同步控制器与上位机之前的数据
        /// </summary>
        /// <param name="entranceID"></param>
        /// <returns></returns>
        [OperationContract]
        bool SynEntranceData(int entranceID);
        /// <summary>
        /// 清空控制器保存的信息
        /// </summary>
        /// <param name="entranceID"></param>
        /// <returns></returns>
        [OperationContract]
        bool ClearEntranceData(int entranceID);
        /// <summary>
        /// 保存卡片(网络版写卡模式会下发到所有控制器，其余只会下发到主控制器）
        /// </summary>
        /// <param name="card">卡片</param>
        /// <param name="action">操作类型</param>
        [OperationContract()]
        bool SaveCard(CardInfo card,ActionType action);
        /// <summary>
        /// 保存卡片到控制器
        /// </summary>
        /// <param name="entranceID">控制器ID</param>
        /// <param name="card">卡片</param>
        /// <param name="action">操作类型</param>
        [OperationContract()]
        bool SaveCardToEntrance(int entranceID, CardInfo card, ActionType action);
        /// <summary>
        /// 保存多张卡片到控制器
        /// </summary>
        /// <param name="centranceID">控制器</param>
        /// <param name="cards">卡片</param>
        /// <param name="action">操作类型</param>
        /// <returns></returns>
        [OperationContract()]
        bool SaveCardsToEntrance(int centranceID,List<CardInfo> cards,ActionType action);
        /// <summary>
        /// 删除卡片(网络版写卡模式会下发到所有控制器，其余只会下发到主控制器）
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        [OperationContract()]
        bool DeleteCard(CardInfo card);
        /// <summary>
        /// 删除控制器卡片
        /// </summary>
        /// <param name="card"></param>
        /// <param name="entranceID">控制器ID</param>
        /// <returns></returns>
        [OperationContract()]
        bool DeleteCardToEntrance(int entranceID, CardInfo card);
        /// <summary>
        /// 清空所有卡片(网络版写卡模式会下发到所有控制器，其余只会下发到主控制器）
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool ClearCards();
        /// <summary>
        /// 清空所有卡片
        /// </summary>
        /// <param name="entranceID">控制器ID</param>
        /// <returns></returns>
        [OperationContract]
        bool ClearCardsToEntrance(int entranceID);

        /// <summary>
        /// 下载车位信息
        /// </summary>
        /// <param name="_vacantInfo"></param>
        [OperationContract()]
        bool DownloadVacantSetting(CarPortSetting vacantSetting);

        /// <summary>
        /// 修正空车位数
        /// </summary>
        /// <param name="vacant">需要增加的空车位数，负数为减小</param>
        /// <returns></returns>
        [OperationContract()]
        bool ModifiedVacant(short vacant);

        /// <summary>
        /// 下载通道权限，网络型只会更新服务器实例，不会下发到控制器
        /// </summary>
        /// <param name="ascLevel"></param>
        [OperationContract()]
        bool DownloadAccessSetting(AccessSetting ascLevel);


        /// <summary>
        /// 下载收费设置，网络型只会更新服务器实例，不会下发到控制器
        /// </summary>
        /// <param name="tariffSetting">收费设置</param>
        /// <returns></returns>
        [OperationContract()]
        bool DownloadTariffSetting(TariffSetting tariffSetting);

        /// <summary>
        /// 下载节假日设置，网络型只会更新服务器实例，不会下发到控制器
        /// </summary>
        /// <param name="holidaySetting"></param>
        /// <returns></returns>
        [OperationContract()]
        bool DownloadHolidaySetting(HolidaySetting holidaySetting);

        /// <summary>
        /// 下载用户设置
        /// </summary>
        /// <param name="us"></param>
        /// <returns></returns>
        [OperationContract]
        bool DownLoadUserSetting(UserSetting us);

        /// <summary>
        /// 下载车型信息
        /// </summary>
        /// <param name="carTypeSetting"></param>
        /// <returns></returns>
        [OperationContract]
        bool DownloadCarTypeSetting(CarTypeSetting carTypeSetting);

        /// <summary>
        /// 事件无效
        /// </summary>
        /// <param name="notify"></param>
        [OperationContract()]
        bool EventInvalid(EventInvalidNotify notify);

        /// <summary>
        /// 事件有效
        /// </summary>
        /// <param name="notify"></param>
        [OperationContract()]
        bool EventValid(EventValidNotify notify);

        /// <summary>
        ///道闸操作
        /// </summary>
        /// <param name="notify"></param>
        [OperationContract()]
        bool GateOperation(GateOperationNotify notify);

        /// <summary>
        /// 设置LED面板即时显示，这些信息不会保存到LED板中
        /// </summary>
        /// <param name="notify"></param>
        [OperationContract(IsOneWay = true)]
        void LedDisplay(SetLedDisplayNotify notify);

        /// <summary>
        /// 复位控制器
        /// </summary>
        /// <param name="notify"></param>
        [OperationContract()]
        bool ResetDevice(DeviceReSetNotify notify);

        /// <summary>
        /// 更换卡片收费车型
        /// </summary>
        /// <param name="notify"></param>
        [OperationContract()]
        bool SwitchCarType(CarTypeSwitchNotify notify);

        /// <summary>
        /// 更换卡片收费通道
        /// </summary>
        /// <param name="notify"></param>
        [OperationContract()]
        bool SwitchEntrance(EntranceSwitchNotify notify);

        /// <summary>
        /// 设置入口通道临时卡数量
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool SetEntranceRemainTempCard(EntranceRemainTempCardNotify notify);

        /// <summary>
        /// 远程读卡,即通过软件模拟通道上读卡器上的读卡,产生的读卡事件与正常刷卡一样
        /// </summary>
        /// <param name="notify"></param>
        /// <returns></returns>
        [OperationContract]
        bool RemoteReadCard(RemoteReadCardNotify notify);

        /// <summary>
        /// 更新系统参数设置通知
        /// </summary>
        /// <param name="notify"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateSystemParamSetting(UpdateSystemParamSettingNotity notify);

        /// <summary>
        /// 事件订阅指定串口事件
        /// </summary>
        [OperationContract()]
        bool Subscription();

        /// <summary>
        /// 取消订阅指定串口事件
        /// </summary>
        [OperationContract()]
        bool UnSubscription();

        /// <summary>
        /// 回声,用于客户端检测与服务器连接是否正常
        /// </summary>
        /// <param name="echo"></param>
        /// <returns></returns>
        [OperationContract]
        string Echo(string echo);


        #region 写卡模式新增
        /// <summary>
        /// 获取停车场状态
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        EntranceStatus GetParkStatus();
        /// <summary>
        /// 获取控制器状态
        /// </summary>
        /// <param name="entranceID">控制器ID</param>
        /// <returns></returns>
        [OperationContract]
        EntranceStatus GetEntranceStatus(int entranceID);
        ///// <summary>
        ///// 获取服务器的工作模式
        ///// </summary>
        ///// <returns></returns>
        //[OperationContract]
        //byte GetServerWorkMode();

        ///// <summary>
        ///// 从服务器生成缴费记录
        ///// </summary>
        ///// <param name="card">卡片信息实体类</param>
        ///// <param name="carType">车辆类型</param>
        ///// <returns></returns>
        //[OperationContract]
        //CardPaymentInfo CreateCardPaymentRecord(CardInfo card, byte carType);

        /// <summary>
        /// 从服务器生成缴费记录
        /// </summary>
        /// <param name="card">卡片信息实体类</param>
        /// <param name="carType">车辆类型</param>
        /// <param name="datetime">缴费时间</param>
        /// <returns></returns>
        [OperationContract]
        CardPaymentInfo CreateCardPaymentRecord(CardInfo card, byte carType, System.DateTime datetime);

        /// <summary>
        /// 下载通道权限到控制器
        /// </summary>
        /// <param name="ascLevel"></param>
        [OperationContract()]
        bool DownloadAccessSettingToEntrance(int entranceID, AccessSetting ascLevel);

        /// <summary>
        /// 下载节假日设置到控制器
        /// </summary>
        /// <param name="holidaySetting"></param>
        /// <returns></returns>
        [OperationContract()]
        bool DownloadHolidaySettingToEntrance(int entranceID, HolidaySetting holidaySetting);

        /// <summary>
        /// 下载收费设置到控制器
        /// </summary>
        /// <param name="tariffSetting">收费设置</param>
        /// <returns></returns>
        [OperationContract()]
        bool DownloadTariffSettingToEntrance(int entranceID, TariffSetting tariffSetting);

        /// <summary>
        /// 下载密钥设置
        /// </summary>
        /// <param name="entranceID"></param>
        /// <param name="keySetting"></param>
        /// <returns></returns>
        [OperationContract()]
        bool DownloadKeySetting( KeySetting keySetting);

        /// <summary>
        /// 下载密钥设置到控制器
        /// </summary>
        /// <param name="entranceID"></param>
        /// <param name="keySetting"></param>
        /// <returns></returns>
        [OperationContract()]
        bool DownloadKeySettingToEntrance(int entranceID, KeySetting keySetting);

        /// <summary>
        /// WaitingCommand服务启用
        /// </summary>
        /// <param name="enable">是否启用</param>
        [OperationContract()]
        void WaitingCommandServiceEnable(bool enable);
        #endregion
    }
}
