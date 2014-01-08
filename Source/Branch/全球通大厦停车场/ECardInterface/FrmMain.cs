using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;
using Ralid.Park.DAL.IDAL;

namespace ECardInterface
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private Form _FrmEcardRecords = null;

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
            catch (Exception ex)
            {
                return false;
            }
        }

        private void SyncCardEventThread()
        {
            ShowMessage("启动事件上传线程");
            while (true)
            {
                try
                {
                    IECardRecordProvider provider = ProviderFactory.Create<IECardRecordProvider>(AppSettings.CurrentSetting.ParkConnect);
                    List<ECardRecord> records = provider.GetAll().QueryObjects;
                    if (records != null && records.Count > 0)
                    {
                        foreach (ECardRecord record in records)
                        {
                            bool ret = SyncToECar(record);
                            provider.Delete(record);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
                Thread.Sleep(5000);
            }
        }

        private void ParkfullCheckThread()
        {
            ShowMessage("启动车场满位短信通知线程");
            Ralid.Park.BusinessModel.Model.TimeZone curTz = null;
            while (true)
            {
                try
                {
                    if (Mysetting.Current != null && Mysetting.Current.ParkfullCheckTimezones != null && Mysetting.Current.ParkfullCheckTimezones.Count > 0)
                    {
                        List<Ralid.Park.BusinessModel.Model.TimeZone> tzs = Mysetting.Current.ParkfullCheckTimezones.Where(tz => tz.IsIn(DateTime.Now)).ToList();
                        if (tzs != null && tzs.Count > 0)
                        {
                            List<ParkInfo> parks = (new ParkBll(AppSettings.CurrentSetting.ParkConnect)).GetAllParks().QueryObjects;
                            if (parks != null && parks.Count > 0 && parks.Exists(park => park.Vacant == 0) && !tzs[0].Equals(curTz))
                            {
                                string msg = string.Empty;
                                List<ParkInfo> temp1 = parks.Where(park => park.Vacant == 0).ToList();
                                List<ParkInfo> temp2 = parks.Where(park => park.Vacant > 0).ToList();
                                if (temp2 != null && temp2.Count > 0)
                                {
                                    msg = string.Format("{0} 车位已满,各位可以将车停放至 {1}", GetParkNameString(temp1), GetParkNameString(temp2));
                                }
                                else
                                {
                                    msg = string.Format("{0} 车位已满,各位可以将车停放至周边其它停车场", GetParkNameString(temp1));
                                }
                                CardSearchCondition con = new CardSearchCondition();
                                con.Status = Ralid.Park.BusinessModel.Enum.CardStatus.Enabled;
                                con.ParkingStatus = Ralid.Park.BusinessModel.Enum.ParkingStatus.Out;
                                List<CardInfo> cards = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCards(con).QueryObjects;
                                if (cards != null && cards.Count > 0)
                                {
                                    if (NotifyECar(msg, cards))
                                    {
                                        curTz = tzs[0];
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
                Thread.Sleep(5000);
            }
        }

        private string GetParkNameString(List<ParkInfo> parks)
        {
            string temp = string.Empty;
            if (parks != null && parks.Count > 0)
            {
                for (int i = 0; i < parks.Count; i++)
                {
                    if (i == 0)
                    {
                        temp = parks[i].ParkName;
                    }
                    else
                    {
                        temp += "," + parks[i].ParkName;
                    }
                }
            }
            return temp;
        }

        private string GetCardIDString(List<CardInfo> cards)
        {
            string temp = string.Empty;
            if (cards != null && cards.Count > 0)
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    if (i == 0)
                    {
                        temp = cards[i].CardID;
                    }
                    else
                    {
                        temp += "," + cards[i].CardID;
                    }
                }
            }
            return temp;
        }

        private bool SyncToECar(ECardRecord record)
        {
            try
            {
                double limitationRemain = (double)(record.LimitationRemain == null ? 35 : record.LimitationRemain.Value);

                int sheetID = 0;
                if (!string.IsNullOrEmpty(record.SheetID)) int.TryParse(record.SheetID, out sheetID);
                ECardService.NightParkingSoapClient client = new ECardService.NightParkingSoapClient();
                ECardService.ResultMessage ret = client.AddParkingHistory(record.CardID,
                                                                          sheetID,
                                                                          record.Carplate,
                                                                          record.EnterDt.Value,
                                                                          record.EventDt,
                                                                          limitationRemain,
                                                                          string.Empty,
                                                                          string.Empty,
                                                                          string.Empty);
                if (!ret.status)
                {
                    ShowMessage("上传车辆出场记录失败,原因:" + ret.Message, Color.Red);
                    Ralid.GeneralLibrary.LOG.FileLog.Log("一车通上传", ret.Message);
                }
                return ret.status;
            }
            catch (Exception ex)
            {
                ShowMessage("上传车辆出场记录调用出现异常:" + ex.Message, Color.Red);
            }
            return false;
        }

        private bool NotifyECar(string msg, List<CardInfo> cards)
        {
            try
            {
                ECardService.ArrayOfString strs = new ECardService.ArrayOfString();
                //strs.AddRange(cards.Select(item => item.CardID));
                strs.Add("0121010274");
                strs.Add("0121010274");
                ECardService.NightParkingSoapClient client = new ECardService.NightParkingSoapClient();
                ECardService.ResultMessage ret = client.ParkingLotFullSMS(msg, strs);
                if (!ret.status)
                {
                    ShowMessage("发送车位满位通知失败,原因:" + ret.Message, Color.Red);
                    Ralid.GeneralLibrary.LOG.FileLog.Log("一车通上传", ret.Message);
                }
                return ret.status;
            }
            catch (Exception ex)
            {
                ShowMessage("发送车位满位通知调用出现异常:" + ex.Message, Color.Red);
            }
            return false;
        }

        private void ShowMessage(string msg, Color color)
        {
            Action action = delegate()
            {
                this.eventReportListBox1.InsertMessage(string.Format("{0} > {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg), color);
            };
            if (this.eventReportListBox1.InvokeRequired)
            {
                this.Invoke(action);
            }
            else
            {
                action();
            }
        }

        private void ShowMessage(string msg)
        {
            ShowMessage(msg, Color.Black);
        }
        #endregion

        #region 事件处理程序
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = "E车通同步工具 (" + Application.ProductVersion + ")";
            Console.WriteLine(AppSettings.CurrentSetting.ParkConnect);
            if (string.IsNullOrEmpty(AppSettings.CurrentSetting.ParkConnect) || !CheckConnect(AppSettings.CurrentSetting.ParkConnect))
            {
                FrmConnect frm = new FrmConnect();
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    mnu_Exit_Click(this.mnu_Exit, EventArgs.Empty);
                    return;
                }
            }

            Mysetting.Current = (new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect)).GetOrCreateSetting<Mysetting>();
            ShowMessage("软件启动");

            //Thread t1 = new Thread(new ThreadStart(SyncCardEventThread));
            //t1.IsBackground = true;
            //t1.Start();


            Thread t2 = new Thread(new ThreadStart(ParkfullCheckThread));
            t2.IsBackground = true;
            t2.Start();
        }

        private void mnu_Exit_Click(object sender, EventArgs e)
        {
            this.notify1.Dispose();
            Environment.Exit(0);
        }

        private void notify1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.ShowInTaskbar = true;
            this.Activate();
            this.Show();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    this.Hide();
            //    this.ShowInTaskbar = false;
            //    e.Cancel = true;
            //}
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.notify1.Dispose();
            Environment.Exit(0);
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
        }
        #endregion

        private void mnu_Connect_Click(object sender, EventArgs e)
        {
            FrmConnect frm = new FrmConnect();
            if (frm.ShowDialog() != DialogResult.OK)
            {
                mnu_Exit_Click(this.mnu_Exit, EventArgs.Empty);
                return;
            }
        }

        private void mnu_Para_Click(object sender, EventArgs e)
        {
            FrmParameter frm = new FrmParameter();
            frm.ShowDialog();
        }

        private void mnu_NotifyTest_Click(object sender, EventArgs e)
        {
            try
            {
                FrmNotifyTest frm = new FrmNotifyTest();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //调用E车通接口
                    ECardService.ArrayOfString cards = new ECardService.ArrayOfString();
                    cards.Add(frm.CardID);
                    ECardService.NightParkingSoapClient client = new ECardService.NightParkingSoapClient();
                    ECardService.ResultMessage ret = client.ParkingLotFullSMS(frm.Msg, cards);
                    if (ret.status)
                    {
                        MessageBox.Show("发送短信成功");
                    }
                    else
                    {
                        MessageBox.Show("发送短信失败:" + ret.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("短信测试调用出现异常:" + ex.Message);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //调用E车通接口
                ECardService.NightParkingSoapClient client = new ECardService.NightParkingSoapClient();
                string ret = client.HelloWorld();
                MessageBox.Show("连通测试返回:" + ret);
            }
            catch (Exception ex)
            {
                MessageBox.Show("连通测试调用出现异常:" + ex.Message);
            }
        }

        private void mnu_EcardRecords_Click(object sender, EventArgs e)
        {
            if (_FrmEcardRecords == null)
            {
                _FrmEcardRecords = new FrmEcardRecords();
                _FrmEcardRecords.FormClosed += delegate(object o, FormClosedEventArgs args)
                {
                    _FrmEcardRecords = null;
                };
            }
            _FrmEcardRecords.Show();
            _FrmEcardRecords.Activate();
        }
    }
}
