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
            List<ParkInfo> parks = ParkBuffer.Current.Parks;
            if (parks != null && parks.Count == 1)  //满位屏只适用于一个系统中只有一个车场的情况
            {
                ParkVacantRender(parks[0]);
            }
        }

        private void ParkVacantRender(ParkInfo park)
        {
            try
            {
                //满位显示屏
                if (_ParkFullLed == null)
                {
                    if (AppSettings.CurrentSetting.ParkFullLedCOMPort > 0)
                    {
                        if (AppSettings.CurrentSetting.ParkVacantLed == 0)
                        {
                            _ParkFullLed = new ZhongKuangLed(AppSettings.CurrentSetting.ParkFullLedCOMPort);
                            _ParkFullLed.Open();
                        }
                        else if (AppSettings.CurrentSetting.ParkVacantLed == 1)
                        {
                            _ParkFullLed = new KeyTopVacantLed(AppSettings.CurrentSetting.ParkFullLedCOMPort);
                            _ParkFullLed.Open();
                        }
                    }
                }
                if (_ParkFullLed != null)
                {
                    if (_ParkFullLed is KeyTopVacantLed)
                    {
                        _ParkFullLed.DisplayMsg((park.Vacant > 0 ? park.Vacant : 0).ToString("D3"), int.MaxValue);
                    }
                    else
                    {
                        _ParkFullLed.DisplayMsg(string.Format("{0}{1}", park.VacantText, park.Vacant > 0 ? park.Vacant : 0), int.MaxValue);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
        }

        private void DixiakongjianRender(ParkInfo park)
        {
            try
            {
                string filePath = System.IO.Path.Combine(Application.StartupPath, "ParkFullLed.xml");
                if (System.IO.File.Exists(filePath))  //显示在地下空间满位显示屏上
                {
                    if (_DixiaKongjian == null)
                    {
                        _DixiaKongjian = ParkFullLed.Create(filePath);
                        _DixiaKongjian.Open();
                    }
                    _DixiaKongjian.DisplayVacantInfo(park.Vacant);
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
                    List<ParkInfo> parks = ParkBuffer.Current.Parks;
                    if (parks != null && parks.Count == 1)  //满位屏只适用于一个系统中只有一个车场的情况
                    {
                        ParkVacantRender(parks[0]);
                    }
                    DixiakongjianRender(park);
                }
            }
        }
        #endregion
    }
}
