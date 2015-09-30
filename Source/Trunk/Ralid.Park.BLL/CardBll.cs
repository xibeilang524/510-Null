using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition ;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.BLL
{
    public class CardBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public CardBll(string repoUri)
        {
            this._RepoUri = repoUri;
            this._Provider = ProviderFactory.Create<ICardProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private string _RepoUri;
        private ICardProvider _Provider;
        #endregion

        #region 私有方法
        /// <summary>
        /// 修改数据库中的卡片,不会修改卡片的运行状态
        /// </summary>
        private bool UpdateCard(CardInfo info, IUnitWork unitWork)
        {
            QueryResult<CardInfo> result = _Provider.GetByID(info.CardID);
            if (result.Result == ResultCode.Successful)
            {
                CardInfo original = result.QueryObject;
                //卡片状态保持用数据库中的状态
                info.ParkingStatus = original.ParkingStatus;
                info.LastDateTime = original.LastDateTime;
                info.LastEntrance = original.LastEntrance;
                info.LastCarPlate = original.LastCarPlate;
                _Provider.Update(info, original, unitWork);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改数据库中的卡片,会修改卡片的运行状态
        /// </summary>
        private bool UpdateCardAll(CardInfo info, IUnitWork unitWork)
        {
            QueryResult<CardInfo> result = _Provider.GetByID(info.CardID);
            if (result.Result == ResultCode.Successful)
            {
                CardInfo original = result.QueryObject;
                _Provider.Update(info, original, unitWork);
                return true;
            }
            return false;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 通过卡号获取卡片信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QueryResult<CardInfo> GetCardByID(string id)
        {
            return _Provider.GetByID(id);
        }

        public QueryResult<CardInfo> GetCardByCardCertificate(string certificate)
        {
            return null;
        }

        /// <summary>
        /// 通过卡号获取卡片的信息（信息中包括最近一条收费记录)
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public CardInfo GetCardDetail(string cardID)
        {
            CardInfo card = _Provider.GetByID(cardID).QueryObject;
            if (card != null)  //已经收费)
            {
                card.LastPayment = (new CardPaymentRecordBll(_RepoUri)).GetLatestRecord(card.CardID, card.LastDateTime, null);
            }
            return card;
        }
        
        /// <summary>
        /// 通过卡号获取卡片的信息，当主数据库连接断开后，会从备用数据库中获取
        /// </summary>
        /// <param name="cardID"></param>
        /// <returns></returns>
        public CardInfo GetCardDetail(string cardID, string standby)
        {
            QueryResult<CardInfo> result = _Provider.GetByID(cardID);
            if (string.IsNullOrEmpty(_RepoUri) || result.Result == ResultCode.CannotConnectServer)
            {
                ICardProvider standbyProvider = ProviderFactory.Create<ICardProvider>(standby);
                result = standbyProvider.GetByID(cardID);
            }
            CardInfo card = result.QueryObject;
            return card;
        }
        
        /// <summary>
        /// 通过查询条件获取卡片
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public QueryResultList<CardInfo> GetCards(CardSearchCondition con)
        {
            return _Provider.GetItems(con);
        }
        /// <summary>
        /// 获取所有卡片(包括车牌名单)
        /// </summary>
        /// <returns></returns>
        public QueryResultList<CardInfo> GetAllCards()
        {
            CardSearchCondition con = new CardSearchCondition();
            //con.Status = CardStatus.Enabled | CardStatus.Disabled | CardStatus.Loss | CardStatus.Recycled;
            return _Provider.GetItems(con);
        }
        /// <summary>
        /// 获取所有权限组为指定权限组的卡片
        /// </summary>
        /// <param name="accessID"></param>
        /// <returns></returns>
        public QueryResultList<CardInfo> GetCardsByAccessID(int accessID)
        {
            CardSearchCondition con = new CardSearchCondition() { AccessID = accessID };
            return _Provider.GetItems(con);
        }
        /// <summary>
        /// 在数据库中增加一张卡片
        /// </summary>
        public CommandResult AddCard(CardInfo info)
        {
            CommandResult result = _Provider.Insert(info);
            return result;
        }

        /// <summary>
        /// 修改卡片信息，此方法只会修改卡片收费相关的信息，如入场状态，入场时间，余额等
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult UpdateCardPaymentInfo(CardInfo info)
        {
            CardInfo original = _Provider.GetByID(info.CardID).QueryObject;
            if (original != null)
            {
                CardInfo card = original.Clone();
                card.ParkingStatus = info.ParkingStatus;
                card.LastDateTime = info.LastDateTime;
                card.PaidDateTime = info.PaidDateTime;
                card.ParkFee = info.ParkFee;
                card.TotalPaidFee = info.TotalPaidFee;
                card.LastCarPlate = info.LastCarPlate;
                card.UpdateFlag = info.UpdateFlag;
                card.FreeDateTime = info.FreeDateTime;

                if (original.IsTempCard)
                {
                    //临时卡保存车牌号码
                    card.CarPlate = info.CarPlate;
                }
                else if (original.CardType.IsPrepayCard)
                {
                    //储值卡需要保存余额
                    card.Balance = info.Balance;
                }
                return _Provider.Update(info, original);
            }
            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }

        /// <summary>
        /// 修改卡片信息，此方法不会修改卡片的运行状态(如入场时间，卡片出入场状态等)也不能修改卡片的有效期，余额等
        /// </summary>
        /// <returns></returns>
        public CommandResult UpdateCard(CardInfo info)
        {
            CardInfo original = _Provider.GetByID(info.CardID).QueryObject;
            if(original !=null)
            {
                //运行状态不能修改
                info.ParkingStatus = original.ParkingStatus;
                info.LastDateTime = original.LastDateTime;
                info.LastEntrance = original.LastEntrance;
                info.LastCarPlate = original.LastCarPlate;
                info.LastNestParkDateTime = original.LastNestParkDateTime;
                //这些参数也不能修改
                info.ValidDate = original.ValidDate;
                info.Deposit = original.Deposit;
                info.Balance = original.Balance;
                return _Provider.Update(info, original);
            }
            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }
        /// <summary>
        /// 修改卡片信息
        /// </summary>
        /// <returns></returns>
        public CommandResult UpdateCardAll(CardInfo info)
        {
            CardInfo original = _Provider.GetByID(info.CardID).QueryObject;
            if (original != null)
            {
                return _Provider.Update(info, original);
            }
            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }

        /// <summary>
        /// 卡片缴费机收费退款
        /// </summary>
        /// <param name="info"></param>
        /// <param name="refund">退还金额</param>
        /// <returns></returns>
        public CommandResult APMCardRefund(CardInfo info,APMRefundRecord record)
        {
            CardInfo original = _Provider.GetByID(info.CardID).QueryObject;

            if (original != null)
            {
                CardInfo newinfo = original.Clone();
                //只会更新以下信息
                newinfo.TotalPaidFee = 0;

                IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
                _Provider.Update(newinfo, original, unitWork);
                
                IAPMRefundRecordProvider recordProvider = ProviderFactory.Create<IAPMRefundRecordProvider>(_RepoUri);
                recordProvider.Insert(record, unitWork);

                CommandResult result = unitWork.Commit();
                if (result.Result == ResultCode.Successful)
                {
                    info.TotalPaidFee = newinfo.TotalPaidFee;
                }

                return result;
            }
            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }


        /// <summary>
        /// 更新卡片免费优惠授权信息
        /// </summary>
        /// <param name="info">新卡片信息</param>
        /// <returns></returns>
        public CommandResult CardFreeAuthorization(CardInfo info)
        {
            CardInfo original = _Provider.GetByID(info.CardID).QueryObject;

            if (original != null)
            {
                CardInfo newinfo = original.Clone();
                //只会更新以下信息
                newinfo.EnableHotelApp = info.EnableHotelApp;
                newinfo.HotelCheckOut = info.HotelCheckOut;
                newinfo.FreeDateTime = info.FreeDateTime;

                CommandResult result = _Provider.Update(newinfo, original);
                return result;
            }
            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }

        /// <summary>
        /// 更新卡片免费优惠授权信息
        /// </summary>
        /// <param name="info">新卡片信息</param>
        /// <param name="both">是否需同时授权到备用数据库</param>
        /// <param name="strandby">备用数据库连接</param>
        /// <returns></returns>
        public CommandResult CardFreeAuthorizationWithStandby(CardInfo info, bool both, string standby)
        {
            //同时更新时需有备用数据库连接
            if (both && string.IsNullOrEmpty(standby))
                return new CommandResult(ResultCode.CannotConnectServer);

            CardInfo original = _Provider.GetByID(info.CardID).QueryObject;
            ICardProvider standbyProvider = ProviderFactory.Create<ICardProvider>(standby);
            CardInfo standbyoriginal = standbyProvider.GetByID(info.CardID).QueryObject;

            if (original != null && (!both || standbyoriginal != null))
            {
                CardInfo newinfo = original.Clone();
                //只会更新以下信息
                newinfo.EnableHotelApp = info.EnableHotelApp;
                newinfo.HotelCheckOut = info.HotelCheckOut;
                newinfo.FreeDateTime = info.FreeDateTime;

                CommandResult result = _Provider.Update(newinfo, original);
                if (both && result.Result == ResultCode.Successful)
                {
                    CardInfo newstandby = standbyoriginal.Clone();
                    //只会更新以下信息
                    newstandby.EnableHotelApp = info.EnableHotelApp;
                    newstandby.HotelCheckOut = info.HotelCheckOut;
                    newstandby.FreeDateTime = info.FreeDateTime;
                    //需要同时更新到备用数据库
                    result = standbyProvider.Update(newstandby, standbyoriginal);
                    if (result.Result != ResultCode.Successful)
                    {
                        //备用数据库更新不成功，将主数据库信息还原
                        _Provider.Update(original, newinfo);
                    }
                }
                return result;
            }
            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }

        /// <summary>
        /// 保存卡片与卡片事件信息（用于停车场产生事件时更新卡片状态并保存事件)
        /// </summary>
        /// <param name="card"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        public CommandResult SaveCardAndEvent(CardInfo card, CardEventReport report)
        {
            CommandResult ret = null;
            CardInfo original = GetCardByID(card.CardID).QueryObject;
            if (original != null)
            {
                IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
                if (card.CardType == CardType.Ticket && report.IsExitEvent)  ////纸票出场后将其删除
                {
                    _Provider.Delete(card, unitWork);
                }
                else
                {
                    CardInfo info = card.Clone();
                    //卡片状态保持用数据库中的状态
                    info.ParkingStatus = report.ParkingStatus;
                    info.LastDateTime = report.EventDateTime;
                    info.LastEntrance = report.EntranceID;
                    info.LastCarPlate = report.CarPlate;
                    info.UpdateFlag = report.UpdateFlag;
                    //如果启用了酒店应用，保留免费时间点，否则清空免费时间点
                    info.FreeDateTime = report.EnableHotelApp ? report.FreeDateTime : null;
                    info.ClearDiscount();//清除优惠录入信息
                    if (info.CardType.IsPrepayCard)
                    {
                        info.Balance = report.Balance;//数据库使用事件的余额
                    }

                    //入口刷卡事件时，将缴费时间，停车费用，累计停车费用清空
                    if (!report.IsExitEvent)
                    {
                        info.ClearPaidData();
                    }
                    _Provider.Update(info, original, unitWork);
                }
                ICardEventProvider icp = ProviderFactory.Create<ICardEventProvider>(_RepoUri);
                icp.Insert((new CardEventRecord(report)), unitWork);
                ret = unitWork.Commit();
                if (ret.Result == ResultCode.Successful) //如果成功，则改变卡片状态
                {
                    //卡片状态保持用数据库中的状态
                    card.ParkingStatus = report.ParkingStatus;
                    card.LastDateTime = report.EventDateTime;
                    card.LastEntrance = report.EntranceID;
                    card.LastCarPlate = report.CarPlate;
                    card.UpdateFlag = report.UpdateFlag;
                    //如果启用了酒店应用，保留免费时间点，否则清空免费时间点
                    card.FreeDateTime = report.EnableHotelApp ? report.FreeDateTime : null;
                    card.ClearDiscount();//清除优惠录入信息
                    if (card.CardType.IsPrepayCard)
                    {
                        card.Balance = report.Balance;//数据库使用事件的余额
                    }
                    //入口刷卡事件时，将缴费时间，停车费用，累计停车费用清空
                    if (!report.IsExitEvent)
                    {
                        card.ClearPaidData();
                    }
                }
            }
            else
            {
                ret = new CommandResult(ResultCode.Fail, ResultCodeDecription.GetDescription(ResultCode.Fail));
            }
            return ret;
        }        

        /// <summary>
        /// 保存卡片以及嵌套车场进出事件
        /// </summary>
        /// <param name="card"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        public CommandResult SaveCardAndNestedEvent(CardInfo card, CardEventReport report)
        {
            //内嵌车场事件只改变卡片的停车状态

            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
            CardInfo info = card.Clone();
            //卡片状态保持用数据库中的状态
            info.ParkingStatus = report.ParkingStatus;
            if (!report.IsExitEvent) info.LastNestParkDateTime = report.EventDateTime;  //如果是入场事件，则更新卡片的最后进入内车场时间
            _Provider.Update(info, card, unitWork);

            ICardEventProvider icp = ProviderFactory.Create<ICardEventProvider>(_RepoUri);
            if (report.IsExitEvent) //如果是入内车场，则不记录最后刷卡时间，如果是出内车场，则保存上次时间
            {
                report.LastDateTime = card.LastNestParkDateTime; 
            }
            else
            {
                report.LastDateTime = null;
            }
            icp.Insert((new CardEventRecord(report)), unitWork);
            CommandResult ret = unitWork.Commit();
            if (ret.Result == ResultCode.Successful) //如果成功，则改变卡片状态
            {
                //卡片状态保持用数据库中的状态
                card.ParkingStatus = report.ParkingStatus;
            }
            return ret;
        }
        /// <summary>
        /// 删除卡片
        /// </summary>
        public CommandResult DeleteCard(CardInfo info)
        {
            if (info.Status == CardStatus.Enabled && !info.IsTempCard && !info.IsManagedCard)
            {
                throw new InvalidOperationException(Resource1.CardBll_CannotDelete);
            }
            else
            {
                string op = OperatorInfo.CurrentOperator.OperatorName;
                string station = WorkStationInfo.CurrentStation.StationName;
                IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
                CardDeleteRecord record = new CardDeleteRecord
                {
                    CardID = info.CardID,
                    OwnerName = info.OwnerName,
                    CardCertificate = info.CardCertificate,
                    CarPlate = info.CarPlate,
                    DeleteDateTime = DateTime.Now,
                    CardType = info.CardType,
                    Balance = info.Balance,
                    ValidDate = info.ValidDate,
                    Deposit = info.Deposit,
                    OperatorID = op,
                    StationID = station
                    
                };
                ICardDeleteRecordProvider icdp = ProviderFactory.Create<ICardDeleteRecordProvider>(_RepoUri);
                icdp.Insert(record, unitWork);
                _Provider.Delete(info, unitWork);
                return unitWork.Commit();
            }
        }

        /// <summary>
        /// 强制删除卡片
        /// </summary>
        public CommandResult DeleteCardAtAll(CardInfo info)
        {
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
            CardDeleteRecord record = new CardDeleteRecord
            {
                CardID = info.CardID,
                OwnerName = info.OwnerName,
                CardCertificate = info.CardCertificate,
                CarPlate = info.CarPlate,
                DeleteDateTime = DateTime.Now,
                CardType = info.CardType,
                Balance = info.Balance,
                ValidDate = info.ValidDate,
                Deposit = info.Deposit,
                OperatorID = op,
                StationID = station,
                Memo =Resource1.Mandate_Delete

            };
            ICardDeleteRecordProvider icdp = ProviderFactory.Create<ICardDeleteRecordProvider>(_RepoUri);
            icdp.Insert(record, unitWork);
            _Provider.Delete(info, unitWork);
            return unitWork.Commit();

        }

        /// <summary>
        /// 无卡挂失
        /// </summary>
        /// <param name="info"></param>
        /// <param name="reason"></param>
        /// <param name="lostCardCost"></param>
        /// <param name="paymode"></param>
        public CommandResult NoCardLoss(string carPlate, string ownerName, string reason, decimal lostCardCost, PaymentMode paymode)
        {
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            CardLostRestoreRecord record = new CardLostRestoreRecord
            {
                CardID = "00000000",//卡号固定8个0
                OwnerName = ownerName,
                CardCertificate = string.Empty,
                CarPlate = carPlate,
                CardStatus = 1, //卡状态固定为1
                LostDateTime = DateTime.Now,
                LostOperator = op,
                LostStation = station,
                LostMemo = reason,
                LostCardCost = lostCardCost,
                PaymentMode = paymode
            };
            ICardLostRestoreRecordProvider lostProvider = ProviderFactory.Create<ICardLostRestoreRecordProvider>(_RepoUri);
            return lostProvider.Insert(record);
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
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            info.IsInPark = false;//挂失后标识为已出场
            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
            CardLostRestoreRecord record = new CardLostRestoreRecord
                {
                    CardID = info.CardID,
                    OwnerName = info.OwnerName,
                    CardCertificate = info.CardCertificate,
                    CarPlate = info.CarPlate,
                    CardStatus = (byte)info.Status, //保存卡片的原有卡状态
                    LostDateTime = DateTime.Now,
                    LostOperator = op,
                    LostStation = station,
                    LostMemo = reason,
                    LostCardCost = lostCardCost,
                    PaymentMode = paymode
                };
            ICardLostRestoreRecordProvider lostProvider = ProviderFactory.Create<ICardLostRestoreRecordProvider>(_RepoUri);
            info.Lost();
            lostProvider.Insert(record, unitWork);
            UpdateCardAll(info, unitWork);
            return unitWork.Commit();
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
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
            CardLostRestoreRecord record = new CardLostRestoreRecord
            {
                CardID = info.CardID,
                OwnerName = info.OwnerName,
                CardCertificate = info.CardCertificate,
                CarPlate = info.CarPlate,
                CardStatus = (byte)info.Status, //保存卡片的原有卡状态
                LostDateTime = DateTime.Now,
                LostOperator = op,
                LostStation = station,
                LostMemo = reason,
                LostCardCost = lostCardCost,
                PaymentMode = paymode
            };
            ICardLostRestoreRecordProvider lostProvider = ProviderFactory.Create<ICardLostRestoreRecordProvider>(_RepoUri);
            lostProvider.Insert(record, unitWork);

            if (parkFee != null)
            {
                parkFee.OperatorID = op;
                parkFee.StationID = station;
                parkFee.PaymentMode = paymode;
                parkFee.Memo = "卡片挂失";
                ICardPaymentRecordProvider icpp = ProviderFactory.Create<ICardPaymentRecordProvider>(_RepoUri);
                icpp.Insert(parkFee, unitWork);
            }
            //卡片置为挂失并出场
            CardInfo original = info.Clone();
            info.Status = CardStatus.Loss;
            info.ParkingStatus = ParkingStatus.Out;
            ICardProvider icp = ProviderFactory.Create<ICardProvider>(_RepoUri);
            icp.Update(info, original, unitWork);

            return unitWork.Commit();
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
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
            CardLostRestoreRecord record = new CardLostRestoreRecord
            {
                CardID = info.CardID,
                RestoreDateTime = DateTime.Now,
                RestoreMemo = reason,
                RestoreOperator = op,
                RestoreStation = station,
            };
            ICardLostRestoreRecordProvider restorProvider = ProviderFactory.Create<ICardLostRestoreRecordProvider>(_RepoUri );
            restorProvider.Insert(record, unitWork);
            info.Restore();
            if (keepParkingStatus)
            {
                UpdateCard(info, unitWork);
            }
            else
            {
                UpdateCardAll(info, unitWork);
            }
            return unitWork.Commit();
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
        public CommandResult CardCharge(CardInfo info, Decimal chargeAmount, Decimal payment, PaymentMode paymentMode, DateTime validDate, string memo, bool keepParkingStatus)
        {
            return CardCharge(info, DateTime.Now, chargeAmount, payment, paymentMode, validDate, memo, keepParkingStatus);
        }

        /// <summary>
        /// 卡片充值
        /// </summary>
        /// <param name="info">卡片信息</param>
        /// <param name="chargeDateTime">充值时间</param>
        /// <param name="chargeAmount">充值金额</param>
        /// <param name="payment">实收金额</param>
        /// <param name="paymentMode">收费方式</param>
        /// <param name="validDate">有效期</param>
        /// <param name="memo">备注</param>
        /// <param name="keepParkingStatus">是否保持数据库中的卡片运行状态</param>
        /// <returns></returns>
        public CommandResult CardCharge(CardInfo info, DateTime chargeDateTime, Decimal chargeAmount, Decimal payment, PaymentMode paymentMode, DateTime validDate, string memo, bool keepParkingStatus)
        {
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);

            info.Charge(chargeAmount);
            info.ValidDate = validDate;
            if (keepParkingStatus)
            {
                UpdateCard(info, unitWork);
            }
            else
            {
                UpdateCardAll(info, unitWork);
            }
            CardChargeRecord record = new CardChargeRecord
                {
                    CardID = info.CardID,
                    ChargeDateTime = chargeDateTime,
                    OwnerName = info.OwnerName,
                    CardCertificate = info.CardCertificate,
                    CarPlate = info.CarPlate,
                    Balance = info.Balance,
                    ValidDate = info.ValidDate,
                    ChargeAmount = chargeAmount,
                    Payment = payment,
                    PaymentMode = paymentMode,
                    OperatorID = op,
                    StationID = station,
                    Memo = memo,
                };
            ICardChargeRecordProvider recordProvider = ProviderFactory.Create<ICardChargeRecordProvider>(_RepoUri);
            recordProvider.Insert(record, unitWork);
            return unitWork.Commit();
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
        public CommandResult CardDefer(CardInfo info, DateTimeRange deferDate, PaymentMode paymentMode, decimal money, string memo, bool keepParkingStatus)
        {
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
            CardDeferRecord record = new CardDeferRecord
            {
                CardID = info.CardID,
                OwnerName = info.OwnerName,
                CardType = info.CardType,
                CardCertificate = info.CardCertificate,
                CarPlate = info.CarPlate,
                OriginalDate = deferDate.Begin,
                CurrentDate = deferDate.End,
                DeferDateTime = DateTime.Now,
                PaymentMode = paymentMode,
                DeferMoney = money,
                OperatorID = op,
                StationID = station,
                Memo = memo
            };
            ICardDeferRecordProvider recordProvider = ProviderFactory.Create<ICardDeferRecordProvider>(_RepoUri);
            recordProvider.Insert(record, unitWork);
            info.ValidDate = deferDate.End;
            if (keepParkingStatus)
            {
                UpdateCard(info, unitWork);
            }
            else
            {
                //写卡模式以卡片运行状态为准
                UpdateCardAll(info, unitWork);
            }
            return unitWork.Commit();
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
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
            CardDisableEnableRecord record = new CardDisableEnableRecord
                {
                    CardID = info.CardID,
                    OwnerName = info.OwnerName,
                    CardCertificate = info.CardCertificate,
                    CarPlate = info.CarPlate,
                    DisableDateTime = DateTime.Now,
                    DisableOperator = op,
                    DisableStationID = station,
                    DisableMemo = reason
                };
            ICardDisableEnableRecordProvider recordProvider = ProviderFactory.Create<ICardDisableEnableRecordProvider>(_RepoUri);
            recordProvider.Insert(record, unitWork);
            info.Disable();
            if (keepParkingStatus)
            {
                UpdateCard(info, unitWork);
            }
            else
            {
                UpdateCardAll(info, unitWork);
            }
            return unitWork.Commit();
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
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
            CardDisableEnableRecord record = new CardDisableEnableRecord
           {
               CardID = info.CardID,
               EnableDateTime = DateTime.Now,
               EnableOperator = op,
               EnableStationID = station,
               EnableMemo = reason,
           };
            ICardDisableEnableRecordProvider recordProvider = ProviderFactory.Create<ICardDisableEnableRecordProvider>(_RepoUri);
            recordProvider.Insert(record, unitWork);
            info.Enable();
            if (keepParkingStatus)
            {
                UpdateCard(info, unitWork);
            }
            else
            {
                UpdateCardAll(info, unitWork);
            }
            return unitWork.Commit();
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
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
            CardRecycleRecord record = new CardRecycleRecord
            {
                CardID = info.CardID,
                OwnerName = info.OwnerName,
                CardCertificate = info.CardCertificate,
                CarPlate = info.CarPlate,
                RecycleDateTime = DateTime.Now,
                CardType = info.CardType,
                Balance = info.Balance,
                ValidDate = info.ValidDate,
                Deposit = info.Deposit,
                RecycleMoney = recycleMoney,
                OperatorID = op,
                StationID = station,
                Memo = memo
            };
            ICardRecycleRecordProvider recordProvider = ProviderFactory.Create<ICardRecycleRecordProvider>(_RepoUri);
            recordProvider.Insert(record, unitWork);
            info.Recycle();
            if (keepParkingStatus)
            {
                UpdateCard(info, unitWork);
            }
            else
            {
                UpdateCardAll(info, unitWork);
            }
            return unitWork.Commit();
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
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri); //工作单元
            CardReleaseRecord record = new CardReleaseRecord
                {
                    CardID = info.CardID,
                    OwnerName = info.OwnerName,
                    CardCertificate = info.CardCertificate,
                    CarPlate = info.CarPlate,
                    ReleaseDateTime = DateTime.Now,
                    CardType = info.CardType,
                    ReleaseMoney = releaseMoney,
                    PaymentMode = paymentMode,
                    Balance = info.Balance,
                    ActivationDate = info.ActivationDate,
                    ValidDate = info.ValidDate,
                    HolidayEnabled = info.HolidayEnabled,
                    Deposit = info.Deposit,
                    OperatorID = op,
                    StationID = station,
                    Memo = memo
                };
            ProviderFactory.Create<ICardReleaseRecordProvider>(_RepoUri).Insert(record, unitWork);
            info.Release();
            ICardProvider icp = ProviderFactory.Create<ICardProvider>(_RepoUri);
            CardInfo origial = icp.GetByID(info.CardID).QueryObject;
            if (origial == null)
            {
                icp.Insert(info, unitWork);
            }
            else
            {
                UpdateCardAll(info, unitWork);
            }
            return unitWork.Commit();
        }
        /// <summary>
        /// 车牌名单发行
        /// </summary>
        /// <param name="info">名单信息</param>
        /// <param name="releaseMoney">收费金额</param>
        /// <param name="paymentMode">收费类型</param>
        /// <param name="memo">备注</param>
        /// <param name="tryCount">卡号生成重试次数</param>
        /// <returns></returns>
        public CommandResult CarPlateRelease(CardInfo info, decimal releaseMoney, PaymentMode paymentMode, string memo, int tryCount)
        {
            CommandResult result = new CommandResult(ResultCode.Fail);
            string cardID = info.CardID;//发行时的卡号
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            ICardProvider icp = ProviderFactory.Create<ICardProvider>(_RepoUri);

            //如果没有卡号，自动生成卡号
            if (string.IsNullOrEmpty(info.CardID))
            {
                for (int i = 0; i < tryCount; i++)
                {
                    string listID = ListCardIDCreater.CreateListCardID();
                    CardInfo idInfo = icp.GetByID(listID).QueryObject;
                    if (idInfo == null)
                    {
                        info.CardID = listID;
                        break;
                    }
                }
            }

            //生成卡号失败，发行失败
            if (string.IsNullOrEmpty(info.CardID))
            {
                result.Message = Resource1.CardBll_CreateListCardIDFail;
                return result;
            }

            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri); //工作单元
            CardReleaseRecord record = new CardReleaseRecord
            {
                CardID = info.CardID,
                OwnerName = info.OwnerName,
                CardCertificate = info.CardCertificate,
                CarPlate = info.CarPlate,
                ReleaseDateTime = DateTime.Now,
                CardType = info.CardType,
                ReleaseMoney = releaseMoney,
                PaymentMode = paymentMode,
                Balance = info.Balance,
                ActivationDate = info.ActivationDate,
                ValidDate = info.ValidDate,
                HolidayEnabled = info.HolidayEnabled,
                Deposit = info.Deposit,
                OperatorID = op,
                StationID = station,
                Memo = memo
            };
            ProviderFactory.Create<ICardReleaseRecordProvider>(_RepoUri).Insert(record, unitWork);
            info.Release();
            if (string.IsNullOrEmpty(cardID))
            {
                //如果发行时没有设置卡号的，不同查找了，直接插入新记录
                icp.Insert(info, unitWork);
            }
            else
            {
                CardInfo origial = icp.GetByID(info.CardID).QueryObject;
                if (origial == null)
                {
                    icp.Insert(info, unitWork);
                }
                else if (origial.IsCardList
                    || (!string.IsNullOrEmpty(origial.CarPlate) && origial.CarPlate != info.CarPlate))
                {
                    //如果已存在该卡号，并且不是当前车牌名单的，返回失败
                    result.Message = string.Format(Resource1.CardBll_CardIDbeUsed, info.CardID);
                    return result;
                }
                else
                {
                    UpdateCardAll(info, unitWork);
                }
            }
            result = unitWork.Commit();

            return result;
        }
        /// <summary>
        /// 收取卡片的停车费
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public CommandResult PayParkFee(CardPaymentInfo payment)
        {
            return PayParkFee(null, payment);
        }

        /// <summary>
        /// 收取卡片的停车费
        /// </summary>
        /// <param name="info">缴费卡片，为空值时从数据库中获取,主要用于写卡模式时读取到卡片的数据</param>
        /// <param name="payment">缴费记录</param>
        /// <returns></returns>
        public CommandResult PayParkFee(CardInfo info, CardPaymentInfo payment)
        {
            CardInfo original = GetCardByID(payment.CardID).QueryObject;
            if (original != null)
            {
                CardInfo card = original.Clone();
                if (info != null)
                {
                    //复制卡片缴费信息
                    CardDateResolver.Instance.CopyPaidDataToCard(card, info);
                }

                card.UpdateFlag = true;
                payment.UpdateFlag = true;

                IUnitWork uw = ProviderFactory.Create<IUnitWork>(_RepoUri);

                if (payment.PaymentMode == PaymentMode.Prepay)
                {
                    card.Balance -= payment.Paid;
                }

                //只有卡片在场或可重复出场，并且与缴费记录的进场时间相同，才会更新卡片信息
                if ((card.IsInPark || card.CanRepeatOut)
                    && payment.EnterDateTime.HasValue
                    && card.LastDateTime == payment.EnterDateTime.Value)
                {
                    //设置卡片缴费信息
                    card.SetPaidData(payment);

                    _Provider.Update(card, original, uw);
                }

                (ProviderFactory.Create<ICardPaymentRecordProvider>(_RepoUri)).Insert(payment, uw);
                CommandResult result = uw.Commit();
                if (result.Result == ResultCode.Successful && info != null)
                {
                    //修改卡片实体类信息
                    CardDateResolver.Instance.CopyPaidDataToCard(info, card);
                }

                return result;
            }

            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }

        /// <summary>
        /// 收取卡片的停车费,并且保存到备份数据库,备份数据库保存成功即可
        /// </summary>
        /// <param name="info">缴费卡片，为空值时从数据库中获取,主要用于写卡模式时读取到卡片的数据</param>
        /// <param name="payment">缴费记录</param>
        /// <param name="standby">备用数据库</param>
        /// <returns></returns>
        public CommandResult PayParkFeeWithStandby(CardInfo info, CardPaymentInfo payment,string standby)
        {
            bool hasMaster = !string.IsNullOrEmpty(_RepoUri);
            bool hasStandby = !string.IsNullOrEmpty(standby);

            if ((!hasMaster && !hasStandby))//两个数据库都没有连接
                return new CommandResult(ResultCode.CannotConnectServer, ResultCodeDecription.GetDescription(ResultCode.CannotConnectServer));

            CardInfo original = null;
            bool updateflag = true;

            //有主数据库时，先从数据库获取
            if (hasMaster)
            {
                original = GetCardByID(payment.CardID).QueryObject;
            }
            else if (hasStandby)//没有主数据库而有备用数据库的，从备用数据库中获取
            {
                updateflag = false;
                ICardProvider sprovider = ProviderFactory.Create<ICardProvider>(standby);
                original = sprovider.GetByID(payment.CardID).QueryObject;
            }

            if (original != null)
            {
                CardInfo card = original.Clone();
                if (info != null)
                {
                    //复制卡片缴费信息
                    CardDateResolver.Instance.CopyPaidDataToCard(card, info);
                }

                IUnitWork uw = null;
                if (updateflag)//上传到主数据库
                {
                    uw = ProviderFactory.Create<IUnitWork>(_RepoUri);
                }
                else//上传到备用数据库
                {
                    uw = ProviderFactory.Create<IUnitWork>(standby);
                }

                if (payment.PaymentMode == PaymentMode.Prepay)
                {
                    card.Balance -= payment.Paid;
                }

                card.UpdateFlag = updateflag;
                payment.UpdateFlag = updateflag;

                //只有卡片在场或可重复出场，并且与缴费记录的进场时间相同，才会更新卡片信息
                if ((card.IsInPark || card.CanRepeatOut)
                    && payment.EnterDateTime.HasValue
                    && card.LastDateTime == payment.EnterDateTime.Value)
                {
                    //设置卡片缴费信息
                    card.SetPaidData(payment);

                    _Provider.Update(card, original, uw);
                }

                ICardPaymentRecordProvider paymentProvider = (ProviderFactory.Create<ICardPaymentRecordProvider>(_RepoUri));
                paymentProvider.Insert(payment, uw);

                CommandResult result = uw.Commit();//上传到数据库，由updateflag确定上传的时哪个数据库
                if (updateflag && hasStandby)//updateflag为True时表示已上传到主数据库,当有备份数据库时,需上传到备份数据库,为False时表示已上传到备份数据库
                {
                    if (result.Result != ResultCode.Successful)//失败后，并标记为未上传
                    {
                        card.UpdateFlag = false;
                        payment.UpdateFlag = false;
                    }

                    uw = ProviderFactory.Create<IUnitWork>(standby);

                    //只有卡片在场或可重复出场，并且与缴费记录的进场时间相同，才会更新卡片信息
                    if ((card.IsInPark || card.CanRepeatOut)
                        && payment.EnterDateTime.HasValue
                        && card.LastDateTime == payment.EnterDateTime.Value)
                    {
                        _Provider.Update(card, original, uw);
                    }
                    paymentProvider.Insert(payment, uw);
                    result = uw.Commit();//上传到备份数据库
                }

                if (result.Result == ResultCode.Successful && info != null)
                {
                    //修改卡片实体类信息
                    CardDateResolver.Instance.CopyPaidDataToCard(info, card);
                    info.UpdateFlag = card.UpdateFlag;
                }

                return result;
            }

            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }

        /// <summary>
        /// 收取卡片的停车费
        /// </summary>
        /// <param name="info">缴费卡片，为空值时从数据库中获取,主要用于写卡模式时读取到卡片的数据</param>
        /// <param name="payment">缴费记录</param>
        /// <param name="standby">备用数据库</param>
        /// <returns></returns>
        public CommandResult PayParkFeeWithStandbyBoth(CardInfo info, CardPaymentInfo payment, string standby)
        {
            bool hasMaster = !string.IsNullOrEmpty(_RepoUri);
            bool hasStandby = !string.IsNullOrEmpty(standby);

            if (!hasMaster || !hasStandby)//两个数据库同时上传时，两个数据库都需要有连接
                return new CommandResult(ResultCode.CannotConnectServer, ResultCodeDecription.GetDescription(ResultCode.CannotConnectServer));
            
            ICardProvider _standbycardProvider = ProviderFactory.Create<ICardProvider>(standby);

            CardInfo original = GetCardByID(payment.CardID).QueryObject;
            CardInfo standbyoriginal = _standbycardProvider.GetByID(payment.CardID).QueryObject;

            if (original != null && standbyoriginal != null)
            {
                CardInfo card = original.Clone();//已主数据库数据为准
                if (info != null)
                {
                    //复制卡片缴费信息
                    CardDateResolver.Instance.CopyPaidDataToCard(card, info);
                }

                IUnitWork uw = ProviderFactory.Create<IUnitWork>(_RepoUri);
                IUnitWork standbyuw = ProviderFactory.Create<IUnitWork>(standby);

                card.UpdateFlag = true;
                payment.UpdateFlag = true;

                //只有卡片在场或可重复出场，并且与缴费记录的进场时间相同，才会更新卡片信息
                if ((card.IsInPark || card.CanRepeatOut)
                    && payment.EnterDateTime.HasValue
                    && card.LastDateTime == payment.EnterDateTime.Value)
                {
                    //设置卡片缴费信息
                    card.SetPaidData(payment);

                    _Provider.Update(card, original, uw);
                    _standbycardProvider.Update(card, standbyoriginal, standbyuw);
                }

                ICardPaymentRecordProvider paymentProvider = (ProviderFactory.Create<ICardPaymentRecordProvider>(_RepoUri));
                ICardPaymentRecordProvider standbypaymentProvider = ProviderFactory.Create<ICardPaymentRecordProvider>(standby);

                paymentProvider.Insert(payment, uw);
                standbypaymentProvider.Insert(payment, standbyuw);

                CommandResult result = uw.Commit();

                //主数据库更新成功，更新备用数据库
                if (result.Result == ResultCode.Successful)
                {
                    result = standbyuw.Commit();
                    //更新备份数据库成功，更新卡片信息
                    if (result.Result == ResultCode.Successful && info != null)
                    {
                        //修改卡片实体类信息，以主数据库信息为准
                        CardDateResolver.Instance.CopyPaidDataToCard(info, card);
                        info.UpdateFlag = card.UpdateFlag;
                    }
                    else //更新备份数据库失败，恢复主数据库信息
                    {
                        try
                        {
                            uw = ProviderFactory.Create<IUnitWork>(_RepoUri);
                            _Provider.Update(original, card, uw);
                            paymentProvider.Delete(payment, uw);

                            CommandResult recoverResult = uw.Commit();
                            if (recoverResult.Result != ResultCode.Successful)
                            {
                                throw new InvalidOperationException("更新备份数据库失败，恢复主数据库数据失败！");
                            }
                        }
                        catch (Exception ex)
                        {
                            GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                        }
                    }
                }

                return result;
            }

            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }

         /// <summary>
        /// 收取卡片的停车费
        /// </summary>
        /// <param name="info">缴费卡片，为空值时从数据库中获取,主要用于写卡模式时读取到卡片的数据</param>
        /// <param name="payment">缴费记录</param>
        /// <param name="standby">备用数据库</param>
        /// <param name="both">是否同时保存到数据库</param>
        /// <param name="offlineHandleCard">是否按脱机模式脱机处理卡片</param>
        /// <returns></returns>
        public CommandResult PayParkFee(CardInfo info, CardPaymentInfo payment, string standby, bool both, bool offlineHandleCard)
        {
            CommandResult result = null;

            if (offlineHandleCard)
            {
                //脱机模式时按脱机模式处理的卡片，收费信息只需要保存到主数据库
                result = PayParkFee(info, payment);
            }
            else if (both)
            {
                //需要同时保存到数据库，必须两个都保存
                result = PayParkFeeWithStandbyBoth(info, payment, standby);
            }
            else
            {
                //需要保存到主数据库和备用数据库，成功保存一个就可以了
                result = PayParkFeeWithStandby(info, payment, standby);
            }
            return result;
        }

        /// <summary>
        /// 插入卡片
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Insert(CardInfo info)
        {
            return _Provider.Insert(info);
        }

        /// <summary>
        /// 删除所有卡片
        /// </summary>
        /// <returns></returns>
        public CommandResult DeteleAllCards()
        {
            return _Provider.DeleteAllItems();
        }

        /// <summary>
        /// 更新卡片，如没有该卡片，则插入卡片
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult UpdateOrInsert(CardInfo info)
        {
            CardInfo original = _Provider.GetByID(info.CardID).QueryObject;
            if (original != null)
            {
                return _Provider.Update(info, original);
            }
            else
            {
                return _Provider.Insert(info);
            }
        }

        /// <summary>
        /// 同步卡片到数据库，不会同步缴费的相关信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult SyncCardToDatabaseWithoutPaymentInfo(CardInfo info,string database)
        {
            if (info == null) return new CommandResult(ResultCode.NoRecord);
            if (string.IsNullOrEmpty(database)) return new CommandResult(ResultCode.CannotConnectServer);

            ICardProvider _databaseProvider = ProviderFactory.Create<ICardProvider>(database);
            CardInfo original = _databaseProvider.GetByID(info.CardID).QueryObject;
            if (original != null)
            {
                CardInfo card = info.Clone();
                card.ParkingStatus = original.ParkingStatus;
                card.LastDateTime = original.LastDateTime;
                card.PaidDateTime = original.PaidDateTime;
                card.ParkFee = original.ParkFee;
                card.TotalPaidFee = original.TotalPaidFee;
                card.LastCarPlate = original.LastCarPlate;
                card.UpdateFlag = original.UpdateFlag;
                card.FreeDateTime = original.FreeDateTime;

                if (original.IsTempCard)
                {
                    //临时卡保存车牌号码
                    card.CarPlate = original.CarPlate;
                }
                return _databaseProvider.Update(info, original);
            }
            else
            {
                return _databaseProvider.Insert(info);
            }
        }

        /// <summary>
        /// 回滚收费信息
        /// </summary>
        /// <param name="beforePay">卡片收取前信息</param>
        /// <param name="record">收费记录</param>
        public void RollbackPayment(CardInfo info, CardPaymentInfo record)
        {
            UpdateCardPaymentInfo(info);
            ICardPaymentRecordProvider payPorvider = ProviderFactory.Create<ICardPaymentRecordProvider>(this._RepoUri);
            payPorvider.Delete(record);
        }

        /// <summary>
        /// 回滚收费信息
        /// </summary>
        /// <param name="beforePay">卡片收取前信息</param>
        /// <param name="chargeDateTime">收费时间</param>
        public void RollbackPayment(CardInfo info, DateTime chargeDateTime)
        {
            UpdateCardPaymentInfo(info);
            ICardPaymentRecordProvider payPorvider = ProviderFactory.Create<ICardPaymentRecordProvider>(this._RepoUri);
            RecordSearchCondition search = new RecordSearchCondition();
            search.CardID = info.CardID;
            search.RecordDateTimeRange = new DateTimeRange(chargeDateTime, chargeDateTime);
            QueryResultList<CardPaymentInfo> records = payPorvider.GetItems(search);
            if (records.Result == ResultCode.Successful)
            {
                foreach (CardPaymentInfo record in records.QueryObjects)
                {
                    payPorvider.Delete(record);
                }
            }
        }
        #endregion

        #region 获取各种卡片操作记录
        /// <summary>
        /// 通过查询条件获取相应的卡片充值记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardChargeRecord> GetCardChargeRecords(RecordSearchCondition search)
        {
            ICardChargeRecordProvider p = ProviderFactory.Create<ICardChargeRecordProvider>(_RepoUri);
            return p.GetItems(search);
        }
        /// <summary>
        /// 通过查询条件获取相应的卡片延期记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardDeferRecord> GetCardDeferRecords(RecordSearchCondition search)
        {
            ICardDeferRecordProvider p = ProviderFactory.Create<ICardDeferRecordProvider>(_RepoUri);
            return p.GetItems(search);
        }
        /// <summary>
        /// 通过查询条件获取相应的卡片挂失恢复记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardLostRestoreRecord> GetCardLostRestoreRecords(RecordSearchCondition search)
        {
            ICardLostRestoreRecordProvider p = ProviderFactory.Create<ICardLostRestoreRecordProvider>(_RepoUri);
            return p.GetItems(search);
        }
        /// <summary>
        /// 通过查询条件获取相应的卡片禁用启用记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardDisableEnableRecord> GetCardDisableEnableRecords(RecordSearchCondition search)
        {
            ICardDisableEnableRecordProvider p = ProviderFactory.Create<ICardDisableEnableRecordProvider>(_RepoUri);
            return p.GetItems(search);
        }
        /// <summary>
        /// 通过查询条件获取相应的卡片回收记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardRecycleRecord> GetCardRecycleRecords(RecordSearchCondition search)
        {
            ICardRecycleRecordProvider p = ProviderFactory.Create<ICardRecycleRecordProvider>(_RepoUri);
            return p.GetItems(search);
        }
        /// <summary>
        /// 通过查询条件获取相应的卡片发行记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardReleaseRecord> GetCardReleaseRecords(RecordSearchCondition search)
        {
            ICardReleaseRecordProvider p = ProviderFactory.Create<ICardReleaseRecordProvider>(_RepoUri);
            return p.GetItems(search);
        }
        /// <summary>
        /// 通过查询条件获取相应的卡片删除记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardDeleteRecord> GetCardDeleteRecords(RecordSearchCondition search)
        {
            ICardDeleteRecordProvider provider = ProviderFactory.Create<ICardDeleteRecordProvider>(_RepoUri);
            return provider.GetItems(search);
        }
        /// <summary>
        /// 通过查询条件获取相应的缴费机退款记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<APMRefundRecord> GetAPMRefundRecords(RecordSearchCondition search)
        {
            IAPMRefundRecordProvider provider = ProviderFactory.Create<IAPMRefundRecordProvider>(_RepoUri);
            return provider.GetItems(search);
        }
        #endregion

        #region 写卡模式相关

        #region 私有方法
        ///// <summary>
        ///// 卡片出内车场费用后的卡片信息（将费用累加到累计停车费用）
        ///// </summary>
        ///// <param name="info">卡片</param>
        ///// <param name="ts">费率</param>
        ///// <param name="carType">车型</param>
        ///// <param name="chargeDateTime">缴费时间</param>
        ///// <returns></returns>
        //private void CardPayNestedParkFee(CardInfo card, TariffSetting ts, Byte carType, DateTime chargeDateTime)
        //{
        //    ParkAccountsInfo parkFee = ts.CalculateCardNestedParkFee(card, carType, chargeDateTime);

        //    ////不产生费用的，不记录缴费时间和累计停车费用，只更新内车场累计停车时间
        //    //if (parkFee.Accounts > 0)
        //    //{
        //    //    card.TotalFee += parkFee.Accounts;
        //    //    card.IsIndoorPaid = true;//更新内车场缴费标识
        //    //    card.PaidDateTime = chargeDateTime;//记录缴费时间
        //    //}

        //    card.TotalFee += parkFee.Accounts;

        //    if (card.IsIndoorPaid && ts.IsInFreeTime(card.PaidDateTime.Value, chargeDateTime))
        //    {
        //        //已缴费，并且处于缴费后免费时间的，不记录缴费时间
        //        //（防止下面这种情况出现：中央收费后设置允许15分钟内可以免费出场，则有些车主在入场后每隔15分钟去刷一次卡交费，
        //        //这样出场时就不会产生费用）
        //    }
        //    else
        //    {
        //        card.PaidDateTime = chargeDateTime;//记录缴费时间
        //    }


        //    card.IsInNestedPark = false ;//出内车场状态
        //    card.IsIndoorPaid = true;//更新内车场缴费标识
        //    card.UpdateIndoorTimeInterval(chargeDateTime);//更新内车场累计停车时间

        //}

        ///// <summary>
        ///// 卡片进入内车场
        ///// </summary>
        ///// <param name="info">卡片</param>
        ///// <param name="enterDateTime">进入时间</param>
        ///// <returns>进行后的卡片信息</returns>
        //private void CardEnterNestedPark(CardInfo info, DateTime enterDateTime)
        //{
        //    info.IsIndoorPaid = false ;//清除内车场缴费标识
        //    info.IsInNestedPark = true;
        //    info.IndoorInDateTime = enterDateTime;
        //}
        #endregion

        #region 公共方法 
        /// <summary>
        /// 删除卡片最近一条由某操作员收费的缴费记录
        /// </summary>
        /// <param name="info">缴费记录</param>
        /// <param name="operatorInfo">缴费的操作员</param>
        /// <returns></returns>
        public CommandResult DeleteLastPayment(CardInfo info,OperatorInfo operatorInfo)
        {
            if (info != null)
            {
                CardInfo card = info.Clone();
                CardPaymentInfo paymentInfo = (new CardPaymentRecordBll(_RepoUri)).GetLatestRecord(info.CardID, info.LastDateTime,operatorInfo);
                if (paymentInfo != null)
                {
                    if (paymentInfo.SettleDateTime != null) return new CommandResult(ResultCode.Fail, "已经结算");
                        ////return new CommandResult (ResultCode .Fail ,(Resource1.FrmCenterCharge_RecordHandled));
                    IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
                    ICardPaymentRecordProvider recordProvider = ProviderFactory.Create<ICardPaymentRecordProvider>(_RepoUri);

                    //已缴费用减去记录收取的费用和折扣费用
                    card.TotalPaidFee -= paymentInfo.Paid + paymentInfo.Discount;
                    if (card.TotalPaidFee < 0) card.TotalPaidFee = 0;
                    //如果已缴费用为0，说明用户未缴费，将卡片标记为未缴费，这是因为当停车费用为零时，标记为已收费时，卡片将不能重新收费
                    if (card.TotalPaidFee == 0) card.IsPaid = false;

                    _Provider.Update(card, info, unitWork);

                    recordProvider.Delete(paymentInfo, unitWork);

                    AlarmInfo alarm = new AlarmInfo();
                    alarm.AlarmDateTime = DateTime.Now;
                    alarm.AlarmType = AlarmType.CancelCardPayment;
                    alarm.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
                    alarm.AlarmDescr = string.Format(Resource1.CardBll_CancelPaymentDescr, paymentInfo.CardID, paymentInfo.CardID,
                        paymentInfo.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss"), paymentInfo.EnterDateTime.Value.ToString("yyyy-MM-dd HH;mm:ss"),
                        paymentInfo.Accounts, paymentInfo.Paid, paymentInfo.Memo);
                    ProviderFactory.Create<IAlarmProvider>(_RepoUri).Insert(alarm, unitWork);

                    CommandResult result = unitWork.Commit();

                    if (result.Result == ResultCode.Successful)
                    {
                        //删除成功，更新卡片信息
                        info.TotalPaidFee = card.TotalPaidFee;
                        info.IsPaid = card.IsPaid;
                    }
                    return result;
                }
                else
                {
                    return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
                }
            }
            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }

        /// <summary>
        /// 删除卡片最近一条由某操作员收费的缴费记录,需要同时删除两个数据库的记录
        /// </summary>
        /// <param name="info">缴费记录</param>
        /// <param name="operatorInfo">缴费的操作员</param>
        /// <param name="standby">备用数据库连接字符串</param>
        /// <returns></returns>
        public CommandResult DeleteLastPaymentBoth(CardInfo info, OperatorInfo operatorInfo, string standby)
        {
            if (info != null)
            {
                CardPaymentInfo paymentInfo = (new CardPaymentRecordBll(_RepoUri)).GetLatestRecord(info.CardID, info.LastDateTime, operatorInfo);
                if (paymentInfo != null)
                {
                    if (paymentInfo.SettleDateTime != null) return new CommandResult(ResultCode.Fail, "已经结算");
                    CardInfo card = info.Clone();
                    IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
                    ICardPaymentRecordProvider recordProvider = ProviderFactory.Create<ICardPaymentRecordProvider>(_RepoUri);

                    //已缴费用减去记录收取的费用和折扣费用
                    card.TotalPaidFee -= paymentInfo.Paid + paymentInfo.Discount;
                    if (card.TotalPaidFee < 0) card.TotalPaidFee = 0;
                    //如果已缴费用为0，说明用户未缴费，将卡片标记为未缴费，这是因为当停车费用为零时，标记为已收费时，卡片将不能重新收费
                    if (card.TotalPaidFee == 0) card.IsPaid = false;

                    _Provider.Update(card, info, unitWork);

                    recordProvider.Delete(paymentInfo, unitWork);

                    AlarmInfo alarm = new AlarmInfo();
                    alarm.AlarmDateTime = DateTime.Now;
                    alarm.AlarmType = AlarmType.CancelCardPayment;
                    alarm.OperatorID = OperatorInfo.CurrentOperator.OperatorName;
                    alarm.AlarmDescr = string.Format(Resource1.CardBll_CancelPaymentDescr, paymentInfo.CardID, paymentInfo.CardID,
                        paymentInfo.ChargeDateTime.ToString("yyyy-MM-dd HH:mm:ss"), paymentInfo.EnterDateTime.Value.ToString("yyyy-MM-dd HH;mm:ss"),
                        paymentInfo.Accounts, paymentInfo.Paid, paymentInfo.Memo);
                    IAlarmProvider _alarmProvider = ProviderFactory.Create<IAlarmProvider>(_RepoUri);
                    _alarmProvider.Insert(alarm, unitWork);

                    CommandResult result = unitWork.Commit();

                    if (result.Result == ResultCode.Successful)
                    {
                        //删除备用数据库的记录
                        ICardProvider standbyProvider = ProviderFactory.Create<ICardProvider>(standby);
                        CardInfo standbyCard = standbyProvider.GetByID(card.CardID).QueryObject;
                        if (standbyCard != null)
                        {
                            IUnitWork standbyUW = ProviderFactory.Create<IUnitWork>(standby);
                            standbyProvider.Update(card, standbyCard, standbyUW);

                            ICardPaymentRecordProvider standbyRecordProvider = ProviderFactory.Create<ICardPaymentRecordProvider>(standby);
                            CardPaymentRecordSearchCondition search = new CardPaymentRecordSearchCondition();
                            search.CardID = paymentInfo.CardID;
                            search.ChargeDateTime = paymentInfo.ChargeDateTime;
                            search.EnterDateTime = paymentInfo.EnterDateTime;
                            List<CardPaymentInfo> standbyPayments = standbyRecordProvider.GetItems(search).QueryObjects;
                            if (standbyPayments != null)
                            {
                                foreach (CardPaymentInfo p in standbyPayments)
                                {
                                    standbyRecordProvider.Delete(p, standbyUW);
                                }
                            }
                            result = standbyUW.Commit();
                            if (result.Result != ResultCode.Successful)
                            {
                                //删除失败，恢复主数据库数据
                                try
                                {
                                    unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
                                    _Provider.Update(info, card, unitWork);
                                    _alarmProvider.Delete(alarm, unitWork);

                                    CommandResult recoverResult = unitWork.Commit();
                                    if (recoverResult.Result == ResultCode.Successful)
                                    {
                                        recoverResult = recordProvider.InsertWithPrimaryKey(paymentInfo);
                                    }

                                    if (recoverResult.Result != ResultCode.Successful)
                                    {
                                        throw new InvalidOperationException("删除备份数据库卡片最近的一条缴费记录，恢复主数据库数据失败！");
                                    }

                                }
                                catch (Exception ex)
                                {
                                    GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                                }
                            }
                        }
                    }

                    if (result.Result == ResultCode.Successful)
                    {
                        //删除成功，更新卡片信息
                        info.TotalPaidFee = card.TotalPaidFee;
                        info.IsPaid = card.IsPaid;
                    }
                    return result;
                }
                else
                {
                    return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
                }
            }
            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }

        /// <summary>
        /// 删除卡片最近一条由某操作员收费的缴费记录
        /// </summary>
        /// <param name="info">缴费记录</param>
        /// <param name="operatorInfo">缴费的操作员</param>
        /// <param name="standby">备用数据库连接字符串</param>
        /// <param name="both">是否同时更新到两个数据库</param>
        /// <returns></returns>
        public CommandResult DeleteLastPayment(CardInfo info,OperatorInfo operatorInfo, string standby, bool both)
        {
            if (both)
            {
                return DeleteLastPaymentBoth(info, operatorInfo, standby);
            }
            else
            {
                return DeleteLastPayment(info, operatorInfo);
            }
        }
        #endregion
        #endregion

        #region 车牌名单相关
        /// <summary>
        /// 通过车牌号码获取所有车牌名单
        /// </summary>
        /// <param name="carPlate"></param>
        /// <returns></returns>
        public List<CardInfo> GetCarPlateLists(string carPlate)
        {
            CardSearchCondition search = new CardSearchCondition();
            search.ListType = CardListType.CarPlate;
            search.ListCarPlate = carPlate;
            return _Provider.GetItems(search).QueryObjects;
        }
        /// <summary>
        /// 通过车牌号码获取第一个车牌名单
        /// </summary>
        /// <param name="carPlate"></param>
        /// <returns></returns>
        public CardInfo GetFirstCarPlateList(string carPlate)
        {
            CardSearchCondition search = new CardSearchCondition();
            search.ListType = CardListType.CarPlate;
            search.ListCarPlate = carPlate;
            QueryResultList<CardInfo> result = _Provider.GetItems(search);
            if (result.Result == ResultCode.Successful
                && result.QueryObjects != null
                && result.QueryObjects.Count > 0)
            {
                return result.QueryObjects[0];
            }

            return null;
        }
        /// <summary>
        /// 通过车牌号码获取第一个车牌名单（信息中包括最近一条收费记录)
        /// </summary>
        /// <param name="carPlate"></param>
        /// <returns></returns>
        public CardInfo GetCarPlateListDetail(string carPlate)
        {
            CardInfo card = null;
            CardSearchCondition search = new CardSearchCondition();
            search.ListType = CardListType.CarPlate;
            search.ListCarPlate = carPlate;
            QueryResultList<CardInfo> result = _Provider.GetItems(search);
            if (result.Result == ResultCode.Successful
                && result.QueryObjects != null
                && result.QueryObjects.Count > 0)
            {
                card = result.QueryObjects[0];
            }
            if (card != null)  //已经收费)
            {
                card.LastPayment = (new CardPaymentRecordBll(_RepoUri)).GetLatestRecord(card.CardID, card.LastDateTime, null);
            }
            return card;
        }
        /// <summary>
        /// 获取所有车牌名单
        /// </summary>
        /// <returns></returns>
        public QueryResultList<CardInfo> GetAllCarPlateLists()
        {
            CardSearchCondition con = new CardSearchCondition();
            //con.Status = CardStatus.Enabled | CardStatus.Disabled | CardStatus.Loss | CardStatus.Recycled;
            con.ListType = CardListType.CarPlate;
            return _Provider.GetItems(con);
        }
        #endregion
    }
}
