using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Result;

namespace Ralid.Park.BLL
{
    /// <summary>
    /// 双机热备热备设置数据库操作类
    /// </summary>
    public class HostStandbySettingBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public HostStandbySettingBll(string repoUri)
        {
            _RepoUri = repoUri;
        }
        #endregion

        #region 私有变量
        private string _RepoUri;
        #endregion

        #region 公共方法
        /// <summary>
        /// 根据parkID获取双机热备设置
        /// </summary>
        /// <returns></returns>
        public HostStandbySetting Get(int parkID)
        {
            SysParaSettingsBll bll = new SysParaSettingsBll(_RepoUri);
            HostStandbySetting setting = bll.GetSetting<HostStandbySetting>("HostStandbySetting_Park" + parkID.ToString());
            return setting;
        }
        /// <summary>
        /// 保存双机热备设置
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public bool Save(HostStandbySetting setting)
        {
            SysParaSettingsBll bll = new SysParaSettingsBll(_RepoUri);
            CommandResult ret = bll.SaveSetting<HostStandbySetting>(setting, "HostStandbySetting_Park" + setting.ParkID.ToString());
            return ret.Result == ResultCode.Successful;
        }
        #endregion
    }
}
