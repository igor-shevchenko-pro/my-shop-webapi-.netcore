using MyShop.Core.Entities.Base;
using MyShop.Core.Entities.ManyToMany;
using MyShop.Core.Interfaces.Entities.Base;
using System.Collections.Generic;

namespace MyShop.Core.Entities
{
    public class Supplier : BaseManualEntity<string>, IBaseLanguageEntity<string>
    {
        public string Description { get; set; }
        public string Email { get; set; }
        public string EmailExtra { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberExtra { get; set; }
        public string Manager { get; set; }
        public string ManagerExtra { get; set; }
        public string Address { get; set; }
        public string AddressExtra { get; set; }
        public string SomeInfo { get; set; }

        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }

        // ManyToMany
        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; } = new List<SupplierProduct>();
    }
}