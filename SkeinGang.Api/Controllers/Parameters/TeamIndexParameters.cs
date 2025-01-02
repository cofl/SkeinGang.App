using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NodaTime;
using SkeinGang.Api.Util;
using SkeinGang.Data.Enums;

namespace SkeinGang.Api.Controllers.Parameters;

/// <summary>
///     Parameter binding for <see cref="TeamsController.Index" />.
/// </summary>
public class TeamIndexParameters
{
    internal const int MaximumTeamCapacity = 1000;

    /// <summary>
    ///     Region of the teams.
    /// </summary>
    /// <example>"eu"</example>
    public List<Region> Region { get; set; } = [];

    /// <summary>
    ///     Focus of the teams.
    /// </summary>
    /// <example>"hot"</example>
    public List<ContentFocus> ContentType { get; set; } = [];

    /// <summary>
    ///     The skill rating of the team.
    /// </summary>
    /// <example>"progression"</example>
    public List<ExperienceLevel> Rating { get; set; } = [];

    /// <summary>
    ///     The difficulty level the team plays at.
    /// </summary>
    /// <example>"normal"</example>
    public List<ContentDifficulty> Difficulty { get; set; } = [];

    /// <summary>
    ///     Minimum remaining slots in the team roster.
    /// </summary>
    /// <example>1</example>

    [Range(0, MaximumTeamCapacity)]
    [DefaultValue(0)]
    public int? MinSlots { get; set; }

    /// <summary>
    ///     Maximum remaining slots in the team roster, inclusive.
    /// </summary>
    /// <example>10</example>

    [Range(0, MaximumTeamCapacity)]
    [DefaultValue(MaximumTeamCapacity)]
    public int? MaxSlots { get; set; }

    internal bool HasSlots => MinSlots.HasValue && MaxSlots.HasValue;

    /// <summary>
    ///     The days the team raids on.
    /// </summary>
    /// <example>"monday"</example>
    public List<IsoDayOfWeek> Day { get; set; } = [];

    /// <summary>
    ///     The time of day the team plays at, in UTC.
    /// </summary>
    /// <example>"15:30"</example>
    public LocalTime? Time { get; set; }

    /// <summary>
    ///     The last time the team starts to play at, in UTC.
    ///     This option is ignored if <see cref="Time">`time`</see> is not present, or if it is before
    ///     <see cref="Time">`time`</see>.
    ///     Defaults to 30 minutes past <see cref="Time">`time`</see>.
    /// </summary>
    /// <example>"16:00"</example>
    public LocalTime? UntilTime { get; set; }

    internal bool HasDuration => Duration.HasValue || MaxDuration.HasValue;

    /// <summary>
    ///     How long the team plays for, in seconds.
    /// </summary>
    /// <example>7200</example>

    [Range(0, long.MaxValue)]
    public long? Duration { get; set; }

    /// <summary>
    ///     The maximum time the team plays for, in seconds, inclusive.
    /// </summary>
    /// <example>10800</example>

    [Range(0, long.MaxValue)]
    public long? MaxDuration { get; set; }

    /// <summary>
    ///     Archive status.
    /// </summary>
    /// <example>false</example>

    [DefaultValue("false")]
    public AnyBoolean? Archived { get; set; } = AnyBoolean.False;

    /// <summary>
    ///     Maximum number of results to retrieve.
    /// </summary>
    /// <example>10</example>

    [Range(1, 1000)]
    [DefaultValue(1000)]
    public int Limit { get; set; } = 1000;

    /// <summary>
    ///     Number of results to skip.
    /// </summary>

    [Range(0, int.MaxValue)]
    [DefaultValue(0)]
    public int Offset { get; set; } = 0;
}
