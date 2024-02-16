using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser> {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder) {
            builder.Property(userInfo => userInfo.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(userInfo => userInfo.LastName).IsRequired().HasMaxLength(200);
            builder.Property(userInfo => userInfo.MiddleName).HasMaxLength(200);
            builder.Property(userInfo => userInfo.Birthday);
            builder.Property(userInfo => userInfo.RegisterDate);
            builder.Property(userInfo => userInfo.Rating);
            builder.Property(userInfo => userInfo.About).HasMaxLength(3000);
            builder.Property(userInfo => userInfo.AvatarProfilePath).HasMaxLength(500);
            builder.Property(userInfo => userInfo.HeaderProfilePath).HasMaxLength(500);
        }
    }
}
