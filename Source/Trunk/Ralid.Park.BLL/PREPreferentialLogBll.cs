using System;
using System.Collections;
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
    public class PREPreferentialLogBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public PREPreferentialLogBll(string repoUri)
        {
            provider = ProviderFactory.Create<IPREPreferentialLogProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private IPREPreferentialLogProvider provider;
        #endregion

        #region 公共方法
        /// <summary>
        /// 根据主键ID获取优惠信息
        /// </summary>
        /// <param name="optID"></param>
        /// <returns></returns>
        public QueryResult<PREPreferentialLog> GetByID(string preID)
        {
            return provider.GetByID(preID);
        }
        /// <summary>
        /// 获取所有优惠信息
        /// </summary>
        /// <returns></returns>
        public QueryResultList<PREPreferentialLog> GetAllPreferentials()
        {
            return provider.GetAll();
        }
        /// <summary>
        /// 根据查询条件获取优惠信息
        /// </summary>
        public QueryResultList<PREPreferentialLog> GetPreferentials(PreferentialReportSearchCondition search)
        {
            return provider.GetItems(search);
        }

        /// <summary>
        /// 增加优惠信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Insert(PREPreferentialLog info)
        {
            return provider.Insert(info);
        }

        #endregion
    }
}
