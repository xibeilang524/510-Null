using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OfflineCardPayingTool
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
            if (Ralid.GeneralLibrary.SingleInstance.OpenSingleProcess())
            {
                Form frm = new FrmOfflineCardPaying();
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
