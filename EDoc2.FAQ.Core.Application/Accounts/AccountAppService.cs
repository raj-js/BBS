using EDoc2.FAQ.Core.Application.DtoBase;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Accounts.Services;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static EDoc2.FAQ.Core.Application.Accounts.Dtos.AccountDtos;

namespace EDoc2.FAQ.Core.Application.Accounts
{
    public class AccountAppService : AppServiceBase, IAccountAppService
    {
        private readonly IAccountService _accountService;

        public AccountAppService(IAccountService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<bool> IsEmailRegistered(string email)
        {
            return await _accountService.GetUsers().AnyAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Response> Register(RegisterReq req)
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
                return Response<Register>.Successed(new Register
                {
                    UserId = user.Id,
                    Code = token
                });
            }
            return Response.Failed(result.Errors.ToRespErrors());
        }

        public async Task<Response> EmailConfirm(EmailConfirmReq req)
        {
            var user = await _accountService.FindUserByIdAsync(req.UserId);
            if (user == null)
                return Response.Failed(Errors.AccountNotFound);

            if (user.IsMuted)
                return Response.Failed(Errors.AccountMuted);

            var result = await UserManager.ConfirmEmailAsync(user, req.Code);
            return result.ToResponse();
        }

        public async Task<Response> GenerateResetPasswordToken(RetrievePasswordReq req)
        {
            var user = await UserManager.FindByEmailAsync(req.Email);
            if (user == null)
                return Response.Failed(Errors.EmailNotFound);

            if (user.IsMuted)
                return Response.Failed(Errors.AccountMuted);

            return Response<RetrievePassword>.Successed(new RetrievePassword
            {
                UserId = user.Id,
                Code = await UserManager.GeneratePasswordResetTokenAsync(user)
            });
        }

        public async Task<Response> ResetPassword(ResetPasswordReq req)
        {
            var user = await _accountService.FindUserByIdAsync(req.UserId);
            if (user == null)
                return Response.Failed(Errors.AccountNotFound);

            if (user.IsMuted)
                return Response.Failed(Errors.AccountMuted);

            var result = await UserManager.ResetPasswordAsync(user, req.Code, req.Password);
            return result.ToResponse();
        }

        public async Task<Response> Login(LoginReq req)
        {
            var user = await UserManager.FindByEmailAsync(req.Email);
            if (user == null)
                return Response.Failed(Errors.EmailNotFound);

            if (user.IsMuted)
                return Response.Failed(Errors.AccountMuted);

            var result = await SignInManager.PasswordSignInAsync(req.Email, req.Password, req.RememberMe, true);
            return result.ToResponse();
        }

        public async Task<Response> Logout()
        {
            if(CurrentUser.IsNull())
                return Response.Failed(Errors.InvalidOperation);

            await SignInManager.SignOutAsync();
            return Response.Successed();
        }

        public async Task<Response> Search(SearchReq req, bool skipAdmin = true)
        {
            var query = _accountService.GetUsers(skipAdmin);

            query = query
                .WhereFalse(req.Nickname.IsNullOrEmpty(), s => s.Nickname.Contains(req.Nickname, StringComparison.OrdinalIgnoreCase))
                .WhereFalse(req.Email.IsNullOrEmpty(), s => s.Email.Contains(req.Email, StringComparison.OrdinalIgnoreCase))
                .WhereNotNull(req.IsMuted, s => s.IsMuted == req.IsMuted.Value)
                .WhereNotNull(req.IsModerator, s => s.UserRoles.Any(r => r.RoleId.Equals(Role.Moderator.Id, StringComparison.OrdinalIgnoreCase)));

            var dtos = query
                .OrderBy(req.OrderBy, req.IsAscending)
                .Skip(req.Skip)
                .Take(req.Take)
                .AsEnumerable()
                .Select(ListItem.From)
                .ToList();
            
            return Response<PagingDto<ListItem>>.Successed(new PagingDto<ListItem>
            {
                TotalCount = await query.CountAsync(),
                Dtos = dtos
            });
        }

        public async Task<Response> MuteUser(string userId)
        {
            var user = await _accountService.FindUserByIdAsync(userId);
            if (user.IsNull())
                return Response.Failed(Errors.AccountNotFound);

            await _accountService.MuteUser(CurrentUser, user);
            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
            return Response.Successed();
        }

        public async Task<Response> UnMuteUser(string userId)
        {
            var user = await _accountService.FindUserByIdAsync(userId);
            if (user.IsNull())
                return Response.Failed(Errors.AccountNotFound);

            await _accountService.UnMuteUser(CurrentUser, user);
            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
            return Response.Successed();
        }

        public async Task<Response> GetProfile(string userId = null)
        {
            var targetUser = userId.IsNullOrEmpty() ?
                CurrentUser :
                await _accountService.FindUserByIdAsync(userId);

            if (targetUser.IsNull())
                return Response.Failed(Errors.AccountNotFound);

            return Response<Profile>.Successed(Profile.From(targetUser)); 
        }

        public async Task<Response> EditProfile(EditProfileReq req)
        {
            if (CurrentUser == null || CurrentUser.Id.Equals(req.Id))
                return Response.Failed(Errors.InvalidOperation);

            var user = await _accountService.FindUserByIdAsync(req.Id);
            if (user.IsNull())
                return Response.Failed(Errors.AccountNotFound);

            user.Nickname = req.Nickname;
            user.Signature = req.Signature;
            user.Gender = req.Gender;
            user.City = req.City;

            await _accountService.EditProfile(user);
            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
            return Response<Profile>.Successed(Profile.From(user));
        }

        public async Task<Response> Follow(string userId)
        {
            if (CurrentUser.Id.Equals(userId, StringComparison.OrdinalIgnoreCase))
                return Response.Failed(Errors.InvalidOperation);

            var targetUser = await _accountService.FindUserByIdAsync(userId);
            if (targetUser.IsNull())
                return Response.Failed(Errors.AccountNotFound);

            if (targetUser.IsAdministrator)
                return Response.Failed(Errors.InvalidOperation);

            await _accountService.FollowUser(CurrentUser, targetUser);
            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
            return Response.Successed();
        }

        public async Task<Response> UnFollow(string userId)
        {
            if (CurrentUser.Id.Equals(userId, StringComparison.OrdinalIgnoreCase))
                return Response.Failed(Errors.InvalidOperation);

            var targetUser = await _accountService.FindUserByIdAsync(userId);
            if (targetUser.IsNull())
                return Response.Failed(Errors.AccountNotFound);

            if (targetUser.IsAdministrator)
                return Response.Failed(Errors.InvalidOperation);

            await _accountService.UnFollowUser(CurrentUser, targetUser);
            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
            return Response.Successed();
        }
    }
}
