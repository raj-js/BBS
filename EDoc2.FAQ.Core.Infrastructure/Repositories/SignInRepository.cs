using EDoc2.FAQ.Core.Domain.Repositories;
using EDoc2.FAQ.Core.Domain.SignIns;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Infrastructure.Repositories
{
    public class SignInRepository : RepositoryBase, ISignInRepository
    {
        private CommunityContext Context => UnitOfWork as CommunityContext;

        public IQueryable<SignInRecord> GetRecords()
        {
            throw new NotImplementedException();
        }

        public async Task AddRecord(SignInRecord record)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SignInRule> GetRules()
        {
            throw new NotImplementedException();
        }

        public async Task AddRule(SignInRule rule)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRulePartly(SignInRule rule, params string[] properties)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteRule(SignInRule rule)
        {
            throw new NotImplementedException();
        }

        public async Task<SignInRecord> FindRecordById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SignInRule> FindRuleById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
