using EDoc2.FAQ.Core.Application.Articles.Dtos;
using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Applications;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Accounts;

namespace EDoc2.FAQ.Core.Application.Articles
{
    public class ArticleAppService : AppServiceBase, IArticleAppService
    {
        private readonly UserManager<User> _userManager;
        private readonly IDistributedCache _distributedCache;
        private readonly IArticleRepository _articleRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ILogger<ArticleAppService> _logger;

        private const string CachePrefix = nameof(ArticleAppService);

        public ArticleAppService(UserManager<User> userManager, 
            IDistributedCache distributedCache, 
            IArticleRepository articleRepository, 
            IApplicationRepository applicationRepository, 
            ILogger<ArticleAppService> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _applicationRepository = applicationRepository ?? throw new ArgumentNullException(nameof(applicationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<PagingDto<ArticleDtos.ListItem>> SearchAsync(ArticleDtos.Search searchDto)
        {
            throw new NotImplementedException();
        }

        public Task<PagingDto<ArticleDtos.ListItem>> SearchByEngineAsync(ArticleDtos.SearchByES searchByESDto)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleDtos.Details> FindAsync(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public ArticleDtos.Details AddArticle(ArticleDtos.Add addDto)
        {
            throw new NotImplementedException();
        }

        public ArticleDtos.Details EditArticle(ArticleDtos.Edit editDto)
        {
            throw new NotImplementedException();
        }

        public Task ViewArticle(Guid articleId, string clentIp, string operatorId = null)
        {
            throw new NotImplementedException();
        }

        public Task LikeArticle(string operatorId, Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task DislikeArticle(string operatorId, Guid articleId)
        {
            throw new NotImplementedException();
        }

        public Task ReportArticle(string operatorId, Guid articleId, string reason)
        {
            throw new NotImplementedException();
        }

        //public async Task<ArticleDetailDto> FindArticleByIdAsync(Guid id)
        //{
        //    var cacheKey = CacheExtensions.GenerateKey<ArticleDetailDto>(id.ToString());
        //    var dtoCache = await _distributedCache.GetAsync(cacheKey);
        //    if (dtoCache != null)
        //        return LZ4MessagePackSerializer.Deserialize<ArticleDetailDto>(dtoCache);

        //    var article = await _articleRepository.FindUserByIdAsync(id);
        //    var articleDto = ArticleDetailDto.FromEntity(article);

        //    dtoCache = LZ4MessagePackSerializer.Serialize(articleDto);
        //    await _distributedCache.SetAsync(cacheKey, dtoCache, GetCacheOptions());

        //    return articleDto;
        //}
    }
}
