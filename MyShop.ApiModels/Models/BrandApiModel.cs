using MyShop.ApiModels.Models.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models
{
    public abstract class BrandBaseApiModel : BaseApiModel<int>
    {
        [JsonProperty("title"), Required]        public string Title { get; set; }
        [JsonProperty("alias"), Required]        public string Alias { get; set; }
        [JsonProperty("description")]            public string Description { get; set; }
        [JsonProperty("seo_title")]              public string SeoTitle { get; set; }
        [JsonProperty("seo_keywords")]           public List<string> SeoKeywords { get; set; }
        [JsonProperty("seo_description")]        public string SeoDescription { get; set; }
        [JsonProperty("file_id")]                public string FileId { get; set; }
    }

    public class BrandGetFullApiModel : BrandBaseApiModel
    {
        [JsonProperty("file")]                   public FileEntityGetMinApiModel File { get; set; }
        [JsonProperty("products")]               public List<ProductGetMinApiModel> Products { get; set; }
    }

    public class BrandGetMinApiModel : BrandBaseApiModel
    {
        [JsonProperty("file")]                   public FileEntityGetMinApiModel File { get; set; }
    }

    public class BrandAddApiModel : BrandBaseApiModel
    {
    }
}