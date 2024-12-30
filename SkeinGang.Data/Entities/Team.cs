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

    public ContentDifficulty ContentDifficulty { get; set; }

    public ContentFocus ContentFocus { get; set; }


    [MaxLength(500)]
    public string Description { get; set; } = "";

    public string? DiscordChannelId { get; set; }

    public string? DiscordGroupId { get; set; }

    public ExperienceLevel ExperienceLevel { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public Region Region { get; set; }

    public byte[]? Icon { get; set; }

    public bool IsArchived { get; set; }

    public long DiscordServerId { get; set; }
    public virtual DiscordServer DiscordServer { get; set; } = null!;

    [MaxLength(100)]
    public string Slug { get; private set; } = null!;

    public IsoDayOfWeek DayOfWeekRaid { get; set; }
    public LocalTime TimeOfRaid { get; set; }
    public Duration RunDuration { get; set; }
    public DateTimeZone TimeZone { get; set; } = null!;
    public bool FollowsSummerTime { get; set; }

    public virtual ICollection<TeamMembership> TeamMemberships { get; set; } = null!;
    public virtual TeamDetail TeamDetail { get; set; } = null!;
}
