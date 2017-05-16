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
        /// 业务发起
        /// </summary>
        /// <param name="req"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [DllImport("MisProtocol.dll")]
        public static extern int bankall_back(byte[] req, byte[] rep, callbackfunc cab);

        [DllImport("MisProtocol.dll")]
        public static extern int bankall(byte[] req, byte[] rep);
    }
}
