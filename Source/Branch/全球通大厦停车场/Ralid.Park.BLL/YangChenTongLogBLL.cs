using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.BLL
{
    public class YangChenTongLogBll
    {
        #region 构造函数
        public YangChenTongLogBll(string repoUri)
        {
            _Provider = ProviderFactory.Create<IYangChenTongLogProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        IYangChenTongLogProvider _Provider;
        #endregion

        #region 公共方法
        public QueryResultList<YangChenTongLog> GetItems(RecordSearchCondition con)
        {
            return _Provider.GetItems(con);
        }

        public CommandResult Insert(YangChenTongLog info)
        {
            return _Provider.Insert(info);
        }

        public CommandResult Delete(YangChenTongLog info)
        {
            return _Provider.Delete(info);
        }
        #endregion
    }
}
