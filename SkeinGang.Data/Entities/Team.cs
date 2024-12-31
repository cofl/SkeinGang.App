using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using SkeinGang.Data.Context;
using SkeinGang.Data.Enums;

namespace SkeinGang.Data.Entities;

[EntityTypeConfiguration<TeamConfiguration, Team>]
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
public class Team
{
    public long TeamId { get; private set; }
    
    public int HoldSlots { get; set; }

    public required ContentDifficulty ContentDifficulty { get; set; }

    public required ContentFocus ContentFocus { get; set; }


    [MaxLength(500)]
    public string Description { get; set; } = "";

    public string? DiscordChannelId { get; set; }

    public string? DiscordGroupId { get; set; }

    public required ExperienceLevel ExperienceLevel { get; set; }

    [MaxLength(100)]
    public required string Name { get; set; } = null!;

    public required Region Region { get; set; }

    public byte[]? Icon { get; set; }

    public bool IsArchived { get; set; }

    public required long DiscordServerId { get; set; }
    public virtual DiscordServer DiscordServer { get; set; } = null!;

    [MaxLength(100)]
    public string Slug { get; private set; } = null!;

    public required IsoDayOfWeek DayOfWeekRaid { get; set; }
    public required LocalTime TimeOfRaid { get; set; }
    public Duration RunDuration { get; set; }
    public required DateTimeZone TimeZone { get; set; } = null!;
    public bool FollowsSummerTime { get; set; }

    public virtual ICollection<TeamMembership> TeamMemberships { get; set; } = null!;
    public virtual TeamDetail TeamDetail { get; set; } = null!;
}
