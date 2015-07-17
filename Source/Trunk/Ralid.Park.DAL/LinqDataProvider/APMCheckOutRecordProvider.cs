using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class APMCheckOutRecordProvider : ProviderBase<APMCheckOutRecord, int>, IAPMCheckOutRecordProvider
    {
        #region 构造函数
        public APMCheckOutRecordProvider()
        {
        }

        public APMCheckOutRecordProvider(string connStr)
            : base(connStr)
        {
        }
        #endregion

        #region 重写基类方法
        protected override APMCheckOutRecord GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.GetTable<APMCheckOutRecord>().SingleOrDefault(item => item.ID == id);
        }

        protected override List<APMCheckOutRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is APMCheckOutRecordSearchCondition)
            {
                IQueryable<APMCheckOutRecord> result = parking.GetTable<APMCheckOutRecord>();
                APMCheckOutRecordSearchCondition condition = search as APMCheckOutRecordSearchCondition;
                result = result.Where(item => item.CheckOutDateTime >= condition.RecordDateTimeRange.Begin &&
                    item.CheckOutDateTime <= condition.RecordDateTimeRange.End);
                if (!string.IsNullOrEmpty(condition.MID))
                {
                    result = result.Where(item => item.MID == condition.MID);
                }
                if (!string.IsNullOrEmpty(condition.APMOperator))
                {
                    result = result.Where(item => item.APMOperator == condition.APMOperator);
                }
                return result.ToList();
            }
            return new List<APMCheckOutRecord>();
        }
        #endregion
    }
}
