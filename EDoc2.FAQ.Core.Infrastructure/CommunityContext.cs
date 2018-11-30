using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Articles;
using EDoc2.FAQ.Core.Domain.Uow;
using EDoc2.FAQ.Core.Infrastructure.EntityConfigurations;
using EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Articles;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Infrastructure
{
    public class CommunityContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IUnitOfWork
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        #region 文章相关

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCommentState> ArticleCommentStates { get; set; }
        public DbSet<ArticleOperation> ArticleOperations { get; set; }
        public DbSet<ArticleOperationSourceType> ArticleOperationSourceTypes { get; set; }
        public DbSet<ArticleOperationType> ArticleOperationTypes { get; set; }
        public DbSet<ArticleProperty> ArticleProperties { get; set; }
        public DbSet<ArticleState> ArticleStates { get; set; }
        public DbSet<ArticleType> ArticleTypes { get; set; }

        #endregion

        #region 会员相关

        public DbSet<ScoreChange> ScoreChanges { get; set; }

        #endregion

        private readonly IMediator _mediator;

        public CommunityContext(DbContextOptions<CommunityContext> options): base(options) { }

        public CommunityContext(DbContextOptions<CommunityContext> options, IMediator mediator) :base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder b)
        {
            #region 文章相关

            b.ApplyConfiguration(new ArticleEntityTypeConfiguration());
            b.ApplyConfiguration(new ArticleCommentEntityTypeConfiguration());
            b.ApplyConfiguration(new ArticleCommentStateEntityTypeConfiguration());
            b.ApplyConfiguration(new ArticlePropertyEntityTypeConfiguration());
            b.ApplyConfiguration(new ArticleStateEntityTypeConfiguration());
            b.ApplyConfiguration(new ArticleTypeEntityTypeConfiguration());
            b.ApplyConfiguration(new ArticleOperationEntityTypeConfiguration());
            b.ApplyConfiguration(new ArticleOperationSourceTypeEntityTypeConfiguration());
            b.ApplyConfiguration(new ArticleOperationTypeEntityTypeConfiguration());

            #endregion

            #region 会员相关

            b.ApplyConfiguration(new UserEntityTypeConfiguration());
            b.ApplyConfiguration(new UserLoginEntityTypeConfiguration());
            b.ApplyConfiguration(new UserRoleEntityTypeConfiguration());
            b.ApplyConfiguration(new UserTokenEntityTypeConfiguration());

            b.ApplyConfiguration(new UserSubscriberEntityTypeConfiguration());

            #endregion
        }

        public async Task<bool> SaveChangesWithDispatchDomainEvents(CancellationToken cancellationToken = default(CancellationToken))
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
