using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Freelance.Application.Common.Exceptions {
    public class UserAlreadyExistsException: Exception {
        public UserAlreadyExistsException(string login, object key): base($"User: {login} key: {key} already exists") {}
    }
}
