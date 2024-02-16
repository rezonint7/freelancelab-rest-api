using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class ReportToSupportConfiguration : IEntityTypeConfiguration<ReportToSupport> {
        public void Configure(EntityTypeBuilder<ReportToSupport> builder) {
            builder.HasKey(report => report.Id);
            builder.HasIndex(report => report.Id).IsUnique();
            builder.Property(report => report.ReportMessage).HasMaxLength(1000).IsRequired();
            builder.Property(report => report.CreatedAt).IsRequired();
            builder.Property(report => report.Status);

            builder.HasOne(report => report.User)
              .WithMany()
              .HasForeignKey(report => report.UserId);

            builder.HasOne(report => report.Reason)
              .WithMany()
              .HasForeignKey(report => report.ReasonId);
        }
    }
}
