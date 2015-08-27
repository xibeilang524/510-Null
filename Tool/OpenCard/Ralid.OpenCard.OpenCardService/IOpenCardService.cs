using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.OpenCard.OpenCardService
{
    public interface IOpenCardService
    {
        event EventHandler<OpenCardEventArgs> OnReadCard;

        event EventHandler<OpenCardEventArgs> OnPaying;

        event EventHandler<OpenCardEventArgs> OnPaidOk;

        event EventHandler<OpenCardEventArgs> OnPaidFail;

        event EventHandler<OpenCardEventArgs> OnError;

        void Init();

        void Dispose();
    }

    public class OpenCardEventArgs : EventArgs
    {
        #region 构造函数
        public OpenCardEventArgs()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 获取或设置刷卡通道
        /// </summary>
        public EntranceInfo Entrance { get; set; }
        /// <summary>
        /// 获取或设置卡号
        /// </summary>
        public string CardID { get; set; }
        /// <summary>
        /// 获取或设置卡片类型
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 获取或设置卡片余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 获取或设置卡片收费信息
        /// </summary>
        public CardPaymentInfo Payment { get; set; }
        /// <summary>
        /// 获取或设置已经成功收取的费用
        /// </summary>
        public decimal Paid { get; set; }
        /// <summary>
        /// 获取或设置系统最近一次产生的错误
        /// </summary>
        public string LastError { get; set; }
        #endregion
    }
}
