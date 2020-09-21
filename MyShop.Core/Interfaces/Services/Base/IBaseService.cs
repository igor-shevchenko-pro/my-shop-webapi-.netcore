using MyShop.ApiModels;
using MyShop.ApiModels.Interfaces.Base;
using MyShop.ApiModels.Models.Response;
using MyShop.Core.Interfaces.Entities.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Services.Base
{
    public interface IBaseService<TModelAdd, TModelGetFull, TModelGetMin, TEntity, TKey, TSorting>
        where TEntity : IBaseEntity<TKey>
        where TModelAdd : IBaseApiModel<TKey>
        where TModelGetFull : IBaseApiModel<TKey>
        where TModelGetMin : IBaseApiModel<TKey>
        where TSorting : Enum
    {
        Task<TKey> AddAsync(TModelAdd model);
        Task<IEnumerable<TKey>> AddAsync(IEnumerable<TModelAdd> models);

        Task<IBaseApiModel<TKey>> GetAsync(TKey id, TypeModelResponseEnum modelResponsetype, int? langId = null);
        Task<PaginationResponseApiModel<IBaseApiModel<TKey>, TSorting>> GetAsync(int start, int count, List<TSorting> sortings,
            TypeModelResponseEnum modelResponsetype, int? langId = null, string query = null);
        Task<PaginationResponseApiModel<IBaseApiModel<TKey>, TSorting>> GetAsync(string userId, int start, int count, List<TSorting> sortings,
             TypeModelResponseEnum modelResponsetype, int? langId = null, string query = null, IEnumerable<TKey> ignoreIds = null);

        Task UpdateAsync(TModelAdd model, int? langId = null);
        Task UpdateAsync(IEnumerable<TModelAdd> models, int? langId = null);

        Task DeleteAsync(TKey id, int? langId = null);
        Task DeleteAsync(IEnumerable<TKey> ids, int? langId = null);
        Task DeleteAsync(TModelAdd model, int? langId = null);
        Task DeleteAsync(IEnumerable<TModelAdd> models, int? langId = null);

        Task ChangeActivityStatusAsync(TKey id, EntityActivityStatusEnum activityStatus, int? langId = null);
    }
}