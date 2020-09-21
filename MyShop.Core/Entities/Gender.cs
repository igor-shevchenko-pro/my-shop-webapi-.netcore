using MyShop.Core.Entities.Base;
using MyShop.Core.Interfaces.Entities.Base;

namespace MyShop.Core.Entities
{
    public class Gender : BaseManualEntity<int>, IBaseLanguageEntity<int>
    {
        public string Symbol { get; set; }

        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }
}
