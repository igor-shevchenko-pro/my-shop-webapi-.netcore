using MyShop.ApiModels;
using MyShop.Core.Entities.Base;

namespace MyShop.Core.Entities
{
    public class FileEntity : BaseEntity<string>
    {
        public string OriginalName { get; set; }
        public string Extension { get; set; }
        public FileEntityTypeEnum Type { get; set; }

        public string ImageGalleryId { get; set; }
        public virtual ImageGallery ImageGallery { get; set; }
    }
}