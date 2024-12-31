using Microsoft.EntityFrameworkCore;
using SkeinGang.AdminUI.Models;
using SkeinGang.Data.Context;
using SkeinGang.Data.Entities;

namespace SkeinGang.AdminUI.Services;

public class DiscordServerService(DataContext context)
{
    public List<DiscordServerDto> FindAll() =>
        context.DiscordServers
            .AsNoTracking()
            .OrderBy(a => a.Id)
            .ProjectToDto()
            .ToList();

    public DiscordServerDto Create(DiscordServerDto server)
    {
        if (server.Id != null)
            throw new ArgumentException(
                paramName: nameof(server),
                message: $"{nameof(server.Id)} must be null."
            );
        var model = context.DiscordServers.Add(new DiscordServer
        {
            ServerId = server.ServerId,
            ServerName = server.ServerName,
        });
        context.SaveChanges();
        return model.Entity.ToDto();
    }

    public DiscordServerDto Update(DiscordServerDto discordServer)
    {
        var model = context.DiscordServers
            .First(server => server.Id == discordServer.Id);
        model.ApplyUpdate(discordServer);
        context.SaveChanges();
        return model.ToDto();
    }
}
