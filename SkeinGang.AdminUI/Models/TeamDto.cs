﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using SkeinGang.Data.Enums;

namespace SkeinGang.AdminUI.Models;

public record TeamDto
{
    [HiddenInput, DataType(DataType.Text)]
    public long TeamId { get; set; }
    
    public string Name { get; set; }
    public Region Region { get; set; }
    public ContentFocus ContentFocus { get; set; }
    public ContentDifficulty ContentDifficulty { get; set; }
    public ExperienceLevel ExperienceLevel { get; set; }
    public IsoDayOfWeek DayOfRaid { get; set; }
    public LocalTime TimeOfRaid { get; set; }
    public int HoldSlots { get; set; }
    public string Description { get; set; }
    public bool IsArchived { get; set; }
    public DateTimeZone TimeZone { get; set; }
    public bool FollowsSummerTime { get; set; }
}