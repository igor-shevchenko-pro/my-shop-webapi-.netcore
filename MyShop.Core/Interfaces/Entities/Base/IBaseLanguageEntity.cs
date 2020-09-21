namespace MyShop.Core.Interfaces.Entities.Base
{
    public interface IBaseLanguageEntity<TKey> : IBaseEntity<TKey>
    {
        int LanguageId { get; set; }
    }
}
