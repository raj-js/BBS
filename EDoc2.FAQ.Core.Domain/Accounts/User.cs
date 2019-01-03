using EDoc2.FAQ.Core.Domain.Accounts.Events;
using EDoc2.FAQ.Core.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using EDoc2.FAQ.Core.Domain.Exceptions;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    public class User : IdentityUser, IEntity, IAggregateRoot
    {
        /// <summary>
        /// 是否被屏蔽
        /// </summary>
        public bool IsMuted { get; internal set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 加入日期
        /// </summary>
        public DateTime JoinDate { get; set; }

        #region 领域事件

        private List<INotification> _domainEvents;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification @event)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(@event);
        }

        public void RemoveDomainEvent(INotification @event) => _domainEvents?.Remove(@event);

        public void ClearDomainEvent() => _domainEvents?.Clear();

        public virtual bool IsTransient()
        {
            return Id.Equals(default(string));
        }

        #endregion

        #region 导航属性

        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserProperty> UserProperties { get; set; }
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }
        public virtual ICollection<UserSubscriber> UserFollows { get; set; }
        public virtual ICollection<UserSubscriber> UserFans { get; set; }
        #endregion

        #region Claims 存取

        private UserClaim GetUserClaim(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException(nameof(type));

            return UserClaims.SingleOrDefault(c => c.ClaimType.Equals(type, StringComparison.OrdinalIgnoreCase));
        }

        private void SetClaimValue(string type, string value)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException(nameof(type));

            var claim = GetUserClaim(type);
            if (claim == null)
            {
                claim = new UserClaim
                {
                    ClaimType = type,
                    ClaimValue = value
                };
                UserClaims.Add(claim);
            }
            else
            {
                claim.ClaimValue = value;
            }
        }

        private T GetClaimValue<T>(string type, Func<string, T> converter = null)
        {
            var claim = GetUserClaim(type);

            return claim == null ? default(T)
                : (converter == null ? (T)(claim.ClaimValue as object) : converter(claim.ClaimValue));
        }

        #endregion

        #region 用户属性存取

        internal UserProperty GetOrSetProperty<T>(string name, T @default = default(T))
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (UserProperties == null)
                UserProperties = new List<UserProperty>();

            var property = UserProperties.SingleOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (property == null)
            {
                property = new UserProperty
                {
                    Name = name,
                    Value = @default?.ToString()
                };
                UserProperties.Add(property);
            }
            else
            {
                if (@default != null && !@default.Equals(default(T)))
                    property.Value = @default.ToString();
            }
            return property;
        }

        private void SetProperty(string name, string value) => GetOrSetProperty(name, value);

        private T GetProperty<T>(string name, T @default = default(T), Func<string, T> converter = null)
        {
            var property = GetOrSetProperty(name, @default);

            return converter == null ? (T)(property.Value as object) : converter(property.Value);
        }

        /// <summary>
        /// 坚持签到天数
        /// </summary>
        public int PersistDays => GetProperty(UserProperty.PersistDays, 0, int.Parse);

        /// <summary>
        /// 积分
        /// </summary>
        public int Score => GetProperty(UserProperty.Score, 0, int.Parse);

        /// <summary>
        /// 粉丝数
        /// </summary>
        public int Fans => GetProperty(UserProperty.Fans, 0, int.Parse);

        /// <summary>
        /// 关注数
        /// </summary>
        public int Follows => GetProperty(UserProperty.Follows, 0, int.Parse);

        /// <summary>
        /// 设置连续签到天数
        /// </summary>
        /// <param name="days"></param>
        private void SetPersistDays(int days)
        {
            if (days < 0)
                throw new ArgumentOutOfRangeException(nameof(days));

            SetProperty(UserProperty.PersistDays, days.ToString());
        }

        /// <summary>
        /// 增长持续签到天数
        /// </summary>
        public void IncrementPersistDays()
        {
            SetPersistDays(PersistDays + 1);
        }

        /// <summary>
        /// 签到天数清零
        /// </summary>
        public void ClearPersistDays()
        {
            SetPersistDays(0);
        }

        /// <summary>
        /// 设置积分
        /// </summary>
        /// <param name="score"></param>
        private void SetScore(int score)
        {
            if (score < 0)
                throw new ArgumentOutOfRangeException(nameof(score));

            SetProperty(UserProperty.Score, score.ToString());
        }

        /// <summary>
        /// 判断是否为某人的粉丝
        /// </summary>
        /// <param name="followId"></param>
        /// <param name="subscriber"></param>
        /// <returns></returns>
        internal bool IsFanOf(string followId, out UserSubscriber subscriber)
        {
            subscriber = null;

            if (UserFollows == null) return false;

            subscriber = UserFollows.SingleOrDefault(s =>
                s.FollowId.Equals(followId, StringComparison.OrdinalIgnoreCase));

            return !subscriber?.IsCancel ?? false;
        }

        /// <summary>
        /// 设置关注数
        /// </summary>
        /// <param name="follows"></param>
        private void SetFollows(int follows)
        {
            if (follows < 0)
                throw new ArgumentOutOfRangeException(nameof(follows));

            SetProperty(UserProperty.Follows, follows.ToString());
        }

        /// <summary>
        /// 设置粉丝数
        /// </summary>
        /// <param name="fans"></param>
        private void SetFans(int fans)
        {
            if (fans < 0)
                throw new ArgumentOutOfRangeException(nameof(fans));

            SetProperty(UserProperty.Fans, fans.ToString());
        }

        /// <summary>
        /// 是否收藏文章
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="favorite"></param>
        /// <returns></returns>
        public bool HasFavorite(Guid articleId, out UserFavorite favorite)
        {
            favorite = null;

            if (UserFavorites == null) return false;

            favorite = UserFavorites.SingleOrDefault(s => s.ArticleId == articleId);

            return !favorite?.IsCancel ?? false;
        }

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="articleId"></param>
        public void AddFavorite(Guid articleId)
        {
            if (HasFavorite(articleId, out var favorite)) return;

            if (favorite == null)
            {
                favorite = new UserFavorite(Id, articleId);
                UserFavorites.Add(favorite);
            }
            else
            {
                favorite.IsCancel = false;
                favorite.OperationTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 取下收藏
        /// </summary>
        /// <param name="articleId"></param>
        public void RemoveFavorite(Guid articleId)
        {
            if (!HasFavorite(articleId, out var favorite)) return;

            favorite.IsCancel = true;
            favorite.OperationTime = DateTime.Now;
        }

        public void SetAvatar(string base64Avatar)
        {
            GetOrSetProperty(UserProperty.Avatar, base64Avatar);
        }

        public string Avatar => GetProperty(UserProperty.Avatar, default(string));

        #endregion

        /// <summary>
        /// 初始化基本属性
        /// </summary>
        public void Initialize()
        {
            IsMuted = false;
            Gender = Gender.Male;
            City = string.Empty;
            Signature = string.Empty;
            JoinDate = DateTime.Now;

            SetPersistDays(0);
            SetScore(0);
            SetFollows(0);
            SetFans(0);
        }

        /// <summary>
        /// 判断是否为指定角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool IsRole(Role role)
        {
            if (role == null) return false;

            return UserRoles?.Any(s => s.RoleId.Equals(role.Id, StringComparison.OrdinalIgnoreCase)) ?? false;
        }

        /// <summary>
        /// 是否为管理员
        /// </summary>
        public bool IsAdministrator => IsRole(Role.Administrator);

        /// <summary>
        /// 是否为版主
        /// </summary>
        public bool IsModerator => IsRole(Role.Moderator);

        /// <summary>
        /// 是否为会员
        /// </summary>
        public bool IsMember => IsRole(Role.Moderator);

        /// <summary>
        /// 是否是指定角色
        /// </summary>
        /// <param name="role"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public bool InRole(Role role, out UserRole userRole)
        {
            userRole = null;

            if (role == null) return false;

            if (UserRoles == null)
                UserRoles = new List<UserRole>();

            userRole = UserRoles.SingleOrDefault(s => s.RoleId.Equals(role.Id, StringComparison.OrdinalIgnoreCase));
            return userRole != null;
        }

        public bool InRoles(params Role[] roles)
        {
            if (roles == null) return false;

            var roleIds = roles.Select(r => r.Id);

            return UserRoles?.Any(s => roleIds.Contains(s.RoleId)) ?? false;
        }

        public bool HasEnoughScore(int score)
        {
            return Score >= score;
        }
    }

    public enum Gender
    {
        Male = 0,
        Female = 1,
        Secret = -1
    }
}
