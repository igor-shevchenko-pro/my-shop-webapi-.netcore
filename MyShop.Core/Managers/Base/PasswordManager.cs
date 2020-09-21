using Microsoft.Extensions.Configuration;
using MyShop.ApiModels;
using MyShop.Core.Entities.UserAccount;
using MyShop.Core.Helpers;
using MyShop.Core.Helpers.Base;
using MyShop.Core.Interfaces.Managers;
using MyShop.Core.Interfaces.Managers.Base;
using MyShop.Core.Interfaces.Managers.LetterManagers.Email;
using MyShop.Core.Interfaces.Managers.LetterManagers.Sms;
using MyShop.Core.Interfaces.Repositories.UserAccount;
using MyShop.Core.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace MyShop.Core.Managers.Base
{
    public class PasswordManager : IPasswordManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IVerificationTokenService _verificationTokenService;
        private readonly IEmailSenderManager _emailSenderManager;
        private readonly IEmailTypeBuilderManager _emailTypeBuilderManager;
        private readonly ISmsSenderManager _smsSenderManager;
        private readonly ISmsTypeBuilderManager _smsTypeBuilderManager;
        private readonly IConfiguration _configuration;
        private readonly string _lifeTimeVerifyToken;

        public PasswordManager(IUserRepository userRepository, 
            IVerificationTokenService verificationTokenService,
            IEmailSenderManager emailSenderManager,
            IEmailTypeBuilderManager emailTypeBuilderManager,
            ISmsSenderManager smsSenderManager,
            ISmsTypeBuilderManager smsTypeBuilderManager,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _verificationTokenService = verificationTokenService;
            _emailSenderManager = emailSenderManager;
            _emailTypeBuilderManager = emailTypeBuilderManager;
            _smsSenderManager = smsSenderManager;
            _smsTypeBuilderManager = smsTypeBuilderManager;
            _configuration = configuration;
            _lifeTimeVerifyToken = _configuration["VerificationToken:Lifetime"];
        }

        public async Task ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var hashOldPassword = AppHelper.Current.GetCryptoHash(oldPassword);

            var user = await _userRepository.FirstOrDefaultAsync(x => x.Password == hashOldPassword && x.Id == userId);
            if (user == null) throw new ArgumentException($"User is not found");

            var hashNewPassword = AppHelper.Current.GetCryptoHash(newPassword);
            if (user.Password == hashNewPassword) throw new ArgumentException($"New password can't be the same as the old one");

            user.Password = hashNewPassword;
            await _userRepository.UpdateAsync(user);
        }

        public async Task RecoveryPasswordRequestAsync(string contact, ContactTypeEnum contacType, FrontClientType frontClient)
        {
            // recovery password by Email
            if (contacType == ContactTypeEnum.Email)
            {
                var user = await _userRepository.FirstOrDefaultAsync(p => p.Email == contact);
                if (user == null) throw new ArgumentException($"User is not found");

                await RecoveryPasswordRequestByEmailAsync(contact, user.UserProfile.LanguageId, frontClient);
            }
            // recovery password by Phone
            else if (contacType == ContactTypeEnum.Phone)
            {
                var user = await _userRepository.FirstOrDefaultAsync(p => p.Phone == contact);
                if (user == null) throw new ArgumentException($"User is not found");

                await RecoveryPasswordRequestBySmsAsync(contact, user.UserProfile.LanguageId);
            }
            else
            {
                throw new Exception("Unsupported contact_type");
            }
        }

        public async Task RecoveryPasswordAsync(string contact, string code, string newPassword, ContactTypeEnum contactType)
        {
            if (contactType == ContactTypeEnum.Email)
            {
                var user = await _userRepository.FirstOrDefaultAsync(p => p.Email == contact);
                if (user == null) throw new ArgumentException($"User is not found");

                await RecoveryPasswordHandlerAsync(user, contact, code, newPassword);
            }
            else if (contactType == ContactTypeEnum.Phone)
            {
                var user = await _userRepository.FirstOrDefaultAsync(p => p.Phone == contact);
                if (user == null) throw new ArgumentException($"User is not found");

                await RecoveryPasswordHandlerAsync(user, contact, code, newPassword);
            }
            else
            {
                throw new Exception("Unsupported contact_type");
            }
        }


        private async Task RecoveryPasswordRequestByEmailAsync(string contact, int langId, FrontClientType frontClient)
        {
            var verificationToken = await _verificationTokenService.AddAsync(contact, ContactTypeEnum.Email, VerificationTokenTypeEnum.Recovery);

            var emailContent = await _emailTypeBuilderManager.GenerateRecoveryEmail(LanguageHelper.Current.GetUserLanguageTypeByEnum(langId),
                                     contact, verificationToken.Token, frontClient);

            Task.Run(() => { _emailSenderManager.SendEmail(emailContent); });
        }

        private async Task RecoveryPasswordRequestBySmsAsync(string contact, int langId)
        {
            var verificationToken = await _verificationTokenService.AddAsync(contact, ContactTypeEnum.Phone, VerificationTokenTypeEnum.Recovery);

            var smsContent = await _smsTypeBuilderManager.GenerateRecoverySms(LanguageHelper.Current.GetUserLanguageTypeByEnum(langId),
                                   contact, verificationToken.Token);

            Task.Run(() => { _smsSenderManager.SendSms(smsContent); });
        }

        private async Task RecoveryPasswordHandlerAsync(User user, string contact, string code, string newPassword)
        {
            // find appropiate VerificationToken in Db
            var token = await _verificationTokenService.GetAsync(contact, code);
            if (token == null)
            {
                throw new Exception($"The recovery password link invalid");
            }

            // check if token expired
            var tokenLifeTime = token.Created.AddMinutes(Convert.ToDouble(_lifeTimeVerifyToken));
            if (tokenLifeTime > DateTime.UtcNow)
            {
                var hashNewPassword = AppHelper.Current.GetCryptoHash(newPassword);
                if (user.Password == hashNewPassword) throw new ArgumentException($"New password can't be the same as the old one");

                // set new password
                user.Password = hashNewPassword;
                await _userRepository.UpdateAsync(user);

                // delete current used verificationToken
                await _verificationTokenService.DeleteAsync(token.Id);

                // delete expired verificationTokens
                await _verificationTokenService.DeleteExpiredAsync(contact, _lifeTimeVerifyToken);
            }
            else
            {
                // delete expired verificationTokens
                await _verificationTokenService.DeleteExpiredAsync(contact, _lifeTimeVerifyToken);

                throw new ArgumentException($"The recovery password link is expired");
            }
        }
    }
}
