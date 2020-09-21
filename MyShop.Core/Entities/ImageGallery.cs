using MyShop.Core.Entities.Base;
using System.Collections.Generic;

namespace MyShop.Core.Entities
{
    public class ImageGallery : BaseEntity<string>
    {
        public virtual Category Category { get; set; }
        public virtual ICollection<FileEntity> GalleryFileEntities { get; set; } = new List<FileEntity>();
    }
}