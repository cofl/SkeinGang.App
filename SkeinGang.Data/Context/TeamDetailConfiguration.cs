using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkeinGang.Data.Entities;

namespace SkeinGang.Data.Context;

internal class TeamDetailConfiguration : IEntityTypeConfiguration<TeamDetail>
{
    public void Configure(EntityTypeBuilder<TeamDetail> entity)
    {
        entity.HasKey(e => e.TeamId);
        entity.ToView("team_detail");

        entity.Property(e => e.ActiveMembers).HasColumnName("active_members");
        entity.Property(e => e.EmptySlots).HasColumnName("empty_slots");
        entity.Property(e => e.HonoraryMembers).HasColumnName("honorary_members");
        entity.Property(e => e.MaximumMembers).HasColumnName("maximum_members");
        entity.Property(e => e.RepMembers).HasColumnName("rep_members");
        entity.Property(e => e.TeamId).HasColumnName("team_id");
        entity.Property(e => e.TotalMembers).HasColumnName("total_members");
    }
}
