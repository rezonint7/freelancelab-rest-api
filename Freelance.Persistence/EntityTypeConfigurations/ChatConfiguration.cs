using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class ChatConfiguration : IEntityTypeConfiguration<Chat> {
        public void Configure(EntityTypeBuilder<Chat> builder) {
            builder.HasKey(chat => chat.Id);
            builder.HasIndex(chat => chat.Id).IsUnique();
            builder.Property(chat => chat.Name).HasMaxLength(300).IsRequired();
        }
    }
}
