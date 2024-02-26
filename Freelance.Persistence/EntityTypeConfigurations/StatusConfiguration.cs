using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class StatusConfiguration : IEntityTypeConfiguration<Status> {
        public void Configure(EntityTypeBuilder<Status> builder) {
            builder.HasKey(currency => currency.Id);
            builder.HasIndex(currency => currency.Id).IsUnique();
            builder.Property(currency => currency.Name).HasMaxLength(200);
        }
    }
}
