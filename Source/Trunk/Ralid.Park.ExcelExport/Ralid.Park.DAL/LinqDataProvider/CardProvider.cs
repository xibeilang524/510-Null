using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.GeneralLibrary.ExceptionHandling ;
using Ralid.Park.BusinessModel.SearchCondition;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class CardProvider : ProviderBase<CardInfo, string>, ICardProvider
    {
        #region 构造函数
        public CardProvider()
        {
        }

        public CardProvider(string connStr)
            : base(connStr)
        {
        }
        #endregion

        #region 重写基类方法
        protected override CardInfo GetingItemByID(string id, ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<CardInfo>(c => c.CarInfo);
            parking.LoadOptions = opt;
            CardInfo info = parking.Card.SingleOrDefault(c => c.CardID == id);
            return info;
        }
        protected override List<CardInfo> GetingAllItems(ParkDataContext parking)
        {
            List<CardInfo> infoes = new List<CardInfo>();
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<CardInfo>(c => c.CarInfo);
            parking.LoadOptions = opt;
            return parking.Card.Select(c => c).OrderBy(c => c.CardID).ToList();
        }
        protected override List<CardInfo> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            List<CardInfo> infoes = new List<CardInfo>();
            if (search is CardSearchCondition)
            {
                CardSearchCondition con = search as CardSearchCondition;
                DataLoadOptions opt = new DataLoadOptions();
                opt.LoadWith<CardInfo>(c => c.CarInfo);
                parking.LoadOptions = opt;
                IQueryable<CardInfo> result = parking.Card;
                if (!string.IsNullOrEmpty(con.CardID))
                {
                    result = result.Where(c => c.CardID.IndexOf(con.CardID) > -1);
                }
                if (con.CardType != null)
                {
                    result = result.Where(c => c.CardType == con.CardType);
                }
                if (con.Status != null)
                {
                    result = result.Where(c => (c.Status & con.Status.Value) == c.Status);
                }
                if (con.CarType != null)
                {
                    result = result.Where(c => c.CarType == con.CarType.Value);
                }
                if (con.AccessID != null)
                {
                    result = result.Where(c => c.AccessID == con.AccessID.Value);
                }
                if (!string.IsNullOrEmpty(con.OwnerName))
                {
                    result = result.Where(c => c.CarInfo.OwnerName == con.OwnerName);
                }
                infoes = result.OrderBy(c => c.CardID).ToList();
            }
            return infoes;
        }
        protected override void InsertingItem(CardInfo info, ParkDataContext parking)
        {
            parking.Card.InsertOnSubmit(info);
        }
        protected override void UpdatingItem(CardInfo newVal, CardInfo original, ParkDataContext parking)
        {
            parking.Card.Attach(newVal, original);
            parking.Car.Attach(newVal.CarInfo, original.CarInfo);
        }
        protected override void DeletingItem(CardInfo info, ParkDataContext parking)
        {
            parking.Card.Attach(info);
            parking.Car.DeleteOnSubmit(info.CarInfo);
            parking.Card.DeleteOnSubmit(info);
        }
        #endregion
    }
}
