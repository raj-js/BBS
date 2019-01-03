using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Services;

namespace EDoc2.FAQ.Core.Domain.SignIns.Services
{
    public class SignInService : DomainService, ISignInService
    {
        public IQueryable<SignInRecord> GetRecords()
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<SignInRule> GetRules()
        {
            throw new System.NotImplementedException();
        }

        public async Task AddRecord(User @operator, SignInRecord record)
        {
            throw new System.NotImplementedException();
        }

        public async Task AddRule(User @operator, SignInRule rule)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateRule(User @operator, SignInRule rule)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteRule(User @operator, SignInRule rule)
        {
            throw new System.NotImplementedException();
        }
    }
}
