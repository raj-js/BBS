using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Articles.Services;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Categories.Services;
using static EDoc2.FAQ.Core.Application.Articles.Dtos.ArticleDtos;

namespace EDoc2.FAQ.Core.Application.Articles
{
    public class ArticleAppService : AppServiceBase, IArticleAppService
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public ArticleAppService(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService ?? throw new ArgumentNullException(nameof(articleService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        public async Task<RespWapper> GetArticleTypes()
        {
            await Task.CompletedTask;
            return RespWapper<List<ValueTitlePair<int>>>.Successed(
                Enums.List<ArticleType>().Select(type => new ValueTitlePair<int>
                {
                    Value = type.Id(),
                    Title = type.Name()
                }));
        }

        public async Task<RespWapper> GetArticleStates()
        {
            await Task.CompletedTask;
            return RespWapper<List<ValueTitlePair<int>>>.Successed(
                Enums.List<ArticleState>().Select(type => new ValueTitlePair<int>
                {
                    Value = type.Id(),
                    Title = type.Name()
                }));
        }

        public async Task<RespWapper> Search(SearchReq req)
        {
            var query = _articleService.GetArticles(CurrentUser);

            query = query
                .WhereFalse(req.Title.IsNullOrEmpty(), s => s.Title.Contains(req.Title, StringComparison.OrdinalIgnoreCase))
                .WhereFalse(req.Keywords.IsNullOrEmpty(), s => s.Keywords.Contains(req.Keywords, StringComparison.OrdinalIgnoreCase))
                .WhereNotNull(req.State, s => s.State == req.State)
                .WhereNotNull(req.Type, s => s.Type == req.Type);

            var dtos = query
                .OrderBy(req.OrderBy, req.IsAscending)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .AsEnumerable()
                .Select(ListItem.From)
                .ToList();

            return RespWapper.Successed(new PagingDto<ListItem>
            {
                TotalCount = await query.CountAsync(),
                Dtos = dtos
            });
        }

        public async Task<RespWapper> View(Guid id)
        {
            var visitor = CurrentUser;
            var article = await _articleService.FindById(visitor, id);

            if (article == null)
                return RespWapper.Failed(new Error
                {
                    Code = "ArticleNotFound",
                    Description = "资源不存在"
                });

            if (visitor == null)
                await _articleService.View(article, HttpContextAccessor.HttpContext.GetClientIp(), Application.ViewInterval);
            else
                await _articleService.View(article, visitor, Application.ViewInterval);

            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(ArticleResp.From(article));
        }

        public async Task<RespWapper> GetComments(LoadCommentsReq req)
        {
            var article = await _articleService.FindById(CurrentUser, req.ArticleId);

            if (article == null)
                return RespWapper.Failed(new Error
                {
                    Code = "ArticleNotFound",
                    Description = "资源不存在"
                });

            var query = _articleService.GetComments(CurrentUser, article);

            var dtos = query
                .OrderBy(req.OrderBy, req.IsAscending)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .AsEnumerable()
                .Select(CommentItem.From)
                .ToList();

            return RespWapper.Successed(new PagingDto<CommentItem>
            {
                TotalCount = await query.CountAsync(),
                Dtos = dtos
            });
        }

        public async Task<RespWapper> AddQuestion(AddQuestionReq req)
        {
            var question = req.To();
            await _articleService.Create(CurrentUser, question);

            var category = await _categoryService.FindCategoryById(req.CategoryId);
            if(category == null || !category.Enabled)
                return RespWapper.Failed(new Error
                {
                    Code = "InvalidCategory",
                    Description = "分类不存在或未启用"
                });

            await _categoryService.AddCategoryArticle(category, question);

            await _articleService.Release(CurrentUser, question, req.Score, Application.IsArticleAuditing);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(question.Id);
        }

        public async Task<RespWapper> AddArticle(AddArticleReq req)
        {
            var article = req.To();
            await _articleService.Create(CurrentUser, article);

            var category = await _categoryService.FindCategoryById(req.CategoryId);
            if (category == null || !category.Enabled)
                return RespWapper.Failed(new Error
                {
                    Code = "InvalidCategory",
                    Description = "分类不存在或未启用"
                });

            await _categoryService.AddCategoryArticle(category, article);

            await _articleService.Release(CurrentUser, article, Application.IsArticleAuditing);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(article.Id);
        }

        public async Task<RespWapper> AddDraft(AddArticleReq req)
        {
            var article = req.To();
            await _articleService.Create(CurrentUser, article);

            var category = await _categoryService.FindCategoryById(req.CategoryId);
            if (category == null || !category.Enabled)
                return RespWapper.Failed(new Error
                {
                    Code = "InvalidCategory",
                    Description = "分类不存在或未启用"
                });

            await _categoryService.AddCategoryArticle(category, article);

            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(article.Id);
        }

        public async Task<RespWapper> EditQuestion(EditQuestionReq req)
        {
            var question = req.To();
            await _articleService.Edit(CurrentUser, question, Application.IsArticleAuditing);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(question.Id);
        }

        public async Task<RespWapper> EditArticle(EditArticleReq req)
        {
            var article = req.To();
            await _articleService.Edit(CurrentUser, article);
            await _articleService.Edit(CurrentUser, article, Application.IsArticleAuditing);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(article.Id);
        }

        public async Task<RespWapper> EditCanComment(EditCanCommentReq req)
        {
            var article = req.To();
            await _articleService.Edit(CurrentUser, article);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(article.Id);
        }

        public async Task<RespWapper> Delete(Guid id)
        {
            var article = await _articleService.FindById(CurrentUser, id);
            if (article == null)
                return RespWapper.Failed(new Error
                {
                    Code = "ArticleNotFound",
                    Description = "资源不存在"
                });

            await _articleService.Delete(CurrentUser, article);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed();
        }

        public async Task<RespWapper> DeleteForced(Guid id)
        {
            var article = await _articleService.FindById(CurrentUser, id);
            if (article == null)
                return RespWapper.Failed(new Error
                {
                    Code = "ArticleNotFound",
                    Description = "资源不存在"
                });

            await _articleService.Delete(CurrentUser, article, false);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed();
        }

        public async Task<RespWapper> TopArticle(TopArticleReq req)
        {
            var article = await _articleService.FindById(CurrentUser, req.Id);
            if (article == null)
                return RespWapper.Failed(new Error
                {
                    Code = "ArticleNotFound",
                    Description = "资源不存在"
                });

            await _articleService.Top(CurrentUser, article, req.IsForever, req.ExpirationTime);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed();
        }

        public async Task<RespWapper> CancelTopArticle(Guid id)
        {
            var article = await _articleService.FindById(CurrentUser, id);
            if (article == null)
                return RespWapper.Failed(new Error
                {
                    Code = "ArticleNotFound",
                    Description = "资源不存在"
                });

            await _articleService.CancelTop(CurrentUser, article);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed();
        }


        public async Task<RespWapper> LikeArticle(Guid id)
        {
            var article = await _articleService.FindById(CurrentUser, id);
            if (article == null)
                return RespWapper.Failed(new Error
                {
                    Code = "ArticleNotFound",
                    Description = "资源不存在"
                });

            await _articleService.Like(CurrentUser, article);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(LikeOrNotResp.Initlize(article.Likes, article.Dislikes));
        }

        public async Task<RespWapper> DislikeArticle(Guid id)
        {
            var article = await _articleService.FindById(CurrentUser, id);
            if (article == null)
                return RespWapper.Failed(new Error
                {
                    Code = "ArticleNotFound",
                    Description = "资源不存在"
                });

            await _articleService.Dislike(CurrentUser, article);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(LikeOrNotResp.Initlize(article.Likes, article.Dislikes));
        }

        public async Task<RespWapper> LikeComment(long id)
        {
            var comment = await _articleService.FindCommentById(CurrentUser, id);
            if (comment == null)
                return RespWapper.Failed(new Error
                {
                    Code = "CommentNotFound",
                    Description = "资源不存在"
                });

            await _articleService.Like(CurrentUser, comment);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(LikeOrNotResp.Initlize(comment.Likes, comment.Dislikes));
        }

        public async Task<RespWapper> DislikeComment(long id)
        {
            var comment = await _articleService.FindCommentById(CurrentUser, id);
            if (comment == null)
                return RespWapper.Failed(new Error
                {
                    Code = "CommentNotFound",
                    Description = "资源不存在"
                });

            await _articleService.Dislike(CurrentUser, comment);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(LikeOrNotResp.Initlize(comment.Likes, comment.Dislikes));
        }


        public async Task<RespWapper> ReplyArticle(ReplyArticleReq req)
        {
            var article = await _articleService.FindById(CurrentUser, req.ArticleId);
            if (article == null)
                return RespWapper.Failed(new Error
                {
                    Code = "ArticleNotFound",
                    Description = "资源不存在"
                });

            var comment = ArticleComment.New(req.Content);
            await _articleService.Reply(CurrentUser, article, comment, Application.IsCommentAuditing);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(CommentItem.From(comment));
        }

        public async Task<RespWapper> ReplyComment(ReplyCommentReq req)
        {
            var article = await _articleService.FindById(CurrentUser, req.ArticleId);
            if (article == null)
                return RespWapper.Failed(new Error
                {
                    Code = "ArticleNotFound",
                    Description = "资源不存在"
                });

            var comment = await _articleService.FindCommentById(CurrentUser, req.CommentId);
            if (comment == null)
                return RespWapper.Failed(new Error
                {
                    Code = "CommentNotFound",
                    Description = "资源不存在"
                });

            var replyComment = ArticleComment.New(req.Content);
            await _articleService.Reply(CurrentUser, article, comment, replyComment, Application.IsCommentAuditing);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(CommentItem.From(replyComment));
        }

        public async Task<RespWapper> Finish(FinishReq req)
        {
            var article = await _articleService.FindById(CurrentUser, req.Id);
            if (article == null)
                return RespWapper.Failed(new Error
                {
                    Code = "ArticleNotFound",
                    Description = "资源不存在"
                });

            ArticleComment adoptComment = null;

            if (!req.Unsatisfactory)
            {
                if (req.AdoptId == null)
                    return RespWapper.Failed(new Error
                    {
                        Code = "CommentNotFound",
                        Description = "资源不存在"
                    });

                adoptComment = await _articleService.FindCommentById(CurrentUser, req.AdoptId.Value);
                if (adoptComment == null)
                    return RespWapper.Failed(new Error
                    {
                        Code = "CommentNotFound",
                        Description = "资源不存在"
                    });
            }

            await _articleService.Finish(article, adoptComment, req.Unsatisfactory);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed();
        }
    }
}
