using System;
using EDoc2.FAQ.Web.Data.Discuss;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EDoc2.FAQ.Web.Data.Identity;

namespace EDoc2.FAQ.Web.Services
{
    public interface IArticleManager
    {
        Task<Article> AddArticle(AppUser publisher, Article article, List<ArticleCategory> categories);

        Task<Article> GetArticle(string articleId);

        List<ArticleComment> GetArticleComments(Article article, int start = 0, int length = int.MaxValue);

        Task<ArticleComment> AddArticleComment(Article article, ArticleComment articleComment);

        Task<List<Article>> GetArticles<TKey>(Expression<Func<Article, bool>> @where = null, int start = 0,
            int length = 0, bool isDesc = true, Expression<Func<Article, TKey>> orderby = null);

        Task<int> CountArticles(Expression<Func<Article, bool>> @where = null);

        Task<List<Category>> GetTags();

        Task<(int praise, int tread)> OperateComment(AppUser appUser, string commentId, bool isPraise = true);

        Task ViewArticle(Article article, string clientIp, AppUser appUser = null);

        Task<List<(string appUserId, int replies)>> GetTopReplies(DateTime start, DateTime end, int tops);

        Task<bool> CloseArticle(AppUser appUser, Article article, bool isSatisfied = false, string commentId = null);
    }
}
