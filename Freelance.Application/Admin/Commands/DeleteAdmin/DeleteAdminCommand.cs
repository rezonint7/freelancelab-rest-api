using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Admin.Commands.DeleteAdmin {
    public class DeleteAdminCommand : IRequest<Unit> {
        public Guid AdminId { get; set; }
    }
}
