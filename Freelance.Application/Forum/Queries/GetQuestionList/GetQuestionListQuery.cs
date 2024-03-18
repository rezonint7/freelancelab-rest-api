using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Queries.GetQuestionList {
    internal class GetQuestionListQuery: IRequest<QuestionListViewModel> {
        public string? Search { get; set; } = string.Empty;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;

    }
}
