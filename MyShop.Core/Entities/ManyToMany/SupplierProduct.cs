using MyShop.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Core.Entities.ManyToMany
{
    public class SupplierProduct : BaseEntity<string>
    {
        [ForeignKey("Supplier")]
        public override string Id { get; set; }
        public int SupplierLanguageId { get; set; }
        public virtual Supplier Supplier { get; set; }


        [ForeignKey("Product")]
        public string ProductId { get; set; }
        public int ProductLanguageId { get; set; }
        public virtual Product Product { get; set; }
    }
}
