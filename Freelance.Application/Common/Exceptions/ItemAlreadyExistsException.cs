using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Common.Exceptions {
	public class ItemAlreadyExistsException: Exception {
        public ItemAlreadyExistsException(string name, string userName): base($"{name} already exists by user: {userName}") { }
    }
}
