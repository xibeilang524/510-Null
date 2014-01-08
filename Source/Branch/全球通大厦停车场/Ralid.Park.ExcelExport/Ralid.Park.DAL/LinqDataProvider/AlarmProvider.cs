using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class AlarmProvider : ProviderBase<AlarmInfo, int>, IAlarmProvider
    {
        public AlarmProvider()
        {
        }

        public AlarmProvider(string connStr)
            : base(connStr)
        {

        }

        #region 重写基类方法
        protected override List<AlarmInfo> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                RecordSearchCondition condition = search as RecordSearchCondition;

                IQueryable<AlarmInfo> result = parking.Alarm;
                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(c => c.AlarmDateTime >= condition.RecordDateTimeRange.Begin).AsQueryable();
                    result = result.Where(c => c.AlarmDateTime <= condition.RecordDateTimeRange.End).AsQueryable();
                }
                if (condition.Operator != null)
                {
                    result = result.Where(c => c.OperatorID == condition.Operator.OperatorID).AsQueryable();
                }
                if (condition is AlarmSearchCondition)
                {
                    AlarmSearchCondition s = condition as AlarmSearchCondition;
                    if (!string.IsNullOrEmpty(s.AlarmSource))
                    {
                        result = result.Where(c => c.AlarmSource.Contains(s.AlarmSource)).AsQueryable();
                    }
                    if (!string.IsNullOrEmpty(s.AlarmType))
                    {
                        result = result.Where(c => c.AlarmType.Contains(s.AlarmType)).AsQueryable();
                    }
                }
                return result.ToList();
            }
            return new List<AlarmInfo>();
        }
        #endregion
    }
}
