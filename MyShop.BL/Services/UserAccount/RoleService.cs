using MyShop.ApiModels;
using MyShop.ApiModels.Interfaces.Base;
using MyShop.ApiModels.Models;
using MyShop.ApiModels.Models.Response;
using MyShop.BL.Services.Base;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Helpers;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Repositories.UserAccount;
using MyShop.Core.Interfaces.Services.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.BL.Services.UserAccount
{
    public class RoleService : BaseService<RoleAddApiModel, RoleGetFullApiModel, RoleGetMinApiModel, Role, string, RoleSortingEnum>, IRoleService
    {
        private readonly new IRoleRepository _repository;

        public RoleService(IRoleRepository repository, IDataMapper dataMapper) : base(repository, dataMapper)
        {
            _repository = repository;
        }

        public override async Task<string> AddAsync(RoleAddApiModel model)
        {
            var checkTitle = await _repository.FirstOrDefaultAsync(x => x.Title.ToLower() == model.Title.ToLower());
            if (checkTitle != null && checkTitle.Title.ToLower() == model.Title.ToLower())
                throw new Exception("Duplicate name");

            return await base.AddAsync(model);
        }

        public override async Task UpdateAsync(RoleAddApiModel model, int? langId = null)
        {
            var checkTitle = await _repository.FirstOrDefaultAsync(x => x.Title.ToLower() == model.Title.ToLower());
            if (checkTitle != null && checkTitle.Title.ToLower() == model.Title.ToLower() && checkTitle.Id != model.Id)
                throw new Exception("Duplicate name");

            await base.UpdateAsync(model, langId);
        }

        // Get all roles with nested managers. Without any other types of users
        public async Task<PaginationResponseApiModel<IBaseApiModel<string>, RoleSortingEnum>> GetAllWithNestedManagersAsync(int start, 
            int count, List<RoleSortingEnum> sorting, string query = null)
        {
            var result = await base.GetAsync(start, count, sorting, TypeModelResponseEnum.GetFullApiModel, null, query);

            if (result != null && result.Count > 0)
            {
                var tempRoles = result.Models as IEnumerable<RoleGetFullApiModel>;

                foreach (var item in tempRoles.ToList())
                {
                    if(item.Title == RoleHelper.Current.Customer.Title || item.Title == RoleHelper.Current.User.Title)
                    {
                        // set empty collection
                        item.Users = new List<UserGetMinApiModel>();
                    }
                }

                result.Models = tempRoles;
            }

            return result;
        }
    }
}
