using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class ResponseImplementerConfiguration : IEntityTypeConfiguration<ResponseImplementer> {
        public void Configure(EntityTypeBuilder<ResponseImplementer> builder) {
            builder.HasKey(response => response.Id);
            builder.HasIndex(response => response.Id).IsUnique();
            builder.Property(response => response.ResponseMessage).HasMaxLength(2000).IsRequired();
            builder.Property(response => response.CreatedAt).IsRequired();
            builder.Property(response => response.UpdatedAt);

            builder.HasOne(response => response.Order)
              .WithMany()
              .HasForeignKey(response => response.OrderId);

            builder.HasOne(response => response.Implementer)
              .WithMany()
              .HasForeignKey(response => response.ImplementerId);
        }
    }
}
