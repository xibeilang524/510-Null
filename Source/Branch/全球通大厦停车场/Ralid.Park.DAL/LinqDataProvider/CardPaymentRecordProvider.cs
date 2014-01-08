using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class CardPaymentRecordProvider : ProviderBase<CardPaymentInfo, int>, ICardPaymentRecordProvider
    {
        public CardPaymentRecordProvider()
        {
        }

        public CardPaymentRecordProvider(string conStr)
            : base(conStr)
        {
        }

        #region 重写基类方法
        protected override CardPaymentInfo GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.GetTable<CardPaymentInfo>().SingleOrDefault(c => c.ID == id);
        }

        protected override List<CardPaymentInfo> GetingItems(ParkDataContext parking, Ralid.Park.BusinessModel.SearchCondition.SearchCondition search)
        {
            List<CardPaymentInfo> items = null;
            IQueryable<CardPaymentInfo> result = parking.GetTable<CardPaymentInfo>();
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition condition = search as RecordSearchCondition;
                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.ChargeDateTime >= condition.RecordDateTimeRange.Begin);
                    result = result.Where(c => c.ChargeDateTime <= condition.RecordDateTimeRange.End);
                }
                if (!string.IsNullOrEmpty(condition.CardID)) result = result.Where(c => c.CardID == condition.CardID);
                if (condition.PaymentMode != null) result = result.Where(c => c.PaymentMode == condition.PaymentMode.Value);
                if (condition.Operator != null) result = result.Where(c => c.OperatorID == condition.Operator.OperatorName);
                if (!string.IsNullOrEmpty(condition.StationID)) result = result.Where(c => c.StationID == condition.StationID);
                if (condition.IsUnSettled != null)
                {
                    if (condition.IsUnSettled.Value) result = result.Where(c => c.SettleDateTime == null);
                    else result = result.Where(c => c.SettleDateTime != null);
                }
                if (condition.SettleDateTime != null) result = result.Where(c => c.SettleDateTime == condition.SettleDateTime.Value);
                if (!string.IsNullOrEmpty(condition.CarPlate)) result = result.Where(c => c.CarPlate.Contains(condition.CarPlate));
                if (condition.CarType != null) result = result.Where(c => c.CarType == condition.CarType.Value);
                if (!string.IsNullOrEmpty(condition.OwnerName)) result = result.Where(c => c.OwnerName.Contains(condition.OwnerName));
                if (!string.IsNullOrEmpty(condition.CardCertificate)) result = result.Where(c => c.CardCertificate.Contains(condition.CardCertificate));
                if (search is CardPaymentRecordSearchCondition)
                {
                    CardPaymentRecordSearchCondition condition1 = search as CardPaymentRecordSearchCondition;
                    if (condition1.EnterDateTime != null) result = result.Where(c => c.EnterDateTime == condition1.EnterDateTime.Value);
                    if (condition1.IsCenterCharge != null && condition1.IsCenterCharge.Value) result = result.Where(c => c.IsCenterCharge == true);
                    if (condition1.IsCenterCharge != null && !condition1.IsCenterCharge.Value) result = result.Where(c => c.IsCenterCharge == false);
                    if (condition1.ChargeDateTime != null) result = result.Where(c => c.ChargeDateTime == condition1.ChargeDateTime.Value);
                    if (condition1.PaymentCode != null) result = result.Where(c => c.PaymentCode == condition1.PaymentCode.Value);
                    if (!string.IsNullOrEmpty(condition1.OperatorCardID)) result = result.Where(c => c.OperatorCardID == condition1.OperatorCardID);
                }
                items = result.ToList();
                if (condition.CardType != null)
                {
                    items = items.Where(c => c.CardType == condition.CardType).ToList();
                }
            }
            else
            {
                items = new List<CardPaymentInfo>();
            }
            return items;
        }
        #endregion
    }
}