using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Ralid.Park.UI;
using Ralid.GeneralLibrary;

namespace Ralid.Park.RalidParking
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。

        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length >= 2)
            {
                CommandLineArgs.UserName = args[0];
                CommandLineArgs.Password = args[1];
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //加一个应用程序级的异常捕捉

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