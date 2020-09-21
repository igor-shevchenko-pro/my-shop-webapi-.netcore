using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models.Auth
{
    public class RecoveryPasswordRequestApiModel
    {
        [JsonProperty("contact"), Required]                  public string Contact { get; set; }
        [JsonProperty("contact_type"), Required]             public ContactTypeEnum ContactType { get; set; }
        [JsonProperty("front_client_type"), Required]        public FrontClientType FrontClientType { get; set; }
    }
}