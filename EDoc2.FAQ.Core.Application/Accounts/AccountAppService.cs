using EDoc2.FAQ.Core.Application.Mails;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Applications;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Application.Accounts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EDoc2.FAQ.Core.Application.Accounts
{
    public class AccountAppService : AppServiceBase, IAccountAppService
    {
        private readonly SignInManager<User> _singInManager;
        private readonly UserManager<User> _userManager;
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountAppService> _logger;

        public AccountAppService(SignInManager<User> singInManager,
            UserManager<User> userManager,
            IAccountRepository accountRepository,
            ILogger<AccountAppService> logger,
            IMailService mailService)
        {
            _singInManager = singInManager ?? throw new ArgumentNullException(nameof(singInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> IsEmailRegistered(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            return await _userManager.Users.AnyAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IdentityResult> Register(AccountDtos.Register dto)
        {
            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                Nickname = dto.Nickname,
            };
            return await _accountRepository.RegisterAsync(user, dto.Password);
        }

        public async Task<string> GenerateResetPasswordToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new InvalidOperationException($"{email} not register");

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<SignInResult> Login(AccountDtos.Login dto)
        {
            return await _singInManager.PasswordSignInAsync(dto.Email, dto.Password, dto.RememberMe, true);
        }

        public async Task<PagingDto<AccountDtos.ListItem>> Search(AccountDtos.Search dto)
        {
            var query = _accountRepository.GetUsers();

            query = query
                .WhereIfNot(string.IsNullOrEmpty(dto.Nickname),
                    s => s.Nickname.Contains(dto.Nickname, StringComparison.OrdinalIgnoreCase))
                .WhereIfNot(string.IsNullOrEmpty(dto.Email),
                    s => s.Email.Contains(dto.Email, StringComparison.OrdinalIgnoreCase));

            var dtos = await query
                .OrderBy(dto.OrderBy, dto.IsAsc)
                .Skip(dto.Skip)
                .Take(dto.Take)
                .ToListAsync();

            var totalCount = await query.CountAsync();
            return new PagingDto<AccountDtos.ListItem>(totalCount, dtos.Select(AccountDtos.ListItem.From).ToList());
        }

        public void MuteUser(string userId)
        {
            throw new NotImplementedException();
        }

        public void GrantModerator(string userId, Guid moduleId)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountDtos.Details> GetUserDetails(string userId)
        {
            if(string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));

            var user = await _accountRepository.FindAsync(userId);

            return null;
        }

        public AccountDtos.Details EditProfile(AccountDtos.Edit editDto)
        {
            throw new NotImplementedException();
        }

        public void Follow(string userId)
        {
            throw new NotImplementedException();
        }

        public void UnFollow(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
