﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.ResponsesCustomerOrders.Commands.DeleteImplementerFromOrder {
    public class DeleteImplementerFromOrderCommand : IRequest<Unit> {
        public Guid CustomerId { get; set; }
        public Guid ImplementerId { get; set; }
        public Guid OrderId { get; set; }
    }
}
