using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using SkeinGang.Data.Enums;

namespace SkeinGang.AdminUI.Models;

public record TeamDto
{
    [HiddenInput]
    [DataType(DataType.Text)]
    [FromForm]
    public long? TeamId { get; set; }

    [FromForm]
    public long? DiscordServerId { get; set; }

    [FromForm]
    public required string Name { get; set; }

    [FromForm]
    public Region Region { get; set; }

    [FromForm]
    public ContentFocus ContentFocus { get; set; }

    [FromForm]
    public ContentDifficulty ContentDifficulty { get; set; }

    [FromForm]
    public ExperienceLevel ExperienceLevel { get; set; }

    [FromForm]
    public IsoDayOfWeek DayOfRaid { get; set; }

    [FromForm]
    public LocalTime TimeOfRaid { get; set; }

    [FromForm]
    [Range(0, 10)]
    public int HoldSlots { get; set; }

    [FromForm]
    public string Description { get; set; } = "";

    [FromForm]
    public bool IsArchived { get; set; }

    [FromForm]
    public required DateTimeZone TimeZone { get; set; }

    [FromForm]
    public bool FollowsSummerTime { get; set; }
}
