using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class CardDeleteRecordProvider : ProviderBase<CardDeleteRecord, DateTime>, ICardDeleteRecordProvider
    {
        #region 构造函数
        public CardDeleteRecordProvider(string conStr)
            : base(conStr)
        {
        }
        #endregion

        #region 重写基类方法
        protected override List<CardDeleteRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition con = search as RecordSearchCondition;
                IQueryable<CardDeleteRecord> result = parking.GetTable<CardDeleteRecord>();
                if (con.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.DeleteDateTime >= con.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.DeleteDateTime <= con.RecordDateTimeRange.End).AsQueryable();
                }
                if (con.Operator != null) result = result.Where(c => c.OperatorID == con.Operator.OperatorName);
                if (con.StationID != null && con.StationID != "") result = result.Where(c => c.StationID == con.StationID);
                if (!string.IsNullOrEmpty(con.CardID)) result = result.Where(c => c.CardID.Contains(con.CardID));
                if (!string.IsNullOrEmpty(con.CardCertificate)) result = result.Where(c => c.CardCertificate.Contains(con.CardCertificate));
                if (!string.IsNullOrEmpty(con.OwnerName)) result = result.Where(c => c.OwnerName.Contains(con.OwnerName));
                if (!string.IsNullOrEmpty(con.CarPlate)) result = result.Where(c => c.CarPlate.Contains(con.CarPlate));
                List<CardDeleteRecord> items = result.ToList();
                if (con.CardType != null) items = items.Where(c => c.CardType == con.CardType).ToList();
                return items;
            }
            return new List<CardDeleteRecord>();
        }
        #endregion
    }
}
