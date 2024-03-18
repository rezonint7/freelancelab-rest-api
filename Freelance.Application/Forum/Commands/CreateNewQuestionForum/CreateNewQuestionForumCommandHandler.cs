using Freelance.Application.Common.Exceptions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Commands.CreateNewQuestionForum {
    internal class CreateNewQuestionForumCommandHandler : IRequestHandler<CreateNewQuestionForumCommand, int> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IUserService _userService;

        public CreateNewQuestionForumCommandHandler(IFreelanceDBContext freelanceDBContext, IUserService userService) {
            _freelanceDBContext = freelanceDBContext;
            _userService = userService;
        }

        public async Task<int> Handle(CreateNewQuestionForumCommand request, CancellationToken cancellationToken) {
            var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);

            if (user == null) { throw new NotFoundException(nameof(ApplicationUser), request.UserId); }

            var questionForum = new QuestionForum {
                Title = request.Title,
                Content = request.Content,
                Tags = request.Tags,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Likes = 0,
                User = user
            };

            await _freelanceDBContext.QuestionsForum.AddAsync(questionForum, cancellationToken);
            await _freelanceDBContext.SaveChangesAsync(cancellationToken);
            return questionForum.Id;
        }
    }
}
