using MyShop.ApiModels;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.DL.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.DL.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier, string, SupplierSortingEnum>, ISupplierRepository
    {
        public SupplierRepository(IGenericRepository<Supplier, string> repository) : base(repository)
        {
        }

        protected override IOrderedEnumerable<Supplier> SortOrderBy(IEnumerable<Supplier> collection, SupplierSortingEnum sorting)
        {
            switch (sorting)
            {
                case SupplierSortingEnum.ByCreateAsc:
                    return collection.OrderBy(x => x.Created);

                case SupplierSortingEnum.ByCreateDesc:
                    return collection.OrderByDescending(x => x.Created);

                case SupplierSortingEnum.ByUpdateAsc:
                    return collection.OrderBy(x => x.Updated);

                case SupplierSortingEnum.ByUpdateDesc:
                    return collection.OrderByDescending(x => x.Updated);

                case SupplierSortingEnum.ByActivityStatusAsc:
                    return collection.OrderBy(x => x.ActivityStatus);

                case SupplierSortingEnum.ByActivityStatusDesc:
                    return collection.OrderByDescending(x => x.ActivityStatus);

                case SupplierSortingEnum.ByTitleAsc:
                    return collection.OrderBy(x => x.Title);

                case SupplierSortingEnum.ByTitleDesc:
                    return collection.OrderByDescending(x => x.Title);

                case SupplierSortingEnum.ByEmailAsc:
                    return collection.OrderBy(x => x.Email);

                case SupplierSortingEnum.ByEmailDesc:
                    return collection.OrderByDescending(x => x.Email);

                case SupplierSortingEnum.ByExtraEmailAsc:
                    return collection.OrderBy(x => x.EmailExtra);

                case SupplierSortingEnum.ByExtraEmailDesc:
                    return collection.OrderByDescending(x => x.EmailExtra);

                case SupplierSortingEnum.ByPhoneAsc:
                    return collection.OrderBy(x => x.PhoneNumber);

                case SupplierSortingEnum.ByPhoneDesc:
                    return collection.OrderByDescending(x => x.PhoneNumber);

                case SupplierSortingEnum.ByExtraPhoneAsc:
                    return collection.OrderBy(x => x.PhoneNumberExtra);

                case SupplierSortingEnum.ByExtraPhoneDesc:
                    return collection.OrderByDescending(x => x.PhoneNumberExtra);

                case SupplierSortingEnum.ByManagerAsc:
                    return collection.OrderBy(x => x.Manager);

                case SupplierSortingEnum.ByManagerDesc:
                    return collection.OrderByDescending(x => x.Manager);

                case SupplierSortingEnum.ByExtraManagerAsc:
                    return collection.OrderBy(x => x.ManagerExtra);

                case SupplierSortingEnum.ByExtraManagerDesc:
                    return collection.OrderByDescending(x => x.ManagerExtra);

                case SupplierSortingEnum.ByAddressAsc:
                    return collection.OrderBy(x => x.Address);

                case SupplierSortingEnum.ByAddressDesc:
                    return collection.OrderByDescending(x => x.Address);

                case SupplierSortingEnum.ByExtraAddressAsc:
                    return collection.OrderBy(x => x.AddressExtra);

                case SupplierSortingEnum.ByExtraAddressDesc:
                    return collection.OrderByDescending(x => x.AddressExtra);


                default:
                    return collection.OrderByDescending(x => x.Created);
            }
        }

        protected override IOrderedEnumerable<Supplier> SortThenBy(IOrderedEnumerable<Supplier> collection, SupplierSortingEnum sorting)
        {
            switch (sorting)
            {
                case SupplierSortingEnum.ByCreateAsc:
                    return collection.OrderBy(x => x.Created);

                case SupplierSortingEnum.ByCreateDesc:
                    return collection.OrderByDescending(x => x.Created);

                case SupplierSortingEnum.ByUpdateAsc:
                    return collection.OrderBy(x => x.Updated);

                case SupplierSortingEnum.ByUpdateDesc:
                    return collection.OrderByDescending(x => x.Updated);

                case SupplierSortingEnum.ByActivityStatusAsc:
                    return collection.OrderBy(x => x.ActivityStatus);

                case SupplierSortingEnum.ByActivityStatusDesc:
                    return collection.OrderByDescending(x => x.ActivityStatus);

                case SupplierSortingEnum.ByTitleAsc:
                    return collection.OrderBy(x => x.Title);

                case SupplierSortingEnum.ByTitleDesc:
                    return collection.OrderByDescending(x => x.Title);

                case SupplierSortingEnum.ByEmailAsc:
                    return collection.OrderBy(x => x.Email);

                case SupplierSortingEnum.ByEmailDesc:
                    return collection.OrderByDescending(x => x.Email);

                case SupplierSortingEnum.ByExtraEmailAsc:
                    return collection.OrderBy(x => x.EmailExtra);

                case SupplierSortingEnum.ByExtraEmailDesc:
                    return collection.OrderByDescending(x => x.EmailExtra);

                case SupplierSortingEnum.ByPhoneAsc:
                    return collection.OrderBy(x => x.PhoneNumber);

                case SupplierSortingEnum.ByPhoneDesc:
                    return collection.OrderByDescending(x => x.PhoneNumber);

                case SupplierSortingEnum.ByExtraPhoneAsc:
                    return collection.OrderBy(x => x.PhoneNumberExtra);

                case SupplierSortingEnum.ByExtraPhoneDesc:
                    return collection.OrderByDescending(x => x.PhoneNumberExtra);

                case SupplierSortingEnum.ByManagerAsc:
                    return collection.OrderBy(x => x.Manager);

                case SupplierSortingEnum.ByManagerDesc:
                    return collection.OrderByDescending(x => x.Manager);

                case SupplierSortingEnum.ByExtraManagerAsc:
                    return collection.OrderBy(x => x.ManagerExtra);

                case SupplierSortingEnum.ByExtraManagerDesc:
                    return collection.OrderByDescending(x => x.ManagerExtra);

                case SupplierSortingEnum.ByAddressAsc:
                    return collection.OrderBy(x => x.Address);

                case SupplierSortingEnum.ByAddressDesc:
                    return collection.OrderByDescending(x => x.Address);

                case SupplierSortingEnum.ByExtraAddressAsc:
                    return collection.OrderBy(x => x.AddressExtra);

                case SupplierSortingEnum.ByExtraAddressDesc:
                    return collection.OrderByDescending(x => x.AddressExtra);


                default:
                    return collection.OrderByDescending(x => x.Created);
            }
        }
    }
}
