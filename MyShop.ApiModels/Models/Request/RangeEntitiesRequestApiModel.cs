using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyShop.ApiModels.Models.Request
{
    public class RangeEntitiesRequestApiModel<TKey>
    {
        [JsonProperty("ids")]                    public List<TKey> Ids { get; set; }
    }
}
