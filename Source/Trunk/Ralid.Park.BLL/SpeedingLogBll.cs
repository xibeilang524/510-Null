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
    /// <summary>
    /// 已处理超速记录逻辑类
    /// </summary>
    public class SpeedingLogBll
    {
        
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public SpeedingLogBll(string repoUri)
        {
            _repoUri = repoUri;
            provider = ProviderFactory.Create<ISpeedingLogProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private ISpeedingLogProvider provider;
        private string _repoUri;
        #endregion

        #region 公共方法
        /// <summary>
        /// 根据主键ID获取已处理超速记录
        /// </summary>
        /// <param name="optID"></param>
        /// <returns></returns>
        public QueryResult<SpeedingLog> GetByID(Guid id)
        {
            return provider.GetByID(id);
        }
        /// <summary>
        /// 获取所有已处理超速记录
        /// </summary>
        /// <returns></returns>
        public QueryResultList<SpeedingLog> GetAllRecords()
        {
            return provider.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取已处理超速记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<SpeedingLog> GetRecords(RecordSearchCondition search)
        {
            return provider.GetItems(search);
        }

        /// <summary>
        /// 增加已处理超速记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Insert(SpeedingLog info)
        {
            return provider.Insert(info);
        }
        /// <summary>
        /// 修改未处理超速记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Update(SpeedingLog info)
        {
            SpeedingLog original = GetByID(info.SpeedingID).QueryObject;
            if (original != null)
            {
                return provider.Update(info, original);
            }
            else
            {
                return new CommandResult(ResultCode.NoRecord);
            }
        }
        /// <summary>
        /// 删除未处理超速记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        /// <exception cref=" "></exception>
        public CommandResult Delete(SpeedingLog info)
        {
            return provider.Delete(info);
        }

        #endregion
    }
}
