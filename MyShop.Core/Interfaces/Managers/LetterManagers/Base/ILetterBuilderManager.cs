using MyShop.ApiModels;
using MyShop.Core.Models.Base;
using System.Threading.Tasks;

namespace MyShop.Core.Interfaces.Managers.LetterManagers.Base
{
    public interface ILetterBuilderManager
    {
        Task<string> GetContent(LetterTextParamsModel letterParams);
        string GetSubject(UserLanguageEnum letterLanguage, LetterTypeEnum letterType);
    }
}