using MyShop.ApiModels;
using MyShop.Core.Entities.Auth;
using MyShop.Core.Interfaces.Repositories.Auth;
using MyShop.Core.Interfaces.Repositories.Base;
using MyShop.DL.Repositories.Base;
using System;
using System.Threading.Tasks;

namespace MyShop.DL.Repositories.Auth
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken, string, EntitySortingEnum>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IGenericRepository<RefreshToken, string> repository) : base(repository)
        {
        }

        public async Task<string> CreateTokenAsync(string userId)
        {
            var token = GenerateRefreshToken();

            await _repository.CreateAsync(new RefreshToken()
            {
                Id = userId,
                Token = token,
                SecurityStamp = token,
                ActivityStatus = EntityActivityStatusEnum.Inactive
            });

            return token;
        }

        public async Task<string> UpdateTokenAsync(string userId, string token)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.Id == userId && 
                                                                    x.Token == token && 
                                                                    x.ActivityStatus == EntityActivityStatusEnum.Inactive);

            if (entity == null)
            {
                // GetAsync old refresh token and deactivate all issued tokens
                entity = await _repository.FirstOrDefaultAsync(x => x.Id == userId && x.Token == token);
                if (entity != null)
                {
                    var securityStamp = entity.SecurityStamp;
                    var collection = await _repository.WhereAsync(x => x.Id == userId && 
                                                                       x.SecurityStamp == securityStamp &&
                                                                       x.ActivityStatus == EntityActivityStatusEnum.Inactive);

                    foreach (var item in collection)
                    {
                        item.ActivityStatus = EntityActivityStatusEnum.Active;
                        await _repository.UpdateAsync(item);
                    }
                }

                throw new ArgumentException($"Refresh token {token} is not found for user {userId}");
            }

            // UpdateAsync old token
            entity.ActivityStatus = EntityActivityStatusEnum.Active;
            await _repository.UpdateAsync(entity, x => x.Id.Equals(entity.Id) && x.Token.Equals(entity.Token));

            // Create new refresh token
            var newToken = new RefreshToken()
            {
                Id = entity.Id,
                Token = GenerateRefreshToken(),
                SecurityStamp = entity.SecurityStamp,
                ActivityStatus = EntityActivityStatusEnum.Inactive,
            };
            await _repository.CreateAsync(newToken);

            return newToken.Token;
        }


        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
