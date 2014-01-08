using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Ralid.Park.BLL;
using Ralid.Park.PlateRecognition;
using Ralid.GeneralLibrary;
using Ralid.GeneralLibrary.Service;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.UI
{
    public partial class FrmCarPlateOfWintone : Form, IPlateRecognition
    {
        #region 静态属性
        private static FrmCarPlateOfWintone _Instance;
        /// <summary>
        /// 获取单个实例
        /// </summary>
        /// <returns></returns>
        public static FrmCarPlateOfWintone GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new FrmCarPlateOfWintone();
            }
            return _Instance;
        }
        #endregion

        #region 车牌识别库封装
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct TH_PlateIDCfg
        {
            public int nMinPlateWidth; // 检测的最小车牌宽度，以像素为单位
            public int nMaxPlateWidth; // 检测的最大车牌宽度，以像素为单位
            public int nMaxImageWidth; // 最大图像宽度
            public int nMaxImageHeight; // 最大图像高度
            public byte bVertCompress; // 是否只取帧图像的一场进行识别。
            public byte bIsFieldImage; // 是否输入场图像
            public byte bOutputSingleFrame; /*是否视频图像中同一个车的多幅图像只输出一次结果*/
            public byte bMovingImage; // 识别运动or 静止图像
            public byte bIsNight; //夜间模式
            public byte nImageFormat; //图像格式
            public IntPtr pFastMemory; /*DSP 等的片内内存，耗时多的运算优先使用这些内存*/
            public int nFastMemorySize; // 快速内存的大小
            public IntPtr pMemory; /*普通内存的地址，内建的内存管理，避免内存泄漏等问题*/
            public int nMemorySize; // 普通内存的大小
            public int nLastError; // 用于传递错误信息
            // 0: 无错误
            // 1: Find Plate(没有找到车牌)
            // 2: 车牌评价值(0 分)
            // 3: 车牌评价值(不及格)
            // 4: 车牌识别分数(0 分)
            // 5: 车牌识别分数(不及格)
            public int nErrorModelSN; // 出错的模块编号
            public byte nOrderOpt;		//输出顺序选项 0-置信度 1-自上而下 2-自下而上
            public byte bLeanCorrection;	// 是否启用车牌旋转功能
            public byte bMovingOutputOpt; 	// 0-内部推送+外部获取 1:外部获取
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 117)]
            public char[] reserve; //保留
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct TH_PlateResult
        {
            public short lic0;
            public char lic1;
            public char lic2;
            public char lic3;
            public char lic4;
            public char lic5;
            public char lic6;
            public int lic7;
            public int lic8;
            public int color0;
            public int color1;
            public int nColor;	//颜色（数值）
            public int nType;	//车牌类型（见定义）
            public int nConfidence;	//整牌可信度
            public int nBright;	//亮度评价
            public int nDirection; // 运动方向，0 unknown, 1 left, 2 right, 3 up , 4 down
            public TH_RECT rcLocation;	//车牌在整个图像中的位置

            public int pbyBits;	// DSP等的片内内存，耗时多的运算优先使用这些内存
            public int nTime;	//识别时间
            public byte nCarBright;    //车的亮度  保留
            public byte nCarColor;		//车的颜色  保留
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
            public char[] reserved;	//保留
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto)]
        struct TH_RECT
        {
            [FieldOffset(0)]
            public int left;
            [FieldOffset(4)]
            public int top;
            [FieldOffset(8)]
            public int right;
            [FieldOffset(12)]
            public int bottom;
        }

        [DllImport("TH_PLATEID.dll")]
        static extern int TH_InitPlateIDSDK(ref TH_PlateIDCfg pPlateConfig);

        [DllImport("TH_PLATEID.dll")]
        static extern int TH_UninitPlateIDSDK(ref TH_PlateIDCfg pPlateConfig);

        [DllImport("TH_PLATEID.dll")]
        static extern int TH_SetImageFormat(byte cImageFormat, bool bVertFlip,
            bool bDwordAligned, ref TH_PlateIDCfg pPlateIDConfig);

        [DllImport("TH_PLATEID.dll")]   ///     识别车牌号码
        static extern int TH_SetRecogThreshold(byte nLocaticon_th, byte nPlate_th, ref TH_PlateIDCfg pPlateConfig);

        [DllImport("TH_PLATEID.dll")]
        static extern int TH_SetDayNightMode(byte bIsNight, ref TH_PlateIDCfg pPlateConfig);

        [DllImport("TH_PLATEID.dll")]
        static extern int TH_SetProvinceOrder(string szProvince, ref TH_PlateIDCfg pPlateConfig);

        [DllImport("TH_PLATEID.dll")]
        static extern int TH_SetEnabledPlateFormat(int dFormat, ref TH_PlateIDCfg pPlateConfig);

        [DllImport("TH_PLATEID.dll")]   ///     识别车牌号码
        static extern int TH_RecogImage(byte[] pbyBits, int nWidth, int nHeight,
           [In, Out]  TH_PlateResult[] pResult, ref int nResultNum, ref TH_RECT prcRange, ref TH_PlateIDCfg pPlateConfig);

        #endregion

        #region 构造函数
        public FrmCarPlateOfWintone()
        {
            InitializeComponent();
            try
            {
                string filePath = Application.StartupPath + @"\WintoneCarplateSetting.xml";
                if (File.Exists(filePath))
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(WintoneCarplateSetting));
                        _CarplateSetting = xs.Deserialize(fs) as WintoneCarplateSetting;
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            if (_CarplateSetting == null) _CarplateSetting = WintoneCarplateSetting.DefaultSetting();
        }
        #endregion

        #region 私有变量
        private TH_PlateIDCfg c_defConfig;
        private WintoneCarplateSetting _CarplateSetting;
        private bool _Inited = false;
        private object _Locker = new object();
        #endregion

        #region 私有方法
        //转换车牌号码
        private string PlateLicense(short sh0, char sh1, char sh2, char sh3)
        {
            StringBuilder sbo = new StringBuilder();
            ///　转换数字及字母 1-2
            byte[] bytearrayNum = new byte[2];
            string constructed = "";
            ASCIIEncoding assencoding = new ASCIIEncoding();
            ///　转换汉字
            for (int i = 0; i < bytearrayNum.Length; i++)
            {
                bytearrayNum[i] = (byte)(sh0 >> 8 * (i) & 0xFF);
            }
            Encoding encod = Encoding.GetEncoding(936);
            byte[] buf2 = Encoding.Convert(encod, Encoding.Unicode, bytearrayNum);
            constructed = Encoding.Unicode.GetString(buf2, 0, buf2.Length);
            if (!constructed.Equals(""))
            {
                sbo.Append(constructed);
                constructed = "";
            }
            ///　转换数字及字母 1-2 
            for (int i = 0; i < bytearrayNum.Length; i++)
            {
                bytearrayNum[i] = (byte)(sh1 >> 8 * (i) & 0xFF);
            }
            constructed = assencoding.GetString(bytearrayNum, 0, bytearrayNum.Length);
            if (!constructed.Equals(""))
            {
                sbo.Append(constructed);
                constructed = "";
            }
            ///　转换数字及字母 3-4 
            for (int i = 0; i < bytearrayNum.Length; i++)
            {
                bytearrayNum[i] = (byte)(sh2 >> 8 * (i) & 0xFF);
            }
            constructed = assencoding.GetString(bytearrayNum, 0, bytearrayNum.Length);
            if (!constructed.Equals(""))
            {
                sbo.Append(constructed);
                constructed = "";
            }
            ///　转换数字及字母 5-6
            for (int i = 0; i < bytearrayNum.Length; i++)
            {
                bytearrayNum[i] = (byte)(sh3 >> 8 * (i) & 0xFF);
            }
            constructed = assencoding.GetString(bytearrayNum, 0, bytearrayNum.Length);
            if (!constructed.Equals(""))
            {
                sbo.Append(constructed);
                constructed = "";
            }
            if (sbo.Length > 0) return sbo.ToString().Trim('\0');
            return string.Empty;
        }

        //读取BMP图片到内存
        private byte[] LockUnlockBitsExample(string strFilename, out int nHeight, out int nWidth)
        {
            // Create a new bitmap. 
            Bitmap bmp = new Bitmap(strFilename);
            nHeight = bmp.Height;
            nWidth = bmp.Width;
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);
            // Get the address of the first line. 
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap. 
            int bytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array. 
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Set every red value to 255.  
            for (int counter = 0; counter < rgbValues.Length; counter += 3)
                rgbValues[counter] = 255;
            // Copy the RGB values back to the bitmap 
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits. 
            bmp.UnlockBits(bmpData);
            return rgbValues;
        }

        private void GetFromInput()
        {
            _CarplateSetting.MinPlateWidth = txtMinPlateWidth.IntergerValue;
            _CarplateSetting.MaxPlateWidth = txtMaxPlateWidth.IntergerValue;
            _CarplateSetting.MaxImageWidth = txtMaxImageWidth.IntergerValue;
            _CarplateSetting.MaxImageHeight = txtMaxImageHeight.IntergerValue;
            _CarplateSetting.OCR_Th = (byte)txtOCR_Th.IntergerValue;
            _CarplateSetting.PlateLocate_Th = (byte)txtPlateLocate_Th.IntergerValue;
            _CarplateSetting.DefaultProvince = txtProvince.Text;
            _CarplateSetting.MovingImage = chkMovingImage.Checked;
            _CarplateSetting.VertCompress = chkVertCompress.Checked;
            _CarplateSetting.IsFieldImage = chkIsFieldImage.Checked;
            _CarplateSetting.LeanCorrection = chkLeanCorrection.Checked;
            _CarplateSetting.IsNight = chkIsNight.Checked;

            _CarplateSetting.ArmPolice_On = chkArmPolice_On.Checked;
            _CarplateSetting.TwoRowYellow_On = chkTwoRowYellow_On.Checked;
            _CarplateSetting.Embassy_On = chkEmbassy_On.Checked;
            _CarplateSetting.TwoRowArmPolice_On = chkTwoRowArmPolice_On.Checked;
            _CarplateSetting.Only_TwoRowYellow_On = chkOnly_TwoRowYellow_On.Checked;
            _CarplateSetting.Tractor_On = chkTractor_On.Checked;
            _CarplateSetting.TwoRowArmy_On = chkTwoRowArmy_On.Checked;
            _CarplateSetting.Individual_On = chkIndividual_On.Checked;
            _CarplateSetting.Only_Location_On = chkOnly_Location_On.Checked;
        }

        private void ShowSetting(WintoneCarplateSetting w)
        {
            txtMinPlateWidth.IntergerValue = w.MinPlateWidth;
            txtMaxPlateWidth.IntergerValue = w.MaxPlateWidth;
            txtMaxImageWidth.IntergerValue = w.MaxImageWidth;
            txtMaxImageHeight.IntergerValue = w.MaxImageHeight;
            txtPlateLocate_Th.IntergerValue = w.PlateLocate_Th;
            txtOCR_Th.IntergerValue = w.OCR_Th;
            txtProvince.Text = w.DefaultProvince;
            chkMovingImage.Checked = w.MovingImage;
            chkVertCompress.Checked = w.VertCompress;
            chkIsFieldImage.Checked = w.IsFieldImage;
            chkLeanCorrection.Checked = w.LeanCorrection;
            chkIsNight.Checked = w.IsNight;

            chkArmPolice_On.Checked = w.ArmPolice_On;
            chkTwoRowArmPolice_On.Checked = w.TwoRowArmPolice_On;
            chkTwoRowArmy_On.Checked = w.TwoRowArmy_On;
            chkTwoRowYellow_On.Checked = w.TwoRowYellow_On;
            chkOnly_TwoRowYellow_On.Checked = w.Only_TwoRowYellow_On;
            chkIndividual_On.Checked = w.Individual_On;
            chkEmbassy_On.Checked = w.Embassy_On;
            chkTractor_On.Checked = w.Tractor_On;
            chkOnly_Location_On.Checked = w.Only_Location_On;
        }
        #endregion

        #region 实现 IPlateRecognition接口
        public PlateRecognitionResult Recognize(int parkID, int entranceID)
        {
            string dir = TempFolderManager.GetCurrentFolder();
            PlateRecognitionResult ret = new PlateRecognitionResult();
            try
            {
                EntranceInfo entrance = ParkBuffer.Current.GetEntrance(entranceID);
                if (entrance != null)
                {
                    foreach (VideoSourceInfo video in entrance.VideoSources)
                    {
                        if (video.IsForCarPlate)
                        {
                            FrmSnapShoter frm = FrmSnapShoter.GetInstance();
                            string path = Path.Combine(dir, string.Format("{0}_{1}_{2}.jpg", "CarPlate", Guid.NewGuid().ToString(), video.VideoID));
                            if (frm.SnapShotTo(video, path, true))
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

        #region 公共方法
        /// <summary>
        /// 通过图片文件识别车牌号
        /// </summary>
        public PlateRecognitionResult Recognize(string path)
        {
            int nWidth = 0;
            int nHeight = 0;
            PlateRecognitionResult result = new PlateRecognitionResult();
            try
            {
                if (_Inited)
                {
                    TH_PlateResult[] pRes = new TH_PlateResult[3];
                    int RetNum = 1;
                    byte[] pBuffer = LockUnlockBitsExample(path, out nHeight, out nWidth);
                    TH_RECT rect = new TH_RECT();
                    rect.top = 0;
                    rect.bottom = nHeight;
                    rect.left = 0;
                    rect.right = nWidth;
                    int nResult = -1;
                    lock (_Locker)
                    {
                        nResult = TH_RecogImage(pBuffer, nWidth, nHeight, pRes, ref RetNum, ref rect, ref c_defConfig);
                    }
                    if (nResult == 0)
                    {
                        result.CarPlate = PlateLicense(pRes[0].lic0, pRes[0].lic1, pRes[0].lic2, pRes[0].lic3);//调用车牌号码转换函数
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return result;
        }

        /// <summary>
        /// 初始化车牌识别
        /// </summary>
        /// <returns></returns>
        public void Init()
        {
            try
            {
                if (_Inited)
                {
                    TH_UninitPlateIDSDK(ref c_defConfig);
                    if (c_defConfig.pFastMemory != IntPtr.Zero) Marshal.FreeHGlobal(c_defConfig.pFastMemory);
                    if (c_defConfig.pMemory != IntPtr.Zero) Marshal.FreeHGlobal(c_defConfig.pMemory);
                }
                else
                {
                    c_defConfig = new TH_PlateIDCfg();
                }
                _Inited = true;
                if (_CarplateSetting == null) _CarplateSetting = WintoneCarplateSetting.DefaultSetting();

                c_defConfig.nFastMemorySize = _CarplateSetting.FastMemorySize;
                if (c_defConfig.nFastMemorySize > 0) c_defConfig.pFastMemory = Marshal.AllocHGlobal(c_defConfig.nFastMemorySize);   // stackalloc char[];// mChar;
                c_defConfig.nMemorySize = _CarplateSetting.MemorySize;
                if (c_defConfig.nMemorySize > 0) c_defConfig.pMemory = Marshal.AllocHGlobal(c_defConfig.nMemorySize);
                c_defConfig.nMinPlateWidth = _CarplateSetting.MinPlateWidth;
                c_defConfig.nMaxPlateWidth = _CarplateSetting.MaxPlateWidth;
                c_defConfig.nMaxImageWidth = _CarplateSetting.MaxImageWidth;
                c_defConfig.nMaxImageHeight = _CarplateSetting.MaxImageHeight;
                c_defConfig.bVertCompress = (byte)(_CarplateSetting.VertCompress ? 1 : 0);
                c_defConfig.bIsFieldImage = (byte)(_CarplateSetting.IsFieldImage ? 1 : 0);
                c_defConfig.bOutputSingleFrame = (byte)(_CarplateSetting.OutputSingleFrame ? 1 : 0);
                c_defConfig.bMovingImage = (byte)(_CarplateSetting.MovingImage ? 1 : 0);
                c_defConfig.bLeanCorrection = (byte)(_CarplateSetting.LeanCorrection ? 1 : 0);		// 倾斜校正默认为关闭
                c_defConfig.nImageFormat = _CarplateSetting.ImageFormat;
                c_defConfig.bIsNight = (byte)(_CarplateSetting.IsNight ? 1 : 0);
                int ret = 0;
                ret = TH_InitPlateIDSDK(ref c_defConfig);
                if (ret != 0)
                {
                    MessageBox.Show(Resources.Resource1.CarPlate_Fail);
                    return;
                }
                ret = TH_SetProvinceOrder(_CarplateSetting.DefaultProvince, ref c_defConfig);
                ret = TH_SetRecogThreshold(_CarplateSetting.PlateLocate_Th, _CarplateSetting.OCR_Th, ref c_defConfig);
                ret = TH_SetEnabledPlateFormat(_CarplateSetting.Individual_On ? 0 : 1, ref c_defConfig);
                ret = TH_SetEnabledPlateFormat(_CarplateSetting.TwoRowYellow_On ? 2 : 3, ref c_defConfig);
                ret = TH_SetEnabledPlateFormat(_CarplateSetting.ArmPolice_On ? 4 : 5, ref c_defConfig);
                ret = TH_SetEnabledPlateFormat(_CarplateSetting.TwoRowArmy_On ? 6 : 7, ref c_defConfig);
                ret = TH_SetEnabledPlateFormat(_CarplateSetting.Tractor_On ? 8 : 9, ref c_defConfig);
                ret = TH_SetEnabledPlateFormat(_CarplateSetting.Only_TwoRowYellow_On ? 10 : 11, ref c_defConfig);
                ret = TH_SetEnabledPlateFormat(_CarplateSetting.Embassy_On ? 12 : 13, ref c_defConfig);
                ret = TH_SetEnabledPlateFormat(_CarplateSetting.Only_Location_On ? 14 : 15, ref c_defConfig);
                ret = TH_SetEnabledPlateFormat(_CarplateSetting.TwoRowArmPolice_On ? 16 : 17, ref c_defConfig);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        #region 事件处理程序
        private void btnInit_Click(object sender, EventArgs e)
        {
            try
            {
                GetFromInput();
                Init();
                string filePath = Application.StartupPath + @"\WintoneCarplateSetting.xml";
                Type t = _CarplateSetting.GetType();
                XmlSerializer xs = new XmlSerializer(t);
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    xs.Serialize(stream, _CarplateSetting);
                }
                MessageBox.Show(Resources.Resource1.CarPlate_SavePara);
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void FrmWintoneCarPlateRecognization_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void FrmWintoneCarPlateRecognization_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_Inited)
            {
                Marshal.FreeHGlobal(c_defConfig.pFastMemory);
                Marshal.FreeHGlobal(c_defConfig.pMemory);
                TH_UninitPlateIDSDK(ref c_defConfig);
                _Inited = false;
            }
        }

        private void FrmWintoneCarPlateRecognization_Load(object sender, EventArgs e)
        {
            if (_CarplateSetting != null)
            {
                ShowSetting(_CarplateSetting);
            }
        }
        #endregion
    }

    public class WintoneCarplateSetting
    {
        #region 静态属性
        /// <summary>
        /// 获取默认设置
        /// </summary>
        /// <returns></returns>
        public static WintoneCarplateSetting DefaultSetting()
        {
            WintoneCarplateSetting w = new WintoneCarplateSetting();
            w.MinPlateWidth = 80;
            w.MaxPlateWidth = 200;
            w.MaxImageWidth = 720;
            w.MaxImageHeight = 576;
            w.FastMemorySize = 0x4000;
            w.MemorySize = 40000000;
            w.DefaultProvince = "粤";
            w.PlateLocate_Th = 5;
            w.OCR_Th = 2;
            w.ImageFormat = 1;
            w.OutputSingleFrame = true;
            return w;
        }
        #endregion

        #region 构造函数
        public WintoneCarplateSetting()
        {

        }
        #endregion

        #region 基本设置
        /// <summary>
        /// 获取或设置检测的最小车牌宽度，以像素为单位
        /// </summary>
        public int MinPlateWidth { get; set; } // 
        /// <summary>
        /// 获取或设置检测的最大车牌宽度，以像素为单位
        /// </summary>
        public int MaxPlateWidth { get; set; } // 
        /// <summary>
        /// 获取或设置最大图像宽度
        /// </summary>
        public int MaxImageWidth { get; set; }
        /// <summary>
        /// 获取或设置最大图像高度
        /// </summary>
        public int MaxImageHeight { get; set; }
        /// <summary>
        /// 获取或设置是否启用隔行抽点
        /// </summary>
        public bool VertCompress { get; set; }
        /// <summary>
        /// 获取或设置是否输入场图像
        /// </summary>
        public bool IsFieldImage { get; set; }
        /// <summary>
        /// 获取或设置是否视频图像中同一个车的多幅图像只输出一次结果
        /// </summary>
        public bool OutputSingleFrame { get; set; }
        /// <summary>
        /// 获取或设置识别运动图像
        /// </summary>
        public bool MovingImage { get; set; }
        /// <summary>
        /// 获取或设置夜间模式(这一个选项慎用,会影响正常的车牌识别结果)
        /// </summary>
        public bool IsNight { get; set; }
        /// <summary>
        /// 获取或设置图片格式
        /// </summary>
        public byte ImageFormat { get; set; }
        /// <summary>
        /// 获取或设置 DSP等的片内内存，耗时多的运算优先使用这些内存
        /// </summary>
        public int FastMemorySize { get; set; }
        /// <summary>
        /// 获取或设置普通内存的大小
        /// </summary>
        public int MemorySize { get; set; }
        /// <summary>
        /// 获取或设置输出顺序选项 0-置信度 1-自上而下 2-自下而上
        /// </summary>
        public byte OrderOpt { get; set; }
        /// <summary>
        /// 获取或设置是否启用倾斜较正
        /// </summary>
        public bool LeanCorrection { get; set; }
        /// <summary>
        /// 获取或设置 0-内部推送+外部获取 1:外部获取
        /// </summary>
        public byte MovingOutputOpt { get; set; }
        /// <summary>
        /// 获取或设置车牌定位阀值(0-9)值越大越严格
        /// </summary>
        public byte PlateLocate_Th { get; set; }
        /// <summary>
        /// 获取或设置车牌识别阀值(0-9)值越大越严格
        /// </summary>
        public byte OCR_Th { get; set; }
        /// <summary>
        /// 获取或设置默认省份
        /// </summary>
        public string DefaultProvince { get; set; }
        #endregion

        #region 车牌类型设置
        /// <summary>
        /// 获取或设置个性化车牌识别是否开启
        /// </summary>
        public bool Individual_On { get; set; }
        /// <summary>
        /// 获取或设置双层黄色车牌是否开启
        /// </summary>
        public bool TwoRowYellow_On { get; set; }
        /// <summary>
        /// 获取或设置单层武警车牌是否开启
        /// </summary>
        public bool ArmPolice_On { get; set; }// 
        /// <summary>
        /// 获取或设置双层军队车牌是否开启
        /// </summary>
        public bool TwoRowArmy_On { get; set; }	// 
        /// <summary>
        /// 获取或设置农用车车牌是否开启
        /// </summary>
        public bool Tractor_On { get; set; }// 
        /// <summary>
        /// 获取或设置只识别双层黄牌是否开启
        /// </summary>
        public bool Only_TwoRowYellow_On { get; set; }	// 
        /// <summary>
        /// 获取或设置使馆车牌是否开启
        /// </summary>
        public bool Embassy_On { get; set; }// 
        /// <summary>
        /// 获取或设置只定位车牌是否开启
        /// </summary>
        public bool Only_Location_On { get; set; }// 
        /// <summary>
        ///获取或设置双层武警车牌是否开启
        /// </summary>
        public bool TwoRowArmPolice_On { get; set; }// 
        #endregion
    }
}