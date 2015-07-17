using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Notify;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.UI.ReportAndStatistics;
using Ralid.Park.ParkAdapter;
using Ralid.GeneralLibrary.SoftDog;
using Ralid.Park.ThirdCommunication;
using Ralid.Park.UI.Resources;
using Ralid.GeneralLibrary;
using Ralid.Park.PlateRecognition;
using Ralid.Park.UI.OutdoorLed;
using Ralid.GeneralLibrary.CardReader;
using Ralid.Park.LocalDataBase.BLL;
using Ralid.Park.LocalDataBase.Model;
using Ralid.Park.SnapShotCapture;
using Ralid.Park.VideoCapture;

namespace Ralid.Park.UI
{
    public partial class FrmMain : Form
    {
        #region 构造函数
        public FrmMain()
        {
            GetCurrentCulture();
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private List<Form> _openedForms = new List<Form>();
        private List<IReportHandler> _eventHandlers = new List<IReportHandler>();
        //private Form _CarPlateForm;
        private bool _hasDeleted = false;
        private int _ForceShiftingAlarmCount = 0;
        private DatetimeSyncService _DatetimeSyncService;
        private DateTime _StartFrom; //软件启动时间
        private TreeNode _EditNode;
        private DataBaseConnectionChecker _DataBaseConnectionChecker;
        private StandbyToMasterSyncService _StandbyToMasterSyncService;
        private LDB_UpdateLoaclDataService _LDB_UpdateLoaclDataService;
        private bool _InitParkingCommunication = false;//是否初始化了通信
        private DateTime? _OperatorLoginTime;//操作员登录时间
        private Dictionary<int, string> _ParkServerLastIP = new Dictionary<int, string>();//最近一次的服务器使用IP集合
        //private Dictionary<int, HostStandbyStatus> _ParkLastHostStandbyStatus = new Dictionary<int, HostStandbyStatus>();//最近一次的双机热备使用状态集合
        private ServerSwitchRemind _ServerSwitchRemind;//停车场切换提醒
        #endregion

        #region 私有方法
        private void ReadSoftDog()
        {
            //if (!ParkingSoftDogVerify.VerifyRight())
            //{
            //    System.Environment.Exit(0);
            //}
        }

        private void tmrCheckDog_Tick(object sender, EventArgs e)
        {
            tmrCheckDog.Enabled = false;
            ParkingSoftDogVerify.Check();
            tmrCheckDog.Enabled = true;
        }

        private void Authenticate(bool editDatabase)
        {
            while (true)
            {
                FrmLogin login = new FrmLogin();
                login.ForbidEditDataBase = !editDatabase;
                DialogResult result = login.ShowDialog();
                if (result == DialogResult.OK)
                {
                    WorkStationInfo ws = GetWorkstationID();
                    WorkStationInfo.CurrentStation = ws;
                    _OperatorLoginTime = DateTime.Now;
                    break;
                }
                else if (result == DialogResult.Cancel)
                {
                    this.Close();
                }
            }
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

            //重新登录时会重新获取工作站，所以这里确认工作站始发通信工作站
            w.IsHostWorkstation = ParkBuffer.Current != null && ParkBuffer.Current.Parks.Exists(p => p.HostWorkstation == w.StationID);
            return w;
        }

        private void InitParkingCommunication()
        {
            if (GlobalVariables.EnableInitParkingCommunication)
            {
                ServiceHelper helper = new ServiceHelper();
                helper.StartServer(WorkStationInfo.CurrentStation.StationID);
                _InitParkingCommunication = true;
            }
            else
            {
                MessageBox.Show(Resource1.FrmMain_ConfirmIP, Resource1.Form_Alert);
                this.lblCommuicationStatus.Text = Resource1.FrmMain_CommunicationFailure;
                _InitParkingCommunication = false;
            }

            ////这里使用登录验证时的数据库来获取停车场硬件参数，
            ////是因为当主数据库与备用数据的命令服务器地址不一致时，可通过选择登录的数据库来选择命令服务器地址
            //ParkBuffer.Current.InValid(AppSettings.CurrentSetting.SelectedParkConnect);
            ParkBuffer.Current.InValid(AppSettings.CurrentSetting.AvailableParkConnect);
            //本工作站连接所有的停车场WCF服务
            foreach (ParkInfo park in ParkBuffer.Current.Parks)
            {
                if (park.IsRootPark &&
                    (park.HostWorkstation == WorkStationInfo.CurrentStation.StationID || WorkStationInfo.CurrentStation.IsInListenList(park))//如果是托管的停车场或者侦听了事件的停车场
                    )
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

        private void InitThirdCommunicationServer()
        {
            try
            {
                string config = AppSettings.CurrentSetting.GetConfigContent("ThirdCommunication");
                if (!string.IsNullOrEmpty(config))
                {
                    ThirdCommunicationServer server = new ThirdCommunicationServer(config);
                    server.Start();
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
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
            KeySetting.Current = ssb.GetOrCreateSetting<KeySetting>();
            GlobalVariables.SetCardReaderKeysetting();

            SaveSystemParametersToLDB();
        }

        private void SaveSystemParametersToLDB()
        {
            try
            {
                //主数据库连接上时，才更新本地数据库参数
                if (DataBaseConnectionsManager.Current.MasterConnected)
                {
                    OperatorBll obll = new OperatorBll(AppSettings.CurrentSetting.ParkConnect);
                    QueryResultList<OperatorInfo> operators = obll.GetAllOperators();

                    WorkstationBll wbll = new WorkstationBll(AppSettings.CurrentSetting.ParkConnect);
                    QueryResultList<WorkStationInfo> workstations = wbll.GetAllWorkstations();

                    if (operators.Result == ResultCode.Successful
                        && workstations.Result == ResultCode.Successful)
                    {

                        LDB_ParkingDataBuffer.Current.Operators = operators.QueryObjects;
                        LDB_ParkingDataBuffer.Current.WorkStations = workstations.QueryObjects;
                        
                        LDB_SysParaSettingsBll lssb = new LDB_SysParaSettingsBll(LDB_AppSettings.Current.LDBConnect);
                        lssb.SaveSettingWithUnitWork<UserSetting>(UserSetting.Current);
                        lssb.SaveSettingWithUnitWork<HolidaySetting>(HolidaySetting.Current);
                        lssb.SaveSettingWithUnitWork<AccessSetting>(AccessSetting.Current);
                        lssb.SaveSettingWithUnitWork<TariffSetting>(TariffSetting.Current);
                        lssb.SaveSettingWithUnitWork<CarTypeSetting>(CarTypeSetting.Current);
                        lssb.SaveSettingWithUnitWork<CustomCardTypeSetting>(CustomCardTypeSetting.Current);
                        lssb.SaveSettingWithUnitWork<BaseCardTypeSetting>(BaseCardTypeSetting.Current);
                        lssb.SaveSettingWithUnitWork<KeySetting>(KeySetting.Current);
                        lssb.SaveSettingWithUnitWork<LDB_ParkingDataBuffer>(LDB_ParkingDataBuffer.Current);
                        CommandResult result = lssb.UnitWorkCommit();                        
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void StartVideoCapture()
        {             
            try
            {
                FrmCarPlateOfDaHua frmDaHua = FrmCarPlateOfDaHua.GetInstance();
                frmDaHua.Init();
                VideoCaptureManager.Instance.Add((int)VideoServerType.DaHua, frmDaHua);

                FrmCarPlateOfXinLuTong frmXLT = FrmCarPlateOfXinLuTong.GetInstance();
                frmXLT.Init();
                VideoCaptureManager.Instance.Add((int)VideoServerType.XinLuTong, frmXLT);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void StartCarPlateRecognize()
        {
            try
            {
                CarPlateRecognizationType carplate = AppSettings.CurrentSetting.CarPlateRecognization;
                //以下是软件方式的车牌识别
                if (carplate == CarPlateRecognizationType.VECON)
                {
                    FrmCarPlateOfVecon frm = FrmCarPlateOfVecon.GetInstance();
                    frm.Init();
                    PlateRecognitionService.CurrentInstance.Add((int)CarPlateRecognizationType.VECON, frm);
                    this.mnu_CarplateRegV.Visible = true;
                    this.mnu_CarplateRegW.Visible = false;
                }
                else
                {
                    FrmCarPlateOfWintone frm = FrmCarPlateOfWintone.GetInstance();
                    frm.Init();
                    PlateRecognitionService.CurrentInstance.Add((int)CarPlateRecognizationType.WINTONE, frm);
                    this.mnu_CarplateRegV.Visible = false;
                    this.mnu_CarplateRegW.Visible = true;
                }

                //以下是车牌识别一体机
                FrmCarPlateOfXinLuTong frmXLT = FrmCarPlateOfXinLuTong.GetInstance();
                frmXLT.Init();
                PlateRecognitionService.CurrentInstance.Add((int)CarPlateRecognizationType.XinLuTong, frmXLT);

                FrmCarPlateOfDaHua frmDaHua = FrmCarPlateOfDaHua.GetInstance();
                frmDaHua.Init();
                PlateRecognitionService.CurrentInstance.Add((int)CarPlateRecognizationType.DaHua, frmDaHua);

                this.mnu_CarplateReg.Visible = true;
                this.mnu_CarPlateTestForFile.Visible = true;
                this.mnu_CarPlateTestForVideo.Visible = true;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void StartCarPlateRecognize(string carplate)
        {
            //try
            //{
            //    if (_CarPlateForm != null) return;//如果_CarPlateForm不为空，说明前面已开始服务了了，这里就不再需要开始了
            //    //string carplate = AppSettings.CurrentSetting.GetConfigContent("CarPlateRecognization");
            //    if (carplate == "WINTONE")
            //    {
            //        FrmCarPlateOfWintone frm = FrmCarPlateOfWintone.GetInstance();
            //        frm.Init();
            //        _CarPlateForm = frm;
            //        PlateRecognitionService.CurrentInstance = new PlateRecognitionService(frm);
            //        this.mnu_CarPlateTestForFile.Visible = true;
            //        this.mnu_CarPlateTestForVideo.Visible = true;
            //    }
            //    else if (carplate == "VECON")
            //    {
            //        FrmCarPlateOfVecon frm = FrmCarPlateOfVecon.GetInstance();
            //        frm.Init();
            //        _CarPlateForm = frm;
            //        PlateRecognitionService.CurrentInstance = new PlateRecognitionService(frm);
            //        this.mnu_CarPlateTestForFile.Visible = true;
            //        this.mnu_CarPlateTestForVideo.Visible = true;
            //    }
            //    else if (carplate == "XinLuTong")
            //    {
            //        FrmCarPlateOfXinLuTong frm = FrmCarPlateOfXinLuTong.GetInstance();
            //        frm.Init();
            //        _CarPlateForm = frm;
            //        this._eventHandlers.Add(frm);//对事件进行抓拍图片
            //        SnapShotCaptureService.CurrentInstance = new SnapShotCaptureService(frm);//创建快照抓拍服务实例
            //        PlateRecognitionService.CurrentInstance = new PlateRecognitionService(frm);
            //        this.mnu_CarPlateTestForFile.Visible = false;
            //        this.mnu_CarPlateTestForVideo.Visible = false;
            //    }
            //    else if (carplate == "DaHua")
            //    {
            //        FrmCarPlateOfDaHua frm = FrmCarPlateOfDaHua.GetInstance();
            //        frm.Init();
            //        _CarPlateForm = frm;
            //        this._eventHandlers.Add(frm);//对事件进行抓拍图片
            //        SnapShotCaptureService.CurrentInstance = new SnapShotCaptureService(frm);//创建快照抓拍服务实例
            //        PlateRecognitionService.CurrentInstance = new PlateRecognitionService(frm);
            //        this.mnu_CarPlateTestForFile.Visible = false;
            //        this.mnu_CarPlateTestForVideo.Visible = false;
            //    }
            //    this.mnu_CarplateReg.Visible = true;
            //}
            //catch (Exception ex)
            //{
            //    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            //}
        }

        private void InitWorkStation()
        {
            this.lblOperator.Text = string.Format(Resource1.FrmMain_lblOperator, OperatorInfo.CurrentOperator.OperatorName);
            this.lblStation.Text = string.Format(Resource1.FrmMain_lblStation, WorkStationInfo.CurrentStation.StationName);
            this.lblEventServiceStatus.Text = AppSettings.CurrentSetting.EnableWriteCard ? Resource1.EnableWriteCard : string.Empty;
            RenderHardwareTree();
            this.entranceTree.Focus();
            this.ShowOperatorRights(OperatorInfo.CurrentOperator);  //操作员权限

            if (UserSetting.Current.EnableForceShifting && OperatorInfo.CurrentOperator.NeedShift)
            {
                this.tmrForceShifting.Enabled = true;
                this.tmrForceShifting.Interval = 30 * 1000; //30S
            }
            LogOperatorLogIn();
        }

        private void ShowSingleForm(Type formType)
        {
            Form instance = null;
            if (!this.formPanel1.Visible) this.formPanel1.Visible = true;
            foreach (Form frm in _openedForms)
            {
                if (frm.GetType() == formType)
                {
                    instance = frm;
                    break;
                }
            }
            if (instance == null)
            {
                instance = (Form)Activator.CreateInstance(formType);
                instance.ShowInTaskbar = false;
                _openedForms.Add(instance);
                if (instance is IReportHandler) this._eventHandlers.Add(instance as IReportHandler);
                instance.MdiParent = this;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
                this.formPanel1.AddAForm(instance);
                instance.FormClosed += delegate(object sender, FormClosedEventArgs e)
                {
                    _openedForms.Remove(instance);
                    if (instance is IReportHandler) this._eventHandlers.Remove(instance as IReportHandler);
                };
            }
            this.formPanel1.HighLightForm(instance);
            instance.Show();
            instance.Activate();
        }

        private void ShowOperatorRights(OperatorInfo op)
        {
            RoleInfo role = op.Role;
            this.mnu_SystemIDSetting.Enabled = role.Permit(Permission.SystemIDSetting);
            this.mnu_Workstations.Enabled = (role.Permit(Permission.ReadWorkstation) || role.Permit(Permission.EditWorkstation));
            this.mnu_Cards.Enabled = (role.Permit(Permission.ReadCards) || role.Permit(Permission.EditCard));
            this.mnu_Operator.Enabled = (role.Permit(Permission.ReadOperaotor) || role.Permit(Permission.EditOperator));
            this.mnu_Roles.Enabled = (role.Permit(Permission.ReadRole) || role.Permit(Permission.EditRole));
            this.mnu_Depts.Enabled = (role.Permit(Permission.ReadDept) || role.Permit(Permission.EditDept));
            this.mnu_SystemOption.Enabled = (role.Permit(Permission.ReadSysSetting) || role.Permit(Permission.EditSysSetting));
            this.mnu_LocalSettings.Enabled = (role.Permit(Permission.ReadLocalSetting) || role.Permit(Permission.EditLocalSetting));
            this.mnu_CardChargeReport.Enabled = role.Permit(Permission.CardChargeReport);
            this.mnu_CardDeferReport.Enabled = role.Permit(Permission.CardDeferReport);
            this.mnu_CardDeferStatistic.Enabled = role.Permit(Permission.CardDeferStatistics);
            this.mnu_CardReleaseReport.Enabled = role.Permit(Permission.CardReleaseReport);
            this.mnu_CardPayingReport.Enabled = role.Permit(Permission.CardPayingReport);
            this.mnu_CardPayingStatistic.Enabled = role.Permit(Permission.CardPayingStatistics);
            this.mnu_CardLostRestoreReport.Enabled = role.Permit(Permission.CardLossRestoreReport);
            this.mnu_CardDisableEnableReport.Enabled = role.Permit(Permission.CardDisableEnableReport);
            this.mnu_CardRecycleReport.Enabled = role.Permit(Permission.CardRecycleReport);
            this.mnu_CardDeleteReport.Enabled = role.Permit(Permission.CardDeleteReport);
            this.mnu_CarFlowStatistics.Enabled = role.Permit(Permission.CarFlowStatistics);
            this.mnu_CardEventReport.Enabled = role.Permit(Permission.CardEventReport);
            this.mnu_OperatorShiftStatistics.Enabled = role.Permit(Permission.OperatorShiftStatistics);
            this.mnu_Alarm.Enabled = role.Permit(Permission.AlarmReport);
            this.mnu_RealReport.Enabled = role.Permit(Permission.RealEvent);

            this.btn_Cards.Enabled = (role.Permit(Permission.ReadCards) || role.Permit(Permission.EditCard));

            this.mnu_AddPark.Enabled = role.Permit(Permission.EditPark);
            this.mnu_AddEntrance1.Enabled = role.Permit(Permission.EditEntrance);
            this.mnu_SearchDevice.Enabled = role.Permit(Permission.EditEntrance);
            this.mnu_DeletePark.Enabled = role.Permit(Permission.EditPark);
            this.mnu_TempCardSetting.Enabled = role.Permit(Permission.TempCardSetting);

            this.mnu_AddVideo.Enabled = role.Permit(Permission.EditVideo);
            this.mnu_Reset.Enabled = role.Permit(Permission.ResetEntrance);
            this.mnu_RemoteReadCard.Enabled = role.Permit(Permission.RemoteReadCard);
            this.mnu_Up.Enabled = role.Permit(Permission.OpenDoor);
            this.mnu_Down.Enabled = role.Permit(Permission.CloseDoor);
            this.mnu_DeleteEntrance.Enabled = role.Permit(Permission.EditEntrance);

            this.mnu_DeleteVideo.Enabled = role.Permit(Permission.EditVideo);
            this.mnu_ParkRename.Enabled = role.Permit(Permission.EditPark);
            this.mnu_EntranceRename.Enabled = role.Permit(Permission.EditEntrance);
            this.mnu_VideoRename.Enabled = role.Permit(Permission.EditVideo);

            this.btn_Monitor.Enabled = role.Permit(Permission.VideoMonitor);
            this.mnu_Monitor.Enabled = role.Permit(Permission.VideoMonitor);
            this.btn_CardPaying.Enabled = role.Permit(Permission.CardPaying);
            this.mnu_CardPaying.Enabled = role.Permit(Permission.CardPaying);
            this.btn_AlarmReport.Enabled = role.Permit(Permission.AlarmReport);
            this.btn_CardeventReport.Enabled = role.Permit(Permission.CardEventReport);
            this.btn_CardPaymentReport.Enabled = role.Permit(Permission.CardPayingReport);
            this.mnu_APM.Enabled = role.Permit(Permission.ReadAPM) || role.Permit(Permission.EditAPM);
            this.mnu_PayOperationLogReport.Enabled = role.Permit(Permission.PayOperationLogReport);
            this.mnu_YCTLog.Enabled = role.Permit(Permission.YangChenTongLogReport);
            this.mnu_CardInPark.Enabled = role.Permit(Permission.CardInparkReport);
            this.mnu_OperatorShift.Enabled = role.Permit(Permission.OperatorSettle);
            this.btn_Shift.Enabled = role.Permit(Permission.OperatorSettle);

            this.mnu_ZSTSetting.Visible = AppSettings.CurrentSetting.EnableZST;
            this.mnu_ZSTSetting.Enabled = role.Permit(Permission.ZSTSetting);

            this.mnu_VehicleLedSetting.Enabled = role.Permit(Permission.VehicleLedSetting);
            this.mnu_HostStandbySetting.Enabled = role.Permit(Permission.HostStandbySetting);
            this.mnu_ServerSwitchReport.Enabled = role.Permit(Permission.ServerSwitchReport);

            this.mnu_SyncDataToStandby.Visible = !string.IsNullOrEmpty(AppSettings.CurrentSetting.StandbyParkConnect);
            this.mnu_SyncDataToStandby.Enabled = role.Permit(Permission.SyncDataToStandby)
                && DataBaseConnectionsManager.Current.MasterConnected
                && DataBaseConnectionsManager.Current.StandbyConnected;

            this.mnu_ExportParameter.Enabled = role.Permit(Permission.ExportParameter);
            this.mnu_ImportRecord.Enabled = role.Permit(Permission.ImportRecord);
            this.mnu_HasPaidCardReport.Enabled = role.Permit(Permission.HasPaidCardReport);
            this.mnu_CardReport.Enabled = role.Permit(Permission.CardReport);

            this.mnu_AddDivision.Enabled = role.Permit(Permission.EditDivision);
            this.mnu_ParkProperty.Enabled = role.Permit(Permission.EditPark);
            this.btn_EntranceProperty.Enabled = role.Permit(Permission.EditEntrance);
            this.mnu_VideoSyncTime.Enabled = role.Permit(Permission.EditVideo);
            this.btn_VideoReboot.Enabled = role.Permit(Permission.EditVideo);
            this.mnu_VideoProperty.Enabled = role.Permit(Permission.EditVideo);
            this.mnu_SetParkTariff.Enabled = (role.Permit(Permission.ReadSysSetting) || role.Permit(Permission.EditSysSetting)); ;

            this.mnu_RoadWays.Enabled = role.Permit(Permission.ReadRoadWay) || role.Permit(Permission.EditRoadWay);
            this.mnu_SwitchEntranceMode.Enabled = role.Permit(Permission.SwitchRoadWay);
            this.btn_SwitchEntranceMode.Enabled = role.Permit(Permission.SwitchRoadWay);

            this.mnu_HotelAuthorization.Enabled = role.Permit(Permission.FreeAuthorization);
            this.btn_FreeAuthorization.Enabled = role.Permit(Permission.FreeAuthorization);
            this.mnu_PosSyncTool.Enabled = role.Permit(Permission.POSSyncTool);
            this.mnu_FreeAuthorizationLog.Enabled = role.Permit(Permission.FreeAuthorizationLogReport);
            this.mnu_CardOut.Enabled = role.Permit(Permission.CardOut);
            this.mnu_WaitingCommand.Enabled = role.Permit(Permission.WaitingCommandReport);
            this.mnu_APMCheckOutRecordRport.Enabled = role.Permit(Permission.APMCheckOutRecordReport);
            this.mnu_APMRefundRecordRport.Enabled = role.Permit(Permission.APMRefundRecordReport);
            this.mnu_APMRefund.Enabled = role.Permit(Permission.APMRefund);
            this.mnu_NoCardLost.Enabled = role.Permit(Permission.NoCardLost);

            this.mnu_SpeedingProcess.Enabled = role.Permit(Permission.SpeedingProcess);
            this.mnu_SpeedingReport.Enabled = role.Permit(Permission.SpeedingReport);
        }

        private void ProcessReport(object sender, ReportBase report)
        {
            foreach (IReportHandler handler in _eventHandlers)
            {
                try
                {
                    handler.ProcessReport(report);
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
            }
        }

        private void RenderHardwareTree()
        {
            this.entranceTree.Init();
            if (AppSettings.CurrentSetting != null && AppSettings.CurrentSetting.ShowOnlyListenedPark)
            {
                foreach (TreeNode pn in entranceTree.ParkNodes)
                {
                    if (entranceTree.IsParkNode(pn))
                    {
                        ParkInfo park = pn.Tag as ParkInfo;
                        if (park != null && (WorkStationInfo.CurrentStation.StationID == park.HostWorkstation || WorkStationInfo.CurrentStation.IsInListenList(park)))
                        {
                            //donothing
                        }
                        else
                        {
                            pn.Remove();
                        }
                    }
                }
                foreach (TreeNode en in entranceTree.EntranceNodes)
                {
                    EntranceInfo entrance = en.Tag as EntranceInfo;
                    if (!WorkStationInfo.CurrentStation.EntranceList.Contains(entrance.EntranceID))
                    {
                        en.Remove();
                    }
                }
            }
        }

        private static void GetCurrentCulture()
        {
            try
            {
                if (string.IsNullOrEmpty(AppSettings.CurrentSetting.Language))
                {
                    string culture = Thread.CurrentThread.CurrentCulture.Parent.Name;
                    if (culture == "zh-Hans" || culture == "zh-CHS")
                    {
                        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-Hans");
                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-Hans");
                    }
                    else if (culture == "zh-Hant" || culture == "zh-CHT")
                    {
                        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-Hant");
                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-Hant");
                    }
                    else
                    {
                        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    }
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(AppSettings.CurrentSetting.Language);
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(AppSettings.CurrentSetting.Language);
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void ShowLanguage()
        {
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            if (culture == "zh-Hans" || culture == "zh-CHS")
            {
                this.mnu_SimpleChinese.Checked = true;
            }
            else if (culture == "zh-Hant" || culture == "zh-CHT")
            {
                this.mnu_TraditionalChinese.Checked = true;
            }
            else
            {
                this.mnu_English.Checked = true;
            }
        }

        private void LogOperatorLogIn()
        {
            AlarmInfo alarm = new AlarmInfo()
            {
                AlarmDateTime = _OperatorLoginTime.HasValue ? _OperatorLoginTime.Value : DateTime.Now,
                AlarmSource = string.Empty,
                AlarmType = AlarmType.OperatorLogIn,
                OperatorID = OperatorInfo.CurrentOperator.OperatorName,
                AlarmDescr = string.Format(Resource1.OperatorLogInAlarm,
                WorkStationInfo.CurrentStation.StationName,
                NetTool.GetHostName(),
                NetTool.GetFirstIP(),
                NetTool.GetLocalMac())
            };
            (new AlarmBll(AppSettings.CurrentSetting.ParkConnect)).Insert(alarm);
        }

        private void LogOperatorLogOut()
        {
            AlarmInfo alarm = new AlarmInfo()
            {
                AlarmDateTime = DateTime.Now,
                AlarmSource = string.Empty,
                AlarmType = AlarmType.OperatorLogOut,
                OperatorID = OperatorInfo.CurrentOperator.OperatorName,
                AlarmDescr = string.Format(Resource1.OperatorLogOutAlarm,
                WorkStationInfo.CurrentStation.StationName,
                _OperatorLoginTime.HasValue ? _OperatorLoginTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty,
                NetTool.GetHostName(),
                NetTool.GetFirstIP(),
                NetTool.GetLocalMac())
            };
            (new AlarmBll(AppSettings.CurrentSetting.ParkConnect)).Insert(alarm);
        }

        private void GetEntrancesStatus(List<ParkInfo> parks)
        {
            foreach (ParkInfo park in parks)
            {
                IParkingAdapter pad = ParkingAdapterManager.Instance[park.ParkID];
                if (pad != null)
                {
                    foreach (EntranceInfo entrance in park.Entrances)
                    {
                        entrance.Status = pad.GetEntranceStatus(entrance.EntranceID);
                    }
                }
            }
        }


        /// <summary>
        /// 从文件中获取记录
        /// </summary>
        /// <returns></returns>
        private List<string> GetRecordsFormFile(RecordType recordType)
        {
            OpenFileDialog frm = new OpenFileDialog();
            string msg = string.Empty;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string file = frm.FileName;
                List<string> records = new List<string>();
                bool dataErr = false;//数据错误标识
                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        dataErr = reader.EndOfStream;
                        if (!dataErr)
                        {
                            string typestr = reader.ReadLine();
                            RecordType type = RecordTypeSerializer.Deserialize(typestr);
                            dataErr = type != recordType;
                            while (!dataErr && !reader.EndOfStream)
                            {
                                string record = reader.ReadLine();
                                if (!string.IsNullOrEmpty(record)) records.Add(record);
                            }
                        }
                    }
                }
                if (records.Count > 0) return records;
                if (dataErr) msg = Resource1.FrmMain_ImportDataErr;
                else msg = Resource1.FrmMain_ImportNoRecord;
                MessageBox.Show(msg);
            }
            return null;
        }

        private void SaveRecordsToPark(List<string> records, RecordType recordType)
        {
            FrmProcessing frmP = new FrmProcessing();
            string msg = string.Empty;
            Action action = delegate()
            {
                try
                {
                    int success = 0;
                    int fail = 0;
                    foreach (string record in records)
                    {
                        switch (recordType)
                        {
                            case RecordType.CardPaymentRecord:
                                SavePaymentsToPark(record, ref success, ref fail);
                                break;
                            case RecordType.CardChargeRecord:
                                SaveChargesToPark(record, ref success, ref fail);
                                break;
                            default:
                                fail++;
                                break;
                        }
                        msg = string.Format(Resource1.FrmMain_ImportNote, success, fail, records.Count);
                        frmP.ShowProgress(msg, (success + fail) / records.Count);
                    }
                }
                catch (ThreadAbortException)
                {
                }
                catch (Exception ex)
                {
                    frmP.ShowProgress(ex.Message, 1);
                }
            };
            Thread t = new Thread(new ThreadStart(action));
            t.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            t.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            t.Start();
            if (frmP.ShowDialog() != DialogResult.OK)
            {
                t.Abort();
            }
            if (!string.IsNullOrEmpty(msg))
            {
                MessageBox.Show(msg);
            }
        }

        private bool SavePaymentsToPark(string record, ref int success, ref int fail)
        {
            CardPaymentRecordBll bll = new CardPaymentRecordBll(AppSettings.CurrentSetting.ParkConnect);
            CardPaymentInfo p = CardPaymentInfoSerializer.Deserialize(record);
            if (p != null)
            {
                CommandResult ret = bll.InsertRecordWithCheck(p);
                if (ret.Result == ResultCode.Successful)
                {
                    success++;
                    return true;
                }
                else
                {
                    fail++;
                }
            }
            else
            {
                fail++;
            }
            return false;
        }

        private bool SaveChargesToPark(string record, ref int success, ref int fail)
        {
            CardChargeRecordBll bll = new CardChargeRecordBll(AppSettings.CurrentSetting.ParkConnect);
            CardChargeRecord p = CardChargeRecordSerializer.Deserialize(record);
            if (p != null)
            {
                CommandResult ret = bll.InsertRecordWithCheck(p);
                if (ret.Result == ResultCode.Successful)
                {
                    success++;
                    return true;
                }
                else
                {
                    fail++;
                }
            }
            else
            {
                fail++;
            }
            return false;
        }

        ///// <summary>
        ///// 获取所有停车场最近一次双机热备使用状态
        ///// </summary>
        ///// <param name="parks"></param>
        //private void GetParkLastHostStandbyStatus(List<ParkInfo> parks)
        //{
        //    if (parks != null)
        //    {
        //        HostStandbySettingBll bll = new HostStandbySettingBll(AppSettings.CurrentSetting.ParkConnect);
        //        foreach (ParkInfo park in parks)
        //        {
        //            HostStandbySetting setting = bll.Get(park.ParkID);
        //            if (setting != null)
        //            {
        //                if (!_ParkLastHostStandbyStatus.ContainsKey(park.ParkID))
        //                {
        //                    _ParkLastHostStandbyStatus.Add(park.ParkID, HostStandbyStatus.UnKnown);
        //                }
        //                _ParkLastHostStandbyStatus[park.ParkID] = setting.LastStatus;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 获取所有停车场最近一次服务器IP
        /// </summary>
        /// <param name="parks"></param>
        private void GetParkServerLastIP(List<ParkInfo> parks)
        {
            if (parks != null)
            {
                HostStandbySettingBll bll = new HostStandbySettingBll(AppSettings.CurrentSetting.ParkConnect);
                foreach (ParkInfo park in parks)
                {
                    HostStandbySetting setting = bll.Get(park.ParkID);
                    if (setting != null)
                    {
                        if (!_ParkServerLastIP.ContainsKey(park.ParkID))
                        {
                            _ParkServerLastIP.Add(park.ParkID, string.Empty);
                        }
                        _ParkServerLastIP[park.ParkID] = setting.LastIP;
                    }
                }
            }
        }

        
        ///// <summary>
        ///// 处理停车场的双机热备使用状态
        ///// </summary>
        ///// <param name="parkID"></param>
        //private void HostStandbyStatusHandle(ParkInfo park)
        //{
        //    HostStandbySettingBll bll = new HostStandbySettingBll(AppSettings.CurrentSetting.ParkConnect);
        //    HostStandbySetting setting = bll.Get(park.ParkID);
        //    if (setting != null)
        //    {
        //        System.Net.IPAddress[] ips = Ralid.GeneralLibrary.NetTool.GetLocalIPS();
        //        HostStandbyStatus newStatus = HostStandbyStatus.UnKnown;
        //        if (setting.IsHost(ips))
        //        {
        //            newStatus = HostStandbyStatus.Host;
        //        }
        //        else if (setting.IsStandby(ips))
        //        {
        //            newStatus = HostStandbyStatus.Standby;
        //        }
        //        setting.LastStatus = newStatus;
        //        setting.LastStart = _StartFrom;

        //        if (bll.Save(setting))
        //        {
        //            HostStandbyStatus oldStatus = HostStandbyStatus.UnKnown;
        //            if (_ParkLastHostStandbyStatus.ContainsKey(park.ParkID))
        //            {
        //                oldStatus = _ParkLastHostStandbyStatus[park.ParkID];
        //            }
        //            AlarmInfo alarm = null;
        //            if (setting.IsHostSwitch(oldStatus, newStatus))
        //            {
        //                alarm = new AlarmInfo()
        //                {
        //                    AlarmDateTime = _StartFrom,
        //                    AlarmSource = string.Empty,
        //                    AlarmType = AlarmType.ServerSwitching,
        //                    OperatorID = OperatorInfo.CurrentOperator.OperatorName,
        //                    AlarmDescr = string.Format("停车场 [{0}] 通讯服务器切换到主机服务器",
        //                    park.ParkName)
        //                };
        //            }
        //            else if (setting.IsStandbySwitch(oldStatus, newStatus))
        //            {
        //                alarm = new AlarmInfo()
        //                {
        //                    AlarmDateTime = _StartFrom,
        //                    AlarmSource = string.Empty,
        //                    AlarmType = AlarmType.ServerSwitching,
        //                    OperatorID = OperatorInfo.CurrentOperator.OperatorName,
        //                    AlarmDescr = string.Format("停车场 [{0}] 通讯服务器切换到从机服务器",
        //                    park.ParkName)
        //                };
        //            }

        //            if (alarm != null)
        //            {
        //                (new AlarmBll(AppSettings.CurrentSetting.ParkConnect)).Insert(alarm); 
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 处理停车场的服务器切换
        /// </summary>
        /// <param name="parkID"></param>
        private void ServerSwitchHandle()
        {
            foreach (ParkInfo park in ParkBuffer.Current.Parks)
            {
                if (WorkStationInfo.CurrentStation.StationID == park.HostWorkstation)  //如果本工作站是停车场的通讯工作站
                {
                    HostStandbySettingBll bll = new HostStandbySettingBll(AppSettings.CurrentSetting.ParkConnect);
                    HostStandbySetting setting = bll.Get(park.ParkID);
                    if (setting != null)
                    {
                        HostStandbySetting oldSetting = setting.Clone();

                        System.Net.IPAddress[] ips = Ralid.GeneralLibrary.NetTool.GetLocalIPS();
                        string switchIP = string.Empty;
                        HostStandbyStatus newStatus = HostStandbyStatus.UnKnown;
                        if (setting.IsHost(ips))
                        {
                            switchIP = setting.HostIP;
                            newStatus = HostStandbyStatus.Host;
                        }
                        else if (setting.IsStandby(ips))
                        {
                            switchIP = setting.StandbyIP;
                            newStatus = HostStandbyStatus.Standby;
                        }
                        else
                        {
                            System.Net.IPAddress ip = GlobalVariables.CurrentParkingCommunicationIP;
                            if (ip != null)
                            {
                                switchIP = ip.ToString();
                            }
                            newStatus = HostStandbyStatus.UnKnown;
                        }
                        setting.LastIP = switchIP;
                        setting.LastStatus = newStatus;
                        setting.LastStart = _StartFrom;

                        if (bll.Save(setting))
                        {
                            if (oldSetting.LastIP != setting.LastIP)
                            {
                                ServerSwitchRecord record = new ServerSwitchRecord();
                                record.ParkID = park.ParkID;
                                record.SwitchDateTime = _StartFrom;
                                record.LastDateTime = oldSetting.LastStart;
                                record.SwitchServerIP = switchIP;
                                record.LastIP = oldSetting.LastIP;
                                record.SwitchStatus = newStatus;
                                record.LastStatus = oldSetting.LastStatus;
                                record.SMSStatus = oldSetting.SendSMS ? SMSSendStatus.Waiting : SMSSendStatus.NotSend;
                                record.Operator = OperatorInfo.CurrentOperator.OperatorName;
                                record.StationID = WorkStationInfo.CurrentStation.StationID;

                                (new ServerSwitchRecordBll(AppSettings.CurrentSetting.ParkConnect)).Insert(record);
                            }
                        }
                    }
                }
            }
        }

        private void CheckServerSwitch(ParkInfo park)
        {
            int parkID = park.ParkID;
            HostStandbySettingBll bll = new HostStandbySettingBll(AppSettings.CurrentSetting.ParkConnect);
            HostStandbySetting setting = bll.Get(parkID);
            if (setting != null)
            {
                if (!_ParkServerLastIP.ContainsKey(parkID))
                {
                    _ParkServerLastIP.Add(parkID, string.Empty);
                }
                if (_ParkServerLastIP[parkID] != setting.LastIP)
                {
                    _ParkServerLastIP[parkID] = setting.LastIP;
                    
                    if (_ServerSwitchRemind == null) _ServerSwitchRemind = new ServerSwitchRemind();
                    _ServerSwitchRemind.Park = park == null ? Resource1.Form_Park : park.ParkName;
                    _ServerSwitchRemind.SwitchTime = setting.LastStart.HasValue ? setting.LastStart.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                    _ServerSwitchRemind.SwitchStatus = Ralid.Park.BusinessModel.Resouce.HostStandbyStatusDescription.GetDescription(setting.LastStatus);
                    _ServerSwitchRemind.SwitchIP = setting.LastIP;

                    string msg = string.Format(Resource1.FrmMain_ServerSwitchWarm, _ServerSwitchRemind.Park, _ServerSwitchRemind.SwitchTime, _ServerSwitchRemind.SwitchStatus, _ServerSwitchRemind.SwitchIP);

                    Action action = delegate()
                    {
                        this.lblServerSwitch.Text = msg;
                    };
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(action);
                    }
                    else
                    {
                        action();
                    }
                }
            }
        }
        #endregion

        #region 硬件树右键菜单的处理
        private void mnu_AddPark_Click(object sender, EventArgs e)
        {
            FrmDetailBase detail = new FrmParkDetail();
            detail.IsAdding = true;
            detail.ItemAdded += HardwareAdded_Handler;
            detail.ShowDialog();
        }


        private void mnu_SwitchEntranceMode_Click(object sender, EventArgs e)
        {
            FrmRoadWay frm = new FrmRoadWay();
            frm.ForSwitchMode = true;
            frm.SwitchModeHandler += SwicthRoadModeHandler;
            frm.ShowDialog();
        }

        private void mnu_AddDivision_Click(object sender, EventArgs e)
        {
            ParkInfo park = this.entranceTree.SelectedNode.Tag as ParkInfo;
            FrmParkDetail detail = new FrmParkDetail();
            detail.ParentPark = park;
            detail.IsAdding = true;
            detail.ItemAdded += HardwareAdded_Handler;
            detail.ShowDialog();
            detail.ItemAdded -= HardwareAdded_Handler;
        }

        private void mnud_AddEntrance_Click(object sender, EventArgs e)
        {
            ParkInfo park = this.entranceTree.SelectedNode.Tag as ParkInfo;
            if (park.DeviceType == EntranceDeviceType.CANEntrance)
            {
                FrmEntranceDetail detail = new FrmEntranceDetail();
                detail.IsAdding = true;
                detail.ItemAdded += HardwareAdded_Handler;
                detail.ShowDialog();
            }
            else
            {
                FrmNetEntranceDetail detail = new FrmNetEntranceDetail();
                detail.IsAdding = true;
                detail.ItemAdded += HardwareAdded_Handler;
                detail.ShowDialog();
            }
        }

        private void mnu_SearchDevice_Click(object sender, EventArgs e)
        {
            FrmSearchDevice frm = new FrmSearchDevice();
            TreeNode node = this.entranceTree.SelectedNode;
            ParkInfo park = node.Tag as ParkInfo;
            frm.ItemAdding += delegate(object device, EventArgs args)
            {
                FrmNetEntranceDetail detail = new FrmNetEntranceDetail();
                detail.IsAdding = true;
                detail.Park = park;
                detail.ParkDevice = device as Ralid.Park.Hardware.ParkDevice;
                detail.ItemAdded += HardwareAdded_Handler;
                detail.ShowDialog();
            };
            frm.ShowDialog();
        }

        private void mnu_OutdoorLedVacant_Click(object sender, EventArgs e)
        {
            ParkInfo park = this.entranceTree.SelectedNode.Tag as ParkInfo;
            if (park != null)
            {
                FrmOutdoorLedSetting frm = new FrmOutdoorLedSetting();
                frm.ItemUpdated += HardwareUpdated_Handler;
                frm.Park = park;
                frm.ShowDialog();
            }
        }

        private void mnu_AddEntrance_Click(object sender, EventArgs e)
        {
            ParkInfo park = this.entranceTree.SelectedNode.Tag as ParkInfo;
            if (park.DeviceType == EntranceDeviceType.CANEntrance)
            {
                FrmEntranceDetail detail = new FrmEntranceDetail();
                detail.Park = park;
                detail.IsAdding = true;
                detail.ItemAdded += HardwareAdded_Handler;
                detail.ShowDialog();
            }
            else
            {
                FrmNetEntranceDetail detail = new FrmNetEntranceDetail();
                detail.Park = park;
                detail.IsAdding = true;
                detail.ItemAdded += HardwareAdded_Handler;
                detail.ShowDialog();
            }
        }

        private void mnu_AddCamera_Click(object sender, EventArgs e)
        {
            FrmVideoSourceDetail detail = new FrmVideoSourceDetail();
            TreeNode node = this.entranceTree.SelectedNode;
            detail.IsAdding = true;
            detail.Entrance = node.Tag as EntranceInfo;
            detail.ItemAdded += HardwareAdded_Handler;
            detail.ShowDialog();
        }

        private void mnu_Rename_Click(object sender, EventArgs e)
        {
            TreeNode node = this.entranceTree.SelectedNode;
            entranceTree.LabelEdit = true;
            if (this.entranceTree.IsParkNode(node))
            {
                node.Text = (node.Tag as ParkInfo).ParkName;
            }
            else if (this.entranceTree.IsEntranceNode(node))
            {
                node.Text = (node.Tag as EntranceInfo).EntranceName;
            }
            else if (this.entranceTree.IsVideoSourceNode(node))
            {
                node.Text = (node.Tag as VideoSourceInfo).VideoName;
            }
            _EditNode = node;
            node.BeginEdit();
        }

        private void mnu_Property_Click(object sender, EventArgs e)
        {
            FrmDetailBase detail = null;
            TreeNode node = this.entranceTree.SelectedNode;
            if (entranceTree.IsParkNode(node))
            {
                detail = new FrmParkDetail();
                detail.UpdatingItem = node.Tag as ParkInfo;
            }
            else if (entranceTree.IsEntranceNode(node))
            {
                EntranceInfo entrance = node.Tag as EntranceInfo;
                ParkInfo park = ParkBuffer.Current.GetPark(entrance.ParkID);
                if (park != null && park.DeviceType == EntranceDeviceType.CANEntrance)
                {
                    detail = new FrmEntranceDetail();
                    detail.UpdatingItem = entrance;
                }
                else
                {
                    detail = new FrmNetEntranceDetail();
                    detail.UpdatingItem = entrance;
                }
            }
            else if (entranceTree.IsVideoSourceNode(node))
            {
                detail = new FrmVideoSourceDetail();
                detail.UpdatingItem = node.Tag as VideoSourceInfo;
            }
            if (detail != null)
            {
                detail.ItemUpdated += HardwareUpdated_Handler;
                detail.IsAdding = false;
                detail.ShowDialog();
            }
        }

        private void mnu_Delete_Click(object sender, EventArgs e)
        {
            TreeNode node = entranceTree.SelectedNode;
            if (entranceTree.IsParkNode(node))
            {
                DeleteParkNode(node);
            }
            else if (entranceTree.IsEntranceNode(node))
            {
                DeleteEntranceNode(node);
            }
            else if (entranceTree.IsVideoSourceNode(node))
            {
                DeleteVideoSourceNode(node);
            }
        }

        private void mnu_Fresh_Click(object sender, EventArgs e)
        {
            ParkBuffer.Current.InValid(AppSettings.CurrentSetting.AvailableParkConnect);
            GetEntrancesStatus(ParkBuffer.Current.Parks);
            RenderHardwareTree();
        }

        private void mnu_Reset_Click(object sender, EventArgs e)
        {
            TreeNode node = entranceTree.SelectedNode;
            if (node != null && entranceTree.IsEntranceNode(node))
            {
                EntranceInfo entrance = node.Tag as EntranceInfo;
                DeviceReSetNotify notify = new DeviceReSetNotify(entrance.EntranceID, 1);
                if (ParkingAdapterManager.Instance[entrance.RootParkID] != null)
                {
                    ParkingAdapterManager.Instance[entrance.RootParkID].ResetDevice(notify);
                }
            }
        }

        private void mnu_Up_Click(object sender, EventArgs e)
        {
            TreeNode node = entranceTree.SelectedNode;
            if (node != null && entranceTree.IsEntranceNode(node))
            {
                EntranceInfo entrance = node.Tag as EntranceInfo;
                GateOperationNotify notify = new GateOperationNotify(entrance.EntranceID, GateOperation.Open, OperatorInfo.CurrentOperator.OperatorName);
                if (ParkingAdapterManager.Instance[entrance.RootParkID] != null)
                {
                    ParkingAdapterManager.Instance[entrance.RootParkID].GateOperation(notify);
                }
            }
        }

        private void mnu_Down_Click(object sender, EventArgs e)
        {
            TreeNode node = entranceTree.SelectedNode;
            if (node != null && entranceTree.IsEntranceNode(node))
            {
                EntranceInfo entrance = node.Tag as EntranceInfo;
                GateOperationNotify notify = new GateOperationNotify(entrance.EntranceID, GateOperation.Close, OperatorInfo.CurrentOperator.OperatorName);
                if (ParkingAdapterManager.Instance[entrance.RootParkID] != null)
                {
                    ParkingAdapterManager.Instance[entrance.RootParkID].GateOperation(notify);
                }
            }
        }

        private void mnu_RemoteReadCard_Click(object sender, EventArgs e)
        {
            if (entranceTree.IsEntranceNode(entranceTree.SelectedNode))
            {
                FrmRemoteReadCard frm = new FrmRemoteReadCard();
                EntranceInfo entrance = entranceTree.SelectedNode.Tag as EntranceInfo;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (AppSettings.CurrentSetting.EnableWriteCard)
                    {
                        //写卡模式时，只有在线处理的卡片才能远程读卡
                        CardBll bll = new CardBll(AppSettings.CurrentSetting.AvailableParkConnect);
                        CardInfo info = bll.GetCardByID(frm.CardID).QueryObject;
                        if (info == null)
                        {
                            MessageBox.Show(Resource1.FrmMain_NoCard);
                            frm.Close();
                            return;
                        }
                        if (!info.OnlineHandleWhenOfflineMode)
                        {
                            MessageBox.Show(Resource1.FrmMain_RemoteReadCardInvalid);
                            frm.Close();
                            return;
                        }
                    }



                    Action action = delegate()
                    {
                        RemoteReadCardNotify notify = new RemoteReadCardNotify(entrance.ParkID, entrance.EntranceID, frm.CardID, frm.ParkingData);
                        if (ParkingAdapterManager.Instance[entrance.RootParkID] != null)
                        {
                            ParkingAdapterManager.Instance[entrance.RootParkID].RemoteReadCard(notify);
                        }
                    };
                    Thread t = new Thread(new ThreadStart(action));
                    t.Start();
                }
                frm.Close();
            }
        }

        private void mnu_CarPlateManualProccess_Click(object sender, EventArgs e)
        {
            if (entranceTree.IsEntranceNode(entranceTree.SelectedNode))
            {
                DialogResult result = DialogResult.Cancel;
                string cardID = string.Empty;
                string carPlate = string.Empty;
                EntranceInfo entrance = entranceTree.SelectedNode.Tag as EntranceInfo;
                if (entrance.IsExitDevice)
                {
                    FrmCarPlateManualExit frm = new FrmCarPlateManualExit();
                    result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        cardID = frm.CardID;
                        carPlate = frm.CarPlate;
                    }
                    frm.Close();
                }
                else
                {
                    FrmCarPlateManualEnter frm = new FrmCarPlateManualEnter();
                    result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        cardID = string.Empty;
                        if (!string.IsNullOrEmpty(frm.CarPlate))
                        {
                            //如果名单车牌不为空，车牌号为名单车牌
                            carPlate = frm.CarPlate;
                        }
                        else
                        {
                            //否则为临时车牌号，当为无车牌时，临时车牌号为空
                            carPlate = frm.VisitorCarPlate;
                        }
                    }
                    frm.Close();
                }
                if (result == DialogResult.OK)
                {
                    Action action = delegate()
                    {
                        RemoteReadCardNotify notify = new RemoteReadCardNotify(entrance.ParkID, entrance.EntranceID, cardID, carPlate, OperatorInfo.CurrentOperator.OperatorName, WorkStationInfo.CurrentStation.StationName);
                        if (ParkingAdapterManager.Instance[entrance.RootParkID] != null)
                        {
                            ParkingAdapterManager.Instance[entrance.RootParkID].RemoteReadCard(notify);
                        }
                    };
                    Thread t = new Thread(new ThreadStart(action));
                    t.Start();
                }
            }
        }

        private void DeleteEntranceNode(TreeNode node)
        {
            CommandResult result = null;
            DialogResult ret = MessageBox.Show(Resource1.FrmMain_DeleteEntranceQuery, Resource1.Form_Query, MessageBoxButtons.YesNo);
            if (ret == DialogResult.Yes)
            {
                EntranceInfo info = node.Tag as EntranceInfo;
                if (info != null)
                {
                    EntranceBll bll = new EntranceBll(AppSettings.CurrentSetting.ParkConnect);
                    result = bll.Delete(info);
                }
                if (result.Result == ResultCode.Successful)
                {
                    entranceTree.DeleteNode(node);
                    ParkBuffer.Current.InValid(AppSettings.CurrentSetting.AvailableParkConnect);
                    GetEntrancesStatus(ParkBuffer.Current.Parks);
                    IParkingAdapter pad = ParkingAdapterManager.Instance[info.RootParkID];
                    if (pad != null) pad.DeleteEntrance(info);
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
        }

        private void DeleteVideoSourceNode(TreeNode node)
        {
            CommandResult result = null;
            DialogResult ret = MessageBox.Show(Resource1.FrmMain_DeleteVideoQuery, Resource1.Form_Query, MessageBoxButtons.YesNo);
            if (ret == DialogResult.Yes)
            {
                VideoSourceInfo info = node.Tag as VideoSourceInfo;
                if (info != null)
                {
                    VideoSourceBll bll = new VideoSourceBll(AppSettings.CurrentSetting.ParkConnect);
                    result = bll.Delete(info);
                }
                if (result.Result == ResultCode.Successful)
                {
                    entranceTree.DeleteNode(node);
                    ParkBuffer.Current.InValid(AppSettings.CurrentSetting.AvailableParkConnect);
                    GetEntrancesStatus(ParkBuffer.Current.Parks);
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
        }

        private void DeleteParkNode(TreeNode node)
        {
            CommandResult result = null;
            DialogResult ret = MessageBox.Show(Resource1.FrmMain_DeleteParkQuery, Resource1.Form_Query, MessageBoxButtons.YesNo);
            if (ret == DialogResult.Yes)
            {
                ParkInfo park = node.Tag as ParkInfo;
                if (park != null)
                {
                    ParkBll parkBll = new ParkBll(AppSettings.CurrentSetting.ParkConnect);
                    result = parkBll.Delete(park);
                    if (result.Result == ResultCode.Successful)
                    {
                        entranceTree.DeleteNode(node);
                        ParkBuffer.Current.InValid(AppSettings.CurrentSetting.AvailableParkConnect);
                        GetEntrancesStatus(ParkBuffer.Current.Parks);
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                    }
                }
            }
        }
        private void HardwareUpdated_Handler(object sender, ItemUpdatedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            TreeNode node = this.entranceTree.SelectedNode;
            if (e.UpdatedItem is ParkInfo)
            {
                ParkInfo park = e.UpdatedItem as ParkInfo;
                entranceTree.RenderPark(node, park);
                if (DataBaseConnectionsManager.Current.StandbyConnected)
                {
                    ParkBll spbll = new ParkBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                    spbll.UpdateOrInsert(park, true);
                }

                CarPortSetting cs = new CarPortSetting(park);
                IParkingAdapter pad = ParkingAdapterManager.Instance[park.RootParkID];
                if (pad != null)
                {
                    bool ret = pad.DownloadVacantSetting(cs);
                    if (!ret)
                    {
                        MessageBox.Show(Resource1.FrmMain_DownloadVacantFail);
                    }
                }
            }
            else if (e.UpdatedItem is EntranceInfo)
            {
                EntranceInfo entrance = e.UpdatedItem as EntranceInfo;
                entranceTree.RenderEntrance(node, entrance);
                if (DataBaseConnectionsManager.Current.StandbyConnected)
                {
                    EntranceBll sebll = new EntranceBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                    sebll.UpdateOrInsert(entrance, true);
                }

                IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                if (pad != null) pad.UpdateEntrance(entrance);
            }
            else if (e.UpdatedItem is VideoSourceInfo)
            {
                VideoSourceInfo video = e.UpdatedItem as VideoSourceInfo;
                entranceTree.RenderVideoSource(node, video);
                if (DataBaseConnectionsManager.Current.StandbyConnected)
                {
                    VideoSourceBll svsbll = new VideoSourceBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                    svsbll.UpdateOrInsert(video, true);
                }
            }
            ParkBuffer.Current.InValid(AppSettings.CurrentSetting.AvailableParkConnect);
            GetEntrancesStatus(ParkBuffer.Current.Parks);
            this.Cursor = Cursors.Arrow;
        }

        public void HardwareAdded_Handler(object sender, ItemAddedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            TreeNode node = this.entranceTree.SelectedNode;
            if (e.AddedItem is ParkInfo)
            {
                ParkInfo park = e.AddedItem as ParkInfo;
                if (park.IsRootPark)
                {
                    entranceTree.AddParkNode(entranceTree.RootNode, e.AddedItem as ParkInfo);
                }
                else
                {
                    entranceTree.AddParkNode(node, e.AddedItem as ParkInfo);
                }

                if (DataBaseConnectionsManager.Current.StandbyConnected)
                {
                    ParkBll spbll = new ParkBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                    spbll.UpdateOrInsert(park, true);
                }
            }
            else if (e.AddedItem is EntranceInfo)
            {
                EntranceInfo entrance = e.AddedItem as EntranceInfo;
                entranceTree.AddEntranceNode(node, entrance);
                if (DataBaseConnectionsManager.Current.StandbyConnected)
                {
                    EntranceBll sebll = new EntranceBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                    sebll.UpdateOrInsert(entrance, true);
                }

                IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                if (pad != null) pad.AddEntrance(entrance);
            }
            else if (e.AddedItem is VideoSourceInfo)
            {
                VideoSourceInfo video = e.AddedItem as VideoSourceInfo;
                entranceTree.AddVideoSourceNode(node, video);
                if (DataBaseConnectionsManager.Current.StandbyConnected)
                {
                    VideoSourceBll svsbll = new VideoSourceBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                    svsbll.UpdateOrInsert(video, true);
                }
            }
            if (node != null)
            {
                node.Expand();
            }
            ParkBuffer.Current.InValid(AppSettings.CurrentSetting.AvailableParkConnect);
            GetEntrancesStatus(ParkBuffer.Current.Parks);
            this.Cursor = Cursors.Arrow;
        }

        private void entranceTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (_EditNode != null)
            {
                TreeNode node = _EditNode;
                if (node != null)
                {
                    if (entranceTree.IsParkNode(node))
                    {
                        ParkInfo park = node.Tag as ParkInfo;
                        if (!string.IsNullOrEmpty(e.Label))
                        {
                            park.ParkName = e.Label;
                            (new ParkBll(AppSettings.CurrentSetting.ParkConnect)).Update(park);
                        }
                        entranceTree.RenderPark(node, park);
                    }
                    else if (entranceTree.IsEntranceNode(node))
                    {
                        EntranceInfo entrance = node.Tag as EntranceInfo;
                        if (!string.IsNullOrEmpty(e.Label))
                        {
                            entrance.EntranceName = e.Label;
                            (new EntranceBll(AppSettings.CurrentSetting.ParkConnect)).Update(entrance);
                        }
                        entranceTree.RenderEntrance(node, entrance);
                    }
                    else if (entranceTree.IsVideoSourceNode(node))
                    {
                        VideoSourceInfo video = node.Tag as VideoSourceInfo;
                        if (!string.IsNullOrEmpty(e.Label))
                        {
                            video.VideoName = e.Label;
                            (new VideoSourceBll(AppSettings.CurrentSetting.ParkConnect)).Update(video);
                        }
                        entranceTree.RenderVideoSource(node, video);
                    }
                    e.CancelEdit = true;
                    entranceTree.LabelEdit = false;
                }
            }
        }

        private void mnu_VideoSyncTime_Click(object sender, EventArgs e)
        {
            TreeNode node = this.entranceTree.SelectedNode;
            if (node != null && (node.Tag is VideoSourceInfo))
            {
                VideoSourceInfo video = node.Tag as VideoSourceInfo;
                video.SyncDateTime(DateTime.Now, 8);
            }
        }

        private void btn_VideoReboot_Click(object sender, EventArgs e)
        {
            TreeNode node = this.entranceTree.SelectedNode;
            if (node != null && (node.Tag is VideoSourceInfo))
            {
                VideoSourceInfo video = node.Tag as VideoSourceInfo;
                video.Reboot();
            }
        }

        private void mnu_TemCardSetting_Click(object sender, EventArgs e)
        {
            TreeNode node = entranceTree.SelectedNode;
            if (node != null && entranceTree.IsEntranceNode(node))
            {
                EntranceInfo entrance = node.Tag as EntranceInfo;
                frmTempCardSetting frm1 = new frmTempCardSetting();
                frm1.TempCardCount = entrance.TempCard;
                if (frm1.ShowDialog() == DialogResult.OK)
                {
                    entrance.TempCard = frm1.TempCardCount;
                    entranceTree.RenderEntrance(node, entrance);
                    CommandResult result = (new EntranceBll(AppSettings.CurrentSetting.ParkConnect)).Update(entrance);
                    if (result.Result == ResultCode.Successful)
                    {
                        EntranceRemainTempCardNotify notify = new EntranceRemainTempCardNotify(entrance.ParkID, entrance.EntranceID, entrance.TempCard);
                        if (ParkingAdapterManager.Instance[entrance.RootParkID] != null)
                        {
                            ParkingAdapterManager.Instance[entrance.RootParkID].SetEntranceRemainTempCard(notify);
                        }
                    }
                    else
                    {
                        MessageBox.Show(result.Message);
                    }
                }
                frm1.Close();
            }
        }

        private void mnu_RealReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmRealReport));
        }

        private void entranceTree_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode node = entranceTree.GetNodeAt(e.Location);
            entranceTree.SelectedNode = node;
            if (e.Button == MouseButtons.Left)
            {
                if (node != null && entranceTree.IsVideoSourceNode(node))
                {
                    entranceTree.DoDragDrop(node.Tag, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (node != null && entranceTree.IsParkNode(node))
                {
                    entranceTree.ContextMenuStrip = parkContextMenu;
                    this.mnu_AddDivision.Visible = (node.Tag as ParkInfo).IsRootPark;
                    this.mnu_OutdoorLedVacant.Visible = UserSetting.Current.EnableOutdoorLed;
                    this.mnu_SearchDevice.Visible = (node.Tag as ParkInfo).DeviceType == EntranceDeviceType.NETEntrance;
                    this.mnu_SetParkTariff.Visible = (node.Tag as ParkInfo).IsRootPark;
                }
                else if (node != null && entranceTree.IsEntranceNode(node))
                {
                    entranceTree.ContextMenuStrip = entranceContextMenu;
                    EntranceInfo entrance = node.Tag as EntranceInfo;
                    mnu_TempCardSetting.Visible = !entrance.IsExitDevice;
                    if (entranceTree.IsParkNode(node.Parent))
                    {
                        ParkInfo park = node.Parent.Tag as ParkInfo;
                        //mnu_RemoteReadCard.Visible = !park.IsWriteCardMode;
                    }
                }
                else if (node != null && entranceTree.IsVideoSourceNode(node))
                {
                    entranceTree.ContextMenuStrip = videoContextMenu;
                }
                else
                {
                    entranceTree.ContextMenuStrip = treeContextMenu;
                }
                ShowOperatorRights(OperatorInfo.CurrentOperator);
            }
        }

        private void SwicthRoadModeHandler(object sender, SwitchRoadModeArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            bool result = true;
            RoadWayInfo info = e.RoadWay;
            if (info != null)
            {
                foreach (int enID in info.EntranceList)
                {
                    EntranceInfo entrance = ParkBuffer.Current.GetEntrance(enID);
                    if (entrance != null)
                    {
                        IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                        if (pad != null)
                        {
                            entrance.Valid = entrance.IsExitDevice ? e.Mode == RoadMode.Exit : e.Mode == RoadMode.Entrance;
                            if (pad.UpdateEntrance(entrance))
                            {
                                if (new EntranceBll(AppSettings.CurrentSetting.CurrentMasterConnect).Update(entrance).Result == ResultCode.Successful)
                                {
                                    TreeNode node = this.entranceTree.GetEntranceNode(entrance.EntranceID);
                                    if (node != null) this.entranceTree.RenderEntrance(node, entrance);
                                    new EntranceBll(AppSettings.CurrentSetting.CurrentStandbyConnect).Update(entrance);
                                }
                            }
                            else
                            {
                                result = false;
                            }
                        }
                    }
                }
            }
            ParkBuffer.Current.InValid(AppSettings.CurrentSetting.AvailableParkConnect);
            GetEntrancesStatus(ParkBuffer.Current.Parks);
            if (result)
            {
                info.Mode = e.Mode;
                new RoadWayBll(AppSettings.CurrentSetting.CurrentMasterConnect).Update(info);
            }
            this.Cursor = Cursors.Arrow;
            if (result)
            {
                MessageBox.Show(Resource1.FrmMain_SwicthRoadModeSuccess);
            }
            else
            {
                MessageBox.Show(Resource1.FrmMain_SwicthRoadModeFail);
            }
        }
        #endregion

        #region 主菜单及工具栏事件处理
        private void mnu_Operators_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmOperators));
        }

        private void mnu_LogOut_Click(object sender, EventArgs e)
        {
            //清空强制交班的参数
            tmrForceShifting.Enabled = false;
            _ForceShiftingAlarmCount = 0;

            LogOperatorLogOut();
            //清空操作员登录时间
            _OperatorLoginTime = null;

            Authenticate(false);
            InitSystemParameters();
            InitWorkStation();
        }

        private void mnu_Roles_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmRoles));
        }

        private void mnu_Workstations_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmWorkstations));
        }

        private void mnu_Cards_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCards));
        }

        private void mnu_SystemIDSetting_Click(object sender, EventArgs e)
        {
            FrmWorkstationSelection frm = new FrmWorkstationSelection();
            if (frm.ShowDialog() == DialogResult.OK && WorkStationInfo.CurrentStation.StationID != frm.SelectedWorkstation)
            {
                AppSettings.CurrentSetting.WorkstationID = frm.SelectedWorkstation;
                MessageBox.Show(Resource1.FrmMain_AlterStationIDQuery, Resource1.Form_Query, MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Environment.Exit(0);
            }
        }

        private void MenuSysParameterSetting_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmSystemOption));
        }

        private void mnu_ZSTSetting_Click(object sender, EventArgs e)
        {
            Form frm = FrmZSTSetting.GetInstance();
            frm.ShowDialog();
        }

        private void mnu_VehicleLedSetting_Click(object sender, EventArgs e)
        {
            FrmVehicleLedSetting frm = FrmVehicleLedSetting.GetInstance();
            frm.ShowDialog();

            try
            {
                //查找事件列表中是否已有车辆信息显示屏窗口
                foreach (IReportHandler handler in this._eventHandlers)
                {
                    if (handler.GetType().Name == typeof(FrmVehicleLedSetting).Name)
                    {
                        return;
                    }
                }

                //如果没有，检查是否需要添加到事件列表中
                VehicleLedSetting vehicleLedSetting = frm.VehicleLedSetting;
                if (vehicleLedSetting != null)
                {
                    List<VehicleLedItem> items = vehicleLedSetting.GetLEDs(WorkStationInfo.CurrentStation.StationID);
                    if (items != null && items.Count > 0)
                    {
                        //如果该工作站下有通信的车辆信息显示屏，将窗口添加到事件列表中
                        this._eventHandlers.Add(frm);
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }


        private void mnu_HostStandbySetting_Click(object sender, EventArgs e)
        {
            FrmHostStandbySetting frm = new FrmHostStandbySetting();
            frm.ShowDialog();
        }

        private void mnu_ExportParameter_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileName = "ParkingParameter.xml";
            saveFileDialog1.Filter = Resource1.Form_XMLFilter;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ParkingParameterDataBufferBll bll = new ParkingParameterDataBufferBll(AppSettings.CurrentSetting.CurrentMasterConnect);
                CommandResult result = bll.ExportParameter(saveFileDialog1.FileName);
                if (result.Result == ResultCode.Successful)
                {
                    MessageBox.Show(Resource1.Form_ExportSuccess);
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
        }

        private void mnu_SyncDataToStandby_Click(object sender, EventArgs e)
        {
            if (_StandbyToMasterSyncService != null) _StandbyToMasterSyncService.Pause();
            FrmSyncDataToStandby frm = new FrmSyncDataToStandby();
            frm.ShowDialog();
            if (_StandbyToMasterSyncService != null) _StandbyToMasterSyncService.Recover();
        }

        private void mnu_ParkingPayDetailReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardPaymentReport));
        }

        private void mnu_OperatorShiftStatistics_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmOperatorLogReport));
        }

        private void mnu_APM_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmAPMMaster));
        }

        private void mnu_RoadWays_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmRoadWay));
        }

        private void mnu_CardChargeReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardChargeReport));
        }

        private void mnu_CardDeferReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardDeferReport));
        }

        private void mnu_CardLostRestoreReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardLostRestoreReport));
        }

        private void mnu_AlarmStatistics_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmAlarmReport));
        }

        private void mnu_CardDisableEnableReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardDisableEnableReport));
        }

        private void mnu_CardEventReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardEventReport));
        }

        private void mnu_CardInPark_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardInParkReport));
        }

        private void mnu_CarFlowStatistics_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCarFlowStatistics));
        }

        private void mnu_CardRecycleReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardRecycleReport));
        }

        private void mnu_CardReleaseReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardReleaseReport));
        }

        private void mnu_CardPaying_Click(object sender, EventArgs e)
        {
            if (WorkStationInfo.CurrentStation.IsCenterCharge)
            {
                ShowSingleForm(typeof(FrmCardCenterCharge));
            }
            else
            {
                ShowSingleForm(typeof(FrmCardPaying));
            }
        }

        private void mnu_HotelAuthorization_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmHotelAuthorization));
        }

        private void mnu_CardOut_Click(object sender, EventArgs e)
        {
            FrmCardOut frm = new FrmCardOut();
            frm.ShowDialog();
        }

        private void mnu_APMRefund_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmAPMRefund));
        }

        private void mnu_NoCardLost_Click(object sender, EventArgs e)
        {
            FrmNoCardLost frm = new FrmNoCardLost();
            frm.ShowDialog();
        }

        private void mnu_SpeedingProcess_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmSpeedingProcess));
        }


        private void btn_Monitor_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmMonitor));
        }

        private void btn_HardWare_Click(object sender, EventArgs e)
        {
            this.panelLeft.Visible = true;
            this.splitter1.Visible = true;
            splitter1_SplitterMoved(splitter1, new SplitterEventArgs(0, 0, 0, 0));
        }

        private void mnu_ChangePwd_Click(object sender, EventArgs e)
        {
            FrmChangePwd frm = new FrmChangePwd();
            frm.Operator = OperatorInfo.CurrentOperator;
            frm.ShowDialog();
        }

        private void mnu_OperatorShift_Click(object sender, EventArgs e)
        {
            //清空强制交班的参数
            tmrForceShifting.Enabled = false;
            _ForceShiftingAlarmCount = 0;

            decimal? handInCash = null;
            decimal? handInPOS = null;
            CardInfo operatorCard = null;

            if (UserSetting.Current.OperatorCardCashWhenSettle && AppSettings.CurrentSetting.EnableWriteCard)
            {
                FrmOperatorCardCashComfirm frmOperator = new FrmOperatorCardCashComfirm();
                frmOperator.Operator = OperatorInfo.CurrentOperator;
                if (frmOperator.ShowDialog() == DialogResult.OK)
                {
                    operatorCard = frmOperator.OperatorCard;
                }
            }

            if (UserSetting.Current.InputHandInCashWhenSettle)
            {
                FrmHandInCashConfirm frm = new FrmHandInCashConfirm();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    handInCash = frm.HandInCash;
                    handInPOS = frm.HandInPOS;
                }
                else
                {
                    return;
                }
            }
            FrmOperatorSettle frmShift = new FrmOperatorSettle();
            frmShift.Operator = OperatorInfo.CurrentOperator;
            frmShift.HandInCash = handInCash;
            frmShift.HandInPOS = handInPOS;
            frmShift.OperatorCard = operatorCard;
            if (frmShift.ShowDialog() == DialogResult.OK)
            {
                Authenticate(false);
                InitSystemParameters();
                InitWorkStation();
            }
        }

        private void mnu_SystemOption_Click(object sender, EventArgs e)
        {
            FrmSystemOption frm = new FrmSystemOption();
            frm.ShowDialog();
            this.lblEventServiceStatus.Text = AppSettings.CurrentSetting.EnableWriteCard ? Resource1.EnableWriteCard : string.Empty;
        }


        private void mnu_LocalSettings_Click(object sender, EventArgs e)
        {
            FrmLocalSettings frm = new FrmLocalSettings();
            frm.ShowDialog();
            this.lblEventServiceStatus.Text = AppSettings.CurrentSetting.EnableWriteCard ? Resource1.EnableWriteCard : string.Empty;
        }

        private void mnu_Alarm_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmAlarmReport));
        }

        private void mnu_CardDeferStatistic_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardDeferStatistics));
        }

        private void mnu_CarplateReg_Click(object sender, EventArgs e)
        {
            Form _CarPlateForm = null;

            if (sender == this.mnu_CarplateRegW)
            {
                _CarPlateForm = FrmCarPlateOfWintone.GetInstance();
            }
            else if (sender == this.mnu_CarplateRegV)
            {
                _CarPlateForm = FrmCarPlateOfVecon.GetInstance();
            }
            else if (sender == this.mnu_CarplateRegX)
            {
                _CarPlateForm = FrmCarPlateOfXinLuTong.GetInstance();
            }
            else if (sender == this.mnu_CarplateRegD)
            {
                _CarPlateForm = FrmCarPlateOfDaHua.GetInstance();
            }

            if (_CarPlateForm != null)
            {
                //ResourceUtil.ApplyResource(_CarPlateForm);
                _CarPlateForm.ShowInTaskbar = false;
                _CarPlateForm.WindowState = FormWindowState.Normal;
                _CarPlateForm.Show();
                _CarPlateForm.Activate();
            }
        }

        private void mnu_CarPlateTestForFile_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCarplateTestForFile));
        }

        private void mnu_CarPlateTestForVideo_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCarplateTestForVideo));
        }

        private void mnu_Manual_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(System.IO.Path.Combine(Application.StartupPath, Resource1.FrmMain_ManualFilePDF));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnu_ReportDetail_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCarTypeReport));
        }

        private void mnu_CardPayingStatistic_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardPayingStatistics));
        }

        private void mnu_Aboat_Click(object sender, EventArgs e)
        {
            frmAboat frm = new frmAboat();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.ShowDialog();
        }

        private void mnu_SnapShoter_Click(object sender, EventArgs e)
        {
            Form frm = FrmSnapShoter.GetInstance();
            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;
            frm.Show();
            frm.Activate();
        }

        private void mnu_PayOperationLogReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmAPMLogReport));
        }


        private void mnu_APMCheckOutRecordRport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmAPMCheckOutReport));
        }

        private void mnu_APMRefundRecordRport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmAPMRefundReport));
        }

        private void mnu_CardDeleteReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardDeleteReport));
        }

        private void mnu_YCTLog_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmYangChenTongLogReport));
        }

        private void mnu_WaitingCommand_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmWaitingCommandReport));
        }

        private void mnu_FreeAuthorizationLog_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmFreeAuthorizationLogReport));
        }

        private void mnu_SpeedingReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmSpeedingReport));
        }

        private void mnu_ServerSwitchReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmServerSwitchReport));
        }

        private void mnu_HasPaidCardReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmHasPaidCardReport));
        }

        private void mnu_Language_Clicked(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show(Resources.Resource1.FrmChangLanguage, Resources.Resource1.Form_Query, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            int openedfromcount = _openedForms.Count;
            for (int i = openedfromcount - 1; i > -1; i--)
            {
                _openedForms[i].Close();
            }
            foreach (ToolStripMenuItem item in mnu_Language.DropDownItems)
            {
                item.Checked = false;
            }

            if (sender == mnu_SimpleChinese)
            {
                mnu_SimpleChinese.Checked = true;
                AppSettings.CurrentSetting.Language = "zh-Hans";
            }
            else if (sender == mnu_TraditionalChinese)
            {
                mnu_TraditionalChinese.Checked = true;
                AppSettings.CurrentSetting.Language = "zh-Hant";
            }
            else if (sender == mnu_English)
            {
                mnu_English.Checked = true;
                AppSettings.CurrentSetting.Language = "en";
            }
            System.Globalization.CultureInfo cli = new System.Globalization.CultureInfo(AppSettings.CurrentSetting.Language);
            if (System.Threading.Thread.CurrentThread.CurrentUICulture != cli)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = cli;
                System.Threading.Thread.CurrentThread.CurrentCulture = cli;
                ResourceUtil.ApplyResource(this);
                this.lblOperator.Text = string.Format(Resource1.FrmMain_lblOperator, OperatorInfo.CurrentOperator.OperatorName);
                this.lblStation.Text = string.Format(Resource1.FrmMain_lblStation, WorkStationInfo.CurrentStation.StationName);
                this.lblStartFrom.Text = string.Format(Resource1.FrmMain_lblStartFrom, _StartFrom.ToString("yyyy-MM-dd HH:mm:ss"));
                this.lblEventServiceStatus.Text = AppSettings.CurrentSetting.EnableWriteCard ? Resource1.EnableWriteCard : string.Empty;
                this.lblCommuicationStatus.Text = _InitParkingCommunication ? string.Empty : Resource1.FrmMain_CommunicationFailure;
                this.lblServerSwitch.Text = _ServerSwitchRemind == null ? string.Empty : string.Format(Resource1.FrmMain_ServerSwitchWarm, _ServerSwitchRemind.Park, _ServerSwitchRemind.SwitchTime, _ServerSwitchRemind.SwitchStatus, _ServerSwitchRemind.SwitchIP);
                _DataBaseConnectionChecker.CurrentUICulture = cli;
                this.ShowDataBaseStatusHandler(this, EventArgs.Empty);
                this.formPanel1.Fresh();
                RenderHardwareTree();
            }
        }
        #endregion

        #region 窗口事件处理
        private void FrmMain_Load(object sender, EventArgs e)
        {
            _StartFrom = DateTime.Now;

            if (AppSettings.CurrentSetting.AuotAddToFirewallException)
            {
                string appPath = Application.ExecutablePath;
                string appName = System.IO.Path.GetFileNameWithoutExtension(appPath);

                Ralid.GeneralLibrary.FireWall.FireWallHelper.NetFwAddApps(appName, appPath, true);
            }

            //用于所有工作站软件都要加密狗的情形
            ReadSoftDog();
            this.tmrCheckDog.Enabled = true;

            ShowLanguage();
            Authenticate(true);
            
            ParkBuffer.Current = new ParkBuffer(AppSettings.CurrentSetting.ParkConnect);
            ParkBuffer.Current.InValid(AppSettings.CurrentSetting.AvailableParkConnect);  //获取所有硬件信息

            GetParkServerLastIP(ParkBuffer.Current.Parks);//获取停车场上一次服务器使用情况
            //服务器切换处理
            ServerSwitchHandle();

            ShowDataBaseStatusHandler(this, EventArgs.Empty);
            //启动数据库连接检查服务
            _DataBaseConnectionChecker = new DataBaseConnectionChecker(AppSettings.CurrentSetting.MasterParkConnect, AppSettings.CurrentSetting.StandbyParkConnect);
            _DataBaseConnectionChecker.DataBaseStatusChangedEvent -= ShowDataBaseStatusHandler;
            _DataBaseConnectionChecker.DataBaseStatusChangedEvent += ShowDataBaseStatusHandler;
            _DataBaseConnectionChecker.CurrentUICulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            _DataBaseConnectionChecker.Start();


            InitSystemParameters();
            InitWorkStation();

            this._eventHandlers.Add(entranceTree);


            foreach (ParkInfo park in ParkBuffer.Current.Parks)
            {
                if (WorkStationInfo.CurrentStation.StationID == park.HostWorkstation)  //如果本工作站是停车场的通讯工作站
                {
                    //只有服务器用到加密狗的情形
                    //ReadSoftDog();
                    //this.tmrCheckDog.Enabled = true;


                    WorkStationInfo.CurrentStation.IsHostWorkstation = true;

                    ////如果启用了软件车牌识别,并且用的摄像机类型是信路通的,车牌识别器也是信路通的,那么就不用再启用事件抓拍,而是由识别器上传图片
                    //if (UserSetting.Current.EnableCarPlateRecognize && UserSetting.Current.SoftWareCarPlateRecognize && //启用车牌识别
                    //    UserSetting.Current.VideoType == 1 && //用的摄像机是信路通
                    //    AppSettings.CurrentSetting.GetConfigContent("CarPlateRecognization") == "XinLuTong")
                    //{

                    //}
                    ////如果用的摄像机类型是大华的,车牌识别器默认使用大华的,那么就不用再启用事件抓拍,而是由识别器上传图片,不管有没有启用车牌识别
                    //else if (UserSetting.Current.VideoType == 3)
                    //{
                    //    StartCarPlateRecognize("DaHua");//默认使用大华摄像机识别
                    //}
                    //else
                    //{
                    //    this._eventHandlers.Add(FrmSnapShoter.GetInstance());   //对事件进行抓拍图片
                    //    SnapShotCaptureService.CurrentInstance = new SnapShotCaptureService(FrmSnapShoter.GetInstance());//创建快照抓拍服务实例
                    //    this.mnu_SnapShoter.Enabled = true;
                    //}

                    this._eventHandlers.Add(FrmSnapShoter.GetInstance());   //对事件进行抓拍图片
                    SnapShotCaptureService.CurrentInstance = new SnapShotCaptureService(FrmSnapShoter.GetInstance());//创建快照抓拍服务实例
                    this.mnu_SnapShoter.Enabled = true;
                    FrmSnapShoter.GetInstance().Hide();

                    StartVideoCapture();//启动摄像机抓拍管理
                    

                    if (UserSetting.Current.EnableCarPlateRecognize && UserSetting.Current.SoftWareCarPlateRecognize) //启用车牌识别
                    {
                        StartCarPlateRecognize();
                        //StartCarPlateRecognize(AppSettings.CurrentSetting.GetConfigContent("CarPlateRecognization"));
                    }

                    if (UserSetting.Current.EnableDeleteOverTimeImages
                        || UserSetting.Current.EnableDeleteOverTimeCardEvents)
                    {
                        this.tmrDeleteOverTime.Enabled = true;
                        this.tmrDeleteOverTime.Interval = 5 * 60 * 1000; //5分钟
                    }

                    if (!string.IsNullOrEmpty(AppSettings.CurrentSetting.MasterParkConnect)
                        && !string.IsNullOrEmpty(AppSettings.CurrentSetting.StandbyParkConnect))
                    {
                        //启动备用数据库同步数据到主数据库服务
                        _StandbyToMasterSyncService = new StandbyToMasterSyncService(AppSettings.CurrentSetting.MasterParkConnect, AppSettings.CurrentSetting.StandbyParkConnect);
                        _StandbyToMasterSyncService.SyncInterval = 1;
                        _StandbyToMasterSyncService.Start();
                    }

                    break;
                }
            }

            //启动同步时间服务
            _DatetimeSyncService = new DatetimeSyncService(AppSettings.CurrentSetting.ParkConnect);
            _DatetimeSyncService.Start();
            //启动上传本地记录服务
            _LDB_UpdateLoaclDataService = new LDB_UpdateLoaclDataService(LDB_AppSettings.Current.LDBConnect, AppSettings.CurrentSetting.MasterParkConnect);
            _LDB_UpdateLoaclDataService.Start();

            //初始化停车场通讯
            System.Threading.Thread t = new Thread(InitParkingCommunication);
            t.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            t.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            t.Start();

            this.lblStartFrom.Text = string.Format(Resource1.FrmMain_lblStartFrom, _StartFrom.ToString("yyyy-MM-dd HH:mm:ss"));

            //交委接口
            InitThirdCommunicationServer();

            //初始化停车场满位屏显示
            ParkVacantLedRender ledRender = new ParkVacantLedRender();
            this._eventHandlers.Add(ledRender);

            //初始化车辆信息LED屏显示
            if (AppSettings.CurrentSetting.VehicleLedCOMPort > 0)
            {
                VehicleLedRender vehicleLedRender = new VehicleLedRender();
                this._eventHandlers.Add(vehicleLedRender);
            }

            //记录启动时间
            Ralid.GeneralLibrary.LOG.FileLog.Log("系统", "软件启动 " + Application.ProductVersion);

            if (AppSettings.CurrentSetting.EnableZST)
            {
                FrmZSTSetting frm = FrmZSTSetting.GetInstance();
                this._eventHandlers.Add(frm);
                frm.Init();
                frm.Hide();
            }

            //初始化车辆信息显示屏设置窗口
            FrmVehicleLedSetting vehicleFrm = FrmVehicleLedSetting.GetInstance();
            vehicleFrm.Init();
            vehicleFrm.Hide();
            VehicleLedSetting vehicleLedSetting = vehicleFrm.VehicleLedSetting;
            if (vehicleLedSetting != null)
            {
                List<VehicleLedItem> items = vehicleLedSetting.GetLEDs(WorkStationInfo.CurrentStation.StationID);
                if (items != null && items.Count > 0)
                {
                    //如果该工作站下有通信的车辆信息显示屏，将窗口添加到事件列表中
                    this._eventHandlers.Add(vehicleFrm);
                }
            }


            //这一行创建一个信路通容器窗体实例
            Ralid.Park.UserControls.VideoPanels.FrmXinlutongContainer form1 = Ralid.Park.UserControls.VideoPanels.FrmXinlutongContainer.GetInstance();
            form1.Init();

            ////屏蔽停车场单独费率支持的功能
            //this.mnu_SetParkTariff.Visible = false;
            //this.mnu_DeleteParkTariff.Visible = false;
        }

        private void pad_ParkApaterReconnected(object sender, EventArgs e)
        {
            Ralid.Park.ParkAdapter.ParkingAdapter pad = sender as Ralid.Park.ParkAdapter.ParkingAdapter;
            if (pad != null)
            {
                ParkInfo padPark = ParkBuffer.Current.GetPark(pad.ParkID);
                if (padPark != null)
                {
                    //检查服务器有没有切换
                    CheckServerSwitch(padPark);
                }

                Action handler = delegate()
                {
                    TreeNode parkNode = entranceTree.GetParkNode(pad.ParkID);
                    if (parkNode != null)
                    {
                        ParkInfo park = parkNode.Tag as ParkInfo;
                        park.Status = pad.GetParkStatus();
                        entranceTree.RenderPark(parkNode, park);

                        foreach (TreeNode node in entranceTree.EntranceNodes)
                        {
                            EntranceInfo entrance = node.Tag as EntranceInfo;
                            if (entrance.ParkID == pad.ParkID)
                            {
                                entrance.Status = pad.GetEntranceStatus(entrance.EntranceID);
                                EntranceInfo info = ParkBuffer.Current.GetEntrance(entrance.EntranceID);
                                if (info != null) info.Status = entrance.Status;
                                entranceTree.RenderEntrance(node, entrance);
                            }
                        }
                    }
                };
                if (this.InvokeRequired)
                {
                    this.Invoke(handler);
                }
                else
                {
                    handler();
                }
            }
        }

        private void pad_ParkAdapterConnectFail(object sender, EventArgs e)
        {
            Action handler = delegate()
            {
                Ralid.Park.ParkAdapter.ParkingAdapter pad = sender as Ralid.Park.ParkAdapter.ParkingAdapter;
                if (pad != null)
                {
                    TreeNode parkNode = entranceTree.GetParkNode(pad.ParkID);
                    if (parkNode != null)
                    {
                        ParkInfo park = parkNode.Tag as ParkInfo;
                        park.Status = EntranceStatus.OffLine;
                        entranceTree.RenderPark(parkNode, park);

                        foreach (TreeNode node in entranceTree.EntranceNodes)
                        {
                            EntranceInfo entrance = node.Tag as EntranceInfo;
                            if (entrance.ParkID == pad.ParkID)
                            {
                                entrance.Status = EntranceStatus.OffLine;
                                entranceTree.RenderEntrance(node, entrance);
                            }
                        }
                    }
                }
            };
            if (this.InvokeRequired)
            {
                this.Invoke(handler);
            }
            else
            {
                handler();
            }
        }

        private void btnCloseLeft_Click(object sender, EventArgs e)
        {
            this.panelLeft.Visible = false;
            this.splitter1.Visible = false;
            splitter1_SplitterMoved(splitter1, new SplitterEventArgs(0, 0, 0, 0));
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            //foreach (Form frm in this.MdiChildren)
            //{
            //    frm.Size = GetMdiChildMaxSize();
            //    frm.Location = new Point(0, 0);
            //}
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            splitter1_SplitterMoved(splitter1, new SplitterEventArgs(0, 0, 0, 0));
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (ParkingAdapter pad in ParkingAdapterManager.Instance.ParkAdapters)
            {
                pad.UnSubscription();
            }
            if (_OperatorLoginTime.HasValue)
            {
                LogOperatorLogOut();
            }
            Ralid.GeneralLibrary.LOG.FileLog.Log("系统", "软件退出 " + Application.ProductVersion);
            Environment.Exit(0);
        }

        private void tmrDeleteOverTime_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.Now.Hour == 2)  //每天凌晨2点删除过期的抓拍图片和进出记录
                {
                    if (!_hasDeleted)
                    {
                        if (UserSetting.Current.EnableDeleteOverTimeImages)
                        {
                            Action delSnapshot = delegate()
                            {
                                DateTime dt = DateTime.Today.AddMonths(-UserSetting.Current.Month);
                                SnapShotBll ssb = new SnapShotBll(AppSettings.CurrentSetting.ImageDBConnStr);
                                ssb.DeleteAllSnapShotBefore(dt);
                                if (DataBaseConnectionsManager.Current.StandbyConnected)
                                {
                                    //如果备用数据库连接上了，删除备用数据库的过期抓拍图片
                                    SnapShotBll sbssb = new SnapShotBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                                    sbssb.DeleteAllSnapShotBefore(dt);
                                }
                            };
                            Thread t = new Thread(new ThreadStart(delSnapshot));
                            t.IsBackground = true;
                            t.Start();
                        }

                        if (UserSetting.Current.EnableDeleteOverTimeCardEvents)
                        {
                            Action delCardEvent = delegate()
                            {
                                DateTime dt = DateTime.Today.AddMonths(-UserSetting.Current.CardEventMonth);
                                CardEventBll ceb = new CardEventBll(AppSettings.CurrentSetting.ParkConnect);
                                ceb.DeleteAllCardEventBefore(dt);
                                if (DataBaseConnectionsManager.Current.StandbyConnected)
                                {
                                    //如果备用数据库连接上了，删除备用数据库的过期进出记录
                                    CardEventBll sbceb = new CardEventBll(AppSettings.CurrentSetting.CurrentStandbyConnect);
                                    sbceb.DeleteAllCardEventBefore(dt);
                                }
                            };
                            Thread t2 = new Thread(new ThreadStart(delCardEvent));
                            t2.IsBackground = true;
                            t2.Start();
                        }

                        _hasDeleted = true;
                    }
                }
                else
                {
                    _hasDeleted = false;
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void tmrForceShifting_Tick(object sender, EventArgs e)
        {
            TimeEntity te = UserSetting.Current.ForceShiftingTime;
            if (te == null) return;

            DateTime dt = DateTime.Now;
            if (dt.Hour == te.Hour && dt.Minute == te.Minute)
            {
                if (AppSettings.CurrentSetting.EnableTTS) Ralid.GeneralLibrary.Speech.TTSSpeech.Instance.Speek(Resource1.FrmMain_ShitNow);
                this.mnu_OperatorShift_Click(this.mnu_OperatorShift, EventArgs.Empty);  //交班
            }
            else
            {
                //每5分钟提醒一次，共提醒两次
                TimeEntity te10 = te.AddMinutes(-10);
                if (_ForceShiftingAlarmCount < 1 && dt.Hour == te10.Hour && dt.Minute == te10.Minute)
                {
                    _ForceShiftingAlarmCount = 1;
                    if (AppSettings.CurrentSetting.EnableTTS) Ralid.GeneralLibrary.Speech.TTSSpeech.Instance.Speek(string.Format(Resource1.FrmMain_ShiftAlert, 10));
                }
                TimeEntity te5 = te.AddMinutes(-5);
                if (_ForceShiftingAlarmCount < 2 && dt.Hour == te5.Hour && dt.Minute == te5.Minute)
                {
                    _ForceShiftingAlarmCount = 2;
                    if (AppSettings.CurrentSetting.EnableTTS) Ralid.GeneralLibrary.Speech.TTSSpeech.Instance.Speek(string.Format(Resource1.FrmMain_ShiftAlert, 5));
                }
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OperatorInfo.CurrentOperator != null)
            {
                DialogResult result;
                if (AppSettings.CurrentSetting.NeedPasswordWhenExit)
                {
                    Form frmclose = new FrmClose();
                    result = frmclose.ShowDialog();
                }
                else
                {
                    result = MessageBox.Show(Resources.Resource1.Form_ExitSystemQuery, Resources.Resource1.Form_Query, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
                if (result != DialogResult.OK) e.Cancel = true;
            }
        }

        private void ShowDataBaseStatusHandler(object sender, EventArgs e)
        {
            if (DataBaseConnectionsManager.Current.MasterStatus == DataBaseConnectionStatus.Connected)
            {
                foreach (ParkInfo park in ParkBuffer.Current.Parks)
                {
                    if (park.IsRootPark &&
                        (park.HostWorkstation == WorkStationInfo.CurrentStation.StationID || WorkStationInfo.CurrentStation.IsInListenList(park))//如果是托管的停车场或者侦听了事件的停车场
                        )
                    {
                        //检查监听的服务器有没有切换
                        CheckServerSwitch(park);
                    }
                }
            }

            Action action = delegate()
            {
                if (DataBaseConnectionsManager.Current.MasterStatus == DataBaseConnectionStatus.Connected)
                {
                    this.lblMasterStatus.Text = Resource1.FrmMain_MasterConnected;
                    this.lblMasterStatus.ForeColor = Color.Black;
                    if (_StandbyToMasterSyncService != null) _StandbyToMasterSyncService.SyncDataBase();
                    if (_LDB_UpdateLoaclDataService != null) _LDB_UpdateLoaclDataService.UpdateLoaclData();
                }
                else if (DataBaseConnectionsManager.Current.MasterStatus == DataBaseConnectionStatus.Disconnect)
                {
                    this.lblMasterStatus.Text = Resource1.FrmMain_MasterDisconnect;
                    this.lblMasterStatus.ForeColor = Color.Red;
                }
                else if (DataBaseConnectionsManager.Current.MasterStatus == DataBaseConnectionStatus.Unconnected
                    && !string.IsNullOrEmpty(AppSettings.CurrentSetting.MasterParkConnect))
                {
                    this.lblMasterStatus.Text = Resource1.FrmMain_MasterUnconnected;
                    this.lblMasterStatus.ForeColor = Color.Red;
                }
                else
                {
                    this.lblMasterStatus.Text = string.Empty;
                }

                if (DataBaseConnectionsManager.Current.StandbyStatus == DataBaseConnectionStatus.Connected)
                {
                    this.lblStandbyStatus.Text = Resource1.FrmMain_StandbyConnected;
                    this.lblStandbyStatus.ForeColor = Color.Black;
                }
                else if (DataBaseConnectionsManager.Current.StandbyStatus == DataBaseConnectionStatus.Disconnect)
                {
                    this.lblStandbyStatus.Text = Resource1.FrmMain_StandbyDisconnect;
                    this.lblStandbyStatus.ForeColor = Color.Red;
                }
                else if (DataBaseConnectionsManager.Current.StandbyStatus == DataBaseConnectionStatus.Unconnected
                    && !string.IsNullOrEmpty(AppSettings.CurrentSetting.StandbyParkConnect))
                {
                    this.lblStandbyStatus.Text = Resource1.FrmMain_StandbyUnconnected;
                    this.lblStandbyStatus.ForeColor = Color.Red;
                }
                else
                {
                    this.lblStandbyStatus.Text = string.Empty;
                }

                this.mnu_SyncDataToStandby.Enabled = OperatorInfo.CurrentOperator != null
                && OperatorInfo.CurrentOperator.Role.Permit(Permission.SyncDataToStandby)
                && DataBaseConnectionsManager.Current.MasterConnected
                && DataBaseConnectionsManager.Current.StandbyConnected;
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
        private void mnu_ImportCardPayments_Click(object sender, EventArgs e)
        {
            List<string> records = GetRecordsFormFile(RecordType.CardPaymentRecord);
            if (records != null)
            {
                SaveRecordsToPark(records, RecordType.CardPaymentRecord);
            }
        }
        private void mnu_ImportChargeRecords_Click(object sender, EventArgs e)
        {
            List<string> records = GetRecordsFormFile(RecordType.CardChargeRecord);
            if (records != null)
            {
                SaveRecordsToPark(records, RecordType.CardChargeRecord);
            }
        }
        #endregion

        private void mnu_CardReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardReport));
        }

        private void mnu_PosSyncTool_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(Ralid.Park.POS.FrmMain));
        }

        private void mnu_Depts_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmDepts));
        }

        private void mnu_SetParkTariff_Click(object sender, EventArgs e)
        {
            FrmParkTarrif tarrif = null;
            TreeNode node = this.entranceTree.SelectedNode;
            if (entranceTree.IsParkNode(node))
            {
                tarrif = new FrmParkTarrif();
                tarrif.ParkID = (node.Tag as ParkInfo).ParkID;
                tarrif.ParkName = (node.Tag as ParkInfo).ParkName;
            }
            if (tarrif != null)
                tarrif.ShowDialog();
        }

        private void mnu_DeleteParkTariff_Click(object sender, EventArgs e)
        {
            CommandResult result = null;
            DialogResult ret = MessageBox.Show(Resource1.FrmMain_DeleteParkTariff, Resource1.Form_Query, MessageBoxButtons.YesNo);
            if (ret == DialogResult.Yes)
            {
                TreeNode node = this.entranceTree.SelectedNode;
                ParkInfo park = node.Tag as ParkInfo;
                if (entranceTree.IsParkNode(node) && park != null)
                {
                    TariffSetting ts = TariffSetting.Current;
                    ts.ParkTariffDictionary.Remove(park.ParkID);
                    SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect);
                    result = ssb.SaveSetting<TariffSetting>(ts);
                    if (result.Result != ResultCode.Successful)
                    {
                        MessageBox.Show(result.Message);
                    }
                    else
                    {
                        MessageBox.Show(Resource1.Form_Success); 
                    }
                }
            }
        }

    }
}
