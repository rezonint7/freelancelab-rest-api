using Freelance.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Commands.CreateOrder {
    public class CreateNewOrderCommand : IRequest<Guid> {
        public Guid CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public decimal ProjectFee { get; set; }
        public string? Tags { get; set; }
        public bool IsUrgent { get; set; }
        public int CurrencyId { get; set; }
        public int CategoryId { get; set; }


    }
}
