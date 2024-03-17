using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using Freelance.Persistence.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Freelance.Persistence {
    public class FreelanceDBContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IFreelanceDBContext {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Implementer> Implementers { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<WorkExperience> WorkExperience { get; set; }
        public DbSet<UserWarning> UserWarnings { get; set; }
        public DbSet<ResponseImplementer> ResponsesImplementer { get; set; }
        public DbSet<QuestionForum> QuestionsForum { get; set; }
        public DbSet<CommentToQuestionForum> CommentsToQuestions { get; set; }
        public DbSet<AnswerToComment> AnswerToComments { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<ReasonToReport> ReasonsToReport { get; set; }
        public DbSet<ReportToSupport> ReportsToSupport { get; set; }

        public FreelanceDBContext(DbContextOptions<FreelanceDBContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ImplementerConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PortfolioItemConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new WorkExperienceConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionForumConfiguration());
            modelBuilder.ApplyConfiguration(new CommentToQuestionForumConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerToCommentConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
            modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
            modelBuilder.ApplyConfiguration(new ReasonToReportConfiguration());
            modelBuilder.ApplyConfiguration(new ReportToSupportConfiguration());
            modelBuilder.ApplyConfiguration(new ResponseImplementerConfiguration());
            modelBuilder.ApplyConfiguration(new UserWarningConfiguration());
            modelBuilder.ApplyConfiguration(new StatusConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
