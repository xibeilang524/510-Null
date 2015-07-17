using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.BusinessModel.Enum
{
    /// <summary>
    /// 命令类型
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// 新增卡片
        /// </summary>
        AddCard = 0,
        /// <summary>
        /// 修改卡片
        /// </summary>
        UpateCard = 1,
        /// <summary>
        /// 删除卡片
        /// </summary>
        DeleteCard = 2,
        /// <summary>
        /// 清除卡片
        /// </summary>
        ClearCard = 3,
        /// <summary>
        /// 下载通道权限
        /// </summary>
        DownloadAccesses = 4,
        /// <summary>
        /// 下载节假日
        /// </summary>
        DownloadHolidays = 5,
        /// <summary>
        /// 下载费率
        /// </summary>
        DownloadTariffs = 6,
        /// <summary>
        /// 下载密钥
        /// </summary>
        DownloadKeySetting = 7

    }
}
