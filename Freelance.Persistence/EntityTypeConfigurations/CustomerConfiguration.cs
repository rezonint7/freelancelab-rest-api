using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer> {
        public void Configure(EntityTypeBuilder<Customer> builder) {
            builder.HasKey(customer => customer.UserId);
            builder.HasIndex(customer => customer.UserId).IsUnique();
            builder.Property(customer => customer.IsTrusted);

            builder.HasOne(customer => customer.User)
                .WithMany(applicationUser => applicationUser.Customers)
                .HasForeignKey(customer => customer.UserId);
        }
    }
}
