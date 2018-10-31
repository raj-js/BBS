using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using EDoc2.FAQ.Web.Data.Common;
using EDoc2.FAQ.Web.Data.Discuss;
using EDoc2.FAQ.Web.Extensions;

namespace EDoc2.FAQ.Web.Models
{
    public class VmArticleForAdd
    {
        [Required]
        public string PageId { get; set; }

        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        public string CategoryId { get; set; }

        [Required]
        public string TagId { get; set; }

        [Required]
        public string ImageCode { get; set; }

        [Required]
        public string Content { get; set; }

        [Range(0, 200)]
        public int RewardScore { get; set; }

        public int LeftScore { get; set; }
    }

    public class VmArticleForDetail
    {
        public VmArticleForDetail(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            State = article.State;
            Views = article.Views;
            Replies = article.Replies;
            PublisherId = article.PublisherId;
            Publisher = article.Publisher.UserClaims.Get<string>(ClaimTypes.Name);
            PublishDate = article.PublishDate;
            Content = article.UseStorage ? "暂未实现" : article.Content;
            RewardScore = article.RewardScore;
            Category = article.ArticleCategories.FirstOrDefault(c => c.Category.SubCategory == ArticleSubTypes.Category)?.Category?.Display;
            AdoptComment = article.AdoptCommentId;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public ArticleState State { get; set; }
        public int Views { get; set; }
        public int Replies { get; set; }
        public string PublisherId { get; set; }
        public string Publisher { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Content { get; set; }
        public int RewardScore { get; set; }

        /// <summary>
        /// 是否已收藏
        /// </summary>
        public bool IsFavorite { get; set; } = false;

        /// <summary>
        /// 当前用户是否为作者
        /// </summary>
        public bool IsAuthor { get; set; } = false;

        public string Category { get; set; }

        public string AdoptComment { get; set; }
    }

    public class VmArticleCommentForDetail
    {
        public VmArticleCommentForDetail(ArticleComment comment)
        {
            Id = comment.Id;
            FromUserId = comment.FromUserId;
            FromUser = comment.FromUser.UserClaims.Get<string>(ClaimTypes.Name);
            IsReplyToComment = comment.IsReplyToComment;

            if (comment.IsReplyToComment)
            {
                ToCommentId = comment.ReplyToComment.Id;
                ToCommentUser = comment.ReplyToComment.FromUser.UserClaims.Get<string>(ClaimTypes.Name);
            }

            ReplyDate = comment.ReplyDate;
            Content = comment.Content;
            Goods = comment.Goods;
            Bads = comment.Bads;
        }

        public string Id { get; set; }
        public string FromUserId { get; set; }
        public string FromUser { get; set; }
        public bool IsReplyToComment { get; set; }
        public string ToCommentId { get; set; }
        public string ToCommentUser { get; set; }
        public DateTime ReplyDate { get; set; }
        public string Content { get; set; }
        public int Goods { get; set; }
        public int Bads { get; set; }
    }

    public class VmArticleCommentForAdd
    {
        [Required]
        public string ArticleId { get; set; }

        public string ToCommentId { get; set; }

        [Required]
        public string Content { get; set; }
    }

    public class VmArticleForList
    {
        public VmArticleForList(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            PublisherId = article.PublisherId;
            Publisher = article.Publisher.UserClaims.Get<string>(ClaimTypes.Name);
            PublishDate = article.PublishDate;
            Views = article.Views;
            Replies = article.Replies;
            IsTop = article.IsTop;
            IsCream = article.IsCream;
            RewardScore = article.RewardScore;
            IsSolved = (article.State & ArticleState.Solved) > 0 ||  (article.State & ArticleState.Dissatisfied) > 0;
            Category = article.ArticleCategories.FirstOrDefault(c => c.Category.SubCategory == ArticleSubTypes.Category)?.Category?.Display;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string PublisherId { get; set; }
        public string Publisher { get; set; }
        public DateTime? PublishDate { get; set; }
        public int Views { get; set; }
        public int Replies { get; set; }
        public string SpeCol { get; set; }
        public bool IsTop { get; set; }
        public int RewardScore { get; set; }
        public bool IsSolved { get; set; }
        public string Category { get; set; }
        public bool IsCream { get; set; }
    }

    public class VmArticleForBuzz
    {
        public VmArticleForBuzz(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            Replies = article.Replies;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public int Replies { get; set; }
    }

    public class VmTag
    {
        public VmTag(Category category)
        {
            Id = category.Id;
            Display = category.Display;
        }

        public string Id { get; set; }
        public string Display { get; set; }
    }

    public class VmArticleForAccountIndex
    {
        public VmArticleForAccountIndex(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            PublishDate = article.PublishDate;
            Views = article.Views;
            Replies = article.Replies;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime? PublishDate { get; set; }
        public int Views { get; set; }
        public int Replies { get; set; }
    }

    public class VmCloseArticle
    {
        public string ArticleId { get; set; }

        /// <summary>
        /// 是否为满意结贴
        /// </summary>
        public bool IsSatisfied { get; set; }

        public string CommentId { get; set; }
    }
}
