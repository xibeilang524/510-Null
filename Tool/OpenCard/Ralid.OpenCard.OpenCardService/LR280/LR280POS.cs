using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ralid.GeneralLibrary;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.OpenCard.OpenCardService.LR280
{
    public class LR280POS
    {
        #region 构造函数
        public LR280POS(byte comport, int baud = 9600)
        {
            Commport = comport;
            _Baud = baud;
        }
        #endregion

        #region 私有变量
        private Random _Random = new Random();
        private int _Baud = 0;
        private object _PortLocker = new object();

        /// <summary>
        /// 获取或设置是否已经打开串口
        /// </summary>
        public bool IsOpen { get; set; }
        #endregion

        #region 私有方法
        private LR280Response Request(LR280Request r)
        {
            lock (_PortLocker)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    var req = GetBytes(r);
                    int ret = LR280Interop.bankall_yt(Commport, req, buffer);
                    if (ret == 0)
                    {
                        return GetResponse(buffer);
                    }
                    else if (ret == -1)
                    {
                        return new LR280Response() { 返回码 = "-1", 错误说明 = "串口未打开" };
                    }
                    return new LR280Response() { 返回码 = ret.ToString(), 错误说明 = "调用函数出错" };
                }
                catch (Exception ex)
                {
                    Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
                    return new LR280Response() { 返回码 = "-2", 错误说明 = ex.Message };
                }
            }
        }

        private byte[] GetBytes(LR280Request r)
        {
            string ret = "";
            string space = "";
            ret += string.IsNullOrEmpty(r.应用类型) ? "01" : r.应用类型;
            ret += string.IsNullOrEmpty(r.POS机号) ? space.PadRight(8) : r.POS机号.PadRight(8);
            ret += string.IsNullOrEmpty(r.POS员工号) ? space.PadRight(8) : r.POS员工号.PadRight(8);
            ret += r.交易类型标志;
            ret += r.金额.ToString().PadLeft(12, '0');
            ret += string.IsNullOrEmpty(r.原交易日期) ? space.PadRight(8) : r.原交易日期;
            ret += string.IsNullOrEmpty(r.原交易参考号) ? space.PadRight(12) : r.原交易参考号;
            ret += string.IsNullOrEmpty(r.原凭证号) ? space.PadRight(6) : r.原凭证号;
            r.LRC = _Random.Next(100, 999);
            ret += r.LRC.ToString().PadLeft(3, '0');
            ret += string.IsNullOrEmpty(r.授权码) ? space.PadRight(6) : r.授权码;
            ret += string.IsNullOrEmpty(r.卡号) ? space.PadRight(20) : r.卡号.PadRight(20);
            return Encoding.GetEncoding("GB2312").GetBytes(ret);
        }

        private LR280Response GetResponse(byte[] val)
        {
            if (val == null || val.Length == 0) return null;
            var str = System.Text.ASCIIEncoding.Default.GetString(val);
            LR280Response ret = new LR280Response();
            ret.返回码 = Encoding.GetEncoding("GB2312").GetString(val, 0, 2).Trim();
            ret.交易类型 = Encoding.GetEncoding("GB2312").GetString(val, 2, 2).Trim();
            ret.银行行号 = Encoding.GetEncoding("GB2312").GetString(val, 4, 4).Trim();
            ret.卡号 = Encoding.GetEncoding("GB2312").GetString(val, 8, 20).Trim();
            ret.凭证号 = Encoding.GetEncoding("GB2312").GetString(val, 28, 6).Trim();
            int m;
            var temp = Encoding.GetEncoding("GB2312").GetString(val, 34, 12).TrimStart('0');
            if (int.TryParse(temp, out m)) ret.金额 = m;
            ret.错误说明 = Encoding.GetEncoding("GB2312").GetString(val, 46, 40).Trim();
            ret.商户号 = Encoding.GetEncoding("GB2312").GetString(val, 86, 15).Trim();
            ret.终端号 = Encoding.GetEncoding("GB2312").GetString(val, 101, 8).Trim();
            ret.批次号 = Encoding.GetEncoding("GB2312").GetString(val, 109, 6).Trim();
            ret.交易日期 = Encoding.GetEncoding("GB2312").GetString(val, 115, 4).Trim();
            ret.交易时间 = Encoding.GetEncoding("GB2312").GetString(val, 119, 6).Trim();
            ret.交易参考号 = Encoding.GetEncoding("GB2312").GetString(val, 125, 12).Trim();
            ret.授权号 = Encoding.GetEncoding("GB2312").GetString(val, 137, 6).Trim();
            ret.清算日期 = Encoding.GetEncoding("GB2312").GetString(val, 143, 4).Trim();
            ret.LRC校验 = Encoding.GetEncoding("GB2312").GetString(val, 147, 3).Trim();
            return ret;
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置串口号
        /// </summary>
        public byte Commport { get; set; }
        /// <summary>
        /// 获取或设置是否记录日志
        /// </summary>
        public bool Log { get; set; }
        #endregion

        #region 公共方法(串口管理)
        /// <summary>
        /// 打开
        /// </summary>
        public int Open()
        {
            var ret = LR280Interop.open_dev(Commport, _Baud);
            if (ret == 0) IsOpen = true;
            return ret;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            LR280Interop.close_dev(Commport);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 签到
        /// </summary>
        public LR280Response CheckIn()
        {
            var r = new LR280Request() { 应用类型 = "01", 交易类型标志 = LR280PAYTYPE.签到 };
            return Request(r);
        }
        /// <summary>
        /// 结算
        /// </summary>
        public LR280Response Clear()
        {
            var r = new LR280Request() { 应用类型 = "01", 交易类型标志 = LR280PAYTYPE.结算 };
            return Request(r);
        }
        /// <summary>
        /// 读卡
        /// </summary>
        /// <returns></returns>
        public LR280Response ReadCard()
        {
            var r = new LR280Request() { 应用类型 = "01", 交易类型标志 = LR280PAYTYPE.读卡 };
            return Request(r);
        }

        public LR280Response 查余额()
        {
            var r = new LR280Request() { 应用类型 = "01", 交易类型标志 = LR280PAYTYPE.查余额 };
            return Request(r);
        }
        /// <summary>
        /// 消费
        /// </summary>
        /// <param name="money">金额（分)</param>
        /// <returns></returns>
        public LR280Response Pay(int money)
        {
            var r = new LR280Request() { 应用类型 = "01", 交易类型标志 = LR280PAYTYPE.消费, 金额 = money };
            return Request(r);
        }
        #endregion
    }
}
