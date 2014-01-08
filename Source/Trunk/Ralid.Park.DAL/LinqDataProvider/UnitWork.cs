using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.DAL.LinqDataProvider
{
    /// <summary>
    /// 单元操作
    /// </summary>
    public class UnitWork : IUnitWork
    {
        private ParkDataContext _Parking;

        public UnitWork(string connStr)
        {
            _Parking = ParkDataContextFactory.CreateParking(connStr);
        }

        public ParkDataContext Parking
        {
            get
            {
                return _Parking;
            }
        }

        public CommandResult Commit()
        {
            try
            {
                if (Parking != null)
                {
                    Parking.SubmitChanges();
                    return new CommandResult(ResultCode.Successful,
                        ResultCodeDecription.GetDescription(ResultCode.Successful));
                }
                else
                {
                    return new CommandResult(ResultCode.CannotConnectServer, ResultCodeDecription.GetDescription(ResultCode.CannotConnectServer));
                }

            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, "UnitWork.Commit()");
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }
    }
}
