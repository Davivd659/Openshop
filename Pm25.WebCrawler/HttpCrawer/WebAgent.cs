using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Pm25.WebCrawler
{
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

        public HttpWebResponse Request(Uri uri, Pm25.WebCrawler.Models.ShopRoomCriteria obj)
        {
            var request = WebRequest.Create(uri.OriginalString) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json";

            string postdata = Common.JsonSerializer.Serialize(obj);

            // jsonstr = "{\"city\":\"北京\",\"from\",\"0\",\"PageNo\",\"1\",\"perPageCount\",\"100\"}";
            byte[] bytes = Encoding.UTF8.GetBytes(postdata);
            Stream sendStream = request.GetRequestStream();
            sendStream.Write(bytes, 0, bytes.Length);
            sendStream.Close();
            //request.ContentLength = bytes.Length;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();
            return response;
        }

    }

}
