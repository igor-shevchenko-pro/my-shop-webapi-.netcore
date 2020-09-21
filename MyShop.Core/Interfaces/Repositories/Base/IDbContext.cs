using System.Collections.Generic;

namespace MyShop.Core.Interfaces.Repositories.Base
{
    public interface IDbContext
    {
        ICollection<T> GetDataInstances<T>() where T : class, new();
        void SaveChanges<T>() where T : class, new();
    }
}
