using MyShop.Core.Entities.Base;
using MyShop.Core.Entities.ManyToMany;
using MyShop.Core.Interfaces.Entities.Base;
using System.Collections.Generic;

namespace MyShop.Core.Entities
{
    public class Product : BaseManualEntity<string>, IBaseLanguageEntity<string>
    {
        public string Alias { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal? OldPrice { get; set; }
        public string ItemNumber { get; set; }
        public string Description { get; set; }
        public string Specifications { get; set; } // Характеристики
        public double? Raiting { get; set; }
        public int AmountInStock { get; set; }

        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }

        public int? SuperPrice { get; set; }
        public int? TopOfSale { get; set; }
        public int? New { get; set; }
        public int? Share { get; set; }

        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        public string ImageGalleryId { get; set; }
        public virtual ImageGallery ImageGallery { get; set; }

        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }

        public virtual ICollection<Modification> Modifications { get; set; } = new List<Modification>();

        // ManyToMany
        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; } = new List<CategoryProduct>();
        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; } = new List<SupplierProduct>();
        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}