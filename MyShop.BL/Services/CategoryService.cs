using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.BL.Services.Base;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.BL.Services
{
    public class CategoryService : BaseService<CategoryAddApiModel, CategoryGetFullApiModel, CategoryGetMinApiModel, Category, int, CategorySortingEnum>, ICategoryService
    {
        private readonly IImageGalleryService _imageGalleryService;

        public CategoryService(ICategoryRepository repository, IImageGalleryService imageGalleryService, IDataMapper dataMapper) 
            : base(repository, dataMapper)
        {
            _imageGalleryService = imageGalleryService;
        }

        public override async Task<int> AddAsync(CategoryAddApiModel model)
        {
            var checkTitle = await _repository.FirstOrDefaultAsync(x => x.Title.ToLower() == model.Title.ToLower());
            if (checkTitle != null && checkTitle.Title.ToLower() == model.Title.ToLower())
                throw new Exception("Duplicate name");

            var checkAlias = await _repository.FirstOrDefaultAsync(x => x.Alias.ToLower() == model.Alias.ToLower());
            if (checkAlias != null && checkAlias.Alias.ToLower() == model.Alias.ToLower())
                throw new Exception("Duplicate URL");

            return await base.AddAsync(model);
        }

        public override async Task UpdateAsync(CategoryAddApiModel model, int? langId = null)
        {
            var checkTitle = await _repository.FirstOrDefaultAsync(x => x.Title.ToLower() == model.Title.ToLower());
            if (checkTitle != null && checkTitle.Title.ToLower() == model.Title.ToLower() && checkTitle.Id != model.Id)
                throw new Exception("Duplicate name");

            var checkAlias = await _repository.FirstOrDefaultAsync(x => x.Alias.ToLower() == model.Alias.ToLower());
            if (checkAlias != null && checkAlias.Alias.ToLower() == model.Alias.ToLower() && checkAlias.Id != model.Id)
                throw new Exception("Duplicate URL");

            await base.UpdateAsync(model, langId);
        }

        public override async Task DeleteAsync(int id, int? langId = null)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.Id == id && x.LanguageId == langId);
            if(entity != null && entity.ImageGalleryId != null)
            {
                await _imageGalleryService.DeleteAsync(entity.ImageGalleryId);
            }

            await base.DeleteAsync(id, langId);
        }

        public override async Task ChangeActivityStatusAsync(int id, EntityActivityStatusEnum status, int? langId = null)
        {
            // set same status for nested categories
            var entities = (await _repository.WhereAsync(x => x.ParentCategoryId == id && 
                                                              x.ParentCategoryLanguageId == langId))?.ToList();
            if (entities != null && entities.Count > 0)
            {
                foreach (var item in entities)
                {
                    // second level of nested categories
                    var entitiesNext = (await _repository.WhereAsync(x => x.ParentCategoryId == item.Id &&
                                                                          x.ParentCategoryLanguageId == langId))?.ToList();
                    if (entitiesNext != null && entitiesNext.Count > 0)
                    {
                        foreach (var itemNext in entitiesNext)
                        {
                            await base.ChangeActivityStatusAsync(itemNext.Id, status, langId);
                        }
                    }

                    await base.ChangeActivityStatusAsync(item.Id, status, langId);
                }
            }

            await base.ChangeActivityStatusAsync(id, status, langId);
        }
    }
}