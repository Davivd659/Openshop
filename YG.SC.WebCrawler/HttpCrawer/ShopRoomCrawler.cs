using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YG.SC.WebCrawler
{
    using YG.SC.DataAccess;
    using YG.SC.Service;
    using YG.SC.WebCrawler.Common;
    using YG.SC.WebCrawler.Models;
    using System.IO;
    public class ShopRoomCrawler : IPageParser<ShopRoom[]>
    {
        private string _htmlContent;
        public ShopRoomCrawler(int PageNo = 1)
        {
            Uri uri = new Uri("http://m.yofang.cn/server/shop/search");
            ShopRoomCriteria criteria = new ShopRoomCriteria();
            // criteria.city = "北京";
            criteria.from = 0;
            criteria.PageNo = PageNo;
            criteria.perPageCount = 20;

            var webResponse = new WebAgent().Request(uri, criteria);
            var stream = webResponse.GetResponseStream();

            string strResponse = ZipWrapper.GetResponseContent(stream);

            _htmlContent = strResponse;
        }
        public ShopRoom[] Extract()
        {
            List<ShopRoom> rooms = new List<ShopRoom>();

            ShopRoomSearchModel crawObj = (ShopRoomSearchModel)JsonSerializer.Deserialize(_htmlContent, typeof(ShopRoomSearchModel));
            if (crawObj != null && crawObj.errorCode == 0)
            {
                List<ShopRoomModel> shops = crawObj.shops;
                foreach (var node in shops)
                {
                    try { 
                    string shopid = node.id;

                    YG.SC.Service.ShopRoomLogic logic = new YG.SC.Service.ShopRoomLogic();
                    bool exist = logic.Exist(shopid);
                    if (exist)
                    {
                        continue;
                    }

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
                    // sr.type = node.houseType;
                    sr.type = node.type;// 房源业态 
                    // sr.Face = node. //朝向， 木有
                    sr.Contacts = node.contact;
                    sr.Mobile = node.mobile;
                    //sr.Rinfo = node.Rinfo;
                    // sr.Rimg = node.Rimg;
                    sr.AddTime = DateTime.Now;
                    sr.source = node.source;
                    sr.from = node.from;
                    sr.fetchDate = node.fetchDate;

                    string roomsearchURL = "http://m.yofang.cn/server/shop/searchInfo?shopId=" + sr.ShopId;
                    var webResponse = new WebAgent().Request(new Uri(roomsearchURL));
                    var stream = webResponse.GetResponseStream();
                    string strResponse = ZipWrapper.GetResponseContent(stream);

                    ShopRoomSearchInfoModel roomModel = (ShopRoomSearchInfoModel)JsonSerializer.Deserialize(strResponse, typeof(ShopRoomSearchInfoModel));
                    if (roomModel != null && roomModel.errorCode == 0)
                    {
                        sr.Rinfo = roomModel.shop.description;
                        sr.type = roomModel.shop.type;
                    }

                    rooms.Add(sr);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return rooms.ToArray();
        }
    }
}