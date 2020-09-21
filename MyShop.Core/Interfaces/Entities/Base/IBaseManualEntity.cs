namespace MyShop.Core.Interfaces.Entities.Base
{
    public interface IBaseManualEntity<TKey> : IBaseEntity<TKey>
    {
        string Title { get; set; }
    }
}
