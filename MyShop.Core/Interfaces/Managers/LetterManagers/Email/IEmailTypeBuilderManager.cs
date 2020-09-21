using MyShop.ApiModels;
using MyShop.Core.Models;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Managers.LetterManagers.Email
{
    public interface IEmailTypeBuilderManager
    {
        Task<IdentityMessageModel> GenerateVerificationEmail(UserLanguageEnum letterLanguage, string email, string verificationToken);
        Task<IdentityMessageModel> GenerateRecoveryEmail(UserLanguageEnum letterLanguage, string email, string verificationToken, FrontClientType frontClient);
        Task<IdentityMessageModel> GenerateSuppotEmail(UserLanguageEnum letterLanguage, string email, string question, string answer);
    }
}
