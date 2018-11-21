using EDoc2.FAQ.Core.Domain.Events;
using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Articles
{
    public class Article : Entity<Guid>, IAggregateRoot
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
        /// 关键字
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ArticleState State { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public ArticleType Type { get; set; }

        /// <summary>
        /// 是否能够评论
        /// </summary>
        public bool CanComment { get; set; }

        /// <summary>
        /// 创建者编号
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 文章属性
        /// </summary>
        public virtual ICollection<ArticleProperty> Properties { get; set; }

        /// <summary>
        /// 文章评论
        /// </summary>
        public virtual ICollection<ArticleComment> Comments { get; set; }

        #region 状态事件

        /// <summary>
        /// 存为草稿
        /// </summary>
        public void SetDraft()
        {
            if(State == null || State.Id == ArticleState.Rejected.Id)
                State = ArticleState.Draft;
        }

        /// <summary>
        /// 准备发布
        /// </summary>
        /// <param name="auditorId">审核人编号</param>
        public void SetAuditing(string auditorId)
        {
            if (State.Id != ArticleState.Draft.Id) return;

            State = ArticleState.Auditing;
            AddDomainEvent(new ArticleStateChangedToAuditingDomainEvent(this, auditorId));
        }

        /// <summary>
        /// 审批驳回
        /// </summary>
        /// <param name="auditorId"></param>
        public void SetRejected(string auditorId)
        {
            if (State.Id != ArticleState.Auditing.Id) return;

            State = ArticleState.Rejected;
            AddDomainEvent(new ArticleStateChangedToRejectedDomainEvent(this, auditorId));
        }

        /// <summary>
        /// 设置为发布
        /// </summary>
        /// <param name="operatorId">
        /// </param>
        public void SetPublished(string operatorId)
        {
            if (State.Id != ArticleState.Auditing.Id) return;

            //如果文章为问题或者交流，则设置为未结帖状态，否则设置为发布状态
            if (Type.Id == ArticleType.Question.Id || Type.Id == ArticleType.Communication.Id)
                State = ArticleState.UnSolved;
            else
                State = ArticleState.Published;

            AddDomainEvent(new ArticleStateChangedToPublishedDomainEvent(this, operatorId));
        }

        /// <summary>
        /// 结贴
        /// </summary>
        public void SetSolved(long adoptCommentId)
        {
            if (State.Id != ArticleState.UnSolved.Id) return;

            State = ArticleState.Solved;
            AddDomainEvent(new ArticleStateChangedToSolvedDomainEvent(this, adoptCommentId));
        }

        /// <summary>
        /// 无满意结贴
        /// </summary>
        public void SetUnsatisfactory()
        {
            if (State.Id != ArticleState.UnSolved.Id) return;

            State = ArticleState.Unsatisfactory;
            AddDomainEvent(new ArticleStateChangedToUnsatisfactoryDomainEvent(this));
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        public void SetDeleted(string operatorId)
        {
            if (State.Id == ArticleState.Auditing.Id) return;

            State = ArticleState.Deleted;
            AddDomainEvent(new ArticleStateChangedToDeletedDomainEvent(this, operatorId));
        }

        #endregion

        #region 属性修改

        private void SetProperty(string name, string value)
        {
            if (Properties == null)
                Properties = new List<ArticleProperty>();

            var prop = Properties.SingleOrDefault(p => p.Name == name);
            if (prop == null)
            {
                prop = new ArticleProperty();
                Properties.Add(prop);
            }

            prop.Name = name;
            prop.Value = value;
        }

        private TValue GetProperty<TValue>(string name, Func<string, TValue> converter = null)
        {
            if (Properties == null)
                Properties = new List<ArticleProperty>();

            var prop = Properties.SingleOrDefault(p => p.Name == name);
            if (prop == null)
                return default(TValue);

            return converter == null ? (TValue)(prop.Value as object) : converter(prop.Value); 
        }

        /// <summary>
        /// 设置悬赏分
        /// </summary>
        public void SetRewardScore(int score)
        {
            if (score < 0) throw new ArgumentOutOfRangeException(nameof(score));

            //只有状态为草稿的时候才能设置悬赏分
            if (State.Id != ArticleState.Draft.Id) return;

            SetProperty(ArticleProperty.RewardScore, score.ToString());
        }

        /// <summary>
        /// 获取悬赏分
        /// </summary>
        /// <returns></returns>
        public int GetRewardScore()
        {
            return GetProperty<int>(ArticleProperty.RewardScore, int.Parse);
        }

        /// <summary>
        /// 是否已经消耗了积分
        /// </summary>
        /// <returns></returns>
        public bool HasSpentScore()
        {
            return GetProperty<bool>(ArticleProperty.HasSpentSocre, bool.Parse);
        }

        /// <summary>
        /// 设置是否消耗过积分
        /// </summary>
        /// <param name="hasSpentScore"></param>
        public void HasSpentScore(bool hasSpentScore)
        {
            SetProperty(ArticleProperty.HasSpentSocre, hasSpentScore.ToString());
        }

        /// <summary>
        /// 获取访问量
        /// </summary>
        /// <returns></returns>
        public int GetPV()
        {
            return GetProperty<int>(ArticleProperty.PvNumber, int.Parse);
        }

        /// <summary>
        /// 设置访问量
        /// </summary>
        /// <param name="pageViews"></param>
        public void SetPV(int pageViews)
        {
            SetProperty(ArticleProperty.PvNumber, pageViews.ToString());
        }

        /// <summary>
        /// 获取最佳回复编号
        /// </summary>
        /// <returns></returns>
        public string GetAdoptCommentId()
        {
            if (State.Id != ArticleState.Solved.Id) return string.Empty;

            return GetProperty<string>(ArticleProperty.AdoptCommentId);
        }

        /// <summary>
        /// 设置最佳回复编号
        /// </summary>
        /// <param name="adoptCommentId"></param>
        public void SetAdoptCommentId(string adoptCommentId)
        {
            if (State.Id != ArticleState.Solved.Id) return;

            SetProperty(ArticleProperty.AdoptCommentId, adoptCommentId);
        }

        /// <summary>
        /// 获取赞数
        /// </summary>
        /// <returns></returns>
        public int GetLikes()
        {
            return GetProperty<int>(ArticleProperty.Likes, int.Parse);
        }

        /// <summary>
        /// 设置赞数
        /// </summary>
        /// <param name="likes"></param>
        public void SetLikes(int likes)
        {
            if (likes < 0)
                throw new ArgumentOutOfRangeException(nameof(likes));

            SetProperty(ArticleProperty.Likes, likes.ToString());
        }

        /// <summary>
        /// 获取踩数
        /// </summary>
        /// <returns></returns>
        public int GetDislikes()
        {
            return GetProperty<int>(ArticleProperty.Dislikes, int.Parse);
        }

        /// <summary>
        /// 设置踩数
        /// </summary>
        /// <param name="dislikes"></param>
        public void SetDislikes(int dislikes)
        {
            if (dislikes < 0)
                throw new ArgumentOutOfRangeException(nameof(dislikes));

            SetProperty(ArticleProperty.Dislikes, dislikes.ToString());
        }

        #endregion
    }
}
