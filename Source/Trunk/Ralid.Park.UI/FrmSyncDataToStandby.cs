using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BLL;

namespace Ralid.Park.UI
{
    public partial class FrmSyncDataToStandby : Form
    {
        #region 构造函数
        public FrmSyncDataToStandby()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private DateTime _Begin;
        private Thread Sync_Thread;//同步线程
        #endregion

        #region 私有方法
        private void InitClear()
        {
            this.lblMaster.Text = string.Empty;
            this.lblStandby.Text = string.Empty;
            this.lblSyncMsg.Text = string.Empty;
            this.lblTime.Text = string.Empty;
            this.progressBar1.Visible = false;
        }

        private void LoadDataBaseInfo()
        {
            SqlConnectionStringBuilder msb = new SqlConnectionStringBuilder(AppSettings.CurrentSetting.MasterParkConnect);
            SqlConnectionStringBuilder ssb = new SqlConnectionStringBuilder(AppSettings.CurrentSetting.StandbyParkConnect);
            this.lblMaster.Text = string.Format("{0} [{1}]", msb.DataSource, msb.InitialCatalog);
            this.lblStandby.Text = string.Format("{0} [{1}]", ssb.DataSource, ssb.InitialCatalog);
        }

        private void NotifyProgress(int? max, int? value)
        {
            Action<int?, int?> action = delegate(int? m, int? v)
            {
                if (m.HasValue)
                {
                    this.progressBar1.Maximum = m.Value;
                }
                if (v.HasValue)
                {
                    this.progressBar1.Value = v.Value;
                }
                else
                {
                    this.progressBar1.Value++;
                }
                this.progressBar1.Invalidate();
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, max, value);
            }
            else
            {
                action(max, value);
            }
        }

        private void NotifyMsg(string msg)
        {
            NotifyMsg(msg, Color.Black);
        }

        private void NotifyMsg(string msg,Color color)
        {
            Action<string, Color> action = delegate(string m, Color c)
            {
                if (m != null)
                {
                    this.lblSyncMsg.ForeColor = c;
                    this.lblSyncMsg.Text = m;
                    this.lblSyncMsg.Refresh();
                    NotifyInfo(msg,c);
                }
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, msg, color);
            }
            else
            {
                action(msg, color);
            }
        }

        private void NotifyInfo(string msg)
        {
            NotifyInfo(msg, Color.Black);
        }

        private void NotifyInfo(string msg, Color color)
        {
            Action<string, Color> action = delegate(string m, Color c)
            {
                if (m != null)
                {
                    this.txtInfo.SelectionColor = c;
                    this.txtInfo.AppendText(m);
                    this.txtInfo.AppendText("\r\n");
                    this.txtInfo.ScrollToCaret();
                    this.txtInfo.Refresh();
                }
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, msg, color);
            }
            else
            {
                action(msg, color);
            }
        }

        /// <summary>
        /// 同步系统设置
        /// </summary>
        private bool SyncSystemOptions()
        {
            NotifyMsg(Resources.Resource1.FrmSyncDataToStandby_SynchronizingOptions);
            NotifyProgress(8, 0);

            bool success = true;
            CommandResult result = null;

            SysParaSettingsBll masterssb = new SysParaSettingsBll(AppSettings.CurrentSetting.CurrentMasterConnect);
            SysParaSettingsBll standbyssb = new SysParaSettingsBll(AppSettings.CurrentSetting.CurrentStandbyConnect);

            UserSetting us = masterssb.GetSetting<UserSetting>();
            success = us != null ? success : false;
            if (us != null)
            {
                result = standbyssb.SaveSetting<UserSetting>(us);
                success = result.Result == ResultCode.Successful;
                NotifyProgress(null, null);
            }
            if (us != null && result.Result == ResultCode.Successful)
                NotifyInfo(string.Format("{0}{1}",Resources.Resource1.FrmSyncDataToStandby_UserSetting,Resources.Resource1.Form_Success));
            else
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_UserSetting, Resources.Resource1.Form_Fail), Color.Red);

            HolidaySetting hs = masterssb.GetSetting<HolidaySetting>();
            success = hs != null ? success : false;
            if (hs != null)
            {
                result = standbyssb.SaveSetting<HolidaySetting>(hs);
                success = result.Result == ResultCode.Successful;
                NotifyProgress(null, null);
            }
            if (hs != null && result.Result == ResultCode.Successful)
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_HolidaySetting, Resources.Resource1.Form_Success));
            else
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_HolidaySetting, Resources.Resource1.Form_Fail), Color.Red);

            AccessSetting acs = masterssb.GetSetting<AccessSetting>();
            success = acs != null ? success : false;
            if (acs != null)
            {
                result = standbyssb.SaveSetting<AccessSetting>(acs);
                success = result.Result == ResultCode.Successful;
                NotifyProgress(null, null);
            }
            if (acs != null && result.Result == ResultCode.Successful)
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_AccessSetting, Resources.Resource1.Form_Success));
            else
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_AccessSetting, Resources.Resource1.Form_Fail), Color.Red);

            TariffSetting ts = masterssb.GetSetting<TariffSetting>();
            success = ts != null ? success : false;
            if (ts != null)
            {
                result = standbyssb.SaveSetting<TariffSetting>(ts);
                success = result.Result == ResultCode.Successful;
                NotifyProgress(null, null);
            }
            if (ts != null && result.Result == ResultCode.Successful)
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_TariffSetting, Resources.Resource1.Form_Success));
            else
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_TariffSetting, Resources.Resource1.Form_Fail), Color.Red);

            CarTypeSetting cts = masterssb.GetSetting<CarTypeSetting>();
            success = cts != null ? success : false;
            if (cts != null)
            {
                result = standbyssb.SaveSetting<CarTypeSetting>(cts);
                success = result.Result == ResultCode.Successful;
                NotifyProgress(null, null);
            }
            if (cts != null && result.Result == ResultCode.Successful)
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_CarTypeSetting, Resources.Resource1.Form_Success));
            else
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_CarTypeSetting, Resources.Resource1.Form_Fail), Color.Red);

            CustomCardTypeSetting ccts = masterssb.GetSetting<CustomCardTypeSetting>();
            success = ccts != null ? success : false;
            if (ccts != null)
            {
                result = standbyssb.SaveSetting<CustomCardTypeSetting>(ccts);
                success = result.Result == ResultCode.Successful;
                NotifyProgress(null, null);
            }
            if (ccts != null && result.Result == ResultCode.Successful)
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_CustomCardTypeSetting, Resources.Resource1.Form_Success));
            else
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_CustomCardTypeSetting, Resources.Resource1.Form_Fail), Color.Red);

            BaseCardTypeSetting bcts = masterssb.GetSetting<BaseCardTypeSetting>();
            success = bcts != null ? success : false;
            if (bcts != null)
            {
                result = standbyssb.SaveSetting<BaseCardTypeSetting>(bcts);
                success = result.Result == ResultCode.Successful;
                NotifyProgress(null, null);
            }
            if (bcts != null && result.Result == ResultCode.Successful)
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_BaseCardTypeSetting, Resources.Resource1.Form_Success));
            else
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_BaseCardTypeSetting, Resources.Resource1.Form_Fail), Color.Red);

            KeySetting ks = masterssb.GetSetting<KeySetting>();
            success = ks != null ? success : false;
            if (ks != null)
            {
                result = standbyssb.SaveSetting<KeySetting>(ks);
                success = result.Result == ResultCode.Successful;
                NotifyProgress(null, null);
            }
            if (ks != null && result.Result == ResultCode.Successful)
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_KeySetting, Resources.Resource1.Form_Success));
            else
                NotifyInfo(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_KeySetting, Resources.Resource1.Form_Fail), Color.Red);

            NotifyMsg(Resources.Resource1.FrmSyncDataToStandby_OptionsFinish);

            return success;
        }

        private bool SyncOperators()
        {
            NotifyMsg(string.Format("{0}{1}......",Resources.Resource1.FrmSyncDataToStandby_Synchronizing,Resources.Resource1.FrmSyncDataToStandby_Operator));
            NotifyProgress(100, 0);

            bool success = true;
            CommandResult result = null;

            OperatorBll masterbll = new OperatorBll(AppSettings.CurrentSetting.CurrentMasterConnect);
            OperatorBll standbybll = new OperatorBll(AppSettings.CurrentSetting.CurrentStandbyConnect);

            QueryResultList<OperatorInfo> infos = masterbll.GetAllOperators();
            success = infos.Result == ResultCode.Successful;
            if (success)
            {
                success = standbybll.DeleteAllOperators().Result == ResultCode.Successful;
                if (success)
                {
                    NotifyProgress(infos.QueryObjects.Count, 0);
                    foreach (OperatorInfo info in infos.QueryObjects)
                    {
                        result = standbybll.Insert(info);
                        success = result.Result == ResultCode.Successful;
                        NotifyProgress(null, null);
                        if (!success) break;
                    }
                }
            }

            if (!success)
            {
                NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_Operator, Resources.Resource1.Form_Fail), Color.Red);
            }
            else 
            {
                NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_Operator, Resources.Resource1.Form_Success));
            }

            return success;
        }

        private bool SyncRoles()
        {
            NotifyMsg(string.Format("{0}{1}......",Resources.Resource1.FrmSyncDataToStandby_Synchronizing,Resources.Resource1.FrmSyncDataToStandby_Role));
            NotifyProgress(100, 0);

            bool success = true;
            CommandResult result = null;

            RoleBll masterbll = new RoleBll(AppSettings.CurrentSetting.CurrentMasterConnect);
            RoleBll standbybll = new RoleBll(AppSettings.CurrentSetting.CurrentStandbyConnect);

            QueryResultList<RoleInfo> infos = masterbll.GetAllRoles();
            success = infos.Result == ResultCode.Successful;
            if (success)
            {
                success = standbybll.DeleteAllRoles().Result == ResultCode.Successful;
                if (success)
                {
                    NotifyProgress(infos.QueryObjects.Count, 0);
                    foreach (RoleInfo info in infos.QueryObjects)
                    {
                        result = standbybll.Insert(info);
                        success = result.Result == ResultCode.Successful;
                        NotifyProgress(null, null);
                        if (!success) break;
                    }
                }
            }

            if (!success)
            {
                NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_Role, Resources.Resource1.Form_Fail), Color.Red);
            }
            else
            {
                NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_Role, Resources.Resource1.Form_Success));
            }

            return success;
        }

        private bool SyncWorkStations()
        {
            NotifyMsg(string.Format("{0}{1}......",Resources.Resource1.FrmSyncDataToStandby_Synchronizing,Resources.Resource1.FrmSyncDataToStandby_WorkStation));
            NotifyProgress(100, 0);

            bool success = true;
            CommandResult result = null;

            WorkstationBll masterbll = new WorkstationBll(AppSettings.CurrentSetting.CurrentMasterConnect);
            WorkstationBll standbybll = new WorkstationBll(AppSettings.CurrentSetting.CurrentStandbyConnect);

            QueryResultList<WorkStationInfo> infos = masterbll.GetAllWorkstations();
            success = infos.Result == ResultCode.Successful;
            if (success)
            {
                success = standbybll.DeleteAllWorkStations().Result == ResultCode.Successful;
                if (success)
                {
                    NotifyProgress(infos.QueryObjects.Count, 0);
                    foreach (WorkStationInfo info in infos.QueryObjects)
                    {
                        result = standbybll.Insert(info);
                        success = result.Result == ResultCode.Successful;
                        NotifyProgress(null, null);
                        if (!success) break;
                    }
                }
            }

            if (!success) NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_WorkStation, Resources.Resource1.Form_Fail), Color.Red);
            else NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_WorkStation, Resources.Resource1.Form_Success));

            return success;
        }

        private bool SyncCards()
        {
            NotifyMsg(string.Format("{0}{1}......",Resources.Resource1.FrmSyncDataToStandby_Synchronizing,Resources.Resource1.FrmSyncDataToStandby_Card));
            NotifyProgress(100, 0);

            bool success = true;
            CommandResult result = null;

            CardBll masterbll = new CardBll(AppSettings.CurrentSetting.CurrentMasterConnect);
            CardBll standbybll = new CardBll(AppSettings.CurrentSetting.CurrentStandbyConnect);

            QueryResultList<CardInfo> infos = masterbll.GetAllCards();
            success = infos.Result == ResultCode.Successful;
            if (success)
            {
                success = standbybll.DeteleAllCards().Result == ResultCode.Successful;
                if (success)
                {
                    NotifyProgress(infos.QueryObjects.Count, 0);
                    foreach (CardInfo info in infos.QueryObjects)
                    {
                        result = standbybll.Insert(info);
                        success = result.Result == ResultCode.Successful;
                        NotifyProgress(null, null);
                        if (!success) break;
                    }
                }
            }

            if (!success) NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_Card, Resources.Resource1.Form_Fail), Color.Red);
            else NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_Card, Resources.Resource1.Form_Success));

            return success;
        }

        private bool SyncParks()
        {
            NotifyMsg(string.Format("{0}{1}......",Resources.Resource1.FrmSyncDataToStandby_Synchronizing,Resources.Resource1.FrmSyncDataToStandby_Park));
            NotifyProgress(100, 0);

            bool success = true;
            CommandResult result = null;

            ParkBll masterbll = new ParkBll(AppSettings.CurrentSetting.CurrentMasterConnect);
            ParkBll standbybll = new ParkBll(AppSettings.CurrentSetting.CurrentStandbyConnect);

            QueryResultList<ParkInfo> infos = masterbll.GetAllParks();
            success = infos.Result == ResultCode.Successful;
            if (success)
            {
                success = standbybll.DeleteAllParks().Result == ResultCode.Successful;
                if (success)
                {
                    NotifyProgress(infos.QueryObjects.Count, 0);
                    foreach (ParkInfo info in infos.QueryObjects)
                    {
                        result = standbybll.InsertWithPrimaryKey(info);
                        success = result.Result == ResultCode.Successful;
                        NotifyProgress(null, null);
                        if (!success) break;
                    }
                }

            }
            if (!success) NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_Park, Resources.Resource1.Form_Fail), Color.Red);
            else NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_Park, Resources.Resource1.Form_Success));

            return success;
        }

        private bool SyncEntrances()
        {
            NotifyMsg(string.Format("{0}{1}......",Resources.Resource1.FrmSyncDataToStandby_Synchronizing,Resources.Resource1.FrmSyncDataToStandby_Entrance));
            NotifyProgress(100, 0);

            bool success = true;
            CommandResult result = null;

            EntranceBll masterbll = new EntranceBll(AppSettings.CurrentSetting.CurrentMasterConnect);
            EntranceBll standbybll = new EntranceBll(AppSettings.CurrentSetting.CurrentStandbyConnect);

            QueryResultList<EntranceInfo> infos = masterbll.GetAllEntraces();
            success = infos.Result == ResultCode.Successful;
            if (success)
            {
                success = standbybll.DeleteAllEntrances().Result == ResultCode.Successful;
                if (success)
                {
                    NotifyProgress(infos.QueryObjects.Count, 0);
                    foreach (EntranceInfo info in infos.QueryObjects)
                    {
                        result = standbybll.InsertWithPrimaryKey(info);
                        success = result.Result == ResultCode.Successful;
                        NotifyProgress(null, null);
                        if (!success) break;
                    }
                }

            }
            if (!success) NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_Entrance, Resources.Resource1.Form_Fail), Color.Red);
            else NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_Entrance, Resources.Resource1.Form_Success));

            return success;
        }

        private bool SyncVideoSources()
        {
            NotifyMsg(string.Format("{0}{1}......",Resources.Resource1.FrmSyncDataToStandby_Synchronizing,Resources.Resource1.FrmSyncDataToStandby_Video));
            NotifyProgress(100, 0);

            bool success = true;
            CommandResult result = null;

            VideoSourceBll masterbll = new VideoSourceBll(AppSettings.CurrentSetting.CurrentMasterConnect);
            VideoSourceBll standbybll = new VideoSourceBll(AppSettings.CurrentSetting.CurrentStandbyConnect);

            QueryResultList<VideoSourceInfo> infos = masterbll.GetAllVideoSources();
            success = infos.Result == ResultCode.Successful;
            if (success)
            {
                success = standbybll.DeleteAllVideos().Result == ResultCode.Successful;
                if (success)
                {
                    NotifyProgress(infos.QueryObjects.Count, 0);
                    foreach (VideoSourceInfo info in infos.QueryObjects)
                    {
                        result = standbybll.InsertWithPrimaryKey(info);
                        success = result.Result == ResultCode.Successful;
                        NotifyProgress(null, null);
                        if (!success) break;
                    }
                }

            }
            if (!success) NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_Video, Resources.Resource1.Form_Fail), Color.Red);
            else NotifyMsg(string.Format("{0}{1}", Resources.Resource1.FrmSyncDataToStandby_Video, Resources.Resource1.Form_Success));

            return success;
        }

        private void SyncDataBase_Thread()
        {
            try
            {
                bool success = true;
                if (this.chkSystemOptions.Checked) success = SyncSystemOptions();
                if (this.chkOperators.Checked) success = SyncOperators();
                if (this.chkRoles.Checked) success = SyncRoles();
                if (this.chkWorkStations.Checked) success = SyncWorkStations();
                if (this.chkParks.Checked) success = SyncParks();
                if (this.chkEntrances.Checked) success = SyncEntrances();
                if (this.chkVideoSources.Checked) success = SyncVideoSources();
                if (this.chkCards.Checked) success = SyncCards();

                NotifyMsg(Resources.Resource1.FrmSyncDataToStandby_DataFinish);

                Action action = delegate()
                {
                    this.timer1.Enabled = false;
                    this.btnStart.Enabled = true;
                    this.gbSyncSelect.Enabled = true;
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
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        #region 窗体事件
        private void FrmSyncDataToStandby_Load(object sender, EventArgs e)
        {
            InitClear();
            LoadDataBaseInfo();
        }
        private void FrmSyncDataToStandby_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Sync_Thread != null && (Sync_Thread.ThreadState == ThreadState.Running || Sync_Thread.ThreadState == ThreadState.WaitSleepJoin))
            {
                string msg=string.Format("{0}\r\n{1}",Resources.Resource1.FrmSyncDataToStandby_CloseAlarm1,Resources.Resource1.FrmSyncDataToStandby_CloseAlarm2);
                if (MessageBox.Show(msg,
                    Resources.Resource1.Form_Alert, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                    == DialogResult.Yes)
                {
                    Sync_Thread.Abort();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            if (this.timer1.Enabled) this.timer1.Enabled = false;
            this.btnStart.Enabled = OperatorInfo.CurrentOperator.Role.Permit(BusinessModel.Enum.Permission.SyncDataToStandby);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.Resource1.FrmSyncDataToStandby_Cover,Resources.Resource1.Form_Query, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2)
                == DialogResult.No) return;

            this.gbSyncSelect.Enabled = false;
            this.btnStart.Enabled = false;
            this.lblSyncMsg.Text = string.Empty;
            this.lblTime.Text = string.Format("{0}：00:00:00", Resources.Resource1.FrmSyncDataToStandby_Time);
            this.txtInfo.Clear();

            _Begin = DateTime.Now;
            this.timer1.Enabled = true;
            this.progressBar1.Visible = true;

            Sync_Thread = new Thread(SyncDataBase_Thread);
            Sync_Thread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            Sync_Thread.CurrentUICulture = Thread.CurrentThread.CurrentCulture; ;
            Sync_Thread.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - _Begin.Ticks);
            this.lblTime.Text = string.Format("{0}：{1}", Resources.Resource1.FrmSyncDataToStandby_Time, ts.ToString(@"hh\:mm\:ss"));
        }
        #endregion



    }
}
