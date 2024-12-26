using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SkeinGang.Data.Context;

namespace SkeinGang.Data.Entities;

[EntityTypeConfiguration<PlayerConfiguration, Player>]
public class Player
{
    public long PlayerId { get; private set; }
    
    [MaxLength(255)]
    public required string GameAccount { get; init; }
    
    [MaxLength(32), MinLength(2)]
    public required string DiscordAccountName { get; init; }
    public long? DiscordAccountId { get; private set; }
    
    public virtual ICollection<TeamMembership> TeamMemberships { get; set; }
}
