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

        private void InitSystemParameters()
        {
            //初始化系统设置
            SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.AvailableParkConnect);
            UserSetting.Current = ssb.GetOrCreateSetting<UserSetting>();
            HolidaySetting.Current = ssb.GetOrCreateSetting<HolidaySetting>();
            AccessSetting.Current = ssb.GetOrCreateSetting<AccessSetting>();
            TariffSetting.Current = ssb.GetOrCreateSetting<TariffSetting>();
            CarTypeSetting.Current = ssb.GetOrCreateSetting<CarTypeSetting>();
            CustomCardTypeSetting.Current = ssb.GetOrCreateSetting<CustomCardTypeSetting>();
            BaseCardTypeSetting.Current = ssb.GetOrCreateSetting<BaseCardTypeSetting>();
            if (GlobalSettings.Current == null) GlobalSettings.Current = new GlobalSettings();
            GlobalSettings.Current.Set<ZSTSetting>(ssb.GetSetting<ZSTSetting>());
            GlobalSettings.Current.Set<YiTingShanFuSetting>(ssb.GetSetting<YiTingShanFuSetting>());
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

        private void InitParkingCommunication()
        {
            foreach (ParkInfo park in ParkBuffer.Current.Parks)
            {
                if (park.IsRootPark)
                {
                    ParkingAdapter pad = new ParkingAdapter(park.ParkAdapterUri);
                    pad.ParkID = park.ParkID;
                    ParkingAdapterManager.Instance.Add(park.ParkID, pad);
                    //pad.Reporting += ProcessReport;
                    //pad.ParkAdapterConnectFail += new EventHandler(pad_ParkAdapterConnectFail);
                    //pad.ParkApaterReconnected += new EventHandler(pad_ParkApaterReconnected);
                    pad.ConnectServer();
                }
            }
        }

        private void ProcessReport(object sender, ReportBase report)
        {

        }
        #endregion

        #region 事件处理程序
        private void FrmMain_Load(object sender, EventArgs e)
        {
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
            System.Threading.Thread t = new Thread(InitParkingCommunication);
            t.Start();
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
