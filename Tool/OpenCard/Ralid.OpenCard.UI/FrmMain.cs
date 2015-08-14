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
using Ralid.Park.BLL;
using Ralid.Park.ParkAdapter;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.OpenCard.OpenCardService;
using Ralid.OpenCard.OpenCardService.YCT;

namespace Ralid.OpenCard.UI
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        #region 私有变量
        private DataBaseConnectionChecker _DBChecker = null;
        private DatetimeSyncService _DatetimeSyncService;
        #endregion

        #region 私有方法
        private void ReadSoftDog()
        {
            //if (!ParkingSoftDogVerify.VerifyRight())
            //{
            //    System.Environment.Exit(0);
            //}
        }

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

        private bool UpGradeDataBase()
        {
            bool result = false;
            string path = System.IO.Path.Combine(Environment.CurrentDirectory, "DbUpdate.sql");
            if (System.IO.File.Exists(path))
            {
                SqlClientHelper.SqlClient client = new SqlClientHelper.SqlClient(AppSettings.CurrentSetting.MasterParkConnect);
                client.Connect();
                client.ExecuteSQLFile(path);
            }
            return result;
        }

        private void InitSystemParameters()
        {
            //初始化系统设置
            SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.AvailableParkConnect);
            UserSetting.Current = ssb.GetOrCreateSetting<UserSetting>();
            CustomCardTypeSetting.Current = ssb.GetOrCreateSetting<CustomCardTypeSetting>();
        }

        private void SetCurrentOperator()
        {
            string optID = AppSettings.CurrentSetting.GetConfigContent("OperatorID");
            if (!string.IsNullOrEmpty(optID)) OperatorInfo.CurrentOperator = new OperatorBll(AppSettings.CurrentSetting.MasterParkConnect).GetByID(optID).QueryObject;
            if (OperatorInfo.CurrentOperator == null) mnu_SelOperator.PerformClick();
            this.lblOperator.Text = string.Format("操作员：{0}", OperatorInfo.CurrentOperator.OperatorName);
        }

        private void SetCurrentStation()
        {
            string workstationID = AppSettings.CurrentSetting.WorkstationID;
            WorkstationBll wbll = new WorkstationBll(AppSettings.CurrentSetting.MasterParkConnect);
            if (!string.IsNullOrEmpty(workstationID)) WorkStationInfo.CurrentStation = wbll.GetWorkStationByID(workstationID);
            if (WorkStationInfo.CurrentStation == null) mnu_SelStation.PerformClick();
            this.lblStation.Text = string.Format("工作站：{0}", WorkStationInfo.CurrentStation.StationName);
        }

        private void InitParkPanel()
        {
            foreach (ParkInfo park in ParkBuffer.Current.Parks)
            {
                if (park.IsRootPark)
                {
                    AddPark(park);
                }
            }
        }

        private void InitParkingCommunication(object obj)
        {
            foreach (ParkInfo park in ParkBuffer.Current.Parks)
            {
                if (park.IsRootPark)
                {
                    ParkingAdapter pad = new ParkingAdapter(park.ParkAdapterUri);
                    pad.ParkID = park.ParkID;
                    ParkingAdapterManager.Instance.Add(park.ParkID, pad);
                    pad.Reporting += ProcessReport;
                    pad.ParkAdapterConnectFail += new EventHandler(pad_ParkAdapterConnectFail);
                    pad.ParkApaterReconnected += new EventHandler(pad_ParkApaterReconnected);
                    pad.ConnectServer();
                }
            }
        }

        private void pad_ParkApaterReconnected(object sender, EventArgs e)
        {
            this.Invoke((Action)(() =>
                {
                    foreach (Control c in pnlPark.Controls)
                    {
                        if (c is PictureBox)
                        {
                            ParkInfo park = c.Tag as ParkInfo;
                            if (park != null && park.ParkID == (sender as ParkingAdapter).ParkID)
                            {
                                (c as PictureBox).Image = global::Ralid.OpenCard.UI.Properties.Resources.serverOK;
                            }
                        }
                    }
                }));
        }

        private void pad_ParkAdapterConnectFail(object sender, EventArgs e)
        {
            this.Invoke((Action)(() =>
                {
                    foreach (Control c in pnlPark.Controls)
                    {
                        if (c is PictureBox)
                        {
                            ParkInfo park = c.Tag as ParkInfo;
                            if (park != null && park.ParkID == (sender as ParkingAdapter).ParkID)
                            {
                                (c as PictureBox).Image = global::Ralid.OpenCard.UI.Properties.Resources.serverFail;
                            }
                        }
                    }
                } ));
        }

        private void InitOpenCardServices(object obj)
        {
            OpenCardMessageHandler handler = new OpenCardMessageHandler();
            GlobalSettings.Current.Set<OpenCardMessageHandler>(handler);
            handler.OnReadCard += new EventHandler<OpenCardEventArgs>(handler_OnReadCard);
            handler.OnPaying += new EventHandler<OpenCardEventArgs>(handler_OnPaying);
            handler.OnPaidOk += new EventHandler<OpenCardEventArgs>(handler_OnPaidOk);
            handler.OnPaidFail += new EventHandler<OpenCardEventArgs>(handler_OnPaidFail);
            handler.OnError += new EventHandler<OpenCardEventArgs>(handler_OnError);

            SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.AvailableParkConnect);
            Ralid.OpenCard.OpenCardService.ZSTSetting zst = ssb.GetSetting<Ralid.OpenCard.OpenCardService.ZSTSetting>();
            if (zst != null)
            {
                handler.Init(zst);
            }
            YiTingShanFuSetting yt = ssb.GetSetting<YiTingShanFuSetting>();
            if (yt != null)
            {
                AppSettings.CurrentSetting.GetYiTingConfig(yt);
                handler.Init(yt);
            }
            YCTSetting yct = ssb.GetSetting<YCTSetting>();
            if (yct != null)
            {
                handler.Init(yct);
            }
        }

        private void handler_OnError(object sender, OpenCardEventArgs e)
        {
            if (chkOpenEvent.Checked) InsertMessage(string.Format("【{0} ＠ {1}】 发生错误 {2}",
                                                 DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                 e.EntranceName,
                                                 e.LastError), Color.Red);
        }

        private void handler_OnPaidFail(object sender, OpenCardEventArgs e)
        {
            if (chkOpenEvent.Checked) InsertMessage(string.Format("【{0} ＠ {1}】 缴费失败 卡号:{2} 原因:{3}",
                                                  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                  e.EntranceName,
                                                  e.CardID,
                                                  e.LastError), Color.Blue);
        }

        private void handler_OnPaidOk(object sender, OpenCardEventArgs e)
        {
            if (chkOpenEvent.Checked) InsertMessage(string.Format("【{0} ＠ {1}】 缴费成功 卡号:{2} 实收:{3} 余额:{4}",
                                                  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                  e.EntranceName,
                                                  e.CardID,
                                                  e.Paid,
                                                  e.Balance.ToString("F2")), Color.Blue);
        }

        private void handler_OnPaying(object sender, OpenCardEventArgs e)
        {
            if (chkOpenEvent.Checked) InsertMessage(string.Format("【{0} ＠ {1}】 查询费用 卡号:{2} 应收:{3}元",
                                                  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                  e.EntranceName,
                                                  e.CardID,
                                                  e.Payment != null ? e.Payment.Accounts : 0), Color.Blue);
        }

        private void handler_OnReadCard(object sender, OpenCardEventArgs e)
        {
            if (chkOpenEvent.Checked) InsertMessage(string.Format("【{0} ＠ {1}】 入场读卡 卡号:{2} 余额:{3}",
                                                  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                                  e.EntranceName,
                                                  e.CardID,
                                                  e.Balance.ToString("F2")), Color.Blue);
        }

        private void InsertMessage(string msg, Color color)
        {
            Action action = delegate()
            {
                eventList.InsertMessage(msg, color);
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

        private void ProcessReport(object sender, ReportBase report)
        {
            if (!chkCardEvent.Checked) return;
            InsertMessage(report.Description, Color.Black);
            //if (report is CardEventReport)
            //{
            //    CardEventReport cr = report as CardEventReport;
            //    if (cr.CardType != null && (cr.CardType.Name == "中山通" || cr.CardType.Name == "闪付卡" || cr.CardType.Name == "羊城通卡"))
            //    {
            //        InsertMessage(cr.Description, Color.Black);
            //    }
            //}
        }

        private void AddPark(ParkInfo park)
        {
            PictureBox pic = new PictureBox();
            pic.Tag = park;
            pic.Image = global::Ralid.OpenCard.UI.Properties.Resources.serverFail;
            pic.MouseEnter += new EventHandler(pic_MouseEnter);
            pic.Dock = DockStyle.Left;
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.BorderStyle = BorderStyle.FixedSingle;
            this.pnlPark .Controls.Add(pic);
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            ParkInfo park = (sender as PictureBox).Tag as ParkInfo;
            this.toolTip1.SetToolTip(sender as Control, park.ParkName);
        }
        #endregion

        #region 事件处理程序
        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text += string.Format(" [{0}]", Application.ProductVersion);
            //用于所有工作站软件都要加密狗的情形
            //ReadSoftDog();
            //this.tmrCheckDog.Enabled = true;

            if (string.IsNullOrEmpty(AppSettings.CurrentSetting.MasterParkConnect) || !CheckConnect(AppSettings.CurrentSetting.MasterParkConnect))
            {
                FrmConnect frm = new FrmConnect();
                if (frm.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
            }
            UpGradeDataBase(); //生成需要的一些表
            ParkBuffer.Current = new ParkBuffer(AppSettings.CurrentSetting.MasterParkConnect);
            ParkBuffer.Current.InValid(AppSettings.CurrentSetting.MasterParkConnect);  //获取所有硬件信息
            SetCurrentOperator(); //设置当前操作员
            SetCurrentStation(); //设置当前工作站
            InitSystemParameters(); //初始化系统参数
            //启动同步时间服务
            _DatetimeSyncService = new DatetimeSyncService(AppSettings.CurrentSetting.ParkConnect);
            _DatetimeSyncService.Start();
            this.lblStartFrom.Text = string.Format("启动时间:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //初始化停车场通讯
            InitParkPanel();
            ThreadPool.QueueUserWorkItem((WaitCallback)InitParkingCommunication);
            //初始化开放卡片服务
            ThreadPool.QueueUserWorkItem((WaitCallback)InitOpenCardServices);
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void mnu_SelOperator_Click(object sender, EventArgs e)
        {
            while (true)
            {
                FrmOperatorSelection frm = new FrmOperatorSelection();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
                OperatorInfo.CurrentOperator = frm.SelectedOperator;
                if (OperatorInfo.CurrentOperator != null)
                {
                    AppSettings.CurrentSetting.SaveConfig("OperatorID", OperatorInfo.CurrentOperator.OperatorID);
                    this.lblOperator.Text = string.Format("操作员：{0}", OperatorInfo.CurrentOperator.OperatorName);
                    break;
                }
            }
        }

        private void mnu_SelStation_Click(object sender, EventArgs e)
        {
            while (true)
            {
                FrmWorkstationSelection frm = new FrmWorkstationSelection();
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
                WorkStationInfo.CurrentStation = new WorkstationBll(AppSettings.CurrentSetting.ParkConnect).GetWorkStationByID(frm.SelectedWorkstation);
                if (WorkStationInfo.CurrentStation != null)
                {
                    AppSettings.CurrentSetting.WorkstationID = WorkStationInfo.CurrentStation.StationID;
                    this.lblStation.Text = string.Format("工作站：{0}", WorkStationInfo.CurrentStation.StationName);
                    break;
                }
            }
        }

        private void mnu_YCT_Click(object sender, EventArgs e)
        {
            FrmYCTSetting frm = new FrmYCTSetting();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void mnu_ZST_Click(object sender, EventArgs e)
        {
            FrmZSTSetting frm = new FrmZSTSetting();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void mnu_YiTing_Click(object sender, EventArgs e)
        {
            FrmYiTingSetting frm = new FrmYiTingSetting();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
        #endregion
    }
}
