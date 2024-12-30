using Microsoft.EntityFrameworkCore;
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
        var model = context.Players.Add(new Player
        {
            GameAccount = player.GameAccount,
            DiscordAccountName = player.DiscordAccountName,
            DiscordAccountId = player.DiscordAccountId,
        }).Entity;
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