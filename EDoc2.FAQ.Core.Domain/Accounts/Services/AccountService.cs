using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Accounts.Services
{
    public class AccountService : DomainService, IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly IAccountRepository _accountRepo;

        public AccountService(UserManager<User> userManager,
            IAccountRepository accountRepo)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _accountRepo = accountRepo ?? throw new ArgumentNullException(nameof(accountRepo));
        }

        public IQueryable<User> GetUsers(bool skipAdmin = true)
        {
            return skipAdmin ? _accountRepo.GetUsers().Where(u => u.UserRoles.All(r => r.RoleId != Role.Administrator.Id)) : _accountRepo.GetUsers();
        }

        public async Task<User> FindUserByIdAsync(string id)
        {
            return await _accountRepo.FindUserByIdAsync(id);
        }

        public User FindUserById(string id)
        {
            return _accountRepo.FindUserById(id);
        }

        public IQueryable<User> GetFollows(User user)
        {
            return user.UserFollows.AsQueryable().Select(s => s.Follow);
        }

        public IQueryable<User> GetFans(User user)
        {
            return user.UserFans.AsQueryable().Select(s => s.Fan);
        }

        public IQueryable<Article> GetFavoriteArticles(User user)
        {
            return user.UserFavorites.AsQueryable().Select(s => s.Article);
        }

        public async Task<IdentityResult> Create(User user, string password, bool isSetAdmin = false, bool allowMultipleAdmin = false)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return result;

            user.Initialize();
            await _accountRepo.UpdateUserAsync(user);

            if (isSetAdmin)
            {
                if (!allowMultipleAdmin)
                {
                    var exists = _accountRepo
                        .GetUsers()
                        .Any(s => s.UserRoles.All(r => r.RoleId != Role.Administrator.Id));

                    result = exists ?
                        IdentityResult.Failed(new IdentityError
                        {
                            Code = "NotAllowMultipluAdmin",
                            Description = "不允许多个管理员"
                        }) :
                        await _userManager.AddToRoleAsync(user, Role.Administrator.NormalizedName);
                }
                else
                {
                    result = await _userManager.AddToRoleAsync(user, Role.Administrator.NormalizedName);
                }
            }
            else
            {
                result = await _userManager.AddToRoleAsync(user, Role.Member.NormalizedName);
            }

            if (!result.Succeeded)
                await _userManager.DeleteAsync(user);

            return result;
        }

        public async Task<User> EditProfile(User user)
        {
            _accountRepo.UpdatePartly(user, 
                nameof(User.Nickname),
                nameof(User.Signature),
                nameof(User.Gender),
                nameof(User.City));

            await Task.CompletedTask;
            return user;
        }

        public async Task MuteUser(User @operator, User targetUser)
        {
            if (!@operator.IsAdministrator && !@operator.IsModerator)
                throw new UnauthorizedAccessException();

            targetUser.IsMuted = true;
            _accountRepo.UpdatePartly(targetUser, nameof(User.IsMuted));
            await Task.CompletedTask;
        }

        public async Task UnMuteUser(User @operator, User targetUser)
        {
            if (!@operator.IsAdministrator && !@operator.IsModerator)
                throw new UnauthorizedAccessException();

            targetUser.IsMuted = false;
            _accountRepo.UpdatePartly(targetUser, nameof(User.IsMuted));
            await Task.CompletedTask;
        }

        public async Task FollowUser(User @operator, User targetUser)
        {
            if (@operator.IsFanOf(targetUser.Id, out var subscriber)) return;

            if (subscriber == null)
                await _accountRepo.AddSubscriber(new UserSubscriber(@operator.Id, targetUser.Id));
            else
            {
                subscriber.IsCancel = false;
                await _accountRepo.UpdateSubscriber(subscriber);
            }

            var followsProperty = @operator.GetOrSetProperty(UserProperty.Follows, @operator.Follows + 1);
            await _accountRepo.UpdateProperty(followsProperty);

            var fansProperty = targetUser.GetOrSetProperty(UserProperty.Fans, targetUser.Fans + 1);
            await _accountRepo.UpdateProperty(fansProperty);
        }

        public async Task UnFollowUser(User @operator, User targetUser)
        {
            if (!@operator.IsFanOf(targetUser.Id, out var subscriber)) return;

            if (subscriber != null)
            {
                subscriber.IsCancel = true;
                await _accountRepo.UpdateSubscriber(subscriber);
            }

            var followsProperty = @operator.GetOrSetProperty(UserProperty.Follows, @operator.Follows - 1);
            await _accountRepo.UpdateProperty(followsProperty);

            var fansProperty = targetUser.GetOrSetProperty(UserProperty.Fans, targetUser.Fans - 1);
            await _accountRepo.UpdateProperty(fansProperty);
        }

        public async Task AddFavorite(User @operator, Article article)
        {
            @operator.AddFavorite(article.Id);
            await Task.CompletedTask;
        }

        public async Task RemoveFavorite(User @operator, Article article)
        {
            @operator.RemoveFavorite(article.Id);
            await Task.CompletedTask;
        }
    }
}
