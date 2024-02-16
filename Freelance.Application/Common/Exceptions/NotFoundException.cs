using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Common.Exceptions {
    public class NotFoundException: Exception {
        public NotFoundException(string name, object key): base($"Entity: {name} key: {key} not found exception") { }
        public NotFoundException(string login): base($"User: {login} not found exception") { }
    }
}
