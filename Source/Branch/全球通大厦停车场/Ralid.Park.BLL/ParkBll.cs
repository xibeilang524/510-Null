using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.BLL
{
    public class ParkBll
    {

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public ParkBll(string repoUri)
        {
            this.provider = ProviderFactory.Create<IParkProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private IParkProvider provider;
        #endregion

        #region 公共方法
        public QueryResult<ParkInfo> GetParkInfoByID(int parkID)
        {
            return provider.GetByID(parkID);
        }

        public QueryResultList<ParkInfo> GetAllParks()
        {
            return provider.GetAll();
        }

        public CommandResult Insert(ParkInfo info)
        {
            return provider.Insert(info);
        }

        public CommandResult Update(ParkInfo info)
        {
            ParkInfo original = GetParkInfoByID(info.ParkID).QueryObject;
            return provider.Update(info, original);
        }

        /// <summary>
        /// 更新停车场的车位数
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult UpdateVacant(int parkID, short vacant)
        {
            ParkInfo original = GetParkInfoByID(parkID).QueryObject;
            ParkInfo oldVal = original.Clone();
            original.Vacant = vacant;
            return provider.Update(original, oldVal);
        }

        public CommandResult Delete(ParkInfo info)
        {
            return provider.Delete(info);
        }
        #endregion
    }
}
