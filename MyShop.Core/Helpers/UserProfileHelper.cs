using MyShop.ApiModels;
using MyShop.ApiModels.Models;
using System;

namespace MyShop.Core.Helpers
{
    public class UserProfileHelper
    {
        private static readonly UserProfileHelper _instance = new UserProfileHelper();
        public static UserProfileHelper Current => _instance;

        public UserProfileHelper()
        {
        }

        public void SetUserProfileDefaultDataForRegistration(ref UserProfileAddApiModel model)
        {
            if (model.LanguageId == 0) 
            {
                model.LanguageId = LanguageHelper.Current.Russian.Id;
            }
            if (model.GenderId == 0)
            {
                model.GenderId = GenderHelper.Current.Unknown.Id;
                model.GenderLanguageId = LanguageHelper.Current.Russian.Id;
            }

            model.FileEntityId = "default-user";
            model.Created = DateTime.UtcNow;
            model.Updated = model.Created;
            model.ActivityStatus = EntityActivityStatusEnum.Inactive;
        }

        public void SetUserProfileDefaultDataForEntity(ref UserProfileAddApiModel model)
        {
            if (model.LanguageId == 0)
            {
                model.LanguageId = LanguageHelper.Current.Russian.Id;
            }
            if (model.GenderId == 0)
            {
                model.GenderId = GenderHelper.Current.Unknown.Id;
                model.GenderLanguageId = LanguageHelper.Current.Russian.Id;
            }

            model.Created = DateTime.UtcNow;
            model.Updated = model.Created;
        }
    }
}
