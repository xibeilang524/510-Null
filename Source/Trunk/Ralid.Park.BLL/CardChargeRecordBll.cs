using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.BLL
{
    /// <summary>
    /// 充值记录逻辑处理类
    /// </summary>
    public class CardChargeRecordBll
    {
        /// <summary>
        /// /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        /// </summary>
        public CardChargeRecordBll(string repoUri)
        {
            provider = ProviderFactory.Create<ICardChargeRecordProvider>(repoUri);
        }

        #region 私有变量
        ICardChargeRecordProvider provider;
        #endregion

        #region 公共方法
        public CommandResult InsertRecordWithCheck(CardChargeRecord info)
        {
            RecordSearchCondition searchCondition = new RecordSearchCondition();
            searchCondition.CardID = info.CardID;
            searchCondition.RecordDateTimeRange = new DateTimeRange(info.ChargeDateTime, info.ChargeDateTime);

            List<CardChargeRecord> check = provider.GetItems(searchCondition).QueryObjects;
            if (check == null || check.Count == 0)
            {
                return provider.Insert(info);
            }
            //已存在该记录，可认为插入成功
            return new CommandResult(ResultCode.Successful, string.Empty);
        }
        #endregion
    }
}
