using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Freelance.Domain;

namespace Freelance.Application.Interfaces {
    public interface IFreelanceDBContext {
        DbSet<Customer> Customers { get; set; }
        DbSet<Implementer> Implementers { get; set; }
        DbSet<UserWarning> UserWarnings { get; set; }

        DbSet<Order> Orders { get; set; }
        DbSet<Status> OrderStatuses { get; set; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<PortfolioItem> PortfolioItems { get; set; }
        DbSet<WorkExperience> WorkExperience { get; set; }
        DbSet<ResponseImplementer> ResponsesImplementer { get; set; }

        DbSet<QuestionForum> QuestionsForum { get; set; }
        DbSet<CommentToQuestionForum> CommentsToQuestions { get; set; }
        DbSet<AnswerToComment> AnswerToComments { get; set; }

        DbSet<Chat> Chats { get; set; }
        DbSet<ChatMessage> ChatMessages { get; set; }
        
        DbSet<Feedback> Feedbacks { get; set; }
        
        DbSet<ReasonToReport> ReasonsToReport { get; set; }
        DbSet<ReportToSupport> ReportsToSupport { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
