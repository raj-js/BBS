using EDoc2.FAQ.Core.Domain.Applications;
using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.SeedWork;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CommunityContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IArticleRepository _articleRepository;

        public IUnitOfWork UnitOfWork => _context;

        public AccountRepository(CommunityContext context,
            UserManager<User> userManager,
            IArticleRepository articleRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        }

        public async Task<IdentityResult> CreateAdmin(User user, string password, bool allowMultipleAdmin = false)
        {
            if (user.IsNull())
                throw new ArgumentNullException(nameof(user));

            if (password.IsNullOrEmpty())
                throw new ArgumentException(nameof(password));

            if (!allowMultipleAdmin)
            {
                if (_context.UserRoles.Any(s => s.RoleId == Role.Administrator.Id))
                {
                    return IdentityResult.Failed(new IdentityError
                    {
                        Code = "NotAllowMultipleAdmin",
                        Description = "不允许多个管理员账号"
                    });
                }
            }
            var identityResult = await _userManager.CreateAsync(user, password);
            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role.Administrator.NormalizedName);
                await UnitOfWork.SaveEntitiesAsync();
            }
            return identityResult;
        }

        public async Task<IdentityResult> RegisterAsync(User user, string password)
        {
            if (user.IsNull())
                throw new ArgumentNullException(nameof(user));

            if (password.IsNullOrEmpty())
                throw new ArgumentException(nameof(password));

            var identityResult = await _userManager.CreateAsync(user, password);

            if (identityResult.Succeeded)
            {
                user.Initialize();
                await _userManager.AddToRoleAsync(user, Role.Member.NormalizedName);
                await UnitOfWork.SaveEntitiesAsync();
            }
            return identityResult;
        }

        public IQueryable<User> GetUsers()
        {
            return _userManager.Users;
        }

        public async Task<User> FindAsync(string id)
        {
            if (id.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(id));

            return await _userManager.FindByIdAsync(id);
        }

        public User Find(string id)
        {
            if (id.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(id));

            return _context.Users.Find(id);
        }

        public async Task Follow(User user, string userId)
        {
            if (user.IsNull())
                throw new ArgumentNullException(nameof(user));

            if (userId.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(userId));

            if (user.IsTransient())
                throw new InvalidOperationException("user is transient");

            user.Follow(userId);
            await UnitOfWork.SaveEntitiesAsync();
        }

        public async Task UnFollow(User user, string userId)
        {
            if (user.IsNull())
                throw new ArgumentNullException(nameof(user));

            if (userId.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(userId));

            if (user.IsTransient())
                throw new InvalidOperationException("user is transient");

            user.UnFollow(userId);
            await UnitOfWork.SaveEntitiesAsync();
        }

        public async Task AddFavorite(User user, Guid articleId)
        {
            if (user.IsNull())
                throw new ArgumentNullException(nameof(user));

            if (user.IsTransient())
                throw new InvalidOperationException("user is transient");

            if (!await _articleRepository.CanFavoriteArticle(articleId))
                throw new InvalidOperationException("article cann't be favorite in this state");

            user.AddFavorite(articleId);
            await UnitOfWork.SaveEntitiesAsync();
        }

        public async Task RemoveFavorite(User user, Guid articleId)
        {
            if (user.IsNull())
                throw new ArgumentNullException(nameof(user));

            if (user.IsTransient())
                throw new InvalidOperationException("user is transient");

            user.RemoveFavorite(articleId);
            await UnitOfWork.SaveEntitiesAsync();
        }

        public IQueryable<User> GetFollows(User user)
        {
            if (user.IsNull())
                throw new ArgumentNullException(nameof(user));

            if (user.IsTransient())
                throw new InvalidOperationException("user is transient");

            return user.UserFollows.AsQueryable().Where(s => !s.IsCancel).Select(s => s.Follow);
        }

        public IQueryable<User> GetFans(User user)
        {
            if (user.IsNull())
                throw new ArgumentNullException(nameof(user));

            if (user.IsTransient())
                throw new InvalidOperationException("user is transient");

            return user.UserFans.AsQueryable().Where(s => !s.IsCancel).Select(s => s.Fan);
        }

        public IQueryable<UserFavorite> GetFavorites(User user)
        {
            return user.UserFavorites.AsQueryable();
        }

        public Task GrantModerator(User @operator, string userId, string moduleId)
        {
            throw new NotImplementedException();
        }

        public Task RecycleModerator(User @operator, string userId, string moduleId)
        {
            throw new NotImplementedException();
        }

        public Task ChangeModerator(User @operator, string grantUserId, string recycleUserId, string moduleId)
        {
            throw new NotImplementedException();
        }
    }
}
