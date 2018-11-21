using AutoMapper;
using EDoc2.FAQ.Core.Domain.Articles;
using System;
using EDoc2.FAQ.Core.Application.ServiceBase;

namespace EDoc2.FAQ.Core.Application.Articles.Dtos
{
    public class ArticleDtos
    {
        #region Request

        public class Search
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

            /// <summary>
            /// 排序
            /// </summary>
            public string OrderBy { get; set; }

            /// <summary>
            /// 跳过个数
            /// </summary>
            public int Skip { get; set; }

            /// <summary>
            /// 获取个数 
            /// NULL 表示获取全部
            /// </summary>
            public int? Take { get; set; }
        }

        public class SearchByES
        {
            /// <summary>
            /// 关键字高亮
            /// </summary>
            public bool Highlight { get; set; }

            /// <summary>
            /// 关键词
            /// </summary>
            public string Keywords { get; set; }

            /// <summary>
            /// 状态编号
            /// </summary>
            public int StateId { get; set; }

            /// <summary>
            /// 类别编号
            /// </summary>
            public int TypeId { get; set; }

            /// <summary>
            /// 排序
            /// </summary>
            public string OrderBy { get; set; }

            /// <summary>
            /// 跳过个数
            /// </summary>
            public int Skip { get; set; }

            /// <summary>
            /// 获取个数 
            /// NULL 表示获取全部
            /// </summary>
            public int? Take { get; set; }
        }

        public class Add : EntityDto<Guid>
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
        }

        public class Edit : EntityDto<Guid>
        {

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
                dto.PV = article.GetPV();
                dto.RewardScore = article.GetRewardScore();
                dto.AdoptCommentId = article.GetAdoptCommentId();
                return dto;
            }
        }

        #endregion
    }
}
