using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;

namespace YG.SC.Service.IService
{
    public interface IGoodsService : IYgService
    {
        void InsertUnit(G_Name model);
        void UpdateUnit(G_Name model);
        G_Name GetById(int Id);
    }
}
