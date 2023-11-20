using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WillsPlatform.Domains.Entities;

namespace WillsPlatform.Infrastructure.Configurations
{
    public class TokenEntityConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(f => f.Text)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.HasOne(f => f.Template)
                   .WithMany(f => f.Tokens)
                   .HasForeignKey(f => f.TemplateId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(f => f.Question)
                    .WithMany()
                    .HasForeignKey(f=>f.QuestionId)
                    .OnDelete(DeleteBehavior.NoAction);            
        }
    }
}
