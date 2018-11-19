using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.SeedWork;
using EDoc2.FAQ.Core.Repositories.EntityFrameworkCore.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Repositories.EntityFrameworkCore
{
    public class CommunityContext : DbContext, IUnitOfWork
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<ArticleState> ArticleStates { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }
        public DbSet<ArticleCommentState> ArticleCommentStates { get; set; }
        public DbSet<ArticleProperty> ArticleProperties { get; set; }
        public DbSet<ArticleOperation> ArticleOperations { get; set; }

        private readonly IMediator _mediator;

        public CommunityContext(DbContextOptions<CommunityContext> options): base(options) { }

        public CommunityContext(DbContextOptions<CommunityContext> options, IMediator mediator) :base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleCommentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleCommentStateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticlePropertyEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleStateEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleTypeEntityTypeConfiguration());
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
