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
        #region 静态变量
        public static readonly string 消费 = "00";
        public static readonly string 撤消 = "01";
        public static readonly string 退货 = "02";
        public static readonly string 查余额 = "03";
        public static readonly string 取打印 = "04";
        public static readonly string 签到 = "05";
        public static readonly string 结算 = "06";
        public static readonly string 读卡 = "70";
        public static readonly string 握手 = "99";
        public static readonly string 查询状态 = "SQ";
        public static readonly string PING网络测试 = "PI";
        public static readonly string 关闭资源 = "CL";
        #endregion

        #region 构造函数
        public LR280POS(byte comport, int baud = 9600)
        {
            Commport = comport;
            _Baud = baud;
        }
        #endregion

        #region 私有变量
        private int _Baud = 0;
        private object _PortLocker = new object();
        #endregion

        #region 私有方法

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

        public bool IsOpened { get; set; }
        /// <summary>
        /// 获取或设置最后一次错误
        /// </summary>
        public string  LastError { get; set; }
        #endregion

        #region 公共方法(串口管理)
        /// <summary>
        /// 打开
        /// </summary>
        public void Open()
        {
            if (!IsOpened) return;
            int ret = LR280Interop.open_dev(Commport, _Baud);
            if (ret == 0) IsOpened = true;
            else LastError = "-1";
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
        public void CheckIn()
        {

        }

        public LR280CardInfo ReadCard()
        {
            return null;
        }


        public bool Pay(string cardID, int money)
        {
            return false;
        }
        #endregion
    }
}
