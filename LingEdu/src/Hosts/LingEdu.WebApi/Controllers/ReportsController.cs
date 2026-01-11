using System;
using System.Security.Claims;
using System.Threading.Tasks;
using LingEdu.Reports.Application.Reports.GetMyReport;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingEdu.WebApi.Controllers
{
    [Route("api/reports")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly ISender _sender;

        public ReportsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var result = await _sender.Send(new GetMyReportQuery(userId));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
