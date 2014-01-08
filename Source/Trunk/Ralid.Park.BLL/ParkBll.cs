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
        /// 更新停车场命令服务器地址
        /// </summary>
        /// <param name="parkID"></param>
        /// <param name="parkAdapterUri"></param>
        /// <returns></returns>
        public CommandResult UpdateParkAdapterUri(int parkID, string parkAdapterUri)
        {
            ParkInfo original = GetParkInfoByID(parkID).QueryObject;
            if (original != null)
            {
                ParkInfo newVal = original.Clone();
                newVal.ParkAdapterUri = parkAdapterUri;
                return provider.Update(newVal, original);
            }
            return new CommandResult(ResultCode.Fail);
        }

        /// <summary>
        /// 更新停车场的车位数
        /// </summary>
        /// <param name="parkID">停车场ID</param>
        /// <param name="vacant">车位数</param>
        /// <returns></returns>
        public CommandResult UpdateVacant(int parkID, short vacant)
        {
            ParkInfo original = GetParkInfoByID(parkID).QueryObject;
            if (original != null)
            {
                ParkInfo newVal = original.Clone();
                newVal.Vacant = vacant;
                return provider.Update(newVal, original);
            }
            return new CommandResult(ResultCode.Fail, string.Empty);
        }

        public CommandResult Delete(ParkInfo info)
        {
            return provider.Delete(info);
        }

        /// <summary>
        /// 更新停车场，如没有该停车场，插入一个停车场
        /// </summary>
        /// <param name="info"></param>
        /// <param name="withPrimaryKey">是否插入主键值</param>
        /// <returns></returns>
        public CommandResult UpdateOrInsert(ParkInfo info, bool withPrimaryKey)
        {
            ParkInfo original = GetParkInfoByID(info.ParkID).QueryObject;
            if (original != null)
            {
                return provider.Update(info, original);
            }
            else if (withPrimaryKey)
            {
                return provider.InsertWithPrimaryKey(info);
            }
            else
            {
                return provider.Insert(info);
            }
        }

        /// <summary>
        /// 插入包括主键值的停车场记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult InsertWithPrimaryKey(ParkInfo info)
        {
            return provider.InsertWithPrimaryKey(info);
        }

        /// <summary>
        /// 删除所有停车场
        /// </summary>
        /// <returns></returns>
        public CommandResult DeleteAllParks()
        {
            return provider.DeleteAllItems();
        }
        #endregion
    }
}
