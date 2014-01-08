using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class CardEventProvider : ProviderBase<CardEventRecord, RecordID>, ICardEventProvider
    {
        public CardEventProvider()
        {
        }

        public CardEventProvider(string connStr)
            : base(connStr)
        {
        }


        #region 重写基类方法
        protected override CardEventRecord GetingItemByID(RecordID id, ParkDataContext parking)
        {
            return parking.CardEvent.SingleOrDefault(c => c.CardID == id.CardID && c.EventDateTime == id.RecordDateTime);
        }

        protected override List<CardEventRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            List<CardEventRecord> items = new List<CardEventRecord>();
            IQueryable<CardEventRecord> result = parking.CardEvent;
            if (search is CardEventSearchCondition)
            {
                CardEventSearchCondition s = search as CardEventSearchCondition;
                if (s.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.EventDateTime >= s.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.EventDateTime <= s.RecordDateTimeRange.End).AsQueryable();
                }
                if (!string.IsNullOrEmpty(s.CardID))
                {
                    result = result.Where(c => c.CardID == s.CardID);
                }
                if (s.Operator != null)
                {
                    result = result.Where(c => c.OperatorID == s.Operator.OperatorID);
                }
                if (!string.IsNullOrEmpty(s.StationID))
                {
                    result = result.Where(c => c.StationID == s.StationID);
                }
                if (!string.IsNullOrEmpty(s.CarPlate))
                {
                    result = result.Where(c => c.CarPlate.Contains(s.CarPlate));
                }
                if (s.OnlyExitEvent)
                {
                    result = result.Where(c => c.IsExitEvent ==true);
                }
                items = result.ToList();
                if (s.CardType != null)
                {
                    items = items.Where(c => c.CardType == s.CardType).ToList();
                }
                if (s.Source != null && s.Source.Count > 0)
                {
                    items = (from c in items
                             join e in s.Source
                             on c.EntranceID equals e.EntranceID 
                             select c).ToList();
                }
            }
            else if (search is RecordSearchCondition)
            {
                RecordSearchCondition s = search as RecordSearchCondition;
                if (s.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.EventDateTime >= s.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.EventDateTime <= s.RecordDateTimeRange.End).AsQueryable();
                }
                if (!string.IsNullOrEmpty(s.CardID))
                {
                    result = result.Where(c => c.CardID == s.CardID);
                }
                if (s.Operator != null)
                {
                    result = result.Where(c => c.OperatorID == s.Operator.OperatorID);
                }
                if (!string.IsNullOrEmpty(s.StationID))
                {
                    result = result.Where(c => c.StationID == s.StationID);
                }
                items = result.ToList();
                if (s.CardType != null)
                {
                    items = items.Where(c => c.CardType == s.CardType).ToList();
                }
            }

            return items;
        }
        #endregion
    }
}
