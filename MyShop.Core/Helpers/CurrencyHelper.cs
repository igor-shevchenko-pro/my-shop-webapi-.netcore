using MyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Core.Helpers
{
    public class CurrencyHelper
    {
        private static readonly CurrencyHelper _instance = new CurrencyHelper();
        public static CurrencyHelper Current => _instance;


        private CurrencyHelper()
        {
            Initialize();

            FullCollection = new List<Currency>();
            FullCollection.AddRange(InitializeRussion());
        }


        private IEnumerable<Currency> InitializeRussion()
        {
            return Collection;
        }

        private void Initialize()
        {
            int langId = LanguageHelper.Current.Russian.Id;

            Uah = new Currency() 
            { 
                Id = 1,
                Title = "Гривна",
                Code = "UAH",
                SymbolLeft = "₴",
                SymbolRight = "грн.",
                IsBase = true
            };

            Usd = new Currency() 
            { 
                Id = 2,
                Title = "Доллар США",
                Code = "USD",
                SymbolLeft = "$",
                SymbolRight = null,
                IsBase = false
            };

            Euro = new Currency() 
            { 
                Id = 3,
                Title = "Евро",
                Code = "EUR",
                SymbolLeft = "€",
                SymbolRight = null,
                IsBase = false
            };

            Collection = new List<Currency>()
            {
                Uah,
                Usd,
                Euro
            };
        }

        public Currency Uah { get; private set; }
        public Currency Usd { get; private set; }
        public Currency Euro { get; private set; }


        public List<Currency> Collection { get; private set; }
        public List<Currency> FullCollection { get; private set; }

        public string GetName(int id)
        {
            var roleName = Collection.FirstOrDefault(x => x.Id == id).Title;
            return roleName ?? throw new ArgumentException($"Entity {id} is not found");
        }

        public string GetCode(int id)
        {
            var codeName = Collection.FirstOrDefault(x => x.Id == id).Code;
            return codeName ?? throw new ArgumentException($"Entity {id} is not found");
        }
    }
}
