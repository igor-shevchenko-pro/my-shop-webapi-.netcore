using MyShop.ApiModels;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.DL.Repositories.Base;

namespace MyShop.DL.Repositories
{
    public class ModificationRepository : BaseRepository<Modification, string, ModificationSortingEnum>, IModificationRepository
    {
        public ModificationRepository(IGenericRepository<Modification, string> repository) : base(repository)
        {
        }
    }
}