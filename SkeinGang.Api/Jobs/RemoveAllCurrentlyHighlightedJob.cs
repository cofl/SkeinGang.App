using Quartz;
using SkeinGang.Api.Services;

namespace SkeinGang.Api.Jobs;

internal class RemoveAllCurrentlyHighlightedJob(
    HighlightService highlight,
    ILogger<RemoveAllCurrentlyHighlightedJob> logger
) : IJob
{
    public static readonly JobKey Key = new(nameof(RemoveAllCurrentlyHighlightedJob), nameof(HighlightService));

    public Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Beginning scheduled removal job {Job} at {UtcNow}.",
            nameof(RemoveAllCurrentlyHighlightedJob), DateTime.UtcNow);
        highlight.RemoveAllCurrentlyHighlighted();
        return Task.CompletedTask;
    }
}
