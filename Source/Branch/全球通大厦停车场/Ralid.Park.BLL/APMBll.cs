using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .BusinessModel .Model ;
using Ralid.Park .BusinessModel .Result ;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park .DAL.IDAL ;

namespace Ralid.Park.BLL
{
    /// <summary>
    /// 表示操作APM自助缴费机的业务逻辑类
    /// </summary>
    public class APMBll
    {
        #region 构造函数
        public APMBll(string repoUri)
        {
            _Provider = ProviderFactory.Create<IAPMProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        IAPMProvider _Provider;
        #endregion

        #region 公共方法
        /// <summary>
        /// 增加自助缴机
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Insert(APM info)
        {
            List<APM> allAPM = GetAllItems().QueryObjects;
            if (allAPM.Exists(apm => apm.SerialNum == info.SerialNum))
            {
                throw new InvalidOperationException(string.Format(Resource1.APMBll_NumbeUsed, info.SerialNum));
            }

            return _Provider.Insert(info);
        }
        /// <summary>
        /// 修改自助缴机
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Update(APM info)
        {
            List<APM> allAPM = GetAllItems().QueryObjects;
            if (allAPM.Exists(apm => apm.SerialNum == info.SerialNum))
            {
                throw new InvalidOperationException(string.Format(Resource1.APMBll_NumbeUsed, info.SerialNum));
            }

            APM original = _Provider.GetByID(info.ID).QueryObject;
            if (original != null)
            {
                return _Provider.Update(info, original);
            }
            else
            {
                return new CommandResult(ResultCode.Fail, string.Empty);
            }
        }
        /// <summary>
        /// 删除自助缴机
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Delete(APM info)
        {
            return _Provider.Delete(info);
        }
        /// <summary>
        /// 获取系统中所有的自助缴机
        /// </summary>
        /// <returns></returns>
        public QueryResultList<APM> GetAllItems()
        {
            return _Provider.GetAll();
        }

        /// <summary>
        /// 通过ID获取自助缴机信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QueryResult<APM> GetByID(int id)
        {
            return _Provider.GetByID(id);
        }

        /// <summary>
        /// 更新APM当前活动时间
        /// </summary>
        /// <param name="info"></param>
        /// <param name="activeDt"></param>
        /// <returns></returns>
        public CommandResult UpdateActiveDateTime(APM info, DateTime activeDt)
        {
            APM original = _Provider.GetByID(info.ID).QueryObject;
            if (original != null)
            {
                APM newVal = original.Clone();
                newVal.ActiveDateTime = activeDt;
                return _Provider.Update(newVal, original);
            }
            else
            {
                return new CommandResult(ResultCode.Fail, string.Empty);
            }
        }
        #endregion
    }
}
