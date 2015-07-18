using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;

namespace Ralid.Park.BLL
{
    public class YCTBlacklistBll
    {
        #region 构造函数
        public YCTBlacklistBll(string repoUri)
        {
            _Provider = ProviderFactory.Create<IYCTBlacklistProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        IYCTBlacklistProvider _Provider;
        #endregion

        #region 公共方法
        public QueryResult<YCTBlacklist> GetByID(string id)
        {
            return _Provider.GetByID(id);
        }

        public QueryResultList<YCTBlacklist> GetItems(SearchCondition con)
        {
            return _Provider.GetItems(con);
        }

        public CommandResult Insert(YCTBlacklist info)
        {
            return _Provider.Insert(info);
        }

        public CommandResult Update(YCTBlacklist newVal)
        {
            YCTBlacklist original = _Provider.GetByID(newVal.ID).QueryObject;
            if (original != null)
            {
                return _Provider.Update(newVal, original);
            }
            else
            {
                return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
            }
        }

        public CommandResult Delete(YCTBlacklist info)
        {
            return _Provider.Delete(info);
        }
        #endregion
    }
}
