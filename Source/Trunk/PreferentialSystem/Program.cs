using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PreferentialSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmMain());

            //当前应用程序只允许启动一个实例

            if (Ralid.GeneralLibrary.SingleInstance.OpenSingleProcess())
            {
                Form frm = new FrmMain();
                frm.FormClosed += new FormClosedEventHandler(frm_FormClosed);
                Application.Run(frm);
            }
            else
            {
                Ralid.GeneralLibrary.SingleInstance.ShowSingleProcess();
            }

        }

        static void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Ralid.GeneralLibrary.SingleInstance.CloseSingleProcess();
        }
    }
}
