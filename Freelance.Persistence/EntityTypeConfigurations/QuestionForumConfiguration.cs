using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class QuestionForumConfiguration : IEntityTypeConfiguration<QuestionForum> {
        public void Configure(EntityTypeBuilder<QuestionForum> builder) {
            builder.HasKey(question => question.Id);
            builder.HasIndex(question => question.Id).IsUnique();
            builder.Property(question => question.Title).HasMaxLength(300).IsRequired();
            builder.Property(question => question.Content).HasMaxLength(5000).IsRequired();
            builder.Property(question => question.CreatedAt).IsRequired();
            builder.Property(question => question.UpdatedAt);
            builder.Property(question => question.LikesBy);

            builder.HasOne(question => question.User)
              .WithMany()
              .HasForeignKey(question => question.UserId);
        }
    }
}
