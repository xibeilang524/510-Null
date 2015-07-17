using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.UI.Resources;
using Ralid.GeneralLibrary.ExceptionHandling;
using Ralid.GeneralLibrary.SoftDog;

namespace Ralid.Park.UI
{
    /// <summary>
    /// 停车场加密狗验证类
    /// </summary>
    public class ParkingSoftDogVerify
    {
        /// <summary>
        /// 检查停车场加密狗权限
        /// </summary>
        /// <returns></returns>
        public static bool VerifyRight()
        {
            SoftDogReader reader = new SoftDogReader();
            SoftDogInfo info = null;
            try
            {
                info = reader.ReadDog();
                if (info == null)
                {
                    MessageBox.Show(Resource1.FrmMain_SoftDogError, Resource1.Form_Alert);
                    return false;
                }
                else if ((info.SoftwareList & SoftwareType.TYPE_PARK) == 0)  //没有写停车场权限
                {
                    MessageBox.Show(Resource1.FrmMain_SoftDogNoRights, Resource1.Form_Alert);
                    return false;
                }
                else if (info.ExpiredDate < DateTime.Today && info.ExpiredDate.AddDays(15) >= DateTime.Today) //已经过期
                {
                    DateTime expire = info.ExpiredDate.AddDays(15);
                    TimeSpan ts = new TimeSpan(expire.Ticks - DateTime.Today.Ticks);
                    MessageBox.Show(string.Format(Resource1.FrmMain_SoftDogExpiredAlert, (int)(ts.TotalDays + 1)), Resource1.Form_Alert);
                }
                else if (info.ExpiredDate.AddDays(15) < DateTime.Today)
                {
                    MessageBox.Show(Resource1.FrmMain_SoftDogExpired, Resource1.Form_Alert);
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true ;
        }

        /// <summary>
        /// 检查加密狗
        /// </summary>
        /// <returns></returns>
        public static bool Check()
        {
            bool result = true ;

            SoftDogReader reader = new SoftDogReader();
            SoftDogInfo info = null;
            try
            {
                info = reader.ReadDog();
                if (info == null)
                {
                    MessageBox.Show(Resource1.FrmMain_SoftDogError, Resource1.Form_Alert);
                    result = false;
                }
                else if ((info.SoftwareList & SoftwareType.TYPE_PARK) == 0)  //没有写停车场权限
                {
                    MessageBox.Show(Resource1.FrmMain_SoftDogNoRights, Resource1.Form_Alert);
                    result = false;
                }
                else if (info.ExpiredDate < DateTime.Today && info.ExpiredDate.AddDays(15) >= DateTime.Today) //已经过期
                {
                    DateTime expire = info.ExpiredDate.AddDays(15);
                    TimeSpan ts = new TimeSpan(expire.Ticks - DateTime.Today.Ticks);
                    MessageBox.Show(string.Format(Resource1.FrmMain_SoftDogExpiredAlert, (int)(ts.TotalDays + 1)), Resource1.Form_Alert);
                    result = false;
                }
                else if (info.ExpiredDate.AddDays(15) < DateTime.Today)
                {
                    MessageBox.Show(Resource1.FrmMain_SoftDogExpired, Resource1.Form_Alert);
                    result = false;
                }
            }
            catch (InvalidOperationException ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                result = false;
            }

            return result;
        }
    }
}
