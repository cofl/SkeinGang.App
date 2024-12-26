using Quartz;
using SkeinGang.Api.Services;

namespace SkeinGang.Api.Jobs;

internal class RemoveInvalidCurrentlyHighlightedJob(HighlightService highlight, ILogger<RemoveInvalidCurrentlyHighlightedJob> logger) : IJob
{
    public static readonly JobKey Key = new(nameof(RemoveInvalidCurrentlyHighlightedJob), nameof(HighlightService));
    public Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Beginning scheduled removal job {Job} at {UtcNow}.", nameof(RemoveInvalidCurrentlyHighlightedJob), DateTime.UtcNow);
        highlight.RemoveInvalidCurrentlyHighlighted();
        return Task.CompletedTask;
    }
}