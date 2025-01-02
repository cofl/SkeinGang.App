using Riok.Mapperly.Abstractions;
using SkeinGang.AdminUI.Models;
using SkeinGang.Data.Entities;

namespace SkeinGang.AdminUI.Mappers;

[Mapper]
public static partial class DiscordServerDtoMapper
{
    [MapperIgnoreSource(nameof(DiscordServer.Statics))]
    public static partial DiscordServerDto ToDto(this DiscordServer discordServer);
    public static partial IQueryable<DiscordServerDto> ProjectToDto(this IQueryable<DiscordServer> discordServers);

    [MapperIgnoreTarget(nameof(DiscordServer.Statics))]
    public static partial DiscordServer ToEntity(this DiscordServerDto dto);
    
    [MapperIgnoreTarget(nameof(DiscordServer.Statics))]
    public static partial void ApplyUpdate([MappingTarget] this DiscordServer discordServer, DiscordServerDto dto);
}
