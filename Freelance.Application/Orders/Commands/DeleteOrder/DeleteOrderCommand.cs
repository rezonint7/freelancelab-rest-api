using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Commands.DeleteOrder {
    public class DeleteOrderCommand: IRequest<Unit> {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
