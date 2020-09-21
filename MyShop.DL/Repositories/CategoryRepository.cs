using MyShop.ApiModels;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.DL.Repositories.Base;
using System.Threading.Tasks;

namespace MyShop.DL.Repositories
{
    public class CategoryRepository : BaseRepository<Category, int, CategorySortingEnum>, ICategoryRepository
    {
        public CategoryRepository(IGenericRepository<Category, int> repository) : base(repository)
        {
        }

        public override async Task AddAsync(Category entity)
        {
            if (entity.Id == 0)
            {
                entity.Id = await _repository.AnyAsync(x => x.Id > 0) ? (await _repository.MaxAsync(x => x.Id) + 1) : 1;
            }

            await base.AddAsync(entity);
        }
    }
}