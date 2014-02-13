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
    /// 表示车场直接由电脑控制显示的满位屏管理类
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
        #endregion

        #region 私有方法
        private void InitLED()
        {
            ParkBuffer pb = new ParkBuffer(AppSettings.CurrentSetting.ParkConnect);
            pb.InValid();
            List<ParkInfo> parks =pb.Parks;
            if (parks != null && parks.Count == 1 && AppSettings.CurrentSetting.ParkFullLedCOMPort > 0)
            {
                //目前这个类通过直接从数据库获取数据的方式来实现车位更新
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RenderVacant_Thread));
                t.IsBackground = true;
                t.Start();
            }
        }

        private void RenderVacant_Thread()
        {
            try
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(2000); //每隔2秒更新一次
                    ParkBuffer pb = new ParkBuffer(AppSettings.CurrentSetting.ParkConnect);
                    pb.InValid();
                    List<ParkInfo> parks = pb.Parks;
                    if (parks != null && parks.Count == 1)
                    {
                        ParkVacantRender(parks[0]);
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
        }

        private void ParkVacantRender(ParkInfo park)
        {
            try
            {
                if (AppSettings.CurrentSetting.ParkVacantLed == 0)
                {
                    _ParkFullLed = new ZhongKuangLed(AppSettings.CurrentSetting.ParkFullLedCOMPort);
                }
                else if (AppSettings.CurrentSetting.ParkVacantLed == 1)
                {
                    _ParkFullLed = new KeyTopVacantLed(AppSettings.CurrentSetting.ParkFullLedCOMPort);
                }
                if (_ParkFullLed != null)
                {
                    _ParkFullLed.Open();
                    if (_ParkFullLed is KeyTopVacantLed)
                    {
                        _ParkFullLed.DisplayMsg(park.Vacant.ToString("D3"), int.MaxValue);
                    }
                    else
                    {
                        _ParkFullLed.DisplayMsg(string.Format("{0}{1}", park.VacantText, park.Vacant), int.MaxValue);
                    }
                    _ParkFullLed.Close();
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

        }
        #endregion
    }
}
