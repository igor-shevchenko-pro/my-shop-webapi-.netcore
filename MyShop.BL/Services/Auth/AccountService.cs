using Microsoft.IdentityModel.Tokens;
using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using MyShop.ApiModels.Models.Auth;
using MyShop.Core.Configurations;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Helpers;
using MyShop.Core.Helpers.Base;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Repositories.ManyToMany;
using MyShop.Core.Interfaces.Repositories.UserAccount;
using MyShop.Core.Interfaces.Services.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyShop.BL.Services.Auth
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IDataMapper _dataMapper;

        public AccountService(IUserRepository userRepository,
            IUserRoleRepository userRoleRepository,
            IRefreshTokenService refreshTokenService,
            IDataMapper dataMapper)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _refreshTokenService = refreshTokenService;
            _dataMapper = dataMapper;
        }

        public async Task<UserGetFullApiModel> AuthenticationAsync(string login, string password, ContactTypeEnum contactType)
        {
            var contact = login.ToLower();
            var hash = AppHelper.Current.GetCryptoHash(password);

            User user = null;

            switch (contactType)
            {
                case ContactTypeEnum.Email:
                    user = await _userRepository.FirstOrDefaultAsync(x => x.Email == contact && x.Password == hash);
                    break;
                case ContactTypeEnum.Phone:
                    user = await _userRepository.FirstOrDefaultAsync(x => x.Phone == contact && x.Password == hash);
                    break;
            }

            if (user == null) throw new ArgumentException($"Login or password is not valid");

            var userModel = _dataMapper.Parse<User, UserGetFullApiModel>(user);
            return userModel;
        }

        public async Task<UserGetFullApiModel> RegistrationAsync(string login, string password, ContactTypeEnum contactType,
            UserProfileAddApiModel userProfile)
        {
            var contact = login.ToLower();
            var hash = AppHelper.Current.GetCryptoHash(password);

            User user = await CreateDefaultUserEntityAsync(contact, hash, contactType);
            UserProfileHelper.Current.SetUserProfileDefaultDataForRegistration(ref userProfile);

            user.SetId();
            user.UserProfileId = user.Id;
            user.UserProfile = _dataMapper.Parse<UserProfileAddApiModel, UserProfile>(userProfile);
            user.UserProfile.Id = user.Id;

            await _userRepository.AddAsync(user);

            user.UserRoles = (await _userRoleRepository.AddRolesAsync(user.Id, RoleHelper.Current.User.Id)).ToList();

            var userModel = _dataMapper.Parse<User, UserGetFullApiModel>(user);
            return userModel;
        }

        public async Task<UserGetFullApiModel> AnonymousAsync()
        {
            User user = new User();

            var userProfile = new UserProfileAddApiModel();
            UserProfileHelper.Current.SetUserProfileDefaultDataForRegistration(ref userProfile);

            user.SetId();
            user.UserProfile = _dataMapper.Parse<UserProfileAddApiModel, UserProfile>(userProfile);
            user.UserProfile.Id = user.Id;

            await _userRepository.AddAsync(user);
            await _userRoleRepository.AddRolesAsync(user.Id, RoleHelper.Current.User.Id);

            var userModel = _dataMapper.Parse<User, UserGetFullApiModel>(user);

            return userModel;
        }

        public async Task<SignInApiModel> GenerateSignInResponseAsync(UserGetFullApiModel userModel, string refreshToken = null)
        {
            // Create claims for token
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userModel.Id),
            };

            var roleClaims = userModel.Roles.Select(x => new Claim(ClaimTypes.Role, RoleHelper.Current.GetName(x.Id)));
            claims.AddRange(roleClaims);

            // Generate JWT
            var createTime = DateTime.UtcNow;
            var expiresTime = createTime.Add(AuthJwtConfig.Current.Lifetime);

            var token = new JwtSecurityToken(
                issuer: AuthJwtConfig.Current.Issuer,
                audience: AuthJwtConfig.Current.Audience,
                claims: claims,
                expires: expiresTime,
                signingCredentials: new SigningCredentials(AuthJwtConfig.Current.SymmetricSecurityKey, AuthJwtConfig.Current.SigningAlgorithm)
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var refresh = string.IsNullOrEmpty(refreshToken)
                                 ? await _refreshTokenService.CreateTokenAsync(userModel.Id)
                                 : await _refreshTokenService.UpdateTokenAsync(userModel.Id, refreshToken);

            SignInApiModel result = new SignInApiModel()
            {
                UserId = userModel.Id,
                Token = jwtToken,
                RefreshToken = refresh,
                LifeTime = expiresTime.ToString()
            };

            return result;
        }

        private async Task<User> CreateDefaultUserEntityAsync(string contact, string password, ContactTypeEnum contactType)
        {
            if(contactType == ContactTypeEnum.Email)
            {
                if (await _userRepository.AnyAsync(x => x.Email.ToLower() == contact.ToLower()))
                {
                    throw new ArgumentException($"User already exist");
                }

                User user = new User()
                {
                    Email = contact,
                    Password = password,
                    IsEmailConfirmed = true,
                };

                return user;
            }
            else if (contactType == ContactTypeEnum.Phone)
            {
                if (await _userRepository.AnyAsync(x => x.Phone == contact))
                {
                    throw new ArgumentException($"User already exist");
                }

                User user = new User()
                {
                    Phone = contact,
                    Password = password,
                    IsPhoneConfirmed = true,
                };

                return user;
            }
            else
            {
                throw new Exception("Unsupported contact_type");
            }
        }
    }
}
