using MyShop.Core.Entities.Base;

namespace MyShop.Core.Entities
{
    public class Modification : BaseManualEntity<string>
    {
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string ProductId { get; set; }
        public int ProductLanguageId { get; set; }
        public virtual Product Product { get; set; }
    }
}
