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
    /// <summary>
    /// 自助缴费机结账记录逻辑类
    /// </summary>
    public class APMChectOutRecordBll
    {
        
        #region 构造函数
        public APMChectOutRecordBll(string repoUri)
        {
            _Provider = ProviderFactory.Create<IAPMCheckOutRecordProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        IAPMCheckOutRecordProvider _Provider;
        #endregion

        #region 公共方法
        public QueryResultList<APMCheckOutRecord> GetItems(APMCheckOutRecordSearchCondition con)
        {
            return _Provider.GetItems(con);
        }

        public CommandResult Insert(APMCheckOutRecord info)
        {
            return _Provider.Insert(info);
        }

        public CommandResult Delete(APMCheckOutRecord info)
        {
            return _Provider.Delete(info);
        }
        #endregion
    }
}
