using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary;

namespace Ralid.GeneralLibrary.CardReader.YCT
{
    /// <summary>
    /// 表示羊城通POS机
    /// </summary>
    public class YCTPOS
    {
        #region 构造函数
        public YCTPOS(byte comport, int baud)
        {
            _Port = new ComPort(comport, baud);
        }
        #endregion

        #region 私有变量
        private ComPort _Port;
        private object _PortLocker = new object();
        private YCTBuffer _buffer = new YCTBuffer();
        private System.Threading.AutoResetEvent _Responsed = new System.Threading.AutoResetEvent(false);
        private YCTPacket _Response = null;
        private int _LastError = -1;
        private YCTWallet _LastWallet = null;
        #endregion

        #region 私有方法
        private void _Port_OnDataArrivedEvent(object sender, byte[] data)
        {
            _buffer.Write(data);
            YCTPacket p = _buffer.Read();
            if (p != null)
            {
                _Response = p;
                _Responsed.Set();
                Ralid.GeneralLibrary.LOG.FileLog.Log("羊城通读卡器", "接收数据: " + HexStringConverter.HexToString(p.AllBytes, " "));
            }
        }

        private byte[] CreateRequest(YCTCommandType cmd, byte[] data)
        {
            ////包结构 头(1byte) + 包长(1byte) + Command(1byte) + data(nbyte) + checksum(1byte)
            List<byte> ret = new List<byte>();
            ret.Add(0xBA); //头
            ret.Add((byte)(2 + (data != null ? data.Length : 0))); //命令+数据+crc的长度
            ret.Add((byte)cmd);
            if (data != null) ret.AddRange(data);
            ret.Add(CRCHelper.CalCRC(ret));
            return ret.ToArray();
        }

        /// <summary>
        /// 向读卡器请求命令，并取得返回包
        /// </summary>
        /// <param name="cmd">请求的命令</param>
        /// <param name="data">请求中包含的数据</param>
        /// <returns></returns>
        private YCTPacket Request(YCTCommandType cmd, byte[] data)
        {
            lock (_PortLocker)
            {
                byte[] request = CreateRequest(cmd, data);
                Ralid.GeneralLibrary.LOG.FileLog.Log("羊城通读卡器", "发送数据: " + HexStringConverter.HexToString(request, " "));
                _Port.OnDataArrivedEvent -= _Port_OnDataArrivedEvent;
                _buffer.Clear();
                _Responsed.Reset();
                _Response = null;
                _Port.OnDataArrivedEvent += _Port_OnDataArrivedEvent;
                _Port.SendData(request);
                if (_Responsed.WaitOne(5000))
                {
                    if (_Response != null && _Response.CheckCRC() && _Response.Command == (byte)cmd)
                    {
                        _LastError = _Response.Status;
                        return _Response;
                    }
                }
            }
            _LastError = -1;
            return null;
        }

        private YCTPaymentInfo GetPaymentInfoFromM1(YCTPacket packet)
        {
            byte[] data = packet.Data; //73个字节
            if (data == null || data.Length == 0) return null;
            try
            {
                YCTPaymentInfo payment = new YCTPaymentInfo();
                payment.终端交易流水号 = BEBinaryConverter.BytesToInt(Slice(data, 0, 4));
                payment.逻辑卡号 = HexStringConverter.HexToString(Slice(data, 4, 8), string.Empty);
                payment.物理卡号 = HexStringConverter.HexToString(Slice(data, 12, 4), string.Empty);
                payment.上次交易设备编号 = HexStringConverter.HexToString(Slice(data, 16, 4), string.Empty);
                payment.上次交易日期时间 = HexStringConverter.HexToString(Slice(data, 20, 7), string.Empty);
                payment.本次交易设备编号 = HexStringConverter.HexToString(Slice(data, 27, 4), string.Empty);
                payment.本次交易日期时间 = DateTime.ParseExact(HexStringConverter.HexToString(Slice(data, 31, 7), string.Empty), "yyyyMMddHHmmss", null);
                payment.交易金额 = BEBinaryConverter.BytesToInt(Slice(data, 38, 4));
                payment.本次余额 = BEBinaryConverter.BytesToInt(Slice(data, 42, 4));
                payment.票价 = BEBinaryConverter.BytesToInt(Slice(data, 46, 4));
                payment.交易类型 = Slice(data, 50, 1)[0];
                payment.票卡消费交易计数 = BEBinaryConverter.BytesToInt(Slice(data, 51, 2));
                payment.累计门槛月份 = HexStringConverter.HexToString(Slice(data, 53, 2), string.Empty);
                payment.公交门槛计数 = Slice(data, 55, 1)[0];
                payment.地铁门槛计数 = Slice(data, 56, 1)[0];
                payment.联乘门槛计数 = Slice(data, 57, 1)[0];
                payment.本次交易入口设备编号 = HexStringConverter.HexToString(Slice(data, 58, 4), string.Empty);
                payment.本次交易入口日期时间 = HexStringConverter.HexToString(Slice(data, 62, 7), string.Empty);
                payment.交易认证码 = HexStringConverter.HexToString(Slice(data, 69, 4), string.Empty);
                return payment;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        private YCTPaymentInfo GetPaymentInfoFromCPU(YCTPacket packet)
        {
            byte[] data = packet.Data; //86字节
            if (data == null || data.Length == 0) return null;
            try
            {
                YCTPaymentInfo payment = new YCTPaymentInfo();
                payment.本次交易设备编号 = HexStringConverter.HexToString(Slice(data, 0, 6), string.Empty);
                payment.终端交易流水号 = BEBinaryConverter.BytesToInt(Slice(data, 6, 4));
                payment.本次交易日期时间 = DateTime.ParseExact(HexStringConverter.HexToString(Slice(data, 10, 7), string.Empty), "yyyyMMddHHmmss", null);
                payment.逻辑卡号 = HexStringConverter.HexToString(Slice(data, 17, 8), string.Empty);
                payment.物理卡号 = HexStringConverter.HexToString(Slice(data, 25, 8), string.Empty);
                payment.交易金额 = BEBinaryConverter.BytesToInt(Slice(data, 33, 4));
                payment.票价 = BEBinaryConverter.BytesToInt(Slice(data, 37, 4));
                payment.本次余额 = BEBinaryConverter.BytesToInt(Slice(data, 41, 4));
                payment.交易类型 = Slice(data, 45, 1)[0];
                payment.附加交易类型 = Slice(data, 46, 1)[0];
                payment.票卡充值交易计数 = BEBinaryConverter.BytesToInt(Slice(data, 47, 2));
                payment.票卡消费交易计数 = BEBinaryConverter.BytesToInt(Slice(data, 49, 2));
                payment.累计门槛月份 = HexStringConverter.HexToString(Slice(data, 51, 2), string.Empty);
                payment.公交门槛计数 = Slice(data, 53, 1)[0];
                payment.地铁门槛计数 = Slice(data, 54, 1)[0];
                payment.联乘门槛计数 = Slice(data, 55, 1)[0];
                payment.本次交易入口设备编号 = HexStringConverter.HexToString(Slice(data, 56, 6), string.Empty);
                payment.本次交易入口日期时间 = HexStringConverter.HexToString(Slice(data, 62, 7), string.Empty);
                payment.上次交易设备编号 = HexStringConverter.HexToString(Slice(data, 69, 6), string.Empty);
                payment.上次交易日期时间 = HexStringConverter.HexToString(Slice(data, 75, 4), string.Empty);
                payment.区域代码 = HexStringConverter.HexToString(Slice(data, 79, 1), string.Empty);
                payment.区域卡类型 = HexStringConverter.HexToString(Slice(data, 80, 2), string.Empty);
                payment.区域子码 = HexStringConverter.HexToString(Slice(data, 82, 1), string.Empty);
                payment.交易认证码 = HexStringConverter.HexToString(Slice(data, 83, 4), string.Empty);
                return payment;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        private byte[] Slice(byte[] source, int index, int count)
        {
            byte[] ret = new byte[count];
            Array.Copy(source, index, ret, 0, count);
            return ret;
        }
        /// <summary>
        /// 获取错误代码的文字说明
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        private string GetErrDescr(int error)
        {
            switch (error)
            {
                case 0: return "操作正常";
                case 0x0F: return "离线月份参数非法";
                case 0x60: return "没有安装SAM卡";
                case 0x61: return "SAM卡初始化错误或未初始化";
                case 0x62: return "SAM卡检验PIN错误";
                case 0x63: return "SAM卡类型与交易类型不匹配";
                case 0x64: return "SAM卡选择文件错误";
                case 0x65: return "SAM卡读错误";
                case 0x66: return "SAM卡写错误";
                case 0x67: return "SAM卡认证错误";
                case 0x68: return "SAM卡随机数错误";
                case 0x69: return "SAM卡DES计算错误";
                case 0x6A: return "SAM卡生成钱包密钥错误";
                case 0x71: return "PKI卡RSA计算错误";
                case 0x72: return "PKI卡DES计算错误";
                case 0x7E: return "SAM卡执行APDU命令错误";
                case 0x7F: return "SAM卡操作超时";
                case 0x80: return "没有卡";
                case 0x81: return "选择卡片错误";
                case 0x82: return "停用卡片错误";
                case 0x83: return "认证卡片错误";
                case 0x84: return "卡片读操作错误";
                case 0x85: return "卡片写操作错误";
                case 0x86: return "卡片写操作中途中断";
                case 0x87: return "充值卡片无响应";
                case 0x90: return "不是本系统的标准卡片";
                case 0x91: return "卡片超出有效期";
                case 0x92: return "城市代码或应用代码错误";
                case 0x93: return "非法卡";
                case 0x94: return "黑名单卡";
                case 0x95: return "钱包余额不足";
                case 0x96: return "钱包余额超出上限";
                case 0x97: return "钱包未启用";
                case 0x98: return "钱包已停用";
                case 0x99: return "钱包正本被破坏";
                case 0x9A: return "钱包已停用";
                case 0x9F: return "公共信息区被破坏";
                case 0xAF: return "卡片操作超时";
                case 0xB0: return "交易操作中途中断";
                case 0xB1: return "交易中断";
                case 0xB2: return "前一步指令未执行或失败";
                case 0xCF: return "交易操作超时";
                case 0xD0: return "远程读写器执行错";
                case 0xE0: return "Mifare硬件初始化错误";
                case 0xE1: return "SAM硬件初始化错误";
                case 0xE2: return "命令错误";
                case 0xE3: return "参数错误";
                case 0xE4: return "检验和错误";
                case 0xE5: return "线路通讯超时";
                case 0xE6: return "内部FLASH写错误";
                case 0x30: return "报文头错";
                case 0x31: return "卡片不一致";
                case 0x32: return "流水号不一致";
                case 0x33: return "MAC错";
                case 0x3F: return "不支付的命令";
                case 0x1A: return "物理卡号不一致";
            }
            return "未知错误";
        }
        /// <summary>
        /// 获取卡片序列号,wgType=0表示WEGEN 34, wgType=1表示WEGEN26协议
        /// </summary>
        /// <returns></returns>
        private string ReadSN(int wgType = 0)
        {
            string ret = null;
            var response = Request(YCTCommandType.ReadSerialNumber, null);
            if (response != null && response.IsCommandExcuteOk)
            {
                byte[] data = response.Data;
                if (data != null && data.Length >= 4)
                {
                    if (wgType == 1)
                    {
                        ret = SEBinaryConverter.BytesToLong(Slice(data, 0, 3)).ToString(); //取前三字节
                    }
                    else
                    {
                        ret = SEBinaryConverter.BytesToLong(Slice(data, 0, 4)).ToString();
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 预消费
        /// </summary>
        /// <param name="paid">消费金额(分为单位)</param>
        /// <param name="walletType">钱包类型 1表示M1 2表示CPU</param>
        /// <param name="maxOfflineMonth">离线过期月数</param>
        /// <returns></returns>
        private YCTPaymentInfo Prepaid(int paid, int walletType, int maxOfflineMonth = 12)
        {
            DateTime dt = DateTime.Now;
            List<byte> data = new List<byte>();
            data.AddRange(BEBinaryConverter.IntToBytes(paid));
            data.AddRange(BEBinaryConverter.IntToBytes(paid));
            data.Add(BCDConverter.IntToBCD(dt.Year / 100));
            data.Add(BCDConverter.IntToBCD(dt.Year % 100));
            data.Add(BCDConverter.IntToBCD(dt.Month));
            data.Add(BCDConverter.IntToBCD(dt.Day));
            data.Add(BCDConverter.IntToBCD(dt.Hour));
            data.Add(BCDConverter.IntToBCD(dt.Minute));
            data.Add(BCDConverter.IntToBCD(dt.Second));
            data.Add((byte)(maxOfflineMonth > 0 ? 0x01 : 0x00));
            data.Add(BCDConverter.IntToBCD(maxOfflineMonth));
            var response = Request(YCTCommandType.Prepaid, data.ToArray());
            if (response != null && response.IsCommandExcuteOk)
            {
                if (walletType == 1) return GetPaymentInfoFromM1(response);
                if (walletType == 2) return GetPaymentInfoFromCPU(response);
            }
            return null;
        }
        #endregion

        #region 公共方法(串口管理)
        /// <summary>
        /// 打开
        /// </summary>
        public void Open()
        {
            _Port.Open();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            _Port.Close();
        }
        /// <summary>
        /// 获取读卡器是否已经打开
        /// </summary>
        public bool IsOpened { get { return _Port.PortOpened; } }
        #endregion

        #region 公共方法
        /// <summary>
        /// 获取读卡器版本
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            string ret = null;
            var resoponse = Request(YCTCommandType.GetVersion, null);
            if (resoponse != null && resoponse.IsCommandExcuteOk)
            {
                byte[] data = resoponse.Data;
                if (data != null && data.Length == 60)
                {
                    ret = string.Format("{0}-{1}-{2}",
                        ASCIIEncoding.ASCII.GetString(Slice(data, 0, 8)),
                        ASCIIEncoding.ASCII.GetString(Slice(data, 8, 32)),
                        ASCIIEncoding.ASCII.GetString(Slice(data, 40, 20)));
                }
            }
            return ret;
        }
        /// <summary>
        /// 设置商家编号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool SetServiceCode(int code)
        {
            code = code % 10000; //取数值的低四位
            byte[] bcd = new byte[] { BCDConverter.IntToBCD(code / 100), BCDConverter.IntToBCD(code % 100) };
            var response = Request(YCTCommandType.SetServiceCode, bcd);
            return (response != null && response.IsCommandExcuteOk);
        }
        /// <summary>
        /// 初始化消费模式
        /// </summary>
        /// <returns></returns>
        public bool InitPaidMode()
        {
            var response = Request(YCTCommandType.InitPaidPara, new byte[] { 0x80 }); //0x80表示正常消费
            return (response != null && response.IsCommandExcuteOk);
        }
        /// <summary>
        /// 询卡
        /// </summary>
        /// <returns></returns>
        public YCTWallet ReadCard(WegenType wg = WegenType.Wengen34)
        {
            var response = Request(YCTCommandType.Poll, null);
            if (response != null && response.IsCommandExcuteOk && response.Data != null && response.Data.Length == 52)
            {
                byte[] data = response.Data;
                YCTWallet w = new YCTWallet();
                w.WalletType = data[0];
                if (w.WalletType == 1)
                {
                    w.PhysicalCardID = HexStringConverter.HexToString(Slice(data, 1, 4), string.Empty); //M1钱包物理卡只取前四字节
                }
                else
                {
                    w.PhysicalCardID = HexStringConverter.HexToString(Slice(data, 1, 8), string.Empty);
                }
                w.LogicCardID = HexStringConverter.HexToString(Slice(data, 9, 8), string.Empty);
                w.Balance = BEBinaryConverter.BytesToInt(Slice(data, 17, 4));
                w.Count = BEBinaryConverter.BytesToInt(Slice(data, 21, 2));
                w.CardType = HexStringConverter.HexToString(Slice(data, 23, 2), string.Empty);
                w.MinBalance = data[25] * 100;
                w.MaxBalance = BEBinaryConverter.BytesToInt(Slice(data, 26, 3));
                w.Deposit = BEBinaryConverter.BytesToInt(Slice(data, 29, 4));
                _LastWallet = w;
                return w;
            }
            else if (LastError == 0x83) //验证出错,说明卡片是其它IC卡,继续读其序列号
            {
                string sn = ReadSN(wg == WegenType.Wengen26 ? 1 : 0);
                if (sn != null)
                {
                    _LastWallet = new YCTWallet() { LogicCardID = sn, PhysicalCardID = sn, CardType = string.Empty };
                    return _LastWallet;
                }
            }
            return null;
        }
        /// <summary>
        /// 消费,返回消费记录,如果返回null,通过 GetlastError获取错误代码
        /// </summary>
        /// <param name="paid">消费金额(分为单位)</param>
        /// <param name="walletType">钱包类型 1表示M1 2表示CPU</param>
        /// <param name="maxOfflineMonth">离线过期月数</param>
        /// <returns></returns>
        public YCTPaymentInfo Paid(int paid, int walletType, int maxOfflineMonth = 12)
        {
            var ret = Prepaid(paid, walletType, maxOfflineMonth);
            if (ret != null)
            {
                var response = Request(YCTCommandType.CompletePaid, null);
                if (response != null && response.IsCommandExcuteOk)
                {
                    if (walletType == 2) ret.交易认证码 = HexStringConverter.HexToString(response.Data, string.Empty);
                    return ret;
                }
            }
            return null;
        }
        /// <summary>
        /// 控制蜂鸣器响
        /// </summary>
        /// <param name="tong"></param>
        /// <param name="delay"></param>
        public void Beep(int tong, int delay)
        {
            List<byte> data=new List<byte> ();
            data.AddRange (BEBinaryConverter .IntToBytes (tong));
            data.AddRange (BEBinaryConverter .IntToBytes (delay ));
            var response = Request(YCTCommandType.Beep, data.ToArray());
        }
        /// <summary>
        /// 获取最后一次操作的错误代码
        /// </summary>
        public int LastError
        {
            get
            {
                return _LastError;
            }
        }
        /// <summary>
        /// 获取最后一次操作的错误描述
        /// </summary>
        public string LastErrorDescr
        {
            get
            {
                return GetErrDescr(_LastError);
            }
        }
        /// <summary>
        /// 获取最后一次读到的羊城通钱包
        /// </summary>
        public YCTWallet LastWallet
        {
            get
            {
                return _LastWallet;
            }
        }
        #endregion
    }
}
