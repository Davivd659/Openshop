using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Service
{
    public interface IFileService : IYgService
    {
        DataAccess.C_File GetById(int p);

        void Update(DataAccess.C_File fDb);
    }
}
