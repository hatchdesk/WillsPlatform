using Domains.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Infrastructure.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Text)
                   .IsRequired();

            builder.HasOne(f => f.Form)
                   .WithMany(f => f.Questions)
                   .HasForeignKey(f => f.FormId);

            builder.HasOne(f => f.Field)
                   .WithMany()
                   .HasForeignKey(f => f.FieldId);
        }
    }
}
