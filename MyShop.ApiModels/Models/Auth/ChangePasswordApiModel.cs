using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models.Auth
{
    public class ChangePasswordApiModel
    {
        [JsonProperty("old_password"), Required]        public string OldPassword { get; set; }
        [JsonProperty("new_password"), Required]        public string NewPassword { get; set; }
    }
}
