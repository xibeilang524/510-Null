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
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition condition = search as RecordSearchCondition;
                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.EventDateTime >= condition.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.EventDateTime <= condition.RecordDateTimeRange.End).AsQueryable();
                }
                if (!string.IsNullOrEmpty(condition.CardID)) result = result.Where(c => c.CardID == condition.CardID);
                if (condition.Operator != null) result = result.Where(c => c.OperatorID == condition.Operator.OperatorName);
                if (!string.IsNullOrEmpty(condition.StationID)) result = result.Where(c => c.StationID == condition.StationID);
                if (!string.IsNullOrEmpty(condition.OwnerName)) result = result.Where(c => c.OwnerName.Contains(condition.OwnerName));
                if (!string.IsNullOrEmpty(condition.Department)) result = result.Where(c => c.Department == condition.Department);
                if (condition.IsUnSettled != null)
                {
                    if (condition.IsUnSettled.Value) result = result.Where(c => c.SettleDateTime == null);
                    else result = result.Where(c => c.SettleDateTime != null);
                }
                if (condition.SettleDateTime != null) result = result.Where(c => c.SettleDateTime == condition.SettleDateTime.Value);
                if (condition.CarType != null) result = result.Where(c => c.CarType == condition.CarType);
                if (!string.IsNullOrEmpty(condition.CardCertificate)) result = result.Where(c => c.CardCertificate.Contains(condition.CardCertificate));
                if (condition.UpdateFlag != null) result = result.Where(c => c.UpdateFlag == condition.UpdateFlag);
                if (search is CardEventSearchCondition)
                {
                    CardEventSearchCondition s = search as CardEventSearchCondition;
                    if (s.CarType != null) result = result.Where(item => item.CarType == s.CarType);
                    if (!string.IsNullOrEmpty(s.CarPlate)) result = result.Where(c => c.CarPlate.Contains(s.CarPlate));
                    if (s.OnlyExitEvent) result = result.Where(item => item.IsExitEvent == true);
                    if (s.OnlyEnterEvent) result = result.Where(item => item.IsExitEvent == false);
                }
                items = result.ToList();
                if (condition.CardType != null)
                {
                    items = items.Where(c => c.CardType == condition.CardType).ToList();
                }
                if (search is CardEventSearchCondition)
                {
                    CardEventSearchCondition s = search as CardEventSearchCondition;
                    if (s.Source != null && s.Source.Count > 0)
                    {
                        items = (from c in items join e in s.Source on c.EntranceID equals e.EntranceID select c).ToList();
                    }
                }
            }
            return items;
        }
        #endregion

        public void DeleteAllCardEventBefore(DateTime eventDatetime)
        {
            try
            {
                ParkDataContext parking = ParkDataContextFactory.CreateParking(base.ConnectStr);
                if (parking != null)
                {
                    string cmd = "delete CardEvent where EventDateTime < '" + eventDatetime.ToString("yyyy-MM-dd") + "'";
                    parking.CommandTimeout = 5 * 60 * 60 * 1000;
                    parking.ExecuteCommand(cmd);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
    }
}
