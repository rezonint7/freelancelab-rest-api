using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Persistence {
    public class DbInitializer {
        public static void Initialize(FreelanceDBContext context) {
            context.Database.EnsureCreated();
        }
    }
}
