using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UserControls
{
    public partial class UCAPMMonitor : UserControl
    {
        public UCAPMMonitor()
        {
            InitializeComponent();
        }

        public void Init()
        {
            timer1.Enabled = true;
            timer1_Tick(timer1, EventArgs.Empty);
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            APM apm=(sender as Control ).Tag as APM ;
            string tip = string.Format("{0} [{1}] {2} \n {3}", apm.SerialNum, apm.Coin,apm.Memo, Ralid.Park.BusinessModel.Resouce.APMStatusDescription.GetDescription(apm.Status));
            if (apm.Coin < 200)
            {
                tip += string.Format("\n {0}", Resources.Resource1.UCAPMMonitor_NeedAddCoin);
            }
            this.toolTip1.SetToolTip(sender as Control, tip);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            List<APM> apms = (new APMBll(AppSettings.CurrentSetting.ParkConnect)).GetAllItems().QueryObjects;
            if (apms != null && apms.Count > 0)
            {
                foreach (APM apm in apms)
                {
                    if (apm.ActiveDateTime == null || (new TimeSpan (DateTime .Now .Ticks -apm.ActiveDateTime .Value .Ticks )).TotalSeconds >30) //如果最近30没有更新活动时间,则表明APM掉线了
                    {
                        if ((apm.Status & APMStatus.ParkingConnectFault) == 0) apm.Status |= APMStatus.ParkingConnectFault;
                    }
                    PictureBox pic = GetAPMPicture(apm);
                    if (pic != null)
                    {
                        pic.Tag = apm;
                    }
                    else
                    {
                        pic = new PictureBox();
                        pic.Size = new Size(50, 50);
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pic.MouseEnter += new EventHandler(pic_MouseEnter);
                        pic.Tag = apm;
                        apmPanel.Controls.Add(pic);
                    }
                    ShowAPMPicture(pic, apm);
                }
            }
        }

        private PictureBox GetAPMPicture(APM apm)
        {
            foreach (Control ctrl in apmPanel.Controls)
            {
                if ((ctrl is PictureBox) && (ctrl.Tag as APM).SerialNum == apm.SerialNum)
                {
                    return ctrl as PictureBox;
                }
            }
            return null;
        }

        private void ShowAPMPicture(PictureBox pic, APM apm)
        {
            if (apm.Status == APMStatus.Normal && apm.Coin >= 200)
            {
                pic.Image = global::Ralid.Park.UserControls.Properties.Resources.atm;
            }
            else if ((apm.Status & APMStatus.ParkingConnectFault) == APMStatus.ParkingConnectFault ||
                    (apm.Status & APMStatus.LocalDBFault) == APMStatus.LocalDBFault ||
                    (apm.Status & APMStatus.BillValidatorFault) == APMStatus.BillValidatorFault ||
                    (apm.Status & APMStatus.CoinChangerFault) == APMStatus.CoinChangerFault ||
                    (apm.Status & APMStatus.ReaderFault) == APMStatus.ReaderFault ||
                    (apm.Status & APMStatus.CashBoxFull) == APMStatus.CashBoxFull ||
                    (apm.Status & APMStatus.LoginParkingFault) == APMStatus.LoginParkingFault)
            {
                pic.Image = global ::Ralid.Park.UserControls.Properties.Resources.atm_Disable;
            }
            else
            {
                pic.Image = global ::Ralid.Park.UserControls.Properties.Resources.atm_Alarm;
            }
        }
    }
}
