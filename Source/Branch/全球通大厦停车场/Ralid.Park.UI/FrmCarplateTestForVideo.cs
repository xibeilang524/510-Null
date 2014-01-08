using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.PlateRecognition;
using Ralid.Park.UserControls.VideoPanels;
using Ralid.GeneralLibrary;

namespace Ralid.Park.UI
{
    public partial class FrmCarplateTestForVideo : Form
    {
        public FrmCarplateTestForVideo()
        {
            InitializeComponent();
        }

        #region 私有变量
        private int _Total, _RegCount;
        private System.Threading.Thread _RegT;
        #endregion

        #region 事件处理程序
        private void FrmCarplateTestForFile_Load(object sender, EventArgs e)
        {

        }

        private void ShowResult(PlateRecognitionResult ret, string file)
        {
            Action action = delegate()
            {
                _Total += 1;
                int row = this.resultGrid.Rows.Add();
                if (row >= (resultGrid.DisplayedRowCount(false) + resultGrid.FirstDisplayedScrollingRowIndex))
                {
                    resultGrid.FirstDisplayedScrollingRowIndex = resultGrid.Rows.Count - resultGrid.DisplayedRowCount(false);
                }
                this.resultGrid.Rows[row].Tag = file;
                this.resultGrid.Rows[row].Cells["colFileName"].Value = System.IO.Path.GetFileName(file);
                this.resultGrid.Rows[row].Cells["colCarPlate"].Value = ret.CarPlate;
                this.resultGrid.Rows[row].Cells["colBackColor"].Value = ret.Color;
                if (!string.IsNullOrEmpty(ret.CarPlate))
                {
                    if (!string.IsNullOrEmpty(txtCarplate.Text))
                    {
                        if (txtCarplate.Text == ret.CarPlate)
                        {
                            _RegCount += 1;
                        }
                        else
                        {
                            this.resultGrid.Rows[row].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                    }
                    else
                    {
                        _RegCount += 1;
                    }
                }
                else
                {
                    this.resultGrid.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
                }
                txtTotal.Text = _Total.ToString();
                txtRegCount.Text = _RegCount.ToString();
                txtPercent.Text = ((double)_RegCount / _Total).ToString("F2");
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.resultGrid.Rows.Clear();
            this.txtTotal.Text = "0";
            this.txtRegCount.Text = "0";
            this.txtPercent.Text = "0";
            _Total = 0;
            _RegCount = 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _RegT = new System.Threading.Thread(RegThread);
            _RegT.Start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void RegThread()
        {
            try
            {
                while (true)
                {
                    if (this.ucVideo.Status == VideoStatus.Playing)
                    {
                        string file = System.IO.Path.Combine(TempFolderManager.GetCurrentFolder(), string.Format("{0}.jpg", DateTime.Now.ToString("yyyyMMddHHmmss")));
                        if (this.ucVideo.SnapShotTo(file))
                        {
                            PlateRecognitionResult ret = PlateRecognitionService.CurrentInstance.Recognize(file);
                            ShowResult(ret, file);
                        }
                    }
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception)
            {
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            _RegT.Abort();
        }
        #endregion

        private void FrmCarplateTestForVideo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ucVideo.Status == VideoStatus.Playing)
            {
                this.ucVideo.Close();
            }
        }
    }
}
