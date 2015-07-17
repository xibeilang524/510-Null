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
    /// 未处理超速记录逻辑类
    /// </summary>
    public class SpeedingRecordBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public SpeedingRecordBll(string repoUri)
        {
            _repoUri = repoUri;
            provider = ProviderFactory.Create<ISpeedingRecordProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private ISpeedingRecordProvider provider;
        private string _repoUri;
        #endregion

        #region 公共方法
        /// <summary>
        /// 根据主键ID获取未处理超速记录
        /// </summary>
        /// <param name="optID"></param>
        /// <returns></returns>
        public QueryResult<SpeedingRecord> GetByID(Guid id)
        {
            return provider.GetByID(id);
        }
        /// <summary>
        /// 获取所有未处理超速记录
        /// </summary>
        /// <returns></returns>
        public QueryResultList<SpeedingRecord> GetAllRecords()
        {
            return provider.GetAll();
        }

        /// <summary>
        /// 根据查询条件获取未处理超速记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<SpeedingRecord> GetRecords(RecordSearchCondition search)
        {
            return provider.GetItems(search);
        }

        /// <summary>
        /// 根据车牌号码获取未处理超速记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<SpeedingRecord> GetRecordsByCarPlate(string carPlate)
        {
            if (!string.IsNullOrEmpty(carPlate))
            {
                RecordSearchCondition search = new RecordSearchCondition();
                search.CarPlate = carPlate;
                return provider.GetItems(search);
            }
            return new QueryResultList<SpeedingRecord>(ResultCode.NoRecord, null);
        }

        /// <summary>
        /// 增加未处理超速记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Insert(SpeedingRecord info)
        {
            return provider.Insert(info);
        }
        /// <summary>
        /// 修改未处理超速记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Update(SpeedingRecord info)
        {
            SpeedingRecord original = GetByID(info.SpeedingID).QueryObject;
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
        public CommandResult Delete(SpeedingRecord info)
        {
            return provider.Delete(info);
        }

        /// <summary>
        /// 处理超速记录
        /// </summary>
        /// <param name="info">未处理的超速记录</param>
        /// <param name="operatorInfo">处理人员</param>
        /// <param name="dateTime">处理时间</param>
        /// <param name="memo">处理备注信息</param>
        /// <returns></returns>
        public CommandResult SpeedingProcessing(SpeedingRecord info, OperatorInfo operatorInfo, DateTime dateTime, string memo)
        {
            if (info != null)
            {
                SpeedingLog log = new SpeedingLog();
                log.SpeedingID = info.SpeedingID;
                log.SpeedingDateTime = info.SpeedingDateTime;
                log.PlateNumber = info.PlateNumber;
                log.Place = info.Place;
                log.Speed = info.Speed;
                log.Photo = info.Photo;
                log.Memo = info.Memo;
                log.DealOperatorID = operatorInfo.OperatorID;
                log.DealDateTime = dateTime;
                log.DealMemo = memo;

                IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(_repoUri);
                ISpeedingLogProvider ilProvider = ProviderFactory.Create<ISpeedingLogProvider>(_repoUri);
                ilProvider.Insert(log, unitWork);
                provider.Delete(info, unitWork);
                return unitWork.Commit();
            }
            return new CommandResult(ResultCode.NoRecord);
        }
        #endregion
    }
}
