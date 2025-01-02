using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkeinGang.Data.Entities;

namespace SkeinGang.Data.Context;

internal class TeamMembershipConfiguration : IEntityTypeConfiguration<TeamMembership>
{
    public void Configure(EntityTypeBuilder<TeamMembership> entity)
    {
        entity.HasKey(e => e.Id).HasName("static_membership_pkey");

        entity.ToTable("static_memberships");

        entity.HasIndex(e => new { e.PlayerId, e.StaticId }, "membership_constraint_unique_membership").IsUnique();

        entity.HasIndex(e => e.PlayerId, "membership_player_id");

        entity.HasIndex(e => e.StaticId, "membership_static_id");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.MembershipType)
            .HasMaxLength(255)
            .HasColumnName("membership_type");
        entity.Property(e => e.PlayerId).HasColumnName("player_id");
        entity.Property(e => e.StaticId).HasColumnName("static_id");

        entity.HasOne(d => d.Player).WithMany(p => p.TeamMemberships)
            .HasForeignKey(d => d.PlayerId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fkopfwm8xpjt6adk5483mel4dn3");

        entity.HasOne(d => d.Team).WithMany(p => p.TeamMemberships)
            .HasForeignKey(d => d.StaticId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fkne54l7wh7x56wtbj0lew4mlsr");
    }
}
