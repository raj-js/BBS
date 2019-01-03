using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Services;

namespace EDoc2.FAQ.Core.Domain.SignIns.Services
{
    public interface ISignInService : IDomainService
    {
        IQueryable<SignInRecord> GetRecords();

        IQueryable<SignInRule> GetRules();

        Task AddRecord(User @operator, SignInRecord record);

        Task AddRule(User @operator, SignInRule rule);

        Task UpdateRule(User @operator, SignInRule rule);

        Task DeleteRule(User @operator, SignInRule rule);
    }
}
