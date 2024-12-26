using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using SkeinGang.Data.Enums;

namespace SkeinGang.Api.Views;

public abstract class AnyTeam(Domain.Team team)
{
    public string Name { get; } = team.Name;
    public string Slug { get; } = team.Slug;
    public string Description { get; } = team.Description;
    public ContentDifficulty Difficulty { get; } = team.ContentDifficulty;
    public ContentFocus ContentFocus { get; } = team.ContentFocus;
    public ExperienceLevel ExperienceLevel { get; } = team.ExperienceLevel;
    public Region Region { get; } = team.Region;
    public string Roster { get; }
    public bool IsArchived { get; } = team.IsArchived;

    public List<TeamTime> Times { get; } = [
        new()
        {
            Day = team.DayOfWeekRaid,
            Time = team.TimeOfRaid,
            DurationSeconds = (uint)Math.Truncate(team.RunDuration.TotalSeconds),
            TimeZone = team.TimeZone,
            FollowsSummerTime = team.FollowsSummerTime,
        }
    ];
}

public sealed class TeamDto(Domain.Team team) : AnyTeam(team);

public sealed class TeamWithMembersDto(Domain.Team team) : AnyTeam(team)
{
    [JsonPropertyOrder(100)]
    public ICollection<TeamMember> Members { get; } =
        team.TeamMemberships
            .Select(member => new TeamMember(member))
            .ToList();
}

public sealed class TeamTime
{
    public required IsoDayOfWeek Day { get; init; }
    public required LocalTime Time { get; init; }
    
    /// <summary>
    /// The run duration in seconds.
    /// </summary>
    [JsonPropertyName("duration")]
    public required uint DurationSeconds { get; init; }
    public required DateTimeZone TimeZone { get; init; }
    
    /// <summary>
    /// If the team follows summer time (if true, the time-of-day is constant in the local timezone regardless of UTC offset).
    /// </summary>
    /// <remarks>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Value</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term>True</term>
    ///         <description>No time-of-day adjustment is made (the time-of-day remains fixed in the local timezone).</description>
    ///     </item>
    ///     <item>
    ///         <term>False</term>
    ///         <description>The team maintains the same offset relative to UTC (the time-of-day shifts in the local timezone).</description>
    ///     </item>
    /// </list>
    /// </remarks>
    public required bool FollowsSummerTime { get; init; }
}

public sealed record TeamMember(MembershipType MembershipType, string Username, string Account, long? DiscordId)
{
    public TeamMember(Domain.TeamMembership member) : this(
        MembershipType: member.MembershipType,
        Username: member.Player.DiscordAccountName,
        Account: member.Player.GameAccount,
        DiscordId: member.Player.DiscordAccountId)
    {
    }
}