using Microsoft.EntityFrameworkCore;
using SkeinGang.Data.Context;

namespace SkeinGang.Data.Entities;

[EntityTypeConfiguration<TeamDetailConfiguration, TeamDetail>]
public class TeamDetail
{
    public long TeamId { get; }
    public long TeamCapacity { get; }
    public long TotalMembers { get; }
    public long HonoraryMembers { get; }
    public long ActiveMembers { get; }
    public long RepMembers { get; }
    public long EmptySlots { get; }
}
