using AutoMapper;
using EDoc2.FAQ.Core.Application.Articles.Dtos;
using EDoc2.FAQ.Core.Domain.Models.ArticleAggregate;

namespace EDoc2.FAQ.Core.Application.Articles.DtoMappers
{
    public class ArticleMapper : IMapper
    {
        public void Config(IMapperConfigurationExpression config)
        {
            config.CreateMap<Article, ArticleDetailDto>();
            config.CreateMap<ArticleDetailDto, Article>();
        }
    }
}
