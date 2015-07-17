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
    public class PREPreferentialBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public PREPreferentialBll(string repoUri)
        {
            provider = ProviderFactory.Create<IPREPreferentialProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private IPREPreferentialProvider provider;
        #endregion

        #region 公共方法
        /// <summary>
        /// 根据主键ID获取优惠信息
        /// </summary>
        /// <param name="optID"></param>
        /// <returns></returns>
        public QueryResult<PREPreferentialInfo> GetByID(string preID)
        {
            return provider.GetByID(preID);
        }
        /// <summary>
        /// 获取所有优惠信息
        /// </summary>
        /// <returns></returns>
        public QueryResultList<PREPreferentialInfo> GetAllPreferentials()
        {
            return provider.GetAll();
        }
        /// <summary>
        /// 根据查询条件获取优惠信息
        /// </summary>
        public QueryResultList<PREPreferentialInfo> GetPreferentials(CardSearchCondition search)
        {
            return provider.GetItems(search);
        }

        /// <summary>
        /// 增加优惠信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Insert(PREPreferentialInfo info)
        {
            return provider.Insert(info);
        }
        /// <summary>
        /// 修改优惠信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Update(PREPreferentialInfo info)
        {
            PREPreferentialInfo original = GetByID(info.PreferentialID.ToString()).QueryObject;
            if (original != null)
            {
                return provider.Update(info, original);
            }
            else
            {
                return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
            }
        }
        /// <summary>
        /// 删除优惠信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        /// <exception cref=" "></exception>
        public CommandResult Delete(PREPreferentialInfo info)
        {
            return provider.Delete(info);
        }

        #endregion
    }
}
