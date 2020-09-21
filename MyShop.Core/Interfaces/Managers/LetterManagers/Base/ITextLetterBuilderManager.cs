using MyShop.Core.Models.Base;

namespace MyShop.Core.Interfaces.Managers.LetterManagers.Base
{
    public interface ITextLetterBuilderManager
    {
        string GetText(LetterTextParamsModel letterParams);
    }
}