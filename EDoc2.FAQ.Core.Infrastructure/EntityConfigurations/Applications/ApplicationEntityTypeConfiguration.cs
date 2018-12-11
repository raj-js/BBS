using EDoc2.FAQ.Core.Domain.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Applications
{
    class ApplicationEntityTypeConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> b)
        {
            b.HasKey(e => e.Id);

            b.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            b.Property(e => e.Version)
                .IsRequired()
                .HasMaxLength(50);

            b.Property(e => e.IconBase64);

            b.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(256);

            b.HasMany(e => e.Settings)
                .WithOne(e => e.Application)
                .HasForeignKey(e => e.ApplicationId)
                .IsRequired();
        }
    }
}
