using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.BusinessModel.Enum;

namespace Ralid.Park.BusinessModel.Resouce
{
    /// <summary>
    /// 命令类型描述类
    /// </summary>
    public class CommandTypeDescription
    {
        public static string GetDescription(CommandType type)
        {
            switch (type)
            {
                case CommandType.AddCard:
                    return Resource1.CommandType_AddCard;
                case CommandType.ClearCard:
                    return Resource1.CommandType_ClearCard;
                case CommandType.UpateCard:
                    return Resource1.CommandType_UpdateCard;
                case CommandType.DeleteCard:
                    return Resource1.CommandType_DeleteCard;
                case CommandType.DownloadAccesses:
                    return Resource1.CommandType_DownloadAccesses;
                case CommandType.DownloadHolidays:
                    return Resource1.CommandType_DownloadHolidays;
                case CommandType.DownloadKeySetting:
                    return Resource1.CommandType_DownloadKeySetting;
                case CommandType.DownloadTariffs:
                    return Resource1.CommandType_DownloadTariffs;
                default:
                    return string.Empty;
            }
        }
    }
}
