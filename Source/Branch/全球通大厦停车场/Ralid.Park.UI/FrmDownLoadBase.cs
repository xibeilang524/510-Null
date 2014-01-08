using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.UserControls;

namespace Ralid.Park.UI
{
    public partial class FrmDownLoadBase : Form
    {
        #region 构造函数
        public FrmDownLoadBase()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private DateTime _Begin;
        private Thread Sync_Thread;//下发线程
        #endregion

        #region 保护变量
        protected bool CancelNeedWaiting = false;//停止下载时需要等待已执行的命令完成
        protected AutoResetEvent CancelWaitingEvent = new AutoResetEvent(false);//等待已执行的命令完成的通知事件
        #endregion

        #region 模板方法
        /// <summary>
        /// 初始化控件
        /// </summary>
        protected virtual void InitControls()
        {

        }
        /// <summary>
        /// 初始化输入
        /// </summary>
        protected virtual void InitInput()
        { 
        }
        /// <summary>
        /// 下发按钮处理方法
        /// </summary>
        protected virtual void btnOk_Handler()
        { 
        }
        /// <summary>
        /// 下发操作具体方法
        /// </summary>
        protected virtual bool DownLoadHandler()
        {
            throw new NotImplementedException("子类没有重写DownLoadHandler方法");
        }

        protected virtual HardwareTree GetHardwareTree()
        {
            throw new NotImplementedException("子类没有重写GetHardwareTree方法");
        }
        #endregion

        #region 私有方法
        private void Init()
        {
            this.label2.Text = string.Empty;
            this.timer1.Enabled = false;
            this.btnOk.Enabled = true;
            this.btnCancel.Enabled = false;
            this.progressBar1.Visible = false;
            this.btnCancel.Text = Resources.Resource1.Form_Cancel;
            InitInput();
        }

        private void SyncMethod()
        {
            try
            {
                bool success = true;
                success = DownLoadHandler();
                if (success)
                {
                    NotifyMessage(Resources.Resource1.FrmDownLoadBase_Finish);
                }
                else
                {
                    NotifyMessage(Resources.Resource1.FrmDownLoadBase_NotDownloadAll);
                }
                NotifyProgress(this.progressBar1.Maximum);
                CancelNeedWaiting = false;
                this.btnCancel.Text = Resources.Resource1.Form_Cancel;
            }
            catch (Exception)
            {

            }
            this.timer1.Enabled = false;
        }
        #endregion

        #region 保护方法

        protected void NotifyMessage(string msg)
        {
            Action<string> action = delegate(string s)
            {
                this.label2.Text = msg;
                if (msg == Resources.Resource1.FrmDownloadAllCards_Complete)  //已经完成
                {
                    Init();
                }
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, msg);
            }
            else
            {
                action(msg);
            }
        }

        protected void NotifyProgress(int? value)
        {
            Action<int?> action = delegate(int? v)
            {
                if (v.HasValue)
                {
                    this.progressBar1.Value = v.Value;
                }
                else
                {
                    this.progressBar1.Value++;
                }
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, value);
            }
            else
            {
                action(value);
            }
        }

        protected void NotifyHardwareTreeEntrance(int entranceID, bool success)
        {
            Action<int, bool> action = delegate(int eID, bool s)
            {
                TreeNode node = GetHardwareTree().GetEntranceNode(eID);
                if (node != null) node.ForeColor = s ? Color.Green : Color.Red;
                this.progressBar1.Value++;
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, entranceID, success);
            }
            else
            {
                action(entranceID, success);
            }
        }

        protected void NotifyHardwareTreePark(int parkID, bool success)
        {
            Action<int, bool> action = delegate(int pID, bool s)
            {
                TreeNode node = GetHardwareTree().GetParkNode(pID);
                if (node != null) node.ForeColor = s ? Color.Green : Color.Red;
            };
            if (this.InvokeRequired)
            {
                this.Invoke(action, parkID, success);
            }
            else
            {
                action(parkID, success);
            }
        }
        #endregion

        #region 窗体事件
        private void FrmDownloadBase_Load(object sender, EventArgs e)
        {
            InitControls();
            Init();
        }

        private void FrmDownloadBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Sync_Thread != null && (Sync_Thread.ThreadState == ThreadState.Running || Sync_Thread.ThreadState == ThreadState.WaitSleepJoin))
            {
                Sync_Thread.Abort();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            btnOk.Enabled = false;
            btnCancel.Enabled = true;
            this.btnCancel.Text = Resources.Resource1.Form_Stop;
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            _Begin = DateTime.Now;

            HardwareTree hardwareTree = GetHardwareTree();
            if (hardwareTree != null) this.progressBar1.Maximum = hardwareTree.GetSelectedEntrances().Count;
            else this.progressBar1.Maximum = 0;
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = true;

            btnOk_Handler();

            Sync_Thread = new Thread(SyncMethod);
            Sync_Thread.Start();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CancelNeedWaiting)
            {
                this.Cursor = Cursors.WaitCursor;
                NotifyMessage(Resources.Resource1.FrmDownLoadBase_StopDownload);
                this.btnCancel.Enabled = false;
                CancelWaitingEvent.WaitOne(15000);

                this.Cursor = Cursors.Arrow;
                this.btnCancel.Enabled = true;
            }
            if (Sync_Thread != null && (Sync_Thread.ThreadState == ThreadState.Running || Sync_Thread.ThreadState == ThreadState.WaitSleepJoin))
            {
                Sync_Thread.Abort();
            }
            Init();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - _Begin.Ticks);
            this.Text = string.Format(Resources.Resource1.FrmDownloadAllCards_Seconds, (int)ts.TotalSeconds);
        }
        #endregion
    }
}
