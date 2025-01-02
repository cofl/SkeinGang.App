using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SkeinGang.Api.Models;

namespace SkeinGang.Api.Controllers;

/// <summary>
///     A controller for retrieving the system health.
/// </summary>
/// <param name="health">The health service.</param>
[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class StatusController(HealthCheckService health) : ControllerBase
{
    /// <summary>
    ///     Query the system health.
    /// </summary>
    /// <remarks>
    ///     Execute health checks and report their results.
    ///     The output of this endpoint may be cached for short durations.
    /// </remarks>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>A health report.</returns>
    [HttpGet]
    [ProducesResponseType<StatusReport>(StatusCodes.Status200OK)]
    [ProducesResponseType<StatusReport>(StatusCodes.Status503ServiceUnavailable)]
    [ResponseCache(Duration = 5, VaryByHeader = "User-Agent")]
    public async Task<IActionResult> GetStatus(CancellationToken cancellationToken)
    {
        var report = await health.CheckHealthAsync(cancellationToken);
        return report.Status switch
        {
            HealthStatus.Healthy or HealthStatus.Degraded => Ok(new StatusReport(report)),
            _ => StatusCode(StatusCodes.Status503ServiceUnavailable, new StatusReport(report)),
        };
    }
}
