using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.DAL.IDAL
{
    public interface  ICardPaymentRecordProvider : IProvider<CardPaymentInfo, int>
    {
        /// <summary>
        /// 插入记录，包括主键值
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        CommandResult InsertWithPrimaryKey(CardPaymentInfo info);
    }
}
