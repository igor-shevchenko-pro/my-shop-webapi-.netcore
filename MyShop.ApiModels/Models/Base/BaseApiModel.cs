using MyShop.ApiModels.Interfaces.Base;
using Newtonsoft.Json;
using System;

namespace MyShop.ApiModels.Models.Base
{
    public abstract class BaseApiModel<TKey> : IBaseApiModel<TKey>
    {
        [JsonProperty("id")]                    public virtual TKey Id { get; set; }
        [JsonProperty("created")]               public virtual DateTime? Created { get; set; }
        [JsonProperty("updated")]               public virtual DateTime? Updated { get; set; }
        [JsonProperty("activity_status")]       public virtual EntityActivityStatusEnum? ActivityStatus { get; set; }
    }
}
