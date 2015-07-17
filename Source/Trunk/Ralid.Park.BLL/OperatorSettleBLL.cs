using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel .SearchCondition ;
using Ralid.GeneralLibrary.ExceptionHandling;

namespace Ralid.Park.BLL
{
    public class OperatorSettleBLL
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public OperatorSettleBLL(string repoUri)
        {
            _RepoUri = repoUri;
            provider = ProviderFactory.Create<IOperatorLogProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private IOperatorLogProvider provider;
        private string _RepoUri;
        #endregion

        #region 私有方法
        //获取事件列表中的应收费用总额
        private decimal CashParkDiscountSum(List<CardPaymentInfo> events)
        {
            decimal amount = 0;
            foreach (CardPaymentInfo e in events)
            {
                if (e.PaymentMode == PaymentMode.Cash)
                {
                    amount += e.Discount;
                }
            }
            return amount;
        }

        //获取事件列表中使用电脑通过收现方式收取的金额
        private decimal CashParkFactSum(List<CardPaymentInfo> events)
        {
            decimal amount = 0;
            foreach (CardPaymentInfo e in events)
            {
                if (e.PaymentMode == PaymentMode.Cash && e.PaymentCode == PaymentCode.Computer)
                {
                    amount += e.Paid;
                }
            }
            return amount;
        }

        private decimal CashPOSSum(List<CardPaymentInfo> events)
        {
            decimal amount = 0;
            foreach (CardPaymentInfo e in events)
            {
                if (e.PaymentMode == PaymentMode.Cash && e.PaymentCode == PaymentCode.POS)
                {
                    amount += e.Paid;
                }
            }
            return amount;
        }
        //获取事件列表中使用操作员卡通过收现方式收取的金额
        private decimal CashOperatorCardSum(List<CardPaymentInfo> events)
        {
            decimal amount = 0;
            foreach (CardPaymentInfo e in events)
            {
                if (e.PaymentMode == PaymentMode.Cash && e.PaymentCode == PaymentCode.FunctionCard)
                {
                    amount += e.Paid;
                }
            }
            return amount;
        }

        private decimal NoCashParkDiscountSum(List<CardPaymentInfo> events)
        {
            decimal amount = 0;
            foreach (CardPaymentInfo e in events)
            {
                if (e.PaymentMode != PaymentMode.Cash)
                {
                    amount += e.Discount;
                }
            }
            return amount;
        }

        private decimal NoCashParkFactSum(List<CardPaymentInfo> events)
        {
            decimal amount = 0;
            foreach (CardPaymentInfo e in events)
            {
                if (e.PaymentMode != PaymentMode.Cash)
                {
                    amount += e.Paid;
                }
            }
            return amount;
        }

        private decimal CashCardReleaseSum(List<CardReleaseRecord> items)
        {
            decimal amount = 0;
            foreach (CardReleaseRecord record in items)
            {
                if (record.PaymentMode == PaymentMode.Cash)
                {
                    amount += record.ReleaseMoney - record.Deposit;
                }
            }
            return amount;
        }

        private decimal NonCashCardReleaseSum(List<CardReleaseRecord> items)
        {
            decimal amount = 0;
            foreach (CardReleaseRecord record in items)
            {
                if (record.PaymentMode != PaymentMode.Cash)
                {
                    amount += record.ReleaseMoney - record.Deposit;
                }
            }
            return amount;
        }

        private decimal CashDepositSum(List<CardReleaseRecord> items)
        {
            decimal amount = 0;
            foreach (CardReleaseRecord record in items)
            {
                if (record.PaymentMode == PaymentMode.Cash)
                {
                    amount += record.Deposit;
                }
            }
            return amount;
        }

        private decimal NonCashDepositSum(List<CardReleaseRecord> items)
        {
            decimal amount = 0;
            foreach (CardReleaseRecord record in items)
            {
                if (record.PaymentMode != PaymentMode.Cash)
                {
                    amount += record.Deposit;
                }
            }
            return amount;
        }

        private decimal CashCardDeferSum(List<CardDeferRecord> items)
        {
            decimal amount = 0;
            foreach (CardDeferRecord record in items)
            {
                if (record.PaymentMode == PaymentMode.Cash)
                {
                    amount += record.DeferMoney;
                }
            }
            return amount;
        }

        private decimal NonCashCardDeferSum(List<CardDeferRecord> items)
        {
            decimal amount = 0;
            foreach (CardDeferRecord record in items)
            {
                if (record.PaymentMode != PaymentMode.Cash)
                {
                    amount += record.DeferMoney;
                }
            }
            return amount;
        }

        private decimal CashCardChargeSum(List<CardChargeRecord> items)
        {
            decimal amount = 0;
            foreach (CardChargeRecord record in items)
            {
                if (record.PaymentMode == PaymentMode.Cash)
                {
                    amount += record.Payment;
                }
            }
            return amount;
        }

        

        private decimal NonCashCardChargeSum(List<CardChargeRecord> items)
        {
            decimal amount = 0;
            foreach (CardChargeRecord record in items)
            {
                if (record.PaymentMode != PaymentMode.Cash)
                {
                    amount += record.Payment;
                }
            }
            return amount;
        }

        private decimal CashCardLostSum(List<CardLostRestoreRecord> items)
        {
            decimal amount = 0;
            foreach (CardLostRestoreRecord item in items)
            {
                if (item.LostCardCost != null && item.PaymentMode == PaymentMode.Cash)
                {
                    amount += item.LostCardCost.Value;
                }
            }
            return amount;
        }

        private decimal NonCashCardLostSum(List<CardLostRestoreRecord> items)
        {
            decimal amount = 0;
            foreach (CardLostRestoreRecord item in items)
            {
                if (item.LostCardCost != null && item.PaymentMode != PaymentMode.Cash)
                {
                    amount += item.LostCardCost.Value;
                }
            }
            return amount;
        }

        private decimal CashCardRecycleSum(List<CardRecycleRecord> items)
        {
            decimal amount = 0;
            foreach (CardRecycleRecord record in items)
            {
                amount += record.RecycleMoney;
            }
            return amount;
        }

        private decimal CashAPMRefundSum(List<APMRefundRecord> items)
        {
            decimal amount = 0;
            foreach (APMRefundRecord record in items)
            {
                amount += record.RefundMoney;
            }
            return amount;
        }


        private int TempCardRecycleSum(List<CardEventRecord> events)
        {
            int amount = 0;
            foreach (CardEventRecord e in events)
            {
                if (e.IsExitEvent && e.CardType == CardType.TempCard)
                {
                    amount++;
                }
            }
            return amount;
        }
        #endregion

        #region 公开方法
        /// <summary>
        /// 操作员结算
        /// </summary>
        /// <param name="opt"></param>
        /// <returns></returns>
        public CommandResult Settle(OperatorSettleLog opt)
        {
            IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_RepoUri);
            IOperatorLogProvider ip = ProviderFactory.Create<IOperatorLogProvider>(_RepoUri);
            ip.Insert(opt, unitWork);

            if (opt.PaymentRecords != null && opt.PaymentRecords.Count > 0)
            {
                ICardPaymentRecordProvider provider = ProviderFactory.Create<ICardPaymentRecordProvider>(_RepoUri);
                foreach (CardPaymentInfo record in opt.PaymentRecords)
                {
                    CardPaymentInfo original = record.Clone();
                    record.SettleDateTime = opt.SettleDateTime;
                    provider.Update(record, original, unitWork);
                }
            }
            if (opt.ReleaseRecords != null && opt.ReleaseRecords.Count > 0)
            {
                ICardReleaseRecordProvider provider = ProviderFactory.Create<ICardReleaseRecordProvider>(_RepoUri);
                foreach (CardReleaseRecord record in opt.ReleaseRecords)
                {
                    CardReleaseRecord original = record.Clone();
                    record.SettleDateTime = opt.SettleDateTime;
                    provider.Update(record, original, unitWork);
                }
            }
            if (opt.ChargeRecords != null && opt.ChargeRecords.Count > 0)
            {
                ICardChargeRecordProvider provider = ProviderFactory.Create<ICardChargeRecordProvider>(_RepoUri);
                foreach (CardChargeRecord record in opt.ChargeRecords)
                {
                    CardChargeRecord original = record.Clone();
                    record.SettleDateTime = opt.SettleDateTime;
                    provider.Update(record, original, unitWork);
                }
            }
            if (opt.DeferRecords != null && opt.DeferRecords.Count > 0)
            {
                ICardDeferRecordProvider provider = ProviderFactory.Create<ICardDeferRecordProvider>(_RepoUri);
                foreach (CardDeferRecord record in opt.DeferRecords)
                {
                    CardDeferRecord original = record.Clone();
                    record.SettleDateTime = opt.SettleDateTime;
                    provider.Update(record, original, unitWork);
                }
            }
            if (opt.CardLostRecords != null && opt.CardLostRecords.Count > 0)
            {
                ICardLostRestoreRecordProvider provider = ProviderFactory.Create<ICardLostRestoreRecordProvider>(_RepoUri);
                foreach (CardLostRestoreRecord record in opt.CardLostRecords)
                {
                    CardLostRestoreRecord original=record.Clone ();
                    record.SettleDateTime =opt.SettleDateTime ;
                    provider.Update(record, original, unitWork);
                }
            }
            if (opt.RecycleRecords != null && opt.RecycleRecords.Count > 0)
            {
                ICardRecycleRecordProvider provider = ProviderFactory.Create<ICardRecycleRecordProvider>(_RepoUri);
                foreach (CardRecycleRecord record in opt.RecycleRecords)
                {
                    CardRecycleRecord original = record.Clone();
                    record.SettleDateTime = opt.SettleDateTime;
                    provider.Update(record, original, unitWork);
                }
            }
            if (opt.APMRefundRecords != null && opt.APMRefundRecords.Count > 0)
            {
                IAPMRefundRecordProvider provider = ProviderFactory.Create<IAPMRefundRecordProvider>(_RepoUri);
                foreach (APMRefundRecord record in opt.APMRefundRecords)
                {
                    APMRefundRecord original = record.Clone();
                    record.SettleDateTime = opt.SettleDateTime;
                    provider.Update(record, original, unitWork);
                }
            }
            if (opt.EventRecords != null && opt.EventRecords.Count > 0)
            {
                ICardEventProvider provider = ProviderFactory.Create<ICardEventProvider>(_RepoUri);
                foreach (CardEventRecord record in opt.EventRecords)
                {
                    CardEventRecord original = record.Clone();
                    record.SettleDateTime = opt.SettleDateTime;
                    provider.Update(record, original, unitWork);
                }
            }
            if (opt.AlarmRecords != null && opt.AlarmRecords.Count > 0)
            {
                IAlarmProvider provider = ProviderFactory.Create<IAlarmProvider>(_RepoUri);
                foreach (AlarmInfo record in opt.AlarmRecords)
                {
                    AlarmInfo original = record.Clone();
                    record.SettleDateTime = opt.SettleDateTime;
                    provider.Update(record, original, unitWork);
                }
            }
            return unitWork.Commit();
        }

        public QueryResultList<OperatorSettleLog> GetOperatorLogs(RecordSearchCondition search)
        {
            return provider.GetItems(search);
        }

        /// <summary>
        /// 生成操作员结算汇总,如果不指定工作站，则表示汇总结果中包括操作员在所有工作站上的收费情况
        /// </summary>
        /// <param name="opt"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        public OperatorSettleLog CreateOperatorLog(OperatorInfo opt, WorkStationInfo station)
        {
            if (opt != null)
            {
                OperatorSettleLog log = new OperatorSettleLog();
                log.OperatorID = opt.OperatorName;
                log.DeptID = opt.DeptID;
                log.Dept = opt.Dept;
                log.SettleDateTime = DateTime.Now;
                if (station != null) log.StationID = station.StationName;
                //查询条件
                RecordSearchCondition recordCon = new RecordSearchCondition();
                recordCon.RecordDateTimeRange = new DateTimeRange(log.SettleFrom == null ? new DateTime(1753, 1, 1, 12, 0, 0) : log.SettleFrom.Value, log.SettleDateTime);
                recordCon.Operator = opt;
                recordCon.IsUnSettled = true;
                if (station != null) recordCon.StationID = station.StationName;
                //查询收费记录
                CardPaymentRecordBll paymentBll = new CardPaymentRecordBll(_RepoUri);
                List<CardPaymentInfo> paymentRecords = paymentBll.GetItems(recordCon).QueryObjects;
                log.PaymentRecords = paymentRecords;
                log.CashParkFact = CashParkFactSum(paymentRecords);
                log.CashOperatorCard = CashOperatorCardSum(paymentRecords);
                log.CashParkDiscount = CashParkDiscountSum(paymentRecords);
                log.NonCashParkFact = NoCashParkFactSum(paymentRecords);
                log.NonCashParkDiscount = NoCashParkDiscountSum(paymentRecords);
                log.CashOfPOS = CashPOSSum(paymentRecords);
                //查询卡片发行记录
                CardBll cbll = new CardBll(_RepoUri);
                List<CardReleaseRecord> cardReleaseRecords = cbll.GetCardReleaseRecords(recordCon).QueryObjects;
                log.ReleaseRecords = cardReleaseRecords;
                log.CashOfCard += CashCardReleaseSum(cardReleaseRecords);
                log.CashOfDeposit += CashDepositSum(cardReleaseRecords);
                log.NonCashOfDeposit += NonCashDepositSum(cardReleaseRecords);
                log.NonCashOfCard += NonCashCardReleaseSum(cardReleaseRecords);
                //查询卡片延期记录
                List<CardDeferRecord> cardDeferRecords = cbll.GetCardDeferRecords(recordCon).QueryObjects;
                log.DeferRecords = cardDeferRecords;
                log.CashOfCard += CashCardDeferSum(cardDeferRecords);
                log.NonCashOfCard += NonCashCardDeferSum(cardDeferRecords);
                //查询卡片充值记录
                List<CardChargeRecord> cardChargeRecords = cbll.GetCardChargeRecords(recordCon).QueryObjects;
                log.ChargeRecords = cardChargeRecords;
                log.CashOfCard += CashCardChargeSum(cardChargeRecords);
                log.NonCashOfCard += NonCashCardChargeSum(cardChargeRecords);
                //卡片挂失记录
                List<CardLostRestoreRecord> cardLostRecords = cbll.GetCardLostRestoreRecords(recordCon).QueryObjects;
                log.CardLostRecords = cardLostRecords;
                log.CashOfCardLost = CashCardLostSum(cardLostRecords);
                log.NonCashOfCardLost = NonCashCardLostSum(cardLostRecords);
                //查询卡片回收记录
                List<CardRecycleRecord> cardRecycleRecords = cbll.GetCardRecycleRecords(recordCon).QueryObjects;
                log.RecycleRecords = cardRecycleRecords;
                log.CashOfCardRecycle += CashCardRecycleSum(cardRecycleRecords);
                //查询缴费机退款记录
                List<APMRefundRecord> apmRefundRecords = cbll.GetAPMRefundRecords(recordCon).QueryObjects;
                log.APMRefundRecords = apmRefundRecords;
                log.CashOfRefund = CashAPMRefundSum(apmRefundRecords);
                //查询报警记录
                AlarmSearchCondition alarmCon = new AlarmSearchCondition();
                alarmCon.RecordDateTimeRange = new DateTimeRange(log.SettleFrom == null ? new DateTime(1753, 1, 1, 12, 0, 0) : log.SettleFrom.Value, log.SettleDateTime);
                alarmCon.Operator = opt;
                alarmCon.IsUnSettled = true;
                alarmCon.AlarmType = AlarmType.Opendoor;
                if (station != null) alarmCon.StationID = station.StationName;

                List<AlarmInfo> alarmReocrds = (new AlarmBll(_RepoUri)).GetAlarms(alarmCon).QueryObjects;
                log.OpenDoorCount = alarmReocrds.Count;
                log.AlarmRecords = alarmReocrds;
                //查询事件
                CardEventSearchCondition eventCon = new CardEventSearchCondition();
                eventCon.RecordDateTimeRange = new DateTimeRange(log.SettleFrom == null ? new DateTime(1753, 1, 1, 12, 0, 0) : log.SettleFrom.Value, log.SettleDateTime);
                eventCon.Operator = opt;
                eventCon.IsUnSettled = true;
                eventCon.OnlyExitEvent = true;
                eventCon.CardType = CardType.TempCard;  //只获取临时卡事件
                if (station != null) eventCon.StationID = station.StationName;

                List<CardEventRecord> handledEvents = (new CardEventBll(_RepoUri)).GetCardEvents(eventCon).QueryObjects;
                log.EventRecords = handledEvents;
                log.TempCardRecycle = TempCardRecycleSum(handledEvents);
                return log;
            }
            else
            {
                throw new InvalidOperationException(Resource1.OperatorSettleBLL_noOperator);
            }
        }

        /// <summary>
        /// 获取操作员最近一次的结算时间
        /// </summary>
        /// <param name="operatorID"></param>
        /// <returns></returns>
        public DateTime? GetLastSettleDateTime(string operatorID)
        {
            OperatorInfo operatorInfo = null;
            if (!string.IsNullOrEmpty(operatorID))
            {
                operatorInfo = new OperatorInfo();
                operatorInfo.OperatorID = operatorID;
                operatorInfo.OperatorName = operatorID;
            }

            RecordSearchCondition search = new RecordSearchCondition();
            search.Operator = operatorInfo;
            List<OperatorSettleLog> logs = provider.GetItems(search).QueryObjects;
            if (logs != null && logs.Count > 0)
            {
                DateTime lastSDatetime = logs.Max(p => p.SettleDateTime);
                return lastSDatetime;
            }
            return null;
        }

        /// <summary>
        /// 获取大于datetime的最近一次结算时间
        /// </summary>
        /// <param name="operatorInfo">操作员</param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public DateTime? GetRecentSettleDateTime(OperatorInfo operatorInfo, DateTime datetime)
        {
            RecordSearchCondition search = new RecordSearchCondition();
            search.RecordDateTimeRange = new DateTimeRange(datetime, DateTime.Now);
            search.Operator = operatorInfo;
            List<OperatorSettleLog> logs = provider.GetItems(search).QueryObjects;
            if (logs != null && logs.Count > 0)
            {
                DateTime log = logs.Min(l => l.SettleDateTime);
                return log;
            }
            return null;
        }

        /// <summary>
        /// 获取大于datetime的最近一次结算时间
        /// </summary>
        /// <param name="operatorID">操作员ID</param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public DateTime? GetRecentSettleDateTime(string operatorID, DateTime datetime)
        {
            OperatorInfo operatorInfo = null;
            if (!string.IsNullOrEmpty(operatorID))
            {
                operatorInfo = new OperatorInfo();
                operatorInfo.OperatorID = operatorID;
                operatorInfo.OperatorName = operatorID;
            }
            return GetRecentSettleDateTime(operatorInfo, datetime);
        }

        /// <summary>
        /// 获取 datetime的前一次结算时间
        /// </summary>
        /// <param name="operatorID"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public DateTime? GetPreSettleDateTime(string operatorID, DateTime datetime)
        {
            OperatorInfo operatorInfo = null;
            if (!string.IsNullOrEmpty(operatorID))
            {
                operatorInfo = new OperatorInfo();
                operatorInfo.OperatorID = operatorID;
                operatorInfo.OperatorName = operatorID;
            }

            RecordSearchCondition search = new RecordSearchCondition();
            search.RecordDateTimeRange = new DateTimeRange(DateTime.MinValue.AddYears(1900), datetime.AddSeconds(-1));
            search.Operator = operatorInfo;
            List<OperatorSettleLog> logs = provider.GetItems(search).QueryObjects;
            if (logs != null && logs.Count > 0)
            {
                DateTime log = logs.Max(l => l.SettleDateTime);
                return log;
            }
            return null;
        }

        #endregion
    }
}
