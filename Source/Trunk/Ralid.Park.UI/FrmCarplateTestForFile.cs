
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Ralid.Park.PlateRecognition;

namespace Ralid.Park.UI
{
    public partial class FrmCarplateTestForFile : Form
    {
        public FrmCarplateTestForFile()
        {
            InitializeComponent();
        }

        #region 私有变量
        private readonly string _Slice = Path.Combine(Application.StartupPath, "Sliced");
        private List<string> _Files = new List<string>();
        private int _CurFileIndex;
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
                this.picImage.Image = System.Drawing.Image.FromFile(file);
                _Total += 1;
                int row = this.resultGrid.Rows.Add();
                if (row >= (resultGrid.DisplayedRowCount(false) + resultGrid.FirstDisplayedScrollingRowIndex))
                {
                    resultGrid.FirstDisplayedScrollingRowIndex = resultGrid.Rows.Count - resultGrid.DisplayedRowCount(false);
                }
                this.resultGrid.Rows[row].Tag = file;
                this.resultGrid.Rows[row].Cells["colFileName"].Value = Path.GetFileName(file);
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Files.Count > 0 && _CurFileIndex < _Files.Count - 1)
            {
                _CurFileIndex++;
                this.picImage.Image = System.Drawing.Image.FromFile(_Files[_CurFileIndex]);
                PlateRecognitionResult ret = PlateRecognitionService.CurrentInstance.Recognize(_Files[_CurFileIndex]);
                ShowResult(ret, _Files[_CurFileIndex]);
                btnNext.Enabled = (_CurFileIndex < _Files.Count - 1);
            }
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1 .ShowDialog() == DialogResult.OK)
            {
                string file = this.openFileDialog1.FileName;
                this.txtDir.Text = Path.GetDirectoryName(file);
                _Files.Clear();
                _Files.AddRange(Directory.GetFiles(this.txtDir.Text, "*.jpg"));
                if (_Files.Count > 0)
                {
                    _CurFileIndex = -1;
                    btnNext.Enabled = true;
                    btnStart.Enabled = true;
                }
                else
                {
                    btnNext.Enabled = false;
                    btnStart.Enabled = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
            if (_Files.Count > 0)
            {
                _RegT = new System.Threading.Thread(RegThread);
                _RegT.Start();
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
        }

        private void RegThread()
        {
            try
            {
                foreach (string file in _Files)
                {
                    PlateRecognitionResult ret=  PlateRecognitionService .CurrentInstance .Recognize (file);
                    ShowResult(ret, file);
                }

                Action action = delegate()
                {
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
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

        private void resultGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.picImage.Image = System.Drawing.Image.FromFile(this.resultGrid.Rows[e.RowIndex].Tag.ToString());
            }
        }

        private void mnu_SaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog open = new SaveFileDialog();
            open.FileName = this.picImage.Tag.ToString();
            if (open.ShowDialog() == DialogResult.OK)
            {
                this.picImage.Image.Save(open.FileName);
            }
        }

        private void mnu_Delete_Click(object sender, EventArgs e)
        {
            if (this.resultGrid.SelectedRows.Count > 0)
            {
                if (MessageBox.Show(Resources.Resource1.FrmMasterBase_DeleteQuery, Resources.Resource1.Form_Alert, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in resultGrid.SelectedRows)
                    {
                        string path = row.Tag.ToString();
                        this.picImage.Image = null;
                        File.Delete(path);
                        resultGrid.Rows.Remove(row);
                    }
                }
            }
        }
        #endregion
    }
}
