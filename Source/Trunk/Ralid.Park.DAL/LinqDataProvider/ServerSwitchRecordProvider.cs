using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class ServerSwitchRecordProvider : ProviderBase<ServerSwitchRecord, int>, IServerSwitchRecordProvider
    {
        #region 构造函数
        public ServerSwitchRecordProvider()
        { 
        }
        public ServerSwitchRecordProvider(string connStr)
            : base(connStr)
        { 
        }
        #endregion

        #region 重写基类方法
        protected override ServerSwitchRecord GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.ServerSwitchRecord.SingleOrDefault(s => s.RecordID == id);
        }

        protected override List<ServerSwitchRecord> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is ServerSwitchRecordSearchCondition)
            {
                ServerSwitchRecordSearchCondition condition = search as ServerSwitchRecordSearchCondition;
                IQueryable<ServerSwitchRecord> result = parking.ServerSwitchRecord;
                if (condition.ParkID > 0)
                {
                    result = result.Where(s => s.ParkID == condition.ParkID);
                }
                if (condition.RecordDateTimeRange != null)
                {
                    result = result.Where(s => s.SwitchDateTime >= condition.RecordDateTimeRange.Begin);
                    result = result.Where(s => s.SwitchDateTime <= condition.RecordDateTimeRange.End);
                }
                if (condition.SMSStatus.HasValue)
                {
                    result = result.Where(s => s.SMSStatus == condition.SMSStatus.Value);
                }

                return result.ToList();
            }
            return new List<ServerSwitchRecord>();
        }
        #endregion
    }
}
