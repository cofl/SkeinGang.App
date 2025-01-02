using Riok.Mapperly.Abstractions;
using SkeinGang.AdminUI.Models;
using SkeinGang.Data.Entities;

namespace SkeinGang.AdminUI.Mappers;

[Mapper]
public static partial class TeamMemberDtoMapper
{
    [MapProperty(nameof(TeamMembership.Id), nameof(TeamMemberDto.MembershipId))]
    [MapProperty(nameof(TeamMembership.Player.GameAccount), nameof(TeamMemberDto.GameAccount))]
    [MapProperty(nameof(TeamMembership.Player.DiscordAccountName), nameof(TeamMemberDto.DiscordAccountName))]
    [MapProperty(nameof(TeamMembership.StaticId), nameof(TeamMemberDto.TeamId))]
    [MapperIgnoreSource(nameof(TeamMembership.Team))]
    public static partial TeamMemberDto ToDto(this TeamMembership membership);
}
