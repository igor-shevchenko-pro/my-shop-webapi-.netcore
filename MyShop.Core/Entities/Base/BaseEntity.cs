using MyShop.ApiModels;
using MyShop.Core.Interfaces.Entities.Base;
using System;

namespace MyShop.Core.Entities.Base
{
    public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
    {
        public virtual TKey Id { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime Updated { get; set; }
        public virtual EntityActivityStatusEnum? ActivityStatus { get; set; }


        public virtual void SetId()
        {
            if (this is BaseEntity<string>) // Magic
            {
                (this as BaseEntity<string>).Id = Guid.NewGuid().ToString();
            }
        }

        public virtual void TrySetId()
        {
            if (Id == null) SetId();
        }
    }
}
