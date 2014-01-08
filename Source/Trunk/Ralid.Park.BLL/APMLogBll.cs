using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Model ;
using Ralid.Park .BusinessModel .Result ;
using Ralid.Park .BusinessModel .SearchCondition;
using Ralid.Park .DAL .IDAL ;

namespace Ralid.Park.BLL
{
    public class APMLogBll
    {
        #region 构造函数
        public APMLogBll(string repoUri)
        {
            _Provider = ProviderFactory.Create<IAPMLogProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        IAPMLogProvider _Provider;
        #endregion

        #region 公共方法
        public QueryResultList<APMLog> GetItems(APMLogSearchCondition con)
        {
            return _Provider.GetItems(con);
        }

        public CommandResult Insert(APMLog info)
        {
            return _Provider.Insert(info);
        }

        public CommandResult Delete(APMLog info)
        {
            return _Provider.Delete(info);
        }
        #endregion
    }
}
