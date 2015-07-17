using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.GeneralLibrary.WinformControl;

namespace Ralid.Park.UI
{
    public partial class FrmOperatorSettle : Form
    {
        public FrmOperatorSettle()
        {
            InitializeComponent();
        }

        #region 公共属性
        /// <summary>
        /// 获取或设置结帐操作员
        /// </summary>
        public OperatorInfo Operator { get; set; }
        /// <summary>
        /// 获取或设置结算操作员在哪个工作站上的操作记录，如果未指定，则表示结算全部工作站上的操作记录
        /// </summary>
        public WorkStationInfo Station { get; set; }
        /// <summary>
        /// 获取或设置操作员实际上交的现金数额
        /// </summary>
        public decimal? HandInCash { get; set; }
        /// <summary>
        /// 获取或设置操作员实际上交的POS收费金额
        /// </summary>
        public decimal? HandInPOS { get; set; }
        /// <summary>
        /// 获取结算成功后的结算记录
        /// </summary>
        public OperatorSettleLog SettledLog { get { return _OperatorLog; } }
        /// <summary>
        /// 获取或设置用于收费的操作员卡
        /// </summary>
        public CardInfo OperatorCard { get; set; }

        public void PrintOperatorSettleLog(OperatorSettleLog settleLog)
        {
            InitControls();
            ShowLogInfo(settleLog, 1);
            btnPrint_Click(this.btnPrint, EventArgs.Empty);
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        #region 私有方法,属性
        private OperatorSettleLog _OperatorLog;

        private void ShowLogInfo(OperatorSettleLog log, int fromWhichHandle)
        {
            this.lblOperator.Text = log.OperatorID;

            //最后一次结算时间
            DateTime? lastSettleDateTime = (new OperatorSettleBLL(AppSettings.CurrentSetting.ParkConnect)).GetLastSettleDateTime(log.OperatorID);
            //当前结算时间的前一笔结算时间
            DateTime? preSettleDateTime = (new OperatorSettleBLL(AppSettings.CurrentSetting.ParkConnect)).GetPreSettleDateTime(log.OperatorID, log.SettleDateTime);

            this.lblSettleDateTime.Text = log.SettleDateTime.ToString("yyyy-MM-dd HH;mm:ss");

            if (lastSettleDateTime != null && fromWhichHandle == 0)
                this.lblSettleDateTime.Text = lastSettleDateTime.Value.ToString("yyyy-MM-dd HH;mm:ss") + "至" + log.SettleDateTime.ToString("yyyy-MM-dd HH;mm:ss");
            if (preSettleDateTime != null && fromWhichHandle == 1)//打印时
                this.lblSettleDateTime.Text = preSettleDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "至" + log.SettleDateTime.ToString("yyyy-MM-dd HH;mm:ss");

            this.txtCashOfPark.Text = log.CashParkFact.ToString("F2");
            this.txtCashOfOperatorCard.Text = log.CashOperatorCard.ToString("F2");
            this.txtCashDiscount.Text = log.CashParkDiscount.ToString("F2");
            this.txtCashOfCard.Text = log.CashOfCard.ToString("F2"); ;
            this.txtCashOfDeposit.Text = log.CashOfDeposit.ToString("F2");
            this.txtCashOfCardLost.Text = log.CashOfCardLost.ToString("F2");
            this.txtCashOfPOS.Text = (log.CashOfPOS != null ? log.CashOfPOS.Value : 0).ToString("F2");
            this.txtCashOfCardRecycle.Text = log.CashOfCardRecycle.ToString("F2");
            this.txtCashOfRefund.Text = log.CashOfRefund == null ? "0.00" : log.CashOfRefund.Value.ToString("F2");
            this.txtCashTotal.Text = log.TotalCash.ToString("F2");
            this.txtHandInCash.Text = log.HandInCash == null ? string.Empty : log.HandInCash.Value.ToString("F2");
            this.txtCashDiffrence.Text = log.CashDiffrence == null ? string.Empty : log.CashDiffrence.Value.ToString("F2");

            this.txtNonCashOfPark.Text = log.NonCashParkFact.ToString("F2"); ;
            this.txtNonCashDiscount.Text = log.NonCashParkDiscount.ToString("F2");
            this.txtNonCashOfCard.Text = log.NonCashOfCard.ToString("F2"); ;
            this.txtNonCashOfDeposit.Text = log.NonCashOfDeposit.ToString("F2"); ;
            this.txtNonCashOfCardLost.Text = log.NonCashOfCardLost.ToString("F2");
            this.txtNonCashTotal.Text = log.TotalNonCash.ToString("F2"); ;
            this.txtHandInPOS.Text = log.HandInPOS == null ? string.Empty : log.HandInPOS.Value.ToString("F2");

            this.txtTempCardRecycle.Text = log.TempCardRecycle.ToString();
            this.txtOpenDoorCount.Text = log.OpenDoorCount.ToString();
        }

        private void InitControls()
        {
            bool notShowDetail = UserSetting.Current != null && UserSetting.Current.NotShowSettleDetail;

            if (notShowDetail)
            {
                this.txtCashDiffrence.Visible = false;
                this.txtCashDiffrence.Tag = false;//这里将控件的Tag设为false，是为了打印时判断是否需要打印，因为使用PrintOperatorSettleLog函数时，全部控件的Visible为false，不能作为判断条件
                this.txtCashDiscount.Visible = false;
                this.txtCashDiscount.Tag = false;
                this.txtCashOfCard.Visible = false;
                this.txtCashOfCard.Tag = false;
                this.txtCashOfCardLost.Visible = false;
                this.txtCashOfCardLost.Tag = false;
                this.txtCashOfCardRecycle.Visible = false;
                this.txtCashOfCardRecycle.Tag = false;
                this.txtCashOfDeposit.Visible = false;
                this.txtCashOfDeposit.Tag = false;
                this.txtCashOfOperatorCard.Visible = false;
                this.txtCashOfOperatorCard.Tag = false;
                this.txtCashOfPark.Visible = false;
                this.txtCashOfPark.Tag = false;
                this.txtCashOfPOS.Visible = false;
                this.txtCashOfPOS.Tag = false;
                this.txtCashOfRefund.Visible = false;
                this.txtCashOfRefund.Tag = false;
                this.txtCashTotal.Visible = false;
                this.txtCashTotal.Tag = false;
                this.txtNonCashDiscount.Visible = false;
                this.txtNonCashDiscount.Tag = false;
                this.txtNonCashOfCard.Visible = false;
                this.txtNonCashOfCard.Tag = false;
                this.txtNonCashOfCardLost.Visible = false;
                this.txtNonCashOfCardLost.Tag = false;
                this.txtNonCashOfDeposit.Visible = false;
                this.txtNonCashOfDeposit.Tag = false;
                this.txtNonCashOfPark.Visible = false;
                this.txtNonCashOfPark.Tag = false;
                this.txtNonCashTotal.Visible = false;
                this.txtNonCashTotal.Tag = false;
                this.txtOpenDoorCount.Visible = false;
                this.txtOpenDoorCount.Tag = false;
                this.txtTempCardRecycle.Visible = false;
                this.txtTempCardRecycle.Tag = false;
            }
        }
        #endregion

        private void FrmOperatorShift_Load(object sender, EventArgs e)
        {
            InitControls();
            if (Operator != null)
            {
                string temp = AppSettings.CurrentSetting.GetConfigContent("AutoPrintSettleInfo");
                this.chkAutoPrint.Checked = temp == "True";

                _OperatorLog = (new OperatorSettleBLL(AppSettings.CurrentSetting.ParkConnect)).CreateOperatorLog(Operator, Station);
                _OperatorLog.HandInCash = HandInCash;
                _OperatorLog.HandInPOS = HandInPOS;
                if (OperatorCard != null)
                {
                    _OperatorLog.CashOperatorCard = OperatorCard.ParkFee;
                }
                ShowLogInfo(_OperatorLog, 0);

                //如果设置了结算时需输入上交金额，自动保存结算记录，成功后把结算按钮的文字改成“确定",
                //并在用户点击确认时只是打印结算单，而不用再保存结算记录
                if (UserSetting.Current.InputHandInCashWhenSettle)  
                {
                    OperatorSettleBLL bllOperatorLog = new OperatorSettleBLL(AppSettings.CurrentSetting.ParkConnect);
                    CommandResult ret = bllOperatorLog.Settle(_OperatorLog);
                    if (ret.Result == ResultCode.Successful)
                    {
                        butOK.Text = Resources.Resource1.FrmOperatorSettle_OK;
                    }
                }
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (butOK.Text == Resources.Resource1.FrmOperatorSettle_OK)
            {
                if (chkAutoPrint.Checked) btnPrint_Click(this.btnPrint, EventArgs.Empty);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                if (_OperatorLog != null)
                {
                    OperatorSettleBLL bllOperatorLog = new OperatorSettleBLL(AppSettings.CurrentSetting.ParkConnect);
                    CommandResult ret = bllOperatorLog.Settle(_OperatorLog);
                    if (ret.Result == ResultCode.Successful)
                    {
                        //写卡模式时，需要将操作员卡的累计停车费用清除
                        if (OperatorCard != null)
                        {
                            OperatorCard.ParkFee = 0;
                            CardOperationManager.Instance.WriteCardLoop(OperatorCard);
                        }
                        if (chkAutoPrint.Checked) btnPrint_Click(this.btnPrint, EventArgs.Empty);
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show(ret.Message);
                    }
                }
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //这里面的一些常量是根据当时调整打印位置时设置的。
            float x = 50;
            float y = 100;
            float buttonPosition = 0;

            Font headFont = new Font("宋体", 30, FontStyle.Bold, GraphicsUnit.Pixel);
            e.Graphics.DrawString(this.Text, headFont, Brushes.Black, new PointF(250, y));
            SizeF sf = e.Graphics.MeasureString(this.Text, headFont);

            y += sf.Height + 50;  //打完标题后向下偏移
            foreach (Control control in this.Controls)
            {
                if (control is Label || control is DecimalTextBox || control is IntergerTextBox || control is GroupBox)
                {
                    if (control.Tag == null)//控件的Tag没有赋值false的可打印
                    {
                        e.Graphics.DrawString(control.Text, control.Font, Brushes.Black, new PointF(control.Location.X + x, control.Location.Y + y));
                    }
                    if (control is GroupBox)
                    {
                        GroupBox gp = control as GroupBox;
                        foreach (Control ctrl in gp.Controls)
                        {
                            if (ctrl.Tag == null)//控件的Tag没有赋值false的可打印
                            {
                                e.Graphics.DrawString(ctrl.Text, ctrl.Font, Brushes.Black, new PointF(ctrl.Location.X + x + gp.Location.X, ctrl.Location.Y + y + gp.Location.Y));
                            }
                            if (buttonPosition <= ctrl.Location.Y + y + gp.Location.Y) buttonPosition = ctrl.Location.Y + y + gp.Location.Y;  //保证竖直方向在打印完所有控件后坐标位于最下面
                        }
                    }
                }
            }

            Font tailFont = new Font("宋体", 15, FontStyle.Bold, GraphicsUnit.Pixel);
            e.Graphics.DrawString(Resources.Resource1.FrmOperatorSettle_Tail, tailFont, Brushes.Black, new PointF(x + 50, buttonPosition + 100));
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                this.printDocument1.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkAutoPrint_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.CurrentSetting.SaveConfig("AutoPrintSettleInfo", chkAutoPrint.Checked ? "True" : "False");
        }

        private void FrmOperatorSettle_Activated(object sender, EventArgs e)
        {
            if (butOK.Text == Resources.Resource1.FrmOperatorSettle_OK)
            {
                //写卡模式时，需要将操作员卡的累计停车费用清除
                if (OperatorCard != null)
                {
                    OperatorCard.ParkFee = 0;
                    CardOperationManager.Instance.WriteCardLoop(OperatorCard);
                }
            }
        }

    }
}
