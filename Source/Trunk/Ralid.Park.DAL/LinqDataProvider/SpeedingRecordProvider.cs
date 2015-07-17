using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class SpeedingRecordProvider : ProviderBase<SpeedingRecord, Guid>, ISpeedingRecordProvider
    {
        public SpeedingRecordProvider()
        { 
        }

        public SpeedingRecordProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override SpeedingRecord GetingItemByID(Guid id, ParkDataContext parking)
        {
            return parking.GetTable<SpeedingRecord>().SingleOrDefault(r => r.SpeedingID == id);
        }

        protected override List<SpeedingRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition con = search as RecordSearchCondition;
                IQueryable<SpeedingRecord> result = parking.GetTable<SpeedingRecord>().AsQueryable();
                if (con.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.SpeedingDateTime >= con.RecordDateTimeRange.Begin);
                    result = result.Where(c => c.SpeedingDateTime <= con.RecordDateTimeRange.End);
                }
                if (!string.IsNullOrEmpty(con.CarPlate))
                {
                    //由于超速记录是以车牌号码关联的，所以这里的车牌必须一致
                    result = result.Where(c => c.PlateNumber == con.CarPlate);
                }
                result = result.OrderByDescending(c => c.SpeedingDateTime);
                return result.ToList();
            }
            return new List<SpeedingRecord>();
        }
        #endregion
    }
}
