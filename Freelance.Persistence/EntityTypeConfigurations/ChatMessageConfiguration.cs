using Freelance.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence.EntityTypeConfigurations {
    internal class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage> {
        public void Configure(EntityTypeBuilder<ChatMessage> builder) {
            builder.HasKey(chat => chat.Id);
            builder.HasIndex(chat => chat.Id).IsUnique();
            builder.Property(chat => chat.Content).HasMaxLength(5000).IsRequired();

            builder.HasOne(chat => chat.Chat)
              .WithMany()
              .HasForeignKey(chat => chat.ChatId);

            builder.HasOne(chat => chat.User)
              .WithMany()
              .HasForeignKey(chat => chat.UserId);
        }
    }
}
