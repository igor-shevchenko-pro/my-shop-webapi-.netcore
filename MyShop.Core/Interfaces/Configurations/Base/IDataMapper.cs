using System.Collections.Generic;

namespace MyShop.Core.Interfaces.Configurations.Base
{
    public interface IDataMapper
    {
        To Parse<From, To>(From model);
        IEnumerable<To> ParseCollection<From, To>(IEnumerable<From> models);
    }
}
