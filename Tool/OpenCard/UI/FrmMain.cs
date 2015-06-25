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
        }

        private bool Authenticate()
        {
            return false;
        }

        private void InitWorkStation()
        {
            this.lblOperator.Text = string.Format("操作员{0}", OperatorInfo.CurrentOperator.OperatorName);
            this.lblStation.Text = string.Format("工作站:{0}", WorkStationInfo.CurrentStation.StationName);
        }

        private WorkStationInfo GetWorkstationID()
        {
            string workstationID = AppSettings.CurrentSetting.WorkstationID;
            WorkstationBll wbll = new WorkstationBll(AppSettings.CurrentSetting.AvailableParkConnect);
            WorkStationInfo w = null;
            if (!string.IsNullOrEmpty(workstationID))
            {
                w = wbll.GetWorkStationByID(workstationID);
            }

            while (w == null)
            {
                FrmWorkstationSelection frm = new FrmWorkstationSelection();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    w = wbll.GetWorkStationByID(frm.SelectedWorkstation);
                    AppSettings.CurrentSetting.WorkstationID = w.StationID;
                }
                else
                {
                    System.Environment.Exit(0);
                }
            }
            return w;
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
            ReadSoftDog();
            this.tmrCheckDog.Enabled = true;

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
            InitSystemParameters(); //初始化系统参数
            //启动同步时间服务
            _DatetimeSyncService = new DatetimeSyncService(AppSettings.CurrentSetting.ParkConnect);
            _DatetimeSyncService.Start();
            this.lblStartFrom.Text = string.Format("启动时间:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //初始化停车场通讯
            System.Threading.Thread t = new Thread(InitParkingCommunication);
            t.Start();
        }
        #endregion

        private void mnu_ZST_Click(object sender, EventArgs e)
        {
            FrmZSTSetting frm = new FrmZSTSetting();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }
    }
}
