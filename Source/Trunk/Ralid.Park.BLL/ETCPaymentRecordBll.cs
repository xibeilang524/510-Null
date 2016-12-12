using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.BLL
{
    /// <summary>
    /// 报警信息的应用层类
    /// </summary>
    public class ETCPaymentRecordBll
    {

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public ETCPaymentRecordBll(string repoUri)
        {
            provider = ProviderFactory.Create<IETCPaymentRecordProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        IETCPaymentRecordProvider provider;
        #endregion

        #region 公共方法
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Insert(ETCPaymentRecord info)
        {
            return provider.Insert(info);
        }

        /// <summary>
        /// 更新记录的上传时间
        /// </summary>
        public CommandResult UpdateUploadTime(ETCPaymentRecord info, DateTime? uploadTime)
        {
            var newVal = info.Clone();
            newVal.UploadTime = uploadTime;
            var ret = provider.Update(newVal, info);
            if (ret.Result == ResultCode.Successful)
            {
                info.UploadTime = uploadTime;
            }
            return ret;
        }

        /// <summary>
        /// 根据条件获取记录
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<ETCPaymentRecord> GetRecords(ETCPaymentRecordSearchCondition search)
        {
            return provider.GetItems(search);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Delete(ETCPaymentRecord info)
        {
            return provider.Delete(info);
        }
        #endregion
    }
}
