using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class RoadWayProvider : ProviderBase<RoadWayInfo, int>, IRoadWayProvider
    {
        #region 构造函数
        public RoadWayProvider()
        {
        }

        public RoadWayProvider(string connStr)
            : base(connStr)
        {

        }
        #endregion

        #region 重写基类方法
        protected override RoadWayInfo GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.GetTable<RoadWayInfo>().SingleOrDefault(item => item.RoadID == id);
        }
        #endregion
    }
}
