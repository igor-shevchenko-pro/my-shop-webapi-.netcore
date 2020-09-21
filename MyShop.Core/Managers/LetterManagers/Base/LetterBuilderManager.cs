using MyShop.ApiModels;
using MyShop.Core.Configurations;
using MyShop.Core.Interfaces.Managers.LetterManagers.Base;
using MyShop.Core.Models.Base;
using System.Threading.Tasks;

namespace MyShop.Core.Managers.LetterManagers.Base
{
    public abstract class LetterBuilderManager : ILetterBuilderManager
    {
        protected ITextLetterBuilderManager _textLetterBuilderManager;

        public LetterBuilderManager(ITextLetterBuilderManager textLetterBuilderManager)
        {
            _textLetterBuilderManager = textLetterBuilderManager;
        }

        public virtual Task<string> GetContent(LetterTextParamsModel letterParams)
        {
            var body = GetWrapper(letterParams);
            return Task.FromResult(body);
        }

        // Defines email's subject
        public virtual string GetSubject(UserLanguageEnum letterLanguage, LetterTypeEnum letterType)
        {
            if (letterLanguage == UserLanguageEnum.Russian && letterType == LetterTypeEnum.RecoveryPassword)
            {
                return "Подтверждение восстановления пароля";
            }
            if (letterLanguage == UserLanguageEnum.Ukrainian && letterType == LetterTypeEnum.RecoveryPassword)
            {
                return "Підтвердження відновлення пароля";
            }
            if (letterLanguage == UserLanguageEnum.English && letterType == LetterTypeEnum.RecoveryPassword)
            {
                return "Password recovery confirmation";
            }

            if (letterLanguage == UserLanguageEnum.Russian && letterType == LetterTypeEnum.ContactVerification)
            {
                return "Проверка адреса электронной почты";
            }
            if (letterLanguage == UserLanguageEnum.Ukrainian && letterType == LetterTypeEnum.ContactVerification)
            {
                return "Перевірка електронної адреси";
            }
            if (letterLanguage == UserLanguageEnum.English && letterType == LetterTypeEnum.ContactVerification)
            {
                return "Verification of email address";
            }

            if (letterLanguage == UserLanguageEnum.Russian && letterType == LetterTypeEnum.SupportService)
            {
                return "Служба поддержки (Название магазина)";
            }
            if (letterLanguage == UserLanguageEnum.Ukrainian && letterType == LetterTypeEnum.SupportService)
            {
                return "Служба підтримки (Назва магазину)";
            }
            if (letterLanguage == UserLanguageEnum.English && letterType == LetterTypeEnum.SupportService)
            {
                return "(My shop title) support service";
            }

            Log.Current.Error("Unsupported letterType");
            return "(Shop title)";
        }

        // Get letter text based on the letter type, contact type (email, sms) and language
        protected virtual string GetText(LetterTextParamsModel letterParams)
        {
            return _textLetterBuilderManager.GetText(letterParams);
        }

        // Used to build the overall structure of the letter (text with wrapper)
        protected abstract string GetWrapper(LetterTextParamsModel letterParams);
    }
}