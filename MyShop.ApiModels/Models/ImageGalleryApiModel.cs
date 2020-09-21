using MyShop.ApiModels.Models.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyShop.ApiModels.Models
{
    public abstract class ImageGalleryBaseApiModel : BaseApiModel<string>
    {
        [JsonProperty("gallery_files")]        public List<FileEntityGetMinApiModel> GalleryFiles { get; set; }
    }

    public class ImageGalleryGetFullApiModel : ImageGalleryBaseApiModel
    {
    }

    public class ImageGalleryGetMinApiModel : ImageGalleryBaseApiModel
    {
    }

    public class ImageGalleryAddApiModel : ImageGalleryBaseApiModel
    {
    }
}
