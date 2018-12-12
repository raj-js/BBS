﻿using EDoc2.FAQ.Core.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Articles.Events;

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
        public virtual ArticleState State { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public virtual ArticleType Type { get; set; }

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
        /// 结帖时间
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 文章属性
        /// </summary>
        public virtual ICollection<ArticleProperty> Properties { get; set; }

        /// <summary>
        /// 文章评论
        /// </summary>
        public virtual ICollection<ArticleComment> Comments { get; set; }

        /// <summary>
        /// 文章置顶
        /// </summary>
        public virtual ArticleTop ArticleTop { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public virtual User Creator { get; set; }

        #region 状态事件

        /// <summary>
        /// 存为草稿
        /// </summary>
        public void SetDraft()
        {
            if(State == null || State.Equals(ArticleState.Rejected))
                State = ArticleState.Draft;
        }

        /// <summary>
        /// 进入审核
        /// </summary>
        /// <param name="auditorId">审核人编号</param>
        public void SetAuditing()
        {
            if (State != ArticleState.Draft) return;

            State = ArticleState.Auditing;
            AddDomainEvent(new ArticleStateChangedToAuditingDomainEvent(this));
        }

        /// <summary>
        /// 审批驳回
        /// </summary>
        /// <param name="auditorId"></param>
        public void SetRejected(string auditorId)
        {
            if (State != ArticleState.Auditing) return;

            State = ArticleState.Rejected;
            AddDomainEvent(new ArticleStateChangedToRejectedDomainEvent(this, auditorId));
        }

        /// <summary>
        /// 设置为发布
        /// </summary>
        public void SetPublished()
        {
            if (State != ArticleState.Auditing) return;

            //如果文章为问题或者交流，则设置为未结帖状态，否则设置为发布状态
            if (Type == ArticleType.Question || Type.Equals(ArticleType.Article))
                State = ArticleState.UnSolved;
            else
                State = ArticleState.Published;

            AddDomainEvent(new ArticleStateChangedToPublishedDomainEvent(this));
        }

        /// <summary>
        /// 结贴
        /// </summary>
        public void SetSolved(long adoptCommentId)
        {
            if (State != ArticleState.UnSolved) return;

            FinishTime = DateTime.Now;
            State = ArticleState.Solved;
            AddDomainEvent(new ArticleStateChangedToSolvedDomainEvent(this, adoptCommentId));
        }

        /// <summary>
        /// 无满意结贴
        /// </summary>
        public void SetUnsatisfactory()
        {
            if (State != ArticleState.UnSolved) return;

            FinishTime = DateTime.Now;
            State = ArticleState.Unsatisfactory;
            AddDomainEvent(new ArticleStateChangedToUnsatisfactoryDomainEvent(this));
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        public void SetDeleted(string operatorId)
        {
            if (State.Equals(ArticleState.Draft) ||
                State.Equals(ArticleState.Rejected))
            {
                State = ArticleState.Deleted;
                AddDomainEvent(new ArticleStateChangedToDeletedDomainEvent(this, operatorId));
            }
        }

        #endregion

        #region 属性修改

        internal ArticleProperty GetOrSetProperty<T>(string name, T @default = default(T))
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (Properties == null)
                Properties = new List<ArticleProperty>();

            var property = Properties.SingleOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (property == null)
            {
                property = new ArticleProperty
                {
                    Name = name,
                    Value = @default.ToString()
                };
                Properties.Add(property);
            }
            else
            {
                if (@default != null && !@default.Equals(default(T)))
                    property.Value = @default.ToString();
            }
            return property;
        }

        private T GetProperty<T>(string name, T @default = default(T), Func<string, T> converter = null)
        {
            var property = GetOrSetProperty(name, @default);

            return converter == null ? (T)(property.Value as object) : converter(property.Value);
        }

        /// <summary>
        /// 获取悬赏分
        /// </summary>
        /// <returns></returns>
        public int Score => GetProperty(ArticleProperty.Score, 0, int.Parse);

        /// <summary>
        /// 是否已经消耗了积分
        /// </summary>
        /// <returns></returns>
        public bool HasSpentScore => GetProperty(ArticleProperty.HasSpentSocre, false, bool.Parse);

        /// <summary>
        /// 获取访问量
        /// </summary>
        /// <returns></returns>
        public int Pv=> GetProperty(ArticleProperty.Pv, 0, int.Parse);

        /// <summary>
        /// 获取最佳回复编号
        /// </summary>
        /// <returns></returns>
        public string AdoptCommentId => GetProperty(ArticleProperty.AdoptCommentId, string.Empty);

        /// <summary>
        /// 获取赞数
        /// </summary>
        /// <returns></returns>
        public int Likes => GetProperty(ArticleProperty.Likes, 0, int.Parse);

        /// <summary>
        /// 获取踩数
        /// </summary>
        /// <returns></returns>
        public int Dislikes => GetProperty(ArticleProperty.Dislikes, 0, int.Parse);

        #endregion
    }
}
