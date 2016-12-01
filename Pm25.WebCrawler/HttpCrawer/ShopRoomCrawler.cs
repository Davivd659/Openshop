using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Filters;
using Winista.Text.HtmlParser.Lex;

namespace Pm25.WebCrawler
{
    using Winista.Text.HtmlParser;
    using HtmlAgilityPack;
    using YG.SC.DataAccess;
    using YG.SC.Service;
    using Pm25.WebCrawler.Common;
    using Pm25.WebCrawler.Models;
    using System.IO;
    public class ShopRoomCrawler : IPageParser<ShopRoom[]>
    {
        private string _htmlContent;
        public ShopRoomCrawler()
        {
            Uri uri = new Uri("http://m.yofang.cn/server/shop/search");
            ShopRoomCriteria criteria = new ShopRoomCriteria();
            criteria.city = "北京";
            criteria.from = 0;
            criteria.PageNo = 1;
            criteria.perPageCount = 10;

            var webResponse = new WebAgent().Request(uri, criteria);
            var stream = webResponse.GetResponseStream();

            string strResponse = ZipWrapper.GetResponseContent(stream);

            _htmlContent = strResponse;
        }
        public ShopRoom[] Extract()
        {
            ShopRoomSearchModel crawObj = (ShopRoomSearchModel)JsonSerializer.Deserialize(_htmlContent, typeof(ShopRoomSearchModel));

            List<ShopRoomModel> shops = crawObj.shops;
            List<ShopRoom> rooms = new List<ShopRoom>();

            foreach (var node in shops)
            { 
                ShopRoom sr = new ShopRoom();
                sr.ShopId = node.id;
                sr.RName = node.title;
                sr.city = node.city;
                sr.district = node.district;
                sr.hotarea = node.hotArea;
                sr.address = node.address;
                sr.sale = node.sale.ToString();
                sr.price = node.price;
                sr.square = node.square;
                sr.type = node.houseType;
                // sr.Face = node. //朝向， 木有
                sr.Contacts = node.contact;
                sr.Mobile = node.mobile;
                //sr.Rinfo = node.Rinfo;
                // sr.Rimg = node.Rimg;
                sr.AddTime = DateTime.Now;
                sr.source = node.source;
                sr.from = node.from;
                sr.fetchDate = node.fetchDate;

                rooms.Add(sr);
            }
            return rooms.ToArray();
        }
    }
}