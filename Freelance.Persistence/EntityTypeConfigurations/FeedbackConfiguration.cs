using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class FeedbackConfiguration : IEntityTypeConfiguration<Feedback> {
        public void Configure(EntityTypeBuilder<Feedback> builder) {
            builder.HasKey(feedback => feedback.Id);
            builder.HasIndex(feedback => feedback.Id).IsUnique();
            builder.Property(feedback => feedback.FeedbackMessage).HasMaxLength(1000);
            builder.Property(feedback => feedback.FeedbackRating).IsRequired();
            builder.Property(feedback => feedback.CreatedAt).IsRequired();
            builder.Property(feedback => feedback.UpdatedAt);

            builder.HasOne(feedback => feedback.User)
              .WithMany()
              .HasForeignKey(feedback => feedback.UserId);
        }
    }
}
