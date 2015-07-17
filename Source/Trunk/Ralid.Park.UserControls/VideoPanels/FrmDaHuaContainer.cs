using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyNet.CCTV.Control.Implement.DaHua;

namespace Ralid.Park.UserControls.VideoPanels
{
    /// <summary>
    /// 表示一个大华视频控件管理器
    /// </summary>
    public partial class FrmDaHuaContainer : Form
    {
        #region 摄像机属性相关
        /// <summary>
        /// SDK在线状态
        /// </summary>
        public enum devState
        {
            /// <summary>
            /// 设备在线
            /// </summary>
            Online,

            /// <summary>
            /// 设备离线
            /// </summary>
            offline,
        }

        /// <summary>
        /// 消息事件订阅状态
        /// </summary>
        public enum subEventState
        {
            /// <summary>
            /// 已订阅
            /// </summary>
            subed,

            /// <summary>
            /// 未订阅状态
            /// </summary>
            noSubecribe,

            /// <summary>
            /// 等待订阅状态
            /// </summary>
            waitSubecribe
        }

        /// <summary>
        /// 相机信息
        /// </summary>
        public class camera
        {
            /// <summary>
            /// 相机ID
            /// </summary>
            public int ID;//相机ID
            /// <summary>
            /// 用户名
            /// </summary>
            public string UserName;//用户名
            /// <summary>
            /// 密码
            /// </summary>
            public string Password;//密码
            /// <summary>
            /// IP地址
            /// </summary>
            public string IpAddress; // IP地址
            /// <summary>
            /// 端口
            /// </summary>
            public int port;         //端口
            /// <summary>
            /// 登录句柄
            /// </summary>
            public int LoginID;      // 登录句柄
            /// <summary>
            /// 事件订阅分析句柄
            /// </summary>
            public int lAnalyse;     //事件订阅分析句柄
            /// <summary>
            /// 设备在线状态
            /// </summary>
            public devState devstate; //设备在线状态
            /// <summary>
            /// 消息订阅状态
            /// </summary>
            public subEventState subState; //消息订阅状态
        }
        #endregion

        #region 摄像机事件相关
        // 回调事件
        public class EventMsg
        {
            public int lAnaly;//分析句柄参数
            public int dwAlarmType;// 消息类型
            public object MsgObj;  // 消息事件

            public byte[] jpgbuffer;    //jpg图片

            /// <summary>
            /// 事件名称等信息
            /// </summary>
            /// <returns></returns>
            public string GetMsgString()
            {
                string str = "未知事件";

                if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_RUNREDLIGHT)
                {
                    DEV_EVENT_TRAFFIC_RUNREDLIGHT_INFO Info = new DEV_EVENT_TRAFFIC_RUNREDLIGHT_INFO();
                    Info = (DEV_EVENT_TRAFFIC_RUNREDLIGHT_INFO)MsgObj;
                    str = "交通违章-闯红灯事件";
                }
                else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFICJUNCTION)
                {
                    DEV_EVENT_TRAFFICJUNCTION_INFO Info = new DEV_EVENT_TRAFFICJUNCTION_INFO();
                    Info = (DEV_EVENT_TRAFFICJUNCTION_INFO)MsgObj;

                    str = "交通路口事件";
                }
                else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_TURNLEFT)
                {
                    DEV_EVENT_TRAFFIC_TURNLEFT_INFO Info = new DEV_EVENT_TRAFFIC_TURNLEFT_INFO();
                    Info = (DEV_EVENT_TRAFFIC_TURNLEFT_INFO)MsgObj;

                    str = "交通违章-违章左转";
                }
                else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_TURNRIGHT)
                {
                    DEV_EVENT_TRAFFIC_TURNRIGHT_INFO Info = new DEV_EVENT_TRAFFIC_TURNRIGHT_INFO();
                    Info = (DEV_EVENT_TRAFFIC_TURNRIGHT_INFO)MsgObj;
                    str = "交通违章-违章右转";
                }
                else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_OVERSPEED)
                {
                    DEV_EVENT_TRAFFIC_OVERSPEED_INFO Info = new DEV_EVENT_TRAFFIC_OVERSPEED_INFO();
                    Info = (DEV_EVENT_TRAFFIC_OVERSPEED_INFO)MsgObj;
                    str = "交通违章-超速";
                }
                else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_UNDERSPEED)
                {
                    DEV_EVENT_TRAFFIC_UNDERSPEED_INFO Info = new DEV_EVENT_TRAFFIC_UNDERSPEED_INFO();
                    Info = (DEV_EVENT_TRAFFIC_UNDERSPEED_INFO)MsgObj;
                    str = "交通违章-低速";
                }
                else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFICGATE)
                {
                    DEV_EVENT_TRAFFICGATE_INFO Info = new DEV_EVENT_TRAFFICGATE_INFO();
                    Info = (DEV_EVENT_TRAFFICGATE_INFO)MsgObj;
                    str = "交通卡口事件";
                }
                else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_MANUALSNAP)
                {
                    DEV_EVENT_TRAFFIC_MANUALSNAP_INFO Info = new DEV_EVENT_TRAFFIC_MANUALSNAP_INFO();
                    Info = (DEV_EVENT_TRAFFIC_MANUALSNAP_INFO)MsgObj;
                    str = "交通手动抓拍事件";
                }
                else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_PARKINGSPACEPARKING)
                {
                    DEV_EVENT_TRAFFIC_PARKINGSPACEPARKING_INFO Info = new DEV_EVENT_TRAFFIC_PARKINGSPACEPARKING_INFO();
                    Info = (DEV_EVENT_TRAFFIC_PARKINGSPACEPARKING_INFO)MsgObj;
                    str = "车位有车事件 车位号" + Info.nLane.ToString() + "有车";
                }
                else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_PARKINGSPACENOPARKING)
                {
                    DEV_EVENT_TRAFFIC_PARKINGSPACENOPARKING_INFO Info = new DEV_EVENT_TRAFFIC_PARKINGSPACENOPARKING_INFO();
                    Info = (DEV_EVENT_TRAFFIC_PARKINGSPACENOPARKING_INFO)MsgObj;
                    str = "车位无车事件 车位号" + Info.nLane.ToString() + "无车";
                }
                else
                {
                    str = str + "未知事件 dwAlarmType = " + dwAlarmType.ToString();
                }

                return str;
            }
        }
        #endregion

        #region 构造函数
        public FrmDaHuaContainer()
        {
            InitializeComponent();
        }
        #endregion

        #region 静态属性
        private static FrmDaHuaContainer _Instance;
        /// <summary>
        /// 获取单个实例
        /// </summary>
        /// <returns></returns>
        public static FrmDaHuaContainer GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new FrmDaHuaContainer();
            }
            return _Instance;
        }
        #endregion

        #region 私有变量
        private fDisConnect disConnect;     //离线回调
        private fHaveReConnect onlineEvent; //重连成功回调
        private fAnalyzerDataCallBack cbAnalyzerDat; // 相机事件回调

        //待连接设备列表
        private static Dictionary<string, camera> prepareConnectDevs = new Dictionary<string, camera>();

        //已连接设备列表,包括在线、离线设备，特存储CLIENT_Login成功的设备
        private static Dictionary<int, camera> ConnectedDevs = new Dictionary<int, camera>();

        //分析句柄列表 ,便于事件消息回调中，从分析句柄获取到camera信息
        private static Dictionary<int, camera> lAnalylists = new Dictionary<int, camera>();

        //回调事件队列
        private static Queue<EventMsg> queueMsgs = new Queue<EventMsg>();
        #endregion

        #region 

        #endregion
    }
}
