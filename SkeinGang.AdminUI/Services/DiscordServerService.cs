using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkeinGang.AdminUI.Models;
using SkeinGang.Data;
using SkeinGang.Data.Context;

namespace SkeinGang.AdminUI.Services;

public class DiscordServerService(DataContext context)
{
    internal List<DiscordServerDto> FindAll() =>
        context.DiscordServers
            .AsNoTracking()
            .OrderBy(a => a.Id)
            .ProjectToDto()
            .ToList();

    internal DiscordServerDto Create(DiscordServerDto server)
    {
        Assert.MustBeNull(server.Id);
        var model = context.DiscordServers.AddNew(server.ToEntity());
        context.SaveChanges();
        return model.ToDto();
    }

    internal DiscordServerDto Update(DiscordServerDto discordServer)
    {
        Assert.MustNotBeNull(discordServer.Id);
        var model = context.DiscordServers
            .First(server => server.Id == discordServer.Id);
        model.ApplyUpdate(discordServer);
        context.SaveChanges();
        return model.ToDto();
    }

    internal IEnumerable<SelectListItem> AsSelectOptions(long? selected) =>
        FindAll()
            .Select(server => new SelectListItem
            {
                Text = server.ServerName,
                Value = server.Id.ToString(),
                Selected = server.Id == selected,
            });
}