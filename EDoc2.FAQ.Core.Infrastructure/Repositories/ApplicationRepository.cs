using EDoc2.FAQ.Core.Domain.Applications;
using EDoc2.FAQ.Core.Domain.Repositories;
using System;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Infrastructure.Extensions;

namespace EDoc2.FAQ.Core.Infrastructure.Repositories
{
    public class ApplicationRepository : RepositoryBase, IApplicationRepository
    {
        private CommunityContext Context => UnitOfWork as CommunityContext;

        public async Task<Application> FindById(Guid id)
        {
            return await Context.Set<Application>().FindAsync(id);
        }

        public async Task Create(Application application)
        {
            await Context.Set<Application>().AddAsync(application);
        }

        public async Task Update(Application application, params string[] properties)
        {
            Context.AttachIfNot(application);
            Context.UpdatePartly(application, properties);
            await Task.CompletedTask;
        }

        public async Task AddSetting(ApplicationSetting setting)
        {
            await Context.Set<ApplicationSetting>().AddAsync(setting);
        }

        public async Task UpdateSetting(ApplicationSetting setting, params string[] properties)
        {
            Context.AttachIfNot(setting);
            Context.UpdatePartly(setting, properties);
            await Task.CompletedTask;
        }
    }
}
