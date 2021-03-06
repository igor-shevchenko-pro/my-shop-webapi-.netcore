﻿using Newtonsoft.Json;

namespace MyShop.ApiModels.Models.Auth
{
    public class SignInApiModel
    {
        [JsonProperty("id")]                    public string UserId { get; set; }
        [JsonProperty("token")]                 public string Token { get; set; }
        [JsonProperty("refresh_token")]         public string RefreshToken { get; set; }
        [JsonProperty("token_life_date")]       public string LifeTime { get; set; }
    }
}
