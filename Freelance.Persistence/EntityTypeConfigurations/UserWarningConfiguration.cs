using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class UserWarningConfiguration : IEntityTypeConfiguration<UserWarning> {
        public void Configure(EntityTypeBuilder<UserWarning> builder) {
            builder.HasKey(response => response.Id);
            builder.HasIndex(response => response.Id).IsUnique();
            builder.Property(response => response.MessageWarning).HasMaxLength(1000).IsRequired();
            builder.Property(response => response.CreatedAt).IsRequired();

            builder.HasOne(report => report.User)
             .WithMany()
             .HasForeignKey(report => report.UserId);

            builder.HasOne(report => report.ReasonWarning)
              .WithMany()
              .HasForeignKey(report => report.ReasonWarningId);
        }
    }
}
