﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using Ralid.OpenCard.OpenCardService.ETC.Response;
using Ralid.Park.BusinessModel.Report;

namespace Ralid.OpenCard.OpenCardService.ETC
{
    internal class ETCDevice : IDisposable
    {
        #region 静态函数
        /// <summary>
        /// 获取所有的ETC设备
        /// </summary>
        /// <returns></returns>
        public static ETCDeviceInfo[] GetAllDevices()
        {
            try
            {
                int plen = 100 * 1000;
                StringBuilder pRet = new StringBuilder(plen);
                var n = ETCInterop.Init(pRet, ref plen);
                if (n == 5)
                {
                    var n1 = ETCInterop.GetResponse(ref n, pRet, ref plen);
                }
                var str = pRet.ToString();
                return JsonConvert.DeserializeObject<ETCDeviceInfo[]>(pRet.ToString().Trim());
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 构造函数
        public ETCDevice(ETCDeviceInfo info)
        {
            _DeviceInfo = info;
        }
        #endregion

        #region 私有变量
        private ETCDeviceInfo _DeviceInfo = null;
        private Thread _Thread_RSURead = null;
        private Thread _Thread_ReaderRead = null;
        private Thread _Thread_Ping = null;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置IP地址
        /// </summary>
        public string IPAddr { get { return _DeviceInfo.IPAddr; } }
        /// <summary>
        /// 获取或设置端口号
        /// </summary>
        public string Port { get { return _DeviceInfo.Port; } }
        /// <summary>
        /// 获取或设置超时时间(单位为秒)
        /// </summary>
        public string TimeOut { get { return _DeviceInfo.TimeOut; } }
        /// <summary>
        /// 获取或设置心跳时间(单位为秒)
        /// </summary>
        public string HeartBeatTime { get { return _DeviceInfo.HeartBeatTime; } }
        /// <summary>
        /// 获取或设置用户名称
        /// </summary>
        public string UserName { get { return _DeviceInfo.UserName; } }
        /// <summary>
        /// 获取或设置用户密码
        /// </summary>
        public string Password { get { return _DeviceInfo.Password; } }
        /// <summary>
        /// 获取或设置省份编号
        /// </summary>
        public string ProvinceNo { get { return _DeviceInfo.ProvinceNo; } }
        /// <summary>
        /// 获取或设置城市编号
        /// </summary>
        public string CityNo { get { return _DeviceInfo.CityNo; } }
        /// <summary>
        /// 获取或设置区域编号
        /// </summary>
        public string AreaNo { get { return _DeviceInfo.AreaNo; } }
        /// <summary>
        /// 获取或设置大门编号
        /// </summary>
        public string GateNo { get { return _DeviceInfo.GateNo; } }
        /// <summary>
        /// 获取或设置车道编号
        /// </summary>
        public string LaneNo { get { return _DeviceInfo.LaneNo; } }
        /// <summary>
        /// 获取或设置天线ID
        /// </summary>
        public string RSUID { get { return _DeviceInfo.RSUID; } }
        /// <summary>
        /// 获取或设置读卡器ID
        /// </summary>
        public string ReaderID { get { return _DeviceInfo.ReaderID; } }

        public int EntranceID { get { return _DeviceInfo.EntranceID; } }
        /// <summary>
        /// 获取是否禁用天线
        /// </summary>
        public bool DisableRSU { get { return _DeviceInfo.DisableRSU; } }
        /// <summary>
        /// 获取或设置是否禁用读卡器
        /// </summary>
        public bool DisableReader { get { return _DeviceInfo.DisableReader; } }

        public int State
        {
            get { return _DeviceInfo.State; }
            set
            {
                if (_DeviceInfo.State != value)
                {
                    _DeviceInfo.State = value;
                    if (_DeviceInfo.State == 1)
                    {
                        OpenCardEventArgs args = new OpenCardEventArgs()
                        {
                            Entrance = Ralid.Park.BLL.ParkBuffer.Current.GetEntrance(EntranceID),
                            LastError = "ETC设备断开",
                        };
                        if (this.OnError != null) this.OnError(this, args);
                    }
                    else if (_DeviceInfo.State == 0)
                    {
                        OpenCardEventArgs args = new OpenCardEventArgs()
                        {
                            Entrance = Ralid.Park.BLL.ParkBuffer.Current.GetEntrance(EntranceID),
                            LastError = "ETC设备恢复",
                        };
                        if (this.OnError != null) this.OnError(this, args);
                    }
                }
            }
        }

        public bool ETCCardReaderEnable { get; set; }
        #endregion

        #region 事件
        public event EventHandler<ReadOBUInfoEventArgs> OnReadOBUInfo;

        public event EventHandler<ReadCardInfoEventArgs> OnReadCardInfo;

        public event EventHandler<OpenCardEventArgs> OnError;
        #endregion

        #region 私有方法
        #region 读卡线程
        private void RSURead_Thread()
        {
            string lastCard = null;
            DateTime lastDT = DateTime.Now;
            try
            {
                Thread.Sleep(2000);
                while (true)
                {
                    try
                    {
                        var r = RSURead();
                        if (r.ErrorCode == 0)
                        {
                            var w = r as GetOBUInfoResponse;
                            if (GlobalSettings.Current.Get<Dictionary<int, CardEventReport>>().ContainsKey(EntranceID))
                            {
                                lastCard = GlobalSettings.Current.Get<Dictionary<int, CardEventReport>>()[EntranceID].CardID;
                                lastDT = GlobalSettings.Current.Get<Dictionary<int, CardEventReport>>()[EntranceID].EventDateTime;
                            }
                            if (w.CardNo == lastCard && CalInterval(lastDT, DateTime.Now) < ETCSetting.GetSetting().ReadSameCardInterval) continue; //同一张卡间隔
                            if (this.OnReadOBUInfo != null) this.OnReadOBUInfo(this, new ReadOBUInfoEventArgs() { OBUInfo = w });
                            Thread.Sleep(1000);
                            State = 0;
                        }
                        else
                        {
                            var msg = ErrorDescr(r.ErrorCode);
                            if (!string.IsNullOrEmpty(msg) && this.OnError != null)
                            {
                                OpenCardEventArgs args = new OpenCardEventArgs()
                                {
                                    Entrance = Ralid.Park.BLL.ParkBuffer.Current.GetEntrance(EntranceID),
                                    LastError = msg,
                                };
                                this.OnError(this, args);
                            }
                            Thread.Sleep(500); //如果某一个函数调用失败，则休眠一段时间，避免循环太快
                        }
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(500); //如果某一个函数调用失败，则休眠一段时间，避免循环太快
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        private void ReaderRead_Thread()
        {
            string lastCard = null;
            DateTime lastDT = DateTime.Now;
            try
            {
                Thread.Sleep(2000);
                while (true)
                {
                    try
                    {
                        var r = ReaderRead();
                        if (r != null && r.ErrorCode == 0)
                        {
                            var w = r as GetCardInfoResponse;
                            if (GlobalSettings.Current.Get<Dictionary<int, CardEventReport>>().ContainsKey(EntranceID))
                            {
                                lastCard = GlobalSettings.Current.Get<Dictionary<int, CardEventReport>>()[EntranceID].CardID;
                                lastDT = GlobalSettings.Current.Get<Dictionary<int, CardEventReport>>()[EntranceID].EventDateTime;
                            }
                            if (w.CardNo == lastCard && CalInterval(lastDT, DateTime.Now) < ETCSetting.GetSetting().ReadSameCardInterval) continue;
                            if (this.OnReadCardInfo != null) this.OnReadCardInfo(this, new ReadCardInfoEventArgs() { CardInfo = w });
                            Thread.Sleep(1000);
                            State = 0;
                        }
                        else
                        {
                            var msg = ErrorDescr(r.ErrorCode);
                            if (!string.IsNullOrEmpty(msg) && this.OnError != null)
                            {
                                OpenCardEventArgs args = new OpenCardEventArgs()
                                {
                                    Entrance = Ralid.Park.BLL.ParkBuffer.Current.GetEntrance(EntranceID),
                                    LastError = msg,
                                };
                                this.OnError(this, args);
                            }
                            Thread.Sleep(500);  //如果某一个函数调用失败，则休眠一段时间，避免循环太快
                        }
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(500); //如果某一个函数调用失败，则休眠一段时间，避免循环太快
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        private void HeartBeat_Thread()
        {
            int count = 0;
            var ping = new Ping();
            try
            {
                while (true)
                {
                    Thread.Sleep(2000);
                    try
                    {
                        var response = ping.Send(System.Net.IPAddress.Parse(IPAddr), 1000);
                        if (response.Status == IPStatus.Success)
                        {
                            count = 0;
                        }
                        else
                        {
                            count++;
                        }
                        if (count == 5) State = 1;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        private double CalInterval(DateTime dt1, DateTime dt2)
        {
            TimeSpan ts = new TimeSpan(dt2.Ticks - dt1.Ticks);
            return ts.TotalSeconds;
        }

        private string ErrorDescr(int errorCode)
        {
            if (errorCode == -1300) return "天线未打开";
            else if (errorCode == -1301) return "天线打开串口失败";
            else if (errorCode == -1302) return "天线调整功率失败";
            else if (errorCode == -2300) return "读卡器未开启";
            else if (errorCode == -2301) return "读卡器初始化失败";
            return null;
        }
        #endregion

        #region 连接设备相关的函数
        #endregion

        #region 天线操作相关的函数
        private GetOBUInfoResponse RSURead()
        {
            int plen = 3000;
            StringBuilder response = new StringBuilder(plen);
            int n = -1;
            n = ETCInterop.RSURead(int.Parse(LaneNo), JsonConvert.SerializeObject(new { TimeOut = "2000" }), response, ref plen);
            if (n != 0)
            {
                System.Threading.Thread.Sleep(1000);
                return new GetOBUInfoResponse() { ErrorCode = n };
            }
            var ret = JsonConvert.DeserializeObject<GetOBUInfoResponse>(response.ToString());
            ret.Content = response.ToString();
            return ret;
        }

        /// <summary>
        /// 进行天线扣款操作
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public WriteCardResponse RSUWrite(GetOBUInfoResponse r, int money, bool isExit, out ETCPaymentList record)
        {
            int n = -1;
            record = null;
            WriteCardResponse ret = null;
            int plen = 3000;
            StringBuilder response = new StringBuilder(plen);
            var request = new
            {
                OBUID = r.OBUID,
                OBUNO = r.OBUNO,
                CardNo = r.CardNo,
                CashMoney = money.ToString(), //如果不改成字符串格式，会报错
                CardAreaNo = AreaNo,
                CardGateNo = GateNo.PadLeft(4, '0'),
                CardLaneNo = LaneNo.PadLeft(4, '0'),
                PassTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                VehPlate = r.CardPlate,
                VehType = r.CardVehClass,
                VehClass = r.CardVehUserType,
                OutFlag = isExit ? "1" : "0",
                OperatorNo = "ffff",
                LittleGateNo = r.LittleGateNo,
                LittleLaneNo = r.LittleLaneNo,
                LittlePassTime = r.LittlePassTime,
                LittleCashMoney = r.LittleCashMoney,
                LittleTime = r.LittleTime,
                OfferType = r.OfferType,
                OfferTime = r.OfferTime,
                BackUp = r.BackUp == null ? string.Empty : r.BackUp, //不能传NULL,
                CheckCode = r.CheckCode,
            };
            n = ETCInterop.RSUWrite(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response, ref plen);
            if (n != 0) return new WriteCardResponse() { ErrorCode = n };
            ret = JsonConvert.DeserializeObject<WriteCardResponse>(response.ToString());
            ret.Content = response.ToString();
            if (ret.ErrorCode == 0)
            {
                ret.CashMoney = money; //这两个属性返回串中没有，人为加上去，后面有用！
                ret.Balance = r.Balance - money;
                record = CreateRecord(r, ret, isExit);
            }
            return ret;
        }
        #endregion

        #region 读卡器操作相关的函数
        private GetCardInfoResponse ReaderRead()
        {
            int n = -1;
            int plen = 3000;
            StringBuilder response = new StringBuilder(plen);
            n = ETCInterop.ReaderRead(int.Parse(LaneNo), JsonConvert.SerializeObject(new { TimeOut = "2000" }), response, ref plen);
            if (n != 0)
            {
                System.Threading.Thread.Sleep(1000);
                return new GetCardInfoResponse() { ErrorCode = n };
            }
            var ret = JsonConvert.DeserializeObject<GetCardInfoResponse>(response.ToString());
            ret.Content = response.ToString();
            return ret;
        }

        /// <summary>
        /// 进行读卡扣款操作
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public WriteCardResponse ReaderWrite(GetCardInfoResponse r, int money, bool isExit, out ETCPaymentList record)
        {
            int n = -1;
            record = null;
            WriteCardResponse ret = null;
            int plen = 3000;
            StringBuilder response = new StringBuilder(plen);
            var request = new
            {
                CardNo = r.CardNo,
                CashMoney = money.ToString(),
                CardAreaNo = AreaNo,
                CardGateNo = GateNo.PadLeft(4, '0'),
                CardLaneNo = LaneNo.PadLeft(4, '0'),
                PassTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                VehPlate = r.CardPlate,
                VehType = r.CardVehClass,
                VehClass = r.CardVehUserType,
                OutFlag = isExit ? "1" : "0",
                OperatorNo = "ffff",
                LittleGateNo = r.LittleGateNo,
                LittleLaneNo = r.LittleLaneNo,
                LittlePassTime = r.LittlePassTime,
                LittleCashMoney = r.LittleCashMoney,
                LittleTime = r.LittleTime,
                OfferType = r.OfferType,
                OfferTime = r.OfferTime,
                BackUp = r.BackUp == null ? string.Empty : r.BackUp, //不能传NULL
                CheckCode = r.CheckCode,
            };
            n = ETCInterop.ReaderWrite(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response, ref plen);
            if (n != 0) return new WriteCardResponse() { ErrorCode = n };
            ret = JsonConvert.DeserializeObject<WriteCardResponse>(response.ToString());
            ret.Content = response.ToString();
            if (ret.ErrorCode == 0)
            {
                ret.CashMoney = money; //这两个属性返回串中没有，人为加上去，后面有用！
                ret.Balance = r.Balance - money;
                record = CreateRecord(r, ret, isExit);
            }
            return ret;
        }
        #endregion

        #region 收费流水相关的操作
        private ETCPaymentList CreateRecord(GetOBUInfoResponse obuInfo, WriteCardResponse writeInfo, bool isExit)
        {
            var ret = new ETCPaymentList()
            {
                ListType = isExit ? 1 : 0,
                ListNo = string.Format("{0}{1}{2}{3}{4}{5}{6}", ProvinceNo, CityNo, AreaNo, GateNo, LaneNo, DateTime.Now.ToString("yyyyMMddHHmmss"), "00"),
                KeyServiceNo = writeInfo.KeyServiceNo,
                TradeType = writeInfo.TradeType,
                TermCode = writeInfo.TermCode,
                TermTradeNo = writeInfo.TermTradeNo,
                CardTradeNo = writeInfo.CardTradeNo,
                Tac = writeInfo.Tac,
                OBUID = obuInfo.OBUID,
                OBUNO = obuInfo.OBUNO,
                CardNo = obuInfo.CardNo,
                CashMoney = writeInfo.CashMoney,
                Balance = writeInfo.Balance,
                TradeDevice = 0,
                VehPicture = null,
                VehPictureLen = 0,
                SquadDate = DateTime.Today.ToString("yyyyMMdd"),
                ShiftID = DateTime.Today.ToString("yyMMdd"),
                ExTime = isExit ? DateTime.Now.ToString("yyyyMMddHHmmss") : null,
                ExAreaNo = isExit ? AreaNo : null,
                ExGateNo = isExit ? GateNo : null,
                ExLaneNo = isExit ? LaneNo : null,
                ExOperatorNo = isExit ? "ffff" : null,
                ExVehPlate = isExit ? obuInfo.CardPlate : null,
                ExVehType = isExit ? obuInfo.CardVehClass : "255",
                ExVehClass = isExit ? obuInfo.CardVehUserType : "255",
                EnTime = isExit ? obuInfo.PassTime : DateTime.Now.ToString("yyyyMMddHHmmss"),
                EnOperatorNo = isExit ? obuInfo.OperatorNo : "ffff",
                EnAreaNo = isExit ? obuInfo.CardAreaNo : AreaNo,
                EnGateNo = isExit ? obuInfo.CardGateNo : GateNo,
                EnLaneNo = isExit ? obuInfo.CardLaneNo : LaneNo,
                EnVehPlate = isExit ? obuInfo.VehPlate : obuInfo.CardPlate,
                EnVehType = isExit ? obuInfo.VehType : obuInfo.CardVehClass,
                EnVehClass = isExit ? obuInfo.VehClass : obuInfo.CardVehUserType,
            };
            if (!string.IsNullOrEmpty(ret.ExGateNo) && ret.ExGateNo.Length > 2) ret.ExGateNo = ret.ExGateNo.Substring(2);
            if (!string.IsNullOrEmpty(ret.ExLaneNo) && ret.ExLaneNo.Length > 2) ret.ExLaneNo = ret.ExLaneNo.Substring(2);
            if (!string.IsNullOrEmpty(ret.EnGateNo) && ret.EnGateNo.Length > 2) ret.EnGateNo = ret.EnGateNo.Substring(2);
            if (!string.IsNullOrEmpty(ret.EnLaneNo) && ret.EnLaneNo.Length > 2) ret.EnLaneNo = ret.EnLaneNo.Substring(2);
            return ret;
        }

        private ETCPaymentList CreateRecord(GetCardInfoResponse cardInfo, WriteCardResponse writeInfo, bool isExit)
        {
            var ret = new ETCPaymentList()
            {
                ListType = isExit ? 1 : 0,
                ListNo = string.Format("{0}{1}{2}{3}{4}{5}{6}", ProvinceNo, CityNo, AreaNo, GateNo, LaneNo, DateTime.Now.ToString("yyyyMMddHHmmss"), "00"),
                KeyServiceNo = writeInfo.KeyServiceNo,
                TradeType = writeInfo.TradeType,
                TermCode = writeInfo.TermCode,
                TermTradeNo = writeInfo.TermTradeNo,
                CardTradeNo = writeInfo.CardTradeNo,
                Tac = writeInfo.Tac,
                OBUID = null,
                OBUNO = null,
                CardNo = cardInfo.CardNo,
                CashMoney = writeInfo.CashMoney,
                Balance = writeInfo.Balance,
                TradeDevice = 1,
                VehPicture = null,
                VehPictureLen = 0,
                SquadDate = DateTime.Today.ToString("yyyyMMdd"),
                ShiftID = DateTime.Today.ToString("yyMMdd"),
                ExTime = isExit ? DateTime.Now.ToString("yyyyMMddHHmmss") : null,
                ExAreaNo = isExit ? AreaNo : null,
                ExGateNo = isExit ? GateNo : null,
                ExLaneNo = isExit ? LaneNo : null,
                ExOperatorNo = isExit ? "ffff" : null,
                ExVehPlate = isExit ? cardInfo.CardPlate : null,
                ExVehType = isExit ? cardInfo.CardVehClass : "255",
                ExVehClass = isExit ? cardInfo.CardVehUserType : "255",
                EnTime = isExit ? cardInfo.PassTime : DateTime.Now.ToString("yyyyMMddHHmmss"),
                EnOperatorNo = isExit ? cardInfo.OperatorNo : "000001",
                EnAreaNo = isExit ? cardInfo.CardAreaNo : AreaNo,
                EnGateNo = isExit ? cardInfo.CardGateNo : GateNo,
                EnLaneNo = isExit ? cardInfo.CardLaneNo : LaneNo,
                EnVehPlate = isExit ? cardInfo.VehPlate : cardInfo.CardPlate,
                EnVehType = isExit ? cardInfo.VehType : cardInfo.CardVehClass,
                EnVehClass = isExit ? cardInfo.VehClass : cardInfo.CardVehUserType,
            };
            if (!string.IsNullOrEmpty(ret.ExGateNo) && ret.ExGateNo.Length > 2) ret.ExGateNo = ret.ExGateNo.Substring(2);
            if (!string.IsNullOrEmpty(ret.ExLaneNo) && ret.ExLaneNo.Length > 2) ret.ExLaneNo = ret.ExLaneNo.Substring(2);
            if (!string.IsNullOrEmpty(ret.EnGateNo) && ret.EnGateNo.Length > 2) ret.EnGateNo = ret.EnGateNo.Substring(2);
            if (!string.IsNullOrEmpty(ret.EnLaneNo) && ret.EnLaneNo.Length > 2) ret.EnLaneNo = ret.EnLaneNo.Substring(2);
            return ret;
        }

        /// <summary>
        /// 上传扣费流水
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public ETCResponse ListUpLoad(ETCPaymentList record)
        {
            int plen = 1000;
            StringBuilder response = new StringBuilder(plen);
            var request = new
            {
                ListType = record.ListType.ToString(),
                ListNo = record.ListNo,
                KeyServiceNo = record.KeyServiceNo,
                TradeType = record.TradeType.ToString(),
                TermCode = record.TermCode,
                TermTradeNo = record.TermTradeNo,
                CardTradeNo = record.CardTradeNo,
                Tac = record.Tac,
                OBUID = record.OBUID == null ? string.Empty : record.OBUID,
                OBUNO = record.OBUNO == null ? string.Empty : record.OBUNO,
                CardNo = record.CardNo,
                CashMoney = record.CashMoney.ToString(),
                Balance = record.Balance.ToString(),
                TradeDevice = record.TradeDevice.ToString(),
                VehPicture = record.VehPicture,
                VehPictureLen = record.VehPictureLen.ToString(),
                SquadDate = record.SquadDate,
                ShiftID = record.ShiftID,
                ExTime = record.ExTime,
                ExAreaNo = record.ExAreaNo,
                ExGateNo = record.ExGateNo,
                ExLaneNo = record.ExLaneNo,
                ExOperatorNo = record.ExOperatorNo,
                ExVehPlate = record.ExVehPlate,
                ExVehType = record.ExVehType,
                ExVehClass = record.ExVehClass,
                EnTime = record.EnTime,
                EnOperatorNo = record.EnOperatorNo,
                EnAreaNo = record.EnAreaNo,
                EnGateNo = record.EnGateNo,
                EnLaneNo = record.EnLaneNo,
                EnVehPlate = record.EnVehPlate,
                EnVehType = record.EnVehType,
                EnVehClass = record.EnVehClass,
            };
            var strReq = JsonConvert.SerializeObject(request);
            var n = ETCInterop.ListUpLoad(int.Parse(LaneNo), strReq, response, ref plen);
            if (n != 0)
            {
                Ralid.GeneralLibrary.LOG.FileLog.Log("未成功上传流水" + LaneNo.ToString(), strReq);
                return new ETCResponse() { ErrorCode = n };
            }
            var ret = JsonConvert.DeserializeObject<WriteCardResponse>(response.ToString());
            ret.Content = response.ToString();
            return ret;
        }
        #endregion
        #endregion

        #region 公共方法
        public void Init()
        {
            if (_Thread_RSURead == null && !DisableRSU)
            {
                _Thread_RSURead = new Thread(new ThreadStart(RSURead_Thread));
                _Thread_RSURead.Start();
            }
            if (_Thread_ReaderRead == null && !DisableReader)
            {
                _Thread_ReaderRead = new Thread(new ThreadStart(ReaderRead_Thread));
                _Thread_ReaderRead.Start();
            }
            if (_Thread_Ping == null)
            {
                _Thread_Ping = new Thread(new ThreadStart(HeartBeat_Thread));
                _Thread_Ping.Start();
            }
        }

        public void Dispose()
        {
            if (_Thread_RSURead != null)
            {
                _Thread_RSURead.Abort();
                _Thread_RSURead = null;
            }
            if (_Thread_ReaderRead != null)
            {
                _Thread_ReaderRead.Abort();
                _Thread_ReaderRead = null;
            }
            if (_Thread_Ping != null)
            {
                _Thread_Ping.Abort();
                _Thread_Ping = null;
            }
        }
        #endregion
    }
}
