using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class PortfolioItemConfiguration : IEntityTypeConfiguration<PortfolioItem> {
        public void Configure(EntityTypeBuilder<PortfolioItem> builder) {
            builder.HasKey(portfolio => portfolio.Id);
            builder.HasIndex(portfolio => portfolio.Id).IsUnique();
            builder.Property(portfolio => portfolio.Title).HasMaxLength(300).IsRequired();
            builder.Property(portfolio => portfolio.Description).HasMaxLength(1000);
            builder.Property(portfolio => portfolio.PhotoPath).HasMaxLength(1000);
            builder.Property(portfolio => portfolio.CreatedAt);

            builder.HasOne(portfolio => portfolio.Category)
             .WithMany()
             .HasForeignKey(portfolio => portfolio.CategoryId);

            builder.HasOne(portfolio => portfolio.Implementer)
             .WithMany()
             .HasForeignKey(portfolio => portfolio.ImplementerId);
        }
    }
}
