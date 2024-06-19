using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Support.Commands.CreateReport {
    internal class CreateReportCommandValidator: AbstractValidator<CreateReportCommand> {
        public CreateReportCommandValidator() {
            RuleFor(createReportCommand => createReportCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(createReportCommand => createReportCommand.ReportMessage).NotEmpty()
                .MinimumLength(10)
                .WithName("Сообщение для тех.поддержки")
                .MaximumLength(1000);
            RuleFor(createReportCommand => createReportCommand.ReasonId).NotEmpty().InclusiveBetween(1, 6);
        }
    }
}
