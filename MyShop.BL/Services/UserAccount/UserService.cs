using MyShop.ApiModels;
using MyShop.ApiModels.Interfaces.Base;
using MyShop.ApiModels.Models;
using MyShop.ApiModels.Models.Response;
using MyShop.BL.Services.Base;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Helpers;
using MyShop.Core.Helpers.Base;
using MyShop.Core.Interfaces.Configurations.Base;
using MyShop.Core.Interfaces.Repositories.ManyToMany;
using MyShop.Core.Interfaces.Repositories.UserAccount;
using MyShop.Core.Interfaces.Services.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.BL.Services.UserAccount
{
    public class UserService : BaseService<UserAddApiModel, UserGetFullApiModel, UserGetMinApiModel, User, string, UserSortingEnum>, IUserService
    {
        new IUserRepository _repository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserService(IUserRepository repository,
            IUserRoleRepository userRoleRepository,
            IDataMapper dataMapper) 
            : base(repository, dataMapper)
        {
            _userRoleRepository = userRoleRepository;
            _repository = repository;
        }
        
        public async override Task<string> AddAsync(UserAddApiModel model)
        {
            if (await _repository.AnyAsync(x => x.Email.ToLower() == model.Email.ToLower()))
            {
                throw new ArgumentException($"User with this Email already exist");
            }
            if (await _repository.AnyAsync(x => x.Phone == model.Phone))
            {
                throw new ArgumentException($"User with this Phone already exist");
            }

            var passwordHash = AppHelper.Current.GetCryptoHash(model.Password);

            User user = new User();
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.UserName = model.UserName;
            user.Password = passwordHash;
            user.ActivityStatus = model.ActivityStatus;

            var userProfile = model.UserProfile;
            UserProfileHelper.Current.SetUserProfileDefaultDataForEntity(ref userProfile);

            user.SetId();
            user.UserProfile = _dataMapper.Parse<UserProfileAddApiModel, UserProfile>(userProfile);
            user.UserProfileId = user.Id;
            user.UserProfile.Id = user.Id;
            user.UserProfile.ActivityStatus = user.ActivityStatus;
            user.UserProfile.FileEntityId = user.UserProfile.FileEntityId ?? "default-user";

            await _repository.AddAsync(user);

            var roles = model.Roles.Select(x => x.Id).ToArray();
            await _userRoleRepository.AddRolesAsync(user.Id, roles);

            return user.Id;
        }

        public async override Task UpdateAsync(UserAddApiModel model, int? langId = null)
        {
            var checkEmail = await _repository.FirstOrDefaultAsync(x => x.Email.ToLower() == model.Email.ToLower());
            if (checkEmail != null && checkEmail.Email.ToLower() == model.Email.ToLower() && checkEmail.Id != model.Id)
                throw new Exception("Duplicate Email");

            var checkPhone = await _repository.FirstOrDefaultAsync(x => x.Phone == model.Phone);
            if (checkPhone != null && checkPhone.Phone == model.Phone && checkPhone.Id != model.Id)
                throw new Exception("Duplicate Phone");


            var user = await _repository.GetAsync(model.Id);
            if(user == null) throw new Exception("Entity is not found");

            var userProfile = _dataMapper.Parse<UserProfileAddApiModel, UserProfile>(model.UserProfile);
            userProfile.Id = model.Id;

            if (user.UserProfile == null)
            {
                user.UserProfile = userProfile;
            }
            else
            {
                user.UserProfile.Address = model.UserProfile.Address;
                user.UserProfile.DateOfBirth = model.UserProfile.DateOfBirth;
                user.UserProfile.FirstName = model.UserProfile.FirstName;
                user.UserProfile.SecondName = model.UserProfile.SecondName;
                user.UserProfile.ActivityStatus = model.ActivityStatus;

                // update file
                if (user.UserProfile.FileEntityId != model.UserProfile.FileEntityId && model.UserProfile.FileEntityId != null)
                {
                    user.UserProfile.FileEntityId = model.UserProfile.FileEntityId;
                }
            }

            user.Email = model.Email;
            user.Phone = model.Phone;
            user.ActivityStatus = model.ActivityStatus;
            user.UserName = model.UserName;
            await _repository.UpdateAsync(user);

            // update roles
            var userRoles = await _userRoleRepository.WhereAsync(x => x.Id == user.Id);
            if (userRoles != null && userRoles.ToList().Count > 0)
            {
                var oldRoles = userRoles.ToList().Select(x => x.Role).ToList();
                var oldRoleIds = oldRoles.Select(x => x.Id).ToArray();
                var newRoles = _dataMapper.Parse<List<RoleAddApiModel>, List<Role>>(model.Roles);
                var newRoleIds = newRoles.Select(x => x.Id).ToArray();

                if (!oldRoleIds.SequenceEqual(newRoleIds))
                {
                    await _userRoleRepository.RemoveAllUserRolesAsync(user.Id);
                    await _userRoleRepository.AddRolesAsync(model.Id, newRoleIds);
                }
            }
        }

        public async Task<PaginationResponseApiModel<IBaseApiModel<string>, UserSortingEnum>> GetManagersAsync(int start, int count,
            List<UserSortingEnum> sortings, TypeModelResponseEnum modelResponseType, int langId)
        {
            var pagination = await _repository.GetManagersAsync(start, count, sortings, langId);

            var result = GetParsedPaginationResult(pagination.Entities, modelResponseType, pagination.Total, pagination.Start,
                                       pagination.Count, pagination.EntitySortings);

            return result;
        }

        public async Task SetDefaultPhotoAsync(string userId)
        {
            var user = await _repository.GetAsync(userId);
            if (user == null) throw new ArgumentException($"User is not found");

            user.UserProfile.FileEntityId = "default-user";
            await _repository.UpdateAsync(user);
        }
    }
}
