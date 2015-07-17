using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.UI;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;

namespace PreferentialSystem
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private List<Form> _openedForms = new List<Form>();
        private List<IReportHandler> _eventHandlers = new List<IReportHandler>();

        private void ReadSoftDog()
        {
            if (!ParkingSoftDogVerify.VerifyRight())
            {
                System.Environment.Exit(0);
            }
        }
        private void ShowLanguage()
        {
            this.mnu_Language.Visible = false;
            //string culture = Thread.CurrentThread.CurrentCulture.Name;
            //if (culture == "zh-Hans" || culture == "zh-CHS")
            //{
            //    this.mnu_SimpleChinese.Checked = true;
            //}
            //else if (culture == "zh-Hant" || culture == "zh-CHT")
            //{
            //    this.mnu_TraditionalChinese.Checked = true;
            //}
            //else
            //{
            //    this.mnu_English.Checked = true;
            //}
        }
        private void Authenticate()
        {
            //WorkStationInfo ws = GetWorkstationID();
            //WorkStationInfo.CurrentStation = ws;
            //if (ws == null)
            //{
            //    MessageBox.Show(Resources.Resource1.FrmOfflineCardPaying_NotWorkStationID, Resources.Resource1.Form_Alert);
            //    Environment.Exit(0);
            //}
            //else
            //{
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
            //}
        }

        private void InitSystemParameters()
        {
            AppSettings.CurrentSetting = new AppSettings(Application.StartupPath + @"\PreferentialSystem.exe.config");
            SysParaSettingsBll ssb = new SysParaSettingsBll(AppSettings.CurrentSetting.AvailableParkConnect);
            UserSetting.Current = ssb.GetOrCreateSetting<UserSetting>();
            PREWorkstationSetting.Current = ssb.GetOrCreateSetting<PREWorkstationSetting>();
            PRESysOptionSetting.Current = ssb.GetOrCreateSetting<PRESysOptionSetting>();
            //从本地获取当前工作站
            string temp = AppSettings.CurrentSetting.GetConfigContent("CurrentWorkstationID");
            Guid guidValue;
            if (Guid.TryParse(temp, out guidValue) && guidValue != null)
            {
                PRESysOptionSetting.Current.PRESysOption.CurrentWorkstationID = guidValue;
                temp = AppSettings.CurrentSetting.GetConfigContent("CurrentWorkstation");
                PRESysOptionSetting.Current.PRESysOption.CurrentWorkstation = temp;
            }
        }

        private void DisplayStatusStrip()
        {
            this.toolStripStatusLabel1.Text = "当前操作员：" + PREOperatorInfo.CurrentOperator.OperatorName;
            this.toolStripStatusLabel2.Text = "优惠工作站：" + PRESysOptionSetting.Current.PRESysOption.CurrentWorkstation;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            #region 加密狗验证
            //ReadSoftDog();
            //this.tmrCheckDog.Enabled = true;
            #endregion
            //语言
            ShowLanguage();
            //登录认证
            Authenticate();
            //系统设置(初始化系统参数)
            InitSystemParameters();
            //登录后显示状态栏
            DisplayStatusStrip();
            //显示用户权限
            ShowOperatorRights(PREOperatorInfo.CurrentOperator);
        }

        private void tmrCheckDog_Tick(object sender, EventArgs e)
        {
            tmrCheckDog.Enabled = false;
            ParkingSoftDogVerify.Check();
            tmrCheckDog.Enabled = true;
        }

        private void mnu_Operator_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmPREOperators));
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

        private void mnu_RoleManager_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmPRERoles));
        }

        private void mnu_WorkStation_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmPREWorkstations));
        }

        private void mnu_SysOptions_Click(object sender, EventArgs e)
        {
            FrmPRESysOptions frm = new FrmPRESysOptions();
            frm.ShowDialog();
        }

        private void mnu_Company_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmPREBusinesses));
        }

        private void mnu_PreferentialInput_Click(object sender, EventArgs e)
        {
            CheckFormIsOpen("FrmPreferentialCancel");
            ShowSingleForm(typeof(FrmPreferentialCore));
        }

        private void mnu_PreferentialCancel_Click(object sender, EventArgs e)
        {
            CheckFormIsOpen("FrmPreferentialCore");
            ShowSingleForm(typeof(FrmPreferentialCancel));
        }

        private void mnu_PRERecord_Click(object sender, EventArgs e)
        {
            ShowSingleForm(typeof(FrmPreferentialReport));
        }

        /// <summary>
        /// 用户菜单权限
        /// </summary>
        /// <param name="op"></param>
        private void ShowOperatorRights(PREOperatorInfo op)
        {
            PRERoleInfo role = op.Role;
            this.mnu_System.Enabled = role.Permit(PREPermission.SystemSetting) || role.Permit(PREPermission.PreferentialCore) || role.Permit(PREPermission.PreferentialCancel);
            this.mnu_SysOptions.Enabled = role.Permit(PREPermission.SystemSetting);
            this.mnu_PreferentialInput.Enabled = role.Permit(PREPermission.PreferentialCore);
            this.mnu_PreferentialCancel.Enabled = role.Permit(PREPermission.PreferentialCancel);
            this.mnu_Data.Enabled = role.Permit(PREPermission.ReadWorkstations) || role.Permit(PREPermission.EditWorkstations) || role.Permit(PREPermission.ReadBusiness) || role.Permit(PREPermission.EditBusiness);
            this.mnu_WorkStation.Enabled = role.Permit(PREPermission.ReadWorkstations) || role.Permit(PREPermission.EditWorkstations);
            this.mnu_Company.Enabled = role.Permit(PREPermission.ReadBusiness) || role.Permit(PREPermission.EditBusiness);
            //this.mnu_SafeSetting.Enabled = role.Permit(PREPermission.OperatorManager) || role.Permit(PREPermission.RoleManager);
            this.mnu_Operator.Enabled = role.Permit(PREPermission.OperatorManager);
            this.mnu_RoleManager.Enabled = role.Permit(PREPermission.RoleManager);
            this.mnu_Report.Enabled = role.Permit(PREPermission.PreferentialReport);
            this.mnu_PRERecord.Enabled = role.Permit(PREPermission.PreferentialReport);
        }

        private void mnu_LogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnu_ChangePassword_Click(object sender, EventArgs e)
        {
            FrmPREChangePwd frm = new FrmPREChangePwd();
            frm.Operator = PREOperatorInfo.CurrentOperator;
            frm.ShowDialog();
        }

        /// <summary>
        /// 检查窗体是否已打开并关闭
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        private bool CheckFormIsOpen(string formName)
        {
            bool bResult = false;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == formName)
                {
                    frm.Close();
                    bResult = true;
                    break;
                }
            }
            return bResult;
        }

        private void mnu_About_Click(object sender, EventArgs e)
        {
            FrmAbout frm = new FrmAbout();
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.ShowDialog();
        }

        private void mnu_Doc_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(System.IO.Path.Combine(Application.StartupPath, "RALID停车场优惠系统用户手册.pdf"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
