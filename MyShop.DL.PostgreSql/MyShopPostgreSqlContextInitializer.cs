using Microsoft.EntityFrameworkCore;
using MyShop.Core.Entities.Base;
using MyShop.Core.Entities;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Interfaces.Repositories.Base;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MyShop.Core.Configurations;
using MyShop.Core.Helpers.Configuration;
using MyShop.Core.Extensions.Base;

namespace MyShop.DL.PostgreSql
{
    public class MyShopPostgreSqlContextInitializer : IDatabaseInitializer
    {
        MyShopPostgreSqlContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _dbInitializePermition;

        public MyShopPostgreSqlContextInitializer(IDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _dbInitializePermition = _configuration["initialize_database"];
            _context = context as MyShopPostgreSqlContext;
            _context.Database.Migrate();
        }

        public void Initialize()
        {
            if (_dbInitializePermition != "1")
            {
                return;
            }

            Log.Current.Message("Start database initialize");
            AddBaseEntities<Language, int>();
            AddBaseEntities<Gender, int>();
            AddBaseEntities<Role, string>();
            AddBaseEntities<Category, int>();
            AddBaseEntities<Currency, int>();
            AddBaseEntities<FileEntity, string>();
            Log.Current.Message("Finish database initialize");
        }
        
        private void AddBaseEntities<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>, new()
        {
            var time = DateTime.UtcNow;
            var table = _context.Set<TEntity>();

            foreach (var item in EntitiesHelper.Current.GetItems<TEntity, TKey>())
            {
                if (table.Any(x => x.Id.Equals(item.Id)) == false)
                {
                    var entity = new TEntity();
                    entity.Copy(item);
                    entity.Id = item.Id;
                    entity.Created = time;
                    entity.Updated = time;

                    table.Add(entity);
                }
            }

            _context.SaveChanges();
        }
    }
}
