using MyShop.ApiModels;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.DL.Repositories.Base;
using System.Threading.Tasks;

namespace MyShop.DL.Repositories
{
    public class BrandRepository : BaseRepository<Brand, int, BrandSortingEnum>, IBrandRepository
    {
        public BrandRepository(IGenericRepository<Brand, int> repository) : base(repository)
        {
        }

        public override async Task AddAsync(Brand entity)
        {
            if (entity.Id == 0)
            {
                entity.Id = await _repository.AnyAsync(x => x.Id > 0) ? (await _repository.MaxAsync(x => x.Id) + 1) : 1;
            }

            await base.AddAsync(entity);
        }
    }
}