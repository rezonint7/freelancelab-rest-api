using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Admin.Commands.UpdateAdmin {
    public class UpdateAdminCommand: IRequest<Unit> {
        public Guid AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
    }
}
