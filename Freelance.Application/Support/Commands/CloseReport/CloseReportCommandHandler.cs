using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Application.Support.Commands.ConfirmReport;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Support.Commands.CloseReport {
    internal class CLoseReportCommandHandler : IRequestHandler<CloseReportCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        public CLoseReportCommandHandler(IFreelanceDBContext freelanceDBContext) {
            _freelanceDBContext = freelanceDBContext;
        }

        public async Task<Unit> Handle(CloseReportCommand request, CancellationToken cancellationToken) {
            var report = await _freelanceDBContext.ReportsToSupport.FirstOrDefaultAsync(i => i.Id == request.ReportId, cancellationToken);
            if (report == null) {
                throw new NotFoundException(nameof(ReportToSupport), request.ReportId.ToString());
            }
            var chat = await _freelanceDBContext.Chats.FirstOrDefaultAsync(i => i.Name == ("Обращение в тех.поддержку #" + report.Id), cancellationToken);
            if (chat != null) {
                _freelanceDBContext.Chats.Remove(chat);
            }
            var status = await _freelanceDBContext.Statuses.FirstOrDefaultAsync(i => i.Id == "archived");
            if (status == null) {
                throw new NotFoundException(nameof(Status), "archived");
            }

            report.Status = status;
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
