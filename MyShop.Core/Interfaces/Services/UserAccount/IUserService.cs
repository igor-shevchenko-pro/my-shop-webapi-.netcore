using MyShop.ApiModels;
using MyShop.ApiModels.Interfaces.Base;
using MyShop.ApiModels.Models;
using MyShop.ApiModels.Models.Response;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Interfaces.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Services.UserAccount
{
    public interface IUserService : IBaseService<UserAddApiModel, UserGetFullApiModel, UserGetMinApiModel, User, string, UserSortingEnum>
    {
        Task<PaginationResponseApiModel<IBaseApiModel<string>, UserSortingEnum>> GetManagersAsync(int start, int count, 
            List<UserSortingEnum> sortings, TypeModelResponseEnum modelResponseType, int langId);
        Task SetDefaultPhotoAsync(string id);
    }
}