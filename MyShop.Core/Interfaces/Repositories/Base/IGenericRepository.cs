using MyShop.Core.Interfaces.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Repositories.Base
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : IBaseEntity<TKey>
    {
        Task CreateAsync(TEntity entity);
        Task CreateAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetAsync(TKey id, Expression<Func<TEntity, bool>> search = null);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<IEnumerable<TEntity>> GetAsync(params Expression<Func<TEntity, bool>>[] filters);

        Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> search = null);

        Task DeleteAsync(TKey id, Expression<Func<TEntity, bool>> search = null);
        Task DeleteAsync(TEntity entity, Expression<Func<TEntity, bool>> search = null);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> search);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> search);
        Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> search);

        Task<TValue> MaxAsync<TValue>(Expression<Func<TEntity, TValue>> search);
    }
}
