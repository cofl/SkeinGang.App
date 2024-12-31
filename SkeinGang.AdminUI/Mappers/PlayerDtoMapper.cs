using Riok.Mapperly.Abstractions;
using SkeinGang.AdminUI.Models;
using SkeinGang.Data.Entities;

namespace SkeinGang.AdminUI.Mappers;

[Mapper]
public static partial class PlayerDtoMapper
{
    [MapProperty(nameof(Player.PlayerId), nameof(PlayerDto.Id))]
    [MapProperty(nameof(Player.TeamMemberships), nameof(PlayerDto.MembershipCount))]
    public static partial PlayerDto ToDto(this Player player);
    public static partial IQueryable<PlayerDto> ProjectToDto(this IQueryable<Player> players);

    [MapperIgnoreSource(nameof(PlayerDto.MembershipCount))]
    [MapperIgnoreSource(nameof(PlayerDto.Id))]
    [MapperIgnoreTarget(nameof(Player.TeamMemberships))]
    public static partial void ApplyUpdate([MappingTarget] this Player player, PlayerDto dto);
    
    private static int? MembershipToCount(ICollection<TeamMembership> teamMemberships)
        => teamMemberships.Count;
}
