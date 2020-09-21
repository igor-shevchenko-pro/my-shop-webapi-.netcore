using MyShop.Core.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Core.Entities
{
    public class Brand : BaseManualEntity<int>
    {
        public string Alias { get; set; }
        public string Description { get; set; }

        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }


        [ForeignKey("FileEntity")]
        public string FileEntityId { get; set; }
        public virtual FileEntity FileEntity { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
