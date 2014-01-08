using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model ;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class CardChargeRecordProvider : ProviderBase<CardChargeRecord, string>, ICardChargeRecordProvider
    {
        public CardChargeRecordProvider()
        {
        }

        public CardChargeRecordProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写基类方法
        protected override List<CardChargeRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition condition = search as RecordSearchCondition;

                IQueryable<CardChargeRecord> result = parking.CardCharge;
                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.ChargeDateTime >= condition.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.ChargeDateTime <= condition.RecordDateTimeRange.End).AsQueryable();
                }
                if (condition.Operator != null)
                {
                    result = result.Where(c => c.OperatorID == condition.Operator.OperatorID).AsQueryable();
                }
                if (condition.StationID != null && condition.StationID != "")
                {
                    result = result.Where(c => c.StationID == condition.StationID).AsQueryable();
                }
                if (condition.CardID != null && condition.CardID != "")
                {
                    result = result.Where(c => c.CardID == condition.CardID).AsQueryable();
                }
                return result.ToList();
            }
            return new List<CardChargeRecord>();
        }
        #endregion
    }
}
