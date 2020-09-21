using MyShop.ApiModels;
using MyShop.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Services
{
    public interface IVerificationTokenService
    {
        Task<VerificationToken> AddAsync(string contact, ContactTypeEnum contactType, VerificationTokenTypeEnum verificationTokenType);
        Task<VerificationToken> GetAsync(string contact, string code);
        Task<IEnumerable<VerificationToken>> GetAllAsync(string contact);
        Task DeleteAsync(string id);
        Task DeleteExpiredAsync(string contact, string lifeTime);
    }
}
