using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Resouce;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.UI.Resources;


namespace Ralid.Park.UI.ReportAndStatistics
{
    public partial class FrmWaitingCommandReport : Ralid.Park.UI.ReportAndStatistics.FrmReportBase
    {
        public FrmWaitingCommandReport()
        {
            InitializeComponent();
            this.ItemSearching += ItemSearching_Handler;
        }

        private void ItemSearching_Handler(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            WaitingCommandSearchCondition con = new WaitingCommandSearchCondition();
            con.EntranceID = this.comEntrances.SelectedEntranceID;
            con.CommandType = this.comCommandType.SelectedCommandType;
            con.CardID = this.txtCardID.Text.Trim();
            con.Status = this.comWaitingCommandStatus.SelectedStatus;
            WaitingCommandBLL bll = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
            List<WaitingCommandInfo> items = bll.GetCommands(con).QueryObjects;
            ShowReportsOnGrid(items);
            this.Cursor = Cursors.Arrow;
        }


        private void ShowReportsOnGrid(List<WaitingCommandInfo> items)
        {
            this.GridView.Rows.Clear();
            foreach (WaitingCommandInfo record in items)
            {
                int index = GridView.Rows.Add();
                DataGridViewRow row = GridView.Rows[index];
                ShowReportOnRow(row, record);
            }
        }

        private void ShowReportOnRow(DataGridViewRow row, WaitingCommandInfo record)
        {
            row.Tag = record;
            EntranceInfo entrance = ParkBuffer.Current.GetEntrance(record.EntranceID);
            row.Cells["colEntrance"].Value = entrance == null ? string.Empty : entrance.EntranceName;
            row.Cells["colCommandType"].Value = CommandTypeDescription.GetDescription(record.Command);
            row.Cells["colCardID"].Value = record.CardID;
            row.Cells["colWaitingCommandStatus"].Value = WaitingCommandStatusDescription.GetDescription(record.Status);
        }

        private void ReDownload()
        {
            FrmProcessing frmP = new FrmProcessing();
            string msg = string.Empty;
            Action action = delegate()
            {
                try
                {
                    int success = 0;
                    int fail = 0;
                    WaitingCommandBLL bll = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                    foreach (DataGridViewRow item in this.GridView.SelectedRows)
                    {
                        if (item.Tag is WaitingCommandInfo)
                        {
                            WaitingCommandInfo info = item.Tag as WaitingCommandInfo;
                            BusinessModel.Enum.WaitingCommandStatus oldStatus = info.Status;
                            if (oldStatus == BusinessModel.Enum.WaitingCommandStatus.Waiting)
                            {
                                success++;
                            }
                            else
                            {
                                info.Status = BusinessModel.Enum.WaitingCommandStatus.Waiting;
                                if (bll.Update(info).Result == ResultCode.Successful)
                                {
                                    success++;
                                    ShowReportOnRow(item, info);
                                }
                                else
                                {
                                    fail++;
                                    info.Status = oldStatus;
                                }
                            }
                        }
                        msg = string.Format(Resource1.Form_Total + "：{0}/{1} " + Resource1.Form_Success + ":{2} " + Resource1.Form_Fail + ":{3}", success + fail, this.GridView.SelectedRows.Count, success, fail);
                        frmP.ShowProgress(msg, (success + fail) / this.GridView.SelectedRows.Count);
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

        private void Delete()
        {
            FrmProcessing frmP = new FrmProcessing();
            string msg = string.Empty;
                    List<DataGridViewRow> deleteRows = new List<DataGridViewRow>();
            Action action = delegate()
            {
                try
                {
                    int success = 0;
                    int fail = 0;
                    WaitingCommandBLL bll = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                    foreach (DataGridViewRow item in this.GridView.SelectedRows)
                    {
                        if (item.Tag is WaitingCommandInfo)
                        {
                            WaitingCommandInfo info = item.Tag as WaitingCommandInfo;
                            BusinessModel.Enum.WaitingCommandStatus oldStatus = info.Status;
                            info.Status = BusinessModel.Enum.WaitingCommandStatus.Waiting;
                            if (bll.Delete(info).Result == ResultCode.Successful)
                            {
                                success++;
                                deleteRows.Add(item);
                            }
                            else
                            {
                                fail++;
                            }
                        }
                        msg = string.Format(Resource1.Form_Total + "：{0}/{1} " + Resource1.Form_Success + ":{2} " + Resource1.Form_Fail + ":{3}", success + fail, this.GridView.SelectedRows.Count, success, fail);
                        frmP.ShowProgress(msg, (success + fail) / this.GridView.SelectedRows.Count);
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
                foreach (DataGridViewRow item in deleteRows)
                {
                    this.GridView.Rows.Remove(item);
                }
                MessageBox.Show(msg);
            }
        }
        

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (this.GridView.SelectedRows.Count > 0)
            {
                ReDownload();
                //int success = 0;
                //WaitingCommandBLL bll = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                //foreach (DataGridViewRow item in this.GridView.SelectedRows)
                //{
                //    if (item.Tag is WaitingCommandInfo)
                //    {
                //        WaitingCommandInfo info = item.Tag as WaitingCommandInfo;
                //        BusinessModel.Enum.WaitingCommandStatus oldStatus = info.Status;
                //        if (oldStatus == BusinessModel.Enum.WaitingCommandStatus.Waiting)
                //        {
                //            success++;
                //        }
                //        else
                //        {
                //            info.Status = BusinessModel.Enum.WaitingCommandStatus.Waiting;
                //            if (bll.Update(info).Result == ResultCode.Successful)
                //            {
                //                success++;
                //                ShowReportOnRow(item, info);
                //            }
                //            else
                //            {
                //                info.Status = oldStatus;
                //            }
                //        }
                //    }
                //}
                //MessageBox.Show(string.Format("重新下发 {0} 条命令", success));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resource1.FrmWaitingCommandReport_Delete, Resource1.Form_Query, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            if (this.GridView.SelectedRows.Count > 0)
            {
                Delete();
                //WaitingCommandBLL bll = new WaitingCommandBLL(AppSettings.CurrentSetting.ParkConnect);
                //List<DataGridViewRow> deleteRows = new List<DataGridViewRow>();
                //foreach (DataGridViewRow item in this.GridView.SelectedRows)
                //{
                //    deleteRows.Add(item);
                //}
                //foreach (DataGridViewRow item in deleteRows)
                //{
                //    if (item.Tag is WaitingCommandInfo)
                //    {
                //        WaitingCommandInfo info = item.Tag as WaitingCommandInfo;
                //        BusinessModel.Enum.WaitingCommandStatus oldStatus = info.Status;
                //        info.Status = BusinessModel.Enum.WaitingCommandStatus.Waiting;
                //        if (bll.Delete(info).Result == ResultCode.Successful)
                //        {
                //            success++;
                //            this.GridView.Rows.Remove(item);
                //        }
                //    }
                //}
                //MessageBox.Show(string.Format("删除 {0} 条命令", success));
            }
        }

        private void FrmWaitingCommandReport_Load(object sender, EventArgs e)
        {
            this.comEntrances.Init();
            this.comCommandType.Init();
            this.comWaitingCommandStatus.Init();
        }
    }
}
