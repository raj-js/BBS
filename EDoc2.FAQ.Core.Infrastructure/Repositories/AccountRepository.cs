using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Repositories;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Infrastructure.Repositories
{
    public class AccountRepository : RepositoryBase, IAccountRepository
    {
        private CommunityContext Context => UnitOfWork as CommunityContext;

        public void AddUser(User user)
        {
            Context.Set<User>().Add(user);
        }

        public async Task AddUserAsync(User user)
        {
            await Context.Set<User>().AddAsync(user);
        }

        public void UpdateUser(User user)
        {
            Context.AttachIfNot(user);
            Context.Set<User>().Update(user);
        }

        public void UpdatePartly(User user, params string[] properties)
        {
            Context.AttachIfNot(user);
            Context.UpdatePartly(user, properties);
        }

        public async Task UpdateUserAsync(User user)
        {
            UpdateUser(user);
            await Task.CompletedTask;
        }

        public IQueryable<User> GetUsers()
        {
            return Context.Users.AsQueryable();
        }

        public User FindUserById(string id)
        {
            return Context.Set<User>().Find(id);
        }

        public async Task<User> FindUserByIdAsync(string id)
        {
            return await Context.FindAsync<User>(id);
        }

        public async Task<UserSubscriber> AddSubscriber(UserSubscriber subscriber)
        {
            return (await Context.AddAsync(subscriber)).Entity;
        }

        public async Task<UserSubscriber> UpdateSubscriber(UserSubscriber subscriber)
        {
            await Task.CompletedTask;
            return Context.Update(subscriber).Entity;
        }

        public async Task<UserProperty> AddProperty(UserProperty property)
        {
            return (await Context.AddAsync(property)).Entity;
        }

        public async Task<UserProperty> UpdateProperty(UserProperty property)
        {
            await Task.CompletedTask;
            return Context.Update(property).Entity;
        }

        public async Task AddScoreChange(UserScoreHistory history)
        {
            await Context.Set<UserScoreHistory>().AddAsync(history);
        }

        public IQueryable<UserScoreHistory> GetScoreChanges()
        {
            return Context.Set<UserScoreHistory>();
        }
    }
}
