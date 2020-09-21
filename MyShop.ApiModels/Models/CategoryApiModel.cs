using MyShop.ApiModels.Models.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models
{
    public abstract class CategoryBaseApiModel : BaseApiModel<int>
    {
        [JsonProperty("title"), Required]                    public string Title { get; set; }
        [JsonProperty("alias"), Required]                    public string Alias { get; set; }
        [JsonProperty("extra_title")]                        public string ExtraTitle { get; set; }
        [JsonProperty("small_description")]                  public string SmallDescription { get; set; }
        [JsonProperty("long_description")]                   public string LongDescription { get; set; }
        [JsonProperty("language_id"), Required]              public int LanguageId { get; set; }
        [JsonProperty("seo_title")]                          public string SeoTitle { get; set; }
        [JsonProperty("seo_keywords")]                       public List<string> SeoKeywords { get; set; }
        [JsonProperty("seo_description")]                    public string SeoDescription { get; set; }
        [JsonProperty("image_gallery_id")]                   public string ImageGalleryId { get; set; }
        [JsonProperty("parent_category_id")]                 public int? ParentCategoryId { get; set; }
        [JsonProperty("parent_category_language_id")]        public int? ParentCategoryLanguageId { get; set; }
    }

    public class CategoryGetFullApiModel : CategoryBaseApiModel
    {
        [JsonProperty("parent_category")]                    public CategoryGetMinApiModel ParentCategory { get; set; }
        [JsonProperty("image_gallery")]                      public ImageGalleryGetMinApiModel ImageGallery { get; set; }
        [JsonProperty("products")]                           public List<ProductGetMinApiModel> Products { get; set; } 
    }

    public class CategoryGetMinApiModel : CategoryBaseApiModel
    {
        [JsonProperty("parent_category")]                    public CategoryGetMinApiModel ParentCategory { get; set; }
        [JsonProperty("image_gallery")]                      public ImageGalleryGetMinApiModel ImageGallery { get; set; }
    }

    public class CategoryAddApiModel : CategoryBaseApiModel
    {
    }
}