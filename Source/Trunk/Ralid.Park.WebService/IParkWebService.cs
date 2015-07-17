using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.WebService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IParkWebService”。
    [ServiceContract]
    public interface IParkWebService
    {
        #region 卡片管理
        /// <summary>
        /// 通过卡号获取卡片信息(查找)
        /// </summary>
        /// <param name="cardID">卡号</param>
        /// <returns></returns>
        [OperationContract]
        QueryResult<CardInfo> GetCardByID(string cardID);

        /// <summary>
        /// 保存卡片信息(新增或修改)
        /// </summary>
        /// <param name="cardID">卡号</param>
        /// <param name="cardNum">卡片编号</param>
        /// <param name="cardType">卡片类型</param>
        /// <param name="carType">收费车型</param>
        /// <param name="status">卡片状态</param>
        /// <param name="index">序号</param>
        /// <param name="parkingFlag">停车标志</param>
        /// <param name="lastDateTime">上一次刷卡时间</param>
        /// <param name="lastEntrance">上次刷卡的通道</param>
        /// <param name="accessID">权限组号</param>
        /// <param name="activationDate">卡片的生效时间</param>
        /// <param name="validDate">有效日期</param>
        /// <param name="balance">储值余额</param>
        /// <param name="deposit">卡片的押金</param>
        /// <param name="discountHour">卡片未用的优惠时数</param>
        /// <param name="options">卡片选项</param>
        /// <returns></returns>
        [OperationContract]
        InterfaceReturnCode SaveCard(string cardID, short cardNum, byte carType, byte status, short index,
            int lastEntrance, string activationDate, DateTime validDate, decimal balance,
            decimal deposit, int discountHour, int options);

        [OperationContract]
        CommandResult SaveCard2(CardInfo card);

        /// <summary>
        /// 通过卡号删除卡片
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        [OperationContract]
        CommandResult DeleteCard(string cardID);
        #endregion

        #region 空车位读取、写入
        /// <summary>
        /// 获取车场车位余数
        /// </summary>
        /// <param name="parkid">车场ID</param>
        /// <returns></returns>
        [OperationContract]
        short GetVacant(int parkid);

        /// <summary>
        /// 设置车场车位余数
        /// </summary>
        /// <param name="parkid">车场ID</param>
        /// <param name="vacant">车位余数</param>
        /// <returns></returns>
        [OperationContract]
        InterfaceReturnCode SetVacant(int parkid, int vacant);
        #endregion

        #region 某张卡片的停车费用
        /// <summary>
        /// 获取卡片的本次收取费用
        /// </summary>
        /// <param name="info"></param>
        /// <param name="operatorInfo"></param>
        /// <returns></returns>
        [OperationContract]
        decimal GetCardLastPayment(CardInfo info, OperatorInfo operatorInfo);
        #endregion

        #region 各种记录查询
        /// <summary>
        /// 通过查询条件获取相应的卡片充值记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [OperationContract]
        QueryResultList<CardChargeRecord> GetCardChargeRecords(RecordSearchCondition search);

        /// <summary>
        /// 通过查询条件获取相应的卡片延期记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [OperationContract]
        QueryResultList<CardDeferRecord> GetCardDeferRecords(RecordSearchCondition search);

        /// <summary>
        /// 通过查询条件获取相应的卡片挂失恢复记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [OperationContract]
        QueryResultList<CardLostRestoreRecord> GetCardLostRestoreRecords(RecordSearchCondition search);

        /// <summary>
        /// 通过查询条件获取相应的卡片禁用启用记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [OperationContract]
        QueryResultList<CardDisableEnableRecord> GetCardDisableEnableRecords(RecordSearchCondition search);

        /// <summary>
        /// 通过查询条件获取相应的卡片回收记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [OperationContract]
        QueryResultList<CardRecycleRecord> GetCardRecycleRecords(RecordSearchCondition search);

        /// <summary>
        /// 通过查询条件获取相应的卡片发行记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [OperationContract]
        QueryResultList<CardReleaseRecord> GetCardReleaseRecords(RecordSearchCondition search);

        /// <summary>
        /// 通过查询条件获取相应的卡片删除记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [OperationContract]
        QueryResultList<CardDeleteRecord> GetCardDeleteRecords(RecordSearchCondition search);
        #endregion

        //#region 设备状态
        ////???
        //#endregion

        #region 获取设备清单
        /// <summary>
        /// 获取所有设备
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        QueryResultList<EntranceInfo> GetAllEntraces();
        #endregion

        #region 卡片状态
        /// <summary>
        /// 根据卡号获取卡片状态
        /// </summary>
        /// <param name="cardID">卡号</param>
        /// <returns></returns>
        [OperationContract]
        CardStatus GetCardStatusByCardID(string cardID);
        #endregion

        #region 停车场信息接口
        /// <summary>
        /// 获取车牌号码所在停车场
        /// </summary>
        /// <param name="carPlate"></param>
        /// <returns></returns>
        [OperationContract]
        QueryResult<ParkInfo> QueryParkByCarPlate(string carPlate);
        /// <summary>
        /// 获取所有停车场信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        QueryResultList<ParkInfo> GetAllPark();
        #endregion

        #region 停车费用支付接口实
        /// <summary>
        /// 获取某卡号的停车收费信息接口
        /// </summary>
        /// <param name="cardID">卡号</param>
        /// <param name="discountHour">优惠时长</param>
        /// <param name="discountAmount">优惠金额</param>
        /// <param name="reserve1">预留1</param>
        /// <param name="reserve2">预留2</param>
        /// <returns>Result 0：成功，其他：失败；QueryObject：返回收费信息对象</returns>
        [OperationContract]
        QueryResult<WSCardPaymentInfo> GetCardPayment(string cardID, string discountHour, string discountAmount, string reserve1, string reserve2);

        /// <summary>
        /// 停车收费接口
        /// </summary>
        /// <param name="cardID">卡号</param>
        /// <param name="chargeDateTime">计费时间（格式：yyyy-MM-dd HH:mm:ss.fff）</param>
        /// <param name="paid">实付金额</param>
        /// <param name="payMode">支付方式[0代表现金，1代表微信，…]</param>
        /// <param name="memo">费用说明</param>
        /// <param name="reserve1">预留1</param>
        /// <param name="reserve2">预留2</param>
        /// <returns>Result 0：成功，其他：失败</returns>
        [OperationContract]
        CommandResult CardFeePay(string cardID, string chargeDateTime, string paid, string payMode, string memo, string reserve1, string reserve2);
        #endregion
    }
}
