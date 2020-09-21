using MyShop.ApiModels;
using System.Collections.Generic;

namespace MyShop.Core.Models.Base
{
    public class LetterTextParamsModel
    {
        public UserLanguageEnum LetterLanguage { get; set; }
        public LetterTypeEnum LetterType { get; set; }
        public ContactTypeEnum TypeContact { get; set; }
        public Dictionary<string, string> Params { get; set; }
    }
}
