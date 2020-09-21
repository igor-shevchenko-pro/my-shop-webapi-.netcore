using MyShop.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Core.Entities.ManyToMany
{
    public class CategoryProduct : BaseEntity<int>
    {
        [ForeignKey("Category")]
        public override int Id { get; set; }
        public int CategoryLanguageId { get; set; }
        public virtual Category Category { get; set; }


        [ForeignKey("Product")]
        public string ProductId { get; set; }
        public int ProductLanguageId { get; set; }
        public virtual Product Product { get; set; }
    }
}