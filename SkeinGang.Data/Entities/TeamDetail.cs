using Microsoft.EntityFrameworkCore;
using SkeinGang.Data.Context;

namespace SkeinGang.Data.Entities;

[EntityTypeConfiguration<TeamDetailConfiguration, TeamDetail>]
public class TeamDetail
{
    public long TeamId { get; private init; }
    public int TeamCapacity { get; private init; }
    public int TotalMembers { get; private init; }
    public int HonoraryMembers { get; private init; }
    public int ActiveMembers { get; private init; }
    public int RepMembers { get; private init; }
    public int EmptySlots { get; private init; }
}
