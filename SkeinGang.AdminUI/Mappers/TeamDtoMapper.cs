using Riok.Mapperly.Abstractions;
using SkeinGang.AdminUI.Models;
using SkeinGang.Data.Entities;

namespace SkeinGang.AdminUI.Mappers;

[Mapper]
public static partial class TeamDtoMapper
{
    [MapperIgnoreSource(nameof(Team.DiscordServer))]
    [MapperIgnoreSource(nameof(Team.DiscordChannelId))]
    [MapperIgnoreSource(nameof(Team.DiscordGroupId))]
    [MapperIgnoreSource(nameof(Team.Icon))]
    [MapperIgnoreSource(nameof(Team.Slug))]
    [MapperIgnoreSource(nameof(Team.RunDuration))]
    [MapperIgnoreSource(nameof(Team.TeamMemberships))]
    [MapperIgnoreSource(nameof(Team.TeamDetail))]
    [MapProperty(nameof(Team.DayOfWeekRaid), nameof(TeamDto.DayOfRaid))]
    public static partial TeamDto ToDto(this Team team);

    [MapperIgnoreSource(nameof(TeamDto.TeamId))]
    [MapperIgnoreTarget(nameof(Team.DiscordServer))]
    [MapperIgnoreTarget(nameof(Team.DiscordChannelId))]
    [MapperIgnoreTarget(nameof(Team.DiscordGroupId))]
    [MapperIgnoreTarget(nameof(Team.Icon))]
    [MapperIgnoreTarget(nameof(Team.Slug))]
    [MapperIgnoreTarget(nameof(Team.RunDuration))]
    [MapperIgnoreTarget(nameof(Team.TeamMemberships))]
    [MapperIgnoreTarget(nameof(Team.TeamDetail))]
    [MapProperty(nameof(TeamDto.DayOfRaid), nameof(Team.DayOfWeekRaid))]
    public static partial Team ToEntity(this TeamDto dto);

    public static partial IQueryable<TeamDto> ProjectToDto(this IQueryable<Team> teams);

    [MapperIgnoreTarget(nameof(Team.DiscordServer))]
    [MapperIgnoreTarget(nameof(Team.DiscordChannelId))]
    [MapperIgnoreTarget(nameof(Team.DiscordGroupId))]
    [MapperIgnoreTarget(nameof(Team.Icon))]
    [MapperIgnoreTarget(nameof(Team.Slug))]
    [MapperIgnoreTarget(nameof(Team.RunDuration))]
    [MapperIgnoreTarget(nameof(Team.TeamMemberships))]
    [MapperIgnoreTarget(nameof(Team.TeamDetail))]
    [MapProperty(nameof(TeamDto.DayOfRaid), nameof(Team.DayOfWeekRaid))]
    public static partial void ApplyUpdate([MappingTarget] this Team team, TeamDto teamDto);
}
