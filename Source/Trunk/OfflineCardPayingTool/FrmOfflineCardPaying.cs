using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.UI;
using Ralid.Park.LocalDataBase.BLL;
using Ralid.Park.LocalDataBase.Model;
using Ralid.GeneralLibrary.CardReader;
using OfflineCardPayingTool.Resources;

namespace OfflineCardPayingTool
{
    public partial class FrmOfflineCardPaying : Form
    {
        #region 构造函数
        public FrmOfflineCardPaying()
        {
            //GetCurrentCulture();
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private List<Form> _openedForms = new List<Form>();
        //private List<IReportHandler> _eventHandlers = new List<IReportHandler>();
        private DateTime _StartFrom; //软件启动时间
        #endregion

        #region 私有方法
        private static void GetCurrentCulture()
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
        private void ReadSoftDog()
        {
            if (!ParkingSoftDogVerify.VerifyRight())
            {
                System.Environment.Exit(0);
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
        private void Authenticate()
        {
            WorkStationInfo ws = GetWorkstationID();
            WorkStationInfo.CurrentStation = ws;
            if (ws == null)
            {
                MessageBox.Show(Resources.Resource1.FrmOfflineCardPaying_NotWorkStationID, Resources.Resource1.Form_Alert);
                Environment.Exit(0);
            }
            else
            {
                while (true)
                {
                    Form login = new FrmLoginTool();
                    DialogResult result = login.ShowDialog();
                    if (result == DialogResult.OK)
                    {

                        break;
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }
        private WorkStationInfo GetWorkstationID()
        {
            LDB_SysParaSettingsBll ssb = new LDB_SysParaSettingsBll(LDB_AppSettings.Current.LDBConnect);
            LDB_ParkingDataBuffer.Current = ssb.GetOrCreateSetting<LDB_ParkingDataBuffer>();

            WorkStationInfo ws = null;
            string workstationID = AppSettings.CurrentSetting.WorkstationID;
            if (!string.IsNullOrEmpty(workstationID))
            {
                ws = LDB_ParkingDataBuffer.Current.WorkStations.FirstOrDefault(w => w.StationID == workstationID);
            }
            return ws;
        }
        private void InitSystemParameters()
        {
            //初始化系统设置
            LDB_SysParaSettingsBll ssb = new LDB_SysParaSettingsBll(LDB_AppSettings.Current.LDBConnect);
            UserSetting.Current = ssb.GetOrCreateSetting<UserSetting>();
            HolidaySetting.Current = ssb.GetOrCreateSetting<HolidaySetting>();
            AccessSetting.Current = ssb.GetOrCreateSetting<AccessSetting>();
            TariffSetting.Current = ssb.GetOrCreateSetting<TariffSetting>();
            CarTypeSetting.Current = ssb.GetOrCreateSetting<CarTypeSetting>();
            CustomCardTypeSetting.Current = ssb.GetOrCreateSetting<CustomCardTypeSetting>();
            KeySetting.Current = ssb.GetOrCreateSetting<KeySetting>();

            GlobalVariables.SetCardReaderKeysetting();
            
        }
        private void InitWorkStation()
        {
            this.lblOperator.Text = string.Format(Resource1.FrmOfflineCardPaying_lblOperator, OperatorInfo.CurrentOperator.OperatorName);
            this.lblStation.Text = string.Format(Resource1.FrmOfflineCardPaying_lblStation, WorkStationInfo.CurrentStation.StationName);
            this.ShowOperatorRights(OperatorInfo.CurrentOperator);  //操作员权限
        }
        private void ShowOperatorRights(OperatorInfo op)
        {
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
                //if (instance is IReportHandler) this._eventHandlers.Add(instance as IReportHandler);
                instance.MdiParent = this;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
                this.formPanel1.AddAForm(instance);
                instance.FormClosed += delegate(object sender, FormClosedEventArgs e)
                {
                    _openedForms.Remove(instance);
                };
            }
            this.formPanel1.HighLightForm(instance);
            instance.Show();
            instance.Activate();
        }
        #endregion


        #region 窗体事件

        private void FrmOfflineCardPaying_Load(object sender, EventArgs e)
        {
            ReadSoftDog();
            this.tmrCheckDog.Enabled = true;

            AppSettings.CurrentSetting = new AppSettings(Application.StartupPath + @"\RalidParking.exe.config");

            ShowLanguage();
            Authenticate();
            
            InitSystemParameters();
            InitWorkStation();

            _StartFrom = DateTime.Now;
            this.lblStartFrom.Text = string.Format(Resource1.FrmOfflineCardPaying_lblStartFrom, _StartFrom.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        private void tmrCheckDog_Tick(object sender, EventArgs e)
        {
            tmrCheckDog.Enabled = false;
            ParkingSoftDogVerify.Check();
            tmrCheckDog.Enabled = true;
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
                this.lblOperator.Text = string.Format(Resource1.FrmOfflineCardPaying_lblOperator, OperatorInfo.CurrentOperator.OperatorName);
                this.lblStation.Text = string.Format(Resource1.FrmOfflineCardPaying_lblStation, WorkStationInfo.CurrentStation.StationName);
                this.lblStartFrom.Text = string.Format(Resource1.FrmOfflineCardPaying_lblStartFrom, _StartFrom.ToString("yyyy-MM-dd HH:mm:ss"));
                lblEventServiceStatus.Text = AppSettings.CurrentSetting.EnableWriteCard ? Resource1.EnableWriteCard : string.Empty;
            }
        }
        private void mnu_SystemOption_Click(object sender, EventArgs e)
        {
            FrmSystemOption frm = new FrmSystemOption();
            frm.IsLDB = true;
            frm.ShowDialog();
        }
        private void mnu_LocalSettings_Click(object sender, EventArgs e)
        {
            FrmLocalSettings frm = new FrmLocalSettings();
            frm.ShowDialog();
        }
        private void mnu_Exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        private void btn_CardPaying_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmPaying));
        }
        private void btn_CardPaymentReport_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmUpdatePaymentRecord));
        }
        #endregion








    }
}
