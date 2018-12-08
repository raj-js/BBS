using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Articles.Services;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Articles;
using static EDoc2.FAQ.Core.Application.Articles.Dtos.ArticleDtos;

namespace EDoc2.FAQ.Core.Application.Articles
{
    public class ArticleAppService : AppServiceBase, IArticleAppService
    {
        private readonly IArticleService _articleService;

        public ArticleAppService(IArticleService articleService)
        {
            _articleService = articleService ?? throw new ArgumentNullException(nameof(articleService));
        }

        public async Task<RespWapper> GetArticleTypes()
        {
            await Task.CompletedTask;
            return RespWapper<List<ValueTitlePair<int>>>.Successed(
                ArticleType.List().Select(type => new ValueTitlePair<int>
                {
                    Value = type.Id,
                    Title = type.Name
                }));
        }

        public async Task<RespWapper> GetArticleStates()
        {
            await Task.CompletedTask;
            return RespWapper<List<ValueTitlePair<int>>>.Successed(
                ArticleState.List().Select(type => new ValueTitlePair<int>
                {
                    Value = type.Id,
                    Title = type.Name
                }));
        }

        public async Task<RespWapper> Search(SearchReq req)
        {
            var query = _articleService.GetArticles();

            query = query
                .WhereFalse(req.Title.IsNullOrEmpty(), s => s.Title.Contains(req.Title, StringComparison.OrdinalIgnoreCase))
                .WhereFalse(req.Keywords.IsNullOrEmpty(), s => s.Keywords.Contains(req.Keywords, StringComparison.OrdinalIgnoreCase))
                .WhereNotNull(req.State, s => s.State.Id == req.State.Value)
                .WhereNotNull(req.Type, s => s.Type.Id == req.Type.Value);

            var dtos = query
                .OrderBy(req.OrderBy, req.IsAscending)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .AsEnumerable()
                .Select(ListItem.From)
                .ToList();

            return RespWapper<PagingDto<ListItem>>.Successed(new PagingDto<ListItem>
            {
                TotalCount = await query.CountAsync(),
                Dtos = dtos
            });
        }

        public Task<RespWapper> View(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> AddQuestion(AddQuestionReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> AddArticle(AddArticleReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> EditQuestion(EditQuestionReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> EditArticle(EditArticleReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> LikeArticle(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> DislikeArticle(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> LikeComment(long id)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> DislikeComment(long id)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> ReportArticle(ReportArticleReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> ReportComment(ReportCommentReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> ReplyArticle(ReplyArticleReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> ReplyComment(ReplyCommentReq req)
        {
            throw new NotImplementedException();
        }
    }
}
