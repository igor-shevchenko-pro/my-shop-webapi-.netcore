using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyShop.ApiModels.Models.Request
{
    public class SortedEntitiesRequestApiModel<TSorting>
    {
        [JsonProperty("start")]                      public int Start { get; set; }
        [JsonProperty("count")]                      public int Count { get; set; }
        [JsonProperty("sortings")]                   public List<TSorting> Sortings { get; set; }
        [JsonProperty("model_response_type")]        public TypeModelResponseEnum ModelResponseType { get; set; }
        [JsonProperty("query")]                      public string Query { get; set; }
    }

    public class SortedEntitiesByUserRequestApiModel<TSorting> : SortedEntitiesRequestApiModel<TSorting>
    {
        [JsonProperty("user_id")]                    public string UserId { get; set; }
    }
}
