using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.WebCrawler
{
    public class StreamHepler
    {
        public static string GetAllText(Stream stream)
        {
            var sr = new StreamReader(stream);
            var result = sr.ReadToEnd();
            return result;
        }
    }
}
