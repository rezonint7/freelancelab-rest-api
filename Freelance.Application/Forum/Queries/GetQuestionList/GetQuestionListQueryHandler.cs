using AutoMapper;
using AutoMapper.QueryableExtensions;
using Freelance.Application.Interfaces;
using Freelance.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Queries.GetQuestionList {
    internal class GetQuestionListQueryHandler : IRequestHandler<GetQuestionListQuery, QuestionListViewModel> {
        private readonly IFreelanceDBContext _freelanceDBContext;
        private readonly IMapper _mapper;

        public GetQuestionListQueryHandler(IFreelanceDBContext freelanceDBContext, IMapper mapper){
            _freelanceDBContext = freelanceDBContext;
            _mapper = mapper;
        }

        public async Task<QuestionListViewModel> Handle(GetQuestionListQuery request, CancellationToken cancellationToken) {
            IQueryable<QuestionForum> questionsForum = _freelanceDBContext.QuestionsForum;
            if (!string.IsNullOrEmpty(request.Search)) {
                questionsForum = questionsForum.Where(order => order.Title.ToLower().Contains(request.Search.ToLower()));
            }

            int totalItems = await questionsForum.CountAsync(cancellationToken);
            int totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

            questionsForum = questionsForum
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            var questions = await questionsForum
                .ProjectTo<QuestionLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new QuestionListViewModel {
                Questions = questions,
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}
