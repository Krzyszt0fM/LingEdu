using System.Security.Claims;
using LingEdu.Subscriptions.Application.Plans;
using LingEdu.Subscriptions.Application.Status;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LingEdu.WebApi.Controllers;

[ApiController]
[Route("api/subscriptions")]
public class SubscriptionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubscriptionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("plans")]
    public async Task<IActionResult> Plans(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetPlansQuery(), cancellationToken);
        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("me/status")]
    public async Task<IActionResult> Status(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        if (userId == Guid.Empty)
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(new GetSubscriptionStatusQuery(userId), cancellationToken);
        return Ok(result.Value);
    }

    [Authorize]
    [HttpPost("me/activate/{planId}")]
    public async Task<IActionResult> Activate(Guid planId, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        if (userId == Guid.Empty)
        {
            return Unauthorized();
        }

        var result = await _mediator.Send(new ActivateSubscriptionCommand(userId, planId, DateTime.UtcNow.AddMonths(1)), cancellationToken);
        if (!result.IsSuccess)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }

    private Guid GetUserId()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue(ClaimTypes.Name);
        return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
    }
}
