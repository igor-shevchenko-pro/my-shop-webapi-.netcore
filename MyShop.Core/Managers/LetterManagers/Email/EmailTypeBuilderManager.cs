using Microsoft.Extensions.Configuration;
using MyShop.ApiModels;
using MyShop.Core.Interfaces.Managers.Base;
using MyShop.Core.Interfaces.Managers.LetterManagers.Email;
using MyShop.Core.Models;
using MyShop.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.Managers.Base.LetterManagers.Email
{
    public class EmailTypeBuilderManager : IEmailTypeBuilderManager
    {
        private readonly IEmailContentBuilderManager _emailContentBuilderManager;
        private readonly IConfiguration _configuration;
        private readonly string _baseClientUrl;
        private readonly string _baseAdminUrl;

        public EmailTypeBuilderManager(IConfiguration configuration, IEmailContentBuilderManager emailContentBuilderManager)
        {
            _emailContentBuilderManager = emailContentBuilderManager;
            _configuration = configuration;
            _baseClientUrl = _configuration["HttpClients:WebClientUrl"];
            _baseAdminUrl = _configuration["HttpClients:AdminPanelClientUrl"];
        }

        public async Task<IdentityMessageModel> GenerateRecoveryEmail(UserLanguageEnum letterLanguage, string email, 
            string verificationToken, FrontClientType frontClient)
        {
            string mainUrl = "";
            if (frontClient == FrontClientType.AdminPanel) mainUrl = _baseAdminUrl;
            else if (frontClient == FrontClientType.WebClient) mainUrl = _baseClientUrl;
            else throw new Exception("Unsupported front_client_type");

            var confirmationLink = $"{mainUrl}/account/recovery_password/{HttpUtility.UrlEncode(verificationToken)}/{HttpUtility.UrlEncode(email)}";
            var subject = _emailContentBuilderManager.GetSubject(letterLanguage, LetterTypeEnum.RecoveryPassword);
            var letterParams = new LetterTextParamsModel()
            {
                LetterLanguage = letterLanguage,
                TypeContact = ContactTypeEnum.Email,
                LetterType = LetterTypeEnum.RecoveryPassword,
                Params = new Dictionary<string, string>()
                {
                    { "contact", email },
                    { "confirmationLink", confirmationLink }
                }
            };

            return await BuildMessageResultModelAsync(letterParams, email, subject);
        }

        public async Task<IdentityMessageModel> GenerateVerificationEmail(UserLanguageEnum letterLanguage, string email, 
            string verificationToken)
        {
            var confirmationLink = $"{_baseClientUrl}/account/confirm_email/{HttpUtility.UrlEncode(verificationToken)}/{HttpUtility.UrlEncode(email)}";
            var subject = _emailContentBuilderManager.GetSubject(letterLanguage, LetterTypeEnum.ContactVerification);
            var letterParams = new LetterTextParamsModel()
            {
                LetterLanguage = letterLanguage,
                TypeContact = ContactTypeEnum.Email,
                LetterType = LetterTypeEnum.ContactVerification,
                Params = new Dictionary<string, string>()
                {
                    { "contact", email },
                    { "confirmationLink", confirmationLink }
                }
            };

            return await BuildMessageResultModelAsync(letterParams, email, subject);
        }

        public async Task<IdentityMessageModel> GenerateSuppotEmail(UserLanguageEnum letterLanguage, string email, string question, 
            string answer)
        {
            var subject = _emailContentBuilderManager.GetSubject(letterLanguage, LetterTypeEnum.SupportService);
            var letterParams = new LetterTextParamsModel()
            {
                LetterLanguage = letterLanguage,
                TypeContact = ContactTypeEnum.Email,
                LetterType = LetterTypeEnum.SupportService,
                Params = new Dictionary<string, string>()
                {
                    { "question", question },
                    { "answer", answer }
                }
            };

            return await BuildMessageResultModelAsync(letterParams, email, subject);
        }


        private async Task<IdentityMessageModel> BuildMessageResultModelAsync(LetterTextParamsModel letterParams, string contact, 
            string subject)
        {
            return new IdentityMessageModel()
            {
                Content = await _emailContentBuilderManager.GetContent(letterParams),
                Destinations = new List<string>() { contact },
                Subject = subject,
            };
        }
    }
}
