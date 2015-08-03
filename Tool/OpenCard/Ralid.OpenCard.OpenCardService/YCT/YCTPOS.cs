using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.GeneralLibrary;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    /// <summary>
    /// 表示羊城通POS机
    /// </summary>
    public class YCTPOS
    {
        #region 构造函数
        public YCTPOS(byte comport, int baud)
        {
            _Port = new CommPort(comport, baud);
        }
        #endregion

        #region 私有变量
        private CommPort _Port;
        private YCTBuffer _buffer = new YCTBuffer();
        private System.Threading.AutoResetEvent _Responsed = new System.Threading.AutoResetEvent(false);
        private YCTPacket _Response = null;
        private int _LastError = -1;
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
            ////包结构 头(1byte) + 包长(2byte) + Command(1byte) + data(nbyte) + checksum32(4byte)
            List<byte> ret = new List<byte>();
            ret.Add(0xDA); //头
            byte[] temp = BEBinaryConverter.ShortToBytes((short)(5 + (data != null ? data.Length : 0))); //命令+数据+crc的长度
            ret.AddRange(temp);
            ret.Add((byte)cmd);
            if (data != null) ret.AddRange(data);
            ret.AddRange(BEBinaryConverter.IntToBytes(CRC32Helper.CRC32(ret.ToArray())));
            return ret.ToArray();
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
                payment.公交门槛计数 = Slice(data, 52, 1)[0];
                payment.地铁门槛计数 = Slice(data, 53, 1)[0];
                payment.联乘门槛计数 = Slice(data, 54, 1)[0];
                payment.本次交易入口设备编号 = HexStringConverter.HexToString(Slice(data, 55, 6), string.Empty);
                payment.本次交易入口日期时间 = HexStringConverter.HexToString(Slice(data, 61, 7), string.Empty);
                payment.上次交易设备编号 = HexStringConverter.HexToString(Slice(data, 68, 6), string.Empty);
                payment.上次交易日期时间 = HexStringConverter.HexToString(Slice(data, 74, 4), string.Empty);
                payment.区域代码 = HexStringConverter.HexToString(Slice(data, 78, 1), string.Empty);
                payment.区域卡类型 = HexStringConverter.HexToString(Slice(data, 79, 2), string.Empty);
                payment.区域子码 = HexStringConverter.HexToString(Slice(data, 81, 1), string.Empty);
                payment.交易认证码 = HexStringConverter.HexToString(Slice(data, 82, 4), string.Empty);
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
        /// 向读卡器请求命令，并取得返回包
        /// </summary>
        /// <param name="cmd">请求的命令</param>
        /// <param name="data">请求中包含的数据</param>
        /// <returns></returns>
        public YCTPacket Request(YCTCommandType cmd, byte[] data)
        {
            byte[] request = CreateRequest(cmd, data);
            Ralid.GeneralLibrary.LOG.FileLog.Log("羊城通读卡器", "发送数据: " + HexStringConverter.HexToString(request, " "));
            _Port.OnDataArrivedEvent -= _Port_OnDataArrivedEvent;
            _buffer.Clear();
            _Responsed.Reset();
            _Response = null;
            _Port.OnDataArrivedEvent += _Port_OnDataArrivedEvent;
            _Port.SendData(request);
            if (_Responsed.WaitOne(2000))
            {
                if (_Response != null && _Response.CheckCRC() && _Response.Command == (byte)cmd)
                {
                    _LastError = _Response.Status;
                    return _Response;
                }
            }
            _LastError = -1;
            return null;
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
        public YCTWallet Poll()
        {
            var response = Request(YCTCommandType.Poll, null);
            if (response != null && response.IsCommandExcuteOk)
            {

            }
            return null;
        }
        /// <summary>
        /// 预消费
        /// </summary>
        /// <param name="paid">消费金额(分为单位)</param>
        /// <param name="walletType">钱包类型 1表示M1 2表示CPU</param>
        /// <param name="maxOfflineMonth">离线过期月数</param>
        /// <returns></returns>
        public YCTPaymentInfo Prepaid(int paid, int walletType, int maxOfflineMonth = 12)
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
        /// <summary>
        /// 完成消费,返回TAC,如果返回空通过 GetlastError获取错误代码
        /// </summary>
        /// <returns></returns>
        public string CompletePaid()
        {
            var response = Request(YCTCommandType.CompletePaid, null);
            if (response != null && response.IsCommandExcuteOk)
            {
                return HexStringConverter.HexToString(response.Data, string.Empty);
            }
            return null;
        }

        public bool RestorePaid()
        {
            return false;
        }
        /// <summary>
        /// 捕捉黑名单
        /// </summary>
        /// <returns></returns>
        public bool CatchBlackList()
        {
            var response = Request(YCTCommandType.CatchBlack, null);
            return (response != null && response.IsCommandExcuteOk);
        }
        #endregion
    }
}
