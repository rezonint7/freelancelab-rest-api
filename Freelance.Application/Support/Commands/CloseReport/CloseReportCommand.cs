using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Support.Commands.CloseReport {
    public class CloseReportCommand: IRequest<Unit> {
        public int ReportId {get; set; }
    }
}
