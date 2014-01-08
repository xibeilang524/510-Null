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
                    result = result.Where(c => c.AlarmDateTime >= condition.RecordDateTimeRange.Begin);
                    result = result.Where(c => c.AlarmDateTime <= condition.RecordDateTimeRange.End);
                }
                if (condition.Operator != null) result = result.Where(c => c.OperatorID == condition.Operator.OperatorName);
                if (condition.IsUnSettled != null)
                {
                    if (condition.IsUnSettled.Value) result = result.Where(c => c.SettleDateTime == null);
                    else result = result.Where(c => c.SettleDateTime != null);
                }
                if (condition.SettleDateTime != null) result = result.Where(c => c.SettleDateTime == condition.SettleDateTime.Value);
                if (condition is AlarmSearchCondition)
                {
                    AlarmSearchCondition s = condition as AlarmSearchCondition;
                    if (!string.IsNullOrEmpty(s.AlarmSource))
                    {
                        result = result.Where(c => c.AlarmSource.Contains(s.AlarmSource));
                    }
                    if (s.AlarmType != null)
                    {
                        result = result.Where(c => c.AlarmType == s.AlarmType);
                    }
                }
                return result.ToList();
            }
            return new List<AlarmInfo>();
        }
        #endregion
    }
}
