using System;
using System.Threading;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Domain.Models.ArticleAggregate;
using EDoc2.FAQ.Core.Domain.Models.CommentAggregate;
using EDoc2.FAQ.Core.Domain.SeedWork;
using EDoc2.FAQ.Core.Repositories.EntityFrameworkCore.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EDoc2.FAQ.Core.Repositories.EntityFrameworkCore
{
    public class CommunityContext : DbContext, IUnitOfWork
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        private readonly IMediator _mediator;

        public CommunityContext(DbContextOptions<CommunityContext> options): base(options) { }

        public CommunityContext(DbContextOptions<CommunityContext> options, IMediator mediator) :base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //在提交数据改动到数据库之前调度所有领域事件
            await _mediator.DispatchDomainEventsAsync(this);

            await SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
