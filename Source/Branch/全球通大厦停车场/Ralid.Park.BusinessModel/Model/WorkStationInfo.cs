using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        #endregion
    }
}
