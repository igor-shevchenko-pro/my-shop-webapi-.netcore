using Microsoft.EntityFrameworkCore;
using MyShop.ApiModels;
using MyShop.Core.Entities.Base;
using MyShop.Core.Extensions.Base;
using MyShop.Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyShop.DL.PostgreSql.Repositories
{
    public class PostgreSqlRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
           where TEntity : BaseEntity<TKey>
    {
        private DbSet<TEntity> _entities { get; set; }
        private DbContext _context { get; set; }

        public PostgreSqlRepository(IDbContext context)
        {
            _context = context as DbContext;
            _entities = _context.Set<TEntity>();        
        }


        public async Task CreateAsync(TEntity entity)
        {
            entity.TrySetId();
            entity.Created = DateTime.UtcNow;
            entity.Updated = entity.Created;
            entity.ActivityStatus = entity.ActivityStatus ?? EntityActivityStatusEnum.Active;

            _entities.Add(entity);

            await _context.SaveChangesAsync();
        }

        public async Task CreateAsync(IEnumerable<TEntity> entities)
        {
            entities?.ToList().ForEach(x => 
            {
                x.TrySetId();
                x.Created = DateTime.UtcNow;
                x.Updated = x.Created;
                x.ActivityStatus = x.ActivityStatus ?? EntityActivityStatusEnum.Active;
            });

            _entities.AddRange(entities);

            await _context.SaveChangesAsync();
        }

        
        public async Task<TEntity> GetAsync(TKey id, Expression<Func<TEntity, bool>> search = null)
        {
            if (search == null)
            {
                search = x => x.Id.Equals(id);
            }

            return await _entities.FirstOrDefaultAsync(search);
        }
        
        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _entities.ToListAsync();
        }
        
        public async Task<IEnumerable<TEntity>> GetAsync(params Expression<Func<TEntity, bool>>[] filters)
        {
            IEnumerable<TEntity> entities = null;

            if (filters != null && filters.Length > 0)
            {
                entities = await _entities.Where(filters[0]).ToListAsync();

                for (int i = 1; i < filters.Length; i++)
                {
                    Func<TEntity, bool> filter = filters[i].Compile();
                    entities = entities.Where(filter);
                }
            }
            else
            {
                entities = await _entities.ToListAsync();
            }

            return entities;
        }


        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> search = null)
        {
            if (search == null)
            {
                search = x => x.Id.Equals(entity.Id);
            }

            var first = await _entities.FirstOrDefaultAsync(search);
            if (first == null)
            {
                throw new ArgumentException($"Entity is not found");
            }

            if (first != entity)
            {
                first.Copy(entity);
                first.Updated = DateTime.UtcNow;
                _entities.Update(first);
            }
            else
            {
                entity.Updated = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(TKey id, Expression<Func<TEntity, bool>> search = null)
        {
            if (search == null)
            {
                search = x => x.Id.Equals(id);
            }

            var first = await _entities.FirstOrDefaultAsync(search);
            if (first == null)
            {
                throw new ArgumentException($"Entity is not found");
            }

            _entities.Remove(first);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity, Expression<Func<TEntity, bool>> search = null)
        {
            if (search == null)
            {
                search = x => x.Id.Equals(entity.Id);
            }

            var first = await _entities.FirstOrDefaultAsync(search);
            if (first == null)
            {
                throw new ArgumentException($"Entity is not found");
            }

            _entities.Remove(first);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> search)
        {
            return await _entities.AnyAsync(search);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> search)
        {
            return await _entities.FirstOrDefaultAsync(search);
        }

        public Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> search)
        {
            Func<TEntity, bool> filter = search.Compile();
            return Task.FromResult(_entities.Where(filter));
        }


        public async Task<TValue> MaxAsync<TValue>(Expression<Func<TEntity, TValue>> search)
        {
            return await _entities.MaxAsync(search);
        }
    }
}
