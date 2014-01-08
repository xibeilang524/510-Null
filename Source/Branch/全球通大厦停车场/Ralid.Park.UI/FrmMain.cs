using System;
using System.Collections.Generic;
using System.Drawing;
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
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.UI.ReportAndStatistics;
using Ralid.Park.ParkAdapter;
using Ralid.GeneralLibrary.SoftDog;
using Ralid.Park.UI.Resources;
using Ralid.GeneralLibrary;
using Ralid.Park.PlateRecognition;
using Ralid.GeneralLibrary.CardReader;

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
        private Form _CarPlateForm;
        private bool _hasDeleted = false;
        private int _ForceShiftingAlarmCount = 0;
        private DatetimeSyncService _DatetimeSyncService;
        private DateTime _StartFrom; //软件启动时间
        private TreeNode _EditNode;
        #endregion

        #region 私有方法
        private void ReadSoftDog()
        {
            if (!ParkingSoftDogVerify.VerifyRight())
            {
                System.Environment.Exit(0);
            }
        }

        private void tmrCheckDog_Tick(object sender, EventArgs e)
        {
            tmrCheckDog.Enabled = false;
            ParkingSoftDogVerify.Check();
            tmrCheckDog.Enabled = true;
        }

        private void Authenticate()
        {
            while (true)
            {
                Form login = new FrmLogin();
                DialogResult result = login.ShowDialog();
                if (result == DialogResult.OK)
                {
                    WorkStationInfo ws = GetWorkstationID();
                    WorkStationInfo.CurrentStation = ws;
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
            WorkstationBll wbll = new WorkstationBll(AppSettings.CurrentSetting.ParkConnect);
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
            }
            return w;
        }

        private void InitParkingCommunication()
        {
            if (GlobalVariables.EnableInitParkingCommunication)
            {
                ServiceHelper helper = new ServiceHelper();
                helper.StartServer(WorkStationInfo.CurrentStation.StationID);
            }
            else
            {
                MessageBox.Show(Resource1.FrmMain_ConfirmIP, Resource1.Form_Alert);
                this.lblCommuicationStatus.Text = Resource1.FrmMain_CommunicationFailure;
            }

            ParkBuffer.Current.InValid();
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

        private void InitSystemParameters()
        {
            //初始化系统设置
            SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.ParkConnect);
            UserSetting.Current = ssb.GetOrCreateSetting<UserSetting>();
            HolidaySetting.Current = ssb.GetOrCreateSetting<HolidaySetting>();
            AccessSetting.Current = ssb.GetOrCreateSetting<AccessSetting>();
            TariffSetting.Current = ssb.GetOrCreateSetting<TariffSetting>();
            CarTypeSetting.Current = ssb.GetOrCreateSetting<CarTypeSetting>();
            CustomCardTypeSetting.Current = ssb.GetOrCreateSetting<CustomCardTypeSetting>();
            KeySetting.Current = ssb.GetOrCreateSetting<KeySetting>();
            //添加读卡器读取扇区2和密钥
            CardReaderManager.GetInstance(UserSetting.Current.WegenType).AddReadSectionAndKey((int)ICCardSection.Parking, GlobalVariables.ParkingKey);
        }

        private void StartCarPlateRecognize()
        {
            try
            {
                string carplate = AppSettings.CurrentSetting.GetConfigContent("CarPlateRecognization");
                if (carplate == "WINTONE")
                {
                    FrmCarPlateOfWintone frm = FrmCarPlateOfWintone.GetInstance();
                    frm.Init();
                    _CarPlateForm = frm;
                    PlateRecognitionService.CurrentInstance = new PlateRecognitionService(frm);
                    this.mnu_CarPlateTestForFile.Visible = true;
                    this.mnu_CarPlateTestForVideo.Visible = true;
                }
                else if (carplate == "VECON")
                {
                    FrmCarPlateOfVecon frm = FrmCarPlateOfVecon.GetInstance();
                    frm.Init();
                    _CarPlateForm = frm;
                    PlateRecognitionService.CurrentInstance = new PlateRecognitionService(frm);
                    this.mnu_CarPlateTestForFile.Visible = true;
                    this.mnu_CarPlateTestForVideo.Visible = true;
                }
                else if (carplate == "XinLuTong")
                {
                    FrmCarPlateOfXinLuTong frm = FrmCarPlateOfXinLuTong.GetInstance();
                    frm.Init();
                    _CarPlateForm = frm;
                    this._eventHandlers.Add(frm);
                    PlateRecognitionService.CurrentInstance = new PlateRecognitionService(frm);
                    this.mnu_CarPlateTestForFile.Visible = false;
                    this.mnu_CarPlateTestForVideo.Visible = false;
                }
                this.mnu_CarplateReg.Visible = true;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
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
            this.mnu_SystemOption.Enabled = (role.Permit(Permission.ReadSysSetting) || role.Permit(Permission.EditSysSetting));
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
            this.mnu_YCTLog.Enabled = role.Permit(Permission.YangChenTongLogReport);
            this.mnu_CardInPark.Enabled = role.Permit(Permission.CardInparkReport);
            this.mnu_OperatorShift.Enabled = role.Permit(Permission.OperatorSettle);
        }

        private void ProcessReport(object sender, ReportBase report)
        {
            foreach (IReportHandler handler in _eventHandlers)
            {
                handler.ProcessReport(report);
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
                AlarmDateTime = DateTime.Now,
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
        #endregion

        #region 临时车辆有关
        private CommandResult AddVisitor(string visitorPlate)
        {
            for (int i = 0; i < 3; i++) //生成一个卡号，然后保存，如果系统中已经存在此卡，则再次尝试，最多三次
            {
                CardInfo card = new CardInfo();
                card.CardID = Create7CharCardID();
                card.CardType = Ralid.Park.BusinessModel.Enum.CardType.Ticket;
                card.Status = CardStatus.Enabled;
                card.OwnerName = "临时访客";
                card.CardCertificate = "临时访客";
                card.CarPlate = visitorPlate;
                card.HolidayEnabled = true;
                card.CanRepeatIn = false;
                card.CanRepeatOut = false;
                card.WithCount = true;
                card.CanEnterWhenFull = true;
                card.EnableWhenExpired = true;
                Ralid.Park.BusinessModel.Result.CommandResult result = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).AddCard(card);
                return result;
            }
            return new CommandResult(ResultCode.Fail, "出错");
        }

        /// <summary>
        /// 生成7位的卡号，
        /// </summary>
        /// <returns></returns>
        private string Create7CharCardID()
        {
            DateTime dt = DateTime.Now;
            int id = dt.Minute * 100000 + dt.Second * 1000 + dt.Millisecond;
            if (id < 1000000)
            {
                id += 7000000;
            }
            return id.ToString();
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
            ParkBuffer.Current.InValid();
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
                string cardPlate = string.Empty;
                FrmRemoteReadCard frm = new FrmRemoteReadCard();
                EntranceInfo entrance = entranceTree.SelectedNode.Tag as EntranceInfo;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    cardPlate = frm.CarPlate;
                    if (!string.IsNullOrEmpty(frm.VisitorCarplate))
                    {
                        CardSearchCondition con = new CardSearchCondition();
                        con.CarPlate = frm.VisitorCarplate;
                        List<CardInfo> cards = (new CardBll(AppSettings.CurrentSetting.ParkConnect)).GetCards(con).QueryObjects;
                        if (cards != null && cards.Count > 0)
                        {
                            cardPlate = cards[0].CarPlate;
                        }
                        else if (cards == null || cards.Count == 0)
                        {
                            CommandResult ret = AddVisitor(frm.VisitorCarplate);
                            if (ret.Result != ResultCode.Successful)
                            {
                                MessageBox.Show("临时车辆放行失败,原因:" + ret.Message);
                                return;
                            }
                            else
                            {
                                cardPlate = frm.VisitorCarplate;
                            }
                        }
                    }
                    Action action = delegate()
                    {
                        RemoteReadCardNotify notify = new RemoteReadCardNotify(entrance.ParkID, entrance.EntranceID, cardPlate,
                            OperatorInfo.CurrentOperator.OperatorName, WorkStationInfo.CurrentStation.StationName);
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
                    ParkBuffer.Current.InValid();
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
                    ParkBuffer.Current.InValid();
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
                        ParkBuffer.Current.InValid();
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

                IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                if (pad != null) pad.UpdateEntrance(entrance);
            }
            else if (e.UpdatedItem is VideoSourceInfo)
            {
                entranceTree.RenderVideoSource(node, e.UpdatedItem as VideoSourceInfo);
            }
            ParkBuffer.Current.InValid();
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
            }
            else if (e.AddedItem is EntranceInfo)
            {
                EntranceInfo entrance = e.AddedItem as EntranceInfo;
                entranceTree.AddEntranceNode(node, entrance);
                IParkingAdapter pad = ParkingAdapterManager.Instance[entrance.RootParkID];
                if (pad != null) pad.AddEntrance(entrance);
            }
            else if (e.AddedItem is VideoSourceInfo)
            {
                entranceTree.AddVideoSourceNode(node, e.AddedItem as VideoSourceInfo);
            }
            if (node != null)
            {
                node.Expand();
            }
            ParkBuffer.Current.InValid();
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
                    this.mnu_AddDivision.Enabled = (node.Tag as ParkInfo).IsRootPark;
                    this.mnu_SearchDevice.Visible = (node.Tag as ParkInfo).DeviceType == EntranceDeviceType.NETEntrance;
                }
                else if (node != null && entranceTree.IsEntranceNode(node))
                {
                    entranceTree.ContextMenuStrip = entranceContextMenu;
                    EntranceInfo entrance = node.Tag as EntranceInfo;
                    mnu_TempCardSetting.Visible = !entrance.IsExitDevice;
                    if (entranceTree.IsParkNode(node.Parent))
                    {
                        ParkInfo park = node.Parent.Tag as ParkInfo;
                        mnu_RemoteReadCard.Visible = !park.IsWriteCardMode;
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

            Authenticate();
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
                }
                else
                {
                    return;
                }
            }
            FrmOperatorSettle frmShift = new FrmOperatorSettle();
            frmShift.Operator = OperatorInfo.CurrentOperator;
            frmShift.HandInCash = handInCash;
            frmShift.OperatorCard = operatorCard;
            if (frmShift.ShowDialog() == DialogResult.OK)
            {
                Authenticate();
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
            if (_CarPlateForm != null)
            {
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
                System.Diagnostics.Process.Start(System.IO.Path.Combine(Application.StartupPath, Resource1.FrmMain_ManualFile));
            }
            catch(Exception ex)
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

        private void mnu_CardDeleteReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmCardDeleteReport));
        }

        private void mnu_YCTLog_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmYangChenTongLogReport));
        }

        private void mnu_Language_Clicked(object sender, EventArgs e)
        {
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
                this.lblCommuicationStatus.Text = string.IsNullOrEmpty(this.lblCommuicationStatus.Text) ? string.Empty : Resource1.FrmMain_CommunicationFailure;
            }
        }
        #endregion

        #region 窗口事件处理
        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (AppSettings.CurrentSetting.AuotAddToFirewallException)
            {
                string appPath = Application.ExecutablePath;
                string appName = System.IO.Path.GetFileNameWithoutExtension(appPath);

                Ralid.GeneralLibrary.FireWall.FireWallHelper.NetFwAddApps(appName, appPath, true);
            }

            //用于所有工作站软件都要加密狗的情形
            //ReadSoftDog();
            //this.tmrCheckDog.Enabled = true;

            ShowLanguage();
            Authenticate();
            ParkBuffer.Current = new ParkBuffer(AppSettings.CurrentSetting.ParkConnect);
            ParkBuffer.Current.InValid();  //获取所有硬件信息
            InitSystemParameters();
            InitWorkStation();

            this._eventHandlers.Add(entranceTree);

            foreach (ParkInfo park in ParkBuffer.Current.Parks)
            {
                if (WorkStationInfo.CurrentStation.StationID == park.HostWorkstation)  //如果本工作站是停车场的通讯工作站
                {
                    ////只有服务器用到加密狗的情形
                    //ReadSoftDog();
                    //this.tmrCheckDog.Enabled = true;

                    //如果启用了软件车牌识别,并且用的摄像机类型是信路通的,车牌识别器也是信路通的,那么就不用再启用事件抓拍,而是由识别器上传图片
                    if (UserSetting.Current.EnableCarPlateRecognize && UserSetting.Current.SoftWareCarPlateRecognize && //启用车牌识别
                        UserSetting.Current.VideoType == 1 && //用的摄像机是信路通
                        AppSettings.CurrentSetting.GetConfigContent("CarPlateRecognization") == "XinLuTong")
                    {
                        
                    }
                    else
                    {
                        this._eventHandlers.Add(FrmSnapShoter.GetInstance());   //对事件进行抓拍图片
                        this.mnu_SnapShoter.Enabled = true;
                    }
                    FrmSnapShoter.GetInstance().Hide();

                    //if (UserSetting.Current.EnableCarPlateRecognize && UserSetting.Current.SoftWareCarPlateRecognize) //启用车牌识别
                    //{
                        StartCarPlateRecognize();
                    //}

                    if (UserSetting.Current.EnableDeleteOverTimeImages)
                    {
                        this.tmrDeleteSnapShot.Enabled = true;
                        this.tmrDeleteSnapShot.Interval = 5 * 60 * 1000; //5分钟
                    }
                    break;
                }
            }

            //启动同步时间服务
            _DatetimeSyncService = new DatetimeSyncService(AppSettings.CurrentSetting.ParkConnect);
            _DatetimeSyncService.Start();

            //初始化停车场通讯
            System.Threading.Thread t = new Thread(InitParkingCommunication);
            t.CurrentCulture = Thread.CurrentThread.CurrentCulture;
            t.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            t.Start();
            _StartFrom = DateTime.Now;
            this.lblStartFrom.Text = string.Format(Resource1.FrmMain_lblStartFrom, _StartFrom.ToString("yyyy-MM-dd HH:mm:ss"));

            //初始化停车场满位屏显示
            ParkVacantLedRender ledRender = new ParkVacantLedRender();
            this._eventHandlers.Add(ledRender);

            //记录启动时间
            Ralid.GeneralLibrary.LOG.FileLog.Log("系统", "软件启动");
            LogOperatorLogIn();

            if (UserSetting.Current.VideoType == 1)
            {
                //这一行创建一个信路通容器窗体实例
                Ralid.Park.UserControls.VideoPanels.FrmXinlutongContainer form1 = Ralid.Park.UserControls.VideoPanels.FrmXinlutongContainer.GetInstance();
                form1.Init();
            }

            mnu_CardPaying_Click(this.mnu_CardPaying, EventArgs.Empty);
        }

        private void pad_ParkApaterReconnected(object sender, EventArgs e)
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
            Ralid.GeneralLibrary.LOG.FileLog.Log("系统", "软件退出");
            Environment.Exit(0);
        }

        private void tmrDeleteSnapShot_Tick(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.Now.Hour == 2)  //每天凌晨2点删除过期的抓拍图片
                {
                    if (!_hasDeleted)
                    {
                        Action delSnapshot = delegate()
                        {
                            DateTime dt = DateTime.Today.AddMonths(-UserSetting.Current.Month);
                            SnapShotBll ssb = new SnapShotBll(AppSettings.CurrentSetting.ParkConnect);
                            ssb.DeleteAllSnapShotBefore(dt);
                        };
                        Thread t = new Thread(new ThreadStart(delSnapshot));
                        t.IsBackground = true;
                        t.Start();

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
        #endregion
    }
}
