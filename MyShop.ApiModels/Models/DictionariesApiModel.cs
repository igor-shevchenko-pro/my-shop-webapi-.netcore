using MyShop.ApiModels.Interfaces.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyShop.ApiModels.Models
{
    public class DictionariesApiModel<TKey>
    {
        [JsonProperty("languages")]        public List<IBaseApiModel<TKey>> Languages { get; set; }
        [JsonProperty("gender")]           public List<IBaseApiModel<TKey>> Genders { get; set; }
    }
}