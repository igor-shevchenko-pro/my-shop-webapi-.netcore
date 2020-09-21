using MyShop.ApiModels;
using MyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Core.Helpers
{
    public class LanguageHelper
    {
        private static readonly LanguageHelper _instance = new LanguageHelper();
        public static LanguageHelper Current => _instance;

        private LanguageHelper()
        {
            Russian = new Language() { Id = 1, Title = "Русский", Symbol = "ru-RU" };
            Ukrainien = new Language() { Id = 2, Title = "Українська", Symbol = "ua-UA" };
            English = new Language() { Id = 3, Title = "English", Symbol = "en-US" };

            Collection = new List<Language>()
            {
                Russian,
                Ukrainien,
                English,
            };
        }

        public List<Language> Collection { get; private set; }

        public Language English { get; private set; }
        public Language Ukrainien { get; private set; }
        public Language Russian { get; private set; }

        public string GetName(int id)
        {
            var roleName = Collection.FirstOrDefault(x => x.Id == id).Title;
            return roleName ?? throw new ArgumentException($"Entity {id} is not found");
        }

        public string GetName(string symbol)
        {
            var roleName = Collection.FirstOrDefault(x => x.Symbol == symbol).Title;
            return roleName ?? throw new ArgumentException($"Entity {symbol} is not found");
        }

        public int GetIdBySymbol(string lang)
        {
            var id = Collection.FirstOrDefault(x => x.Symbol.Equals(lang))?.Id ?? 1;
            return id;
        }
        
        public string GetSymbolById(int id)
        {
            var roleName = Collection.FirstOrDefault(x => x.Id == id).Symbol;
            return roleName ?? throw new ArgumentException($"Entity {id} is not found");
        }

        public UserLanguageEnum GetUserLanguageTypeByEnum(int id)
        {
            switch (id)
            {
                case 1:
                    return UserLanguageEnum.Russian;
                case 2:
                    return UserLanguageEnum.Ukrainian;
                case 3:
                    return UserLanguageEnum.English;

                default:
                    return UserLanguageEnum.Russian;
            }
        }
    }
}
