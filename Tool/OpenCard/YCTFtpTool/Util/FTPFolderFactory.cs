using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Security.AccessControl;
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
                if (string.IsNullOrEmpty(path)) path = System.IO.Path.Combine(Application.StartupPath, "FTP");
                AppSettings.CurrentSetting.SaveConfig("YCTFtpPath", path);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path = Path.Combine(path, "发送");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
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
                if (string.IsNullOrEmpty(path)) path = System.IO.Path.Combine(Application.StartupPath, "FTP");
                AppSettings.CurrentSetting.SaveConfig("YCTFtpPath", path);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path = Path.Combine(path, "接收");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
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
