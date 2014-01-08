using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 表示停车收费员停车费用结算记录
    /// </summary>
    public class OperatorSettleLog
    {
        #region 公共属性
        /// <summary>
        /// 获取或设置下班时间
        /// </summary>
        public DateTime SettleDateTime { get; set; }
        /// <summary>
        /// 获取或设置操作员ID
        /// </summary>
        public string OperatorID { get; set; }
        /// <summary>
        /// 获取或设置上班时间
        /// </summary>
        public DateTime? SettleFrom { get; set; }
        /// <summary>
        /// 获取或设置上班时的工作站名
        /// </summary>
        public string StationID { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期间，实际使用电脑按现金收取的时租卡费用
        /// </summary>
        public decimal CashParkFact { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期间，实际使用操作员卡（收费功能卡）按现金收取的时租卡费用
        /// </summary>
        public decimal CashOperatorCard { get; set; }
        /// <summary>
        ///获取或设置操作员当班期间，以现金收取的时租卡收入部分
        /// </summary>
        public decimal CashParkDiscount { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期间,现金收取的卡片发行,充值,延期等的费用
        /// </summary>
        public decimal CashOfCard { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期间现金收取的卡片押金
        /// </summary>
        public decimal CashOfDeposit { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期间卡片回收返还的现金
        /// </summary>
        public decimal CashOfCardRecycle { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期间，现金收取的卡片挂失工本费
        /// </summary>
        public decimal CashOfCardLost { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期间，实际上缴的现金
        /// </summary>
        public decimal? HandInCash { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期间,实际收取非现金的时租卡费用
        /// </summary>
        public decimal NonCashParkFact { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期间,以非现金收取的时租卡收入部分
        /// </summary>
        public decimal NonCashParkDiscount { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期间,非现金收取的卡片发行,充值,延期等的费用
        /// </summary>
        public decimal NonCashOfCard { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期间,非现金收取的卡片押金
        /// </summary>
        public decimal NonCashOfDeposit { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期间，非现金收取的卡片挂失工本费
        /// </summary>
        public decimal NonCashOfCardLost { get; set; }
        /// <summary>
        /// 获取或设置操作员当班时收回的临时卡片数量 
        /// </summary>
        public int TempCardRecycle { get; set; }
        /// <summary>
        /// 获取或设置操作员当班期通过软件抬闸的次数
        /// </summary>
        public int OpenDoorCount { get; set; }
        #endregion

        #region 只读属性
        /// <summary>
        /// 获取操作员当班期间收到的所有现金数量
        /// </summary>
        public decimal TotalCash
        {
            get
            {
                return CashParkFact + CashOperatorCard + CashOfCard + CashOfDeposit + CashOfCardLost - CashOfCardRecycle;
            }
        }
        /// <summary>
        /// 获取操作员当班期间收取的所有非现金总额
        /// </summary>
        public decimal TotalNonCash
        {
            get
            {
                return NonCashParkFact + NonCashOfCard + NonCashOfDeposit + NonCashOfCardLost;
            }
        }
        /// <summary>
        /// 获取时租卡收入
        /// </summary>
        public decimal ParkRevenue
        {
            get
            {
                return CashParkFact + NonCashParkFact;
            }
        }
        /// <summary>
        /// 获取卡片延期,充值等收入
        /// </summary>
        public decimal CardRevenue
        {
            get
            {
                return CashOfCard + NonCashOfCard;
            }
        }
        /// <summary>
        /// 获取押金收入
        /// </summary>
        public decimal DepositRevenue
        {
            get
            {
                return CashOfDeposit + NonCashOfDeposit - CashOfCardRecycle;
            }
        }
        /// <summary>
        /// 获取折扣
        /// </summary>
        public decimal Discount
        {
            get
            {
                return CashParkDiscount + NonCashParkDiscount;
            }
        }
        /// <summary>
        /// 收入总额
        /// </summary>
        public decimal TotalRevenue
        {
            get
            {
                return CardRevenue + ParkRevenue + DepositRevenue;
            }
        }
        /// <summary>
        /// 获取操作员上交现金与账上现金的差额
        /// </summary>
        public decimal? CashDiffrence
        {
            get
            {
                if (HandInCash == null) return null;
                return HandInCash.Value - TotalCash;
            }
        }
        /// <summary>
        /// 获取实收停车费用现金
        /// </summary>
        public decimal CashParkFee
        {
            get
            {
                return CashParkFact + CashOperatorCard;
            }
        }
        #endregion

        #region 只有生成结算记录时才有的属性
        /// <summary>
        /// 获取或设置结算的所有收费记录
        /// </summary>
        public List<CardPaymentInfo> PaymentRecords { get; set; }

        public List<CardReleaseRecord> ReleaseRecords { get; set; }

        public List<CardChargeRecord> ChargeRecords { get; set; }

        public List<CardRecycleRecord> RecycleRecords { get; set; }

        public List<CardLostRestoreRecord> CardLostRecords { get; set; }

        public List<CardDeferRecord> DeferRecords { get; set; }

        public List<CardEventRecord> EventRecords { get; set; }

        public List<AlarmInfo> AlarmRecords { get; set; }
        #endregion
    }
}
