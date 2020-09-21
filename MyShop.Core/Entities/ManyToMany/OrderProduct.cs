using MyShop.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Core.Entities.ManyToMany
{
    public class OrderProduct : BaseEntity<string>
    {
        [ForeignKey("Order")]
        public override string Id { get; set; }
        public virtual Order Order { get; set; }


        [ForeignKey("Product")]
        public string ProductId { get; set; }
        public int ProductLanguageId { get; set; }
        public virtual Product Product { get; set; }
    }
}