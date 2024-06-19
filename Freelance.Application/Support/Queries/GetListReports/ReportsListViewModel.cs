using Freelance.Application.UserProfiles.ApplicationUsers.Queries.GetImplementerList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Support.Queries.GetListReports {
    public class ReportsListViewModel {
        public IList<ReportLookupDto> Reports { get; set; }

        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
