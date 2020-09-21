using MyShop.ApiModels;
using MyShop.Core.Entities;
using MyShop.Core.Helpers.Base;
using MyShop.Core.Interfaces.Repositories;
using MyShop.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.BL.Services
{
    public class VerificationTokenService : IVerificationTokenService
    {
        private readonly IVerificationTokenRepository _repository;

        public VerificationTokenService(IVerificationTokenRepository repository)
        {
            _repository = repository;
        }

        public async Task<VerificationToken> AddAsync(string contact, ContactTypeEnum contactType, VerificationTokenTypeEnum verificationTokenType)
        {
            var entity = GenerateVerificationTokenEntity(contact, contactType, verificationTokenType);
            var tempCode = entity.Token;

            entity.Token = AppHelper.Current.GetCryptoHash(entity.Token);
            await _repository.AddAsync(entity);

            entity.Token = tempCode;
            return entity;
        }

        public async Task<VerificationToken> GetAsync(string contact, string code)
        {
            var hash = AppHelper.Current.GetCryptoHash(code);
            return await _repository.FirstOrDefaultAsync(x => x.Contact == contact && x.Token == hash);
        }

        public async Task<IEnumerable<VerificationToken>> GetAllAsync(string contact)
        {
            return await _repository.WhereAsync(x => x.Contact == contact);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task DeleteExpiredAsync(string contact, string lifeTime)
        {
            var entities = await _repository.WhereAsync(x => x.Contact == contact);

            if (entities != null && entities.ToList().Count > 0)
            {
                foreach (var item in entities.ToList())
                {
                    if (item.Created.AddMinutes(Convert.ToDouble(lifeTime)) <= DateTime.UtcNow)
                    {
                        await _repository.DeleteAsync(item.Id);
                    }
                }
            }
        }


        private VerificationToken GenerateVerificationTokenEntity(string contact, ContactTypeEnum contactType, 
            VerificationTokenTypeEnum tokenType)
        {
            string token = null;

            if (contactType == ContactTypeEnum.Email) token = Guid.NewGuid().ToString();
            if (contactType == ContactTypeEnum.Phone) token = new Random().Next(1000, 9999).ToString();

            return new VerificationToken()
            {
                Id = Guid.NewGuid().ToString(),
                Contact = contact,
                Token = token,
                TokenType = tokenType
            };
        }
    }
}
