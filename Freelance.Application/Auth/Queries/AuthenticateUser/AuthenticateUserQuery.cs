using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Auth.Queries.AuthenticateUser {
    public class AuthenticateUserQuery: IRequest<string>{
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
