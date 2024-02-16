using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Interfaces {
    public interface IJwtService {
        public Task<string> GenerateJwtToken(ApplicationUser user, int expires);
    }
}
