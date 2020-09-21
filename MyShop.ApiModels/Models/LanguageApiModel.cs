using MyShop.ApiModels.Models.Base;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models
{
    public abstract class LanguageBaseApiModel : BaseApiModel<int>
    {
        [JsonProperty("title"), Required]         public string Title { get; set; }
        [JsonProperty("symbol"), Required]        public string Symbol { get; set; }
    }

    public class LanguageGetFullApiModel : LanguageBaseApiModel
    {
    }

    public class LanguageGetMinApiModel : LanguageBaseApiModel
    {
    }

    public class LanguageAddApiModel : LanguageBaseApiModel
    {
    }
}
