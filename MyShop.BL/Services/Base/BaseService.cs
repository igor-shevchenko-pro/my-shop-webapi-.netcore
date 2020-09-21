using MyShop.ApiModels;
using MyShop.ApiModels.Interfaces.Base;
using MyShop.ApiModels.Models.Response;
using MyShop.Core.Entities.Base;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Entities.Base;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.Core.Interfaces.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyShop.BL.Services.Base
{
    public abstract class BaseService<TModelAdd, TModelGetFull, TModelGetMin, TEntity, TKey, TSorting> 
        : IBaseService<TModelAdd, TModelGetFull, TModelGetMin, TEntity, TKey, TSorting>
            where TEntity : IBaseEntity<TKey>
            where TModelAdd : IBaseApiModel<TKey>
            where TModelGetFull : IBaseApiModel<TKey>
            where TModelGetMin : IBaseApiModel<TKey>
            where TSorting : Enum
    {

        protected IBaseRepository<TEntity, TKey, TSorting> _repository;
        protected IDataMapper _dataMapper;

        public BaseService(IBaseRepository<TEntity, TKey, TSorting> repository, IDataMapper dataMapper)
        {
            _dataMapper = dataMapper;
            _repository = repository;
        }

        public virtual async Task<TKey> AddAsync(TModelAdd model)
        {
            var entity = _dataMapper.Parse<TModelAdd, TEntity>(model);
            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public virtual async Task<IEnumerable<TKey>> AddAsync(IEnumerable<TModelAdd> models)
        {
            var entities = _dataMapper.Parse<IEnumerable<TModelAdd>, IEnumerable<TEntity>>(models);
            await _repository.AddAsync(entities);
            var entitiesIds = entities.Select(x => x.Id);
            return entitiesIds;
        }


        public virtual async Task<IBaseApiModel<TKey>> GetAsync(TKey id, TypeModelResponseEnum modelResponseType, int? langId = null)
        {
            var type = typeof(TEntity);
            Expression<Func<TEntity, bool>> filter = null;

            if (type.GetInterface(typeof(IBaseLanguageEntity<TKey>).Name) != null && langId != null)
            {
                filter = x => ((IBaseLanguageEntity<TKey>)x).LanguageId.Equals(langId) && x.Id.Equals(id);
            }

            var entity = await _repository.GetAsync(id, filter);
            var model = GetParsedResult(entity, modelResponseType);

            return model;
        }
        
        public virtual async Task<PaginationResponseApiModel<IBaseApiModel<TKey>, TSorting>> GetAsync(int start, int count, 
            List<TSorting> sortings, TypeModelResponseEnum modelResponseType, int? langId = null, string query = null)
        {
            CollectionOfEntities<TEntity, TSorting> pagination = null;
            var type = typeof(TEntity);
            List<Expression<Func<TEntity, bool>>> filters = new List<Expression<Func<TEntity, bool>>>();

            if (type.GetInterface(typeof(IBaseLanguageEntity<TKey>).Name) != null && langId != null)
            {
                filters.Add(x => ((IBaseLanguageEntity<TKey>)x).LanguageId.Equals(langId));
            }
            if (!string.IsNullOrEmpty(query) && type.GetInterface(typeof(IBaseManualEntity<TKey>).Name) != null)
            {
                var q = query.ToLower();
                filters.Add(x => ((IBaseManualEntity<TKey>)x).Title != null && ((IBaseManualEntity<TKey>)x).Title.ToLower().Contains(q));
            }

            if (filters.Count > 0) pagination = await _repository.GetAsync(start, count, sortings, filters?.ToArray());
            else pagination = await _repository.GetAsync(start, count, sortings);

            var result = GetParsedPaginationResult(pagination.Entities, modelResponseType, pagination.Total, pagination.Start,
                                                   pagination.Count, pagination.EntitySortings);

            return result;
        }
        
        public virtual async Task<PaginationResponseApiModel<IBaseApiModel<TKey>, TSorting>> GetAsync(string userId, int start, 
            int count, List<TSorting> sortings, TypeModelResponseEnum modelResponseType, int? langId = null, string query = null, 
            IEnumerable<TKey> ignoreIds = null)
        {
            var type = typeof(TEntity);
            List<Expression<Func<TEntity, bool>>> filters = new List<Expression<Func<TEntity, bool>>>();

            if (type.GetInterface(typeof(IBaseLanguageEntity<TKey>).Name) != null && langId != null)
            {
                filters.Add(x => ((IBaseLanguageEntity<TKey>)x).LanguageId.Equals(langId));
            }
            if (!string.IsNullOrEmpty(userId) && type.GetInterface(typeof(IUserEntity).Name) != null)
            {
                filters.Add(x => ((IUserEntity)x).UserId == userId);
            }
            if (!string.IsNullOrEmpty(query) && type.GetInterface(typeof(IBaseManualEntity<TKey>).Name) != null)
            {
                var q = query.ToLower();
                filters.Add(x => ((IBaseManualEntity<TKey>)x).Title != null && ((IBaseManualEntity<TKey>)x).Title.ToLower().Contains(q));
            }
            if (ignoreIds != null && ignoreIds.Count() > 0)
            {
                filters.Add(x => !ignoreIds.Any(id => id.Equals(x.Id)));
            }

            var pagination = await _repository.GetAsync(start, count, sortings, filters.ToArray());

            var result = GetParsedPaginationResult(pagination.Entities, modelResponseType, pagination.Total, pagination.Start,
                                                   pagination.Count, pagination.EntitySortings);

            return result;
        }


        public virtual async Task UpdateAsync(TModelAdd model, int? langId = null)
        {
            var type = typeof(TEntity);
            Expression<Func<TEntity, bool>> filter = null;

            if (type.GetInterface(typeof(IBaseLanguageEntity<TKey>).Name) != null && langId != null)
            {
                filter = x => ((IBaseLanguageEntity<TKey>)x).LanguageId.Equals(langId) && x.Id.Equals(model.Id);
            }

            var entity = _dataMapper.Parse<TModelAdd, TEntity>(model);
            await _repository.UpdateAsync(entity, filter);
        }

        public virtual async Task UpdateAsync(IEnumerable<TModelAdd> models, int? langId = null)
        {
            foreach(var item in models)
            {
                await UpdateAsync(item, langId);
            }
        }


        public virtual async Task DeleteAsync(TKey id, int? langId = null)
        {
            var type = typeof(TEntity);
            Expression<Func<TEntity, bool>> filter = null;

            if (type.GetInterface(typeof(IBaseLanguageEntity<TKey>).Name) != null && langId != null)
            {
                filter = x => ((IBaseLanguageEntity<TKey>)x).LanguageId.Equals(langId) && x.Id.Equals(id);
            }

            await _repository.DeleteAsync(id, filter);
        }

        public virtual async Task DeleteAsync(IEnumerable<TKey> ids, int? langId = null)
        {
            foreach(var item in ids)
            {
                await DeleteAsync(item, langId);
            }
        }

        public virtual async Task DeleteAsync(TModelAdd model, int? langId = null)
        {
            var type = typeof(TEntity);
            Expression<Func<TEntity, bool>> filter = null;

            if (type.GetInterface(typeof(IBaseLanguageEntity<TKey>).Name) != null && langId != null)
            {
                filter = x => ((IBaseLanguageEntity<TKey>)x).LanguageId.Equals(langId) && x.Id.Equals(model.Id);
            }

            var entity = _dataMapper.Parse<TModelAdd, TEntity>(model);
            await _repository.DeleteAsync(entity, filter);
        }

        public virtual async Task DeleteAsync(IEnumerable<TModelAdd> models, int? langId = null)
        {
            foreach(var item in models)
            {
                await DeleteAsync(item, langId);
            }
        }


        public virtual async Task ChangeActivityStatusAsync(TKey id, EntityActivityStatusEnum activityStatus, int? langId = null)
        {
            var type = typeof(TEntity);
            Expression<Func<TEntity, bool>> filter = null;

            if (type.GetInterface(typeof(IBaseLanguageEntity<TKey>).Name) != null && langId != null)
            {
                filter = x => ((IBaseLanguageEntity<TKey>)x).LanguageId.Equals(langId) && x.Id.Equals(id);
            }

            var entity = await _repository.GetAsync(id, filter);
            entity.ActivityStatus = activityStatus;

            await _repository.UpdateAsync(entity);
        }


        protected IBaseApiModel<TKey> GetParsedResult(TEntity entity, TypeModelResponseEnum modelResponseType)
        {
            switch (modelResponseType)
            {
                case TypeModelResponseEnum.GetFullApiModel:
                    return _dataMapper.Parse<TEntity, TModelGetFull>(entity);

                case TypeModelResponseEnum.GetMinApiModel:
                    return _dataMapper.Parse<TEntity, TModelGetMin>(entity);


                default:
                    return _dataMapper.Parse<TEntity, TModelGetFull>(entity);
            }
        }

        protected PaginationResponseApiModel<IBaseApiModel<TKey>, TSorting> GetParsedPaginationResult(IEnumerable<TEntity> entities,
            TypeModelResponseEnum modelResponseType, int total, int start, int count, List<TSorting> sortings)
        {
            dynamic models; 

            switch (modelResponseType)
            {
                case TypeModelResponseEnum.GetFullApiModel:
                    models = _dataMapper.ParseCollection<TEntity, TModelGetFull>(entities);
                    break;
                case TypeModelResponseEnum.GetMinApiModel:
                    models = _dataMapper.ParseCollection<TEntity, TModelGetMin>(entities);
                    break;

                default:
                    models = _dataMapper.ParseCollection<TEntity, TModelGetFull>(entities);
                    break;
            }

            var result = new PaginationResponseApiModel<IBaseApiModel<TKey>, TSorting>()
            {
                Total = total,
                Start = start,
                Count = count,
                EntitySortings = sortings,
                Models = models,
            };

            return result;
        }
    }
}
