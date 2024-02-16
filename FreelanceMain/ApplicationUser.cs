using Microsoft.AspNetCore.Identity;

namespace Freelance.Domain {
    public class ApplicationUser: IdentityUser<Guid> {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? About { get; set; }
        public double Rating { get; set; } = 0.0;
        public DateTime? Birthday { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string? AvatarProfilePath { get; set; }
        public string? HeaderProfilePath { get; set; }

        public virtual ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
        public virtual ICollection<Implementer> Implementers { get; set; } = new HashSet<Implementer>();
        public virtual ICollection<UserWarning> UserWarnings { get; set; } = new HashSet<UserWarning>();
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new HashSet<Feedback>();
        public virtual ICollection<Chat> Chats { get; set; } = new HashSet<Chat>();
    }
}
