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
    public class SupplierService : BaseService<SupplierAddApiModel, SupplierGetFullApiModel, SupplierGetMinApiModel, Supplier, string, SupplierSortingEnum>, ISupplierService
    {
        public SupplierService(ISupplierRepository repository, IDataMapper dataMapper) : base(repository, dataMapper)
        {
        }

        public override async Task<string> AddAsync(SupplierAddApiModel model)
        {
            var checkTitle = await _repository.FirstOrDefaultAsync(x => x.Title.ToLower() == model.Title.ToLower());
            if (checkTitle != null && checkTitle.Title.ToLower() == model.Title.ToLower())
                throw new Exception("Duplicate name");

            return await base.AddAsync(model);
        }

        public override async Task UpdateAsync(SupplierAddApiModel model, int? langId = null)
        {
            var checkTitle = await _repository.FirstOrDefaultAsync(x => x.Title.ToLower() == model.Title.ToLower());
            if (checkTitle != null && checkTitle.Title.ToLower() == model.Title.ToLower() && checkTitle.Id != model.Id)
                throw new Exception("Duplicate name");

            await base.UpdateAsync(model, langId);
        }
    }
}
