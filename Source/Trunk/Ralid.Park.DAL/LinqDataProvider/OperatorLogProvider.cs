using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class OperatorLogProvider : ProviderBase<OperatorSettleLog, DateTime>, IOperatorLogProvider
    {
        public OperatorLogProvider()
        {
        }

        public OperatorLogProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写基类方法
        protected override OperatorSettleLog GetingItemByID(DateTime id, ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<OperatorSettleLog>(o => o.Dept);
            parking.LoadOptions = opt;
            return parking.OperatorLog.SingleOrDefault(o => o.SettleDateTime == id);
        }

        protected override List<OperatorSettleLog> GetingAllItems(ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<OperatorSettleLog>(o => o.Dept);
            parking.LoadOptions = opt;
            return parking.OperatorLog.ToList();
        }

        protected override List<OperatorSettleLog> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition condition = search as RecordSearchCondition;
                DataLoadOptions opt = new DataLoadOptions();
                opt.LoadWith<OperatorSettleLog>(o => o.Dept);
                parking.LoadOptions = opt;
                IQueryable<OperatorSettleLog> result = parking.OperatorLog;
                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(o => o.SettleDateTime >= condition.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(o => o.SettleDateTime <= condition.RecordDateTimeRange.End).AsQueryable();
                }
                if (condition.Operator != null)
                {
                    result = result.Where(c => c.OperatorID == condition.Operator.OperatorName).AsQueryable();
                }
                if (condition.Dept != null)
                {
                    result = result.Where(c => c.DeptID == condition.Dept.DeptID).AsQueryable();
                }
                if (condition.StationID != null && condition.StationID != "")
                {
                    result = result.Where(o => o.StationID == condition.StationID).AsQueryable();
                }
                return result.ToList();
            }
            return new List<OperatorSettleLog>();
        }

        protected override void InsertingItem(OperatorSettleLog info, ParkDataContext parking)
        {
            if (info.Dept != null) parking.GetTable<DeptInfo>().Attach(info.Dept);//不需要在插入操作员结算记录时同时插入操作员的部门
            base.InsertingItem(info, parking);
        }
        #endregion
    }
}
