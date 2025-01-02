using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkeinGang.Data.Entities;

namespace SkeinGang.Data.Context;

internal class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> entity)
    {
        entity.HasKey(e => e.TeamId).HasName("statics_pkey");

        entity.ToTable("statics");

        entity.HasIndex(e => e.Name, "static_constraint_unique_names").IsUnique();

        entity.HasIndex(e => e.ContentDifficulty, "static_content_difficulty");

        entity.HasIndex(e => e.ContentFocus, "static_content_type");

        entity.HasIndex(e => e.DiscordServerId, "static_discord_server_id");

        entity.HasIndex(e => e.ExperienceLevel, "static_experience_level");

        entity.HasIndex(e => e.Region, "static_region");

        entity.HasIndex(e => e.Slug, "static_slug");

        entity.HasIndex(e => e.Slug, "statics_slug_key").IsUnique();

        entity.HasIndex(e => e.Slug, "statics_slug_key1").IsUnique();

        entity.Property(e => e.TeamId).HasColumnName("id");
        entity.Property(e => e.ContentDifficulty)
            .HasMaxLength(255)
            .HasColumnName("content_difficulty_level");
        entity.Property(e => e.ContentFocus)
            .HasMaxLength(255)
            .HasColumnName("content_type");
        entity.Property(e => e.DayOfWeekRaid)
            .HasMaxLength(255)
            .HasColumnName("day_of_week_raid");
        entity.Property(e => e.Description)
            .HasDefaultValueSql("''::character varying")
            .HasColumnName("description");
        entity.Property(e => e.DiscordChannelId)
            .HasMaxLength(255)
            .HasColumnName("discord_channel_id");
        entity.Property(e => e.DiscordGroupId)
            .HasMaxLength(255)
            .HasColumnName("discord_group_id");
        entity.Property(e => e.DiscordServerId).HasColumnName("discord_server_id");
        entity.Property(e => e.ExperienceLevel)
            .HasMaxLength(255)
            .HasColumnName("experience_level");
        entity.Property(e => e.FollowsSummerTime)
            .HasDefaultValue(false)
            .HasColumnName("follows_summer_time");
        entity.Property(e => e.HoldSlots)
            .HasDefaultValue(0)
            .HasColumnName("hold_slots");
        entity.Property(e => e.Icon).HasColumnName("icon");
        entity.Property(e => e.IsArchived)
            .HasDefaultValue(false)
            .HasColumnName("is_archived");
        entity.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
        entity.Property(e => e.Region)
            .HasMaxLength(255)
            .HasColumnName("region");
        entity.Property(e => e.RunDuration)
            .HasDefaultValueSql("'02:00:00'::interval")
            .HasColumnName("run_duration");
        entity.Property(e => e.Slug)
            .HasComputedColumnSql(
                @"TRIM(BOTH '-'::text FROM regexp_replace(lower((name)::text), '[^a-z0-9\\-_]+'::text, '-'::text, 'gi'::text))",
                true)
            .HasColumnName("slug");
        entity.Property(e => e.TimeOfRaid)
            .HasPrecision(6)
            .HasColumnName("time_of_raid");
        entity.Property(e => e.TimeZone).HasColumnName("time_zone");

        entity.HasOne(d => d.DiscordServer).WithMany(p => p.Statics)
            .HasForeignKey(d => d.DiscordServerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fkecl0d3kccgn2lomvv8y8g4lou");

        entity.HasOne(d => d.TeamDetail).WithOne()
            .HasForeignKey<Team>(d => d.TeamId)
            .HasConstraintName("fk_static_team_detail");
    }
}
