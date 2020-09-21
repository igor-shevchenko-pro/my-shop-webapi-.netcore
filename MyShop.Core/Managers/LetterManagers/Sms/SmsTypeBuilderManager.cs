using MyShop.ApiModels;
using MyShop.Core.Interfaces.Managers.Base;
using MyShop.Core.Interfaces.Managers.LetterManagers.Sms;
using MyShop.Core.Models;
using MyShop.Core.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShop.Core.Managers.LetterManagers.Sms
{
    public class SmsTypeBuilderManager : ISmsTypeBuilderManager
    {
        private readonly ISmsContentBuilderManager _smsContentBuilderManager;

        public SmsTypeBuilderManager(ISmsContentBuilderManager smsContentBuilderManager)
        {
            _smsContentBuilderManager = smsContentBuilderManager;
        }

        public async Task<IdentityMessageModel> GenerateRecoverySms(UserLanguageEnum letterLanguage, string phone, string verificationToken)
        {
            var subject = _smsContentBuilderManager.GetSubject(letterLanguage, LetterTypeEnum.RecoveryPassword);
            var letterParams = new LetterTextParamsModel()
            {
                LetterLanguage = letterLanguage,
                LetterType = LetterTypeEnum.RecoveryPassword,
                TypeContact = ContactTypeEnum.Phone,
                Params = new Dictionary<string, string>()
                {
                    { "contact", phone },
                    { "confirmationLink", verificationToken }
                }
            };

            return await BuildMessageResultModelAsync(letterParams, phone, subject);
        }

        public async Task<IdentityMessageModel> GenerateVerificationSms(UserLanguageEnum letterLanguage, string phone, string verificationToken)
        {
            var subject = _smsContentBuilderManager.GetSubject(letterLanguage, LetterTypeEnum.ContactVerification);
            var letterParams = new LetterTextParamsModel()
            {
                LetterLanguage = letterLanguage,
                LetterType = LetterTypeEnum.ContactVerification,
                TypeContact = ContactTypeEnum.Phone,
                Params = new Dictionary<string, string>()
                {
                    { "contact", phone },
                    { "confirmationLink", verificationToken }
                }
            };

            return await BuildMessageResultModelAsync(letterParams, phone, subject);
        }


        private async Task<IdentityMessageModel> BuildMessageResultModelAsync(LetterTextParamsModel letterParams, string contact,
            string subject)
        {
            return new IdentityMessageModel()
            {
                Content = await _smsContentBuilderManager.GetContent(letterParams),
                Destinations = new List<string>() { contact },
                Subject = subject,
            };
        }
    }
}
