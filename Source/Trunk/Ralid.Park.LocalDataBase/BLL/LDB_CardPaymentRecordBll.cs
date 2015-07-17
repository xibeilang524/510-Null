using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.LocalDataBase.DAL.IDAL;
using Ralid.Park.LocalDataBase.Model;

namespace Ralid.Park.LocalDataBase.BLL
{
    public class LDB_CardPaymentRecordBll
    {

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public LDB_CardPaymentRecordBll(string repoUri)
        {
            _RepoUri = repoUri;
            _Provider = LDB_ProviderFactory.Create<LDB_ICardPaymentRecordProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private LDB_ICardPaymentRecordProvider _Provider;
        private string _RepoUri;
        #endregion

        #region 公共方法
        public QueryResult<LDB_CardPaymentInfo> GetCardPaymentInfoByID(int id)
        {
            return _Provider.GetByID(id);
        }

        public CommandResult Insert(LDB_CardPaymentInfo info)
        {
            return _Provider.Insert(info);
        }

        public CommandResult Update(LDB_CardPaymentInfo info)
        {
            if (info.ID.HasValue)
            {
                LDB_CardPaymentInfo oldVal = _Provider.GetByID(info.ID.Value).QueryObject;
                return _Provider.Update(info, oldVal);
            }
            return new CommandResult(ResultCode.Fail, string.Empty);
        }

        public CommandResult Delete(LDB_CardPaymentInfo info)
        {
            return _Provider.Delete(info);
        }

        public List<LDB_CardPaymentInfo> GetAll()
        {
            return _Provider.GetAll().QueryObjects;
        }

        public QueryResultList<LDB_CardPaymentInfo> GetItems(RecordSearchCondition search)
        {
            return _Provider.GetItems(search);
        }

        /// <summary>
        /// 收取卡片的停车费
        /// </summary>
        /// <param name="info">缴费卡片，为空值时从数据库中获取,主要用于写卡模式时读取到卡片的数据</param>
        /// <param name="payment">缴费记录</param>
        /// <returns></returns>
        public CommandResult PayParkFee(CardInfo info, CardPaymentInfo payment)
        {
            if (info != null)
            {
                LDB_CardPaymentInfo ldbRecord = LDB_InfoFactory.CreateLDBCardPaymentInfo(payment);
                CommandResult result = _Provider.Insert(ldbRecord);
                if (result.Result == ResultCode.Successful)
                {
                    if (payment.PaymentMode == PaymentMode.Prepay)
                    {
                        info.Balance -= payment.Paid;
                    }

                    //只有卡片在场或可重复出场，并且与缴费记录的进场时间相同，才会更新卡片信息
                    if ((info.IsInPark || info.CanRepeatOut)
                        && payment.EnterDateTime.HasValue
                        && info.LastDateTime == payment.EnterDateTime.Value)
                    {
                        //设置卡片缴费信息
                        info.SetPaidData(payment);
                    }
                }

                return result;
            }

            return new CommandResult(ResultCode.NoRecord, ResultCodeDecription.GetDescription(ResultCode.NoRecord));
        }        

        /// <summary>
        /// 删除卡片某时间的缴费记录
        /// </summary>
        /// <param name="cardid"></param>
        /// <param name="datetime"></param>
        public void DeleteCardPayment(string cardid, DateTime datetime)
        {
            RecordSearchCondition search = new RecordSearchCondition();
            search.CardID = cardid;
            search.RecordDateTimeRange = new DateTimeRange(datetime, datetime);
            QueryResultList<LDB_CardPaymentInfo> records = _Provider.GetItems(search);
            if (records.Result == ResultCode.Successful)
            {
                foreach (LDB_CardPaymentInfo record in records.QueryObjects)
                {
                    _Provider.Delete(record);
                }
            }
        }
        #endregion
    }
}
