using MyShop.ApiModels;
using MyShop.Core.Models;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Managers.LetterManagers.Sms
{
    public interface ISmsTypeBuilderManager
    {
        Task<IdentityMessageModel> GenerateVerificationSms(UserLanguageEnum letterLanguage, string phone, string verificationToken);
        Task<IdentityMessageModel> GenerateRecoverySms(UserLanguageEnum letterLanguage, string phone, string verificationToken);
    }
}
