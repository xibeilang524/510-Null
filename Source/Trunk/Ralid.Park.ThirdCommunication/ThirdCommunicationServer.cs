using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ralid.Park.ParkAdapter;
using Ralid.Park.BLL;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Configuration;
using Ralid.Park.BusinessModel.Report;

namespace Ralid.Park.ThirdCommunication
{
    /// <summary>
    /// 
    /// </summary>
    public class ThirdCommunicationServer : MarshalByRefObject
    {
        #region 构造函数
        public ThirdCommunicationServer(string setting) //  net:192.168.15.129:80  或者 com:1:9600
        {
            string[] strs = setting.Split(':');
            if (strs.Length == 3)
            {
                if (strs[0] == "net")
                {
                    _Communication = new NetCommunication(strs[1], Convert.ToInt32(strs[2]));
                }
                else if (strs[0] == "com")
                {
                    _Communication = new ComCommunication(Convert.ToByte(strs[1]), Convert.ToInt32(strs[2]));
                }
            }
        }
        #endregion

        #region 私有变量
        private ICommunication _Communication;
        private Stack<CardEventReport> _SendingEvents = new Stack<CardEventReport>();
        private List<ParkingAdapter> _Adapters = new List<ParkingAdapter>();
        private object _Locker = new object();
        private AutoResetEvent _AutoResetEvent = new AutoResetEvent(false);
        #endregion

        #region 公共属性
        public FrmMain MainForm { get; set; }
        #endregion

        #region 公共方法
        public void Start()
        {
            if (_Communication != null)
            {
                ConnectCommunication();
            }

            string stationID = AppSettings.CurrentSetting.WorkstationID;
            if (!string.IsNullOrEmpty(stationID))
            {
                WorkStationInfo.CurrentStation = (new WorkstationBll(AppSettings.CurrentSetting.ParkConnect)).GetWorkStationByID(stationID);
            }
            if (WorkStationInfo.CurrentStation == null)
            {
                foreach (ParkInfo park in ParkBuffer.Current.Parks)
                {
                    ParkingAdapter pad = new ParkingAdapter(park.ParkAdapterUri);
                    pad.ParkID = park.ParkID;
                    pad.Reporting += pad_CardEventReporting;
                    if (pad.ConnectServer())
                    {
                        if (MainForm != null) MainForm.WriteLine(string.Format("连接{0}服务器成功", park.ParkName));
                    }
                    else
                    {
                        if (MainForm != null) MainForm.WriteLine(string.Format("连接{0}服务器失败", park.ParkName));
                    }
                    _Adapters.Add(pad);
                }
            }
            else
            {
                //本工作站连接所有的停车场WCF服务
                foreach (ParkInfo park in ParkBuffer.Current.Parks)
                {
                    if (park.IsRootPark && WorkStationInfo.CurrentStation.IsInListenList(park))//如果是托管的停车场或者侦听了事件的停车场
                    {
                        ParkingAdapter pad = new ParkingAdapter(park.ParkAdapterUri);
                        pad.ParkID = park.ParkID;
                        pad.Reporting += pad_CardEventReporting;
                        if (pad.ConnectServer())
                        {
                            if (MainForm != null) MainForm.WriteLine(string.Format("连接{0}服务器成功", park.ParkName));
                        }
                        else
                        {
                            if (MainForm != null) MainForm.WriteLine(string.Format("连接{0}服务器失败", park.ParkName));
                        }
                        _Adapters.Add(pad);
                    }
                }
            }

            Thread t = new Thread(CheckConnected_Thread);
            t.IsBackground = true;
            t.Start();

            Thread t2 = new Thread(SendParkVacant_Thread);
            t2.IsBackground = true;
            t2.Start();

            Thread t3 = new Thread(Send_Thread);
            t3.IsBackground = true;
            t3.Start();
        }

        public void Close()
        {
            if (_Adapters != null && _Adapters.Count > 0)
            {
                foreach (ParkingAdapter pad in _Adapters)
                {
                    pad.Close();
                }
            }
            if (_Communication != null)
            {
                _Communication.Close();
            }
        }
        #endregion

        #region 私有方法
        private void pad_CardEventReporting(object sender, ReportBase report)
        {
            try
            {
                if (report is CardEventReport)
                {
                    CardEventReport cer = report as CardEventReport;
                    if (cer.EventStatus == CardEventStatus.Valid)
                    {
                        lock (_Locker)
                        {
                            if (_SendingEvents.Count >= 1000)
                            {
                                _SendingEvents.Clear();
                            }
                            _SendingEvents.Push(cer);
                            _AutoResetEvent.Set();
                        }
                    }
                }
            }
            catch 
            {
            }
        }

        private void Send_Thread()
        {
            while (_AutoResetEvent.WaitOne(int.MaxValue))
            {
                CardEventReport report = Pop();
                while (report != null)
                {
                    Message msg = new Message(report);
                    SendMessage(msg);
                    if (MainForm != null) MainForm.WriteLine(msg.ToString());
                    report = Pop();
                }
                _AutoResetEvent.Reset();
            }
        }

        private CardEventReport Pop()
        {
            lock (_Locker)
            {
                if (_SendingEvents.Count > 0)
                {
                    return _SendingEvents.Pop();
                }
            }
            return null;
        }

        private void SendMessage(Message msg)
        {
            try
            {
                if (_Communication != null && _Communication.IsConnected())
                {
                    byte[] data = msg.GetMessageBytes();
                    if (data != null && data.Length > 0)
                    {
                        _Communication.SendMessage(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        private void SendParkVacant_Thread()
        {
            while (true)
            {
                ParkBuffer.Current.InValid();
                int totalPort = 0;
                int vacant = 0;
                foreach (ParkInfo park in ParkBuffer.Current.Parks)
                {
                    totalPort += park.TotalPosition;
                    vacant += park.Vacant;
                }
                Message msg = new Message(totalPort, vacant);
                SendMessage(msg);
                Thread.Sleep(60 * 60 * 1000);
            }
        }

        private void CheckConnected_Thread()
        {
            if (_Communication != null)
            {
                while (true)
                {
                    if (!_Communication.IsConnected())
                    {
                        ConnectCommunication();
                    }
                    Thread.Sleep(5 * 1000);
                }
            }
        }

        private void ConnectCommunication()
        {
            _Communication.Connect();
            if (_Communication.IsConnected())
            {
                if (MainForm != null) MainForm.WriteLine(string.Format("{0} 连接数据采集器成功", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }
            else
            {
                if (MainForm != null) MainForm.WriteLine(string.Format("{0} 连接数据采集器失败", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }
        }
        #endregion
    }
}
