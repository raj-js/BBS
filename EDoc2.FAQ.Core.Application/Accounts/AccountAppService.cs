using EDoc2.FAQ.Core.Application.Accounts.Dtos;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Accounts.Services;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Exceptions;

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
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            return await _accountService.GetUsers().AnyAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<AccountDtos.RegisterResp> Register(AccountDtos.RegisterReq req)
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
                return AccountDtos.RegisterResp.Success(user.Id, token);
            }
            else
                return AccountDtos.RegisterResp.Failed(result.Errors.ToArray());
        }

        public async Task<IdentityResult> EmailConfirm(AccountDtos.EmailConfirmReq req)
        {
            var user = await _accountService.FindUserByIdAsync(req.UserId);

            if (user == null)
                throw new AccountNotFoundException(req.UserId);

            return await UserManager.ConfirmEmailAsync(user, req.Code);
        }

        public async Task<AccountDtos.RetrievePasswordResp> GenerateResetPasswordToken(AccountDtos.RetrievePasswordReq req)
        {
            var user = await UserManager.FindByEmailAsync(req.Email);

            if (user == null)
                throw new EmailNotFoundException(req.Email);

            return new AccountDtos.RetrievePasswordResp
            {
                UserId = user.Id,
                Code = await UserManager.GeneratePasswordResetTokenAsync(user)
            };
        }

        public async Task ResetPassword(AccountDtos.ResetPasswordReq req)
        {
            var user = await _accountService.FindUserByIdAsync(req.UserId);

            if (user == null)
                throw new AccountNotFoundException(req.UserId);

            await UserManager.ResetPasswordAsync(user, req.Code, req.Password);
        }

        public async Task<SignInResult> Login(AccountDtos.LoginReq dto)
        {
            return await SignInManager.PasswordSignInAsync(dto.Email, dto.Password, dto.RememberMe, true);
        }

        public async Task<PagingDto<AccountDtos.ListItem>> Search(AccountDtos.SearchReq dto, bool skipAdmin = true)
        {
            var query = _accountService.GetUsers(skipAdmin);

            query = query
                .WhereFalse(dto.Nickname.IsNullOrEmpty(), s => s.Nickname.Contains(dto.Nickname, StringComparison.OrdinalIgnoreCase))
                .WhereFalse(dto.Email.IsNullOrEmpty(), s => s.Email.Contains(dto.Email, StringComparison.OrdinalIgnoreCase))
                .WhereNotNull(dto.IsMuted, s => s.IsMuted == dto.IsMuted.Value)
                .WhereNotNull(dto.IsModerator, s => s.UserRoles.Any(r => r.RoleId.Equals(Role.Moderator.Id, StringComparison.OrdinalIgnoreCase)));

            var dtos = query
                .OrderBy(dto.OrderBy, dto.IsAscending)
                .Skip(dto.Skip)
                .Take(dto.Take)
                .AsEnumerable()
                .Select(AccountDtos.ListItem.From)
                .ToList();

            return new PagingDto<AccountDtos.ListItem>
            {
                TotalCount = await query.CountAsync(),
                Dtos = dtos
            };
        }

        public async Task MuteUser(string userId)
        {
            if(userId.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(userId));

            var user = await _accountService.FindUserByIdAsync(userId);
            if(user.IsNull())
                throw new AccountNotFoundException(userId);

            await _accountService.MuteUser(CurrentUser, user);
            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
        }

        public async Task<AccountDtos.Details> GetUserProfile(string userId = null)
        {
            var targetUser = userId.IsNullOrEmpty() ? 
                CurrentUser :
                await _accountService.FindUserByIdAsync(userId);

            if(targetUser.IsNull())
                throw new AccountNotFoundException(userId);

            return AccountDtos.Details.From(targetUser);
        }

        public async Task<AccountDtos.Details> EditProfile(AccountDtos.EditProfileReq editProfileReqDto)
        {
            //edit profile

            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
            return AccountDtos.Details.From(CurrentUser);
        }

        public async Task Follow(string userId)
        {
            if (CurrentUser.Id.Equals(userId, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException();

            var targetUser = await _accountService.FindUserByIdAsync(userId);
            if(targetUser.IsNull())
                throw new AccountNotFoundException(userId);

            if(targetUser.IsAdministrator)
                throw new InvalidOperationException();

            await _accountService.FollowUser(CurrentUser, targetUser);
            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
        }

        public async Task UnFollow(string userId)
        {
            if (CurrentUser.Id.Equals(userId, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException();

            var targetUser = await _accountService.FindUserByIdAsync(userId);
            if (targetUser.IsNull())
                throw new AccountNotFoundException(userId);

            if (targetUser.IsAdministrator)
                throw new InvalidOperationException();

            await _accountService.UnFollowUser(CurrentUser, targetUser);
            await UnitOfWork.SaveChangesWithDispatchDomainEvents();
        }
    }
}
