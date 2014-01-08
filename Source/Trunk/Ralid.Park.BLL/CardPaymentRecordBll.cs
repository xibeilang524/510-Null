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
    public class CardPaymentRecordBll
    {
        /// <summary>
        /// /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        /// </summary>
        public CardPaymentRecordBll(string repoUri)
        {
            provider = ProviderFactory.Create<ICardPaymentRecordProvider>(repoUri);
            cp = ProviderFactory.Create<ICardProvider>(repoUri);
        }

        #region 私有变量
        ICardPaymentRecordProvider provider;
        ICardProvider cp;
        #endregion

        #region 公共方法
        public CardPaymentInfo GetByID(RecordID id)
        {
            CardPaymentRecordSearchCondition con = new CardPaymentRecordSearchCondition();
            con.CardID = id.CardID;
            con.RecordDateTimeRange = new DateTimeRange(id.RecordDateTime, id.RecordDateTime);
            List<CardPaymentInfo> records = provider.GetItems(con).QueryObjects;
            if (records.Count == 1)
            {
                return records[0];
            }
            else if (records.Count == 0)
            {
                return null;
            }
            else
            {
                throw new InvalidCastException(Resource1.CardPaymentRecordBll_notSingle);
            }
        }

        /// <summary>
        /// 获取卡号为cardID,进场时间为enterDateTime的最近的一条收费记录
        /// </summary>
        /// <param name="cardID"></param>
        /// <param name="enterDateTime"></param>
        /// <returns></returns>
        public CardPaymentInfo GetLatestRecord(string cardID, DateTime enterDateTime, OperatorInfo operatorInfo)
        {
            CardPaymentRecordSearchCondition con = new CardPaymentRecordSearchCondition();
            con.CardID = cardID;
            con.EnterDateTime = enterDateTime;
            con.Operator = operatorInfo;
            List<CardPaymentInfo> records = provider.GetItems(con).QueryObjects;
            CardPaymentInfo record = null;

            if (records.Count > 0)
            {
                records = (from r in records
                           orderby r.ChargeDateTime descending
                           select r).ToList();
                record = records[0];
            }
            return record;
        }

        public CommandResult Insert(CardPaymentInfo info)
        {
            return provider.Insert(info);
        }

        public CommandResult Update(CardPaymentInfo info)
        {
            CardPaymentInfo oldVal = provider.GetByID(info.ID).QueryObject;
            return provider.Update(info, oldVal);
        }

        public CommandResult Delete(CardPaymentInfo info)
        {
            return provider.Delete(info);
        }

        public QueryResultList<CardPaymentInfo> GetItems(RecordSearchCondition search)
        {
            return provider.GetItems(search);
        }

        public CommandResult InsertRecordWithCheck(CardPaymentInfo info)
        {
            CardPaymentRecordSearchCondition searchCondition = new CardPaymentRecordSearchCondition();
            searchCondition.CardID = info.CardID;
            searchCondition.ChargeDateTime = info.ChargeDateTime;

            List<CardPaymentInfo> check = provider.GetItems(searchCondition).QueryObjects;
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
