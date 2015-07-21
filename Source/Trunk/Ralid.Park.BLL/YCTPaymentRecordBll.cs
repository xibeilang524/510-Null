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
    public class YCTPaymentRecordBll
    {
        #region 构造函数
        public YCTPaymentRecordBll(string repoUri)
        {
            _Provider = ProviderFactory.Create<IYCTPaymentRecordProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        IYCTPaymentRecordProvider _Provider;
        #endregion

        #region 公共方法
        public QueryResult<YCTPaymentRecord> GetByID(int id)
        {
            return _Provider.GetByID(id);
        }

        public QueryResultList<YCTPaymentRecord> GetItems(SearchCondition con)
        {
            return _Provider.GetItems(con);
        }

        public CommandResult Insert(YCTPaymentRecord info)
        {
            return _Provider.Insert(info);
        }

        public CommandResult Update(YCTPaymentRecord newVal)
        {
            YCTPaymentRecord original = _Provider.GetByID(newVal.ID).QueryObject;
            if (original != null)
            {
                return _Provider.Update(newVal, original);
            }
            else
            {
                return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
            }
        }
		
		public CommandResult Update(YCTPaymentRecord newVal,YCTPaymentRecord oldVal)
        {
            return _Provider.Update(newVal,oldVal);
        }

        public CommandResult Delete(YCTPaymentRecord info)
        {
            return _Provider.Delete(info);
        }
        #endregion
    }
}
