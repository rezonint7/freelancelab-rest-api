using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Support.Commands.CreateReport {
    internal class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, int> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;
        public CreateReportCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<int> Handle(CreateReportCommand request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
            var reason = await _freelanceDBContext.ReasonsToReport.FindAsync(request.ReasonId, cancellationToken);
            var status = await _freelanceDBContext.Statuses.FirstOrDefaultAsync(status => status.Id == "open");

            var report = new ReportToSupport {
                User = user,
                Reason = reason,
                ReportMessage = request.ReportMessage,
                CreatedAt = DateTime.Now,
                Status = status,
            };

            await _freelanceDBContext.ReportsToSupport.AddAsync(report, cancellationToken);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return report.Id;
        }
    }
}
