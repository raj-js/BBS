using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using EDoc2.FAQ.Web.Data.Common;
using EDoc2.FAQ.Web.Data.Discuss;
using EDoc2.FAQ.Web.Data.Identity;
using EDoc2.FAQ.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Crypto.Tls;

namespace EDoc2.FAQ.Web.Models
{
    public class LoginForm
    {
        [Required(ErrorMessage = "必填")]
        [EmailAddress(ErrorMessage = "无效的邮箱")]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Required(ErrorMessage = "必填")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住账号?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterForm
    {
        [Required(ErrorMessage = "必填")]
        [EmailAddress(ErrorMessage = "无效的邮箱")]
        [Remote(action: "CheckEmail", controller: "Account")]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Required(ErrorMessage = "必填")]
        [Display(Name = "图形码")]
        public string ImageCode { get; set; }

        [Required(ErrorMessage = "必填")]
        [Display(Name = "验证码")]
        public string VerifyCode { get; set; }

        [Required(ErrorMessage = "必填")]
        [StringLength(16, ErrorMessage = "{0} 长度必须在 {2} 到 {1} 个字符之间", MinimumLength = 1)]
        [Display(Name = "昵称")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "必填")]
        [StringLength(16, ErrorMessage = "{0} 长度必须在 {2} 到 {1} 个字符之间", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次密码不一致")]
        public string ConfirmPassword { get; set; }
    }

    public class VmAccount
    {
        public string Id { get; set; }
        public string NickName { get; set; }
        public int Score { get; set; }
        public DateTime? JoinDate { get; set; }
        public string ComeFrom { get; set; }
        public string Signature { get; set; }
        public List<VmQuestionForHome> RecentQuestions { get; set; } = new List<VmQuestionForHome>();
        public List<VmAnswerForHome> RecentAnswers { get; set; } = new List<VmAnswerForHome>();
    }

    public class VmQuestionForHome
    {
        public VmQuestionForHome(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            CreateDate = article.PublishDate.GetValueOrDefault();
            Views = article.Views;
            Replies = article.Replies;
            IsTop = article.IsTop;
            IsCream = article.IsCream;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public int Views { get; set; }
        public int Replies { get; set; }
        public bool IsTop { get; set; }
        public bool IsCream { get; set; }
    }

    public class VmAnswerForHome
    {
        public VmAnswerForHome(ArticleComment comment)
        {
            Id = comment.Id;
            QId = comment.ArticleId;
            QTitle = comment.Article.Title;
            AnswerContent = comment.Content;
            AnswerDate = comment.ReplyDate;
        }

        public string Id { get; set; }
        public string QId { get; set; }
        public string QTitle { get; set; }
        public string AnswerContent { get; set; }
        public DateTime AnswerDate { get; set; }
    }

    public class VmAccountForBasic
    {
        public string Id { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "{0} 长度必须在 {2} 到 {1} 个字符之间", MinimumLength = 1)]
        public string NickName { get; set; }

        [Required]
        public int Gender { get; set; }

        public string ComeFrom { get; set; }

        public string Signature { get; set; }
    }

    public class VmModifyPassword
    {
        [Required]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "必填")]
        [StringLength(16, ErrorMessage = "{0} 长度必须在 {2} 到 {1} 个字符之间", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "两次密码不一致")]
        public string ConfirmPassword { get; set; }
    }

    public class VmNoticeForAccount
    {
        public VmNoticeForAccount(NoticeReceive entity)
        {
            var notice = entity.Notice;
            NoticeId = notice.Id;
            Who = notice.Who;

            if (Who == NoticeWho.User)
            {
                Sender = notice.Sender.UserClaims.Get<string>(ClaimTypes.Name);
                SenderId = notice.Sender.Id;
            }

            InWhere = notice.Where;
            AtWhen = notice.When;
            DoWhat = notice.What;
            Description = notice.Description;
            ReadState = entity.State;
        }

        public string NoticeId { get; set; }
        public NoticeWho Who { get; set; }
        public string Sender { get; set; }
        public string SenderId { get; set; }
        public NoticeWhere InWhere { get; set; }
        public DateTime AtWhen { get; set; }
        public NoticeWhat DoWhat { get; set; }
        public string Description { get; set; }
        public NoticeReadState ReadState { get; set; }
    }

    public class VmActiveTop
    {
        public List<VmDailySignIn> Newest { get; set; } = new List<VmDailySignIn>();
        public List<VmDailySignIn> Fastest { get; set; } = new List<VmDailySignIn>();
        public List<VmDailySignIn> Longest { get; set; } = new List<VmDailySignIn>();
    }

    public class VmDailySignIn
    {
        public VmDailySignIn(DailySignIn entity)
        {
            UserId = entity.UserId;
            User = entity.User.UserClaims.Get<string>(ClaimTypes.Name);
            SignTime = entity.SignInTime;
        }

        public VmDailySignIn(AppUserClaim keepSignDaysClaim)
        {
            UserId = keepSignDaysClaim.UserId;
            User = keepSignDaysClaim.User.UserClaims.Get<string>(ClaimTypes.Name);

            int.TryParse(keepSignDaysClaim.ClaimValue, out var days);
            KeepSignDays = days;
        }

        public string UserId { get; set; }
        public string User { get; set; }
        public DateTime SignTime { get; set; }
        public int KeepSignDays { get; set; }
    }
}
