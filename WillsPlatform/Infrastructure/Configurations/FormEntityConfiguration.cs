using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class FormConfiguration :IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder) 
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                   .IsRequired()
                   .HasMaxLength(500);
        }
    }
}
