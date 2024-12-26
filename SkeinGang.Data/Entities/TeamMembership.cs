using Microsoft.EntityFrameworkCore;
using SkeinGang.Data.Context;
using SkeinGang.Data.Enums;

namespace SkeinGang.Data.Entities;

[EntityTypeConfiguration<TeamMembershipConfiguration, TeamMembership>]
public class TeamMembership
{
    public long Id { get; set; }
    public long PlayerId { get; set; }
    public long StaticId { get; set; }
    public MembershipType MembershipType { get; set; }
    
    public virtual Player Player { get; set; } = null!;
    public virtual Team Team { get; set; } = null!;
}
