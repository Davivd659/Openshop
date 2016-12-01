using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.Service.IService;
using YG.SC.DataAccess;

namespace YG.SC.Service.IService
{
    public interface IHotareaService:IYgService
    {
        hotarea[] GetByParentId(int id);
    }
}
