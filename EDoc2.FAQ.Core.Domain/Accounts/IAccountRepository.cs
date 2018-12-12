using EDoc2.FAQ.Core.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.Accounts
{
    public interface IAccountRepository : IRepository<User>
    {
        void AddUser(User user);
        Task AddUserAsync(User user);
        void UpdateUser(User user);
        void UpdatePartly(User user, params string[] properties);
        Task UpdateUserAsync(User user);
        IQueryable<User> GetUsers();
        User FindUserById(string id);
        Task<User> FindUserByIdAsync(string id);
        Task<UserSubscriber> AddSubscriber(UserSubscriber subscriber);
        Task<UserSubscriber> UpdateSubscriber(UserSubscriber subscriber);
        Task<UserProperty> AddProperty(UserProperty property);
        Task<UserProperty> UpdateProperty(UserProperty property);

        Task AddScoreChange(UserScoreHistory history);
        IQueryable<UserScoreHistory> GetScoreChanges();
    }
}
