using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Services
{
    public interface IFileEntityService : IBaseService<FileEntityAddApiModel, FileEntityGetFullApiModel, FileEntityGetMinApiModel, FileEntity, string, FileEntitySortingEnum>
    {
        Task<List<string>> AddRange(FileEntitiesAddApiModel model);
        Task DeleteRange(List<string> ids);
        Task<FileEntityGetFullApiModel> GetDataAsync(string id);
        Task<IEnumerable<FileEntityGetMinApiModel>> GetRangeAsync(IEnumerable<string> fileEntityIds);
    }
}