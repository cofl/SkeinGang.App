using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SkeinGang.Data.Context;

namespace SkeinGang.Data.Entities;

[EntityTypeConfiguration<PlayerConfiguration, Player>]
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
public class Player
{
    public long PlayerId { get; private set; }
    
    [MaxLength(255)]
    public required string GameAccount { get; set; }
    
    [MaxLength(32), MinLength(2)]
    public required string DiscordAccountName { get; set; }
    public long? DiscordAccountId { get; set; }
    
    public virtual ICollection<TeamMembership> TeamMemberships { get; set; } = null!;
}
