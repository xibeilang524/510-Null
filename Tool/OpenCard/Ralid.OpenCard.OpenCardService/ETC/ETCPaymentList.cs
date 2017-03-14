using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.OpenCard.OpenCardService.ETC.Response;

namespace Ralid.OpenCard.OpenCardService.ETC
{
    public class ETCPaymentList
    {
        #region 构造函数
        public ETCPaymentList()
        {
        }
        #endregion

        #region 公共属性
        /// <summary>
        /// 流水类型 0：入口流水  1：出口流水
        /// </summary>
        public int ListType { get; set; }
        /// <summary>
        /// 流水号（28位）：省份编号（2位）+城市编号（2位）+小区编号（4位）+大门编号（2位）+车道编号（2位）+时间（yyyymmddhhmmss,14位）+顺序号（2位）。
        /// </summary>
        public string ListNo { get; set; }
        /// <summary>
        /// 密钥服务流水
        /// </summary>
        public string KeyServiceNo { get; set; }
        /// <summary>
        /// 消费类型  6:普通消   9:复合消费
        /// </summary>
        public int TradeType { get; set; }
        /// <summary>
        /// 终端交易序列号
        /// </summary>
        public string TermTradeNo { get; set; }
        /// <summary>
        /// 卡片交易序列号
        /// </summary>
        public string CardTradeNo { get; set; }
        /// <summary>
        /// PSAM终端号
        /// </summary>
        public string TermCode { get; set; }
        /// <summary>
        /// 校验数据
        /// </summary>
        public string Tac { get; set; }
        /// <summary>
        /// OBU号
        /// </summary>
        public string OBUID { get; set; }
        /// <summary>
        /// OBU应用序列号
        /// </summary>
        public string OBUNO { get; set; }
        /// <summary>
        /// CPU卡表面号
        /// </summary>
        public string CardNo { get; set; }
        /// <summary>
        /// 消费金额
        /// </summary>
        public int CashMoney { get; set; }
        /// <summary>
        /// 卡片余额（单位：分，消费后的余额）
        /// </summary>
        public long Balance { get; set; }
        /// <summary>
        /// 交易设备  0:使用RSU交易  1:使用读卡器交易 2：使用咪表终端交易
        /// </summary>
        public int TradeDevice { get; set; }
        /// <summary>
        /// 车辆图片(Base64编码),无车辆图片为NULL
        /// </summary>
        public string VehPicture { get; set; }
        /// <summary>
        /// 图片长度
        /// </summary>
        public int VehPictureLen { get; set; }
        /// <summary>
        /// 工班日期（yyyymmdd）
        /// </summary>
        public string SquadDate { get; set; }
        /// <summary>
        /// 收费员班次
        /// </summary>
        public string ShiftID { get; set; }
        /// <summary>
        /// 出口时间（yyyymmddhhmmss）,入口为NULL
        /// </summary>
        public string ExTime { get; set; }
        /// <summary>
        /// 出口区域编号
        /// </summary>
        public string ExAreaNo { get; set; }
        /// <summary>
        /// 出口大门编码(2位)
        /// </summary>
        public string ExGateNo { get; set; }
        /// <summary>
        /// 出口车道编码(2位)
        /// </summary>
        public string ExLaneNo { get; set; }
        /// <summary>
        /// 出口收费员工号,入口为NULL。（6位）
        /// </summary>
        public string ExOperatorNo { get; set; }
        /// <summary>
        /// 出口车牌,入口为NULL。（不超过12位）
        /// </summary>
        public string ExVehPlate { get; set; }
        /// <summary>
        /// 出口车型,入口为255。
        /// </summary>
        public string ExVehType { get; set; }
        /// <summary>
        /// 出口车种,入口为255。
        /// </summary>
        public string ExVehClass { get; set; }
        /// <summary>
        /// 入口时间（yyyymmddhhmmss）,出口时从卡片读取
        /// </summary>
        public string EnTime { get; set; }
        /// <summary>
        /// 入口收费员工号,出口时从卡片读取。
        /// </summary>
        public string EnOperatorNo { get; set; }
        /// <summary>
        /// 入口区域编号
        /// </summary>
        public string EnAreaNo { get; set; }
        /// <summary>
        /// 入口大门编码(2位)
        /// </summary>
        public string EnGateNo { get; set; }
        /// <summary>
        /// 入口车道编码(2位)
        /// </summary>
        public string EnLaneNo { get; set; }
        /// <summary>
        /// 入口车牌,出口时从卡片入口信息文件读取
        /// </summary>
        public string EnVehPlate { get; set; }
        /// <summary>
        /// 入口车型,出口时从卡片入口信息文件读取
        /// </summary>
        public string EnVehType { get; set; }
        /// <summary>
        /// 入口车种,出口时从卡片入口信息文件读取
        /// </summary>
        public string EnVehClass { get; set; }
        #endregion
    }
}
