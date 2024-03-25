using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Commands.CompleteOrder
{
    public class CompleteOrderCommand : IRequest<Unit>
    {
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
    }
}
