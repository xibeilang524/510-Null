using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;


namespace Ralid.Park.DAL.LinqDataProvider
{
    public class SpeedingLogProvider : ProviderBase<SpeedingLog, Guid>, ISpeedingLogProvider
    {
        public SpeedingLogProvider()
        { 
        }

        public SpeedingLogProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override SpeedingLog GetingItemByID(Guid id, ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<SpeedingLog>(l => l.DealOperator);
            parking.LoadOptions = opt;
            return parking.GetTable<SpeedingLog>().SingleOrDefault(r => r.SpeedingID == id);
        }

        protected override List<SpeedingLog> GetingAllItems(ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<SpeedingLog>(l => l.DealOperator);
            parking.LoadOptions = opt;
            return parking.GetTable<SpeedingLog>().ToList();
        }

        protected override List<SpeedingLog> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition con = search as RecordSearchCondition;
                DataLoadOptions opt = new DataLoadOptions();
                opt.LoadWith<SpeedingLog>(l => l.DealOperator);
                parking.LoadOptions = opt;
                IQueryable<SpeedingLog> result = parking.GetTable<SpeedingLog>().AsQueryable();
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
            return new List<SpeedingLog>();
        }

        protected override void InsertingItem(SpeedingLog info, ParkDataContext parking)
        {
            if (info.DealOperator != null) parking.GetTable<OperatorInfo>().Attach(info.DealOperator);//不需要在插入已处理违章记录时同时插入处理的操作员
            base.InsertingItem(info, parking);
        }
        #endregion
    }
}
