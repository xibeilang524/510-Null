using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip ;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    public class ZipFileWriter : IDisposable
    {
        #region  构造函数
        public ZipFileWriter(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            _Stream = new ZipOutputStream(fs);
        }
        #endregion

        #region 私有变量
        private ZipOutputStream _Stream = null;
        #endregion

        #region 公共方法
        /// <summary>
        /// 将内容添加到压缩文件
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="data">文件内容</param>
        public void WriteFile(string file, byte[] data)
        {
            if (_Stream != null)
            {
                ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                entry.DateTime = DateTime.Now;
                _Stream.PutNextEntry(entry);
                if (data != null && data.Length > 0) _Stream.Write(data, 0, data.Length);
            }
        }

        public void Dispose()
        {
            if (_Stream != null) _Stream.Dispose();
        }
        #endregion
    }
}
