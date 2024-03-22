using AutoMapper;
using Freelance.Application.Forum.Commands.CreateNewAnswerToComment;
using Freelance.Application.Forum.Commands.CreateNewCommentToQuestion;
using Freelance.Application.Forum.Commands.CreateNewQuestionForum;
using Freelance.Application.Forum.Commands.DeleteAnswerToComment;
using Freelance.Application.Forum.Commands.DeleteCommentToQuestion;
using Freelance.Application.Forum.Commands.DeleteQuestionForum;
using Freelance.Application.Forum.Commands.LikeToQuestion;
using Freelance.Application.Forum.Commands.UpdateAnswerToComment;
using Freelance.Application.Forum.Commands.UpdateCommentToQuestion;
using Freelance.Application.Forum.Commands.UpdateQuestionForum;
using Freelance.Application.Forum.Queries.GetQuestionDetails;
using Freelance.Application.Forum.Queries.GetQuestionList;
using Freelance.Application.Orders.Commands.CreateOrder;
using Freelance.Application.Orders.Commands.DeleteOrder;
using Freelance.WebApi.Models.Forum;
using Freelance.WebApi.Models.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.WebApi.Controllers.API
{
    [Route("api/forum")]
    public class ForumController : BaseController {
        private readonly IMapper _mapper;
        public ForumController(IMapper mapper) => _mapper = mapper;

        #region Questions

        [HttpGet("questions")]
        public async Task<ActionResult<QuestionListViewModel>> GetAllQuestions(string? search = "", int pageSize = 20, int page = 1) {
            var query = new GetQuestionListQuery {
                Search = search,
                Page = page,
                PageSize = pageSize
            };

            var viewModel = await Mediator.Send(query);
            return Ok(viewModel);
        }

        //[HttpGet("question/details/{id}")]
        //public async Task<ActionResult<QuestionDetailsViewModel>> GetDetailsQuestion(int id) {
        //    var query = new GetQuestionDetailsQuery { QuestionId = id };

        //    var viewModel = await Mediator.Send(query);
        //    return Ok(viewModel);
        //}

        [HttpPost("question/create")]
        public async Task<ActionResult<int>> CreateNewQuestion([FromBody] CreateQuestionDto createQuestionDto) {
            var command = _mapper.Map<CreateNewQuestionForumCommand>(createQuestionDto);
            command.UserId = UserId;

            var questionId = await Mediator.Send(command);

            return Created($"{questionId}", questionId);
        }

        [HttpPut("question/update")]
        public async Task<ActionResult<Unit>> UpdateQuestion([FromBody] UpdateQuestionDto updateQuestionDto) {
            var command = _mapper.Map<UpdateQuestionForumCommand>(updateQuestionDto);
            command.UserId = UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("question/delete/{id}")]
        public async Task<ActionResult<Unit>> DeleteQuestion(int id) {
            var command = new DeleteQuestionForumCommand {
                QuestionId = id,
                UserId = UserId
            };
            await Mediator.Send(command);

            return NoContent();
        }

        #endregion

        #region Comments

        [HttpPost("question/comment/create")]
        public async Task<ActionResult<int>> CreateNewComment([FromBody] CreateCommentDto createCommentDto) {
            var command = _mapper.Map<CreateNewCommentToQuestionCommand>(createCommentDto);
            command.UserId = UserId;

            var commentId = await Mediator.Send(command);

            return Created($"{commentId}", commentId);
        }

        [HttpPut("question/comment/update")]
        public async Task<ActionResult<Unit>> UpdateComment([FromBody] UpdateCommentDto updateCommentDto) {
            var command = _mapper.Map<UpdateCommentToQuestionCommand>(updateCommentDto);
            command.UserId = UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("question/comment/delete/{id}")]
        public async Task<ActionResult<Unit>> DeleteComment(int id) {
            var command = new DeleteCommentToQuestionCommand {
                CommentId = id,
                UserId = UserId
            };
            await Mediator.Send(command);

            return NoContent();
        }

        #endregion

        #region Answers

        [HttpPost("question/comment/answer/create")]
        public async Task<ActionResult<int>> CreateNewAnswer([FromBody] CreateAnswerDto createAnswerDto) {
            var command = _mapper.Map<CreateNewAnswerToCommentCommand>(createAnswerDto);
            command.UserId = UserId;

            var answerId = await Mediator.Send(command);

            return Created($"{answerId}", answerId);
        }

        [HttpPut("question/comment/answer/update")]
        public async Task<ActionResult<Unit>> UpdateAnswer([FromBody] UpdateAnswerDto updateAnswerDto) {
            var command = _mapper.Map<UpdateAnswerToCommentCommand>(updateAnswerDto);
            command.UserId = UserId;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("question/comment/answer/delete/{id}")]
        public async Task<ActionResult<Unit>> DeleteAnswer(int id) {
            var command = new DeleteAnswerToCommentCommand {
                AnswerId = id,
                UserId = UserId
            };
            await Mediator.Send(command);

            return NoContent();
        }

        #endregion

        #region Likes

        [HttpPatch("question/{id}/like")]
        public async Task<ActionResult<Unit>> LikeToQuestion(int id) {
            var command = new LikeToQuestionCommand {
                QuestionId = id,
                UserId = UserId
            };
            await Mediator.Send(command);

            return NoContent();
        }

        #endregion
    }
}
