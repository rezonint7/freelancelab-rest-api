﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Queries.GetQuestionDetails {
    public class GetQuestionDetailsQuery: IRequest<QuestionDetailsViewModel> {
        public int QuestionId { get; set; }
    }
}
