using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using SkeinGang.Data.Enums;

namespace SkeinGang.AdminUI.Models;

public record TeamMemberDto
{
    public long PlayerId { get; set; }
    public long TeamId { get; set; }
    public long MembershipId { get; set; }

    [FromForm]
    [Required]
    public string GameAccount { get; init; }

    [FromForm]
    [Required]
    public string DiscordAccountName { get; init; }

    public MembershipType MembershipType { get; set; }
}
