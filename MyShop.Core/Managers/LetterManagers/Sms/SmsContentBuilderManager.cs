using MyShop.Core.Interfaces.Managers.Base;
using MyShop.Core.Interfaces.Managers.LetterManagers.Base;
using MyShop.Core.Managers.LetterManagers.Base;
using MyShop.Core.Models.Base;

namespace MyShop.Core.Managers.Base.LetterManagers
{
    public class SmsContentBuilderManager : LetterBuilderManager, ISmsContentBuilderManager
    {
        public SmsContentBuilderManager(ITextLetterBuilderManager textLetterBuilderManager) : base(textLetterBuilderManager)
        {
        }

        protected override string GetWrapper(LetterTextParamsModel letterParams)
        {
            return GetText(letterParams);
        }
    }
}
