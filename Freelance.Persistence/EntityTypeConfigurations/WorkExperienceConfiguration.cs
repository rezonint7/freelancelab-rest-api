using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class WorkExperienceConfiguration : IEntityTypeConfiguration<WorkExperience> {
        public void Configure(EntityTypeBuilder<WorkExperience> builder) {
            builder.HasKey(work => work.Id);
            builder.HasIndex(work => work.Id).IsUnique();
            builder.Property(work => work.Name).HasMaxLength(300);
        }
    }
}
