using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class VideoSourceProvider : ProviderBase<VideoSourceInfo,int>, IVideoSourceProvider
    {
        public VideoSourceProvider()
        {
        }

        public VideoSourceProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override VideoSourceInfo GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.VideoSource.SingleOrDefault(v => v.VideoID == id);
        }
        public CommandResult InsertWithPrimaryKey(VideoSourceInfo info)
        {
            CommandResult result = null;
            try
            {
                ParkDataContext parking = ParkDataContextFactory.CreateParking(base.ConnectStr);
                if (parking != null)
                {
                    string cmdtext = string.Empty;
                    cmdtext += string.Format("SET IDENTITY_INSERT VideoSource ON;");
                    cmdtext += info.SQLInsertWithPrimaryCmd;
                    cmdtext += string.Format("SET IDENTITY_INSERT VideoSource OFF;");
                    parking.ExecuteCommand(cmdtext);
                    result = new CommandResult(ResultCode.Successful, string.Empty);
                }
                else
                {
                    result = new CommandResult(ResultCode.Fail, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result = new CommandResult(ResultCode.Fail, string.Empty);
            }
            return result;
        }

        public CommandResult DeleteAllItems()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(base.ConnectStr))
                {
                    using (SqlCommand cmd = new SqlCommand("delete from VideoSource", con))
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
