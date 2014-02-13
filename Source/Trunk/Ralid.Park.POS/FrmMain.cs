using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.GeneralLibrary;
using Ralid.GeneralLibrary.SoftDog;

namespace Ralid.Park.POS
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        #region 私有变量
        private List<Form> _openedForms = new List<Form>();
        #endregion

        #region 私有方法
        private bool CheckConnect(string conStr)
        {
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.ConnectionString = conStr;
                    con.Open();
                    con.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void SetValue(POS.Model.MySetting mysetting, UserSetting us)
        {
            mysetting.WegenType = (POS.Model.WegenType)us.WegenType;
        }

        private void SetValue(POS.Model.MySetting mysetting, KeySetting ks)
        {
            mysetting.ParkingKey = ks.ParkingKey;
            mysetting.ParkingSection = ks.ParkingSection;
        }

        private void SetValue(POS.Model.MySetting mysetting, HolidaySetting hs)
        {
            mysetting.SundayIsRest = hs.SundayIsRest;
            mysetting.SaturdayIsRest = hs.SaturdayIsRest;
            if (hs.Holidays != null && hs.Holidays.Count > 0)
            {
                foreach (HolidayInfo h in hs.Holidays)
                {
                    if (mysetting.Holidays == null) mysetting.Holidays = new List<POS.Model.HolidayInfo>();
                    POS.Model.HolidayInfo holiday = new POS.Model.HolidayInfo();
                    holiday.StartDate = h.StartDate;
                    holiday.EndDate = h.EndDate;
                    if (h.WeekenToWorkDayInterval != null && h.WeekenToWorkDayInterval.Count > 0)
                    {
                        foreach (DatetimeInterval di in h.WeekenToWorkDayInterval)
                        {
                            if (holiday.WeekenToWorkDayInterval == null) holiday.WeekenToWorkDayInterval = new List<Ralid.Park.POS.Model.DatetimeInterval>();
                            holiday.WeekenToWorkDayInterval.Add(new Ralid.Park.POS.Model.DatetimeInterval()
                            {
                                Begin = di.Begin,
                                End = di.End
                            });
                        }
                    }
                    mysetting.Holidays.Add(holiday);
                }
            }
        }

        private void SetValue(POS.Model.MySetting mysetting, TariffSetting ts)
        {
            if (ts.TariffOption != null) mysetting.FreeTimeAfterPay = ts.TariffOption.FreeTimeAfterPay;
            if (ts.TariffArray != null && ts.TariffArray.Count > 0)
            {
                foreach (TariffBase tb in ts.TariffArray)
                {
                    if (tb is TariffOfDixiakongjian)
                    {
                        if (mysetting.TariffOfDixiakongjians == null) mysetting.TariffOfDixiakongjians = new List<TariffOfDixiakongjian>();
                        mysetting.TariffOfDixiakongjians.Add(tb as TariffOfDixiakongjian);
                    }
                    else if (tb is TariffOfGuanZhou)
                    {
                        if (mysetting.TariffOfGuanZhous == null) mysetting.TariffOfGuanZhous = new List<TariffOfGuanZhou>();
                        mysetting.TariffOfGuanZhous.Add(tb as TariffOfGuanZhou);
                    }
                    else if (tb is TariffOfLimitation)
                    {
                        if (mysetting.TariffOfLimitations == null) mysetting.TariffOfLimitations = new List<TariffOfLimitation>();
                        mysetting.TariffOfLimitations.Add(tb as TariffOfLimitation);
                    }
                    else if (tb is TariffOfTurningLimited)
                    {
                        if (mysetting.TariffOfTurningLimiteds == null) mysetting.TariffOfTurningLimiteds = new List<TariffOfTurningLimited>();
                        mysetting.TariffOfTurningLimiteds.Add(tb as TariffOfTurningLimited);
                    }
                    else if (tb is TariffOfTurning)
                    {
                        if (mysetting.TariffOfTurnings == null) mysetting.TariffOfTurnings = new List<TariffOfTurning>();
                        mysetting.TariffOfTurnings.Add(tb as TariffOfTurning);
                    }
                    else if (tb is TariffPerDay)
                    {
                        if (mysetting.TariffPerDays == null) mysetting.TariffPerDays = new List<TariffPerDay>();
                        mysetting.TariffPerDays.Add(tb as TariffPerDay);
                    }
                    else if (tb is TariffPerTime)
                    {
                        if (mysetting.TariffPerTimes == null) mysetting.TariffPerTimes = new List<TariffPerTime>();
                        mysetting.TariffPerTimes.Add(tb as TariffPerTime);
                    }
                }
            }
        }

        private void SetValue(POS.Model.MySetting mysetting, List<OperatorInfo> opts)
        {
            mysetting.Operators = (from opt in opts
                                   orderby opt.OperatorName ascending
                                   select new POS.Model.OperatorInfo()
                                 {
                                     OperatorID = opt.OperatorID,
                                     OperatorName = opt.OperatorName,
                                     OperatorNum = opt.OperatorNum,
                                     Password = (new DTEncrypt()).Encrypt(opt.Password),
                                 }
                                 ).ToList();
        }

        private void SavePaymentsToPark(List<string> records)
        {
            FrmProcessing frmP = new FrmProcessing();
            Action action = delegate()
            {
                try
                {
                    int success = 0;
                    int fail = 0;
                    CardPaymentRecordBll bll = new CardPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect);
                    foreach (string record in records)
                    {
                        Ralid.Park.POS.Model.CardPaymentInfo p = Ralid.Park.POS.Model.CardPaymentInfoSerializer.Deserialize(record);
                        if (p != null)
                        {
                            CardPaymentInfo item = CreateFrom(p);
                            CommandResult ret = bll.InsertRecordWithCheck(item);
                            if (ret.Result == ResultCode.Successful) success++; else fail++;
                        }
                        else
                        {
                            fail++;
                        }
                        frmP.ShowProgress(string.Format("数据导入状态 成功:{0}  失败:{1}  总共需导入: {2}", success, fail, records.Count), (decimal)(success + fail) / records.Count);
                    }
                    ShowMessage(string.Format("数据导入状态 成功:{0}  失败:{1}  总共需导入: {2}", success, fail, records.Count), Color.Black);
                }
                catch (ThreadAbortException)
                {
                }
                catch (Exception ex)
                {
                    frmP.ShowProgress(ex.Message, 1);
                    ShowMessage(ex.Message, Color.Red);
                }
            };
            Thread t = new Thread(new ThreadStart(action));
            t.Start();
            if (frmP.ShowDialog() != DialogResult.OK)
            {
                t.Abort();
            }
        }

        private CardPaymentInfo CreateFrom(Ralid.Park.POS.Model.CardPaymentInfo p)
        {
            CardPaymentInfo item = new CardPaymentInfo();
            item.ChargeDateTime = p.ChargeDateTime;
            item.CardID = p.CardID;
            item.EnterDateTime = p.EnterDateTime;
            item.CarPlate = p.CarPlate;
            item.CardType = (Ralid.Park.BusinessModel.Enum.CardType)p.CardType;
            item.CarType = p.CarType;
            item.TariffType = (Ralid.Park.BusinessModel.Enum.TariffType)p.TariffType;
            item.LastTotalPaid = p.LastTotalPaid;
            item.Accounts = p.Accounts;
            item.Paid = p.Paid;
            item.Discount = p.Discount;
            item.PaymentMode = (Ralid.Park.BusinessModel.Enum.PaymentMode)p.PaymentMode;
            item.PaymentCode = (Ralid.Park.BusinessModel.Enum.PaymentCode)p.PaymentCode;
            item.IsCenterCharge = p.IsCenterCharge;
            item.OperatorID = p.Operator;
            item.StationID = p.StationID;
            item.Memo = p.Memo;
            return item;
        }

        private void ShowMessage(string msg, Color color)
        {
            Action action = delegate()
            {
                this.eventList.InsertMessage(string.Format("[{0}]:{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg));
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }
        #endregion

        #region 事件处理程序
        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        private void btnExportSetting_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否需要导出参数?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                string path = System.IO.Path.Combine(Application.StartupPath, "MySetting.xml");
                POS.Model.MySetting mysetting = new Model.MySetting();
                UserSetting us = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<UserSetting>();
                if (us != null) SetValue(mysetting, us);
                KeySetting ks = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<KeySetting>();
                if (ks != null) SetValue(mysetting, ks);
                TariffSetting ts = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<TariffSetting>();
                if (ts != null) SetValue(mysetting, ts);
                HolidaySetting hs = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetSetting<HolidaySetting>();
                if (hs != null) SetValue(mysetting, hs);
                List<OperatorInfo> opts = (new OperatorBll(AppSettings.CurrentSetting.ParkConnect)).GetAllOperators().QueryObjects;
                if (opts != null) SetValue(mysetting, opts);

                XmlSerializer ser = new XmlSerializer(typeof(POS.Model.MySetting));
                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    ser.Serialize(stream, mysetting);
                }
                string remote = @"FlashDisk\RalidPos\MySetting.xml";
                OpenNETCF.Desktop.Communication.RAPI rapi = new OpenNETCF.Desktop.Communication.RAPI();
                if (rapi.DevicePresent)
                {
                    rapi.Connect();
                    rapi.CopyFileToDevice(path, remote, true);
                    rapi.Disconnect();
                }
                ShowMessage("导出参数成功,手持机上的收费软件需要重启后参数才会生效", Color.Black);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, Color.Red);
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void btnCardEvent_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否需要导入收费记录?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                string path = System.IO.Path.Combine(Application.StartupPath, "Record.txt");
                string remote = @"FlashDisk\RalidPos\Record.txt";
                OpenNETCF.Desktop.Communication.RAPI rapi = new OpenNETCF.Desktop.Communication.RAPI();
                if (rapi.DevicePresent)
                {
                    rapi.Connect();
                    rapi.CopyFileFromDevice(path, remote,true);
                    rapi.Disconnect();
                    List<string> records = new List<string>();
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader reader = new StreamReader(fs))
                        {
                            while (!reader.EndOfStream)
                            {
                                string record = reader.ReadLine();
                                if (!string.IsNullOrEmpty(record)) records.Add(record);
                            }
                        }
                    }
                    if (records.Count > 0) SavePaymentsToPark(records);
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, Color.Red);
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        //private void btnCardEvent_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("是否需要导入收费记录?", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        //    try
        //    {
        //        string path = System.IO.Path.Combine(Application.StartupPath, "Record.txt");
        //        string remote = @"FlashDisk\RalidPos\Record.txt";
        //        if (RAPI.CopyFileToLocal(path, remote))
        //        {
        //            List<string> records = new List<string>();
        //            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        //            {
        //                using (StreamReader reader = new StreamReader(fs))
        //                {
        //                    while (!reader.EndOfStream)
        //                    {
        //                        string record = reader.ReadLine();
        //                        if (!string.IsNullOrEmpty(record)) records.Add(record);
        //                    }
        //                }
        //            }
        //            if (records.Count > 0) SavePaymentsToPark(records);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMessage(ex.Message, Color.Red);
        //        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
        //    }
        //}
        #endregion
    }
}
