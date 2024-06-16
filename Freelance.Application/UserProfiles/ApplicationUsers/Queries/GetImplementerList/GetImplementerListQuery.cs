using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetImplementerList {
    public class GetImplementerListQuery : IRequest<ImplementerListViewModel> {
        public string? Search { get; set; } = string.Empty;
        public string Categories { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
