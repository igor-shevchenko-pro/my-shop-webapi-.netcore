using MyShop.Core.Entities;
using MyShop.Core.Entities.Base;
using MyShop.Core.Entities.UserAccount;
using System.Collections.Generic;

namespace MyShop.Core.Helpers.Configuration
{
    public class EntitiesHelper
    {
        private static readonly EntitiesHelper _instance = new EntitiesHelper();
        public static EntitiesHelper Current => _instance;

        private EntitiesHelper()
        {
        }

        public RoleHelper Roles => RoleHelper.Current;
        public GenderHelper Genders => GenderHelper.Current;
        public LanguageHelper Languages => LanguageHelper.Current;
        public CategoryHelper Categories => CategoryHelper.Current;
        public CurrencyHelper Currencies => CurrencyHelper.Current;
        public FileEntityHelper FileEntities => FileEntityHelper.Current;

        public IEnumerable<TEntity> GetItems<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (typeof(TEntity).Name == nameof(Role)) return (IEnumerable<TEntity>)Roles.Roles;
            if (typeof(TEntity).Name == nameof(Gender)) return (IEnumerable<TEntity>)Genders.FullCollection;
            if (typeof(TEntity).Name == nameof(Language)) return (IEnumerable<TEntity>)Languages.Collection;
            if (typeof(TEntity).Name == nameof(Category)) return (IEnumerable<TEntity>)Categories.Collection;
            if (typeof(TEntity).Name == nameof(Currency)) return (IEnumerable<TEntity>)Currencies.Collection;
            if (typeof(TEntity).Name == nameof(FileEntity)) return (IEnumerable<TEntity>)FileEntities.Collection;

            return new List<TEntity>();
        }
    }
}
