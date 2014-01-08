using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class YangChenTongLogProvider : ProviderBase<YangChenTongLog, int>, IYangChenTongLogProvider
    {
        #region 构造函数
        public YangChenTongLogProvider()
        {
        }

        public YangChenTongLogProvider(string connStr)
            : base(connStr)
        {
        }
        #endregion

        #region 重写基类方法
        protected override YangChenTongLog GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.GetTable<YangChenTongLog>().SingleOrDefault(item => item.LogID == id);
        }

        protected override List<YangChenTongLog> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is RecordSearchCondition)
            {
                IQueryable<YangChenTongLog> result = parking.GetTable<YangChenTongLog>();
                RecordSearchCondition condition = search as RecordSearchCondition;
                result = result.Where(item => item.LogDateTime >= condition.RecordDateTimeRange.Begin &&
                    item.LogDateTime <= condition.RecordDateTimeRange.End);
                if (!string.IsNullOrEmpty(condition.CardID))
                {
                    result = result.Where(item => item.CardID == condition.CardID);
                }
                return result.ToList();
            }
            return new List<YangChenTongLog>();
        }
        #endregion
    }
}

