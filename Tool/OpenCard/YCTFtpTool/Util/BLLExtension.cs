using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park .DAL .IDAL ;
using Ralid.Park.BLL;
using Ralid.Park .BusinessModel .Model ;
using Ralid.Park .BusinessModel .Result ;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.OpenCard.YCTFtpTool
{
    public static class BLLExtension
    {
        public static CommandResult BatchChangeUploadFile(this YCTPaymentRecordBll bll, List<YCTPaymentRecord> records, string uploadFile)
        {
            try
            {
                IUnitWork unitWork = ProviderFactory.Create<IUnitWork>(AppSettings.CurrentSetting.MasterParkConnect);
                IYCTPaymentRecordProvider provider = ProviderFactory.Create<IYCTPaymentRecordProvider>(AppSettings.CurrentSetting.MasterParkConnect);
                foreach (var item in records)
                {
                    var newVal = item.Clone();
                    newVal.UploadFile = uploadFile;
                    provider.Update(newVal, item, unitWork);
                }
                return unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }
    }
}
