using EDoc2.FAQ.Web.Data;
using EDoc2.FAQ.Web.Data.Common;
using EDoc2.FAQ.Web.Data.Discuss;
using EDoc2.FAQ.Web.Data.Identity;
using EDoc2.FAQ.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Web.Services
{
    public class ArticleManager : IArticleManager
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ArticleManager> _logger;
        private readonly AppDbContext _appDbContext;

        public ArticleManager(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IServiceProvider provider,
            IMemoryCache memoryCache,
            ILogger<ArticleManager> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _memoryCache = memoryCache;

            var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _appDbContext = scope.ServiceProvider.GetService<AppDbContext>();
            _logger = logger;
        }

        public async Task<Article> AddArticle(AppUser publisher, Article article, List<ArticleCategory> categories)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            var publisherScore = publisher.UserClaims.Get(ClaimConsts.Score, int.Parse);
            if (publisherScore < article.RewardScore)
                throw new InvalidOperationException("财富值不足");

            var transcation = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                //修改财富值
                var scoreClaim = await _appDbContext.UserClaims.FirstAsync(c => c.UserId == publisher.Id
                    && c.ClaimType == ClaimConsts.Score);
                scoreClaim.ClaimValue = (int.Parse(scoreClaim.ClaimValue) - article.RewardScore).ToString();

                //新增财富值修改记录
                _appDbContext.LogScores.Add(new LogScore
                {
                    UserId = publisher.Id,
                    Type = "发布",
                    Description = $"发布新文章， 悬赏分：{article.RewardScore}",
                    DateTime = DateTime.Now,
                    Score = article.RewardScore * -1,
                    Total = int.Parse(scoreClaim.ClaimValue)
                });

                //发布通知

                //添加文章实体
                var entity = await _appDbContext.Articles.AddAsync(article);

                //关联 article categories
                await _appDbContext.ArticleCategories.AddRangeAsync(categories);

                await _appDbContext.SaveChangesAsync();

                transcation.Commit();

                return entity.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                transcation.Rollback();
                return null;
            }
        }

        public async Task<Article> GetArticle(string articleId)
        {
            if (string.IsNullOrWhiteSpace(articleId))
                throw new ArgumentNullException(nameof(articleId));

            return await _appDbContext.Articles.FindAsync(articleId);
        }

        public List<ArticleComment> GetArticleComments(Article article, int start = 0, int length = int.MaxValue)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            start = start < 0 ? 0 : start;
            length = length < 0 ? 15 : length;

            var comments = article.Comments
                .OrderBy(item => item.ReplyDate)
                .Skip(start)
                .Take(length);

            return comments.ToList();
        }

        public async Task<ArticleComment> AddArticleComment(Article article, ArticleComment articleComment)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            articleComment.ArticleId = article.Id;
            article.Comments.Add(articleComment);
            article.Replies = article.Comments.Count;

            await _appDbContext.SaveChangesAsync();
            return articleComment;
        }

        public async Task<List<Article>> GetArticles<TKey>(Expression<Func<Article, bool>> @where = null, int start = 0,
            int length = 0, bool isDesc = true, Expression<Func<Article, TKey>> orderby = null)
        {
            var query = _appDbContext.Articles.Where(item => true);
            if (@where != null)
                query = query.Where(@where);

            if (orderby == null)
                orderby = item => (TKey)((object)item.Id);

            var orderedQuery = isDesc ? query.OrderBy(@orderby) : query.OrderByDescending(@orderby);
            var articles = await orderedQuery.Skip(start)
                .Take(length)
                .ToListAsync();

            return articles;
        }

        public async Task<int> CountArticles(Expression<Func<Article, bool>> @where = null)
        {
            var query = _appDbContext.Articles.Where(item => true);
            if (@where != null)
                query = query.Where(@where);

            return await query.CountAsync();
        }

        public async Task<List<Category>> GetTags()
        {
            return await _appDbContext.Categories.Where(c => c.SubCategory == ArticleSubTypes.Tag).ToListAsync();
        }

        public async Task<(int praise, int tread)> OperateComment(AppUser appUser, string commentId, bool isPraise = true)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            if (string.IsNullOrWhiteSpace(commentId))
                throw new ArgumentNullException(nameof(commentId));

            var comment = await _appDbContext.ArticleComments.FindAsync(commentId);
            if (comment == null)
                throw new InvalidOperationException($"the id '{commentId}' of comment wasn't found.");

            var type = isPraise ? OperateType.Praise : OperateType.Tread;
            var commentOp = await _appDbContext.CommentOps
                .SingleOrDefaultAsync(c => appUser.Id.Equals(c.OperatorId) && commentId.Equals(c.CommentId));
            if (commentOp == null)
            {
                commentOp = new CommentOp
                {
                    CommentId = commentId,
                    OperatorId = appUser.Id,
                    OperateDate = DateTime.Now,
                    Type = type
                };
                await _appDbContext.CommentOps.AddAsync(commentOp);
            }
            else
            {
                if (commentOp.Type == type)
                {
                    _appDbContext.CommentOps.Remove(commentOp);
                }
                else
                {
                    commentOp.OperateDate = DateTime.Now;
                    commentOp.Type = type;
                }
            }
            await _appDbContext.SaveChangesAsync();

            comment.Goods = await _appDbContext.CommentOps
                .CountAsync(c => commentId.Equals(c.CommentId) && c.Type == OperateType.Praise);
            comment.Bads = await _appDbContext.CommentOps
                .CountAsync(c => commentId.Equals(c.CommentId) && c.Type == OperateType.Tread);
            await _appDbContext.SaveChangesAsync();
            return (comment.Goods, comment.Bads);
        }

        public async Task ViewArticle(Article article, string clientIp, AppUser appUser = null)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            if (string.IsNullOrWhiteSpace(clientIp))
                throw new ArgumentNullException(nameof(clientIp));


            var viewKey = $"view_{article.Id}_{(appUser == null ? clientIp : appUser.Id)}";
            if (!_memoryCache.TryGetValue(viewKey, out _))
            {
                //一小时内，单ip 或者 单用户 针对相同文章 只能访问一次, 过期时间设置太长会导致资源占用更多
                _memoryCache.Set(viewKey, DateTime.Now, TimeSpan.FromHours(1));
                article.Views += 1;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<(string appUserId, int replies)>> GetTopReplies(DateTime start, DateTime end, int tops)
        {
            var groups = await _appDbContext.ArticleComments
                .Where(item => item.ReplyDate >= start &&
                               item.ReplyDate <= DateTime.Now &&
                               !string.IsNullOrWhiteSpace(item.FromUserId))
                .GroupBy(item => item.FromUser)
                .Select(g => new
                {
                    UserId = g.Key.Id,
                    Replies = g.Count()
                })
                .OrderByDescending(g => g.Replies)
                .ToListAsync();
            return groups.Select(g => (g.UserId, g.Replies)).ToList();
        }

        public async Task<bool> CloseArticle(AppUser appUser, Article article, bool isSatisfied = false, string commentId = null)
        {
            if (appUser == null)
                throw new ArgumentNullException(nameof(appUser));

            if (article == null)
                throw new ArgumentNullException(nameof(article));

            if (isSatisfied && string.IsNullOrWhiteSpace(commentId))
                throw new ArgumentNullException(nameof(commentId));

            if (!article.PublisherId.Equals(appUser.Id))
                throw new UnauthorizedAccessException();

            if ((article.State & ArticleState.NotSolve) == 0) return false;

            if (isSatisfied)
            {
                article.State = article.State ^ ArticleState.NotSolve | ArticleState.Solved;
                var adoptComment = await _appDbContext.ArticleComments.FindAsync(commentId);
                if (adoptComment == null)
                    throw new InvalidOperationException($"编号为 {commentId} 的回复不存在");

                article.AdoptCommentId = commentId;
            }
            else
            {
                //无满意结贴
                article.State = article.State ^ ArticleState.NotSolve | ArticleState.Dissatisfied | ArticleState.Solved;
                article.AdoptCommentId = null;
            }
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
