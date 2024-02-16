using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Admin.Commands.DeleteAdmin {
    internal class DeleteAdminCommandValidator: AbstractValidator<DeleteAdminCommand>{
        public DeleteAdminCommandValidator() {
            RuleFor(deleteAdm => deleteAdm.AdminId).NotEqual(Guid.Empty);
        }
    }
}
