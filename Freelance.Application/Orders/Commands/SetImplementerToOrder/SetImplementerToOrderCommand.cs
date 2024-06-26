﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Commands.SetImplementerToOrder
{
    public class SetImplementerToOrderCommand : IRequest<Unit>
    {
        public Guid CustomerId { get; set; }
        public Guid ImplementerId { get; set; }
        public Guid OrderId { get; set; }
    }
}
