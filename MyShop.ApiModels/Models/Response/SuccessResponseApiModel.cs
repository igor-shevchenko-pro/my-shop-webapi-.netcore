using Newtonsoft.Json;

namespace MyShop.ApiModels.Models.Response
{
    public class SuccessResponseApiModel
    {
        [JsonProperty("response")]        public string Response { get; set; }
        [JsonProperty("id")]              public string Id { get; set; }
    }
}