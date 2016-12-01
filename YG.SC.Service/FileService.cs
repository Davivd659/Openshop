using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YG.SC.DataAccess;
using YG.SC.Repository;

namespace YG.SC.Service
{
    public class FileService : IFileService
    {
        private readonly IRepository<C_File> _FileRepository;

        public FileService(IRepository<C_File> FileRepository)
        {
            _FileRepository = FileRepository;
        }

        public void Dispose()
        {
            _FileRepository.Dispose();
        }

        public C_File GetById(int id)
        {
            return _FileRepository.GetById(id);
        }


        public void Update(C_File fDb)
        {
            _FileRepository.Update(fDb);
            _FileRepository.SaveChanges();
        }
    }
}
