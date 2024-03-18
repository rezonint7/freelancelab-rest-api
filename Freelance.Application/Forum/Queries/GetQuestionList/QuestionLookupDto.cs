using Freelance.Application.Common.Mapping;
using Freelance.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance.Application.Forum.Queries.GetQuestionList {
    internal class QuestionLookupDto: IMapWith<QuestionForum> {
        public Guid QuestionId { get; set; }
        public string Title { get; set; }

    }
}
