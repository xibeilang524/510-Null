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
        public static extern int Initialize(StringBuilder pResult, ref int pLaneNum, StringBuilder pErrMsg); //初始化

        [DllImport("EtcController")]
        public static extern int Uninstall(); // 反初始化

        [DllImport("EtcController")]
        public static extern int connectserver(int iLaneNo, string ip, int port); //连接嵌入式控制板

        [DllImport("EtcController")]
        public static extern int quitserver(int iLaneNo);  // 断开与控制板的连接

        [DllImport("EtcController")]
        public static extern int LaneLogin(int iLaneNo, string pReq, StringBuilder pResp); //登录控制板

        [DllImport("EtcController")]
        public static extern int LaneQuit(int iLaneNo, string pReq, StringBuilder pResp); //退出登录控制板

        [DllImport("EtcController")]
        public static extern int HeartBeat(int iLaneNo); //向指定通道发送心跳包

        [DllImport("EtcController")]
        public static extern int RSUOpen(int iLaneNo, string pReq, StringBuilder pResp); //打开天线

        [DllImport("EtcController")]
        public static extern int RSUClose(int iLaneNo, string pReq, StringBuilder pResp); //关闭天线

        [DllImport("EtcController")]
        public static extern int OBUSearch(int iLaneNo, string pReq, StringBuilder pResp); //搜索OBU

        [DllImport("EtcController")]
        public static extern int GetCardNo(int iLaneNo, string pReg, StringBuilder pResp);  //天线获取卡号

        [DllImport("EtcController")]
        public static extern int GetOBUInfo(int iLaneNo, string pReg, StringBuilder pResp); //获取OBU车辆信息

        [DllImport("EtcController")]
        public static extern int GetOBUInfo_GD(int iLaneNo, string pReg, StringBuilder pResp); ////获取OBU车辆信息_广东

        [DllImport("EtcController")]
        public static extern int RSUWriteCard(int iLaneNo, string pReg, StringBuilder pResp); //天线写出入口信息和消费

        [DllImport("EtcController")]
        public static extern int RSUWriteCard_GD(int iLaneNo, string pReg, StringBuilder pResp); //天线写出入口信息和消费接口(广东)

        //本接口特制获取消费交易认证，用于重新获取MAC2和TAC码。一般是当卡片余额已经发生变化且没有取到交易的MAC2和TAC码 等异常交易才需调用该接口。
        [DllImport("EtcController")]
        public static extern int RSUTransActionProve(int iLaneNo, string pReg, StringBuilder pResp); //天线交易认证接口

        [DllImport("EtcController")]
        public static extern int CardReaderOpen(int iLaneNo, string pReg, StringBuilder pResp); //打开读卡器

        [DllImport("EtcController")]
        public static extern int CardReaderClose(int iLaneNo, string pReg, StringBuilder pResp); //关闭读卡器

        [DllImport("EtcController")]
        public static extern int CardSearch(int iLaneNo, string pReg, StringBuilder pResp); //读卡器寻卡

        [DllImport("EtcController")]
        public static extern int GetCardInfo(int iLaneNo, string pReg, StringBuilder pResp); //读卡器获取卡片信息

        [DllImport("EtcController")]
        public static extern int GetCardInfo_GD(int iLaneNo, string pReg, StringBuilder pResp); //读卡器获取卡片信息_广东

        [DllImport("EtcController")]
        public static extern int CardReaderWriteCard(int iLaneNo, string pReg, StringBuilder pResp); //读卡器写出入口信息和消费

        [DllImport("EtcController")]
        public static extern int CardReaderWriteCard_GD(int iLaneNo, string pReg, StringBuilder pResp); //读卡器写出入口信息和消费接口(广东)

        [DllImport("EtcController")]
        public static extern int CardReaderTransActionProve(int iLaneNo, string pReg, StringBuilder pResp); //读卡器交易认证接口

        [DllImport("EtcController")]
        public static extern int ListUpLoad(int iLaneNo, string pReg, StringBuilder pResp);  //流水上传接口

        [DllImport("EtcController")]
        public static extern int ListQuery(int iLaneNo, string pReg, StringBuilder pResp); //流水查询接口

        [DllImport("EtcController")]
        public static extern int SetParameter(int iLaneNo, string pReg, StringBuilder pResp); //参数设置

        [DllImport("EtcController")]
        public static extern int BlackListQuery(int iLaneNo, string pReg, StringBuilder pResp); //黑名单查询

        [DllImport("EtcController")]
        public static extern int StatusQuery(int iLanNo, string pReg, StringBuilder pResp); //设备状态查询
        #endregion
    }
}
