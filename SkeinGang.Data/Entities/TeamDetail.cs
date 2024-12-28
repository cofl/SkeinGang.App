using Microsoft.EntityFrameworkCore;
using SkeinGang.Data.Context;

namespace SkeinGang.Data.Entities;

[EntityTypeConfiguration<TeamDetailConfiguration, TeamDetail>]
public class TeamDetail
{
    public long TeamId { get; }
    public int TeamCapacity { get; }
    public int TotalMembers { get; }
    public int HonoraryMembers { get; }
    public int ActiveMembers { get; }
    public int RepMembers { get; }
    public int EmptySlots { get; }
}
