using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkeinGang.Data.Entities;

namespace SkeinGang.Data.Context;

internal class DiscordServerConfiguration : IEntityTypeConfiguration<DiscordServer>
{
    public void Configure(EntityTypeBuilder<DiscordServer> entity)
    {
        entity.HasKey(e => e.Id).HasName("discord_servers_pkey");

        entity.ToTable("discord_servers");

        entity.HasIndex(e => e.ServerId, "discord_server_id");
        entity.HasIndex(e => e.ServerName, "discord_server_name");

        entity.Property(e => e.Id)
            .HasColumnName("id");
        entity.Property(e => e.ServerId)
            .HasColumnName("server_id");
        entity.Property(e => e.ServerName)
            .HasColumnName("server_name");
    }
}
