using MyShop.Core.Entities.Base;
using MyShop.Core.Interfaces.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Repositories.Base
{
    public interface IBaseRepository<TEntity, TKey, TSorting> 
        where TEntity : IBaseEntity<TKey>
        where TSorting : Enum
    {
        Task AddAsync(TEntity entity);
        Task AddAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetAsync(TKey id, Expression<Func<TEntity, bool>> search = null);
        Task<CollectionOfEntities<TEntity, TSorting>> GetAsync(int start, int count, List<TSorting> sorting);
        Task<CollectionOfEntities<TEntity, TSorting>> GetAsync(int start, int count, List<TSorting> sortings, 
            params Expression<Func<TEntity, bool>>[] filters);

        Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> search = null);

        Task DeleteAsync(TKey id, Expression<Func<TEntity, bool>> search = null);
        Task DeleteAsync(TEntity item, Expression<Func<TEntity, bool>> search = null);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> search);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> search);
        Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> search);

        Task<TValue> MaxAsync<TValue>(Expression<Func<TEntity, TValue>> search);
    }
}
