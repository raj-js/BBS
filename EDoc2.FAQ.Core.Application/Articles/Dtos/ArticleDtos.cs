using AutoMapper;
using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Domain.Articles;
using System;
using System.ComponentModel.DataAnnotations;

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
            public int? StateId { get; set; }

            /// <summary>
            /// 类别编号
            /// </summary>
            public int? TypeId { get; set; }

            public string OrderBy { get; set; } = "Id";

            public bool IsAscending { get; set; }

            public int Skip { get; set; }

            public int Take { get; set; }
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
            /// 悬赏分
            /// </summary>
            [Required]
            [Range(0, 500)]
            public int RewardScore { get; set; }
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
            /// 是否可以评论
            /// </summary>
            [Required]
            public bool CanComment { get; set; }
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

        #endregion

        #region Response

        public class ListItem : EntityDto<Guid>
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
            /// 关键词
            /// </summary>
            public string Keywords { get; set; }

            /// <summary>
            /// 状态编号
            /// </summary>
            public int StateId { get; set; }

            /// <summary>
            /// 状态描述
            /// </summary>
            public string StateName { get; set; }

            /// <summary>
            /// 类别编号
            /// </summary>
            public int TypeId { get; set; }

            /// <summary>
            /// 类别描述
            /// </summary>
            public string TypeName { get; set; }

            /// <summary>
            /// 是否能评论
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
            public int PV { get; set; }

            /// <summary>
            /// 悬赏分
            /// </summary>
            public int RewardScore { get; set; }

            /// <summary>
            /// 创建人编号
            /// </summary>
            public string CreatorId { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreationTime { get; set; }
        }

        public class Details : EntityDto<Guid>
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
            /// 状态编号
            /// </summary>
            public int StateId { get; set; }

            /// <summary>
            /// 状态描述
            /// </summary>
            public string StateName { get; set; }

            /// <summary>
            /// 类别编号
            /// </summary>
            public int TypeId { get; set; }

            /// <summary>
            /// 类别描述
            /// </summary>
            public string TypeName { get; set; }

            /// <summary>
            /// 是否能评论
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
            public int PV { get; set; }

            /// <summary>
            /// 悬赏分
            /// </summary>
            public int RewardScore { get; set; }

            /// <summary>
            /// 最佳回复编号
            /// </summary>
            public string AdoptCommentId { get; set; }

            /// <summary>
            /// 创建人编号
            /// </summary>
            public string CreatorId { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreationTime { get; set; }

            public static Details FromEntity(Article article)
            {
                var dto = Mapper.Map<Article, Details>(article);
                dto.StateId = article.State.Id;
                dto.StateName = article.State.Name;
                dto.TypeId = article.Type.Id;
                dto.TypeName = article.Type.Name;
                dto.Likes = article.GetLikes();
                dto.Dislikes = article.GetDislikes();
                dto.PV = article.GetPv();
                dto.RewardScore = article.GetRewardScore();
                dto.AdoptCommentId = article.GetAdoptCommentId();
                return dto;
            }
        }

        #endregion
    }
}
