using EDoc2.FAQ.Core.Domain.Authorization;
using EDoc2.FAQ.Core.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EDoc2.FAQ.Core.Repositories.EntityFrameworkCore.Contexts
{
    public class AppIdentityContext : IdentityDbContext<User,
        Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options) { }

        public AppIdentityContext(DbContextOptions<AppIdentityContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsForIdentityAsync(this);

            await SaveChangesAsync(cancellationToken);

            return true;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

    public class AppIdentityContextDbFactory : IDesignTimeDbContextFactory<AppIdentityContext>
    {
        public AppIdentityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppIdentityContext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectsV13;Initial Catalog=EDoc2.FAQ.AppIdentity;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new AppIdentityContext(optionsBuilder.Options);
        }
    }
}
