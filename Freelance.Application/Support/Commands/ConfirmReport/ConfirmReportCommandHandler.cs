using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Support.Commands.ConfirmReport {
    internal class ConfirmReportCommandHandler : IRequestHandler<ConfirmReportCommand, Unit> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;
        public ConfirmReportCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<Unit> Handle(ConfirmReportCommand request, CancellationToken cancellationToken) {
            var report = await _freelanceDBContext.ReportsToSupport.Include(i => i.User).FirstOrDefaultAsync(i => i.Id == request.ReportId, cancellationToken);
            if (report == null) {
                throw new NotFoundException(nameof(ReportToSupport), request.ReportId.ToString());
            }
            var supportAgent = await _userService.GetUserByIdAsync(request.SupportId, cancellationToken);
            if (supportAgent == null) {
                throw new NotFoundException(nameof(ApplicationUser), request.SupportId.ToString());
            }

            var status = await _freelanceDBContext.Statuses.FirstOrDefaultAsync(i => i.Id == "in_progress");
            if (status == null) {
                throw new NotFoundException(nameof(Status), "in_progress");
            }

            report.Status = status;
            var chat = new Chat {
                Name = "Обращение в тех.поддержку #" + report.Id,
                AdminId = supportAgent.Id,
                Users = new List<ApplicationUser> { report.User, supportAgent }
            };
            await _freelanceDBContext.Chats.AddAsync(chat);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
