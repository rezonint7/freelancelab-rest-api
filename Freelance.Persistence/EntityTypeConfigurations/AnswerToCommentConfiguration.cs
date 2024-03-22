using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class AnswerToCommentConfiguration : IEntityTypeConfiguration<AnswerToComment> {
        public void Configure(EntityTypeBuilder<AnswerToComment> builder) {
            builder.HasKey(comment => comment.Id);
            builder.HasIndex(comment => comment.Id).IsUnique();
            builder.Property(comment => comment.AnswerMessage).HasMaxLength(1000).IsRequired();
            builder.Property(comment => comment.CreatedAt).IsRequired();
            builder.Property(comment => comment.UpdatedAt);

            builder.HasOne(comment => comment.User)
              .WithMany()
              .HasForeignKey(comment => comment.UserId);
        }
    }
}
