using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.OpenCard.YCTFtpTool
{
    public class FTPFolderFactory
    {
        /// <summary>
        /// 生成上传文件存放的目录
        /// </summary>
        /// <returns></returns>
        public static string CreateUploadFolder()
        {
            try
            {
                string path = AppSettings.CurrentSetting.GetConfigContent("YCTFtpPath");
                if (string.IsNullOrEmpty(path)) return null;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path = Path.Combine(path, "上传");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                //path = Path.Combine(path, dt.ToString("yyyy年MM月"));
                //if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                return path;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        /// <summary>
        /// 生成下载文件存放的目录
        /// </summary>
        /// <returns></returns>
        public static string CreateDownloadFolder()
        {
            try
            {
                string path = AppSettings.CurrentSetting.GetConfigContent("YCTFtpPath");
                if (string.IsNullOrEmpty(path)) return null;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path = Path.Combine(path, "下载");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                //path = Path.Combine(path, dt.ToString("yyyy年MM月"));
                //if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                return path;
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
            return null;
        }
    }
}
