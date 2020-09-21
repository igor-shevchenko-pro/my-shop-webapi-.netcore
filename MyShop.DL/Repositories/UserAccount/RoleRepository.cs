using MyShop.ApiModels;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.Core.Interfaces.Repositories.UserAccount;
using MyShop.DL.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.DL.Repositories.UserAccount
{
    public class RoleRepository : BaseRepository<Role, string, RoleSortingEnum>, IRoleRepository
    {
        public RoleRepository(IGenericRepository<Role, string> repository) : base(repository)
        {
        }

        protected override IOrderedEnumerable<Role> SortOrderBy(IEnumerable<Role> collection, RoleSortingEnum sorting)
        {
            switch (sorting)
            {
                case RoleSortingEnum.ByCreateAsc:
                    return collection.OrderBy(x => x.Created);

                case RoleSortingEnum.ByCreateDesc:
                    return collection.OrderByDescending(x => x.Created);

                case RoleSortingEnum.ByUpdateAsc:
                    return collection.OrderBy(x => x.Updated);

                case RoleSortingEnum.ByUpdateDesc:
                    return collection.OrderByDescending(x => x.Updated);

                case RoleSortingEnum.ByActivityStatusAsc:
                    return collection.OrderBy(x => x.ActivityStatus);

                case RoleSortingEnum.ByActivityStatusDesc:
                    return collection.OrderByDescending(x => x.ActivityStatus);

                case RoleSortingEnum.ByTitleAsc:
                    return collection.OrderBy(x => x.Title);

                case RoleSortingEnum.ByTitleDesc:
                    return collection.OrderByDescending(x => x.Title);


                default:
                    return collection.OrderByDescending(x => x.Created);
            }
        }

        protected override IOrderedEnumerable<Role> SortThenBy(IOrderedEnumerable<Role> collection, RoleSortingEnum sorting)
        {
            switch (sorting)
            {
                case RoleSortingEnum.ByCreateAsc:
                    return collection.OrderBy(x => x.Created);

                case RoleSortingEnum.ByCreateDesc:
                    return collection.OrderByDescending(x => x.Created);

                case RoleSortingEnum.ByUpdateAsc:
                    return collection.OrderBy(x => x.Updated);

                case RoleSortingEnum.ByUpdateDesc:
                    return collection.OrderByDescending(x => x.Updated);

                case RoleSortingEnum.ByActivityStatusAsc:
                    return collection.OrderBy(x => x.ActivityStatus);

                case RoleSortingEnum.ByActivityStatusDesc:
                    return collection.OrderByDescending(x => x.ActivityStatus);

                case RoleSortingEnum.ByTitleAsc:
                    return collection.OrderBy(x => x.Title);

                case RoleSortingEnum.ByTitleDesc:
                    return collection.OrderByDescending(x => x.Title);


                default:
                    return collection.OrderByDescending(x => x.Created);
            }
        }
    }
}