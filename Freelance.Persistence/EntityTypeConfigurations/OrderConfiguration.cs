using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Freelance.Domain;

namespace Freelance.Persistence.EntityTypeConfigurations {
    public class OrderConfiguration : IEntityTypeConfiguration<Order> {
        public void Configure(EntityTypeBuilder<Order> builder) {
            builder.HasKey(order => order.OrderId);
            builder.HasIndex(order => order.OrderId).IsUnique();
            builder.Property(order => order.Title).IsRequired().HasMaxLength(300);
            builder.Property(order => order.Description).IsRequired().HasMaxLength(1000);
            builder.Property(order => order.Tags).HasMaxLength(700);
            builder.Property(order => order.CreatedAt).IsRequired();
            builder.Property(order => order.UpdatedAt);
            builder.Property(order => order.ProjectFee);
            builder.Property(order => order.IsUrgent);

            builder.HasOne(order => order.Category)
              .WithMany()
              .HasForeignKey(order => order.CategoryId);

            builder.HasOne(order => order.Status)
              .WithMany()
              .HasForeignKey(order => order.StatusId);

            builder.HasOne(order => order.Currency)
              .WithMany()
              .HasForeignKey(order => order.CurrencyId);

            builder.HasOne(order => order.Implementer)
            .WithMany()
            .HasForeignKey(order => order.ImplementerId)
            .IsRequired(false);
        }
    }
}
