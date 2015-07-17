using System;
using System.Linq;
using System.Collections.Generic;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Factory;
using Ralid.Park.BusinessModel;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.WebService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“ParkWebService”。
    public class ParkWebService : IParkWebService
    {
        #region 私有方法
        private InterfaceReturnCode CreateInterfaceReturnCode(ResultCode code)
        {
            switch (code)
            {
                case ResultCode.Successful:
                    return InterfaceReturnCode.Success;//成功
                case ResultCode.CannotConnectServer:
                    return InterfaceReturnCode.CannotConnectDatabase;//连接数据库失败
                case ResultCode.NoRecord:
                    return InterfaceReturnCode.DatabaseNoRecord;//未找到记录
                case ResultCode.SaveDataError:
                    return InterfaceReturnCode.DatabaseSaveDataError;//数据写入数据库失败
                default:
                    return InterfaceReturnCode.DatabaseError;//失败
            }
        }

        /// <summary>
        /// 通过权限组名称获取权限组ID，权限组名称为空时表示所有权限，返回0，没有找到的返回-1
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int GetAccessID(string name)
        {
            if (string.IsNullOrEmpty(name)) return 0;

            SysParaSettingsBll sbll = new SysParaSettingsBll(AppConifg.Current.ParkingConnection);
            AccessSetting accessSetting = sbll.GetOrCreateSetting<AccessSetting>();
            AccessInfo access = null;
            if (accessSetting.Accesses != null) access = accessSetting.Accesses.FirstOrDefault(item => item.Name == name);
            if (access != null) return access.ID;

            return -1;
        }

        /// <summary>
        /// 检验卡片有效性
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private bool ValidateCard(CardInfo card, out string msg)
        {
            if (card.Status == CardStatus.Recycled) //卡片已注销
            {
                msg = "卡片已注销";
                return false;
            }
            if (card.Status == CardStatus.Disabled)  //卡片已锁定
            {
                msg = "卡片已锁定";
                return false;
            }
            if (card.Status == CardStatus.Loss)   //卡片已挂失
            {
                msg = "卡片已挂失";
                return false;
            }
            if (card.ActivationDate > DateTime.Now) //卡片未到生效期
            {
                msg = "卡片未到生效期";
                return false;
            }
            if (card.ValidDate < DateTime.Today && card.CardType != Ralid.Park.BusinessModel.Enum.CardType.TempCard && !card.EnableWhenExpired) //卡片已过期
            {
                msg = "卡片已过期";
                return false;
            }
            msg = string.Empty;
            return true;
        }
        #endregion

        #region IParkWebService接口实现方法
        public short GetVacant(int parkid)
        {
            short vacant = 0;
            QueryResult<ParkInfo> item = new ParkBll(AppConifg.Current.ParkingConnection).GetParkInfoByID(parkid);
            if (item != null && item.QueryObject != null)
            {
                vacant = item.QueryObject.Vacant;
            }
            return vacant;
        }

        public InterfaceReturnCode SetVacant(int parkid, int vacant)
        {
            CommandResult cr = new ParkBll(AppConifg.Current.ParkingConnection).UpdateVacant(parkid, (short)vacant);
            return CreateInterfaceReturnCode(cr.Result);
        }

        /// <summary>
        /// 通过查询条件获取相应的卡片充值记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardChargeRecord> GetCardChargeRecords(RecordSearchCondition search)
        {
            QueryResultList<CardChargeRecord> ret = new CardBll(AppConifg.Current.ParkingConnection).GetCardChargeRecords(search);
            return ret;
        }
        /// <summary>
        /// 通过查询条件获取相应的卡片延期记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardDeferRecord> GetCardDeferRecords(RecordSearchCondition search)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).GetCardDeferRecords(search);
        }
        /// <summary>
        /// 通过查询条件获取相应的卡片挂失恢复记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardLostRestoreRecord> GetCardLostRestoreRecords(RecordSearchCondition search)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).GetCardLostRestoreRecords(search);
        }
        /// <summary>
        /// 通过查询条件获取相应的卡片禁用启用记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardDisableEnableRecord> GetCardDisableEnableRecords(RecordSearchCondition search)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).GetCardDisableEnableRecords(search);
        }
        /// <summary>
        /// 通过查询条件获取相应的卡片回收记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardRecycleRecord> GetCardRecycleRecords(RecordSearchCondition search)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).GetCardRecycleRecords(search);
        }
        /// <summary>
        /// 通过查询条件获取相应的卡片发行记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardReleaseRecord> GetCardReleaseRecords(RecordSearchCondition search)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).GetCardReleaseRecords(search);
        }
        /// <summary>
        /// 通过查询条件获取相应的卡片删除记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardDeleteRecord> GetCardDeleteRecords(RecordSearchCondition search)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).GetCardDeleteRecords(search);
        }
        #endregion

        #region 卡片管理
        /// <summary>
        /// 通过卡号获取卡片信息
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public QueryResult<CardInfo> GetCardByID(string cardID)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).GetCardByID(cardID);
        }
        
        /// <summary>
        /// 通过查询条件获取卡片
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public QueryResultList<CardInfo> GetCards(CardSearchCondition con)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).GetCards(con);
        }

        /// <summary>
        /// 获取所有权限组为指定权限组的卡片
        /// </summary>
        /// <param name="accessID"></param>
        /// <returns></returns>
        public QueryResultList<CardInfo> GetCardsByAccessID(int accessID)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).GetCardsByAccessID(accessID);
        }

        //保存卡片信息,如果存在则直接覆盖
        public InterfaceReturnCode SaveCard(string cardID, short cardNum, byte carType, byte status, short index,
            int lastEntrance, string activationDate, DateTime validDate, decimal balance,
            decimal deposit, int discountHour, int options)
        {
            DateTime activation;
            if (!DateTime.TryParse(activationDate, out activation))
            {
                return InterfaceReturnCode.ParameterError;
            }
            //卡片状态  = 1 Enabled 已发行, = 3 Disabled 禁用
            byte cardStatus = (byte)(status == 0 ? 3 : 1);

            ICardProvider provider = ProviderFactory.Create<ICardProvider>(AppConifg.Current.ParkingConnection);
            CardInfo info = null;
            QueryResult<CardInfo> find = provider.GetByID(cardID);
            if (find.Result == ResultCode.Successful || find.Result == ResultCode.NoRecord)
            {
                if (find.QueryObject != null)
                    info = find.QueryObject.Clone();
            }
            else
            {
                return CreateInterfaceReturnCode(find.Result);
            }
            if (info == null)
            {
                info = new CardInfo();

                //以下为卡片默认设置
                info.CardType = Ralid.Park.BusinessModel.Enum.CardType.MonthRentCard;//卡片类型定位月卡

                info.CanRepeatOut = false;
                info.CanRepeatIn = false;
                info.HolidayEnabled = true;
                info.WithCount = true;
                info.OnlineHandleWhenOfflineMode = false;
                info.CanEnterWhenFull = true;
                info.EnableWhenExpired = true;
                info.ValidDate = new DateTime(2099, 12, 31);
            }
            info.CardID = cardID;
            info.CardNum = cardNum;
            info.AccessID = 0;//表示所有权限
            info.CarType = carType;
            info.Index = index;
            info.ParkingStatus = ParkingStatus.Out;
            info.LastDateTime = DateTime.Now;
            info.LastEntrance = lastEntrance;

            info.Status = (Ralid.Park.BusinessModel.Enum.CardStatus)cardStatus;
            info.ActivationDate = activation;

            CommandResult result = null;
            if (find.QueryObject == null)
            {
                result = provider.Insert(info);
            }
            else
            {
                result = provider.Update(info, find.QueryObject);
            }
            return CreateInterfaceReturnCode(result.Result);

        }

        public CommandResult SaveCard2(CardInfo info)
        {
            CardInfo original = GetCardByID(info.CardID).QueryObject;
            if (original != null)
            {
                return new CardBll(AppConifg.Current.ParkingConnection).UpdateCard(info);
            }
            else
            {
                return new CardBll(AppConifg.Current.ParkingConnection).AddCard(info);
            }
        }

        /// <summary>
        /// 删除卡片
        /// </summary>
        public CommandResult DeleteCard(string cardID)
        {
            CardBll bll = new CardBll(AppConifg.Current.ParkingConnection);
            CardInfo card = bll.GetCardByID(cardID).QueryObject;
            if (card != null)
                return bll.DeleteCard(card);
            else
                return null;
        }
        /// <summary>
        /// 卡片挂失
        /// </summary>
        /// <param name="info"></param>
        /// <param name="reason"></param>
        /// <param name="lostCardCost"></param>
        /// <param name="paymode"></param>
        public CommandResult CardLoss(CardInfo info, string reason, decimal lostCardCost, PaymentMode paymode)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).CardLoss(info, reason, lostCardCost, paymode);
        }
        /// <summary>
        /// 卡片挂失,除了收取了卡片工本费还收取了卡片在场内的停车费用,并把卡片置为出场
        /// </summary>
        /// <param name="info"></param>
        /// <param name="reason"></param>
        /// <param name="lostCardCost"></param>
        /// <param name="paymode"></param>
        /// <param name="parkFee"></param>
        /// <returns></returns>
        public CommandResult CardLoss(CardInfo info, string reason, decimal lostCardCost, PaymentMode paymode, CardPaymentInfo parkFee)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).CardLoss(info, reason, lostCardCost, paymode, parkFee);
        }
        /// <summary>
        /// 卡片恢复
        /// </summary>
        /// <param name="info"></param>
        /// <param name="reason"></param>
        /// <param name="keepParkingStatus">是否保持数据库中的卡片运行状态</param>
        /// <returns></returns>
        public CommandResult CardRestore(CardInfo info, string reason, bool keepParkingStatus)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).CardRestore(info, reason, keepParkingStatus);
        }
        /// <summary>
        /// 卡片充值
        /// </summary>
        /// <param name="info">卡片信息</param>
        /// <param name="chargeAmount">充值金额</param>
        /// <param name="payment">实收金额</param>
        /// <param name="paymentMode">收费方式</param>
        /// <param name="validDate">有效期</param>
        /// <param name="memo">备注</param>
        /// <param name="keepParkingStatus">是否保持数据库中的卡片运行状态</param>
        /// <returns></returns>
        public CommandResult CardCharge(CardInfo info, Decimal chargeAmount, Decimal payment, PaymentMode paymentMode, DateTime validDate, bool keepParkingStatus, string memo)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).CardCharge(info, chargeAmount, payment, paymentMode, validDate, memo, keepParkingStatus);
        }

        /// <summary>
        /// 卡片延期
        /// </summary>
        /// <param name="info"></param>
        /// <param name="newValidDate"></param>
        /// <param name="paymentMode"></param>
        /// <param name="money"></param>
        /// <param name="memo"></param>
        /// <param name="keepParkingStatus">是否保持卡片运行状态</param>
        /// <returns></returns>
        public CommandResult CardDefer(CardInfo info, DateTimeRange deferDate, PaymentMode paymentMode, decimal money, bool keepParkingStatus, string memo)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).CardDefer(info, deferDate, paymentMode, money, memo, keepParkingStatus);
        }
        /// <summary>
        /// 卡片禁用
        /// </summary>
        /// <param name="info"></param>
        /// <param name="reason"></param>
        /// <param name="keepParkingStatus">是否保持卡片运行状态</param>
        /// <returns></returns>
        public CommandResult CardDisable(CardInfo info, string reason, bool keepParkingStatus)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).CardDisable(info, reason, keepParkingStatus);
        }
        /// <summary>
        /// 卡片解禁
        /// </summary>
        /// <param name="info"></param>
        /// <param name="reason"></param>
        /// <param name="keepParkingStatus">是否保持卡片运行状态</param>
        /// <returns></returns>
        public CommandResult CardEnable(CardInfo info, string reason, bool keepParkingStatus)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).CardEnable(info, reason, keepParkingStatus);
        }
        /// <summary>
        /// 卡片回收
        /// </summary>
        /// <param name="info"></param>
        /// <param name="recycleMoney"></param>
        /// <param name="memo"></param>
        /// <param name="keepParkingStatus">是否保持卡片运行状态</param>
        /// <returns></returns>
        public CommandResult CardRecycle(CardInfo info, decimal recycleMoney, string memo, bool keepParkingStatus)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).CardRecycle(info, recycleMoney, memo, keepParkingStatus);
        }
        /// <summary>
        /// 卡片发行
        /// </summary>
        /// <param name="info"></param>
        /// <param name="releaseMoney"></param>
        /// <param name="paymentMode"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public CommandResult CardRelease(CardInfo info, decimal releaseMoney, PaymentMode paymentMode, string memo)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).CardRelease(info, releaseMoney, paymentMode, memo);
        }
        #endregion

        #region 某张卡片的停车费用
        public decimal GetCardLastPayment(CardInfo info, OperatorInfo operatorInfo)
        {
            decimal paid = 0;
            CardPaymentInfo paymentInfo = (new CardPaymentRecordBll(AppConifg.Current.ParkingConnection)).GetLatestRecord(info.CardID, info.LastDateTime, operatorInfo);
            if (paymentInfo != null)
                paid = paymentInfo.Paid;
            return paid;
        }
        #endregion

        #region 获取设备清单
        public QueryResultList<EntranceInfo> GetAllEntraces()
        {
            return new EntranceBll(AppConifg.Current.ParkingConnection).GetAllEntraces();
        }
        #endregion

        #region 卡片状态
        public CardStatus GetCardStatusByCardID(string cardID)
        {
            return new CardBll(AppConifg.Current.ParkingConnection).GetCardByID(cardID).QueryObject.Status;
        }
        #endregion


        #region 停车场信息接口实现
        /// <summary>
        /// 获取车牌号码所在停车场
        /// </summary>
        /// <param name="carPlate"></param>
        /// <returns></returns>
        public QueryResult<ParkInfo> QueryParkByCarPlate(string carPlate)
        {
            QueryResult<ParkInfo> result = new BusinessModel.Result.QueryResult<BusinessModel.Model.ParkInfo>(ResultCode.Fail, null);
            if (string.IsNullOrEmpty(carPlate))
            {
                result.Message = "参数错误";
                return result;
            }

            result = new ParkBll(AppConifg.Current.ParkingConnection).GetParkInfoCarPlate(carPlate);

            return result;
        }
        /// <summary>
        /// 获取所有停车场信息
        /// </summary>
        /// <returns></returns>
        public QueryResultList<ParkInfo> GetAllPark()
        {
            return new ParkBll(AppConifg.Current.ParkingConnection).GetAllParks();
        }
        #endregion


        #region 停车费用支付接口实现
        /// <summary>
        /// 获取某卡号的停车收费信息接口
        /// </summary>
        /// <param name="cardID">卡号</param>
        /// <param name="discountHour">优惠时长</param>
        /// <param name="discountAmount">优惠金额</param>
        /// <param name="reserve1">预留1</param>
        /// <param name="reserve2">预留2</param>
        /// <returns>Result 0：成功，其他：失败；QueryObject：返回收费信息对象</returns>
        public QueryResult<WSCardPaymentInfo> GetCardPayment(string cardID, string discountHour, string discountAmount, string reserve1, string reserve2)
        {
            try
            {
                #region 先验证输入参数
                if (string.IsNullOrEmpty(cardID.Trim()))
                {
                    return new QueryResult<WSCardPaymentInfo>(ResultCode.ParameterError, "参数卡号错误", null);
                }
                int discountHourInt = 0;
                if (!string.IsNullOrEmpty(discountHour.Trim()))
                {
                    if (!int.TryParse(discountHour, out discountHourInt))
                    {
                        return new QueryResult<WSCardPaymentInfo>(ResultCode.ParameterError, "参数优惠时长错误", null);
                    }
                }
                decimal discountAmountD = 0;
                if (!string.IsNullOrEmpty(discountAmount.Trim()))
                {
                    if (!decimal.TryParse(discountAmount, out discountAmountD))
                    {
                        return new QueryResult<WSCardPaymentInfo>(ResultCode.ParameterError, "参数优惠金额错误", null);
                    }
                }
                #endregion

                #region 接口日志记录
                if (AppConifg.Current.Log)
                {
                    try
                    {
                        string log = string.Format("卡号：{0} 优惠时长：{1} 优惠金额：{2} 预留1：{3} 预留2：{4}"
                            , cardID
                            , discountHour
                            , discountAmount
                            , reserve1
                            , reserve2);
                        Ralid.GeneralLibrary.LOG.FileLog.Log(AppConifg.Current.LogPath, "GetCardPayment", log);
                    }
                    catch (Exception ex)
                    {
                        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                    }
                }
                #endregion

                #region 验证卡片信息
                //查找该卡号的卡片
                CardBll cardBll = new CardBll(AppConifg.Current.ParkingConnection);
                QueryResult<CardInfo> cResult = cardBll.GetCardByID(cardID);
                if (cResult.Result != ResultCode.Successful)
                {
                    return new QueryResult<WSCardPaymentInfo>(cResult.Result, "获取卡片信息失败", null);
                }
                CardInfo card = cResult.QueryObject;
                if (card == null)
                {
                    return new QueryResult<WSCardPaymentInfo>(ResultCode.NoRecord, "此卡未登记", null);
                }
                if (!card.IsInPark)
                {
                    return new QueryResult<WSCardPaymentInfo>(ResultCode.Fail, "此卡已出场", null);
                }
                string msg = string.Empty;
                if (!ValidateCard(card, out msg))
                {
                    return new QueryResult<WSCardPaymentInfo>(ResultCode.Fail, msg, null);
                }
                DateTime chargeDateTime = DateTime.Now;
                if (card.LastDateTime > chargeDateTime)
                {
                    return new QueryResult<WSCardPaymentInfo>(ResultCode.Fail, "入场时间晚于计费时间", null);
                }

                //获取卡片所在停车场信息
                EntranceBll entranceBll = new EntranceBll(AppConifg.Current.ParkingConnection);
                EntranceInfo entrance = entranceBll.GetEntranceInfo(card.LastEntrance).QueryObject;
                if (entrance == null)
                {
                    return new QueryResult<WSCardPaymentInfo>(ResultCode.NoRecord, "没有找到入场通道信息", null);
                }
                ParkBll parkBll = new ParkBll(AppConifg.Current.ParkingConnection);
                ParkInfo park = parkBll.GetParkInfoByID(entrance.ParkID).QueryObject;
                if (park == null)
                {
                    return new QueryResult<WSCardPaymentInfo>(ResultCode.NoRecord, "没有找到停车场信息", null);
                }
                //判断卡片合法性
                if (card.IsCardList && park.IsWriteCardMode && !card.OnlineHandleWhenOfflineMode)
                {
                    //写卡模式时，脱机处理的卡片名单不能缴费
                    return new QueryResult<WSCardPaymentInfo>(ResultCode.Fail, "该卡片为写卡处理卡片，不能进行在线缴费", null);
                }
                #endregion

                #region 获取费率和节假日
                SysParaSettingsBll ssb = new SysParaSettingsBll(AppConifg.Current.ParkingConnection);
                TariffSetting tariff = ssb.GetSetting<TariffSetting>();
                if (tariff == null)
                {
                    return new QueryResult<WSCardPaymentInfo>(ResultCode.Fail, "获取费率失败", null);
                }
                TariffSetting.Current = tariff;
                HolidaySetting holiday = ssb.GetSetting<HolidaySetting>();
                if (holiday == null)
                {
                    return new QueryResult<WSCardPaymentInfo>(ResultCode.Fail, "获取节假日失败", null);
                }
                HolidaySetting.Current = holiday;
                #endregion

                #region 判断是否已缴过费
                if (card.IsCompletedPaid && tariff.IsInFreeTime(card.PaidDateTime.Value, chargeDateTime))
                {
                    //已缴费，并且未过免费时间,不允许缴费
                    msg = string.Format("已缴费，请在{0}分钟内离场！", tariff.FreeTimeRemaining(card.PaidDateTime.Value, chargeDateTime));
                    return new QueryResult<WSCardPaymentInfo>(ResultCode.Fail, msg, null);
                }
                #endregion

                //重设卡片优惠时长
                card.DiscountHour += discountHourInt;
                if (card.DiscountHour > 0xFF)
                {
                    card.DiscountHour = 0xFF;
                }

                //生成卡片缴费记录
                CardPaymentInfo chargeRecord = CardPaymentInfoFactory.CreateCardPaymentRecord(park.ParkID, card, tariff, card.CarType, chargeDateTime);
                //计算优惠后的缴费费用
                chargeRecord.Discount += discountAmountD;
                if (chargeRecord.Discount > chargeRecord.Accounts)
                {
                    //如果优惠金额比应收费用多，优惠金额为应收费用，这是为了防止实际支付费用为负数的情况出现
                    chargeRecord.Discount = chargeRecord.Accounts;
                }

                WSCardPaymentInfo wsRecord = new WSCardPaymentInfo();
                wsRecord.SetWSCardPaymentInfo(chargeRecord);
                wsRecord.EntranceName = entrance.EntranceName;

                return new QueryResult<WSCardPaymentInfo>(ResultCode.Successful, wsRecord);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                string msg = string.Format("接口发生异常，异常信息：{0}", ex.Message);
                return new QueryResult<WSCardPaymentInfo>(ResultCode.InterfaceException, msg, null);
            }
        }

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
        public CommandResult CardFeePay(string cardID, string chargeDateTime, string paid, string payMode, string memo, string reserve1, string reserve2)
        {
            try
            {
                #region 先验证输入参数
                if (string.IsNullOrEmpty(cardID.Trim()))
                {
                    return new CommandResult(ResultCode.ParameterError, "参数卡号错误");
                }
                DateTime chargeTime = new DateTime(2011, 1, 1);
                if (!DateTime.TryParse(chargeDateTime, out chargeTime))
                {
                    return new CommandResult(ResultCode.ParameterError, "参数计费时间错误");
                }
                if (chargeTime > DateTime.Now)
                {
                    return new CommandResult(ResultCode.ParameterError, "计费时间大于系统当前时间");
                }
                decimal paidD = 0;
                if (!decimal.TryParse(paid, out paidD))
                {
                    return new CommandResult(ResultCode.ParameterError, "参数实付金额错误");
                }
                int payModeI = 0;
                if (!int.TryParse(payMode, out payModeI))
                {
                    return new CommandResult(ResultCode.ParameterError, "参数支付方式错误");
                }
                #endregion

                #region 接口日志记录
                if (AppConifg.Current.Log)
                {
                    try
                    {
                        string log = string.Format("卡号：{0} 计费时间：{1} 实付金额：{2} 支付方式：{3} 费用说明：{4} 预留1：{5} 预留2：{6}"
                            , cardID
                            , chargeDateTime
                            , paid
                            , payMode
                            , memo
                            , reserve1
                            , reserve2);
                        Ralid.GeneralLibrary.LOG.FileLog.Log(AppConifg.Current.LogPath, "CardFeePay", log);
                    }
                    catch (Exception ex)
                    {
                        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                    }
                }
                #endregion

                #region 验证卡片信息
                //查找该卡号的卡片
                CardBll cardBll = new CardBll(AppConifg.Current.ParkingConnection);
                QueryResult<CardInfo> cResult = cardBll.GetCardByID(cardID);
                if (cResult.Result != ResultCode.Successful)
                {
                    return new CommandResult(cResult.Result, "获取卡片信息失败");
                }
                CardInfo card = cResult.QueryObject;
                if (card == null)
                {
                    return new CommandResult(ResultCode.NoRecord, "此卡未登记");
                }
                if (!card.IsInPark)
                {
                    return new CommandResult(ResultCode.Fail, "此卡已出场");
                }
                string msg = string.Empty;
                if (!ValidateCard(card, out msg))
                {
                    return new CommandResult(ResultCode.Fail, msg);
                }
                if (card.LastDateTime > chargeTime)
                {
                    return new CommandResult(ResultCode.ParameterError, "入场时间晚于计费时间");
                }
                //获取卡片所在停车场信息
                EntranceBll entranceBll = new EntranceBll(AppConifg.Current.ParkingConnection);
                EntranceInfo entrance = entranceBll.GetEntranceInfo(card.LastEntrance).QueryObject;
                if (entrance == null)
                {
                    return new CommandResult(ResultCode.NoRecord, "没有找到入场通道信息");
                }
                ParkBll parkBll = new ParkBll(AppConifg.Current.ParkingConnection);
                ParkInfo park = parkBll.GetParkInfoByID(entrance.ParkID).QueryObject;
                if (park == null)
                {
                    return new CommandResult(ResultCode.NoRecord, "没有找到停车场信息");
                }
                //判断卡片合法性
                if (card.IsCardList && park.IsWriteCardMode && !card.OnlineHandleWhenOfflineMode)
                {
                    //写卡模式时，脱机处理的卡片名单不能缴费
                    return new CommandResult(ResultCode.Fail, "该卡片为写卡处理卡片，不能进行在线收费");
                }               
                #endregion

                #region 获取费率和节假日
                SysParaSettingsBll ssb = new SysParaSettingsBll(AppConifg.Current.ParkingConnection);
                TariffSetting tariff = ssb.GetSetting<TariffSetting>();
                if (tariff == null)
                {
                    return new CommandResult(ResultCode.Fail, "获取费率失败");
                }
                TariffSetting.Current = tariff;
                HolidaySetting holiday = ssb.GetSetting<HolidaySetting>();
                if (holiday == null)
                {
                    return new CommandResult(ResultCode.Fail, "获取节假日失败");
                }
                HolidaySetting.Current = holiday;
                #endregion

                #region 判断是否已缴过费
                if (card.IsCompletedPaid && tariff.IsInFreeTime(card.PaidDateTime.Value, chargeTime))
                {
                    //已缴费，并且未过免费时间,不允许缴费
                    msg = string.Format("已缴费，请在{0}分钟内离场！", tariff.FreeTimeRemaining(card.PaidDateTime.Value, chargeTime));
                    return new CommandResult(ResultCode.Fail, msg);
                }
                #endregion

                //生成卡片缴费记录
                CardPaymentInfo chargeRecord = CardPaymentInfoFactory.CreateCardPaymentRecord(park.ParkID, card, tariff, card.CarType, chargeTime);
                //将收费信息重新赋值
                chargeRecord.Paid = paidD;
                chargeRecord.Discount = chargeRecord.Accounts - paidD;
                if (chargeRecord.Discount < 0)
                {
                    chargeRecord.Discount = 0;
                }
                chargeRecord.PaymentCode = PaymentCode.Internet;
                chargeRecord.PaymentMode = payModeI == 1 ? PaymentMode.WeChat : PaymentMode.Cash;
                chargeRecord.IsCenterCharge = true;
                chargeRecord.StationID = string.Empty;
                chargeRecord.OperatorID = string.Empty;
                if (!string.IsNullOrEmpty(memo))
                {
                    chargeRecord.Memo = memo;
                }
                if (chargeRecord.Memo == null)
                {
                    chargeRecord.Memo = string.Empty;
                }

                CommandResult result = cardBll.PayParkFee(chargeRecord);
                if (result.Result == ResultCode.Successful)
                {
                    //缴费成功，返回缴费后离场提示信息
                    msg = string.Format("缴费成功，请在{0}分钟内离场！", tariff.FreeTimeRemaining(chargeTime, DateTime.Now));
                }

                return result;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                string msg = string.Format("接口发生异常，异常信息：{0}", ex.Message);
                return new CommandResult(ResultCode.InterfaceException, msg);
            }
        }
        #endregion
    }
}
