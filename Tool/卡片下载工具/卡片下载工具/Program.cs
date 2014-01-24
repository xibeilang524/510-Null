using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Ralid.GeneralLibrary;

namespace Ralid.Park.DownloadCard
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
            if (SingleInstance.OpenSingleProcess())
            {
                Application.Run(new FrmMain());
            }
            else
            {
                SingleInstance.ShowSingleProcess();
            }
        }
    }
}
