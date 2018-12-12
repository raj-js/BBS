using AutoMapper;
using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Domain.Articles;
using System;
using System.ComponentModel.DataAnnotations;
using EDoc2.FAQ.Core.Infrastructure.Extensions;

namespace EDoc2.FAQ.Core.Application.Articles.Dtos
{
    public class ArticleDtos
    {
        #region Request

        /// <summary>
        /// 搜索请求
        /// </summary>
        public class SearchReq : IPagingRequest
        {
            /// <summary>
            /// 标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 关键词
            /// </summary>
            public string Keywords { get; set; }

            /// <summary>
            /// 状态编号
            /// </summary>
            public ArticleState? State { get; set; }

            /// <summary>
            /// 类别编号
            /// </summary>
            public ArticleType? Type { get; set; }

            public string OrderBy { get; set; } = "CreationTime";

            public bool IsAscending { get; set; }

            public int PageIndex { get; set; }

            public int PageSize { get; set; }
        }

        public class LoadCommentsReq : IPagingRequest
        {
            public Guid ArticleId { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public string OrderBy { get; set; } = "CreationTime";
            public bool IsAscending { get; set; } = true;
        }

        /// <summary>
        /// 添加问题请求
        /// </summary>
        public class AddQuestionReq
        {
            /// <summary>
            /// 标题
            /// </summary>
            [Required]
            [MaxLength(50)]
            public string Title { get; set; }

            /// <summary>
            /// 关键词
            /// </summary>
            [Required]
            [MaxLength(50)]
            public string Keywords { get; set; }

            /// <summary>
            /// 内容
            /// </summary>
            [Required]
            [MinLength(15)]
            public string Content { get; set; }

            /// <summary>
            /// 类别
            /// </summary>
            [Required]
            public Guid CategoryId { get; set; }

            /// <summary>
            /// 悬赏分
            /// </summary>
            [Required]
            [Range(0, 500)]
            public int Score { get; set; }

            public Article To()
            {
                return new Article
                {
                    Title = Title,
                    Keywords = Keywords,
                    Content = Content,
                    Type = ArticleType.Question
                };
            }
        }

        /// <summary>
        /// 新增文章请求
        /// </summary>
        public class AddArticleReq
        {
            /// <summary>
            /// 标题
            /// </summary>
            [Required]
            [MaxLength(50)]
            public string Title { get; set; }

            [Required]
            [MaxLength(128)]
            public string Summary { get; set; }

            /// <summary>
            /// 关键词
            /// </summary>
            [Required]
            [MaxLength(50)]
            public string Keywords { get; set; }

            /// <summary>
            /// 内容
            /// </summary>
            [Required]
            [MinLength(50)]
            public string Content { get; set; }

            /// <summary>
            /// 类别
            /// </summary>
            [Required]
            public Guid CategoryId { get; set; }

            /// <summary>
            /// 是否可以评论
            /// </summary>
            [Required]
            public bool CanComment { get; set; }

            public Article To()
            {
                return new Article
                {
                    Title = Title,
                    Summary = Summary,
                    Keywords = Keywords,
                    Content = Content,
                    Type = ArticleType.Article,
                    CanComment = CanComment
                };
            }
        }

        /// <summary>
        /// 修改问题请求
        /// </summary>
        public class EditQuestionReq : EntityDto<Guid>
        {
            /// <summary>
            /// 关键词
            /// </summary>
            [Required]
            [MaxLength(50)]
            public string Keywords { get; set; }

            /// <summary>
            /// 内容
            /// </summary>
            [Required]
            [MinLength(15)]
            public string Content { get; set; }

            public Article To()
            {
                return new Article
                {
                    Id = Id,
                    Keywords = Keywords,
                    Content = Content,
                };
            }
        }

        /// <summary>
        /// 更新文章请求
        /// </summary>
        public class EditArticleReq : EntityDto<Guid>
        {
            /// <summary>
            /// 摘要
            /// </summary>
            [Required]
            [MaxLength(128)]
            public string Summary { get; set; }

            /// <summary>
            /// 关键词
            /// </summary>
            [Required]
            [MaxLength(50)]
            public string Keywords { get; set; }

            /// <summary>
            /// 内容
            /// </summary>
            [Required]
            [MinLength(50)]
            public string Content { get; set; }

            /// <summary>
            /// 是否可以评论
            /// </summary>
            [Required]
            public bool CanComment { get; set; }

            public Article To()
            {
                return new Article
                {
                    Id = Id,
                    Summary = Summary,
                    Keywords = Keywords,
                    Content = Content,
                    CanComment = CanComment
                };
            }
        }

        /// <summary>
        /// 只修改文章是否可以评论
        /// </summary>
        public class EditCanCommentReq : EntityDto<Guid>
        {
            /// <summary>
            /// 是否可以评论
            /// </summary>
            [Required]
            public bool CanComment { get; set; }

            public Article To()
            {
                return new Article
                {
                    Id = Id,
                    CanComment = CanComment
                };
            }
        }

        /// <summary>
        /// 置顶文章请求
        /// </summary>
        public class TopArticleReq : EntityDto<Guid>
        {
            public bool IsForever { get; set; }

            public DateTime? ExpirationTime { get; set; }
        }

        /// <summary>
        /// 举报文章请求
        /// </summary>
        public class ReportArticleReq : EntityDto<Guid>
        {
            [Required]
            [StringLength(128, MinimumLength = 5)]
            public string Reason { get; set; }
        }

        /// <summary>
        /// 举报文章请求
        /// </summary>
        public class ReportCommentReq : EntityDto<long>
        {
            [Required]
            [StringLength(128, MinimumLength = 5)]
            public string Reason { get; set; }
        }

        /// <summary>
        /// 回复文章
        /// </summary>
        public class ReplyArticleReq
        {
            /// <summary>
            /// 文章编号
            /// </summary>
            [Required]
            public Guid ArticleId { get; set; }

            /// <summary>
            /// 回复内容
            /// </summary>
            [Required]
            [MaxLength(256)]
            public string Content { get; set; }
        }

        /// <summary>
        /// 回复评论
        /// </summary>
        public class ReplyCommentReq
        {
            /// <summary>
            /// 文章编号
            /// </summary>
            [Required]
            public Guid ArticleId { get; set; }

            /// <summary>
            /// 被回复评论编号
            /// </summary>
            [Required]
            public long CommentId { get; set; }

            /// <summary>
            /// 回复内容
            /// </summary>
            [Required]
            [MaxLength(256)]
            public string Content { get; set; }
        }

        /// <summary>
        /// 结帖请求
        /// </summary>
        public class FinishReq: EntityDto<Guid>
        {
            /// <summary>
            /// 是否为满意结帖
            /// </summary>
            [Required]
            public bool Unsatisfactory { get; set; }

            /// <summary>
            /// 最佳回复编号
            /// </summary>
            public long? AdoptId { get; set; }
        }

        #endregion

        #region Response

        public class ValueTitlePair<TKey>
        {
            public TKey Value { get; set; }

            public string Title { get; set; }
        }

        public class ListItem : EntityDto<Guid>
        {
            /// <summary>
            /// 标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 关键词
            /// </summary>
            public string Keywords { get; set; }

            /// <summary>
            /// 状态
            /// </summary>
            public string State { get; set; }

            /// <summary>
            /// 类别
            /// </summary>
            public string Type { get; set; }

            /// <summary>
            /// 赞
            /// </summary>
            public int Likes { get; set; }

            /// <summary>
            /// 踩
            /// </summary>
            public int Dislikes { get; set; }

            /// <summary>
            /// 访问量
            /// </summary>
            public int Pv { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreationTime { get; set; }

            public static ListItem From(Article article)
            {
                return new ListItem
                {
                    Id = article.Id,
                    Title = article.Title,
                    Keywords = article.Keywords,
                    State = article.State.Name(),
                    Type = article.Type.Name(),
                    Likes = article.Likes,
                    Dislikes = article.Dislikes,
                    Pv = article.Pv,
                    CreationTime = article.CreationTime
                };
            }
        }

        public class ArticleResp : EntityDto<Guid>
        {
            /// <summary>
            /// 标题
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// 摘要
            /// </summary>
            public string Summary { get; set; }

            /// <summary>
            /// 内容
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// 关键词
            /// </summary>
            public string Keywords { get; set; }

            /// <summary>
            /// 类型
            /// </summary>
            public ArticleType Type { get; set; }

            /// <summary>
            /// 状态
            /// </summary>
            public ArticleState State { get; set; }

            public string StateDisplay { get; set; }

            /// <summary>
            /// 能否回复
            /// </summary>
            public bool CanComment { get; set; }

            /// <summary>
            /// 赞
            /// </summary>
            public int Likes { get; set; }

            /// <summary>
            /// 踩
            /// </summary>
            public int Dislikes { get; set; }

            /// <summary>
            /// 访问量
            /// </summary>
            public int Pv { get; set; }

            /// <summary>
            /// 创建人编号
            /// </summary>
            public string CreatorId { get; set; }

            /// <summary>
            /// 创建人昵称
            /// </summary>
            public string CreatorNick { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreationTime { get; set; }

            /// <summary>
            /// 悬赏分
            /// </summary>
            public int Score { get; set; }

            /// <summary>
            /// 最佳回复编号
            /// </summary>
            public string AdoptId { get; set; }

            public static ArticleResp From(Article article)
            {
                return new ArticleResp
                {
                    //公共部分
                    Id = article.Id,
                    Title = article.Title,
                    Keywords = article.Keywords,
                    Content = article.Content,
                    CanComment = article.CanComment,
                    Likes = article.Likes,
                    Dislikes = article.Dislikes,
                    Pv = article.Pv,
                    CreatorId = article.CreatorId,
                    CreationTime = article.CreationTime,
                    CreatorNick = article.Creator.Nickname,
                    Type = article.Type,
                    State = article.State,
                    StateDisplay = article.State.Name(),

                    //评论
                    Score = article.Score,
                    AdoptId = article.AdoptCommentId,

                    //文章
                    Summary = article.Summary
                };
            }
        }

        public class LikeOrNotResp
        {
            public int Likes { get; set; }

            public int Dislikes { get; set; }

            public static LikeOrNotResp Initlize(int likes, int dislikes)
            {
                return new LikeOrNotResp
                {
                    Likes = likes,
                    Dislikes = dislikes
                };
            }
        }

        public class CommentItem : EntityDto<long>
        {
            public long? ParentId { get; set; }

            public string Content { get; set; }

            public string CreatorNick { get; set; }

            public DateTime CreationTime { get; set; }

            public static CommentItem From(ArticleComment comment)
            {
                return new CommentItem
                {
                    Id = comment.Id,
                    ParentId = comment.ParentCommentId,
                    Content = comment.Content,
                    CreatorNick = comment.Creator.Nickname,
                    CreationTime = comment.CreationTime
                };
            }
        }

        #endregion
    }
}
