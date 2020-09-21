using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models.Auth
{
    public class RegistrationApiModel
    {
        [JsonProperty("login"), Required]               public string Login { get; set; }
        [JsonProperty("password"), Required]            public string Password { get; set; }
        [JsonProperty("contact_type"), Required]        public ContactTypeEnum ContactType { get; set; }
        [JsonProperty("user_profile")]                  public UserProfileAddApiModel UserProfile { get; set; }
    }
}