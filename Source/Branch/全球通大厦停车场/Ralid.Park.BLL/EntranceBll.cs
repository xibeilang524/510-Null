using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.BLL
{
    public class EntranceBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public EntranceBll(string repoUri)
        {
            provider = ProviderFactory.Create<IEntranceProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private IEntranceProvider provider;
        #endregion

        #region 公共方法
        public QueryResult<EntranceInfo> GetEntranceInfo(int id)
        {
            return provider.GetByID(id);
        }

        public QueryResultList<EntranceInfo> GetAllEntraces()
        {
            return provider.GetAll();
        }

        public QueryResultList<EntranceInfo> GetEntrancesOfPark(int parkID)
        {
            EntranceSearchCondition con = new EntranceSearchCondition();
            con.ParkID = parkID;
            return provider.GetItems(con);
        }

        public CommandResult Update(EntranceInfo info)
        {
            EntranceInfo original = provider.GetByID(info.EntranceID).QueryObject;
            return provider.Update(info, original);
        }

        public CommandResult Insert(EntranceInfo info)
        {
            return provider.Insert(info);
        }


        public CommandResult Delete(EntranceInfo info)
        {
            return provider.Delete(info);
        }
        #endregion
    }
}
