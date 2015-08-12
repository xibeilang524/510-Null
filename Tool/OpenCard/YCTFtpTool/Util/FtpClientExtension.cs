using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.FtpClient;

namespace Ralid.OpenCard.YCTFtpTool
{
    public static class FtpExtention
    {
        public static void Download(this FtpClient client, string path, System.IO.Stream stream)
        {
            using (var s = client.OpenRead(path))
            {
                byte[] data = new byte[1024];
                int count = s.Read(data, 0, data.Length);
                while (count > 0)
                {
                    stream.Write(data, 0, count);
                    count = s.Read(data, 0, data.Length);
                }
            }
        }

        public static void Upload(this FtpClient client, string remotepath, System.IO.Stream stream)
        {
            using (var s = client.OpenWrite(remotepath))
            {
                byte[] data = new byte[1024];
                int count = stream.Read(data, 0, data.Length);
                while (count > 0)
                {
                    s.Write(data, 0, count);
                    count = stream.Read(data, 0, data.Length);
                }
            }
        }
    }
}
