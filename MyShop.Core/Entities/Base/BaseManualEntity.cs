using MyShop.Core.Interfaces.Entities.Base;

namespace MyShop.Core.Entities.Base
{
    public abstract class BaseManualEntity<TKey> : BaseEntity<TKey>, IBaseManualEntity<TKey>
    {
        public string Title { get; set; }
    }
}
