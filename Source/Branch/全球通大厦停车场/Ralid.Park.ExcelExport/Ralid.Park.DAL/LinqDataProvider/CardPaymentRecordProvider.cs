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

            if (search is CardPaymentRecordSearchCondition)
            {
                CardPaymentRecordSearchCondition condition = search as CardPaymentRecordSearchCondition;

                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.ChargeDateTime >= condition.RecordDateTimeRange.Begin);
                    result = result.Where(c => c.ChargeDateTime <= condition.RecordDateTimeRange.End);
                }
                if (!string.IsNullOrEmpty(condition.CardID))
                {
                    result = result.Where(c => c.CardID == condition.CardID);
                }
                if (condition.EnterDateTime != null)
                {
                    result = result.Where(c => c.EnterDateTime == condition.EnterDateTime.Value);
                }
                if (condition.PaymentMode != null)
                {
                    result = result.Where(c => c.PaymentMode == condition.PaymentMode.Value);
                }
                if (condition.Operator != null)
                {
                    result = result.Where(c => c.OperatorID == condition.Operator.OperatorID);
                }
                if (!string.IsNullOrEmpty(condition.StationID))
                {
                    result = result.Where(c => c.StationID == condition.StationID);
                }
                if (!string.IsNullOrEmpty(condition.CarPalte))
                {
                    result = result.Where(c => c.CarPlate.Contains(condition.CarPalte));
                }
                if (condition.PaymentMode != null)
                {
                    result = result.Where(c => c.PaymentMode == condition.PaymentMode);
                }
                items = result.ToList();
                if (condition.CardType != null)
                {
                    items = items.Where(c => c.CardType == condition.CardType).ToList();
                }
            }
            else if (search is RecordSearchCondition)
            {
                RecordSearchCondition condition = search as RecordSearchCondition;
                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.ChargeDateTime >= condition.RecordDateTimeRange.Begin);
                    result = result.Where(c => c.ChargeDateTime <= condition.RecordDateTimeRange.End);
                }
                if (!string.IsNullOrEmpty(condition.CardID))
                {
                    result = result.Where(c => c.CardID == condition.CardID);
                }
                if (condition.PaymentMode != null)
                {
                    result = result.Where(c => c.PaymentMode == condition.PaymentMode.Value);
                }
                if (condition.Operator != null)
                {
                    result = result.Where(c => c.OperatorID == condition.Operator.OperatorID);
                }
                if (!string.IsNullOrEmpty(condition.StationID))
                {
                    result = result.Where(c => c.StationID == condition.StationID);
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