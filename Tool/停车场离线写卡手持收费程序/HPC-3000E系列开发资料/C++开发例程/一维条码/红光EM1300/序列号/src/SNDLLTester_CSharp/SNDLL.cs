using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SNDLL_CSharp
{
    enum RET_VALUE
    {
        GETSNSUCCESS,
        SYSTEMNOTSUPPORT,
        OPENDRIVEFAILED,
        GETSNFAILED
    }
    class SNDLL
    {
        [System.Runtime.InteropServices.DllImport("SNDll.dll")]

        /*********************************************************************************************************
        ** Function name:           GetSerialNumber
        ** Descriptions:            获取手持机序列号                         
        ** input parameters:        pSN		接收序列号的指针
        ** output parameters:       NONE
        ** 
        ** Returned value:          GETSNSUCCESS		获取成功
        **							SYSTEMNOTSUPPORT	系统不支持
        **							OPENDRIVEFAILED		打开驱动失败
        **							GETSNFAILED			获取序列号失败
        *********************************************************************************************************/
        public static extern int GetSerialNumber(ref ulong pSN);
    }
}