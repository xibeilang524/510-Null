using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ralid.Park.BusinessModel.Configuration;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    public class FTPFolderFactory
    {
        public static string CreateUploadFolder(DateTime dt)
        {
            try
            {
                string path = AppSettings.CurrentSetting.GetConfigContent("YCTFtpPath");
                if (string.IsNullOrEmpty(path)) return null;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path = Path.Combine(path, "上传");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path = Path.Combine(path, dt.ToString("yyyy年MM月"));
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
