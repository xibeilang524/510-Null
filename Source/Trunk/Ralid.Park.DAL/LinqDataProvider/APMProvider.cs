using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class APMProvider : ProviderBase<APM, int>, IAPMProvider
    {
        #region 构造函数
        public APMProvider()
        {
        }

        public APMProvider(string connStr)
            : base(connStr)
        {

        }
        #endregion

        #region 重写基类方法
        protected override APM GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.GetTable<APM>().SingleOrDefault(item => item.ID == id);
        }

        protected override List<APM> GetingItems(ParkDataContext parking, BusinessModel.SearchCondition.SearchCondition search)
        {
            return parking.GetTable<APM>().ToList();
        }
        #endregion
    }
}
