using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class ReasonToReportConfiguration : IEntityTypeConfiguration<ReasonToReport> {
        public void Configure(EntityTypeBuilder<ReasonToReport> builder) {
            builder.HasKey(reason => reason.Id);
            builder.HasIndex(reason => reason.Id).IsUnique();
            builder.Property(reason => reason.Name).HasMaxLength(300).IsRequired();
        }
    }
}
