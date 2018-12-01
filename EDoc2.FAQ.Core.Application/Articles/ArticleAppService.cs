using EDoc2.FAQ.Core.Application.Articles.Dtos;
using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using System;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Application.Articles
{
    public class ArticleAppService : AppServiceBase, IArticleAppService
    {
        public Task<Response> Search(ArticleDtos.SearchReq req)
        {
            throw new NotImplementedException();
        }

        public Task<Response> View(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> AddQuestion(ArticleDtos.AddQuestionReq req)
        {
            throw new NotImplementedException();
        }

        public Task<Response> AddArticle(ArticleDtos.AddArticleReq req)
        {
            throw new NotImplementedException();
        }

        public Task<Response> EditQuestion(ArticleDtos.EditQuestionReq req)
        {
            throw new NotImplementedException();
        }

        public Task<Response> EditArticle(ArticleDtos.EditArticleReq req)
        {
            throw new NotImplementedException();
        }

        public Task<Response> LikeArticle(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DislikeArticle(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> LikeComment(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DislikeComment(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> ReportArticle(ArticleDtos.ReportArticleReq req)
        {
            throw new NotImplementedException();
        }

        public Task<Response> ReportComment(ArticleDtos.ReportCommentReq req)
        {
            throw new NotImplementedException();
        }

        public Task<Response> ReplyArticle(ArticleDtos.ReplyArticleReq req)
        {
            throw new NotImplementedException();
        }

        public Task<Response> ReplyComment(ArticleDtos.ReplyCommentReq req)
        {
            throw new NotImplementedException();
        }
    }
}
