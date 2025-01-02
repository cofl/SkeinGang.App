using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using SkeinGang.Data.Context;

namespace SkeinGang.Data.Entities;

[EntityTypeConfiguration<DiscordServerConfiguration, DiscordServer>]
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
public class DiscordServer
{
    public long Id { get; set; }
    public required long? ServerId { get; set; }

    [MaxLength(100)]
    [MinLength(2)]
    public required string? ServerName { get; set; }

    public virtual ICollection<Team> Statics { get; set; } = null!;
}
