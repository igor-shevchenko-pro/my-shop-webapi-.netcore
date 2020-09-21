using MyShop.ApiModels.Models.Base;
using MyShop.ApiModels.Validations.CustomAttributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.ApiModels.Models
{
    public abstract class FileEntityBaseApiModel : BaseApiModel<string>
    {
        [JsonProperty("name"), Required]                                  public string OriginalName { get; set; }
        [JsonProperty("extension"), Required, FileEntityExtension]        public string Extension { get; set; }
        [JsonProperty("type"), Required]                                  public FileEntityTypeEnum Type { get; set; }
        [JsonProperty("image_gallery_id")]                                public string ImageGalleryId { get; set; }
        [JsonProperty("url")]                                             public string Url { get; set; }
        [JsonIgnore]                                                      public string MimeType { get; set; }
    }

    public class FileEntityGetFullApiModel : FileEntityBaseApiModel
    {
        [JsonProperty("bytes")]                                           public byte[] Bytes { get; set; }
    }

    public class FileEntityGetMinApiModel : FileEntityBaseApiModel
    {
    }

    public class FileEntityAddApiModel : FileEntityBaseApiModel
    {
        [JsonProperty("bytes"), Required, FileEntitySize]                 public byte[] Bytes { get; set; }
    }

    public class FileEntitiesAddApiModel
    {
        [JsonProperty("files"), Required]                                 public List<FileEntityAddApiModel> FileEntities { get; set; }
        [JsonProperty("image_gallery_id")]                                public string ImageGalleryId { get; set; }
    }
}
