using EDoc2.FAQ.Core.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Domain.SignIns
{
    /// <summary>
    /// 签到管理
    /// </summary>
    public interface ISignInRepository : IRepository<SignInRecord>
    {
        IQueryable<SignInRecord> GetRecords();

        Task AddRecord(SignInRecord record);

        IQueryable<SignInRule> GetRules();

        Task AddRule(SignInRule rule);

        Task UpdateRulePartly(SignInRule rule, params string[] properties);

        Task DeleteRule(SignInRule rule);

        Task<SignInRecord> FindRecordById(int id);

        Task<SignInRule> FindRuleById(int id);
    }
}
