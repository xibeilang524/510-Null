using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Ralid.Park.POS
{
    internal class RAPI
    {
        #region 私有变量
        private const uint GENERIC_WRITE = 0x40000000; 	// 设置读写权限
        private const uint GENERIC_READ = 0x80000000;   //设置读权限
        private const short CREATE_NEW = 1; 			// 创建新文件
        private const short CREATE_ALWAYS = 2;          // 创建新文件,如果文件已经存在,则清空
        private const short OPEN_EXISTING = 3;          // 
        private const short FILE_ATTRIBUTE_NORMAL = 0x80; 	// 设置文件属性
        private const short INVALID_HANDLE_VALUE = -1; 	// 错误句柄

        #endregion

        #region 动态库声明
        [DllImport("rapi.dll", CharSet = CharSet.Unicode)]
        internal static extern int CeRapiGetError();

        [DllImport("rapi.dll", CharSet = CharSet.Unicode)]
        internal static extern int CeRapiInit();

        // 声明要引用的API
        [DllImport("rapi.dll", CharSet = CharSet.Unicode)]
        internal static extern int CeCloseHandle(IntPtr hObject);

        // 声明要引用的API
        [DllImport("rapi.dll", CharSet = CharSet.Unicode)]
        internal static extern int CeReadFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, int lpOverlappe);

        [DllImport("rapi.dll", CharSet = CharSet.Unicode)]
        internal static extern int CeWriteFile(IntPtr hFile, byte[] lpBuffer, int nNumberOfbytesToWrite, ref int lpNumberOfbytesWritten, int lpOverlapped);

        [DllImport("rapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr CeCreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            int dwShareMode,
            int lpSecurityAttributes,
            int dwCreationDisposition,
            int dwFlagsAndAttributes,
            int hTemplateFile);



        // 声明要引用的API
        [DllImport("rapi.dll", CharSet = CharSet.Unicode)]
        internal static extern int CeRapiUninit();
        #endregion

        #region 公共方法
        /// <summary>
        /// 复制文件到pc
        /// </summary>
        /// <param name="localFileName"></param>
        /// <param name="remoteFileName"></param>
        /// <returns></returns>
        public static bool CopyFileToLocal(string localFileName, string remoteFileName)
        {
            IntPtr remoteFile = IntPtr.Zero;
            byte[] buffer = new byte[0x1000]; 			// 传输缓冲区定义为4k
            FileStream localFile;
            int bytesread = 0;
            //初始化
            int ret = RAPI.CeRapiInit();
            if (ret != 0) throw new Exception("连接设备失败,错误代码:" + RAPI.CeRapiGetError().ToString());
            // 创建远程文件
            remoteFile = RAPI.CeCreateFile(remoteFileName, GENERIC_READ, 0, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, 0);

            // 检查文件是否创建成功
            if ((int)remoteFile == INVALID_HANDLE_VALUE) throw new Exception("未能创建文件句柄,错误代码:" + RAPI.CeRapiGetError().ToString());

            // 打开本地文件
            localFile = new FileStream(localFileName, FileMode.Create, FileAccess.Write);
            while (Convert.ToBoolean(RAPI.CeReadFile(remoteFile, buffer, buffer.Length, ref bytesread, 0)) && bytesread > 0)
            {
                localFile.Write(buffer, 0, bytesread);
            }

            // 关闭本地文件
            localFile.Close();

            // 关闭远程文件
            RAPI.CeCloseHandle(remoteFile);
            RAPI.CeRapiUninit();
            return true;
        }

        /// <summary>
        /// 复制文件到手持机
        /// </summary>
        /// <param name="localFileName"></param>
        /// <param name="remoteFileName"></param>
        /// <returns></returns>
        public static bool CopyFileToRemote(string localFileName, string remoteFileName)
        {
            IntPtr remoteFile = IntPtr.Zero;
            byte[] buffer = new byte[0x1000]; 			// 传输缓冲区定义为4k
            FileStream localFile;
            int bytesread = 0;
            int byteswritten = 0;

            //初始化
            int ret = RAPI.CeRapiInit();
            if (ret != 0) throw new Exception("连接设备失败,错误代码:" + RAPI.CeRapiGetError().ToString());

            // 创建远程文件
            remoteFile = RAPI.CeCreateFile(remoteFileName, GENERIC_WRITE, 0, 0, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);

            // 检查文件是否创建成功
            if ((int)remoteFile == INVALID_HANDLE_VALUE) throw new Exception("未能创建文件句柄,错误代码:" + RAPI.CeRapiGetError().ToString());

            // 打开本地文件
            localFile = new FileStream(localFileName, FileMode.Open);

            // 读取4K字节
            bytesread = localFile.Read(buffer, 0, buffer.Length);
            while (bytesread > 0)
            {
                //// 写缓冲区数据到远程设备文件
                if (!Convert.ToBoolean(RAPI.CeWriteFile(remoteFile, buffer, bytesread, ref byteswritten, 0)))
                { // 检查是否成功，不成功关闭文件句柄，抛出异常
                    RAPI.CeCloseHandle(remoteFile);
                    throw new Exception("文件写入失败,错误代码:" + RAPI.CeRapiGetError().ToString());
                }
                //连接读取后续内容
                bytesread = localFile.Read(buffer, 0, buffer.Length);
            }
            // 关闭本地文件
            localFile.Close();
            // 关闭远程文件
            RAPI.CeCloseHandle(remoteFile);
            RAPI.CeRapiUninit();
            return true;
        }
        #endregion
    }
}
