using EDoc2.FAQ.Web.Data.Identity;
using System;
using EDoc2.FAQ.Web.Data.Common;

namespace EDoc2.FAQ.Web.Data.Discuss
{
    public class ArticleFavorite
    {
        public int Id { get; set; }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public DateTime OperateDate { get; set; }

        /// <summary>
        /// 收藏状态
        /// 避免取消收藏后删除数据导致无法与上次操作时间进行对比，
        /// 可控制用户操作频率
        /// </summary>
        public FavoriteState State { get; set; }

        public virtual Article Article { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
