using EDoc2.FAQ.Core.Domain.Notifies;
using EDoc2.FAQ.Core.Domain.Repositories;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Infrastructure.Repositories
{
    public class NotifyRepository: RepositoryBase, INotifyRepository
    {
        private CommunityContext Context => UnitOfWork as CommunityContext;

        public IQueryable<Notify> GetNotifies()
        {
            return Context.Set<Notify>();
        }

        public async Task<Notify> FindNotifyById(Guid id)
        {
            return await Context.Set<Notify>().FindAsync(id);
        }

        public async Task AddNotify(Notify notify)
        {
            await Context.Set<Notify>().AddAsync(notify);
        }

        public async Task UpdateNotify(Notify notify, params string[] properties)
        {
            Context.AttachIfNot(notify);
            Context.UpdatePartly(notify, properties);
            await Task.CompletedTask;
        }

        public async Task DeleteNotify(Notify notify)
        {
            Context.Remove(notify);
            await Task.CompletedTask;
        }

        public IQueryable<NotifyBelong> GetNotifyBelongTo()
        {
            return Context.Set<NotifyBelong>();
        }
    }
}
