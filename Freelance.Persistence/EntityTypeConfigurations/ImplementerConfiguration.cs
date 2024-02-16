using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class ImplementerConfiguration : IEntityTypeConfiguration<Implementer> {
        public void Configure(EntityTypeBuilder<Implementer> builder) {
            builder.HasKey(inplementer => inplementer.UserId);
            builder.HasIndex(inplementer => inplementer.UserId).IsUnique();
            builder.Property(inplementer => inplementer.Specialization).HasMaxLength(500);
            builder.Property(inplementer => inplementer.Skills).HasMaxLength(700);

            builder.HasOne(implementer => implementer.User)
                .WithMany(applicationUser => applicationUser.Implementers)
                .HasForeignKey(implementer => implementer.UserId);

            builder.HasOne(implementer => implementer.Category)
               .WithMany()
               .HasForeignKey(implementer => implementer.CategoryId);

            builder.HasOne(implementer => implementer.WorkExperience)
               .WithMany()
               .HasForeignKey(implementer => implementer.WorkExperienceId);
        }
    }
}
