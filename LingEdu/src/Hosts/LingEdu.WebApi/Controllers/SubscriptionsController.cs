using LingEdu.Subscriptions.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using LingEdu.Users.Domain.Users;

namespace LingEdu.WebApi.Controllers
{
    [Route("api/subscriptions")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IUserRepository _userRepository;

        public SubscriptionsController(ISubscriptionService subscriptionService, IUserRepository userRepository)
        {
            _subscriptionService = subscriptionService;
            _userRepository = userRepository;

        }

        [HttpGet("plans")]
        public async Task<IActionResult> GetPlans()
        {
            var plans = await _subscriptionService.GetPlansAsync();
            return Ok(plans);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMySubscription()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var status = await _subscriptionService.GetUserStatusAsync(userId);
            return Ok(status);
        }

        [HttpPost("purchase/{planId}")]
        [Authorize]
        public async Task<IActionResult> Purchase(Guid planId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            await _subscriptionService.ActivateSubscriptionAsync(userId, planId);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
            {
                return NotFound();
            }

            user.MarkAsPremium();
            await _userRepository.UpdateAsync(user);

            return Ok(new { message = "Subscription activated" });
        }
    }
}