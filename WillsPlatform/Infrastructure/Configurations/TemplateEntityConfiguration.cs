using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WillsPlatform.Domains.Entities;

namespace WillsPlatform.Infrastructure.Configurations
{
    public class TemplateEntityConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Text)
                   .IsRequired();

            builder.HasOne(f => f.Form)
                   .WithMany(f => f.Templates)
                   .HasForeignKey(f => f.FormId);
        }
    }
}
