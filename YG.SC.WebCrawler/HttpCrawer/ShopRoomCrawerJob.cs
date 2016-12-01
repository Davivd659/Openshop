using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Quartz;
using Quartz.Impl;

namespace YG.SC.WebCrawler
{
    using YG.SC.DataAccess;
    using YG.SC.WebCrawler;
    using System.Threading.Tasks;
    using System.Threading;

    public class ShopRoomCrawerJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Guid jobkey = Guid.NewGuid();
            //for (int i = 1; i < 1000;i++ )
            //{
            int _pg = 0;
            int _maxpg = 1000;
            var _pageModel = YG.SC.WebCrawler.Models.PageModel.Instance;

            if (_pageModel.PageIndex > _maxpg)
            {
                _pageModel.PageIndex = 0;
            }

            _pageModel.PageIndex = _pageModel.PageIndex + 1;
            _pg = _pageModel.PageIndex;


            ShopRoomCrawler cra = new ShopRoomCrawler(_pg);
            var list = cra.Extract();
            YG.SC.Service.ShopRoomLogic logic = new YG.SC.Service.ShopRoomLogic();
            if (list.Count() > 0)
            {
                logic.AddList(list.ToList());
            }
            //}
        }
    }
}