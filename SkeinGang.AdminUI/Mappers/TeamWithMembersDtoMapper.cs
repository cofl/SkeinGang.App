using Riok.Mapperly.Abstractions;
using SkeinGang.AdminUI.Models;
using SkeinGang.Data.Entities;

namespace SkeinGang.AdminUI.Mappers;

[Mapper]
[UseStaticMapper(typeof(TeamMemberDtoMapper))]
public static partial class TeamWithMembersDtoMapper
{
    [MapperIgnoreSource(nameof(Team.DiscordServer))]
    [MapperIgnoreSource(nameof(Team.DiscordChannelId))]
    [MapperIgnoreSource(nameof(Team.DiscordGroupId))]
    [MapperIgnoreSource(nameof(Team.Icon))]
    [MapperIgnoreSource(nameof(Team.Slug))]
    [MapperIgnoreSource(nameof(Team.RunDuration))]
    [MapperIgnoreSource(nameof(Team.TeamDetail))]
    [MapProperty(nameof(Team.DayOfWeekRaid), nameof(TeamWithMembersDto.DayOfRaid))]
    [MapProperty(nameof(Team.TeamMemberships), nameof(TeamWithMembersDto.Members))]
    public static partial TeamWithMembersDto ToWithMembersDto(this Team team);
}
