using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Repositories;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Modules
{
    public interface IModuleRepository : IRepository<Module>
    {
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        Module Add(Module module);

        /// <summary>
        /// 启用/禁用模块
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        Module Enable(Module module);

        /// <summary>
        /// 修改模块
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        Module Update(Module module);

        /// <summary>
        /// 根据编号查找模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Module Find(Guid id);

        /// <summary>
        /// 根据编号查找模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Module> FindAsync(Guid id);

        /// <summary>
        /// 查找多个模块
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<Module>> GetModules();
    }
}
