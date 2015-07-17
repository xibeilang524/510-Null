using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Data.SqlClient;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class PREPreferentialProvider : ProviderBase<PREPreferentialInfo, string>, IPREPreferentialProvider
    {
        public PREPreferentialProvider()
        {
        }

        public PREPreferentialProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override PREPreferentialInfo GetingItemByID(string id, ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<PREPreferentialInfo>(o => o.Operator);
            parking.LoadOptions = opt;
            return parking.PREPreferentialInfo.SingleOrDefault(o => o.PreferentialID.ToString() == id);
        }

        protected override List<PREPreferentialInfo> GetingAllItems(ParkDataContext parking)
        {
            DataLoadOptions opt = new DataLoadOptions();
            opt.LoadWith<PREPreferentialInfo>(o => o.Operator);
            parking.LoadOptions = opt;
            return parking.PREPreferentialInfo.ToList();
        }


        protected override List<PREPreferentialInfo> GetingItems(ParkDataContext parking, SearchCondition search)
        {
            if (search is CardSearchCondition)
            {
                CardSearchCondition con = search as CardSearchCondition;
                IQueryable<PREPreferentialInfo> result = parking.PREPreferentialInfo.AsQueryable();
                if (!string.IsNullOrEmpty(con.CardID))
                {
                    result = result.Where(o => o.CardID == con.CardID);
                }
                if (con.LastDateTime != null)
                {
                    result = result.Where(o => o.EntranceTime >= con.LastDateTime.Begin);
                    result = result.Where(o => o.EntranceTime <= con.LastDateTime.End);
                }
                return result.ToList();
            }
            else
            {
                return new List<PREPreferentialInfo>();
            }
        }

        protected override void InsertingItem(PREPreferentialInfo info, ParkDataContext parking)
        {
            if (info.Operator != null) parking.GetTable<PREOperatorInfo>().Attach(info.Operator);//不需要在插入优惠信息时同时插入操作员的角色

            base.InsertingItem(info, parking);
        }

        public CommandResult DeleteAllItems()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(base.ConnectStr))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from PREPreferential", con))
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
        #endregion
    }
}
