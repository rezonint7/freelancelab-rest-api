using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Admin.Queries.GelAllAdmins {
    public class GetAllAdminsQuery: IRequest<AdminListViewModel> { }
}
