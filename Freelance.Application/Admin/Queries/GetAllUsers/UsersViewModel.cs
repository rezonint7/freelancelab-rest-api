using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Admin.Queries.GetAllUsers {
    public class UsersViewModel {
        public IList<UserLookupDto> Customers { get; set; }
        public IList<UserLookupDto> Implementers { get; set; }
    }
}
