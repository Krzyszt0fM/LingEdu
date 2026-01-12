using System;
using System.Security.Claims;
using System.Threading.Tasks;
using LingEdu.Ranking.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingEdu.WebApi.Controllers
{
    [Route("api/ranking")]
    [ApiController]
    [Authorize]
    public class RankingController : ControllerBase
    {
        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService)
        {
            _rankingService = rankingService;
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTop([FromQuery] int limit = 10)
        {
            var result = await _rankingService.GetTopAsync(limit);
            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var result = await _rankingService.GetUserScoreAsync(userId);
            return Ok(result);
        }
    }
}
