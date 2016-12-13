using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Ralid.OpenCard.OpenCardService.ETC.Response;

namespace Ralid.OpenCard.OpenCardService.ETC
{
    public class ETCDevice : IDisposable
    {
        #region 构造函数
        public ETCDevice()
        {
        }
        #endregion

        #region 私有变量
        private Thread _Thread_RSURead = null;
        private Thread _Thread_ReaderRead = null;
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置IP地址
        /// </summary>
        public string IPAddr { get; set; }
        /// <summary>
        /// 获取或设置端口号
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// 获取或设置超时时间(单位为秒)
        /// </summary>
        public string TimeOut { get; set; }
        /// <summary>
        /// 获取或设置心跳时间(单位为秒)
        /// </summary>
        public string HeartBeatTime { get; set; }
        /// <summary>
        /// 获取或设置用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 获取或设置用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 获取或设置省份编号
        /// </summary>
        public string ProvinceNo { get; set; }
        /// <summary>
        /// 获取或设置城市编号
        /// </summary>
        public string CityNo { get; set; }
        /// <summary>
        /// 获取或设置区域编号
        /// </summary>
        public string AreaNo { get; set; }
        /// <summary>
        /// 获取或设置大门编号
        /// </summary>
        public string GateNo { get; set; }
        /// <summary>
        /// 获取或设置车道编号
        /// </summary>
        public string LaneNo { get; set; }
        /// <summary>
        /// 获取或设置天线ID
        /// </summary>
        public string EcRSUID { get; set; }
        /// <summary>
        /// 获取或设置读卡器ID
        /// </summary>
        public string EcReaderID { get; set; }
        /// <summary>
        /// 获取或设置是否不启用天线
        /// </summary>
        public bool RSUDisable { get; set; }
        /// <summary>
        /// 获取或设置是否不启用读卡器
        /// </summary>
        public bool ReaderDisable { get; set; }
        /// <summary>
        /// 获取或设置是否在广东省内使用此设备
        /// </summary>
        public bool UseInGD
        {
            get { return ProvinceNo == "44"; }
        }
        /// <summary>
        /// 是否是出口设备，否则是入口设备
        /// </summary>
        public bool IsExit { get; set; }
        #endregion

        #region 私有方法
        private void RSURead_Thread()
        {
            try
            {
                Thread.Sleep(2000);
                while (true)
                {
                    try
                    {

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        private void ReaderRead_Thread()
        {
            try
            {
                Thread.Sleep(2000);
                while (true)
                {
                    try
                    {



                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        #region 连接设备相关的函数
        private int Connect()
        {
            var r = ETCInterop.connectserver(int.Parse(LaneNo), IPAddr, int.Parse(Port));
            if (r == 0)
            {
                StringBuilder response = new StringBuilder(100);
                var request = new
                {
                    UserName = UserName,
                    PassWord = Password,
                    ProvinceNo = ProvinceNo,
                    CityNo = CityNo,
                    AreaNo = AreaNo,
                    GateNo = GateNo,
                    LaneNo = LaneNo
                };
                ETCInterop.LaneLogin(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response);
                var ret = JsonConvert.DeserializeObject<ETCResponse>(response.ToString());
                ret.Content = response.ToString();
                r = ret.ErrorCode;
            }
            return r;
        }

        private void Disconnect()
        {
            StringBuilder response = new StringBuilder(3000);
            var request = new
            {
                UserName = UserName,
                ProvinceNo = ProvinceNo,
                CityNo = CityNo,
                AreaNo = AreaNo,
                GateNo = GateNo,
                LaneNo = LaneNo
            };
            ETCInterop.LaneQuit(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response);
            ETCInterop.quitserver(int.Parse(LaneNo));
        }
        #endregion

        #region 天线操作相关的函数
        private ETCResponse RSUOpen()
        {
            StringBuilder response = new StringBuilder(100);
            var n = ETCInterop.RSUOpen(int.Parse(LaneNo), JsonConvert.SerializeObject(new { UserName = UserName, RSUID = EcRSUID }), response);
            if (n != 0) return new ETCResponse() { ErrorCode = n };
            var ret = JsonConvert.DeserializeObject<ETCResponse>(response.ToString());
            ret.Content = response.ToString();
            return ret;
        }

        private ETCResponse RSUClose()
        {
            StringBuilder response = new StringBuilder(100);
            var n = ETCInterop.RSUClose(int.Parse(LaneNo), JsonConvert.SerializeObject(new { UserName = UserName, RSUID = EcRSUID }), response);
            if (n != 0) return new ETCResponse() { ErrorCode = n };
            var ret = JsonConvert.DeserializeObject<ETCResponse>(response.ToString());
            ret.Content = response.ToString();
            return ret;
        }

        private OBUSearchResponse OBUSearch()
        {
            StringBuilder response = new StringBuilder(100);
            var n = ETCInterop.OBUSearch(int.Parse(LaneNo), JsonConvert.SerializeObject(new { UserName = UserName, RSUID = EcRSUID, TimeOut = "2000" }), response);
            if (n != 0) return new OBUSearchResponse { ErrorCode = n };
            var ret = JsonConvert.DeserializeObject<OBUSearchResponse>(response.ToString());
            ret.Content = response.ToString();
            return ret;
        }

        private GetOBUInfoResponse GetOBUInfo(OBUSearchResponse r)
        {
            StringBuilder response = new StringBuilder(3000);
            var request = new
            {
                UserName = UserName,
                ProvinceNo = ProvinceNo,
                CityNo = CityNo,
                AreaNo = AreaNo,
                GateNo = GateNo,
                LaneNo = LaneNo,
                RSUID = EcRSUID,
                OBUID = r.OBUID,
                OBUNO = r.OBUNO
            };
            int n = -1;
            if (UseInGD) n = ETCInterop.GetOBUInfo_GD(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response); //广东接口
            else n = ETCInterop.GetOBUInfo(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response);
            if (n != 0) return new GetOBUInfoResponse() { ErrorCode = n };
            var ret = JsonConvert.DeserializeObject<GetOBUInfoResponse>(response.ToString());
            ret.Content = response.ToString();
            ret.OBUID = r.OBUID; //这两个属性返回串中没有，是手动设置上去的，后面用得到，一定需要
            ret.OBUNO = r.OBUNO;
            return ret;
        }

        private WriteCardResponse RSUWriteCard(GetOBUInfoResponse r, int money, out ETCPaymentRecord record)
        {
            int n = -1;
            record = null;
            WriteCardResponse ret = null;
            StringBuilder response = new StringBuilder(3000);
            if (UseInGD)
            {
                var request = new
                {
                    UserName = UserName,
                    Password = Password,
                    ProvinceNo = ProvinceNo,
                    CityNo = CityNo,
                    AreaNo = AreaNo,
                    GateNo = GateNo,
                    LaneNo = LaneNo,
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
                    OutFlag = IsExit ? "1" : "0",
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
                    RSUID = EcRSUID
                };
                n = ETCInterop.RSUWriteCard_GD(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response);
                if (n != 0) return new WriteCardResponse() { ErrorCode = n };
                ret = JsonConvert.DeserializeObject<WriteCardResponse>(response.ToString());
                ret.Content = response.ToString();
            }
            else
            {
                var request = new
                {
                    UserName = UserName,
                    Password = Password,
                    ProvinceNo = ProvinceNo,
                    CityNo = CityNo,
                    AreaNo = AreaNo,
                    GateNo = GateNo,
                    LaneNo = LaneNo,
                    OBUID = r.OBUID,
                    OBUNO = r.OBUNO,
                    CardNo = r.CardNo,
                    CashMoney = money.ToString(),
                    CardAreaNo = AreaNo,
                    CardGateNo = GateNo.PadLeft(4, '0'),
                    CardLaneNo = LaneNo.PadLeft(4, '0'),
                    PassTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    VehPlate = r.CardPlate,
                    VehType = r.CardVehClass,
                    VehClass = r.CardVehUserType,
                    OutFlag = IsExit ? "1" : "0",
                    OperatorNo = "ffff",
                    RSUID = EcRSUID
                };
                n = ETCInterop.RSUWriteCard(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response);
                if (n != 0) return new WriteCardResponse() { ErrorCode = n };
                ret = JsonConvert.DeserializeObject<WriteCardResponse>(response.ToString());
                ret.Content = response.ToString();
            }
            if (ret.ErrorCode == -1328) //半条记录的情况，需要验证
            {
                response = new StringBuilder(1000); //重新创建一个，用来接收消息
                var request = new
                {
                    UserName = UserName,
                    ProvinceNo = ProvinceNo,
                    CityNo = CityNo,
                    AreaNo = AreaNo,
                    GateNo = GateNo,
                    LaneNo = LaneNo,
                    OBUID = r.OBUID,
                    OBUNO = r.OBUNO,
                    CardNo = r.CardNo,
                    RelyServiceNo = ret.KeyServiceNo,
                    RSUID = EcRSUID
                };
                n = ETCInterop.RSUTransActionProve(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response);
                if (n != 0) return new WriteCardResponse() { ErrorCode = n };
                ret = JsonConvert.DeserializeObject<WriteCardResponse>(response.ToString());
                ret.Content = response.ToString();
            }
            if (ret.ErrorCode == 0)
            {
                ret.CashMoney = money; //这两个属性返回串中没有，人为加上去，后面有用！
                ret.Balance = r.Balance - money;
                record = CreateRecord(r, ret);
            }
            return ret;
        }
        #endregion

        #region 读卡器操作相关的函数
        private ETCResponse CardReaderOpen()
        {
            StringBuilder response = new StringBuilder(100);
            var n = ETCInterop.CardReaderOpen(int.Parse(LaneNo), JsonConvert.SerializeObject(new { UserName = UserName, ReaderID = EcReaderID }), response);
            if (n != 0) return new ETCResponse() { ErrorCode = n };
            var ret = JsonConvert.DeserializeObject<ETCResponse>(response.ToString());
            ret.Content = response.ToString();
            return ret;
        }

        private ETCResponse CardReaderOpenClose()
        {
            StringBuilder response = new StringBuilder(100);
            var n = ETCInterop.CardReaderClose(int.Parse(LaneNo), JsonConvert.SerializeObject(new { UserName = UserName, ReaderID = EcReaderID }), response);
            if (n != 0) return new ETCResponse() { ErrorCode = n };
            var ret = JsonConvert.DeserializeObject<ETCResponse>(response.ToString());
            ret.Content = response.ToString();
            return ret;
        }

        private CardSearchResponse CardSearch()
        {
            StringBuilder response = new StringBuilder(100);
            var n = ETCInterop.CardSearch(int.Parse(LaneNo), JsonConvert.SerializeObject(new { UserName = UserName, ReaderID = EcReaderID, TimeOut = "2000" }), response);
            if (n != 0) return new CardSearchResponse() { ErrorCode = n };
            var ret = JsonConvert.DeserializeObject<CardSearchResponse>(response.ToString());
            ret.Content = response.ToString();
            return ret;
        }

        private GetCardInfoResponse GetCardInfoFromReader(CardSearchResponse r)
        {
            int n = -1;
            StringBuilder response = new StringBuilder(3000);
            if (UseInGD)
            {
                n = ETCInterop.GetCardInfo_GD(int.Parse(LaneNo), JsonConvert.SerializeObject(new { UserName = UserName, CardNo = r.CardNo, ReaderID = EcReaderID }), response);
            }
            else
            {
                n = ETCInterop.GetCardInfo(int.Parse(LaneNo), JsonConvert.SerializeObject(new { UserName = UserName, CardNo = r.CardNo, ReaderID = EcReaderID }), response);
            }
            if (n != 0) return new GetCardInfoResponse() { ErrorCode = n };
            var ret = JsonConvert.DeserializeObject<GetCardInfoResponse>(response.ToString());
            ret.Content = response.ToString();
            ret.CardNo = r.CardNo; //由于返回的串中没有这个属性，所以这里设置进去
            return ret;
        }

        private WriteCardResponse CardReaderWriteCard(GetCardInfoResponse r, int money, out ETCPaymentRecord record)
        {
            int n = -1;
            record = null;
            WriteCardResponse ret = null;
            StringBuilder response = new StringBuilder(3000);
            if (UseInGD)
            {
                var request = new
                {
                    UserName = UserName,
                    PassWord = Password,
                    ProvinceNo = ProvinceNo,
                    CityNo = CityNo,
                    AreaNo = AreaNo,
                    GateNo = GateNo,
                    LaneNo = LaneNo,
                    CardNo = r.CardNo,
                    CashMoney = money.ToString(),
                    CardAreaNo = AreaNo,
                    CardGateNo = GateNo.PadLeft(4, '0'),
                    CardLaneNo = LaneNo.PadLeft(4, '0'),
                    PassTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    VehPlate = r.CardPlate,
                    VehType = r.CardVehClass,
                    VehClass = r.CardVehUserType,
                    OutFlag = IsExit ? "1" : "0",
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
                    ReaderID = EcReaderID
                };
                n = ETCInterop.CardReaderWriteCard_GD(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response);
                if (n != 0) return new WriteCardResponse() { ErrorCode = n };
                ret = JsonConvert.DeserializeObject<WriteCardResponse>(response.ToString());
                ret.Content = response.ToString();
            }
            else
            {
                var request = new
                {
                    UserName = UserName,
                    PassWord = Password,
                    ProvinceNo = ProvinceNo,
                    CityNo = CityNo,
                    AreaNo = AreaNo,
                    GateNo = GateNo,
                    LaneNo = LaneNo,
                    CardNo = r.CardNo,
                    CashMoney = money,
                    CardAreaNo = AreaNo,
                    CardGateNo = GateNo.PadLeft(4, '0'),
                    CardLaneNo = LaneNo.PadLeft(4, '0'),
                    PassTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    VehPlate = r.CardPlate,
                    VehType = r.CardVehClass,
                    VehClass = r.CardVehUserType,
                    OutFlag = IsExit ? "1" : "0",
                    OperatorNo = "ffff",
                    ReaderID = EcReaderID
                };
                n = ETCInterop.CardReaderWriteCard(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response);
                if (n != 0) return new WriteCardResponse() { ErrorCode = n };
                ret = JsonConvert.DeserializeObject<WriteCardResponse>(response.ToString());
                ret.Content = response.ToString();
            }
            if (ret.ErrorCode == -2320) //半条记录的情况，需要验证
            {
                response = new StringBuilder(1000);
                var request = new
                {
                    UserName = UserName,
                    ProvinceNo = ProvinceNo,
                    CityNo = CityNo,
                    AreaNo = AreaNo,
                    GateNo = GateNo,
                    LaneNo = LaneNo,
                    CardNo = r.CardNo,
                    RelyServiceNo = ret.KeyServiceNo,
                    ReaderID = EcReaderID
                };
                n = ETCInterop.CardReaderTransActionProve(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response);
                if (n != 0) return new WriteCardResponse { ErrorCode = n };
                ret = JsonConvert.DeserializeObject<WriteCardResponse>(response.ToString());
                ret.Content = response.ToString();
            }
            if (ret.ErrorCode == 0)
            {
                ret.CashMoney = money; //这两个属性返回串中没有，人为加上去，后面有用！
                ret.Balance = r.Balance - money;
                record = CreateRecord(r, ret);
            }
            return ret;
        }
        #endregion

        #region 收费流水相关的操作
        private ETCPaymentRecord CreateRecord(GetOBUInfoResponse obuInfo, WriteCardResponse writeInfo)
        {
            var ret = new ETCPaymentRecord()
            {
                ListType = IsExit ? 1 : 0,
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
                ExTime = IsExit ? DateTime.Now.ToString("yyyyMMddHHmmss") : null,
                ExAreaNo = IsExit ? AreaNo : null,
                ExGateNo = IsExit ? GateNo : null,
                ExLaneNo = IsExit ? LaneNo : null,
                ExOperatorNo = IsExit ? "ffff" : null,
                ExVehPlate = IsExit ? obuInfo.CardPlate : null,
                ExVehType = IsExit ? obuInfo.CardVehClass : "255",
                ExVehClass = IsExit ? obuInfo.CardVehUserType : "255",
                EnTime = IsExit ? obuInfo.PassTime : DateTime.Now.ToString("yyyyMMddHHmmss"),
                EnOperatorNo = IsExit ? obuInfo.OperatorNo : "ffff",
                EnAreaNo = IsExit ? obuInfo.CardAreaNo : AreaNo,
                EnGateNo = IsExit ? obuInfo.CardGateNo : GateNo,
                EnLaneNo = IsExit ? obuInfo.CardLaneNo : LaneNo,
                EnVehPlate = IsExit ? obuInfo.VehPlate : obuInfo.CardPlate,
                EnVehType = IsExit ? obuInfo.VehType : obuInfo.CardVehClass,
                EnVehClass = IsExit ? obuInfo.VehClass : obuInfo.CardVehUserType,
            };
            if (!string.IsNullOrEmpty(ret.ExGateNo) && ret.ExGateNo.Length > 2) ret.ExGateNo = ret.ExGateNo.Substring(2);
            if (!string.IsNullOrEmpty(ret.ExLaneNo) && ret.ExLaneNo.Length > 2) ret.ExLaneNo = ret.ExLaneNo.Substring(2);
            if (!string.IsNullOrEmpty(ret.EnGateNo) && ret.EnGateNo.Length > 2) ret.EnGateNo = ret.EnGateNo.Substring(2);
            if (!string.IsNullOrEmpty(ret.EnLaneNo) && ret.EnLaneNo.Length > 2) ret.EnLaneNo = ret.EnLaneNo.Substring(2);
            return ret;
        }

        private ETCPaymentRecord CreateRecord(GetCardInfoResponse cardInfo, WriteCardResponse writeInfo)
        {
            var ret = new ETCPaymentRecord()
            {
                ListType = IsExit ? 1 : 0,
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
                ExTime = IsExit ? DateTime.Now.ToString("yyyyMMddHHmmss") : null,
                ExAreaNo = IsExit ? AreaNo : null,
                ExGateNo = IsExit ? GateNo : null,
                ExLaneNo = IsExit ? LaneNo : null,
                ExOperatorNo = IsExit ? "ffff" : null,
                ExVehPlate = IsExit ? cardInfo.CardPlate : null,
                ExVehType = IsExit ? cardInfo.CardVehClass : "255",
                ExVehClass = IsExit ? cardInfo.CardVehUserType : "255",
                EnTime = IsExit ? cardInfo.PassTime : DateTime.Now.ToString("yyyyMMddHHmmss"),
                EnOperatorNo = IsExit ? cardInfo.OperatorNo : "000001",
                EnAreaNo = IsExit ? cardInfo.CardAreaNo : AreaNo,
                EnGateNo = IsExit ? cardInfo.CardGateNo : GateNo,
                EnLaneNo = IsExit ? cardInfo.CardLaneNo : LaneNo,
                EnVehPlate = IsExit ? cardInfo.VehPlate : cardInfo.CardPlate,
                EnVehType = IsExit ? cardInfo.VehType : cardInfo.CardVehClass,
                EnVehClass = IsExit ? cardInfo.VehClass : cardInfo.CardVehUserType,
            };
            if (!string.IsNullOrEmpty(ret.ExGateNo) && ret.ExGateNo.Length > 2) ret.ExGateNo = ret.ExGateNo.Substring(2);
            if (!string.IsNullOrEmpty(ret.ExLaneNo) && ret.ExLaneNo.Length > 2) ret.ExLaneNo = ret.ExLaneNo.Substring(2);
            if (!string.IsNullOrEmpty(ret.EnGateNo) && ret.EnGateNo.Length > 2) ret.EnGateNo = ret.EnGateNo.Substring(2);
            if (!string.IsNullOrEmpty(ret.EnLaneNo) && ret.EnLaneNo.Length > 2) ret.EnLaneNo = ret.EnLaneNo.Substring(2);
            return ret;
        }

        private ETCResponse ListUpLoad(ETCPaymentRecord record)
        {
            StringBuilder response = new StringBuilder(100);
            var request = new
            {
                UserName = UserName,
                ProvinceNo = ProvinceNo,
                CityNo = CityNo,
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
            var n = ETCInterop.ListUpLoad(int.Parse(LaneNo), JsonConvert.SerializeObject(request), response);
            if (n != 0) return new ETCResponse() { ErrorCode = n };
            var ret = JsonConvert.DeserializeObject<WriteCardResponse>(response.ToString());
            ret.Content = response.ToString();
            return ret;
        }
        #endregion
        #endregion

        #region 公共方法
        public void Init()
        {
            //if (!RSUDisable)
            //{
            //    if (_Thread_RSURead == null)
            //    {
            //        _Thread_RSURead = new Thread(new ThreadStart(RSURead_Thread));
            //        _Thread_RSURead.Start();
            //    }
            //}
            //else
            //{
            //    if (_Thread_RSURead != null)
            //    {
            //        _Thread_RSURead.Abort();
            //        _Thread_RSURead = null;
            //    }
            //}
            //if (!ReaderDisable)
            //{
            //    if (_Thread_ReaderRead == null)
            //    {
            //        _Thread_ReaderRead = new Thread(new ThreadStart(ReaderRead_Thread));
            //        _Thread_ReaderRead.Start();
            //    }
            //}
            //else
            //{
            //    if (_Thread_ReaderRead != null)
            //    {
            //        _Thread_ReaderRead.Abort();
            //        _Thread_ReaderRead = null;
            //    }
            //}
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
        }

        /// <summary>
        /// 进行天线扣款操作
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public int DoRSUPay(int money)
        {
            ETCResponse r = RSUOpen();
            if (r != null && r.ErrorCode == 0) r = OBUSearch();
            if (r != null && r.ErrorCode == 0) r = GetOBUInfo(r as OBUSearchResponse);
            if (r != null && r.ErrorCode == 0)
            {
                ETCPaymentRecord record = null;
                r = RSUWriteCard(r as GetOBUInfoResponse, money, out record);
                if (record != null) r = ListUpLoad(record);
            }
            if (r != null) return r.ErrorCode;
            return -1;
        }

        /// <summary>
        /// 进行读卡扣款操作
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public int DoReaderPay(int money)
        {
            ETCResponse r = CardReaderOpen();
            if (r != null && r.ErrorCode == 0) r = CardSearch();
            if (r != null && r.ErrorCode == 0) r = GetCardInfoFromReader(r as CardSearchResponse);
            if (r != null && r.ErrorCode == 0)
            {
                ETCPaymentRecord record = null;
                r = CardReaderWriteCard(r as GetCardInfoResponse, money, out record);
                if (record != null) r = ListUpLoad(record);
            }
            return r.ErrorCode;
        }
        #endregion
    }
}
