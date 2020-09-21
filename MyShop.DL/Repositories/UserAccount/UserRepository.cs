using MyShop.ApiModels;
using MyShop.Core.Entities.Base;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Helpers;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.Core.Interfaces.Repositories.ManyToMany;
using MyShop.Core.Interfaces.Repositories.UserAccount;
using MyShop.DL.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.DL.Repositories.UserAccount
{
    public class UserRepository : BaseRepository<User, string, UserSortingEnum>, IUserRepository
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRepository(IGenericRepository<User, string> repository, IUserRoleRepository userRoleRepository) 
            : base(repository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<CollectionOfEntities<User, UserSortingEnum>> GetManagersAsync(int start, int count, 
            List<UserSortingEnum> sortings, int langId)
        {
            var result = new CollectionOfEntities<User, UserSortingEnum>();
            List<User> entities = null;

            var models = (await _userRoleRepository.WhereAsync(x => x.RoleId != RoleHelper.Current.User.Id &&
                                                                    x.RoleId != RoleHelper.Current.Customer.Id))?.ToList();
            if (models != null && models.Count > 0)
            {
                entities = models.Select(x => x.User)?.ToList()
                                 .Where(y => y.UserProfile.LanguageId == langId)?.ToList();
            }

            entities = entities?.Distinct().ToList();

            result.Total = entities?.Count() ?? 0;
            result.Start = start;
            result.EntitySortings = sortings;

            var x = count == 0 ? result.Total - start : count;

            result.Entities = SortCollection(entities ?? new List<User>(), sortings).Skip(start).Take(x);
            result.Count = result.Entities.Count();

            return result;
        }


        protected override IOrderedEnumerable<User> SortOrderBy(IEnumerable<User> collection, UserSortingEnum sorting)
        {
            switch (sorting)
            {
                case UserSortingEnum.ByCreateAsc:
                    return collection.OrderBy(x => x.Created);

                case UserSortingEnum.ByCreateDesc:
                    return collection.OrderByDescending(x => x.Created);

                case UserSortingEnum.ByUpdateAsc:
                    return collection.OrderBy(x => x.Updated);

                case UserSortingEnum.ByUpdateDesc:
                    return collection.OrderByDescending(x => x.Updated);

                case UserSortingEnum.ByActivityStatusAsc:
                    return collection.OrderBy(x => x.ActivityStatus);

                case UserSortingEnum.ByActivityStatusDesc:
                    return collection.OrderByDescending(x => x.ActivityStatus);


                default:
                    return collection.OrderByDescending(x => x.Created);
            }
        }

        protected override IOrderedEnumerable<User> SortThenBy(IOrderedEnumerable<User> collection, UserSortingEnum sorting)
        {
            switch (sorting)
            {
                case UserSortingEnum.ByCreateAsc:
                    return collection.OrderBy(x => x.Created);

                case UserSortingEnum.ByCreateDesc:
                    return collection.OrderByDescending(x => x.Created);

                case UserSortingEnum.ByUpdateAsc:
                    return collection.OrderBy(x => x.Updated);

                case UserSortingEnum.ByUpdateDesc:
                    return collection.OrderByDescending(x => x.Updated);

                case UserSortingEnum.ByActivityStatusAsc:
                    return collection.OrderBy(x => x.ActivityStatus);

                case UserSortingEnum.ByActivityStatusDesc:
                    return collection.OrderByDescending(x => x.ActivityStatus);


                default:
                    return collection.OrderByDescending(x => x.Created);
            }
        }
    }
}
