using System;
using System.Security.Claims;
using System.Threading.Tasks;
using LingEdu.Contests.Application.Common;
using LingEdu.Contests.Application.Contests.GetActive;
using LingEdu.Contests.Application.Contests.Join;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingEdu.WebApi.Controllers
{
    [Route("api/contests")]
    [ApiController]
    [Authorize]
    public class ContestsController : ControllerBase
    {
        private readonly ISender _sender;

        public ContestsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(new GetActiveContestsQuery(userId));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("{id:guid}/join")]
        public async Task<IActionResult> Join(Guid id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(new JoinContestCommand(userId, id));

            if (result.IsSuccess)
            {
                return Ok(new { message = "Joined" });
            }

            return result.Error switch
            {
                ContestErrors.ContestNotFound => NotFound(),
                ContestErrors.ContestNotActive => BadRequest(result.Error),
                ContestErrors.AlreadyJoined => Conflict(result.Error),
                _ => BadRequest(result.Error)
            };
        }
    }
}
