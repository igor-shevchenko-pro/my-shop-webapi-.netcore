using MyShop.ApiModels.Models.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models
{
    public abstract class UserBaseApiModel : BaseApiModel<string>
    {
        [JsonProperty("user_name")]                     public string UserName { get; set; }
        [JsonProperty("email")]                         public string Email { get; set; }
        [JsonProperty("phone")]                         public string Phone { get; set; }
        [JsonProperty("is_email_confirmed")]            public bool IsEmailConfirmed { get; set; }
        [JsonProperty("is_phone_confirmed")]            public bool IsPhoneConfirmed { get; set; }
        [JsonProperty("user_profile_id")]               public string UserProfileId { get; set; }
    }

    public class UserGetFullApiModel : UserBaseApiModel
    {
        [JsonProperty("roles")]                         public List<RoleGetMinApiModel> Roles { get; set; }
        [JsonProperty("user_profile")]                  public UserProfileGetFullApiModel UserProfile { get; set; }
    }

    public class UserGetMinApiModel : UserBaseApiModel
    {
        [JsonProperty("user_profile")]                  public UserProfileGetFullApiModel UserProfile { get; set; }
    }

    public class UserAddApiModel : UserBaseApiModel
    {
        [JsonProperty("password"), Required]            public string Password { get; set; }
        [JsonProperty("roles"), Required]               public List<RoleAddApiModel> Roles { get; set; }
        [JsonProperty("user_profile"), Required]        public UserProfileAddApiModel UserProfile { get; set; }
    }
}
