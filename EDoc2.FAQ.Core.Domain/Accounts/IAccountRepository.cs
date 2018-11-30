using EDoc2.FAQ.Core.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    public interface IAccountRepository : IRepository<User>
    {
        void AddUser(User user);
        Task AddUserAsync(User user);
        void UpdateUser(User user);
        Task UpdateUserAsync(User user);
        IQueryable<User> GetUsers();
        User FindUserById(string id);
        Task<User> FindUserByIdAsync(string id);
    }
}
