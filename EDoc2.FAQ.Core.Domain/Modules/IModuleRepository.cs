using System;
using System.Linq;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Modules
{
    public interface IModuleRepository : IRepository<Module>
    {
        Module Add(Module module);

        Module Enable(Module module);

        Module Update(Module module);

        Module Find(Guid id);

        Task<Module> FindAsync(Guid id);

        Task<IQueryable<Module>> GetModules();
    }
}
