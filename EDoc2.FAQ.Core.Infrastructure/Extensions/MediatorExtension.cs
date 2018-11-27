using EDoc2.FAQ.Core.Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Infrastructure.Extensions
{
    public static class MediatorExtension
    {
        /// <summary>
        /// 发布DB上下文中追踪实体所有的领域事件
        /// </summary>
        /// <param name="this"></param>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static async Task DispatchDomainEventsAsync(this IMediator @this, DbContext ctx)
        {
            var domainEntries = ctx.ChangeTracker.Entries<IEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntries
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntries.ForEach(entry => entry.Entity.ClearDomainEvent());

            var tasks = domainEvents.Select(async (@event) => { await @this.Publish(@event); });
            await Task.WhenAll(tasks);
        }
    }
}
