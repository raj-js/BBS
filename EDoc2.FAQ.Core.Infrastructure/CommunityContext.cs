using EDoc2.FAQ.Core.Domain.Accounts;
using EDoc2.FAQ.Core.Domain.Uow;
using EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Accounts;
using EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Articles;
using EDoc2.FAQ.Core.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Threading;
using System.Threading.Tasks;
using EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Applications;
using EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Categories;

namespace EDoc2.FAQ.Core.Infrastructure
{
    public class CommunityContext : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IUnitOfWork
    {
        public Guid UniqueId { get; set; } = Guid.NewGuid();

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
            b.ApplyConfiguration(new ArticlePropertyEntityTypeConfiguration());
            b.ApplyConfiguration(new ArticleOperationEntityTypeConfiguration());
            b.ApplyConfiguration(new ArticleTopEntityTypeConfiguration());

            #endregion

            #region 会员相关

            b.ApplyConfiguration(new UserEntityTypeConfiguration());
            b.ApplyConfiguration(new UserLoginEntityTypeConfiguration());
            b.ApplyConfiguration(new UserRoleEntityTypeConfiguration());
            b.ApplyConfiguration(new UserTokenEntityTypeConfiguration());
            b.ApplyConfiguration(new UserSubscriberEntityTypeConfiguration());
            b.ApplyConfiguration(new UserClaimEntityTypeConfiguration());
            b.ApplyConfiguration(new UserPropertyEntityTypeConfiguration());
            b.ApplyConfiguration(new UserScoreHistoryEntityTypeConfiguration());

            #endregion

            #region 类别相关

            b.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            b.ApplyConfiguration(new CategoryModeratorEntityTypeConfiguration());

            #endregion

            #region 系统相关

            b.ApplyConfiguration(new ApplicationEntityTypeConfiguration());
            b.ApplyConfiguration(new ApplicationSettingEntityTypeConfiguration());

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
