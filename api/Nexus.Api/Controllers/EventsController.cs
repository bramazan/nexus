using Microsoft.AspNetCore.Mvc;
using Nexus.Application.Common.Interfaces;

namespace Nexus.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly INormalizationService _normalizationService;

        public EventsController(INormalizationService normalizationService)
        {
            _normalizationService = normalizationService;
        }

        [HttpPost("normalize")]
        public async Task<IActionResult> NormalizeEvents(CancellationToken cancellationToken)
        {
            await _normalizationService.NormalizePendingEventsAsync(cancellationToken);
            return Ok(new { message = "Normalization Job Triggered" });
        }
    }
}
