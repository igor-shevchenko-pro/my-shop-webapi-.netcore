using MyShop.ApiModels.Models.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models
{
    public abstract class GenderBaseApiModel : BaseApiModel<int>
    {
        [JsonProperty("title"), Required]              public string Title { get; set; }
        [JsonProperty("symbol"), Required]             public string Symbol { get; set; }
        [JsonProperty("language_id"), Required]        public int LanguageId { get; set; }
    }

    public class GenderGetFullApiModel : GenderBaseApiModel
    {
    }

    public class GenderGetMinApiModel : GenderBaseApiModel
    {
    }

    public class GenderAddApiModel : GenderBaseApiModel
    {
    }
}
