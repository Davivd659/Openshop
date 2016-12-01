using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using System.Threading.Tasks;

using System.IO;

namespace openshopAutoConsol
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    string url = "http://craw.kaidianing.com/";
                    var response = new WebAgent().Request(new Uri(url));
                    Stream stream = response.GetResponseStream();
                    string _content = GetAllText(stream);

                    Console.WriteLine("Data1:" + _content + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                System.Threading.Thread.Sleep(1000 * 60);
            }
        }
        public static string GetAllText(Stream stream)
        {
            var sr = new StreamReader(stream);
            var result = sr.ReadToEnd();
            return result;
        }
    }

    public class WebAgent
    {
        public HttpWebResponse Request(Uri uri)
        {
            var request = WebRequest.Create(uri.OriginalString) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            var response = request.GetResponse() as HttpWebResponse;
            return response;
        }
    }

}
