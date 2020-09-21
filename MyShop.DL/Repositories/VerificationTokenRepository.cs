using MyShop.ApiModels;
using MyShop.Core.Entities;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.DL.Repositories.Base;

namespace MyShop.DL.Repositories
{
    public class VerificationTokenRepository : BaseRepository<VerificationToken, string, EntitySortingEnum>, IVerificationTokenRepository
    {
        public VerificationTokenRepository(IGenericRepository<VerificationToken, string> repository) : base(repository)
        {
        }
    }
}
