using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Report
{
    public class CarplateRecReport:ReportBase 
    {
                #region 构造函数
        public CarplateRecReport()
        {
        }

        public CarplateRecReport(int parkID, int entranceID, DateTime eventDateTime, string sourceName)
            : base(parkID, entranceID, eventDateTime, sourceName)
        {
            
        }
        #endregion

        #region 公共属性
        
        #endregion

        #region 重写基类方法
        public override string Description
        {
            get
            {
                return "处理车牌识别";
            }
        }
        #endregion
    }
}
