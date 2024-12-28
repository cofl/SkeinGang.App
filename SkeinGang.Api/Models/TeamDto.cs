using System.Text.Json.Serialization;
using NodaTime;
using SkeinGang.Api.Util;
using SkeinGang.Data.Enums;

namespace SkeinGang.Api.Models;

/// <summary>
/// Base class for team DTOs.
/// </summary>
/// <param name="team">A team entity to populate the DTO with.</param>
public abstract class AnyTeam(Domain.Team team)
{
    /// <summary>
    /// Name of the team.
    /// </summary>
    public string Name { get; } = team.Name;
    
    /// <summary>
    /// URL-ified named of the team, used as an ID.
    /// </summary>
    public string Slug { get; } = team.Slug;
    
    /// <summary>
    /// Use-provided description for the team.
    /// </summary>
    public string Description { get; } = team.Description;
    
    /// <summary>
    /// Difficulty the team plays at.
    /// </summary>
    public ContentDifficulty Difficulty { get; } = team.ContentDifficulty;
    
    /// <summary>
    /// Content the team primarily plays.
    /// </summary>
    public ContentFocus ContentFocus { get; } = team.ContentFocus;
    
    /// <summary>
    /// Experience rating for the team.
    /// </summary>
    public ExperienceLevel ExperienceLevel { get; } = team.ExperienceLevel;
    
    /// <summary>
    /// Global region the team is based in.
    /// </summary>
    /// <remarks>
    /// This does *not* correspond to the Guild Wars 2 server cluster the team plays on.
    /// </remarks>
    public Region Region { get; } = team.Region;

    /// <summary>
    /// Summary of information about the team roster.
    /// </summary>
    public TeamRoster Roster { get; } = new()
    {
        Maximum = team.TeamDetail.TeamCapacity,
        Total = team.TeamDetail.TotalMembers,
        Held = team.HoldSlots,
        Active = team.TeamDetail.ActiveMembers,
    };
    
    /// <summary>
    /// If the team has been archived.
    /// </summary>
    public bool IsArchived { get; } = team.IsArchived;

    /// <summary>
    /// List of times the team plays at.
    /// </summary>
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

/// <summary>
/// A team, without detailed member information.
/// </summary>
[DefaultRequired]
public sealed class TeamDto(Domain.Team team) : AnyTeam(team);

/// <summary>
/// A team, including detailed member information.
/// </summary>
[DefaultRequired]
public sealed class TeamWithMembersDto(Domain.Team team) : AnyTeam(team)
{
    /// <summary>
    /// List of members on the team.
    /// </summary>
    [JsonPropertyOrder(100)]
    public ICollection<TeamMember> Members { get; } =
        team.TeamMemberships
            .Select(member => new TeamMember(member))
            .ToList();
}

/// <summary>
/// A time the team raids at.
/// </summary>
public sealed class TeamTime
{
    /// <summary>
    /// Day-of-week the team raids.
    /// </summary>
    public required IsoDayOfWeek Day { get; init; }
    
    /// <summary>
    /// Time-of-day the team raids, in UTC.
    /// </summary>
    public required LocalTime Time { get; init; }
    
    /// <summary>
    /// The run duration in seconds.
    /// </summary>
    [JsonPropertyName("duration")]
    public required uint DurationSeconds { get; init; }
    
    /// <summary>
    /// An IANA time zone ID. 
    /// </summary>
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

/// <summary>
/// Summary information about a team's roster.
/// </summary>
public sealed class TeamRoster
{
    /// <summary>
    /// Active member capacity of the team.
    /// </summary>
    public required int Maximum { get; init; }
    
    /// <summary>
    /// Total count of all active and inactive members on the roster.
    /// This may be larger than the team's capacity.
    /// </summary>
    public required int Total { get; init; }
    
    /// <summary>
    /// Slots held for future members (counts against the team capacity).
    /// </summary>
    public required int Held { get; init; }
    
    /// <summary>
    /// Active members and representatives, excluding honorary members.
    /// </summary>
    public required int Active { get; init; }
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