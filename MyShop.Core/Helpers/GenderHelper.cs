using MyShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Core.Helpers
{
    public class GenderHelper
    {
        private static readonly GenderHelper _instance = new GenderHelper();
        public static GenderHelper Current => _instance;

        private GenderHelper()
        {
            Initialize();

            FullCollection = new List<Gender>();
            FullCollection.AddRange(InitializeEnglish());
            FullCollection.AddRange(InitializeRussian());
            FullCollection.AddRange(InitializeUkrainien());
        }

        public List<Gender> Collection { get; private set; }
        public List<Gender> FullCollection { get; private set; }

        public Gender Male { get; private set; }
        public Gender Female { get; private set; }
        public Gender Unknown { get; private set; }

        private void Initialize()
        {
            var langId = LanguageHelper.Current.English.Id;

            Male = new Gender() { Id = 1, Title = "Male", Symbol = "M", LanguageId = langId };
            Female = new Gender() { Id = 2, Title = "Female", Symbol = "W", LanguageId = langId };
            Unknown = new Gender() { Id = 3, Title = "Unknown", Symbol = "U", LanguageId = langId };

            Collection = new List<Gender>()
            {
                Male,
                Female,
                Unknown
            };
        }

        private List<Gender> InitializeEnglish()
        {
            return Collection;
        }

        private List<Gender> InitializeRussian()
        {
            var langId = LanguageHelper.Current.Russian.Id;

            var result = new List<Gender>()
            {
                new Gender() { Id = 1, Title = "Мужской", Symbol = "М", LanguageId = langId },
                new Gender() { Id = 2, Title = "Женский", Symbol = "Ж", LanguageId = langId },
                new Gender() { Id = 3, Title = "Неопределен", Symbol = "Н", LanguageId = langId }
            };

            return result;
        }

        private List<Gender> InitializeUkrainien()
        {
            var langId = LanguageHelper.Current.Ukrainien.Id;

            var result = new List<Gender>()
            {
                new Gender() { Id = 1, Title = "Чоловіча", Symbol = "Ч", LanguageId = langId },
                new Gender() { Id = 2, Title = "Жіноча", Symbol = "Ж", LanguageId = langId},
                new Gender() { Id = 3, Title = "Невизначена", Symbol = "Н", LanguageId = langId}
            };

            return result;
        }

        public string GetName(int id)
        {
            var roleName = Collection.FirstOrDefault(x => x.Id == id).Title;
            return roleName ?? throw new ArgumentException($"Entity {id} is not found");
        }
    }
}
