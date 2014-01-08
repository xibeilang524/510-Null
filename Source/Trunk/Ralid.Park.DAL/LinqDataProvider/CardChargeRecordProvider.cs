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
                RecordSearchCondition con = search as RecordSearchCondition;

                IQueryable<CardChargeRecord> result = parking.CardCharge;
                if (con.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.ChargeDateTime >= con.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.ChargeDateTime <= con.RecordDateTimeRange.End).AsQueryable();
                }
                if (con.Operator != null) result = result.Where(c => c.OperatorID == con.Operator.OperatorName).AsQueryable();
                if (con.StationID != null && con.StationID != "") result = result.Where(c => c.StationID == con.StationID).AsQueryable();
                if (!string.IsNullOrEmpty(con.CardID)) result = result.Where(c => c.CardID.Contains(con.CardID));
                if (!string.IsNullOrEmpty(con.CardCertificate)) result = result.Where(c => c.CardCertificate.Contains(con.CardCertificate));
                if (!string.IsNullOrEmpty(con.OwnerName)) result = result.Where(c => c.OwnerName.Contains(con.OwnerName));
                if (!string.IsNullOrEmpty(con.CarPlate)) result = result.Where(c => c.CarPlate.Contains(con.CarPlate));
                if (con.IsUnSettled != null)
                {
                    if (con.IsUnSettled.Value) result = result.Where(c => c.SettleDateTime == null);
                    else result = result.Where(c => c.SettleDateTime != null);
                }
                if (con.SettleDateTime != null) result = result.Where(c => c.SettleDateTime == con.SettleDateTime.Value);
                return result.ToList();
            }
            return new List<CardChargeRecord>();
        }
        #endregion
    }
}
