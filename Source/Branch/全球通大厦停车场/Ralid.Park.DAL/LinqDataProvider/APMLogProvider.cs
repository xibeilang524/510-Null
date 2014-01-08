using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class APMLogProvider : ProviderBase<APMLog, int>, IAPMLogProvider
    {
        #region 构造函数
        public APMLogProvider()
        {
        }

        public APMLogProvider(string connStr)
            : base(connStr)
        {
        }
        #endregion

        #region 重写基类方法
        protected override APMLog GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.GetTable<APMLog>().SingleOrDefault(item => item.ID == id);
        }

        protected override List<APMLog> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is APMLogSearchCondition)
            {
                IQueryable<APMLog> result = parking.GetTable<APMLog>();
                APMLogSearchCondition condition = search as APMLogSearchCondition;
                result = result.Where(item => item.LogDateTime >= condition.RecordDateTimeRange.Begin &&
                    item.LogDateTime <= condition.RecordDateTimeRange.End);
                if (!string.IsNullOrEmpty(condition.SerialNum))
                {
                    result = result.Where(item => item.SerialNumber == condition.SerialNum);
                }
                if (!string.IsNullOrEmpty(condition.MID))
                {
                    result = result.Where(item => item.MID == condition.MID);
                }
                if (!string.IsNullOrEmpty(condition.CardID))
                {
                    result = result.Where(item => item.CardID == condition.CardID);
                }
                if (!string.IsNullOrEmpty(condition.Description))
                {
                    result = result.Where(item => item.Description.Contains(condition.Description));
                }
                if (condition.Types != null && condition.Types.Count > 0)
                {
                    result = result.Where(item => condition.Types.Contains(item.LogType));
                }
                return result.ToList();
            }
            return new List<APMLog>();
        }
        #endregion
    }
}
