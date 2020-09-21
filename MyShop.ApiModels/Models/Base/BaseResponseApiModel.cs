using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyShop.ApiModels.Models.Base
{
    public class BaseResponseApiModel
    {
        [JsonProperty("errors")]        public List<string> Errors { get; set; }
    }
}
