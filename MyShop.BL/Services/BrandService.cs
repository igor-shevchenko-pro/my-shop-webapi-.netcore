using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.BL.Services.Base;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace MyShop.BL.Services
{
    public class BrandService : BaseService<BrandAddApiModel, BrandGetFullApiModel, BrandGetMinApiModel, Brand, int, BrandSortingEnum>, IBrandService
    {
        private readonly new IBrandRepository _repository;

        public BrandService(IBrandRepository repository, IDataMapper dataMapper) : base(repository, dataMapper)
        {
            _repository = repository;
        }

        public override async Task<int> AddAsync(BrandAddApiModel model)
        {
            var checkTitle = await _repository.FirstOrDefaultAsync(x => x.Title.ToLower() == model.Title.ToLower());
            if (checkTitle != null && checkTitle.Title.ToLower() == model.Title.ToLower())
                throw new Exception("Duplicate name");

            var checkAlias = await _repository.FirstOrDefaultAsync(x => x.Alias.ToLower() == model.Alias.ToLower());
            if (checkAlias != null && checkAlias.Alias.ToLower() == model.Alias.ToLower())
                throw new Exception("Duplicate URL");

            return await base.AddAsync(model);
        }

        public override async Task UpdateAsync(BrandAddApiModel model, int? langId = null)
        {
            var checkTitle = await _repository.FirstOrDefaultAsync(x => x.Title.ToLower() == model.Title.ToLower());
            if (checkTitle != null && checkTitle.Title.ToLower() == model.Title.ToLower() && checkTitle.Id != model.Id)
                throw new Exception("Duplicate name");

            var checkAlias = await _repository.FirstOrDefaultAsync(x => x.Alias.ToLower() == model.Alias.ToLower());
            if (checkAlias != null && checkAlias.Alias.ToLower() == model.Alias.ToLower() && checkAlias.Id != model.Id)
                throw new Exception("Duplicate URL");

            await base.UpdateAsync(model, langId);
        }
    }
}