using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.DAL.IDAL;

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
                Parking.SubmitChanges();
                return new CommandResult(Ralid.Park.BusinessModel.Result.ResultCode.Successful,
                    Ralid.Park.BusinessModel.Result.ResultCode.Successful.ToString());

            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, "UnitWork.Commit()");
                return new CommandResult(Ralid.Park.BusinessModel.Result.ResultCode.Fail, ex.Message);
            }
        }
    }
}
