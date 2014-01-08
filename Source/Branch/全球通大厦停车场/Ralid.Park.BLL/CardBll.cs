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
                card.LastPayment = (new CardPaymentRecordBll(_RepoUri)).GetLatestRecord(card.CardID, card.LastDateTime);
            }
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
        /// 获取所有卡片
        /// </summary>
        /// <returns></returns>
        public QueryResultList<CardInfo> GetAllCards()
        {
            CardSearchCondition con = new CardSearchCondition();
            con.Status = CardStatus.Enabled | CardStatus.Disabled | CardStatus.Loss | CardStatus.Recycled;
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
        /// 修改卡片信息，此方法不会修改卡片的运行状态(如入场时间，卡片出入场状态等)也不能修改卡片的有效期，余额等
        /// </summary>
        /// <returns></returns>
        public CommandResult UpdateCard(CardInfo info)
        {
            CardInfo original = _Provider.GetByID(info.CardID).QueryObject;
            if (original != null)
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
        /// 保存卡片与卡片事件信息（用于停车场产生事件时更新卡片状态并保存事件)
        /// </summary>
        /// <param name="card"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        public CommandResult SaveCardAndEvent(CardInfo card, CardEventReport report)
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
                if (report.LimitationRemain != info.LimitationRemain)
                {
                    info.LimitationRemain = report.LimitationRemain;
                    info.LimitationTimestamp = report.EventDateTime;
                }
                //入口刷卡事件时，将缴费时间，停车费用，累计停车费用清空
                if (!report.IsExitEvent)
                {
                    info.ClearPaidData();
                }
                _Provider.Update(info, card, unitWork);
            }
            ICardEventProvider icp = ProviderFactory.Create<ICardEventProvider>(_RepoUri);
            icp.Insert((new CardEventRecord(report)), unitWork);

            if (report.IsExitEvent && report.Limitation > 0) //出场事件且有限时停车的记录才要记录到上传表中。
            {
                ECardRecord ecr = new ECardRecord()
                {
                    SheetID = card.SheetID,
                    Carplate = report.CarPlate,
                    CardID = report.CardID,
                    EventDt = report.EventDateTime,
                    EnterDt = report.LastDateTime,
                    Limitation = report.Limitation,
                    LimitationRemain = report.LimitationRemain
                };
                IECardRecordProvider iecr = ProviderFactory.Create<IECardRecordProvider>(_RepoUri);
                iecr.Insert(ecr, unitWork);
            }
            CommandResult ret = unitWork.Commit();
            if (ret.Result == ResultCode.Successful) //如果成功，则改变卡片状态
            {
                //卡片状态保持用数据库中的状态
                card.ParkingStatus = report.ParkingStatus;
                card.LastDateTime = report.EventDateTime;
                card.LastEntrance = report.EntranceID;
                card.LastCarPlate = report.CarPlate;
                card.LimitationRemain = report.LimitationRemain;
                //入口刷卡事件时，将缴费时间，停车费用，累计停车费用清空
                if (!report.IsExitEvent)
                {
                    card.ClearPaidData();
                }
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
        /// 卡片挂失
        /// </summary>
        /// <param name="info"></param>
        /// <param name="reason"></param>
        /// <param name="lostCardCost"></param>
        /// <param name="paymode"></param>
        /// <param name="keepParkingStatus">是否保持数据库中的卡片运行状态</param>
        public CommandResult CardLoss(CardInfo info, string reason, decimal lostCardCost, PaymentMode paymode, bool keepParkingStatus)
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
            info.Lost();
            lostProvider.Insert(record, unitWork);
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
            ICardLostRestoreRecordProvider restorProvider = ProviderFactory.Create<ICardLostRestoreRecordProvider>(_RepoUri);
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
        /// <param name="info"></param>
        /// <param name="chargeAmount"></param>
        /// <param name="payment"></param>
        /// <param name="paymentMode"></param>
        /// <param name="validDate"></param>
        /// <param name="memo"></param>
        /// <param name="keepParkingStatus">是否保持数据库中的卡片运行状态</param>
        /// <returns></returns>
        public CommandResult CardCharge(CardInfo info, Decimal chargeAmount, Decimal payment, PaymentMode paymentMode, DateTime validDate, string memo, bool keepParkingStatus)
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
                    ChargeDateTime = DateTime.Now,
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
        public CommandResult CardDefer(CardInfo info, DateTime newValidDate, PaymentMode paymentMode, decimal money, string memo, bool keepParkingStatus)
        {
            string op = OperatorInfo.CurrentOperator.OperatorName;
            string station = WorkStationInfo.CurrentStation.StationName;
            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
            CardDeferRecord record = new CardDeferRecord
            {
                CardID = info.CardID,
                OwnerName = info.OwnerName,
                CardCertificate = info.CardCertificate,
                CarPlate = info.CarPlate,
                OriginalDate = info.ValidDate,
                CurrentDate = newValidDate,
                DeferDateTime = DateTime.Now,
                PaymentMode = paymentMode,
                DeferMoney = money,
                OperatorID = op,
                StationID = station,
                Memo = memo
            };
            ICardDeferRecordProvider recordProvider = ProviderFactory.Create<ICardDeferRecordProvider>(_RepoUri);
            recordProvider.Insert(record, unitWork);
            info.ValidDate = newValidDate;
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
        /// <param name="keepParkingStatus">是否保持卡片运行状态</param>
        /// <returns></returns>
        public CommandResult CardRelease(CardInfo info, decimal releaseMoney, PaymentMode paymentMode, string memo, bool keepParkingStatus)
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
            else if (keepParkingStatus)
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
                    //info.Balance = card.Balance;
                    //info.ParkingStatus = card.ParkingStatus;
                    //info.ParkFee = card.ParkFee;
                    //info.PaidDateTime = card.PaidDateTime;
                    //info.TotalFee = card.TotalFee;
                }

                return result;
            }

            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
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
        ///// <summary>
        ///// 更新卡片写卡模式的相关数据（包括卡格式版本、室内停车场的进入时间、
        ///// 室内停车场累计停车时间、缴费时间、当前车场已收的停车费用、累计停车费用、停车状态等）
        ///// </summary>
        ///// <param name="info"></param>
        ///// <returns></returns>
        //public CommandResult UpdateOffLineCardData(CardInfo info)
        //{
        //    CardInfo original = _Provider.GetByID(info.CardID).QueryObject;
        //    if (original != null)
        //    {
        //        CardInfo card = original.Clone();
        //        //只更新写卡模式相关的属性
        //        card.CardVersion = info.CardVersion;
        //        card.IndoorInDateTime = info.IndoorInDateTime;
        //        card.IndoorTimeInterval = info.IndoorTimeInterval;
        //        card.PaidDateTime = info.PaidDateTime;
        //        card.ParkFee = info.ParkFee;
        //        card.TotalFee = info.TotalFee;

        //        //收费时需要更停车状态
        //        card.ParkingStatus = info.ParkingStatus;

        //        return _Provider.Update(card, original);
        //    }
        //    return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        //}

        /// <summary>
        /// 删除卡片最近的一条缴费记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult DeleteLastPayment(CardInfo info)
        {
            if (info.LastPayment != null)
            {
                CardInfo card = info.Clone();
                CardPaymentInfo paymentInfo = info.LastPayment;
                //CardPaymentInfo record = null;
                IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
                ICardPaymentRecordProvider recordProvider = ProviderFactory.Create<ICardPaymentRecordProvider>(_RepoUri);

                ////重新设置卡片费用信息
                ////card.TotalFee += paymentInfo.Paid + paymentInfo.Discount;//加上删除的收费费用和折扣

                //if (!string.IsNullOrEmpty(paymentInfo.LastStationID)
                //    || paymentInfo.LastTotalPaid != 0
                //    || paymentInfo.LastTotalDiscount != 0)
                //{
                //    //有上上次的缴费记录
                //    //先查找卡片是否有多条缴费记录
                //    CardPaymentRecordSearchCondition con = new CardPaymentRecordSearchCondition();
                //    con.CardID = card.CardID;
                //    con.EnterDateTime = card.LastDateTime;
                //    List<CardPaymentInfo> records = recordProvider.GetItems(con).QueryObjects;
                //    if (records.Count > 1)//有多条缴费记录
                //    {
                //        records = (from r in records
                //                   orderby r.ChargeDateTime descending
                //                   select r).ToList();
                //        record = records[1];//获取第二近的缴费记录
                //    }

                //}
                //if (record != null)
                //{
                //    //删除的记录的车场停车费用比上一条记录的停车费用多，说明两条记录之间有费用产生
                //    if (paymentInfo.ParkFee > record.ParkFee)
                //    {
                //        //如果两条记录相隔缴费时间内有产生费用，累计费用需要减去产生的费用
                //        card.TotalFee -= paymentInfo.ParkFee - record.ParkFee;
                //        card.ParkFee = record.ParkFee;//重新设置外车场费用
                //    }
                //    card.PaidDateTime = record.ChargeDateTime;//重新设置收费时间
                //    card.IsPaid = true;
                //}
                //else
                //{
                //    //没有上上次的缴费记录
                //    card.TotalFee -= paymentInfo.Accounts;//减去应缴费用
                //    card.TotalFee = card.TotalFee < 0 ? 0 : card.TotalFee;
                //    card.ParkFee = 0;
                //    card.IsPaid = false;//设置为未缴费
                //    card.PaidDateTime = null;
                //}

                //已缴费用减去记录收取的费用和折扣费用
                card.TotalPaidFee -= paymentInfo.Paid + paymentInfo.Discount;
                if (card.TotalPaidFee < 0) card.TotalPaidFee = 0;

                _Provider.Update(card, info, unitWork);

                recordProvider.Delete(paymentInfo);

                CommandResult result = unitWork.Commit();

                if (result.Result == ResultCode.Successful)
                {
                    //删除成功，更新卡片信息
                    //info.ParkingStatus = card.ParkingStatus;
                    //info.TotalFee = card.TotalFee;
                    //info.PaidDateTime = card.PaidDateTime;
                    //info.ParkFee = card.ParkFee;
                    info.TotalPaidFee = card.TotalPaidFee;
                }
                return result;

            }
            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }

        ///// <summary>
        ///// 收取卡片的内车场停车费（将费用累加到累计停车费用）
        ///// </summary>
        ///// <param name="payment"></param>
        ///// <returns></returns>
        //public CommandResult PayNestedParkFee(CardInfo info, TariffSetting ts, Byte carType, DateTime chargeDateTime)
        //{
        //    CardInfo card = info.Clone();
        //    CardPayNestedParkFee(card, ts, carType, chargeDateTime);

        //    CommandResult result = _Provider.Update(card, info);
        //    if (result.Result == ResultCode.Successful)
        //    {
        //        //更新数据库成功后，更新卡片相关信息
        //        info.ParkingStatus = card.ParkingStatus;
        //        info.PaidDateTime = card.PaidDateTime;
        //        info.TotalFee = card.TotalFee;
        //        info.IndoorTimeInterval = card.IndoorTimeInterval;
        //    }

        //    return result;
        //}
        #endregion
        #endregion

        #region 全球通接口
        public CommandResult SaveCard(string sheetNo, string staffID, string name, string mobile, string dept, string carplate, int state, DateTime activationDate, string access)
        {
            return null;
        }

        public CommandResult Update(string sheetNo, int state, DateTime activationDate)
        {
            return null;
        }
        #endregion
    }
}
