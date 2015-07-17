using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Resouce;

namespace Ralid.Park.BLL
{
    public class PREBusinessesBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public PREBusinessesBll(string repoUri)
        {
            _RepoUri = repoUri;
            provider = ProviderFactory.Create<IPREBusinessesProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private IPREBusinessesProvider provider;
        private string _RepoUri;
        #endregion

        #region 公共方法
        public QueryResult<PREBusinesses> GetBusinessesByID(string busID)
        {
            return provider.GetByID(busID);
        }

        public QueryResultList<PREBusinesses> GetBusinesses(PreferentialReportSearchCondition search)
        {
            return provider.GetItems(search);
        }

        public QueryResultList<PREBusinesses> GetAllBusinesses()
        {
            return provider.GetAll();
        }

        public CommandResult Update(PREBusinesses newVal)
        {
            PREBusinesses original = GetBusinessesByID(newVal.BusinessesID.ToString()).QueryObject;
            if (original != null)
            {
                return provider.Update(newVal, original);
            }
            else
            {
                return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
            }
        }

        public CommandResult Insert(PREBusinesses info)
        {
            return provider.Insert(info);
        }

        public CommandResult Delete(PREBusinesses info)
        {
            return provider.Delete(info);
        }

        public CommandResult DeleteAllItems()
        {
            return provider.DeleteAllItems();
        }

        public CommandResult UpdateOrInsert(PREBusinesses info)
        {
            PREBusinesses original = provider.GetByID(info.BusinessesID.ToString()).QueryObject;
            if (original != null)
            {
                return provider.Update(info, original);
            }
            else
            {
                return provider.Insert(info);
            }
        }
        #endregion
    }
}
