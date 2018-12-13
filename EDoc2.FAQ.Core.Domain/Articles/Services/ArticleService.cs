using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Accounts.Services;
using EDoc2.FAQ.Core.Domain.Exceptions;
using EDoc2.FAQ.Core.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Articles.Services
{
    public class ArticleService : DomainService, IArticleService
    {
        private readonly IArticleRepository _articleRepo;
        private readonly IAccountService _accountService;

        public ArticleService(IArticleRepository articleRepo, IAccountService accountService)
        {
            _articleRepo = articleRepo ?? throw new ArgumentNullException(nameof(articleRepo));
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public IQueryable<Article> GetArticles(User @operator)
        {
            var query = _articleRepo.GetArticles();

            //游客
            const ArticleState allowState = ArticleState.Published | 
                                            ArticleState.Solved | 
                                            ArticleState.UnSolved |
                                            ArticleState.Unsatisfactory;

            if (@operator == null)
                return query.Where(s => (s.State & allowState) > 0);

            //版主
            if (@operator.IsModerator)
                return query.Where(s => !s.State.Equals(ArticleState.Deleted));

            //管理员
            if (@operator.IsAdministrator)
                return query;

            //普通会员
            return query.Where(s => (s.State & allowState) > 0 || 
                                    (!s.State.Equals(ArticleState.Deleted) && s.CreatorId.Equals(@operator.Id)));
        }

        public async Task<Article> FindById(User @operator, Guid articleId)
        {
            var article = await _articleRepo.FindById(articleId);

            if (article == null) return null;

            //游客
            const ArticleState allowState = ArticleState.Published |
                                            ArticleState.Solved |
                                            ArticleState.UnSolved |
                                            ArticleState.Unsatisfactory;

            if (@operator == null && (article.State & allowState) == 0)
            {
                return null;
            }

            //普通会员
            if (@operator != null && !@operator.IsAdministrator && !@operator.IsModerator &&
                (article.State & allowState) == 0 && 
                (!@operator.Id.Equals(article.CreatorId) || article.State.Equals(ArticleState.Deleted)))
            {
                return null;
            }

            //版主
            if (@operator != null && !@operator.IsAdministrator && @operator.IsModerator &&
                article.State.Equals(ArticleState.Deleted))
            {
                return null;
            }

            //管理员
            //...

            return article;
        }

        public async Task View(Article article, User user, int viewInterval)
        {
            var lastView = await _articleRepo
                .GetOperations()
                .OrderBy(s => s.OperationTime)
                .LastOrDefaultAsync(s => s.OperatorType.Equals(ArticleOperationOperatorType.User) &&
                            s.Operator.Equals(user.Id) &&
                            s.TargetType.Equals(ArticleOperationTargetType.Article) &&
                            s.Target == article.Id.ToString() &&
                            s.Type.Equals(ArticleOperationType.View));

            //上一次查看的时间与当前时间间隔不足 view interval 设定时间
            if (lastView != null && lastView.OperationTime.AddMinutes(viewInterval) > DateTime.Now) return;

            //增加访问记录
            var operation = new ArticleOperation
            {
                Operator = user.Id,
                OperatorType = ArticleOperationOperatorType.User,
                Target = article.Id.ToString(),
                TargetType = ArticleOperationTargetType.Article,
                Type = ArticleOperationType.View,
                OperationTime = DateTime.Now,
                IsCancel = false
            };
            await _articleRepo.AddOperation(operation);

            //增加文章访问量
            article.GetOrSetProperty(ArticleProperty.Pv, article.Pv + 1);
        }

        public async Task View(Article article, string clientIp, int viewInterval)
        {
            var lastView = await _articleRepo
                .GetOperations()
                .OrderBy(s => s.OperationTime)
                .LastOrDefaultAsync(s => s.OperatorType.Equals(ArticleOperationOperatorType.Ip) &&
                                         s.Operator.Equals(clientIp) &&
                                         s.TargetType.Equals(ArticleOperationTargetType.Article) &&
                                         s.Target == article.Id.ToString() &&
                                         s.Type.Equals(ArticleOperationType.View));

            //上一次查看的时间与当前时间间隔不足 view interval 设定时间
            if (lastView != null && lastView.OperationTime.AddMinutes(viewInterval) > DateTime.Now) return;

            //增加访问记录
            var operation = new ArticleOperation
            {
                Operator = clientIp,
                OperatorType = ArticleOperationOperatorType.Ip,
                Target = article.Id.ToString(),
                TargetType = ArticleOperationTargetType.Article,
                Type = ArticleOperationType.View,
                OperationTime = DateTime.Now,
                IsCancel = false
            };
            await _articleRepo.AddOperation(operation);

            //增加文章访问量
            article.GetOrSetProperty(ArticleProperty.Pv, article.Pv + 1);
        }

        public IQueryable<ArticleComment> GetComments(User @operator, Article article)
        {
            var query = _articleRepo.GetArticleComments()
                .Include(s => s.Creator)
                .Where(s => s.ArticleId == article.Id);

            if (@operator == null)
                return query.Where(s => s.State.Equals(ArticleCommentState.Validated));

            if (@operator.IsAdministrator)
                return query;

            if (@operator.IsModerator)
                return query.Where(s => !s.State.Equals(ArticleCommentState.Deleted));

            return query.Where(s => s.State.Equals(ArticleCommentState.Validated) ||
                                   (s.CreatorId.Equals(@operator.Id) && !s.State.Equals(ArticleCommentState.Deleted)));
        }

        public async Task<ArticleComment> FindCommentById(User @operator, long id)
        {
            var comment = await _articleRepo.GetArticleComments().SingleOrDefaultAsync(s => s.Id == id);

            if (comment == null) return null;

            //游客
            if (@operator == null &&
                !comment.State.Equals(ArticleCommentState.Validated))
            {
                return null;
            }

            //普通会员
            if (@operator != null &&
                !comment.State.Equals(ArticleCommentState.Validated) && 
                (!comment.CreatorId.Equals(@operator.Id) || comment.State.Equals(ArticleCommentState.Deleted)))
            {
                return null;
            }

            //版主
            if (@operator != null && !@operator.IsAdministrator && @operator.IsModerator && 
                comment.State.Equals(ArticleCommentState.Deleted))
            {
                return null;
            }

            //管理员
            //...

            return comment;
        }

        public async Task Create(User author, Article article)
        {
            article.CreatorId = author.Id;
            article.CreationTime = DateTime.Now;

            if (article.Type.Equals(ArticleType.Question))
                article.CanComment = true;

            article.SetDraft();
            await _articleRepo.Add(article);
        }

        public async Task Edit(User @operator, Article article, bool approve)
        {
            if (article.IsTransient()) return;
            if (!@operator.Id.Equals(article.CreatorId)) return;
            if (@operator.IsMuted) return;

            //文章处于草稿， 审核驳回， 审核通过（发布）， 未结帖状态时， 才能被编辑
            if (article.State.Equals(ArticleState.Draft) ||
                article.State.Equals(ArticleState.Rejected) ||
                article.State.Equals(ArticleState.Published) ||
                article.State.Equals(ArticleState.UnSolved))
            {
                if (approve)
                    article.SetDraft();
                else
                    article.SetAuditing();

                if (article.Type.Equals(ArticleType.Question))
                {
                    await _articleRepo.Update(article,
                        nameof(Article.Content),
                        nameof(Article.Keywords),
                        nameof(Article.State));
                }
                else if (article.Type.Equals(ArticleType.Article))
                {
                    await _articleRepo.Update(article,
                        nameof(Article.Title),
                        nameof(Article.Summary),
                        nameof(Article.Content),
                        nameof(Article.Keywords),
                        nameof(Article.CanComment),
                        nameof(Article.State));
                }
            }
        }

        public async Task Release(User author, Article article, bool approve = false)
        {
            if (!author.Id.Equals(article.CreatorId)) return;
            if (!article.State.Equals(ArticleState.Draft)) return;

            if (approve)
                article.SetAuditing();
            else
                article.SetPublished();

            await _articleRepo.Update(article, nameof(Article.State));
        }

        public async Task Release(User author, Article article, int score, bool approve = false)
        {
            if (!author.Id.Equals(article.CreatorId)) return;
            if (!article.State.Equals(ArticleState.Draft)) return;

            await _accountService.MinuScore(author, score, UserScoreChangeReason.AskQuestion);

            article.GetOrSetProperty(ArticleProperty.HasSpentSocre, true);
            article.GetOrSetProperty(ArticleProperty.Score, score);

            if (approve)
                article.SetAuditing();
            else
                article.SetPublished();
        }

        public async Task Delete(User @operator, Article article, bool isSoftDelete = true)
        {
            if (article.IsTransient()) return;

            //审核状态无法删除
            if (article.State.Equals(ArticleState.Auditing)) return;

            if (isSoftDelete)
                article.SetDeleted(@operator.Id);
            else
                await _articleRepo.Delete(article);

            //增加操作记录
            var operation = new ArticleOperation
            {
                Operator = @operator.Id,
                OperatorType = ArticleOperationOperatorType.User,
                Target = article.Id.ToString(),
                TargetType = ArticleOperationTargetType.Article,
                Type = ArticleOperationType.Delete,
                OperationTime = DateTime.Now,
                IsCancel = false
            };
            await _articleRepo.AddOperation(operation);
        }

        public async Task Finish(Article article, ArticleComment adoptComment = null, bool unsatisfactory = true)
        {
            if (!article.State.Equals(ArticleState.UnSolved)) return;

            //无满意结贴
            if (unsatisfactory)
            {
                article.SetUnsatisfactory();
            }
            else
            {
                if (adoptComment == null)
                    throw new ArgumentNullException(nameof(adoptComment));

                article.SetSolved(adoptComment.Id);

                var replyer = await _accountService.FindUserByIdAsync(adoptComment.CreatorId);
                if (replyer == null)
                    throw new AccountNotFoundException(adoptComment.CreatorId);

                await _accountService.PlusScore(replyer, article.Score, UserScoreChangeReason.BestReply);
            }
        }

        public async Task Like(User @operator, Article article)
        {
            //查看当前用户是否点赞过此文章
            var likeOperation = await _articleRepo.GetOperations()
                .SingleOrDefaultAsync(s => s.OperatorType.Equals(ArticleOperationOperatorType.User) &&
                               s.Operator.Equals(@operator.Id) &&
                               s.TargetType.Equals(ArticleOperationTargetType.Article) &&
                               s.Target == article.Id.ToString() &&
                               s.Type.Equals(ArticleOperationType.Like));

            var likes = article.Likes;
            if (likeOperation != null)
            {
                likeOperation.IsCancel = !likeOperation.IsCancel;
                await _articleRepo.UpdateOperation(likeOperation, nameof(ArticleOperation.IsCancel));

                if (likeOperation.IsCancel)
                    likes--;
                else
                    likes++;
            }
            else
            {
                likeOperation = new ArticleOperation
                {
                    Operator = @operator.Id,
                    OperatorType = ArticleOperationOperatorType.User,
                    Target = article.Id.ToString(),
                    TargetType = ArticleOperationTargetType.Article,
                    Type = ArticleOperationType.Like,
                    OperationTime = DateTime.Now,
                    IsCancel = false
                };
                await _articleRepo.AddOperation(likeOperation);

                likes++;
            }

            var likesProperty = article.GetOrSetProperty(ArticleProperty.Likes, likes);
            await _articleRepo.UpdateArticleProperty(likesProperty);

            //查看当前用户是否踩过此文章， 如果有， 则取消
            var dislikeOperation = await _articleRepo.GetOperations()
                .SingleOrDefaultAsync(s => s.OperatorType.Equals(ArticleOperationOperatorType.User) &&
                                           s.Operator.Equals(@operator.Id) &&
                                           s.TargetType.Equals(ArticleOperationTargetType.Article) &&
                                           s.Target == article.Id.ToString() &&
                                           s.Type.Equals(ArticleOperationType.Dislike));

            //踩文章记录存在，且未取消
            if (dislikeOperation != null && !dislikeOperation.IsCancel)
            {
                dislikeOperation.IsCancel = true;
                await _articleRepo.UpdateOperation(dislikeOperation, nameof(ArticleOperation.IsCancel));

                var dislikesProperty = article.GetOrSetProperty(ArticleProperty.Dislikes, article.Dislikes - 1);
                await _articleRepo.UpdateArticleProperty(dislikesProperty);
            }
        }

        public async Task Dislike(User @operator, Article article)
        {
            //查看当前用户是否踩过此文章
            var dislikeOperation = await _articleRepo.GetOperations()
                .SingleOrDefaultAsync(s => s.OperatorType.Equals(ArticleOperationOperatorType.User) &&
                               s.Operator.Equals(@operator.Id) &&
                               s.TargetType.Equals(ArticleOperationTargetType.Article) &&
                               s.Target == article.Id.ToString() &&
                               s.Type.Equals(ArticleOperationType.Dislike));

            var dislikes = article.Dislikes;
            if (dislikeOperation != null)
            {
                dislikeOperation.IsCancel = !dislikeOperation.IsCancel;
                await _articleRepo.UpdateOperation(dislikeOperation, nameof(ArticleOperation.IsCancel));

                if (dislikeOperation.IsCancel)
                    dislikes--;
                else
                    dislikes++;
            }
            else
            {
                dislikeOperation = new ArticleOperation
                {
                    Operator = @operator.Id,
                    OperatorType = ArticleOperationOperatorType.User,
                    Target = article.Id.ToString(),
                    TargetType = ArticleOperationTargetType.Article,
                    Type = ArticleOperationType.Dislike,
                    OperationTime = DateTime.Now,
                    IsCancel = false
                };
                await _articleRepo.AddOperation(dislikeOperation);

                dislikes++;
            }

            var dislikesProperty = article.GetOrSetProperty(ArticleProperty.Dislikes, dislikes);
            await _articleRepo.UpdateArticleProperty(dislikesProperty);

            //查看当前用户是否赞过此文章， 如果有， 则取消
            var likeOperation = await _articleRepo.GetOperations()
                .SingleOrDefaultAsync(s => s.OperatorType.Equals(ArticleOperationOperatorType.User) &&
                                           s.Operator.Equals(@operator.Id) &&
                                           s.TargetType.Equals(ArticleOperationTargetType.Article) &&
                                           s.Target == article.Id.ToString() &&
                                           s.Type.Equals(ArticleOperationType.Like));

            //赞文章记录存在，且未取消
            if (likeOperation != null && !likeOperation.IsCancel)
            {
                likeOperation.IsCancel = true;
                await _articleRepo.UpdateOperation(likeOperation, nameof(ArticleOperation.IsCancel));

                var likesProperty = article.GetOrSetProperty(ArticleProperty.Likes, article.Likes - 1);
                await _articleRepo.UpdateArticleProperty(likesProperty);
            }
        }

        public async Task Like(User @operator, ArticleComment comment)
        {
            //查看当前用户是否点赞过此评论
            var likeOperation = await _articleRepo.GetOperations()
                .SingleOrDefaultAsync(s => s.OperatorType.Equals(ArticleOperationOperatorType.User) &&
                               s.Operator.Equals(@operator.Id) &&
                               s.TargetType.Equals(ArticleOperationTargetType.Comment) &&
                               s.Target == comment.Id.ToString() &&
                               s.Type.Equals(ArticleOperationType.Like));

            var likes = comment.Likes;
            if (likeOperation != null)
            {
                likeOperation.IsCancel = !likeOperation.IsCancel;
                await _articleRepo.UpdateOperation(likeOperation, nameof(ArticleOperation.IsCancel));

                if (likeOperation.IsCancel)
                    likes--;
                else
                    likes++;
            }
            else
            {
                likeOperation = new ArticleOperation
                {
                    Operator = @operator.Id,
                    OperatorType = ArticleOperationOperatorType.User,
                    Target = comment.Id.ToString(),
                    TargetType = ArticleOperationTargetType.Comment,
                    Type = ArticleOperationType.Like,
                    OperationTime = DateTime.Now,
                    IsCancel = false
                };
                await _articleRepo.AddOperation(likeOperation);

                likes++;
            }

            comment.Likes = likes;

            //查看当前用户是否踩过此评论， 如果有， 则取消
            var dislikeOperation = await _articleRepo.GetOperations()
                .SingleOrDefaultAsync(s => s.OperatorType.Equals(ArticleOperationOperatorType.User) &&
                                           s.Operator.Equals(@operator.Id) &&
                                           s.TargetType.Equals(ArticleOperationTargetType.Comment) &&
                                           s.Target == comment.Id.ToString() &&
                                           s.Type.Equals(ArticleOperationType.Dislike));

            //踩评论记录存在，且未取消
            if (dislikeOperation != null && !dislikeOperation.IsCancel)
            {
                dislikeOperation.IsCancel = true;
                await _articleRepo.UpdateOperation(dislikeOperation, nameof(ArticleOperation.IsCancel));

                comment.Dislikes = comment.Dislikes - 1;
            }

            await _articleRepo.UpdateComment(comment,
                nameof(ArticleComment.Likes),
                nameof(ArticleComment.Dislikes));
        }

        public async Task Dislike(User @operator, ArticleComment comment)
        {
            //查看当前用户是否踩过此评论
            var dislikeOperation = await _articleRepo.GetOperations()
                .SingleOrDefaultAsync(s => s.OperatorType.Equals(ArticleOperationOperatorType.User) &&
                               s.Operator.Equals(@operator.Id) &&
                               s.TargetType.Equals(ArticleOperationTargetType.Comment) &&
                               s.Target == comment.Id.ToString() &&
                               s.Type.Equals(ArticleOperationType.Dislike));

            var dislikes = comment.Dislikes;
            if (dislikeOperation != null)
            {
                dislikeOperation.IsCancel = !dislikeOperation.IsCancel;
                await _articleRepo.UpdateOperation(dislikeOperation, nameof(ArticleOperation.IsCancel));

                if (dislikeOperation.IsCancel)
                    dislikes--;
                else
                    dislikes++;
            }
            else
            {
                dislikeOperation = new ArticleOperation
                {
                    Operator = @operator.Id,
                    OperatorType = ArticleOperationOperatorType.User,
                    Target = comment.Id.ToString(),
                    TargetType = ArticleOperationTargetType.Comment,
                    Type = ArticleOperationType.Dislike,
                    OperationTime = DateTime.Now,
                    IsCancel = false
                };
                await _articleRepo.AddOperation(dislikeOperation);

                dislikes++;
            }


            comment.Dislikes = dislikes;

            //查看当前用户是否赞过此评论， 如果有， 则取消
            var likeOperation = await _articleRepo.GetOperations()
                .SingleOrDefaultAsync(s => s.OperatorType.Equals(ArticleOperationOperatorType.User) &&
                                           s.Operator.Equals(@operator.Id) &&
                                           s.TargetType.Equals(ArticleOperationTargetType.Comment) &&
                                           s.Target == comment.Id.ToString() &&
                                           s.Type.Equals(ArticleOperationType.Like));

            //赞评论记录存在，且未取消
            if (likeOperation != null && !likeOperation.IsCancel)
            {
                likeOperation.IsCancel = true;
                await _articleRepo.UpdateOperation(likeOperation, nameof(ArticleOperation.IsCancel));

                comment.Likes = comment.Likes - 1;
            }

            await _articleRepo.UpdateComment(comment,
                nameof(ArticleComment.Likes),
                nameof(ArticleComment.Dislikes));
        }

        public async Task Reply(User @operator, Article article, ArticleComment replyComment, bool approve = false)
        {
            if (!article.CanComment) return;

            replyComment.ArticleId = article.Id;
            replyComment.CreatorId = @operator.Id;
            replyComment.ParentCommentId = null;

            if (approve)
                replyComment.SetAuditing();
            else
                replyComment.SetValidated();

            await _articleRepo.AddComment(replyComment);
        }

        public async Task Reply(User @operator, Article article, ArticleComment comment, ArticleComment replyComment,
            bool approve = false)
        {
            if (!article.CanComment) return;
            if (!comment.State.Equals(ArticleCommentState.Validated)) return;

            replyComment.ArticleId = article.Id;
            replyComment.CreatorId = @operator.Id;
            replyComment.ParentCommentId = comment.Id;

            replyComment.Likes = 0;
            replyComment.Dislikes = 0;

            if (approve)
                replyComment.SetAuditing();
            else
                replyComment.SetValidated();

            await _articleRepo.AddComment(replyComment);
        }

        public async Task Top(User @operator, Article article, bool isForever, DateTime? expirationTime = null)
        {
            if (!@operator.IsAdministrator && !@operator.IsModerator)
                throw new UnauthorizedAccessException();

            var articleTop = await _articleRepo.GetArticleTops()
                .SingleOrDefaultAsync(s => s.ArticleId == article.Id);

            if (articleTop == null)
            {
                articleTop = new ArticleTop
                {
                    ArticleId = article.Id,
                    CreationTime = DateTime.Now,
                    IsForever = isForever,
                    ExpirationTime = expirationTime,
                    IsCancel = false
                };
                await _articleRepo.AddArticleTop(articleTop);
            }
            else if (articleTop.IsCancel)
            {
                articleTop.IsCancel = true;
                articleTop.IsForever = isForever;
                articleTop.ExpirationTime = expirationTime;

                await _articleRepo.UpdateArticleTop(articleTop,
                    nameof(ArticleTop.IsCancel),
                    nameof(ArticleTop.IsForever),
                    nameof(ArticleTop.ExpirationTime));
            }
        }

        public async Task CancelTop(User @operator, Article article)
        {
            if (!@operator.IsAdministrator && !@operator.IsModerator)
                throw new UnauthorizedAccessException();

            var articleTop = await _articleRepo.GetArticleTops()
                .SingleOrDefaultAsync(s => s.ArticleId == article.Id);

            if (articleTop != null)
            {
                articleTop.IsCancel = true;

                await _articleRepo.UpdateArticleTop(articleTop, nameof(ArticleTop.IsCancel));
            }
        }

        public async Task Edit(User @operator, Article article)
        {
            if (article.IsTransient()) return;

            if (!@operator.Id.Equals(article.CreatorId))
                throw new InvalidOperationException();

            if (article.State.Equals(ArticleState.Deleted))
                throw new InvalidOperationException();

            if (article.Type.Equals(ArticleType.Question))
            {
                await _articleRepo.Update(article, nameof(Article.Keywords));
            }
            else if (article.Type.Equals(ArticleType.Article))
            {
                await _articleRepo.Update(article,
                    nameof(Article.Summary),
                    nameof(Article.Keywords));
            }
        }

        public async Task CanComment(User @operator, Article article, bool canComment)
        {
            if (article.IsTransient()) return;

            if (!@operator.Id.Equals(article.CreatorId))
                throw new InvalidOperationException();

            if (article.State.Equals(ArticleState.Deleted))
                throw new InvalidOperationException();

            if (article.Type.Equals(ArticleType.Question))
                throw new InvalidOperationException();

            article.CanComment = canComment;
            await _articleRepo.Update(article, nameof(Article.CanComment));
        }
    }
}
