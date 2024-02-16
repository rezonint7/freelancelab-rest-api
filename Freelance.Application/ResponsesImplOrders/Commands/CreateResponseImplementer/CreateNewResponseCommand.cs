using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.ResponsesImplOrders.Commands.CreateResponseImplementer {
    public class CreateNewResponseCommand : IRequest<Unit> {
        public Guid ImplementerId { get; set; }
        public Guid OrderId { get; set; }
        public string ResponseMessage { get; set; }
    }
}
