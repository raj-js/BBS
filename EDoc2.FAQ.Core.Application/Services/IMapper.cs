using AutoMapper;
using System;

namespace EDoc2.FAQ.Core.Application
{
    public interface IMapper
    {
        void Config(IMapperConfigurationExpression config);
    }
}
