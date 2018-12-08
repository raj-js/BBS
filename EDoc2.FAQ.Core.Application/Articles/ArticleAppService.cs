using EDoc2.FAQ.Core.Application.Articles.Dtos;
using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using System;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Application.Articles
{
    public class ArticleAppService : AppServiceBase, IArticleAppService
    {
        public Task<RespWapper> Search(ArticleDtos.SearchReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> View(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> AddQuestion(ArticleDtos.AddQuestionReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> AddArticle(ArticleDtos.AddArticleReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> EditQuestion(ArticleDtos.EditQuestionReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> EditArticle(ArticleDtos.EditArticleReq req)
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

        public Task<RespWapper> ReportArticle(ArticleDtos.ReportArticleReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> ReportComment(ArticleDtos.ReportCommentReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> ReplyArticle(ArticleDtos.ReplyArticleReq req)
        {
            throw new NotImplementedException();
        }

        public Task<RespWapper> ReplyComment(ArticleDtos.ReplyCommentReq req)
        {
            throw new NotImplementedException();
        }
    }
}
