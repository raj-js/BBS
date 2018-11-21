using EDoc2.FAQ.Core.Application.Authorization.Dtos;
using EDoc2.FAQ.Core.Application.Mails;
using EDoc2.FAQ.Core.Application.ServiceBase;
using EDoc2.FAQ.Core.Domain.Authorization;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EDoc2.FAQ.Core.Application.Authorization
{
    public class UserAppService : AppServiceBase, IUserAppService
    {
        private readonly SignInManager<User> _singInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        private readonly ILogger<UserAppService> _logger;

        public UserAppService(SignInManager<User> singInManager,
            UserManager<User> userManager,
            IUserRepository userRepository,
            ILogger<UserAppService> logger,
            IMailService mailService)
        {
            _singInManager = singInManager ?? throw new ArgumentNullException(nameof(singInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        public async Task<SignInResult> Login(UserDtos.Login dto)
        {
            return await _singInManager.PasswordSignInAsync(dto.Email, dto.Password, dto.RememberMe, true);
        }

        public async Task<IdentityResult> Register(UserDtos.Register dto)
        {
            var user = new User
            {
                Email = dto.Email
            };
            var identityResult = await _userManager.CreateAsync(user, dto.Password);
            if (!identityResult.Succeeded) return identityResult;

            user.Nickname = dto.Nickname;
            user.JoinDate = DateTime.Now;

            user.SetNickname(user.Nickname);
            user.SetJoinDate(user.JoinDate);
            user.SetScore(0);
            await _userManager.UpdateAsync(user);

            return identityResult;
        }

        public async Task<PagingDto<UserDtos.ListItem>> Search(UserDtos.Search dto)
        {
            var query = await _userRepository.GetUsers();

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
            return new PagingDto<UserDtos.ListItem>(totalCount, dtos.Select(UserDtos.ListItem.From).ToList());
        }
    }
}
