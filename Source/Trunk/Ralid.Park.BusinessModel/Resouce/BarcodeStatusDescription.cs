using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 条码枪状态的文字描述
    /// </summary>
    public class BarcodeStatusDescription
    {
        public static string GetDescription(bool opened, int num)
        {
            return string.Format("{0} {1} {2}", Resource1.BarcodeGun, num.ToString(), opened ? Resource1.BarcodeStatus_OK : Resource1.BarcodeStatus_ComPortNotOpend);
        }
    }
}
