using EDoc2.FAQ.Core.Domain.SeedWork;

namespace EDoc2.FAQ.Core.Domain.Modules
{
    public interface IModuleRepository : IRepository<Module>
    {
        Module Add(Module module);
    }
}
