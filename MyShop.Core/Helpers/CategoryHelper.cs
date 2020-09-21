using MyShop.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyShop.Core.Helpers
{
    public class CategoryHelper
    {
        private static readonly CategoryHelper _instance = new CategoryHelper();
        public static CategoryHelper Current => _instance;

        private CategoryHelper()
        {
            Initialize();

            FullCollection = new List<Category>();
            FullCollection.AddRange(InitializeRussian());
            //FullCollection.AddRange(InitializeEnglish());
            //FullCollection.AddRange(InitializeUkrainien());
        }

        public List<Category> Collection { get; private set; }
        public List<Category> FullCollection { get; private set; }


        public string GetName(int id)
        {
            var roleName = Collection.FirstOrDefault(x => x.Id == id).Title;
            return roleName ?? throw new ArgumentException($"Entity {id} is not found");
        }

        private void Initialize()
        {
            //var langId = LanguageHelper.Current.Russian.Id;
            Collection = LoadFromFile();
        }

        private List<Category> InitializeRussian()
        {
            return Collection;
        }

        private List<Category> InitializeEnglish()
        {
            var langId = LanguageHelper.Current.English.Id;

            var result = new List<Category>()
            {
            };

            return result;
        }

        private List<Category> InitializeUkrainien()
        {
            var langId = LanguageHelper.Current.Ukrainien.Id;

            var result = new List<Category>()
            {
            };

            return result;
        }

        private List<Category> LoadFromFile()
        {
            var filename = $"Categories";

            var path = Path.Combine(Directory.GetCurrentDirectory(), "AppData\\", $"{filename}.json");            
            var json = System.IO.File.ReadAllText(path);

            return JsonConvert.DeserializeObject<List<Category>>(json);
        }
    }
}