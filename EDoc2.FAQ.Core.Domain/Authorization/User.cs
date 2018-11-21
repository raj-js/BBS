using EDoc2.FAQ.Core.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDoc2.FAQ.Core.Domain.Authorization
{
    public class User : IdentityUser, IEntity, IAggregateRoot
    {
        public string Nickname { get; set; }

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
            return Id.Equals(default(int));
        }

        #endregion

        #region 导航属性

        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        #endregion

        #region Claims 存取

        private UserClaim GetUserClaim(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException(nameof(type));

            return UserClaims.SingleOrDefault(c => c.ClaimType.Equals(type));
        }

        private void SetClaim(string type, string value)
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

        /// <summary>
        /// 获取昵称
        /// </summary>
        /// <returns></returns>
        public string GetNickname()
        {
            return GetClaimValue<string>(UserClaim.Nickname);
        }

        /// <summary>
        /// 设置昵称
        /// </summary>
        /// <param name="nickname"></param>
        public void SetNickname(string nickname)
        {
            SetClaim(UserClaim.Nickname, nickname);
        }


        /// <summary>
        /// 获取加入日期
        /// </summary>
        /// <returns></returns>
        public DateTime GetJoinDate()
        {
            return GetClaimValue(UserClaim.JoinDate, DateTime.Parse);
        }

        /// <summary>
        /// 设置加入日期
        /// </summary>
        /// <param name="joinDate"></param>
        public void SetJoinDate(DateTime joinDate)
        {
            SetClaim(UserClaim.JoinDate, $"{joinDate:yyyy-MM-dd}");
        }

        /// <summary>
        /// 获取性别
        /// </summary>
        /// <returns></returns>
        public int GetGender()
        {
            return GetClaimValue(UserClaim.Gender, int.Parse);
        }

        /// <summary>
        /// 设置性别
        /// </summary>
        /// <param name="gender"></param>
        public void SetGender(int gender)
        {
            SetClaim(UserClaim.Gender, gender.ToString());
        }

        /// <summary>
        /// 获取城市
        /// </summary>
        /// <returns></returns>
        public string GetCity()
        {
            return GetClaimValue<string>(UserClaim.City);
        }

        /// <summary>
        /// 设置城市
        /// </summary>
        /// <param name="city"></param>
        public void SetCity(string city)
        {
            SetClaim(UserClaim.City, city);
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <returns></returns>
        public string GetSignature()
        {
            return GetClaimValue<string>(UserClaim.Signature);
        }

        /// <summary>
        /// 设置签名
        /// </summary>
        /// <param name="signature"></param>
        public void SetSignature(string signature)
        {
            SetClaim(UserClaim.Signature, signature);
        }

        /// <summary>
        /// 获取连续签到天数
        /// </summary>
        /// <returns></returns>
        public int GetPersistSignInDays()
        {
            return GetClaimValue(UserClaim.PersistSignInDays, int.Parse);
        }

        /// <summary>
        /// 设置连续签到天数
        /// </summary>
        /// <param name="days"></param>
        public void SetPersistSignInDays(int days)
        {
            if (days < 0)
                throw new ArgumentOutOfRangeException(nameof(days));

            SetClaim(UserClaim.PersistSignInDays, days.ToString());
        }

        /// <summary>
        /// 获取积分
        /// </summary>
        /// <returns></returns>
        public int GetScore()
        {
            return GetClaimValue(UserClaim.Score, int.Parse);
        }

        /// <summary>
        /// 设置积分
        /// </summary>
        /// <param name="score"></param>
        public void SetScore(int score)
        {
            if (score < 0)
                throw new ArgumentOutOfRangeException(nameof(score));

            SetClaim(UserClaim.Score, score.ToString());
        }

        #endregion
    }
}
