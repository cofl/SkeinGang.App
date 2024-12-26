using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SkeinGang.Data.Context;

namespace SkeinGang.Data.Entities;

[EntityTypeConfiguration<DiscordServerConfiguration, DiscordServer>]
public class DiscordServer
{
    public long Id { get; private set; }
    public required long? ServerId { get; set; }
    
    [MaxLength(100), MinLength(2)]
    public required string? ServerName { get; set; }

    public virtual ICollection<Team> Statics { get; set; } = new List<Team>();
}
