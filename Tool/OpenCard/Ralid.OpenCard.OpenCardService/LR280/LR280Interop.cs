using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Ralid.OpenCard.OpenCardService.LR280
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    internal delegate void callbackfunc(byte ret, IntPtr retstr);

    internal class LR280Interop
    {
        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="commport">串口号</param>
        /// <param name="bps">波特率</param>
        /// <returns></returns>
        [DllImport("mis_yt.dll")]
        public static extern int open_dev(int commport, int bps); 
        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <param name="commport"></param>
        /// <returns></returns>
        [DllImport("mis_yt.dll")]
        public static extern int close_dev(int commport); //
        /// <summary>
        /// 发起业务
        /// </summary>
        /// <param name="commport"></param>
        /// <param name="req"></param>
        /// <param name="rep"></param>
        /// <returns></returns>
        [DllImport("mis_yt.dll")]
        public static extern int bankall_yt(int commport, byte[] req, byte[] rep); //
    }
}
