using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Admin.Queries.GetDetailsAdmin {
    internal class GetDetailsAdminQueryValidator: AbstractValidator<GetDetailsAdminQuery> {
        public GetDetailsAdminQueryValidator() => RuleFor(getAdminQuery => getAdminQuery.AdminId).NotEqual(Guid.Empty);

    }
}
