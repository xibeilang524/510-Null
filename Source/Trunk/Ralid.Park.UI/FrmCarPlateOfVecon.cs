using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceProcess;
using System.Runtime.Serialization;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Threading;
using Ralid.Park.BLL;
using Ralid.Park.PlateRecognition;
using Ralid.GeneralLibrary;
using Ralid.GeneralLibrary.Service;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    public partial class FrmCarPlateOfVecon : Form, IPlateRecognition
    {
        #region 静态属性
        private static FrmCarPlateOfVecon _Instance;
        /// <summary>
        /// 获取单个实例
        /// </summary>
        /// <returns></returns>
        public static FrmCarPlateOfVecon GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new FrmCarPlateOfVecon();
            }
            return _Instance;
        }
        #endregion

        #region 构造函数
        private FrmCarPlateOfVecon()
        {
            InitializeComponent();
        }
        #endregion

        #region 私有变量
        private object _Locker = new object();  //车牌识别多线程之间串行进行，所以车牌识别时加锁

        private bool initSuccess;
        private readonly string Para = @"p1|/car|/c|/e|/m|file";
        private readonly short Success = 0;
        private string dic = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
        #endregion
        
        #region 公共方法
        /// <summary>
        /// 通过图片文件识别车牌号
        /// </summary>
        public PlateRecognitionResult Recognize(string path)
        {
            PlateRecognitionResult result = new PlateRecognitionResult();
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    string plnInfo = string.Empty;
                    if (initSuccess)
                    {
                        short ret = rm.RecognizeImageFile(path, string.Empty, ref plnInfo);
                        if (ret == Success)
                        {
                            result = ExtractPlate(plnInfo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                }
            }
            return result;
        }

        /// <summary>
        /// 初始化车牌识别
        /// </summary>
        /// <returns></returns>
        public void Init()
        {
            Action action = delegate()
            {
                if (!initSuccess) //如果已经初始化了
                {
                    try
                    {
                        initSuccess = rm.InitializeRecognizer(Para, string.Empty) == Success;
                        if (!initSuccess)
                        {
                            //MessageBox.Show(Resources.Resource1.CarPlate_Fail);
                            //Ralid.GeneralLibrary.LOG.FileLog.Log("System", Resources.Resource1.CarPlate_Fail);

                            Ralid.GeneralLibrary.LOG.FileLog.Log("系统", "型号V车牌识别初始化失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                    }
                }
            };
            Thread t = new Thread(new ThreadStart(action));
            t.Start();
        }
        #endregion

        #region 私有方法

        private string GetColorDescr(int colorCode)
        {
            switch (colorCode)
            {
                case 1:
                    return Resources.Resource1.CarPlate_WhiteOnBlue;
                case 2:
                    return Resources.Resource1.CarPlate_BlackOnYellow;
                case 3:
                    return Resources.Resource1.CarPlate_WhiteOnBlack;
                case 4:
                    return Resources.Resource1.CarPlate_BlackOnWhite;
                case 5:
                    return Resources.Resource1.CarPlate_RedOnBlack;
                case 6:
                    return Resources.Resource1.CarPlate_RedOnWhite;
                case 7:
                    return Resources.Resource1.CarPlate_WhiteOnRed;
                case 8:
                    return Resources.Resource1.CarPlate_WhiteOnGreen;
                default:
                    return Resources.Resource1.CarPlate_UnknowColor;
            }
        }

        private PlateRecognitionResult ExtractPlate(string plnInfo)
        {
            //plnInfo 的格式形如 [pln|score|坐标|color]只取pln
            PlateRecognitionResult result = new PlateRecognitionResult();
            if (!string.IsNullOrEmpty(plnInfo))
            {
                try
                {
                    plnInfo = plnInfo.Trim('[', ']');
                    string[] infoes = plnInfo.Split('|');
                    if (infoes != null && infoes.Length > 0)
                    {
                        result.CarPlate = infoes[0];

                        int temp = 0;
                        int.TryParse(infoes[1], out temp);
                        result.Color = GetColorDescr(temp);
                    }
                }
                catch
                {
                }
            }
            return result;
        }

        private void DoKill(string processName)
        {
            Process[] ps = Process.GetProcessesByName(processName);
            if (ps.Count() > 0)
            {
                Process process = new Process();
                process.StartInfo.FileName = "taskkill.exe";
                process.StartInfo.Arguments = string.Format("/F /im {0}.exe", processName);
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
            }
        }
        #endregion

        #region IPlateRecognition 成员
        public PlateRecognitionResult Recognize(int parkID, int entranceID)
        {
            string dir = TempFolderManager.GetCurrentFolder();
            PlateRecognitionResult ret = new PlateRecognitionResult();
            try
            {
                //EntranceInfo entrance = ParkBuffer.Current.GetEntrance(entranceID);
                EntranceInfo entrance = ParkBuffer.Current.GetEntrance(parkID, entranceID);
                if (entrance != null)
                {
                    foreach (VideoSourceInfo video in entrance.VideoSources)
                    {
                        if (video.IsForCarPlate)
                        {
                            FrmSnapShoter frm = FrmSnapShoter.GetInstance();
                            string path = Path.Combine(dir, string.Format("{0}_{1}_{2}.jpg", "CarPlate", Guid.NewGuid().ToString(), video.VideoID));
                            if (frm.SnapShotTo(video, ref path, true, false))
                            {
                                ret = Recognize(path);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return ret;
        }
        #endregion

        #region 事件处理方法
        private void FrmVeconCarPlateRecognization_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void FrmCarPlateRecognizationImp_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (initSuccess)
            {
                rm.ReleaseRecognizer();
            }
            DoKill("AVTRSrv");
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            Init();
            if (!initSuccess)
            {
                MessageBox.Show(Resources.Resource1.CarPlate_Fail);
            }
        }
        #endregion 
    }
}
