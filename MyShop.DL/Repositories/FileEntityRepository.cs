using MyShop.ApiModels;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.DL.Repositories.Base;

namespace MyShop.DL.Repositories
{
    public class FileEntityRepository : BaseRepository<FileEntity, string, FileEntitySortingEnum>, IFileEntityRepository
    {
        public FileEntityRepository(IGenericRepository<FileEntity, string> repository) : base(repository)
        {
        }
    }
}