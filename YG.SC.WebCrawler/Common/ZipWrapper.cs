using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WebCrawler.Common
{
    using System;

    using System.Text;

    using System.IO;

    using System.IO.Compression;

    using System.Collections.Generic;


    public class ZipWrapper
    {
        /// <summary>
        /// 将Gzip格式的Stream 转为String
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetResponseContent(Stream stream)
        {
            using (MemoryStream tempMs = new MemoryStream())
            {
                GZipStream Decompress = new GZipStream(stream, CompressionMode.Decompress);

                Decompress.CopyTo(tempMs);

                Decompress.Close();

                byte[] bitArry = tempMs.ToArray();

                string str = System.Text.Encoding.UTF8.GetString(bitArry);

                return str;
            }
        }
    }

}