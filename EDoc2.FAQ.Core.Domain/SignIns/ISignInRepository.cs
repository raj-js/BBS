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
        /// <summary>
        /// 获取签到记录
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<SignInRecord>> GetSignInRecordsAsync();

        /// <summary>
        /// 用户签到
        /// </summary>
        /// <returns></returns>
        Task SignIn(string userId);

        /// <summary>
        /// 获取签到规则
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<SignInRule>> GetSignInRulesAsync();

        /// <summary>
        /// 增加签到规则
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        SignInRule AddRule(SignInRule rule);

        /// <summary>
        /// 根据编号获取签到规则
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SignInRule FindRule(int id);

        /// <summary>
        /// 根据编号获取签到规则
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SignInRule> FindRuleAsync(int id);

        /// <summary>
        /// 更新签到规则
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        SignInRule UpdateRule(SignInRule rule);

        /// <summary>
        /// 删除签到规则
        /// </summary>
        /// <param name="rule"></param>
        void DeleteRule(SignInRule rule);
    }
}
