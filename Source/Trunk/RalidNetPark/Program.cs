using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Ralid.Park.UI;
using Ralid.GeneralLibrary;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Ralid.Park.RalidParking
{
    static class Program
    {
        #region Dll调用
        [DllImport("mscomm32.ocx")]
        public static extern int DllRegisterServer();//注册时用
        [DllImport("mscomm32.ocx")]
        public static extern int DllUnregisterServer();//取消注册时用
        #endregion

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

            RegsvrDll();//注册控件

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

        static void RegsvrDll()//注册控件
        {
            RegistryKey rkTest = Registry.ClassesRoot.OpenSubKey("CLSID\\{648A5600-2C6E-101B-82B6-000000000014}\\");//查询mscomm32.ocx是否已注册
            if (rkTest == null)
            {    //Dll没有注册，在这里调用DllRegisterServer()
                int i = DllRegisterServer();//>= 0:注册成功!,<0:注册失败
            }
        }
    }
}