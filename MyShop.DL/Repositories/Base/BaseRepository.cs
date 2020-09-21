using MyShop.ApiModels;
using MyShop.Core.Entities.Base;
using MyShop.Core.Interfaces.Entities.Base;
using MyShop.Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyShop.DL.Repositories.Base
{
    public class BaseRepository<TEntity, TKey, TSorting> : IBaseRepository<TEntity, TKey, TSorting>
            where TEntity : IBaseEntity<TKey>
            where TSorting : Enum
    {
        protected IGenericRepository<TEntity, TKey> _repository;

        public BaseRepository(IGenericRepository<TEntity, TKey> repository)
        {
            _repository = repository;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _repository.CreateAsync(entity);
        }

        public virtual async Task AddAsync(IEnumerable<TEntity> entities)
        {
            await _repository.CreateAsync(entities);
        }

       
        public virtual async Task<TEntity> GetAsync(TKey id, Expression<Func<TEntity, bool>> search = null)
        {
            return await _repository.GetAsync(id, search);
        }
        
        public virtual async Task<CollectionOfEntities<TEntity, TSorting>> GetAsync(int start, int count, List<TSorting> sortings)
        {
            var result = new CollectionOfEntities<TEntity, TSorting>();
            var entities = await _repository.GetAsync();

            result.Total = entities?.Count() ?? 0;
            result.Start = start;
            result.EntitySortings = sortings;

            var x = count == 0 ? result.Total - start : count;

            result.Entities = SortCollection(entities, sortings).Skip(start).Take(x);
            result.Count = result.Entities.Count();

            return result;
        }
        
        public virtual async Task<CollectionOfEntities<TEntity, TSorting>> GetAsync(int start, int count, List<TSorting> sortings,
            params Expression<Func<TEntity, bool>>[] filters)
        {
            var result = new CollectionOfEntities<TEntity, TSorting>();
            var entities = await _repository.GetAsync(filters);

            result.Total = entities?.Count() ?? 0;
            result.Start = start;
            result.EntitySortings = sortings;

            var x = count == 0 ? result.Total - start : count;

            result.Entities = SortCollection(entities, sortings).Skip(start).Take(x);
            result.Count = result.Entities.Count();

            return result;
        }


        public virtual async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> search = null)
        {
            await _repository.UpdateAsync(entity, search);
        }


        public virtual async Task DeleteAsync(TKey id, Expression<Func<TEntity, bool>> search = null)
        {
            await _repository.DeleteAsync(id, search);
        }

        public virtual async Task DeleteAsync(TEntity entity, Expression<Func<TEntity, bool>> search = null)
        {
            await _repository.DeleteAsync(entity, search);
        }



        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> search)
        {
            return await _repository.AnyAsync(search);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> search)
        {
            return await _repository.FirstOrDefaultAsync(search);
        }

        public virtual async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> search)
        {
            return await _repository.WhereAsync(search);
        }


        public virtual async Task<TValue> MaxAsync<TValue>(Expression<Func<TEntity, TValue>> search)
        {
            return await _repository.MaxAsync(search);
        }


        protected IEnumerable<TEntity> SortCollection(IEnumerable<TEntity> collection, List<TSorting> sortings)
        {
            if (sortings == null || sortings.Count == 0) return collection;

            var sortedCollection = SortOrderBy(collection, sortings.First());
            for (int i = 1; i < sortings.Count; i++)
            {
                sortedCollection = SortThenBy(sortedCollection, sortings[i]);
            }

            return sortedCollection;
        }

        protected virtual IOrderedEnumerable<TEntity> SortOrderBy(IEnumerable<TEntity> collection, TSorting sorting)
        {
            switch (sorting)
            {
                case EntitySortingEnum.ByCreateAsc:
                    return collection.OrderBy(x => x.Created);

                case EntitySortingEnum.ByCreateDesc:
                    return collection.OrderByDescending(x => x.Created);

                case EntitySortingEnum.ByUpdateAsc:
                    return collection.OrderBy(x => x.Updated);

                case EntitySortingEnum.ByUpdateDesc:
                    return collection.OrderByDescending(x => x.Updated);

                case EntitySortingEnum.ByActivityStatusAsc:
                    return collection.OrderBy(x => x.ActivityStatus);

                case EntitySortingEnum.ByActivityStatusDesc:
                    return collection.OrderByDescending(x => x.ActivityStatus);


                default:
                    return collection.OrderByDescending(x => x.Created);
            }
        }
        
        protected virtual IOrderedEnumerable<TEntity> SortThenBy(IOrderedEnumerable<TEntity> collection, TSorting sorting)
        {
            switch (sorting)
            {
                case EntitySortingEnum.ByCreateAsc:
                    return collection.OrderBy(x => x.Created);

                case EntitySortingEnum.ByCreateDesc:
                    return collection.OrderByDescending(x => x.Created);

                case EntitySortingEnum.ByUpdateAsc:
                    return collection.OrderBy(x => x.Updated);

                case EntitySortingEnum.ByUpdateDesc:
                    return collection.OrderByDescending(x => x.Updated);

                case EntitySortingEnum.ByActivityStatusAsc:
                    return collection.OrderBy(x => x.ActivityStatus);

                case EntitySortingEnum.ByActivityStatusDesc:
                    return collection.OrderByDescending(x => x.ActivityStatus);


                default:
                    return collection.OrderByDescending(x => x.Created);
            }
        }
    }
}
