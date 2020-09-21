using MyShop.ApiModels;

namespace MyShop.Core.Models.Base
{
    public class LetterTextModel
    {
        public UserLanguageEnum LetterLanguage { get; set; }
        public LetterTypeEnum LetterType { get; set; }
        public ContactTypeEnum TypeContact { get; set; }
        public string Text { get; set; }
    }
}
