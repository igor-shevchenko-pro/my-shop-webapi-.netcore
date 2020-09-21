using MyShop.ApiModels.Models.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models
{
    public abstract class UserProfileBaseApiModel : BaseApiModel<string>
    {
        [JsonProperty("first_name"), Required]         public string FirstName { get; set; }
        [JsonProperty("second_name"), Required]        public string SecondName { get; set; }
        [JsonProperty("date_of_birth")]                public string DateOfBirth { get; set; }
        [JsonProperty("address")]                      public string Address { get; set; }
        [JsonProperty("file_id")]                      public string FileEntityId { get; set; }
        [JsonProperty("language_id"), Required]        public int LanguageId { get; set; }
        [JsonProperty("gender_id"), Required]          public int GenderId { get; set; }
    }

    public class UserProfileGetFullApiModel : UserProfileBaseApiModel
    {
        [JsonProperty("file")]                         public FileEntityGetMinApiModel FileEntity { get; set; }
    }

    public class UserProfileGetMinApiModel : UserProfileBaseApiModel
    {
    }

    public class UserProfileAddApiModel : UserProfileBaseApiModel
    {
        [JsonIgnore]                                   public int GenderLanguageId { get; set; }
    }
}