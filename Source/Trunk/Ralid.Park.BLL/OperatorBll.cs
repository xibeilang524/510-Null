using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Resouce;

namespace Ralid.Park.BLL
{
    public class OperatorBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public OperatorBll(string repoUri)
        {
            provider =ProviderFactory.Create<IOperatorProvider>(repoUri );
        }
        #endregion

        #region 私有变量
        private IOperatorProvider provider;
        #endregion

        #region 公共方法
        /// <summary>
        /// 登录验证
        /// </summary>
        public bool Authentication(string logName, string pwd)
        {
            OperatorInfo info = GetByID(logName).QueryObject;
            if (info != null)
            {
                if (info.OperatorID == logName && info.Password == pwd)
                {
                    OperatorInfo.CurrentOperator = info;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据操作员ID获取操作员
        /// </summary>
        /// <param name="optID"></param>
        /// <returns></returns>
        public QueryResult<OperatorInfo> GetByID(string optID)
        {
            return provider.GetByID(optID);
        }
        /// <summary>
        /// 获取所有操作员
        /// </summary>
        /// <returns></returns>
        public QueryResultList<OperatorInfo> GetAllOperators()
        {
            return provider.GetAll();
        }
        /// <summary>
        /// 根据查询条件获取操作员
        /// </summary>
        public QueryResultList<OperatorInfo> GetOperators(OperatorSearchCondition search)
        {
            return provider.GetItems(search);
        }
        /// <summary>
        /// 通过操作员编号获取操作员
        /// </summary>
        /// <param name="optNum"></param>
        /// <returns></returns>
        public OperatorInfo GetOperatorByOperatorNum(byte optNum)
        {
            OperatorInfo opt = null;
            OperatorSearchCondition con = new OperatorSearchCondition();
            con.OperatorNum = optNum;
            List<OperatorInfo> opts = GetOperators(con).QueryObjects;
            if (opts.Count == 1)
            {
                opt = opts[0];
            }
            return opt;
        }
        /// <summary>
        /// 增加操作员,如果操作员编号已被使用,抛出InvalidOperationException
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Insert(OperatorInfo info)
        {
            List<OperatorInfo> allOpt = GetAllOperators().QueryObjects;
            if (allOpt.Exists(opt => opt.OperatorID == info.OperatorID))
            {
                throw new InvalidOperationException(string.Format(Resource1.OperatorBll_IDbeUsed, info.OperatorID));
            }
            if (allOpt.Exists(opt => opt.OperatorName == info.OperatorName))
            {
                throw new InvalidOperationException(string.Format(Resource1.OperatorBll_NamebeUsed, info.OperatorName));
            }
            if (allOpt.Exists(opt => opt.OperatorNum == info.OperatorNum))
            {
                throw new InvalidOperationException(string.Format(Resource1.OperatorBll_NumbeUsed, info.OperatorNum));
            }
            CommandResult ret = provider.Insert(info);
            return ret;
        }
        /// <summary>
        /// 修改操作员,如果操作员编号已被使用,抛出InvalidOperationException
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Update(OperatorInfo info)
        {
            List<OperatorInfo> allOpt = GetAllOperators().QueryObjects;
            if (allOpt.Exists(opt => opt.OperatorID != info.OperatorID && opt.OperatorName == info.OperatorName))
            {
                throw new InvalidOperationException(string.Format(Resource1.OperatorBll_NamebeUsed, info.OperatorName));
            }
            if (allOpt.Exists(opt => opt.OperatorID != info.OperatorID && opt.OperatorNum == info.OperatorNum))
            {
                throw new InvalidOperationException(string.Format(Resource1.OperatorBll_NumbeUsed, info.OperatorNum));
            }

            OperatorInfo original = GetByID(info.OperatorID).QueryObject;
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
        /// 删除操作员
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        /// <exception cref=" "></exception>
        public CommandResult Delete(OperatorInfo info)
        {
            if (!info.CanDelete)
            {
                throw new InvalidOperationException(string.Format(Resource1.OperatorBll_CannotDelete, info.OperatorID));
            }
            return provider.Delete(info);
        }

        /// <summary>
        /// 删除所有操作员
        /// </summary>
        /// <returns></returns>
        public CommandResult DeleteAllOperators()
        {
            return provider.DeleteAllItems();
        }

        /// <summary>
        /// 更新操作员，如没有，插入新的操作员记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult UpdateOrInsert(OperatorInfo info)
        {
            OperatorInfo original = provider.GetByID(info.OperatorID).QueryObject;
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
