using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.SeedWork;
using EDoc2.FAQ.Core.Repositories.EntityFrameworkCore.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Repositories.EntityFrameworkCore.Contexts
{
    public class CommunityContext : DbContext, IUnitOfWork
    {
        public DbSet<Article> Articles { get; set; }//
        public DbSet<ArticleComment> ArticleComments { get; set; } //
        public DbSet<ArticleCommentState> ArticleCommentStates { get; set; } //
        public DbSet<ArticleOperation> ArticleOperations { get; set; }
        public DbSet<ArticleOperationSourceType> ArticleOperationSourceTypes { get; set; }
        public DbSet<ArticleOperationType> ArticleOperationTypes { get; set; }
        public DbSet<ArticleProperty> ArticleProperties { get; set; }
        public DbSet<ArticleState> ArticleStates { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }

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

            modelBuilder.ApplyConfiguration(new ArticleOperationEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleOperationSourceTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleOperationTypeEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);

            await SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public class CommunityContextDbFactory : IDesignTimeDbContextFactory<CommunityContext>
    {
        public CommunityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CommunityContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectsV13;Initial Catalog=EDoc2.FAQ.Community;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new CommunityContext(optionsBuilder.Options);
        }
    }
}
