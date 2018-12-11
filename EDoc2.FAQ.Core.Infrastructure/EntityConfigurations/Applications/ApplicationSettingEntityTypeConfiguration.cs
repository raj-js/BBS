using EDoc2.FAQ.Core.Domain.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDoc2.FAQ.Core.Infrastructure.EntityConfigurations.Applications
{
    class ApplicationSettingEntityTypeConfiguration: IEntityTypeConfiguration<ApplicationSetting>
    {
        public void Configure(EntityTypeBuilder<ApplicationSetting> b)
        {
            b.HasKey(e => e.Id);

            b.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            b.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            b.Property(e => e.Value)
                .IsRequired();

            b.Property(e => e.Description)
                .IsRequired(false)
                .HasMaxLength(256);
        }
    }
}
