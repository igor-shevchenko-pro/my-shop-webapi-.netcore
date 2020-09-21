using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyShop.ApiModels.Models.Response
{
    public class PaginationResponseApiModel<IApiModel, TSorting>
    {
        [JsonProperty("start")]           public int Start { get; set; }
        [JsonProperty("count")]           public int Count { get; set; }
        [JsonProperty("total")]           public int Total { get; set; }
        [JsonProperty("sortings")]        public List<TSorting> EntitySortings { get; set; }
        [JsonProperty("models")]          public IEnumerable<IApiModel> Models { get; set; }
    }
}