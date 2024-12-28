using System.Text.Json.Serialization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NodaTime;
using SkeinGang.Api.Util;

namespace SkeinGang.Api.Models;

/// <summary>
/// A status report.
/// </summary>
[DefaultRequired]
public class StatusReport
{
    /// <summary>
    /// The current status of the service.
    /// </summary>
    public HealthStatus Status { get; }
    
    /// <summary>
    /// The time this status report was generated.
    /// </summary>
    public string Time { get; }

    /// <summary>
    /// The duration it took to check the service health, in milliseconds.
    /// </summary>
    public double Duration { get; }

    /// <summary>
    /// The status results for each individual check.
    /// </summary>
    public IReadOnlyDictionary<string, StatusResult> Results { get; }

    /// <summary>
    /// The result of a single healthcheck.
    /// </summary>
    /// <param name="Status">The current status of the check.</param>
    /// <param name="Description">A description for the check.</param>
    /// <param name="Data">Any additional data returned by the check.</param>
    [DefaultRequired]
    public record StatusResult(
        HealthStatus Status,
        [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        string? Description,
        [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        IReadOnlyDictionary<string, object>? Data
    );

    /// <summary>
    /// Create a status report from a <paramref name="report" />.
    /// </summary>
    /// <param name="report">A health report.</param>
    public StatusReport(HealthReport report)
    {
        Status = report.Status;
        Time = SystemClock.Instance.GetCurrentInstant().ToString();
        Duration = report.TotalDuration.TotalMilliseconds;
        Results = new Dictionary<string, StatusResult>(report.Entries
            .Select(entry =>
                new KeyValuePair<string, StatusResult>(entry.Key, new StatusResult(
                    Status: entry.Value.Status,
                    Description: entry.Value.Description,
                    Data: entry.Value.Data.Any() ? entry.Value.Data : null
                ))
            ));
    }
}