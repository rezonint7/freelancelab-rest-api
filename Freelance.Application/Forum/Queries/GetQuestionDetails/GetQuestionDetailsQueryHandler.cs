using AutoMapper;
using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Freelance.Application.Forum.Queries.GetQuestionDetails {
    internal class GetQuestionDetailsQueryHandler : IRequestHandler<GetQuestionDetailsQuery, QuestionDetailsViewModel> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IMapper _mapper;

        public GetQuestionDetailsQueryHandler(IFreelanceDBContext freelanceDBContext, IMapper mapper) {
            _freelanceDBContext = freelanceDBContext;
            _mapper = mapper;
        }

        public async Task<QuestionDetailsViewModel> Handle(GetQuestionDetailsQuery request, CancellationToken cancellationToken) {
            var question = await _freelanceDBContext.QuestionsForum
                .Include(i => i.User)
                .Include(i => i.Comments)
                .ThenInclude(i => i.User)
                .Include(i => i.Comments)
                .ThenInclude(i => i.Answers)
                .ThenInclude(i => i.User)
                .FirstOrDefaultAsync(question => question.Id == request.QuestionId, cancellationToken);

            if (question == null) {
                throw new NotFoundException(nameof(QuestionForum), request.QuestionId);
            }

            return _mapper.Map<QuestionDetailsViewModel>(question);
        }
    }
}
