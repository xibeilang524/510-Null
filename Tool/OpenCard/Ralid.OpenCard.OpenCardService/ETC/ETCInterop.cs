using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.ETC
{
    internal class ETCInterop
    {
        #region 库函数封装
        [DllImport("EtcController")]
        public static extern int Init(StringBuilder pResult, ref int pLen); //初始化

        [DllImport("EtcController")]
        public static extern int Uninit(); // 反初始化

        [DllImport("EtcController")]
        public static extern int GetResponse(ref int pErrorCode, StringBuilder pResult, ref int pLen);

        [DllImport("EtcController")]
        public static extern int RSURead(int iLaneNo, string pReg, StringBuilder pResp, ref int pLen);//获取OBU车辆信息

        [DllImport("EtcController")]
        public static extern int RSUWrite(int iLaneNo, string pReg, StringBuilder pResp, ref int pLen); //天线写出入口信息和消费

        [DllImport("EtcController")]
        public static extern int ReaderRead(int iLaneNo, string pReg, StringBuilder pResp, ref int pLen); //读卡器获取卡片信息

        [DllImport("EtcController")]
        public static extern int ReaderWrite(int iLaneNo, string pReg, StringBuilder pResp, ref int pLen); //读卡器写出入口信息和消费

        [DllImport("EtcController")]
        public static extern int ListUpLoad(int iLaneNo, string pReg, StringBuilder pResp, ref int pLen);  //流水上传接口
        #endregion
    }
}
