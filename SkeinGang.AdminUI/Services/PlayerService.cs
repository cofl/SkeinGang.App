using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SkeinGang.AdminUI.Models;
using SkeinGang.Data.Context;
using SkeinGang.Data.Entities;

namespace SkeinGang.AdminUI.Services;

public class PlayerService(DataContext context)
{
    public List<PlayerDto> FindAll(string? searchString = null) =>
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

    public List<PlayerDto> FindIncomplete() =>
        context.Players.AsNoTracking()
            .Where(player => string.IsNullOrEmpty(player.GameAccount)
                             || string.IsNullOrEmpty(player.DiscordAccountName))
            .OrderBy(player => player.PlayerId)
            .ProjectToDto()
            .ToList();

    public PlayerDto? FindById(long id) =>
        context.Players
            .AsNoTracking()
            .ProjectToDto()
            .FirstOrDefault(player => player.Id == id);

    public PlayerDto Create(PlayerDto player)
    {
        if (player.Id != null)
            throw new ArgumentException(
                paramName: nameof(player),
                message: $"{nameof(player.Id)} must be null."
            );
        var model = new Player
        {
            GameAccount = player.GameAccount,
            DiscordAccountName = player.DiscordAccountName,
            DiscordAccountId = player.DiscordAccountId,
        };
        context.Players.Add(model);
        context.SaveChanges();
        return model.ToDto();
    }

    public PlayerDto Update(PlayerDto player)
    {
        var model = context.Players
            .AsTracking()
            .Include(entity => entity.TeamMemberships)
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

    internal static Player EntityWithDtoRelated(this EntityEntry<Player> entry)
    {
        entry.Reference(entity => entity.TeamMemberships).Load();
        return entry.Entity;
    }
}