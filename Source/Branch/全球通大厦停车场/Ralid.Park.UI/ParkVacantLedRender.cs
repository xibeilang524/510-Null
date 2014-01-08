using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary.LED;
using Ralid.GeneralLibrary.ExceptionHandling;

namespace Ralid.Park.UI
{
    /// <summary>
    /// 表示
    /// </summary>
    class ParkVacantLedRender : Ralid.Park.BusinessModel.Interface.IReportHandler
    {
        #region 构造函数
        public ParkVacantLedRender()
        {
            InitLED();
        }
        #endregion

        #region 私有变量
        private IParkingLed _ParkFullLed;
        private ParkFullLed _DixiaKongjian;  //地下空间用
        #endregion

        #region 私有方法
        private void InitLED()
        {
            try
            {
                //满位显示屏
                if (AppSettings.CurrentSetting.ParkFullLedCOMPort > 0)
                {
                    _ParkFullLed = new ZhongKuangLed(AppSettings.CurrentSetting.ParkFullLedCOMPort);
                    _ParkFullLed.Open();
                    if (ParkBuffer.Current.Parks.Count == 1)  //显示车位余数
                    {
                        ParkInfo park = ParkBuffer.Current.Parks[0];
                        _ParkFullLed.DisplayMsg(string.Format("{0}{1}", park.VacantText, park.Vacant), int.MaxValue);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }

            try
            {
                string filePath = System.IO.Path.Combine(Application.StartupPath, "ParkFullLed.xml");
                if (System.IO.File.Exists(filePath))  //显示在地下空间满位显示屏上
                {
                    _DixiaKongjian = ParkFullLed.Create(filePath);
                    _DixiaKongjian.Open();
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        #region 实现 IReportHandler 接口
        public void ProcessReport(ReportBase report)
        {
            if (report is ParkVacantReport)
            {
                ParkVacantReport r = report as ParkVacantReport;
                ParkInfo park = ParkBuffer.Current.GetPark(r.ParkID);
                if (park != null)
                {
                    if (_ParkFullLed != null)
                    {
                        _ParkFullLed.DisplayMsg(string.Format("{0}{1}", park.VacantText, r.ParkVacant), int.MaxValue);
                    }

                    if (_DixiaKongjian != null)
                    {
                        _DixiaKongjian.DisplayVacantInfo(r.ParkVacant);
                    }
                }
            }
        }
        #endregion
    }
}
