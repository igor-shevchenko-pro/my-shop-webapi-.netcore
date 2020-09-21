using System;

namespace MyShop.ApiModels.Interfaces.Base
{
    public interface IBaseApiModel<TKey>
    {
        TKey Id { get; set; }
        DateTime? Created { get; set; }
        DateTime? Updated { get; set; }
        EntityActivityStatusEnum? ActivityStatus { get; set; }
    }
}
