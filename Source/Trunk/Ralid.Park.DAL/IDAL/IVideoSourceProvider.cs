using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.DAL.IDAL
{
    public interface IVideoSourceProvider:IProvider<VideoSourceInfo,int>
    {
        /// <summary>
        /// 删除所有对象
        /// </summary>
        /// <returns></returns>
        CommandResult DeleteAllItems();
        /// <summary>
        /// 插入记录，包括主键值
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        CommandResult InsertWithPrimaryKey(VideoSourceInfo info);
    }
}
