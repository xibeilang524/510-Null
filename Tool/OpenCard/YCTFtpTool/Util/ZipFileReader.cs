using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;

namespace Ralid.OpenCard.YCTFtpTool
{
    public class ZipFileReader : IDisposable
    {
        #region 构造函数
        public ZipFileReader(string zipFile)
        {
            _Zip = new ZipFile(zipFile);
        }
        #endregion

        #region 私有变量
        private ZipInputStream _Stream = null;
        private ZipFile _Zip = null;
        #endregion

        #region 公共方法
        public byte[] ReadFile(string file)
        {
            if (_Zip != null)
            {
                foreach (ZipEntry f in _Zip)
                {
                    if (f.IsFile && f.Name.ToUpper() == file.ToUpper() && f.Size > 0)
                    {
                        byte[] data = new byte[f.Size];
                        using (var s = _Zip.GetInputStream(f))
                        {
                            s.Read(data, 0, data.Length);
                            return data;
                        }
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 读取第一个以某个前缀开头的文件
        /// </summary>
        /// <param name="filePrefix"></param>
        /// <returns></returns>
        public byte[] ReadFirst(string filePrefix)
        {
            if (_Zip != null)
            {
                foreach (ZipEntry f in _Zip)
                {
                    if (f.IsFile && f.Name.ToUpper().IndexOf(filePrefix.ToUpper()) == 0 && f.Size > 0)
                    {
                        byte[] data = new byte[f.Size];
                        using (var s = _Zip.GetInputStream(f))
                        {
                            s.Read(data, 0, data.Length);
                            return data;
                        }
                    }
                }
            }
            return null;
        }

        public void Dispose()
        {
            if (_Zip != null ) _Zip.Close();
        }
        #endregion
    }
}
