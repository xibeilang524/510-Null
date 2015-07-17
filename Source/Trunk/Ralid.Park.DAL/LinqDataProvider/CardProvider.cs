using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
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
                    result = result.Where(c => c.Status == con.Status.Value);
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
                if (!string.IsNullOrEmpty(con.Department))
                {
                    result = result.Where(c => c.Department.Contains(con.Department));
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
                if (con.IsIn != null)
                {
                    if (con.IsIn.Value)
                        result = result.Where(c => (c.ParkingStatus & ParkingStatus.In) == ParkingStatus.In);
                    else
                        result = result.Where(c => (c.ParkingStatus & ParkingStatus.In) != ParkingStatus.In);
                }
                if (con.LastDateTime != null)
                {
                    result = result.Where(c => c.LastDateTime >= con.LastDateTime.Begin && c.LastDateTime <= con.LastDateTime.End);
                }
                if (con.UpdateFlag != null)
                {
                    result = result.Where(c => c.UpdateFlag == con.UpdateFlag);
                }
                if (!string.IsNullOrEmpty(con.CarPlateOrLast))
                {
                    result = result.Where(c => c.LastCarPlate.Contains(con.CarPlateOrLast) || c.CarPlate.Contains(con.CarPlateOrLast));
                }
                if (con.ListType != null)
                {
                    result = result.Where(c => c.ListType == con.ListType.Value);
                }
                if (!string.IsNullOrEmpty(con.ListCarPlate))
                {
                    result = result.Where(c => c.CarPlate == con.ListCarPlate);
                }
                if (con.CardType != null)
                {
                    //这里不能使用Card类的CardType来作为条件查询，因为CardType未做映射，会报错
                    //必须使用_CardType作为条件
                    result = result.Where(c => c._CardType == con.CardType.ID);
                }
                if (con.OnlineHandleWhenOfflineMode != null)
                {
                    if (con.OnlineHandleWhenOfflineMode.Value)
                    {
                        //脱机时在线处理卡片
                        result = result.Where(c => (c.Options & CardOptions.OfflineHandleWhenOfflineMode) != CardOptions.OfflineHandleWhenOfflineMode);
                    }
                    else
                    {
                        //脱机时脱机线处理卡片
                        result = result.Where(c => (c.Options & CardOptions.OfflineHandleWhenOfflineMode) == CardOptions.OfflineHandleWhenOfflineMode);
                    }
                }

                //先排序
                result = result.OrderBy(c => c.CardID);
                con.TotalCountEx = result.Count();
                if (con.PageSize > 0 && con.PageIndex > 0)
                {
                    //需要分页时，再分页
                    result = result.Skip((con.PageIndex - 1) * con.PageSize).Take(con.PageSize);
                    con.TotalCount = result.Count();
                }
                infoes = result.ToList();
                //infoes = result.OrderBy(c => c.CardID).ToList();
                //if (con.CardType != null)
                //{
                //    infoes = infoes.Where(c => c.CardType == con.CardType).ToList();
                //}
            }
            return infoes;
        }
        #endregion

        public CommandResult DeleteAllItems()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(base.ConnectStr))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from Card", con))
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();
                        return new CommandResult(ResultCode.Successful, string.Empty);
                    }
                }
            }
            catch (SqlException ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCodeResolver.GetFromSqlExceptionNumber(ex.Number), ex.Message);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }
    }
}
