using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YG.SC.DataAccess;

namespace YG.SC.Service
{
    public class ShopRoomLogic
    {
        public void Add(YG.SC.DataAccess.ShopRoom model)
        {
            using (var db = new YG.SC.DataAccess.Entities())
            {
                db.ShopRoom.Add(model);
                db.SaveChanges();
            }
        }
        public void AddList(List<YG.SC.DataAccess.ShopRoom> rooms)
        {
            using (var db = new YG.SC.DataAccess.Entities())
            {
                db.ShopRoom.AddRange(rooms);
                db.SaveChanges();
            }
        }
        public bool Exist(string shopId)
        {
            using (var db = new YG.SC.DataAccess.Entities())
            {
                var shopExist = db.ShopRoom.Any(m => m.ShopId == shopId);
                return shopExist;
            }
        }
     

    }
}
