using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Orders.Commands.DeleteResponseImplementer
{
    public class DeleteResponseCommand : IRequest<Unit>
    {
        public Guid ResponseId { get; set; }
        public Guid ImplementerId { get; set; }
    }
}
