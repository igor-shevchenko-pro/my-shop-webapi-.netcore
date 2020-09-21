using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models.Auth
{
    public class RefreshTokenApiModel
    {
        [JsonProperty("refresh_token"), Required]        public string RefreshToken { get; set; }
    }
}