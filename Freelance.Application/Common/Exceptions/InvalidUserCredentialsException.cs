using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Common.Exceptions {
    public class InvalidUserCredentialsException: Exception {
        public InvalidUserCredentialsException(): base($"Wrong login or password") { }
    }
}
