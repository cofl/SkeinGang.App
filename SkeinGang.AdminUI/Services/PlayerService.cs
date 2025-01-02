using Microsoft.EntityFrameworkCore;
using SkeinGang.AdminUI.Models;
using SkeinGang.Data;
using SkeinGang.Data.Context;
using SkeinGang.Data.Entities;

namespace SkeinGang.AdminUI.Services;

public class PlayerService(DataContext context)
{
    internal List<PlayerDto> FindAll(string? searchString = null) =>
        (context.Players.AsNoTracking() switch
        {
            var filtered when searchString != null =>
                filtered.Where(player => player.GameAccount.Contains(searchString))
                    .OrderBy(player => player.PlayerId)
                    .Take(50),
            var unfiltered => unfiltered
                .OrderBy(player => player.PlayerId)
        })
        .ProjectToDto()
        .ToList();

    internal List<PlayerDto> FindIncomplete() =>
        context.Players.AsNoTracking()
            .Where(player => string.IsNullOrEmpty(player.GameAccount)
                             || string.IsNullOrEmpty(player.DiscordAccountName))
            .OrderBy(player => player.PlayerId)
            .ProjectToDto()
            .ToList();

    internal PlayerDto? FindById(long id) =>
        context.Players
            .AsNoTracking()
            .ProjectToDto()
            .FirstOrDefault(player => player.Id == id);

    internal PlayerDto Create(PlayerDto player)
    {
        Assert.MustBeNull(player.Id);
        var model = context.Players.AddNew(player.ToEntity());
        context.SaveChanges();
        return model.ToDto();
    }

    internal PlayerDto Update(PlayerDto player)
    {
        Assert.MustNotBeNull(player.Id);
        var model = context.Players
            .AsTracking()
            .IncludePlayerDtoRelated()
            .First(entity => entity.PlayerId == player.Id);
        model.ApplyUpdate(player);
        context.SaveChanges();
        return model.ToDto();
    }
}

file static class PlayerServiceExtensions
{
    internal static IQueryable<Player> IncludePlayerDtoRelated(this IQueryable<Player> teams)
        => teams.Include(t => t.TeamMemberships);
}