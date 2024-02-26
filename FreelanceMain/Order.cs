using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Domain {
    public class Order {
        public Guid OrderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public decimal? ProjectFee { get; set; }
        public string StatusId { get; set; }
        public bool IsUrgent { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? ImplementerId { get; set; }
        public int CategoryId { get; set; }
        public int CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Category Category { get; set; }
        public virtual Status Status { get; set; }
        public virtual Implementer? Implementer { get; set; }
        public ICollection<ResponseImplementer> Responses { get; set; } = new HashSet<ResponseImplementer>();
    }
}
