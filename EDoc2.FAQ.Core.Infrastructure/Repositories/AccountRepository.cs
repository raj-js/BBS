using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Uow;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EDoc2.FAQ.Core.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CommunityContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public AccountRepository(CommunityContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddUser(User user)
        {
            _context.Set<User>().Add(user);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Set<User>().AddAsync(user);
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task UpdateUserAsync(User user)
        {
            UpdateUser(user);
            await Task.CompletedTask;
        }

        public IQueryable<User> GetUsers()
        {
            return _context.Users.AsQueryable();
        }

        public User FindUserById(string id)
        {
            return _context.Set<User>().Find(id);
        }

        public async Task<User> FindUserByIdAsync(string id)
        {
            return await _context.Set<User>().FindAsync(id);
        }
    }
}
