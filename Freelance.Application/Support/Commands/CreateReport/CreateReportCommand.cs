using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Support.Commands.CreateReport {
    public class CreateReportCommand: IRequest<int> {
        public Guid UserId { get; set; }
        public string ReportMessage { get; set; }
        public int ReasonId { get; set; }
    }
}
