using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Quartz;
using SkeinGang.Api.Jobs;
using SkeinGang.Data.Context;

namespace SkeinGang.Api.Services;

// TODO: get the state into an external store so the application itself can be truly stateless.
// That way we can ditch the IHighlightService song-and-dance.
// Maybe just put it in the database? Should be (relatively) cheap, just need to start doing migrations.
public class HighlightService(IHighlightService serviceSingleton, DataContext context)
{
    public List<long> CurrentlyHighlighted => serviceSingleton.RefillCurrentlyHighlighted(context);
    public void RemoveInvalidCurrentlyHighlighted() => serviceSingleton.RemoveInvalidCurrentlyHighlighted(context);
    public void RemoveAllCurrentlyHighlighted() => serviceSingleton.RemoveAllCurrentlyHighlighted();
}

internal static class HighlightServiceExtensions
{
    internal static IServiceCollection AddHighlightService(this IServiceCollection services)
    {
        services.AddSingleton<IHighlightService, HighlightServiceSingleton>();
        services.AddScoped<HighlightService>();
        services.AddTransient<RemoveInvalidCurrentlyHighlightedJob>();
        services.AddTransient<RemoveAllCurrentlyHighlightedJob>();
        return services;
    }

    internal static void ScheduleHighlightJobs(this IServiceCollectionQuartzConfigurator quartz)
    {
        // Schedule invalid currently-highlighted teams to be removed every hour.
        quartz.ScheduleJob<RemoveInvalidCurrentlyHighlightedJob>(
            trigger => trigger
                .WithIdentity(nameof(RemoveInvalidCurrentlyHighlightedJob), nameof(HighlightService))
                .WithCronSchedule("0 0 * * * ?"),
            job => job.WithIdentity(RemoveInvalidCurrentlyHighlightedJob.Key)
        );
        
        // Schedule all currently-highlighted teams to be removed at midnight on Sunday.
        quartz.ScheduleJob<RemoveAllCurrentlyHighlightedJob>(
            trigger => trigger
                .WithIdentity(nameof(RemoveAllCurrentlyHighlightedJob), nameof(HighlightService))
                .WithCronSchedule("0 0 0 ? * SUN"),
            job => job.WithIdentity(RemoveAllCurrentlyHighlightedJob.Key)
        );
    }
}

file class HighlightServiceSingleton(ILogger<HighlightService> logger) : IHighlightService
{
    private const int HighlightCount = 1;
    private const int HistoryCount = 3;

    private List<long> CurrentlyHighlightedTeamIds { get; } = [];
    private List<long> RecentlyHighlightedTeamIds { get; } = [];

    public List<long> RefillCurrentlyHighlighted(DataContext context)
    {
        if (CurrentlyHighlightedTeamIds.Count >= HighlightCount)
            return CurrentlyHighlightedTeamIds;

        var newTeams = context.Teams
            .Where(t => t.TeamDetail.TotalMembers > 0
                        && t.TeamDetail.EmptySlots > 0
                        && t.Description != ""
                        && !t.IsArchived
                        && !RecentlyHighlightedTeamIds.Contains(t.TeamId))
            .OrderBy(r => EF.Functions.Random())
            .Take(HighlightCount - CurrentlyHighlightedTeamIds.Count)
            .Select(t => t.TeamId);
        
        CurrentlyHighlightedTeamIds.AddRange(newTeams);
        RecentlyHighlightedTeamIds.AddRange(newTeams);
        
        if (RecentlyHighlightedTeamIds.Count >= HistoryCount)
            RecentlyHighlightedTeamIds.RemoveRange(0, RecentlyHighlightedTeamIds.Count - HistoryCount);

        return CurrentlyHighlightedTeamIds;
    }

    public void RemoveInvalidCurrentlyHighlighted(DataContext context)
    {
        var badTeams = context.Teams
            .Where(t => CurrentlyHighlightedTeamIds.Contains(t.TeamId) && (
                t.Description == ""
                || t.TeamDetail.TotalMembers <= 0
                || t.TeamDetail.EmptySlots <= 0
                || t.IsArchived))
            .Select(t => t.TeamId)
            .ToHashSet();
        CurrentlyHighlightedTeamIds.RemoveAll(id => badTeams.Contains(id));
        logger.LogInformation("Removed {count} invalid highlighted teams at {UtcNow}.", badTeams.Count, DateTime.UtcNow);
    }

    public void RemoveAllCurrentlyHighlighted()
    {
        var count = CurrentlyHighlightedTeamIds.Count;
        CurrentlyHighlightedTeamIds.Clear();
        logger.LogInformation("Removed {count} invalid highlighted teams at {UtcNow}.", count, DateTime.UtcNow);
    }
}