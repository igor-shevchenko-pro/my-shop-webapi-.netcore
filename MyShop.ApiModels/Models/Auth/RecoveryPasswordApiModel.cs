using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models.Auth
{
    public class RecoveryPasswordApiModel
    {
        [JsonProperty("contact"), Required]             public string Contact { get; set; }
        [JsonProperty("code"), Required]                public string Code { get; set; }
        [JsonProperty("new_password"), Required]        public string NewPassword { get; set; }
        [JsonProperty("contact_type"), Required]        public ContactTypeEnum ContactType { get; set; }
    }
}
