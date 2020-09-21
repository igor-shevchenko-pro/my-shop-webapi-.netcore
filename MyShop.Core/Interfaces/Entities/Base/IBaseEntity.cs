using MyShop.ApiModels;
using System;

namespace MyShop.Core.Interfaces.Entities.Base
{
    public interface IBaseEntity<TKey> : IBaseEntity
    {
        TKey Id { get; set; }
    }

    public interface IBaseEntity
    {
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
        EntityActivityStatusEnum? ActivityStatus { get; set; }
    }
}
