using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LingEdu.Exercises.Application.Common;
using LingEdu.Exercises.Application.Exercises.GetExercise;
using LingEdu.Exercises.Application.Exercises.GetForToday;
using LingEdu.Exercises.Application.Exercises.SubmitSolution;
using LingEdu.WebApi.Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingEdu.WebApi.Controllers
{
    [Route("api/exercises")]
    [ApiController]
    [Authorize]
    public class ExercisesController : ControllerBase
    {
        private readonly ISender _sender;

        public ExercisesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("for-today")]
        public async Task<IActionResult> GetForToday()
        {
            var result = await _sender.Send(new GetExercisesForTodayQuery());
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(new GetExerciseQuery(userId, id));

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return result.Error switch
            {
                ExerciseErrors.PremiumRequired => Forbid(),
                ExerciseErrors.ExerciseNotFound => NotFound(),
                ExerciseErrors.UserNotFound => Unauthorized(),
                _ => BadRequest(result.Error)
            };
        }

        [HttpPost("{id}/submit")]
        public async Task<IActionResult> Submit(Guid id, [FromBody] SubmitExerciseRequest request)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var answers = request.Answers
                .Select(a => new SubmitSolutionAnswer(a.QuestionId, a.SelectedOptionId))
                .ToList();

            var result = await _sender.Send(new SubmitSolutionCommand(userId, id, answers));

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return result.Error switch
            {
                ExerciseErrors.PremiumRequired => Forbid(),
                ExerciseErrors.ExerciseNotFound => NotFound(),
                ExerciseErrors.UserNotFound => Unauthorized(),
                _ => BadRequest(result.Error)
            };
        }
    }
}
