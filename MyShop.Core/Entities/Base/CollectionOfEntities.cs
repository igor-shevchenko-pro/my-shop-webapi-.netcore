using System.Collections.Generic;

namespace MyShop.Core.Entities.Base
{
    public class CollectionOfEntities<TEntity, TSorting>
    {
        public int Start { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public List<TSorting> EntitySortings { get; set; }
        public IEnumerable<TEntity> Entities { get; set; }
    }
}
