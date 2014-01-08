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
            CardInfo info = parking.Card.SingleOrDefault(c => c.CardID == id);
            return info;
        }

        protected override List<CardInfo> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            List<CardInfo> infoes = new List<CardInfo>();
            if (search is CardSearchCondition)
            {
                CardSearchCondition con = search as CardSearchCondition;
                IQueryable<CardInfo> result = parking.Card;
                if (!string.IsNullOrEmpty(con.CardID))
                {
                    result = result.Where(c => c.CardID.IndexOf(con.CardID) > -1);
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
                    result = result.Where(c => c.OwnerName.Contains(con.OwnerName));
                }
                if (!string.IsNullOrEmpty(con.LastCarPlate))
                {
                    result = result.Where(c => c.LastCarPlate.Contains(con.LastCarPlate));
                }
                if (!string.IsNullOrEmpty(con.CarPlate))
                {
                    result = result.Where(c => c.CarPlate.Contains(con.CarPlate));
                }
                if (!string.IsNullOrEmpty(con.CardCertificate))
                {
                    result = result.Where(c => c.CardCertificate.Contains(con.CardCertificate));
                }
                if (con.ParkingStatus != null) result = result.Where(c => c.ParkingStatus == con.ParkingStatus);
                if (con.LastDateTime != null)
                {
                    result = result.Where(c => c.LastDateTime >= con.LastDateTime.Begin && c.LastDateTime <= con.LastDateTime.End);
                }
                infoes = result.OrderBy(c => c.CardID).ToList();
                if (con.CardType != null)
                {
                    infoes = infoes.Where(c => c.CardType == con.CardType).ToList();
                }
            }
            return infoes;
        }
        #endregion
    }
}
