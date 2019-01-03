using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.Mails;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Accounts.Services;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using EDoc2.FAQ.Core.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EDoc2.FAQ.Core.Domain.Articles.Services;
using static EDoc2.FAQ.Core.Application.Accounts.Dtos.AccountDtos;

namespace EDoc2.FAQ.Core.Application.Accounts
{
    public class AccountAppService : AppServiceBase, IAccountAppService
    {
        private readonly IAccountService _accountService;
        private readonly JwtSetting _jwtSetting;
        private readonly AuthorizeSetting _authorizeSetting;
        private readonly IMailService _mailService;
        private readonly IArticleService _articleService;

        public AccountAppService(IAccountService accountService,
            IOptions<JwtSetting> jwtOptions,
            IOptions<AuthorizeSetting> authOptions, 
            IMailService mailService, IArticleService articleService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _mailService = mailService;
            _articleService = articleService;
            _jwtSetting = jwtOptions.Value;
            _authorizeSetting = authOptions.Value;
        }

        public async Task<bool> IsEmailRegistered(string email)
        {
            return await _accountService.GetUsers().AnyAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<RespWapper> Register(RegisterReq req)
        {
            var user = new User
            {
                UserName = req.Email,
                Email = req.Email,
                Nickname = req.Nickname,
            };
            var result = await _accountService.Create(user, req.Password);
            await UnitOfWork.SaveChangesWithDispatchDomainEvents();

            if (result.Succeeded)
            {
                var token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                var activateUrl = $"{_authorizeSetting.ActivateUrl}/{user.Id}/{HttpUtility.UrlEncode(token)}";
                _mailService.SendConfirmEmail(user.Email, activateUrl);

                return RespWapper.Successed();
            }
            return RespWapper.Failed(result.Errors.ToRespErrors());
        }

        public async Task<RespWapper> EmailConfirm(EmailConfirmReq req)
        {
            var user = await UserManager.FindByIdAsync(req.UserId);
            if (user == null)
                return RespWapper.Failed(Errors.AccountNotFound);

            if (user.IsMuted)
                return RespWapper.Failed(Errors.AccountMuted);

            var result = await UserManager.ConfirmEmailAsync(user, req.Code);
            return result.ToResponse();
        }

        public async Task<RespWapper> RetrievePassword(RetrievePasswordReq req)
        {
            var user = await UserManager.FindByEmailAsync(req.Email);
            if (user == null)
                return RespWapper.Failed(Errors.EmailNotFound);

            if (user.IsMuted)
                return RespWapper.Failed(Errors.AccountMuted);

            var token = await UserManager.GeneratePasswordResetTokenAsync(user);
            var resetUrl = $"{_authorizeSetting.ResetUrl}/{user.Id}/{HttpUtility.UrlEncode(token)}";
            _mailService.SendResetPassword(user.Email, resetUrl);

            return RespWapper.Successed();
        }

        public async Task<RespWapper> ResetPassword(ResetPasswordReq req)
        {
            var user = await UserManager.FindByIdAsync(req.UserId);
            if (user == null)
                return RespWapper.Failed(Errors.AccountNotFound);

            if (user.IsMuted)
                return RespWapper.Failed(Errors.AccountMuted);

            var result = await UserManager.ResetPasswordAsync(user, req.Code, req.Password);
            return result.ToResponse();
        }

        public async Task<RespWapper> Authorize(LoginReq req)
        {
            var user = await UserManager.FindByEmailAsync(req.Email);
            if (user.IsNull())
                return RespWapper.Failed(Errors.EmailNotFound);

            if (user.IsMuted)
                return RespWapper.Failed(Errors.AccountMuted);

            var result = await SignInManager.PasswordSignInAsync(req.Email, req.Password, req.RememberMe, true);

            if (result.Succeeded)
            {
                var claims = new []
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, string.Join(',',user.UserRoles.Select(s=>s.Role.NormalizedName).ToArray())),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(nameof(User.Nickname), user.Nickname),
                };

                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSetting.Secret));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var securityToken = new JwtSecurityToken(_jwtSetting.Issuer, 
                    _jwtSetting.Audience, 
                    claims, 
                    DateTime.Now, 
                    DateTime.Now.AddMinutes(30), 
                    credentials);

                var handler = new JwtSecurityTokenHandler();
                return RespWapper<string>.Successed(handler.WriteToken(securityToken));
            }

            return result.ToResponse();
        }

        public async Task<RespWapper> Logout()
        {
            if (CurrentUser.IsNull())
                return RespWapper.Failed(Errors.InvalidOperation);

            await SignInManager.SignOutAsync();
            return RespWapper.Successed();
        }

        public async Task<RespWapper<PagingDto<ListItem>>> Search(SearchReq req, bool skipAdmin = true)
        {
            var query = _accountService.GetUsers(skipAdmin);

            query = query
                .WhereFalse(req.Nickname.IsNullOrEmpty(), s => s.Nickname.Contains(req.Nickname, StringComparison.OrdinalIgnoreCase))
                .WhereFalse(req.Email.IsNullOrEmpty(), s => s.Email.Contains(req.Email, StringComparison.OrdinalIgnoreCase))
                .WhereNotNull(req.IsMuted, s => s.IsMuted == req.IsMuted.Value)
                .WhereNotNull(req.IsModerator, s => s.UserRoles.Any(r => r.RoleId.Equals(Role.Moderator.Id, StringComparison.OrdinalIgnoreCase)));

            var dtos = query
                .OrderBy(req.OrderBy, req.IsAscending)
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .AsEnumerable()
                .Select(ListItem.From)
                .ToList();

            return RespWapper<PagingDto<ListItem>>.Successed(new PagingDto<ListItem>
            {
                TotalCount = await query.CountAsync(),
                Dtos = dtos
            });
        }

        public async Task<RespWapper> MuteUser(string userId)
        {
            var user = await _accountService.FindUserByIdAsync(userId);
            if (user.IsNull())
                return RespWapper.Failed(Errors.AccountNotFound);

            await _accountService.MuteUser(CurrentUser, user);
            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
            return RespWapper.Successed();
        }

        public async Task<RespWapper> UnMuteUser(string userId)
        {
            var user = await _accountService.FindUserByIdAsync(userId);
            if (user.IsNull())
                return RespWapper.Failed(Errors.AccountNotFound);

            await _accountService.UnMuteUser(CurrentUser, user);
            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
            return RespWapper.Successed();
        }

        public async Task<RespWapper> GetProfile(string userId = null)
        {
            var targetUser = userId.IsNullOrEmpty() ?
                CurrentUser :
                await _accountService.FindUserByIdAsync(userId);

            if (targetUser.IsNull())
                return RespWapper.Failed(Errors.AccountNotFound);

            return RespWapper<ProfileResp>.Successed(ProfileResp.From(targetUser));
        }

        public async Task<RespWapper> EditProfile(EditProfileReq req)
        {
            if (CurrentUser == null || !CurrentUser.Id.Equals(req.Id))
                return RespWapper.Failed(Errors.InvalidOperation);

            CurrentUser.Company = req.Company;
            CurrentUser.Signature = req.Signature;
            CurrentUser.Gender = req.Gender;
            CurrentUser.City = req.City;

            await _accountService.EditProfile(CurrentUser);
            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
            return RespWapper<ProfileResp>.Successed(ProfileResp.From(CurrentUser));
        }

        public async Task<RespWapper> FollowOrNot(string userId)
        {
            if (CurrentUser.Id.Equals(userId, StringComparison.OrdinalIgnoreCase))
                return RespWapper.Failed(Errors.InvalidOperation);

            var targetUser = await _accountService.FindUserByIdAsync(userId);
            if (targetUser.IsNull())
                return RespWapper.Failed(Errors.AccountNotFound);

            var isFollowed = await _accountService.IsFollow(CurrentUser, userId);
            if (isFollowed)
                await _accountService.UnFollowUser(CurrentUser, targetUser);
            else
                await _accountService.FollowUser(CurrentUser, targetUser);

            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
            return RespWapper.Successed();
        }

        public async Task<RespWapper> GetFollows(GetFollowOrFansReq req)
        {
            var targetUser = await _accountService.FindUserByIdAsync(req.Id);
            if (targetUser.IsNull())
                return RespWapper.Failed(Errors.AccountNotFound);

            var query = _accountService.GetFollows(targetUser);
            var dtos = query
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .AsEnumerable()
                .Select(UserSimpleResp.From)
                .ToList();

            return RespWapper.Successed(new PagingDto<UserSimpleResp>
            {
                TotalCount = query.Count(),
                Dtos = dtos
            });
        }

        public async Task<RespWapper> GetFans(GetFollowOrFansReq req)
        {
            var targetUser = await _accountService.FindUserByIdAsync(req.Id);
            if (targetUser.IsNull())
                return RespWapper.Failed(Errors.AccountNotFound);

            var query = _accountService.GetFans(targetUser);
            var dtos = query
                .Skip((req.PageIndex - 1) * req.PageSize)
                .Take(req.PageSize)
                .AsEnumerable()
                .Select(UserSimpleResp.From)
                .ToList();

            return RespWapper.Successed(new PagingDto<UserSimpleResp>
            {
                TotalCount = query.Count(),
                Dtos = dtos
            });
        }

        public async Task<RespWapper> FavoriteOrNot(Guid id)
        {
            var article =  await _articleService.FindById(CurrentUser, id);
            if (article == null)
                return RespWapper.Failed(new Error
                {
                    Code = "ArticleNotFound",
                    Description = "资源不存在"
                });


            var isFavorite = await _accountService.IsFavorite(CurrentUser, id);

            if (isFavorite)
                await _accountService.RemoveFavorite(CurrentUser, article);
            else
                await _accountService.AddFavorite(CurrentUser, article);

            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed();
        }

        public async Task<RespWapper> IsFavorite(Guid id)
        {
            return RespWapper.Successed(await _accountService.IsFavorite(CurrentUser, id));
        }

        public async Task<RespWapper> IsFollow(string id)
        {
            return RespWapper.Successed(await _accountService.IsFollow(CurrentUser, id));
        }

        public async Task<RespWapper> ModifyPassword(ModifyPasswordReq req)
        {
            var user = await UserManager.GetUserAsync(HttpContextAccessor.HttpContext.User);
            if (user == null)
                return RespWapper.Failed(Errors.InvalidOperation);

            var identityResult = await UserManager.ChangePasswordAsync(user, req.Password, req.NewPassword);
            return identityResult.ToResponse();
        }

        public async Task<RespWapper> ModifyAvatar(byte[] avatar)
        {
            if (CurrentUser.IsNull())
                return RespWapper.Failed(Errors.AccountNotFound);

            var base64Image = Convert.ToBase64String(avatar);
            await _accountService.ModifyAvatar(CurrentUser, base64Image);
            await UnitOfWork.SaveChangesAsync();
            return RespWapper.Successed(base64Image);
        }

        public async Task<RespWapper> GetAvatar(string id)
        {
            if (id.IsNullOrEmpty()) 
                return RespWapper.Failed();

            var user = await _accountService.FindUserByIdAsync(id);
            var base64Image = user.Avatar ?? user.DefaultAvatar();
            return RespWapper.Successed(Convert.FromBase64String(base64Image));
        }
    }
}
