using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Interface;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.UserControls.VideoPanels;
namespace Ralid.Park.UI
{
    public partial class FrmMonitor : Form, IReportHandler
    {
        public FrmMonitor()
        {
            InitializeComponent();
        }

        #region ICardEventHandler 成员
        public void ProcessReport(ReportBase report)
        {

        }
        #endregion

        #region 事件处理程序
        private void FrmMonitor_Load(object sender, EventArgs e)
        {
            int rows = 2;
            int columns = 2;
            int videoCount = 0;
            if (AppSettings.CurrentSetting.OpenLastOpenedVideo)
            {
                try
                {
                    string filePath = Application.StartupPath + @"\OpenedVideoes.xml";
                    if (File.Exists(filePath))
                    {
                        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            XmlSerializer xs = new XmlSerializer(typeof(List<VideoSourceInfo>));
                            List<VideoSourceInfo> openVideos = (List<VideoSourceInfo>)xs.Deserialize(fs);
                            videoCount = openVideos.Count;
                            while (videoCount > rows * columns)
                            {
                                rows++;
                                columns++;
                            }
                            this.ucMonitor.SetShowMode(rows, columns);
                            this.ucMonitor.RenderAndPlayVideoes(openVideos);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
            }
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            this.ucMonitor.SetShowMode(2, 2);
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            this.ucMonitor.SetShowMode(3, 3);
        }

        private void btnSixteen_Click(object sender, EventArgs e)
        {
            this.ucMonitor.SetShowMode(4, 4);
        }
        
        private void FrmMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show(Resources.Resource1.Form_ExitQuery, Resources.Resource1.Form_Query, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.OK)
            //{
            //    e.Cancel = true;
            //}
        }

        private void FrmMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                string filePath = Application.StartupPath + @"\OpenedVideoes.xml";
                List<VideoSourceInfo> openedVideoes = new List<VideoSourceInfo>();
                foreach (VideoPanel vp in this.ucMonitor.VideoPanelCollection)
                {
                    if (vp.Status == VideoStatus.Playing)
                    {
                        openedVideoes.Add(vp.VideoSource);
                    }
                }
                Type t = openedVideoes.GetType();
                XmlSerializer xs = new XmlSerializer(t);
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    xs.Serialize(stream, openedVideoes);
                }
                this.ucMonitor.Clear();
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion
    }
}
