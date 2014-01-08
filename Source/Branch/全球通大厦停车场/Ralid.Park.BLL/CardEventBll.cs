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
    public class CardEventBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public CardEventBll(string repoUri)
        {
            provider = ProviderFactory.Create<ICardEventProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        ICardEventProvider provider;
        #endregion

        #region 公共方法
        /// <summary>
        /// 通过ＩＤ号获取卡片事件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QueryResult<CardEventRecord> GetByID(RecordID id)
        {
            return provider.GetByID(id);
        }
        /// <summary>
        /// 插入卡片事件
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public CommandResult Insert(CardEventRecord info)
        {
            return provider.Insert(info);
        }
        /// <summary>
        /// 通过查询条件获取卡片事件
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public QueryResultList<CardEventRecord> GetCardEvents(RecordSearchCondition search)
        {
            return provider.GetItems(search);
        }
        /// <summary>
        /// 获取卡片在某个时间的第一个卡片事件
        /// </summary>
        /// <param name="cardID"></param>
        /// <param name="cardEventDateTime"></param>
        /// <returns></returns>
        public CardEventRecord GetFirstCardEvent(string cardID, DateTime cardEventDateTime)
        {
            RecordSearchCondition con = new RecordSearchCondition();
            con.CardID = cardID;
            con.RecordDateTimeRange = new DateTimeRange(cardEventDateTime, cardEventDateTime);
            QueryResultList<CardEventRecord> ret = provider.GetItems(con);
            if (ret.Result == ResultCode.Successful && ret.QueryObjects.Count > 0)
            {
                return ret.QueryObjects[0];
            }
            return null;
        }

        /// <summary>
        /// 车流量统计
        /// </summary>
        public List<CarFlowStatistics> CarFlowStaticstics(RecordSearchCondition search, CarFlowStatisticsType statisticsType)
        {
            List<CarFlowStatistics> statistics = new List<CarFlowStatistics>();
            List<CardEventRecord> cardEvents = this.GetCardEvents(search).QueryObjects;
            IEnumerable<IGrouping<string, CardEventRecord>> groups = null;
            if (cardEvents.Count > 0)
            {
                switch (statisticsType)
                {
                    case CarFlowStatisticsType.perHour:
                        groups = cardEvents.GroupBy(c => c.EventDateTime.ToString("yyyy-MM-dd HH:00:00"));
                        break;
                    case CarFlowStatisticsType.perDay:
                        groups = cardEvents.GroupBy(c => c.EventDateTime.ToString("yyyy-MM-dd"));
                        break;
                    case CarFlowStatisticsType.perMonth:
                        groups = cardEvents.GroupBy(c => c.EventDateTime.ToString("yyyy-MM"));
                        break;
                    default:
                        break;
                }
                if (groups != null)
                {
                    foreach (var group in groups)
                    {
                        CarFlowStatistics item = new CarFlowStatistics
                        {
                            TimeInterval = group.Key,
                            TempCardIn = group.Count(c => c.CardType.IsTempCard && !c.IsExitEvent),
                            MonthCardIn = group.Count(c => c.CardType.IsMonthCard && !c.IsExitEvent),
                            PrepayCardIn = group.Count(c => c.CardType.IsPrepayCard && !c.IsExitEvent),
                            TempCardOut = group.Count(c => c.CardType.IsTempCard && c.IsExitEvent),
                            MonthCardOut = group.Count(c => c.CardType.IsMonthCard && c.IsExitEvent),
                            PrepayCardOut = group.Count(c => c.CardType.IsPrepayCard && c.IsExitEvent)
                        };
                        statistics.Add(item);
                    }
                }

            }
            return statistics;
        }
        #endregion
    }
}
