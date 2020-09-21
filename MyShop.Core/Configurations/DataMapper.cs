using AutoMapper;
using MyShop.Core.Interfaces.Configurations.Base;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Core.Configurations
{
    public class DataMapper : IDataMapper
    {
        IMapper _mapper;

        public DataMapper(IMapper dataAdapter)
        {
            _mapper = dataAdapter;
        }

        public To Parse<From, To>(From model)
        {
            return _mapper.Map<From, To>(model);
        }

        public IEnumerable<To> ParseCollection<From, To>(IEnumerable<From> models)
        {
            return models.Select(Parse<From, To>).ToList();
        }
    }
}
