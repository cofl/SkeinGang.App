using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkeinGang.Data.Entities;

namespace SkeinGang.Data.Context;

internal class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> entity)
    {
        entity.HasKey(e => e.PlayerId).HasName("players_pkey");

        entity.ToTable("players");
        entity.HasIndex(e => e.GameAccount, "players_gw2name_key").IsUnique();

        entity.Property(e => e.PlayerId)
            .HasColumnName("id");
        entity.Property(e => e.DiscordAccountName)
            .HasColumnName("discord_name");
        entity.Property(e => e.DiscordAccountId)
            .HasColumnName("discord_user_id");
        entity.Property(e => e.GameAccount)
            .HasColumnName("gw2name");
    }
}
