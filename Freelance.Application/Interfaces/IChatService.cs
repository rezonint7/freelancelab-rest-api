using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Interfaces {
    public interface IChatService {
        public Task<Guid> CreateNewChatAsync(Guid implementerId, Guid customerId, Guid orderId, CancellationToken cancellationToken);
    }
}
