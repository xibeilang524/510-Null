using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.Park.BusinessModel.Model
{
    [Serializable ()]
    public class WorkStationInfo
    {
        /// <summary>
        /// 获取或设置当前系统的工作站
        /// </summary>
        public static WorkStationInfo CurrentStation { get; set; }

        private readonly string defaultStation = "DefaultStation";

        private char _CenterCharge;
        private List<int> _EntranceList;

        private string _EntranceIDs
        {
            get
            {
                if (EntranceList != null && EntranceList.Count > 0)
                {
                    string[] list = _EntranceList.Select(en => en.ToString()).ToArray();
                    return string.Join(",", list);
                }
                return string.Empty;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _EntranceList = new List<int>();
                }
                else
                {
                    _EntranceList = value.Split(',').Select(str => int.Parse(str)).ToList();
                }
            }
        }

        #region 公共属性
        /// <summary>
        /// 工作站ID
        /// </summary>
        public string StationID { get; set; }

        /// <summary>
        /// 工作站名
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 工作站上一操作员交班时剩余的卡片数
        /// </summary>
        public int Cards{get;set;}

        /// <summary>
        /// 工作站上一操作员交班时剩余的现金
        /// </summary>
        public decimal Cash { get; set; }

        /// <summary>
        /// 获取或设置目前当班的收费操作员ID
        /// </summary>
        public string OnDutyOperator { get; set; }

        /// <summary>
        /// 获取或设置当班操作员的当班时间
        /// </summary>
        public DateTime? OnDutyDateTime{get;set;}
       
        /// <summary>
        /// 工作站控制的控制器列表
        /// </summary>
        public List<int> EntranceList
        {
            get
            {
                return _EntranceList;
            }
            set
            {
                _EntranceList = value;
            }
        }

        /// <summary>
        /// 是否是中央收费
        /// </summary>
        public bool IsCenterCharge
        {
            get
            {
                return _CenterCharge == '1';
            }
            set
            {
                _CenterCharge = value ? '1' : '0';
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 工作站是否可以删除,系统默认的工作站不可删除
        /// </summary>
        public bool CanDelete
        {
            get
            {
                return StationID != defaultStation;
            }
        }

        /// <summary>
        /// 查看某个停车场是否是工作站要侦听的停车场
        /// </summary>
        /// <param name="park"></param>
        /// <returns></returns>
        public bool IsInListenList(ParkInfo park)
        {
            foreach (EntranceInfo entrance in park.Entrances)
            {
                if (_EntranceList.Exists(e => e == entrance.EntranceID))
                {
                    return true;
                }
            }
            if (park.SubParks != null && park.SubParks.Count > 0)
            {
                foreach (ParkInfo sub in park.SubParks)
                {
                    if (IsInListenList(sub)) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取工作站是否某个停车场的通信工作站
        /// </summary>
        public bool IsHostWorkstation { get; set; }
        
        /// <summary>
        /// 获取工作站是否需要同时更新两个数据库
        /// </summary>
        public bool NeedBothDatabaseUpdate
        {
            get
            {
                //不为通信工作站，设置了主数据和备用数据库
                return !IsHostWorkstation
                    && HasMasterDatabase
                    && HasStandbyDatabase;
            }
        }
        /// <summary>
        /// 获取工作站是否有主数据库
        /// </summary>
        public bool HasMasterDatabase
        {
            get
            {
                return !string.IsNullOrEmpty(AppSettings.CurrentSetting.MasterParkConnect);
            }
        }
        /// <summary>
        /// 获取工作站是否有备用数据库
        /// </summary>
        public bool HasStandbyDatabase
        {
            get
            {
                return !string.IsNullOrEmpty(AppSettings.CurrentSetting.StandbyParkConnect);
            }
        }
        #endregion

        #region 公共方法 
        /// <summary>
        /// 操作员交班
        /// </summary>
        /// <param name="operatorID">要交班的操作员ID</param>
        /// <param name="cash">操作员留给下一班操作员的现金</param>
        /// <param name="cards">操作员留给下一班操作员的临时卡</param>
        /// <param name="offduty">交班时间</param>
        public void OperatorOffDuty(string operatorID, decimal cash, int cards, DateTime offduty)
        {
            this.Cash = cash;
            this.Cards = cards;
            this.OnDutyOperator = null;
            this.OnDutyDateTime = null;
        }

        /// <summary>
        /// 操作员上班
        /// </summary>
        /// <param name="operatorID">要上班的操作员ID</param>
        /// <param name="cash">上一班操作员留下的现金</param>
        /// <param name="cards">上一班操作员留下的临时卡</param>
        /// <param name="onduty">操作员上班时间</param>
        public void OperatorOnDuty(string operatorID, decimal cash, int cards,DateTime onduty)
        {
            this.Cash = cash;
            this.Cards = cards;
            this.OnDutyDateTime = onduty;
            this.OnDutyOperator = operatorID;
        }

        ///// <summary>
        ///// 卡片在工作站是否需要同时更新两个数据库
        ///// </summary>
        //public bool CardNeedBothDatabaseUpdate(CardInfo card)
        //{
        //    bool result = NeedBothDatabaseUpdate;

        //    //写卡模式模式时，脱机处理的卡片不需要同时更新两个数据库
        //    if (result
        //        && AppSettings.CurrentSetting.EnableWriteCard
        //        && card != null
        //        && !card.OnlineHandleWhenOfflineMode)
        //    {
        //        result = false;
        //    }
        //    return result;
        //}

        /// <summary>
        /// 工作站能否缴费
        /// </summary>
        /// <param name="offlineHandleCard">是否脱机处理卡片</param>
        /// <param name="msg">返回的信息</param>
        /// <returns></returns>
        public bool CanPayment(bool offlineHandleCard, out string msg)
        {
            //脱机模式处理卡片不需要数据库连接也可以缴费
            if (!offlineHandleCard)
            {
                //如设置备用数据库，通信工作站只需要备用数据库连接就可以了，非通信工作站需要主数据库和备用数据库都连接
                if (HasStandbyDatabase)
                {
                    if (!DataBaseConnectionsManager.Current.StandbyConnected)
                    {
                        msg = "与备用数据库连接失败！";
                        return false;
                    }

                    if (!IsHostWorkstation
                        && !DataBaseConnectionsManager.Current.MasterConnected)
                    {
                        msg = "与主数据库连接失败！";
                        return false;
                    }
                }
                else if (HasMasterDatabase)
                {
                    if (!DataBaseConnectionsManager.Current.MasterConnected)
                    {
                        msg = "与主数据库连接失败！";
                        return false;
                    }
                }
                else
                {
                    msg = "没有设置数据库！";
                    return false;
                }
            }

            msg = string.Empty;
            return true;
        }
        #endregion
    }
}
