using MyShop.Core.Entities.Base;
using MyShop.Core.Entities.ManyToMany;
using MyShop.Core.Interfaces.Entities.Base;
using System.Collections.Generic;

namespace MyShop.Core.Entities
{
    public class Category : BaseManualEntity<int>, IBaseLanguageEntity<int>
    {
        public string Alias { get; set; }
        public string ExtraTitle { get; set; }
        public string SmallDescription { get; set; }
        public string LongDescription { get; set; }

        public int? ParentCategoryId { get; set; }
        public int? ParentCategoryLanguageId { get; set; }
        public virtual Category ParentCategory { get; set; }

        public string ImageGalleryId { get; set; }
        public virtual ImageGallery ImageGallery { get; set; }

        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }

        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }

        // ManyToMany
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new List<CategoryProduct>();
    }
}
