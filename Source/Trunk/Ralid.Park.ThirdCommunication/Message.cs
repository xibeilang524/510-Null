using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.ThirdCommunication
{
    /// <summary>
    /// 表示一个消息类
    /// </summary>
    public class Message
    {
        /*
           消息统一采用GB2312字符串；消息格式如下：
           开始符	功能关键字	分隔符	数据长度	分隔符	数据	结束符
           其中数据格式如下：
           数据字段1	数据分隔符	数据字段2	数据分隔符	……	数据字段n
           开始符：“~”；  结束符：“#”； 分隔符：“&”； 数据分隔符：“|”；
        */

        #region 静态变量和方法
        private static int _SerialNum;
        private static object _Locker = new object();

        private static int SerialNum()
        {
            lock (_Locker)
            {
                return ++_SerialNum;
            }
        }
        #endregion

        #region 构造函数
        public Message(int totalPort, int vacant) //车位余数消息
        {
            //~PCPKU&0070&2008120800001|1001|400|200|200|200|40|30|0|0|40|30|2008-12-08 17:00:00 #
            DateTime dt = DateTime.Now;
            string serial = dt.ToString("yyyyMMdd") + SerialNum().ToString("00000");

            string data = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}",
                serial, ParkID, totalPort, vacant, 0, 0, 0, 0, 0, 0, 0, 0, dt.ToString("yyyy-MM-dd HH:mm:ss"));
            _Message = string.Format("~{0}&{1}&{2}#", "PCPKU", System.Text.ASCIIEncoding.GetEncoding("GB2312").GetByteCount(data).ToString("0000"), data);
        }

        public Message(Ralid.Park.BusinessModel.Report.CardEventReport report) //卡片进出记录消息
        {
            DateTime dt = DateTime.Now;
            string serialNum = dt.ToString("yyyyMMdd") + SerialNum().ToString("00000");
            string carplate = !string.IsNullOrEmpty(report.CarPlate) ? report.CarPlate : new string(' ', 1);
            string data;

            if (report.IsExitEvent)
            {
                //~ PCQRU &0108&2008120800001|1001|粤A8888Q|2008-12-08 13:00:00|2008-12-08 17:00:00|临保|00:04:00|40|200|2008-12-08 17:00:00 #

                string cardType = report.CardType.IsMonthCard ? "月保" : "临保";
                string interval = ParkInterval(report);

                data = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}",
                    serialNum,
                    ParkID,
                    carplate,
                    report.LastDateTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    report.EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    cardType,
                    interval,
                    (int)(report.CardPaymentInfo != null ? report.CardPaymentInfo.Paid : 0),
                    GetVacant(),
                    dt.ToString("yyyy-MM-dd HH:mm:ss"));
                _Message = string.Format("~{0}&{1}&{2}#", "PCQRU", System.Text.ASCIIEncoding.GetEncoding("GB2312").GetByteCount(data).ToString("0000"), data);
            }
            else
            {
                //~PCERU&0070&2008120800001|1001|粤A8888Q|2008-12-08 13:00:00|40|2008-12-08 17:00:00#
                data = string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                    serialNum,
                    ParkID,
                    carplate,
                    report.EventDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    GetVacant(),
                    dt.ToString("yyyy-MM-dd HH:mm:ss"));
                _Message = string.Format("~{0}&{1}&{2}#", "PCERU", System.Text.ASCIIEncoding.GetEncoding("GB2312").GetByteCount(data).ToString("0000"), data);
            }
        }
        #endregion

        #region 私有成员
        private string _Message;
        private string ParkID
        {
            get
            {
                string temp = Ralid.Park.BusinessModel.Configuration.AppSettings.CurrentSetting.GetConfigContent("ParkID");
                return string.IsNullOrEmpty(temp) ? "0000" : temp;
            }
        }
        #endregion

        #region 私有方法
        private string ParkInterval(Ralid.Park.BusinessModel.Report.CardEventReport report)
        {
            if (report.LastDateTime != null)
            {
                TimeSpan span = new TimeSpan(report.EventDateTime.Ticks - report.LastDateTime.Value.Ticks);
                return string.Format("{0:D2}:{1:D2}:{2:D2}", span.Days, span.Hours, span.Minutes);
            }
            else
            {
                return "00:00:00";
            }
        }

        private int GetVacant()
        {
            int vacant = 0;
            try
            {
                ParkBuffer.Current.InValid();
                foreach (ParkInfo park in ParkBuffer.Current.Parks)
                {
                    vacant += park.Vacant;
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return vacant;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 把消息转换成字节码
        /// </summary>
        /// <returns></returns>
        public byte[] GetMessageBytes()
        {
            if (!string.IsNullOrEmpty(_Message))
            {
                return System.Text.ASCIIEncoding.GetEncoding("GB2312").GetBytes(_Message);
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            return _Message;
        }
        #endregion
    }
}
