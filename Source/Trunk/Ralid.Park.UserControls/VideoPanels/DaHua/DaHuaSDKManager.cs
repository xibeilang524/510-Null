using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;
using Ralid.GeneralLibrary;
using MyNet.CCTV.Control.Implement.DaHua;

namespace Ralid.Park.UserControls.VideoPanels
{
    /// <summary>
    /// 表示大华SDK包的管理者
    /// </summary>
    public class DaHuaSDKManager
    {
        #region 静态属性
        private static DaHuaSDKManager _Instance;
        /// <summary>
        /// 获取当前实例
        /// </summary>
        /// <returns></returns>
        public static DaHuaSDKManager GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new DaHuaSDKManager();
            }
            return _Instance;
        }
        #endregion

        #region 私有变量
        private fDisConnect disConnect;       //设备离线消息
        private fHaveReConnect onlineMsg;     //设备重新在线消息

        private bool initialized; //SDK是否已初始化
        #endregion

        #region 公共事件
        /// <summary>
        /// 设备离线处理事件处理
        /// </summary>
        public event fDisConnect DisConnectEventHandle;
        /// <summary>
        /// 设备重新在线事件处理
        /// </summary>
        public event fHaveReConnect OnlineMsgEventHandle;
        #endregion

        #region 构造函数
        public DaHuaSDKManager()
        {
            disConnect = new fDisConnect(DisConnectEvent);
            onlineMsg = new fHaveReConnect(OnlineEvent);
        }
        #endregion

        #region 私有事件
        /// <summary>
        /// 设备离线事件
        /// </summary>
        /// <param name="lLoginID"></param>
        /// <param name="pchDVRIP"></param>
        /// <param name="nDVRPort"></param>
        /// <param name="dwUser"></param>
        private void DisConnectEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            if (DisConnectEventHandle != null)
            {
                DisConnectEventHandle(lLoginID, pchDVRIP, nDVRPort, dwUser);
            }
        }

        /// <summary>
        /// 自动重连成功事件
        /// </summary>
        /// <param name="lLoginID"></param>
        /// <param name="pchDVRIP"></param>
        /// <param name="nDVRPort"></param>
        /// <param name="dwUser"></param>
        private void OnlineEvent(int lLoginID, StringBuilder pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            if (OnlineMsgEventHandle != null)
            {
                OnlineMsgEventHandle(lLoginID, pchDVRIP, nDVRPort, dwUser);
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化SDK
        /// </summary>
        public bool InitSDK()
        {
            //初始化SDK
            if (initialized == false)
            {
                DHClient.DHInit(disConnect, IntPtr.Zero);
                DHClient.DHSetAutoReconnect(onlineMsg, IntPtr.Zero);
                initialized = true;
            }
            return initialized;
        }

        /// <summary>
        /// 保存抓拍图片
        /// </summary>
        /// <param name="buf"></param>
        /// <returns>保存路径</returns>
        public string SaveSnapImage(byte[] buf)
        {
            string dir = TempFolderManager.GetCurrentFolder();
            string path = Path.Combine(dir, Guid.NewGuid().ToString() + ".jpg");
            if (SaveSnapImage(path, buf))
            {
                return path;
            }
            return string.Empty;
        }

        /// <summary>
        /// 保存抓拍图片到指定路径
        /// </summary>
        /// <param name="path"></param>
        /// <param name="buf"></param>
        /// <returns></returns>
        public bool SaveSnapImage(string path, byte[] buf)
        {
            try
            {
                //File.WriteAllBytes(path, buf);
                using (System.IO.FileStream fs = System.IO.File.Create(path))
                {
                    fs.Write(buf, 0, (int)buf.Length);
                    fs.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 保存车牌小图:大华早期交通抓拍机，设备不传单独的车牌小图文件，只传车牌在大图中的坐标；由应用来自行裁剪。2014年后，陆续有设备版本，支持单独传车牌小图，小图附录在pBuffer后面。
        /// </summary>
        /// <param name="plateObj">分析物</param>
        /// <param name="pBuffer">数据偏移</param>
        /// <param name="buf">图片数据</param>
        /// <returns>保存路径</returns>
        public string SavePlateJpg(DH_MSG_OBJECT plateObj, IntPtr pBuffer,byte[] buf)
        {
            if ((plateObj.bPicEnble == 1)) //根据pBuffer中数据偏移保存小图图片文件
            {
                // 记录车牌小图文件
                if (plateObj.stPicInfo.dwFileLenth > 0)
                {
                    byte[] bufs = new byte[plateObj.stPicInfo.dwFileLenth];

                    // Marshal.
                    IntPtr pSmapic = (IntPtr)(pBuffer.ToInt32() + (int)(plateObj.stPicInfo.dwOffSet));

                    Marshal.Copy(pSmapic, bufs, 0, bufs.Length);
                    string dir = TempFolderManager.GetCurrentFolder();
                    string sfile = Path.Combine(dir, @"PlateNumber_" + Guid.NewGuid().ToString() + ".jpg");
                    using (System.IO.FileStream fs = System.IO.File.Create(sfile))
                    {
                        fs.Write(bufs, 0, bufs.Length);
                        fs.Close();
                    }

                    return sfile;
                }

            }
            else   //根据大图中的坐标偏移计算显示车牌小图
            {

                if (plateObj.BoundingBox.bottom == 0 && plateObj.BoundingBox.top == 0)
                {
                    return string.Empty;
                }

                DH_RECT dhRect = plateObj.BoundingBox;
                //1.BoundingBox的值是在8192*8192坐标系下的值，必须转化为图片中的坐标
                //2.OSD在图片中占了64行,如果没有OSD，下面的关于OSD的处理需要去掉(把OSD_HEIGHT置为0)
                const int OSD_HEIGHT = 64;
                const int HIMETRIC_INCH = 2540;
                System.IO.MemoryStream memo = new System.IO.MemoryStream(buf);
                Image im = Image.FromStream(memo);
                long nWidth = im.Width;
                long nHeight = im.Height;

                //此处有会引起Error creating window handle错误，而且句柄创建后也没有使用，所以这里注销了 by Jan 2015-05-25
                //System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
                //System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(p.Handle);
                //IntPtr hdc = g.GetHdc();

                nHeight = nHeight - OSD_HEIGHT;
                if ((nWidth <= 0) || (nHeight <= 0))
                {
                    return string.Empty;
                }
                DH_RECT dstRect = new DH_RECT();
                dstRect.left = (int)Math.Ceiling(((double)(nWidth * dhRect.left)) / 8192.0);
                dstRect.right = (int)Math.Ceiling((double)(nWidth * dhRect.right) / 8192.0);
                dstRect.bottom = (int)Math.Ceiling((double)(nHeight * dhRect.bottom) / 8192.0);
                dstRect.top = (int)Math.Ceiling((double)(nHeight * dhRect.top) / 8192.0);

                int x = (int)((double)dstRect.left);
                int y = (int)((double)(dstRect.top + OSD_HEIGHT));
                int w = (int)((double)(dstRect.right - dstRect.left));
                int h = (int)((double)(dstRect.bottom - dstRect.top));

                Bitmap bi = (Bitmap)im.Clone();
                Rectangle rect = new Rectangle(x, y, w, h);
                Bitmap carIM = bi.Clone(rect, bi.PixelFormat);

                string dir = TempFolderManager.GetCurrentFolder();
                string sfile = Path.Combine(dir, @"PlateNumber_" + Guid.NewGuid().ToString() + ".jpg");
                carIM.Save(sfile);

                return sfile;
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取识别对象 车身对象 事件发生时间 车道号等信息
        /// </summary>
        /// <param name="dwAlarmType"></param>
        /// <param name="pAlarmInfo"></param>
        /// <returns></returns>
        public bool GetStuObject(UInt32 dwAlarmType, IntPtr pAlarmInfo, out DH_MSG_OBJECT stuObj, out DH_MSG_OBJECT vehicleObj, out NET_TIME_EX outUTC, out int outlane, out string strMsg)
        {
            DH_MSG_OBJECT msg = new DH_MSG_OBJECT();
            DH_MSG_OBJECT veahcile = new DH_MSG_OBJECT();
            NET_TIME_EX utc = new NET_TIME_EX();
            int lane = 0;

            DH_EVENT_FILE_INFO fileinfo = new DH_EVENT_FILE_INFO();

            string EventMsg = "未定义事件";


            if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFICGATE)
            {
                DEV_EVENT_TRAFFICGATE_INFO Info = new DEV_EVENT_TRAFFICGATE_INFO();
                Info = (DEV_EVENT_TRAFFICGATE_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(DEV_EVENT_TRAFFICGATE_INFO));
                stuObj = Info.stuObject;
                vehicleObj = Info.stuVehicle;
                outUTC = Info.UTC;
                outlane = Info.nLane;
                fileinfo = Info.stuFileInfo;
                EventMsg = "交通卡口事件";
            }
            else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFICJUNCTION)
            {
                DEV_EVENT_TRAFFICJUNCTION_INFO Info = new DEV_EVENT_TRAFFICJUNCTION_INFO();
                Info = (DEV_EVENT_TRAFFICJUNCTION_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(DEV_EVENT_TRAFFICJUNCTION_INFO));
                stuObj = Info.stuObject;
                vehicleObj = Info.stuVehicle;
                outUTC = Info.UTC;
                outlane = Info.nLane;
                fileinfo = Info.stuFileInfo;

                EventMsg = "交通路口事件";
            }
            else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_RUNREDLIGHT)
            {
                DEV_EVENT_TRAFFIC_RUNREDLIGHT_INFO Info = new DEV_EVENT_TRAFFIC_RUNREDLIGHT_INFO();
                Info = (DEV_EVENT_TRAFFIC_RUNREDLIGHT_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(DEV_EVENT_TRAFFIC_RUNREDLIGHT_INFO));
                stuObj = Info.stuObject;
                vehicleObj = Info.stuVehicle;
                outUTC = Info.UTC;
                outlane = Info.nLane;
                fileinfo = Info.stuFileInfo;

                EventMsg = "交通违章-闯红灯事件";
            }
            else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_TURNLEFT)
            {
                DEV_EVENT_TRAFFIC_TURNLEFT_INFO Info = new DEV_EVENT_TRAFFIC_TURNLEFT_INFO();
                Info = (DEV_EVENT_TRAFFIC_TURNLEFT_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(DEV_EVENT_TRAFFIC_TURNLEFT_INFO));
                stuObj = Info.stuObject;
                vehicleObj = Info.stuVehicle;
                outUTC = Info.UTC;
                outlane = Info.nLane;
                fileinfo = Info.stuFileInfo;

                EventMsg = "交通违章-违章左转";
            }
            else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_TURNRIGHT)
            {
                DEV_EVENT_TRAFFIC_TURNRIGHT_INFO Info = new DEV_EVENT_TRAFFIC_TURNRIGHT_INFO();
                Info = (DEV_EVENT_TRAFFIC_TURNRIGHT_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(DEV_EVENT_TRAFFIC_TURNRIGHT_INFO));
                stuObj = Info.stuObject;
                vehicleObj = Info.stuVehicle;
                outUTC = Info.UTC;
                outlane = Info.nLane;
                fileinfo = Info.stuFileInfo;

                EventMsg = "交通违章-违章右转";
            }
            else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_OVERSPEED)
            {
                DEV_EVENT_TRAFFIC_OVERSPEED_INFO Info = new DEV_EVENT_TRAFFIC_OVERSPEED_INFO();
                Info = (DEV_EVENT_TRAFFIC_OVERSPEED_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(DEV_EVENT_TRAFFIC_OVERSPEED_INFO));
                stuObj = Info.stuObject;
                vehicleObj = Info.stuVehicle;
                outUTC = Info.UTC;
                outlane = Info.nLane;
                fileinfo = Info.stuFileInfo;

                EventMsg = "交通违章-超速";
            }
            else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_UNDERSPEED)
            {
                DEV_EVENT_TRAFFIC_UNDERSPEED_INFO Info = new DEV_EVENT_TRAFFIC_UNDERSPEED_INFO();
                Info = (DEV_EVENT_TRAFFIC_UNDERSPEED_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(DEV_EVENT_TRAFFIC_UNDERSPEED_INFO));
                stuObj = Info.stuObject;
                vehicleObj = Info.stuVehicle;
                outUTC = Info.UTC;
                outlane = Info.nLane;
                fileinfo = Info.stuFileInfo;

                EventMsg = "交通违章-低速";
            }
            else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_MANUALSNAP)
            {
                DEV_EVENT_TRAFFIC_MANUALSNAP_INFO Info = new DEV_EVENT_TRAFFIC_MANUALSNAP_INFO();
                Info = (DEV_EVENT_TRAFFIC_MANUALSNAP_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(DEV_EVENT_TRAFFIC_MANUALSNAP_INFO));
                stuObj = Info.stuObject;
                vehicleObj = Info.stuVehicle;
                outUTC = Info.UTC;
                outlane = Info.nLane;
                fileinfo = Info.stuFileInfo;

                EventMsg = "交通手动抓拍事件";
            }
            else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_PARKING)
            {
                DEV_EVENT_TRAFFIC_PARKING_INFO Info = new DEV_EVENT_TRAFFIC_PARKING_INFO();
                Info = (DEV_EVENT_TRAFFIC_PARKING_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(DEV_EVENT_TRAFFIC_PARKING_INFO));
                stuObj = Info.stuObject;
                vehicleObj = Info.stuVehicle;
                outUTC = Info.UTC;
                outlane = Info.nLane;
                fileinfo = Info.stuFileInfo;

                EventMsg = "交通违章停车";
            }
            else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_PARKINGSPACENOPARKING)
            {
                DEV_EVENT_TRAFFIC_PARKINGSPACENOPARKING_INFO Info = new DEV_EVENT_TRAFFIC_PARKINGSPACENOPARKING_INFO();
                Info = (DEV_EVENT_TRAFFIC_PARKINGSPACENOPARKING_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(DEV_EVENT_TRAFFIC_PARKINGSPACENOPARKING_INFO));
                stuObj = Info.stuObject;
                vehicleObj = Info.stuVehicle;
                outUTC = Info.UTC;
                outlane = Info.nLane;
                fileinfo = Info.stuFileInfo;

                EventMsg = "车位无车事件";
            }
            else if (dwAlarmType == EventIvs.EVENT_IVS_TRAFFIC_PARKINGSPACEPARKING)
            {
                DEV_EVENT_TRAFFIC_PARKINGSPACEPARKING_INFO Info = new DEV_EVENT_TRAFFIC_PARKINGSPACEPARKING_INFO();
                Info = (DEV_EVENT_TRAFFIC_PARKINGSPACEPARKING_INFO)Marshal.PtrToStructure(pAlarmInfo, typeof(DEV_EVENT_TRAFFIC_PARKINGSPACEPARKING_INFO));
                stuObj = Info.stuObject;
                vehicleObj = Info.stuVehicle;
                outUTC = Info.UTC;
                outlane = Info.nLane;
                fileinfo = Info.stuFileInfo;

                EventMsg = "车位有车事件";
            }
            else
            {
                stuObj = msg;
                vehicleObj = veahcile;
                outUTC = utc;
                outlane = 0;

                EventMsg = "未处理事件dwAlarmType = " + dwAlarmType.ToString();
            }

            if (!EventMsg.Contains("未处理事件"))
            {
                EventMsg = EventMsg + ";组编号GroupID = " + fileinfo.nGroupId + ";图片组总数bount = " + fileinfo.bCount + ";当前图片序号bIndex=" + fileinfo.bIndex;
                try
                {
                    //车牌
                    string platenumber = Encoding.GetEncoding("gb2312").GetString(stuObj.szText);
                    string[] plate = platenumber.Split('\0');
                    if (plate.Length > 0)
                    {
                        EventMsg += ";车牌号 = " + plate[0];
                    }

                    //车标
                    string strType = Encoding.GetEncoding("gb2312").GetString(vehicleObj.szText);
                    string[] vechitypes = strType.Split('\0');
                    if (vechitypes.Length > 0)
                    {
                        // "Unknown"未知 
                        // "Audi" 奥迪
                        // "Honda" 本田
                        // "Buick" 别克
                        // "Volkswagen" 大众
                        // "Toyota" 丰田
                        // "BMW" 宝马
                        // "Peugeot" 标致
                        // "Ford" 福特
                        // "Mazda" 马自达
                        // "Nissan" 尼桑
                        // "Hyundai" 现代
                        // "Suzuki" 铃木
                        // "Citroen" 雪铁龙
                        // "Benz" 奔驰
                        // "BYD" 比亚迪
                        // "Geely" 吉利
                        // "Lexus" 雷克萨斯
                        // "Chevrolet" 雪佛兰
                        // "Chery" 奇瑞
                        // "Kia" 起亚
                        // "Charade" 夏利
                        // "DF" 东风
                        // "Naveco" 依维柯
                        // "SGMW" 五菱
                        // "Jinbei" 金杯

                        if (!vechitypes[0].Equals(""))
                        {
                            EventMsg += ";车标 = " + vechitypes[0];
                        }
                    }
                }
                catch
                {
                    ;
                }
                finally
                {
                    ;
                }
            }
            strMsg = EventMsg;
            return true;

        }

        //颜色对照表
        public string GetColorString(uint RGBColor)
        {
            string strColor = RGBColor.ToString();
            uint Color = RGBColor >> 8;
            if (Color == 0x000000)
            {
                strColor = "黑色";
            }
            else if (Color == 0xFFFFFF)
            {
                strColor = "白色";
            }
            else if (Color == 0xFF0000)
            {
                strColor = "红色";
            }
            else if (Color == 0x0000FF)
            {
                strColor = "蓝色";
            }
            else if (Color == 0x00FF00)
            {
                strColor = "绿色";
            }
            else if (Color == 0xFFFF00)
            {
                strColor = "黄色";
            }
            else if (Color == 0x808080)
            {
                strColor = "灰色";
            }
            else if (Color == 0xFFA500)
            {
                strColor = "橙色";
            }
            else
            {
                strColor = "未定义颜色值";
            }

            return strColor;


        }
        #endregion
    }
}
